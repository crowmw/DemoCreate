using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Repository.Models
{
    public class AgeRange
    {
        public AgeRange()
        { }

        [Key]
        public int AgeRangeId { get; set; }

        [Display(Name = "Przedział wiekowy")]
        public string AgeRangeName { get; set; }
    }
}