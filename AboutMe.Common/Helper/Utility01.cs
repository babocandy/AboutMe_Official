using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Configuration;

using System.Web.Mvc;

using System.Data.Entity;
using System.Data.Entity.Core.Objects;




namespace AboutMe.Common.Helper
{
    public class Utility01
    {

        public static List<SelectListItem> GetDropDownList<T>(string text, string value, string selected, List<T> result) where T : class
        {
            

                List<SelectListItem> list = new List<SelectListItem>();
                list.Add(new SelectListItem { Text = "-Please select-", Value = string.Empty });
                //IQueryable<T> result = Db.Repository<T>();

                //IQueryable<T> result = DataValues;
               // var lisData = (from items in result
                var lisData = (from items in result.AsQueryable()  // AsQueryable 추가 ssw
                               select items).AsEnumerable().Select(m => new SelectListItem
                               {
                                   Text = (string)m.GetType().GetProperty(text).GetValue(m),
                                   Value = (string)m.GetType().GetProperty(value).GetValue(m),
                                   Selected = (selected != "") ? ((string)
                                     m.GetType().GetProperty(value).GetValue(m) == 
                                     selected ? true : false) : false,
                               }).ToList();
                list.AddRange(lisData);
                return list;
          }

     


    }
}
