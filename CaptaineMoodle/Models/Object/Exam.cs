using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaptaineMoodle.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public Course Course { get; set; }

        public int UserId { get; set; }
        public String Description { get; set; }
        public String Grade { get; set; }
    }
}