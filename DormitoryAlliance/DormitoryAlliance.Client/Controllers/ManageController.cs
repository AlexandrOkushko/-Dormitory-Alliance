using DormitoryAlliance.Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DormitoryAlliance.Client.Controllers
{
    [Authorize(Roles = "admin")]
    public class ManageController : Controller
    {
        private DormitoryAllianceDbContext _context;

        public ManageController(DormitoryAllianceDbContext context)
        {
            _context = context;
        }

        #region Student

        // GET: Manage
        public ActionResult StudentIndex()
        {
            var students = _context.Students
                .Include(s => s.Group)
                .Include(s => s.Room).Take(300).ToList();

            return View(students);
        }

        // GET: Manage/StudentDetails/5
        public ActionResult StudentDetails(int id)
        {
            var student = _context.Students
                .Include(s => s.Group)
                .Include(s => s.Room)
                .FirstOrDefault(x => x.Id == id);

            return View(student);
        }

        // GET: Manage/StudentCreate
        public ActionResult StudentCreate()
        {
            var rooms = _context.Rooms
                        .Include(room => room.Dormitory)
                        .OrderBy(room => room.Number)
                        .Select(room => new
                        {
                            room.Id,
                            Name = $"{(room.Number % 100 == 27 ? (room.Number.ToString()[0] + "12A") : room.Number)} ({room.DormitoryId})"
                        });

            ViewBag.Rooms = new SelectList(rooms, "Id", "Name");

            var groups = _context.Groups
                .OrderBy(group => group.Name);

            ViewBag.Groups = new SelectList(groups, "Id", "Name");

            return View();
        }

        // POST: Manage/StudentCreate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StudentCreate(IFormCollection collection)
        {
            try
            {
                var student = new Student
                {
                    Name = collection["Name"],
                    Surname = collection["Surname"],
                    Patronymic = collection["Patronymic"],
                    RoomId = int.TryParse(collection["RoomId"], out int rid) ? rid : 0,
                    GroupId = int.TryParse(collection["GroupId"], out int gid) ? gid : 0,
                    Course = int.TryParse(collection["Course"], out int course) ? course : 0
                };

                _context.Students.Add(student);

                _context.SaveChanges();

                return RedirectToAction(nameof(StudentIndex));
            }
            catch
            {
                return View();
            }
        }

        // GET: Manage/StudentEdit/5
        public ActionResult StudentEdit(int? id)
        {
            if (id != null)
            {
                var student = _context.Students
                    .Include(s => s.Group)
                    .Include(s => s.Room)
                    .FirstOrDefault(x => x.Id == id);

                if (student?.Name != null)
                {
                    var rooms = _context.Rooms
                        .Include(room => room.Dormitory)
                        .OrderBy(room => room.Number)
                        .Select(room => new
                        {
                            room.Id,
                            Name = $"{(room.Number % 100 == 27 ? (room.Number.ToString()[0] + "12A" ) : room.Number)} ({room.DormitoryId})"
                        });

                    ViewBag.Rooms = new SelectList(rooms, "Id", "Name");

                    var groups = _context.Groups
                        .OrderBy(group => group.Name);

                    ViewBag.Groups = new SelectList(groups, "Id", "Name");

                    return View(student);
                }
            }

            Console.WriteLine("Incorect id");
            return RedirectToAction(nameof(StudentIndex));
        }

        // POST: Manage/StudentEdit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StudentEdit(int id, IFormCollection collection)
        {
            try
            {
                var student = new Student
                {
                    Id = id,
                    Name = collection["Name"],
                    Surname = collection["Surname"],
                    Patronymic = collection["Patronymic"],
                    RoomId = int.TryParse(collection["RoomId"], out int rid) ? rid : 0,
                    GroupId = int.TryParse(collection["GroupId"], out int gid) ? gid : 0,
                    Course = int.TryParse(collection["Course"], out int course) ? course : 0
                };

                _context.Students.Update(student);

                _context.SaveChanges();

                return RedirectToAction(nameof(StudentIndex));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View();
            }
        }

        // GET: Manage/StudentDelete/5
        public ActionResult StudentDelete(int id)
        {
            var student = _context.Students.Where(s => s.Id == id)
                .Include(x => x.Group)
                .Include(x => x.Room).ThenInclude(x => x.Dormitory)
                .FirstOrDefault();

            if (student == null)
            {
                return RedirectToAction(nameof(StudentIndex));
            }

            return View(student);
        }

        // POST: Manage/StudentDelete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StudentDelete(int id, IFormCollection collection)
        {
            try
            {
                var student = _context.Students.Find(id);

                if (student != null)
                {
                    _context.Students.Remove(student);

                    _context.SaveChanges();
                }

                return RedirectToAction(nameof(StudentIndex));
            }
            catch
            {
                return View();
            }
        }

        #endregion Student

        #region Group

        // GET: Manage
        public ActionResult GroupIndex()
        {
            var groups = _context.Groups.Take(300).ToList();

            return View(groups);
        }

        // GET: Manage/GroupDetails/5
        public ActionResult GroupDetails(int id)
        {
            var student = _context.Groups.FirstOrDefault(x => x.Id == id);

            return View(student);
        }

        // GET: Manage/GroupCreate
        public ActionResult GroupCreate()
        {
            return View();
        }

        // POST: Manage/GroupCreate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GroupCreate(IFormCollection collection)
        {
            try
            {
                var group = new Group { Name = collection["Name"] };

                if (_context.Groups?.Count(x => x.Name == group.Name) > 0)
                {
                    Console.WriteLine("This name already exist.");

                    return View();
                }

                _context.Groups.Add(group);

                _context.SaveChanges();

                return RedirectToAction(nameof(GroupIndex));
            }
            catch
            {
                return View();
            }
        }

        // GET: Manage/GroupEdit/5
        public ActionResult GroupEdit(int? id)
        {
            if (id != null)
            {
                var group = _context.Groups.FirstOrDefault(x => x.Id == id);

                if (group?.Name != null)
                {
                    return View(group);
                }
            }

            Console.WriteLine("Incorect id");
            return RedirectToAction(nameof(StudentIndex));
        }

        // POST: Manage/GroupEdit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GroupEdit(int id, IFormCollection collection)
        {
            try
            {
                var group = new Group
                {
                    Id = id,
                    Name = collection["Name"]
                };

                _context.Groups.Update(group);

                _context.SaveChanges();

                return RedirectToAction(nameof(GroupIndex));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View();
            }
        }

        // GET: Manage/GroupDelete/5
        public ActionResult GroupDelete(int id)
        {
            var group = _context.Groups.FirstOrDefault(x => x.Id == id);

            if (group == null)
            {
                return RedirectToAction(nameof(GroupIndex));
            }

            return View(group);
        }

        // POST: Manage/GroupDelete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GroupDelete(int id, IFormCollection collection)
        {
            try
            {
                var group = _context.Groups.Find(id);

                if (group != null)
                {
                    _context.Groups.Remove(group);

                    _context.SaveChanges();
                }

                return RedirectToAction(nameof(GroupIndex));
            }
            catch
            {
                return View();
            }
        }

        #endregion Group

    }
}
