using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Object
{
    public class AssignementBook
    {
        public int Id { get; set; }
        public List<Assignement> Assignements {get; set;}
    }
}