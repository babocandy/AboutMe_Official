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


    public class PopupParam
    {
        public int? IDX { get; set; }
        public string MEDIA_GBN { get; set; }
        public string TITLE { get; set; }

        public string WEB_IMG_NAME { get; set; }

        public int? POS_TOP { get; set; }
        public int? POS_LEFT { get; set; }

        public int? SIZE_WIDTH { get; set; }
        public int? SIZE_HEIGHT { get; set; }

        public string WEB_LINK { get; set; }
        public string WEB_TARGET { get; set; }

        public string MOBILE_IMG_NAME { get; set; }
        public string MOBILE_LINK { get; set; }

        public string IS_DISPLAY { get; set; }
        public string DISPLAY_START { get; set; }
        public string DISPLAY_END { get; set; }
    }

    
    public class PopupSearchParam
    {
        private int? _page;

        //public int? IDX { get; set; }

        public int?  PAGE{ 
            get{
                return _page ?? 1;
            }
            set
            {
                _page = value;
            }
        }

        public string SEARCH_KEY { get; set; }
        public string SEARCH_VALUE { get; set; }

        public string IS_DISPLAY { get; set; }
        public string IS_EXPIRE { get; set; }

    }

    public class PopupInfo
    {

        private string _media;

        public int IDX { get; set; }
        public string MEDIA_GBN { get; set; }
        public string TITLE { get; set; }
        public Boolean IS_DISPLAY { get; set; }
        public string DISPLAY_START { get; set; }
        public string DISPLAY_END { get; set; }
        public string WEB_IMG { get; set; }
        public string WEB_LINK { get; set; }
        public string MOBILE_IMG { get; set; }
        public string MOBILE_LINK { get; set; }
        public string INS_DATE { get; set; }
    }

}
