using DormitoryAlliance.Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DormitoryAlliance.Client.Controllers
{
    public class HomeController : Controller
    {
        private DormitoryAllianceDbContext _context;

        public HomeController(DormitoryAllianceDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("/scheme/dormitory{dormitoryId:int}")]
        public ViewResult Scheme(int dormitoryId)
        {
            var rooms =
                from room in _context.Rooms
                join d in _context.Dormitories
                    on room.DormitoryId equals d.Id
                where d.Id == dormitoryId
                select new Room
                {
                    Id = room.Id,
                    Number = room.Number,
                    DormitoryId = room.DormitoryId,
                    Dormitory = d
                };
            rooms = rooms.OrderBy(n => n.Number);
            return View(rooms);
        }

        public IActionResult InDevelopment()
        {
            return View();
        }

        [Authorize(Roles = "admin, user")]
        [HttpGet("/Home/Roommates{Id:int}")]
        public IActionResult Roommates(int Id)
        {
            var roommates =
                from student in _context.Students
                join @group in _context.Groups
                    on student.GroupId equals @group.Id
                where student.RoomId == Id
                select new Student
                {
                    Id = student.Id,
                    Name = student.Name,
                    Surname = student.Surname,
                    Patronymic = student.Patronymic,
                    GroupId = student.GroupId,
                    Group = @group,
                    Course = student.Course
                };

            return PartialView("_RoommatesModelPartial", roommates);
        }
    }
}
