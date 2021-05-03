using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaptaineMoodle.Models
{
    public class Event
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Text { get; set; }
        public string Color { get; set; }
    }
}
