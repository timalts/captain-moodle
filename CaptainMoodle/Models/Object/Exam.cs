using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Object
{
    public class Exam
    {
        public int Id { get; set; }
        public Course Course { get; set; }
        public User User { get; set; }
        public String Description { get; set; }
        public String Grade { get; set; }
    }
}