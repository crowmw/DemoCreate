using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public int QuestionnaireId { get; set; }

        [Display(Name = "UserId: ")]
        public int UserId { get; set; }

        [Display(Name ="Title: ")]
        public string Title { get; set; }

        [Display(Name ="Vote1ID: ")]
        public int Vote1Id { get; set; }

        [Display(Name = "Vote2ID: ")]
        public int Vote2Id { get; set; }

        [Display(Name = "TimeOfCreation: ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TimeOfCreation { get; set; }

        [Display(Name ="Selecion: ")]
        public int Selection { get; set; }
        public virtual Vote Vote { get; set; }
        public virtual User User { get; set; }
    }


}