using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboutMe.Domain.Entity.AdminDisplay
{
    public class DisplayerParam
    {
        public int? IDX { get; set; }
        public string URL { get; set; }
        public string KIND { get; set; }
        public string SUB_KIND { get; set; }
        public int? SEQ { get; set; }
        public string P_CODE { get; set; }
        public string TITLE1 { get; set; }
        public string TITLE2 { get; set; }
        public string IMG_NAME { get; set; }
        public HttpPostedFileBase IMG_FILE { get; set; }
    }
}
