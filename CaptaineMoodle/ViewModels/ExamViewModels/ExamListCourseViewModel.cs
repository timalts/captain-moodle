using CaptaineMoodle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaptaineMoodle.ViewModels.ExamViewModels
{
    public class ExamListCourseViewModel
    {
        public Exam exam { get; set; }
        public ExamListCourseViewModel()
        {
            listCourse = new List<Course>();
        }

        public List<Course> listCourse;
    }
}
