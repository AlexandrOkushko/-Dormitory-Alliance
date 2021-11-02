using DormitoryAlliance.Client.Models;
using DormitoryAlliance.Client.Models.Repository;
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
        private IDormitoryAllianceRepository _repository;
        public int PageSize { get; set; } = 4;

        public HomeController(IDormitoryAllianceRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

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
        public ViewResult List(int? dormitoryId, int? groupId)
        {
            var students =
                from student in _repository.Students
                join @room in _repository.Rooms
                    on student.RoomId equals @room.Id
                join dormitory in _repository.Dormitories
                    on @room.DormitoryId equals dormitory.Id
                join @group in _repository.Groups
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

        [HttpGet("/d")]
        public ViewResult Dormitory()
        {
            var flours =
                from dormitory in _repository.Dormitories
                select dormitory.Id;

            return View(flours);
        }

        [HttpGet("/d{id:int}")]
        public ViewResult Floors(int id)
        {
            int floors =
                (from dormitory in _repository.Dormitories
                where dormitory.Id == id
                select dormitory.Floors).First();

            return View((id, floors));
        }

        [HttpGet("/d{dormitoryId:int}/f{floor:int}")]
        public ViewResult Rooms(int dormitoryId, int floor)
        {
            var rooms =
                from room in _repository.Rooms
                join d in _repository.Dormitories
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
    }
}
