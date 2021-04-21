using CaptaineMoodle.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CaptaineMoodle.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public int Amount { get; set; }
        public string Semester { get; set; }
        public bool Paid { get; set; }
    }
}
