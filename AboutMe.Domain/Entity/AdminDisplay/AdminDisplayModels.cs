using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboutMe.Domain.Entity.AdminDisplay
{
    public class WebMainBannerParam
    {
        public int? IDX { get; set; }
        public string URL { get; set; }
        public HttpPostedFileBase IMG { get; set; }
    }
}
