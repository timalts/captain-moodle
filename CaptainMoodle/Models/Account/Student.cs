using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Account
{
    public class Student
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public int Birth { get; set; }

        //Waiting for implementation
        //public List<Course> Course {get; set}
        public string Campus { get; set; }

        public string Contact { get; set; }

        //Waiting for implementation
        //public List<Payment> Payment {get; set}

        //Waiting for implementation
        //public List<Exam> Exam {get; set}

        //Waiting for implementation
        //public AssignementBook AssignementBook {get; set}
    }
}
