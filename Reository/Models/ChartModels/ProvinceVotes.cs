using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repository.Models.ChartModels
{
    public class ProvinceVotes
    {
        public ProvinceVotes() { }
        public string ProvinceName { get; set; }
        public int Votes { get; set; }
    }
}