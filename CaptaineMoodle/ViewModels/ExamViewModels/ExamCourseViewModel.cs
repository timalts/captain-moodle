using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaptaineMoodle.Models;

namespace CaptaineMoodle.ViewModels
{
    public class ExamCourseViewModel
    {
        public ExamCourseViewModel()
        {
            examCourseList = new List<ExamCourseModel>();
        }

        public List<ExamCourseModel> examCourseList;
    }
}
