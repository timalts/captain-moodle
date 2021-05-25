using CaptaineMoodle.Areas.Identity.Data;
using CaptaineMoodle.Data;
using CaptaineMoodle.Models;
using CaptaineMoodle.ViewModels.ExamViewModels;
using CaptaineMoodle.ViewModels;
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
    public class ExamController : Controller
    {
        public readonly AuthDbContext _context;
        private readonly UserManager<User> _userManager;
        private object examCourseViewModel;

        public ExamController(AuthDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Exam
        public ActionResult Index()
        {
            var exams = from c in _context.Exam select c;
            var model = new ExamCourseViewModel();
            if (exams.Any())
            {
                foreach (var _exam in exams)
                {
                    var _course = _context.Course.Find(_exam.CourseId);
                    var examCourse = new ExamCourseModel
                    {
                        exam = _exam,
                        course = _course,
                    };

                    model.examCourseList.Add(examCourse);
                }

                return View(model);
            }
            else
            {
                return View(model);
            }
        }

        public async Task<ActionResult> Create()
        {
            IEnumerable<Course> courses;
            var usr = await GetCurrentUserAsync();

            if (User.IsInRole("Admin"))
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

        // POST: Exam/CreateExam
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Course,Description,Start,End")] Exam exam)
        {
            User usr = await GetCurrentUserAsync();
            exam.CreatorId = usr.Id;

            var nvc = Request.Form;
            string user_post = nvc["Course"];

            var sameuser = _context.Course.FirstOrDefault(u => u.Name == user_post);
            exam.Course = sameuser;

            _context.Add(exam);
            await _context.SaveChangesAsync();
            
            return (RedirectToAction(nameof(Index)));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var exam = await _context.Exam
                .FirstOrDefaultAsync(m => m.Id == id);
            var course = await _context.Course
                .FirstOrDefaultAsync(m => m.Id == exam.CourseId);
            if (exam == null)
            {
                return NotFound();
            }
            ExamCourseModel examCourse = new ExamCourseModel()
            {
                exam = exam,
                course = course,
            };

            return View(examCourse);
        }

        // POST: Exam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exam = await _context.Exam.FindAsync(id);
            _context.Exam.Remove(exam);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool ExamExists(int id)
        {
            return _context.Exam.Any(e => e.Id == id);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var exam = await _context.Exam.FindAsync(id);
            if (exam == null)
            {
                return NotFound();
            }
            var course = await _context.Course
                .FirstOrDefaultAsync(m => m.Id == exam.CourseId);
            ExamCourseModel examCourse = new ExamCourseModel()
            {
                exam = exam,
                course = course,
            };
            return View(examCourse);
        }

        // POST: Courses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseId,CreatorId,Description,UsersId,Start,End")] Exam exam)
        {
            if (id != exam.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamExists(exam.Id))
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
    }
}
