using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaptaineMoodle.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string CreatorId { get; set; }
        public string UsersId { get; set; }
        public String Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}