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

        [HttpGet("/list/dormitory{dormitoryId:int}")]
        public ViewResult List(int? dormitoryId, int? groupId)
        {
            var s =
                (from student in _repository.Students
                    join dormitory in _repository.Dormitories on student.DormitoryId equals dormitory.Id
                    join @group in _repository.Groups on student.GroupId equals @group.Id
                    where dormitoryId == null || student.DormitoryId == dormitoryId
                    where groupId == null || student.GroupId == groupId
                    select new Student
                    {
                        Id = student.Id,
                        Name = student.Name ,
                        Surname = student.Surname ,
                        Patronymic = student.Patronymic ,
                        Room = student.Room ,
                        DormitoryId = student.DormitoryId ,
                        Dormitory = dormitory,
                        GroupId = student.GroupId,
                        Group = @group,
                        Course = student.Course
                    });
                


            return View(s);
        }
    }
}
