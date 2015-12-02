using Reository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Repository.Models
{
    public class Vote
    {
        public Vote()
        {
            VotedUsers = new HashSet<Choose>();
        }

        [Display(Name = "VoteId: ")]
        [Required]
        public Guid VoteId { get; set; }

        public string Image { get; set; }

        [Display(Name = "Title: ")]
        [Required]
        public string VoteTitle { get; set; }
        public Guid? QuestionnaireId { get; set; }
        public virtual Questionnaire Questionnaire { get; set; }

        public virtual ICollection<Choose> VotedUsers { get; set; }
    }
}
