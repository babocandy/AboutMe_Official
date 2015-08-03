using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboutMe.Domain.Entity.AdminReview
{
    public class AdminReviewRouteParam
    {
        private int? _page;

        public int? PAGE
        {
            get
            {
                return _page ?? 1;
            }
            set
            {
                _page = value;
            }
        }

        public string SEARCH_KEY { get; set; }
        public string SEARCH_VALUE { get; set; }
        public string MEDIA_GBN { get;set; }
        public string SEL_DATE_FROM { get;set; }
        public string SEL_DATE_TO { get;set; }
        public string IS_PHOTO { get;set; }
        public string IS_DISPLAY { get; set; }
    }

    public class AdminReviewUserModel
    {
        public int? IDX { get; set; }

        public string M_ID { get; set; }
        public string M_NAME { get; set; }
        public string P_CODE { get; set; }
        public string P_NAME { get; set; }
        public string C_CATE_CODE { get; set; }
        public string CATE_GBN { get; set; }
        public string COMMENT { get; set; }
        public string IS_PHOTO { get; set; }
        public string INS_DATE { get; set; }
        public string IS_BEST { get; set; }
       // public string IS_BEST_2 { get; set; }
        public string P_MAIN_IMG { get; set; }
        public int? VIEW_CNT { get; set; }
        public string M_GRADE { get; set; }
        public string MEDIA_GBN { get; set; }
        public string ADD_IMAGE { get; set; }
        public string IS_DISPLAY { get; set; }
      //  public string IS_DISPLAY_2 { get; set; }
        public string SKIN_TYPE { get; set; }
    }

    public class AdminReviewSaveParam{
        public int? IDX { get; set; }
        public string COMMENT { get; set; }
        public string IS_DISPLAY{get;set;}
        public string IS_BEST{get;set;}
    }
}
