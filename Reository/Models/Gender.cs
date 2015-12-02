using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repository.Models
{
    public class Gender
    {
        public Gender() { }
        public Gender(string id, string name)
        {
            GenderId = id;
            GenderName = name;
        }
        public string GenderId { get; set; }
        public string GenderName { get; set; }
        //public virtual ICollection<User> User { get; set; }
    }
}