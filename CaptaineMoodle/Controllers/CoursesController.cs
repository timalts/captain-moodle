using CaptaineMoodle.Areas.Identity.Data;
using CaptaineMoodle.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaptaineMoodle.Controllers
{
    public class CoursesController : Controller
    {
        public readonly AuthDbContext _context;
        private readonly UserManager<User> _userManager;

        public CoursesController(AuthDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //Method to get the current login user
        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


        // GET: Courses
        public ActionResult ListCourses()
        {
            var courses = from c in _context.Course select c;
            

            return View(courses);
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Get_ByUsersId()
        {

            User usr = await GetCurrentUserAsync();



            return View(_context.Course.Where(c => c.UsersId.Contains(usr.Id)));
        }

        // GET: Courses/Create
        public ActionResult CreateCourse()
        {
            return View();
        }

        // POST: Courses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCourse([Bind("Name,Description,Start,End")] Course course)
        {
            User usr = await GetCurrentUserAsync();
            course.CreatorId = usr.Id;
            course.UsersId = "";


            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }


        // GET: Courses/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Courses/Edit/5
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

        // GET: Courses/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Course.FindAsync(id);
            _context.Course.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ListCourses));
        }
    }
}
