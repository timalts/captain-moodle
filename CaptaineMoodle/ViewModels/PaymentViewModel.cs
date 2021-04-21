using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaptaineMoodle.ViewModels
{
    public class PaymentViewModel
    {
        public PaymentViewModel()
        {
            Users = new List<string>();
        }
        public int Id { get; set; }
        public List<String> Users { get; set; }
        public int Amount { get; set; }
        public string Semester { get; set; }
        public bool Paid { get; set; }
    }
}
