using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CaptaineMoodle.ViewModels
{
    public class CreateAssignementViewModel
    {
        [Required]
        public string CourseId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Grade { get; set; }


    }
}
