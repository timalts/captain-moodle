using CaptaineMoodle.Areas.Identity.Data;
using CaptaineMoodle.Data;
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
                return RedirectToAction(nameof(ListCourses));
            }
            return RedirectToAction(nameof(ListCourses));
        }

        public ActionResult SuscribeCourse(int Id)
        {
            return View();
        }

        


        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);  
        }

        // POST: Courses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id ,[Bind("Id,Name,CreatorId,Description,UsersId,Start,End")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ListCourses));
            }
            return RedirectToAction(nameof(ListCourses));
        }

        [HttpPost]
        public async Task<IActionResult> Suscribe(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User usr = await GetCurrentUserAsync();

            var course = await _context.Course.FindAsync(id);

            course.UsersId += usr.Id + ";";
            _context.Course.Update(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ListCourses));
        }

        [HttpPost]
        public async Task<IActionResult> UnSuscribe(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User usr = await GetCurrentUserAsync();

            var course = await _context.Course.FindAsync(id);

            course.UsersId = course.UsersId.Replace(usr.Id + ";", "");
            _context.Course.Update(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ListCourses));
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
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
        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.Id == id);
        }
    }
}
