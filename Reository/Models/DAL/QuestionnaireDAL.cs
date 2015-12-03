using Repository.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reository.Models.DAL
{
    public class QuestionnaireDAL
    {
        public static bool Add(Questionnaire questionnaire)
        {
            var result = false;
            try
            {
                using (DCContext db = new DCContext())
                {
                    if(questionnaire != null)
                    {
                        questionnaire.QuestionnaireId = Guid.NewGuid();
                        questionnaire.Vote1.VoteId = Guid.NewGuid();
                        questionnaire.Vote2.VoteId = Guid.NewGuid();
                        questionnaire.Vote1.QuestionnaireId = questionnaire.QuestionnaireId;
                        questionnaire.Vote2.QuestionnaireId = questionnaire.QuestionnaireId;
                        db.Questionnaire.Add(questionnaire);
                        db.SaveChanges();
                        result = true;
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return result;
        }

        public static List<Questionnaire> GetQuestionnaires()
        {
            List<Questionnaire> result = null;
            try
            {
                using (DCContext db = new DCContext())
                {
                   result = db.Questionnaire.Where(x => x.QuestionnaireId != Guid.Empty).ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return result;
        }

        public static Questionnaire GetQuestionnaireById(string id)
        {
            Questionnaire result = null;
            try
            {
                using (DCContext db = new DCContext())
                {
                    result = db.Questionnaire.Find(id);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return result;
        }
    }
}
