using Repository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Reository.Models
{
    public class Choose
    {
        public Choose() { }

        [Required]
        public Guid ChooseId { get; set; }

        public Guid VoteId { get; set; }
        public virtual Vote vote { get; set; }

        public string UserId { get; set; }
        public virtual User user { get; set; }
    }
}