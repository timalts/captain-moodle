using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaptaineMoodle.Models
{
    public class Course
    {
        public int CreatorId { get; set; }
        public string Description {get; set; }
        public List<Assignement> Assignements {get; set; }
        public List<Exam> Exams {get; set; }
        public DateTime Start {get; set; }
        public DateTime End {get; set; }
    }
}