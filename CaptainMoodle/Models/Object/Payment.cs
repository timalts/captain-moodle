using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Payment
    {
        public int Amount {get; set;}
        public string Semester {get; set;}
        public bool Paid {get; set;}
    }
}