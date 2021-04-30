using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaptaineMoodle.Areas.Identity.Data;
using CaptaineMoodle.Data;
using CaptaineMoodle.Models;
using Microsoft.AspNetCore.Identity;

namespace CaptaineMoodle.Controllers
{
    public class AssignementController : Controller
    {
        public readonly AuthDbContext _context;
        private readonly UserManager<User> _userManager;

        public AssignementController(AuthDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //Method to get the current login user
        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: AssignementController
        public ActionResult Index()
        {
            var assignements = _context.Assignement.ToList();

            return View(assignements);
        }

        // GET: AssignementController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AssignementController/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        // POST: AssignementController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("CourseId,Description,Grade")] Assignement assignement)
        {
            var course = await _context.Course.FindAsync(assignement.CourseId);

            if(course.UsersId != "")
            {
                char[] separators = new char[] { ' ', ';' };
                string[] users = course.UsersId.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                foreach (var user in users)
                {
                    var _assignement = new Assignement
                    {
                        CourseId = assignement.CourseId,
                        Description = assignement.Description,
                        Grade = assignement.Grade,
                        StudentId = user
                    };

                    _context.Add(_assignement);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Error));
            }
        }

        // GET: AssignementController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AssignementController/Edit/5
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

        // GET: AssignementController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AssignementController/Delete/5
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
