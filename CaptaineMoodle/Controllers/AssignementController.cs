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
using Microsoft.EntityFrameworkCore;
using CaptaineMoodle.Models.Object;

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
            IEnumerable<Course> courses = from c in _context.Course select c;
            ViewBag.courses = courses;

            return View(assignements);
        }

        // GET: AssignementController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AssignementController/Create
        public async Task<IActionResult> Create()
        {
            IEnumerable<Course> courses;
            var usr = await GetCurrentUserAsync();

            if(User.IsInRole("Admin"))
            {
                courses = from c in _context.Course select c;
            }
            else
            {
                courses = _context.Course.Where(c => c.CreatorId == usr.Id);
            }
            ViewBag.courses = courses;

            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        // POST: AssignementController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Id,Course,Description,Grade")] Assignement assignement)
        {
                User teacher = await GetCurrentUserAsync();
                var nvc = Request.Form;
                string user_post = nvc["Course"];

                var sameuser = _context.Course.FirstOrDefault(u => u.Name == user_post);
                assignement.Course = sameuser;

            char[] separators = new char[] { ' ', ';' };
            string[] users = assignement.Course.UsersId.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            if (assignement.Course.UsersId != "")
            {
                    foreach (var user in users)
                    {
                        Assignement _assignement = new Assignement();
                        _assignement.Course = assignement.Course;
                        _assignement.Description = assignement.Description;
                        _assignement.Grade = assignement.Grade;
                        _assignement.TeacherId = teacher.Id;
                        _assignement.StudentId = user;

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
        public async Task<IActionResult> Edit(int id)
        {
            var assignement = await _context.Assignement.FindAsync(id);
            if ( assignement == null)
            {
                return NotFound();
            }
            return View(assignement);

        }

        // POST: AssignementController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseId,Description,StudentId,TeacherId,Grade")] Assignement assignement)
        {
            string student = assignement.StudentId;
            if (id != assignement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    assignement.StudentId = student;
                    _context.Update(assignement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                    if (!AssignementExists(assignement.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: AssignementController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var _asssignement = await _context.Assignement
                .FirstOrDefaultAsync(m => m.Id == id);
            var _course = await _context.Course
                .FirstOrDefaultAsync(m => m.Id == _asssignement.CourseId);
            if (_asssignement == null)
            {
                return NotFound();
            }
            AssignementCoursModel assignementCours = new AssignementCoursModel()
            {
                assignement = _asssignement,
                course = _course,
            };

            return View(assignementCours);
        }

        // POST: Exam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assignement = await _context.Assignement.FindAsync(id);
            _context.Assignement.Remove(assignement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool ExamExists(int id)
        {
            return _context.Exam.Any(e => e.Id == id);
        }


        private bool AssignementExists(int id)
        {
            return _context.Assignement.Any(e => e.Id == id);
        }
    }
}
