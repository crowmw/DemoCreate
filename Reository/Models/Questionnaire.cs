using Reository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Repository.Models
{
    public class Questionnaire
    {
        public Questionnaire()
        {
        }

        [Display(Name = "QuestionnaireID: ")]
        [Key]
        public Guid QuestionnaireId { get; set; }

        [Display(Name ="Title: ")]
        [Required]
        public string Title { get; set; }

        [Display(Name = "TimeOfCreation: ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TimeOfCreation { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
        
        public Guid? Vote1Id { get; set; }

        public Guid? Vote2Id { get; set; }
        [ForeignKey("Vote1Id")]
        public virtual Vote Vote1 { get; set; }
        [ForeignKey("Vote2Id")]
        public virtual Vote Vote2 { get; set; }
    }


}