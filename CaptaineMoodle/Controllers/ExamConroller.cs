using CaptaineMoodle.Areas.Identity.Data;
using CaptaineMoodle.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public ExamController(AuthDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ExamConroller
        public ActionResult ListExams()
        {
            return View();
        }

        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public ActionResult ListCourses()
        {
            var courses = from c in _context.Course select c;


            return View(courses);
        }
    }
}
