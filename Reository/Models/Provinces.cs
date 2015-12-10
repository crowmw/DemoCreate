using Repository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Repository.Models
{
    public class Provinces
    {
        public Provinces()
        { }

        [Key]
        public int ProvinceId { get; set; }

        [Display(Name = "Województwo")]
        public string ProvinceName { get; set; }
    }
}