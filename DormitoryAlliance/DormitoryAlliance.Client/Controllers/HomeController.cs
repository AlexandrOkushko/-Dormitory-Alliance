using DormitoryAlliance.Client.Models;
using DormitoryAlliance.Client.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DormitoryAlliance.Client.Controllers
{
    public class HomeController : Controller
    {
        private DormitoryAllianceDbContext _context;
        public int PageSize { get; set; } = 4;

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

        [HttpGet("/list")]
        [HttpGet("/list/dormitory{dormitoryId:int}")]
        [HttpGet("/list/dormitory{dormitoryId:int}/group{groupId:int}")]
        [Obsolete]
        public ViewResult List(int? dormitoryId, int? groupId)
        {
            var students =
                from student in _context.Students
                join @room in _context.Rooms
                    on student.RoomId equals @room.Id
                join dormitory in _context.Dormitories
                    on @room.DormitoryId equals dormitory.Id
                join @group in _context.Groups
                    on student.GroupId equals @group.Id
                
                where dormitoryId == null || @room.DormitoryId == dormitoryId
                where groupId == null || student.GroupId == groupId
                select new Student
                {
                    Id = student.Id,
                    Name = student.Name,
                    Surname = student.Surname,
                    Patronymic = student.Patronymic,
                    RoomId = student.RoomId,
                    Room = @room,
                    GroupId = student.GroupId,
                    Group = @group,
                    Course = student.Course
                };

            return View(students);
        }

        [Authorize(Roles = "admin, user")]
        [HttpGet("/d")]
        public ViewResult Dormitory()
        {
            var flours =
                from dormitory in _context.Dormitories
                select dormitory.Id;

            return View(flours);
        }

        [Authorize(Roles = "admin, user")]
        [HttpGet("/d{id:int}")]
        public ViewResult Floors(int id)
        {
            int floors =
                (from dormitory in _context.Dormitories
                where dormitory.Id == id
                select dormitory.Floors).First();

            return View((id, floors));
        }

        [Authorize(Roles = "admin, user")]
        [HttpGet("/d{dormitoryId:int}/f{floor:int}")]
        public ViewResult Rooms(int dormitoryId, int floor)
        {
            var rooms =
                from room in _context.Rooms
                join d in _context.Dormitories
                    on room.DormitoryId equals d.Id
                where d.Id == dormitoryId 
                && room.Number > floor * 100
                && room.Number < floor * 100 + 100
                select new Room
                {
                    Id = room.Id,
                    Number = room.Number,
                    DormitoryId = room.DormitoryId,
                    Dormitory = d
                };

            return View(rooms);
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
