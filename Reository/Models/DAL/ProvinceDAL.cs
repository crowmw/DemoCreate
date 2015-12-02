using Repository.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Reository.Models.DAL
{
    public class ProvinceDAL
    {
        public static List<Provinces> GetProvinces()
        {
            List<Provinces> result = null;
            try
            {
                using (DCContext db = new DCContext())
                {
                    result = db.Provinces.ToList();
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