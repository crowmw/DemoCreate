using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reository.Models
{
    public class User_Questionnaire
    {
        public User_Questionnaire()
        { }

        public Guid User_QuestionnaireId { get; set; }
        public string UserId { get; set; }
        public Guid QuestionnaireId { get; set; }
    }
}