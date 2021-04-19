using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaptaineMoodle.Models
{
    public class Assignement
    {
        public Course Course { get; set; }
        public int Id { get; set; }
        public String Description { get; set; }
        public String Grade { get; set; }
    }
}