using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboutMe.Domain.Entity.Review
{

    public class ReviewProductInfo
    {
        public int? IDX { get; set; }
        public string M_ID { get; set; }
        public string M_NAME { get; set; }
        public string M_SEX { get; set; }
        public string M_BIRTHDAY { get; set; }

        public int? ORDER_DETAIL_IDX { get; set; }

        public string P_CODE { get; set; }
        public string P_NAME { get; set; }
        public string P_SUB_TITLE { get; set; }
        public string P_MAIN_IMG { get; set; }
        public string C_CATE_CODE { get; set; }

        public string SKIN_TYPE { get; set; }
        public string COMMENT { get; set; }
        public string ADD_IMAGE { get; set; }
       
        public string INS_DATE { get; set; }

        public bool IS_PHOTO { get; set; }
        public bool IS_BEAUTY { get; set; }
        public bool IS_BEST { get; set; }
    }

    // public class ReviewProductListViewModel
    public class ReviewProductListParam
    {
        public int? TAIL_IDX { get; set; }
        public string CATEGORY_CODE { get; set; }
        public string SORT { get; set; }
    }

    public class ReviewProductListParamInShopping
    {
        public string P_CODE { get; set; }
        public int? PAGE_NO { get; set; }
    }

    /**
     * 리뷰 작성시 사용하는 파람객체
     */
    public class MyReviewPdtInputParam
    {
        //public int? IDX { get; set; }
        public int? ORDER_DETAIL_IDX { get; set; }
        public string P_CODE { get; set; }
    }

    /**
     * 저장, 수정시 db service으로 제공되는 파람
     */
    public class MyReviewPdtParamOnSaveToDb
    {
        public int? IDX { get; set; }
        public string M_ID { get; set; }
        public int? ORDER_DETAIL_IDX { get; set; }
        public string P_CODE { get; set; }
        public string COMMENT { get; set; }
        public string ADD_IMAGE { get; set; }
        public string SKIN_TYPE { get; set; }
    }
}
