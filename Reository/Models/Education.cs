using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Repository.Models
{
    public class Education
    {
        public Education() { }

        [Key]
        public int EducationId { get; set; }

        [Display(Name = "Wykształcenie")]
        public string EducationName { get; set; }

    }
}