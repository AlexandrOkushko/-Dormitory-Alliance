using DormitoryAlliance.Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DormitoryAlliance.Client.Controllers
{
    public class ManageController : Controller
    {
        private DormitoryAllianceDbContext _context;

        public ManageController(DormitoryAllianceDbContext context)
        {
            _context = context;
        }

        // GET: Manage
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var students = _context.Students
                .Include(s => s.Group)
                .Include(s => s.Room).ToList();

            return View(students);
        }

        // GET: Manage/StudentDetails/5
        [Authorize(Roles = "admin, user")]
        public ActionResult StudentDetails(int id)
        {
            var student = _context.Students
                .Include(s => s.Group)
                .Include(s => s.Room)
                .FirstOrDefault(x => x.Id == id);

            return View(student);
        }

        // GET: Manage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Manage/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Manage/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Manage/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Manage/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
