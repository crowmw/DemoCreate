using Repository.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reository.Models.DAL
{
    public class UserDAL
    {
        public static User GetUserByID(string id)
        {
            User result = null;
            try
            {
                if(id != null)
                {
                    using (DCContext db = new DCContext())
                    {
                        result = db.User.Where(x => x.Id == id)
                                        .FirstOrDefault();
                    }
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
