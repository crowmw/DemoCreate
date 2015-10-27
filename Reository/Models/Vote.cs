using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Repository.Models
{
    public class Vote
    {
        public Vote()
        {
            Vote_User = new HashSet<Vote_User>();
        }

        [Display(Name = "VoteId: ")]
        public int VoteId { get; set; }

        public string VoteImagePath { get; set; }

        [Display(Name = "Title: ")]
        public string VoteTitle { get; set; }

        public ICollection<Vote_User> Vote_User { get; set; }
    }
}
