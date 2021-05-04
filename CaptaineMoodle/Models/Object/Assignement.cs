using CaptaineMoodle.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CaptaineMoodle.Models
{
    public class Assignement
    {
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public string Grade { get; set; }
        public string StudentId { get; set; }
        public string TeacherId { get; set; }
    }
}