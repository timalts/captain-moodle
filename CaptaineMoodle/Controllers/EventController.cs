using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaptaineMoodle.Areas.Identity.Data;
using CaptaineMoodle.Data;
using CaptaineMoodle.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaptaineMoodle.Controllers
{
    [Produces("application/json")]
    [Route("api/Events")]
    public class EventsController : Controller
    {
        public readonly AuthDbContext _context;
        private readonly UserManager<User> _userManager;
        public EventsController(AuthDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        //Method to get the current login user
        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: api/Events
        [HttpGet]
        public async Task<IEnumerable<Event>> GetEvents([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            User usr = await GetCurrentUserAsync();

            IEnumerable<Course> courses;
            IEnumerable<Exam> exams;

            if(await _userManager.IsInRoleAsync(usr, "Admin"))
            {
                 courses = _context.Course;
                 exams = _context.Exam;
            }
            else if(await _userManager.IsInRoleAsync(usr, "Teacher"))
            {
                 courses = _context.Course.Where(c => c.CreatorId == usr.Id);
                 exams = _context.Exam.Where(c => c.CreatorId == usr.Id);
            }
            else
            {
                 courses = _context.Course.Where(c => c.UsersId.Contains(usr.Id));
                 exams = _context.Exam.Where(c => c.UsersId.Contains(usr.Id));
            }
            
            List<Event> events = new List<Event>();

            foreach (var course in courses)
            {
                if (course.Start >= start && course.End <= end)
                {
                    var _event = new Event
                    {
                        Start = course.Start,
                        End = course.End,
                        Text = course.Name+Environment.NewLine+Environment.NewLine+course.Description,
                        Color = "Blue"
                    };

                    events.Add(_event);
                }
            }

            foreach (var exam in exams)
            {
                if (exam.Start >= start && exam.End <= end)
                {
                    var course = await _context.Course.FindAsync(exam.CourseId);

                    var _event = new Event
                    {
                        Start = exam.Start,
                        End = exam.End,
                        Text = course.Name+Environment.NewLine+Environment.NewLine+exam.Description,
                        Color = "Yellow"
                    };

                    events.Add(_event);
                }
            }
            return events;
        }

    }

}
