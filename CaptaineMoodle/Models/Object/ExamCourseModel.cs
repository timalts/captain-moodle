using CaptaineMoodle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaptaineMoodle.Models
{
    public class ExamCourseModel
    {
        public Exam exam { get; set; }
        public Course course { get; set; }
    }
}
