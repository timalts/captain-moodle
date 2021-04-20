using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaptaineMoodle.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int Amount { get; set; }
        public string Semester { get; set; }
        public bool Paid { get; set; }
    }
}
