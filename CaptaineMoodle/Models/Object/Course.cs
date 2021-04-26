using CaptaineMoodle.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaptaineMoodle.Models
{
    public class Course
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string CreatorId { get; set; }
        public string Description {get; set; }

        public string UsersId { get; set; }
        public DateTime Start {get; set; }
        public DateTime End {get; set; }
    }
}