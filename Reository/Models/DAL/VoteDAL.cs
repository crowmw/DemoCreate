using Repository.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Reository.Models.DAL
{
    public class VoteDAL
    {
        public static bool Add(Vote vote)
        {
            var result = false;
            try
            {
                if(vote != null)
                {
                    using (DCContext db = new DCContext())
                    {
                        vote.VoteId = Guid.NewGuid();
                        db.Vote.Add(vote);
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
    }
}