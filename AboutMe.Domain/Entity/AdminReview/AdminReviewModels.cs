using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace AboutMe.Domain.Entity.AdminReview
{
    [Serializable] 
    public class AdminReviewRouteParam
    {
        public AdminReviewRouteParam()
        {
            PAGE = 1;
        }


        public int? PAGE { get; set; }

        public string SEARCH_KEY { get; set; }
        public string SEARCH_VALUE { get; set; }
        
        public string MEDIA_GBN { get;set; }
        public string MEDIA_GBN_W { get; set; }
        public string MEDIA_GBN_M { get; set; }

        public string SEL_DATE_FROM { get;set; }
        public string SEL_DATE_TO { get;set; }

        public string IS_PHOTO { get;set; }
        public string IS_PHOTO_Y { get; set; }
        public string IS_PHOTO_N { get; set; }

        public string IS_DISPLAY { get; set; }
        public string IS_DISPLAY_Y { get; set; }
        public string IS_DISPLAY_N { get; set; }
    }

    public class AdminReviewUpdateViewModel : SP_ADMIN_REVIEW_PRODUCT_INFO_Result
    {
        /*
        [Required(ErrorMessage = "*")]
        public override string COMMENT { get; set; }
        
        public int? IDX { get; set; }
        public string COMMENT { get; set; }
        public string IS_DISPLAY{get;set;}
        public string IS_BEST{get;set;}

        public SP_ADMIN_REVIEW_PRODUCT_INFO_Result Review { get; set; }
         * */
    }
    /*
    public class AdminReviewSaveParam{
        public int? IDX { get; set; }
        //public string COMMENT { get; set; }
        public string IS_DISPLAY{get;set;}
        public string IS_BEST{get;set;}
    }*/
    /*
    public class AdminReviewThemaParamToInputDB
    {
        public int? IDX { get; set; }
        public string TITLE { get; set; }
        public string IS_DISPLAY { get; set; }
        public string TAG { get; set; }
    }
    */

    /**
     * 체험단리뷰 마스터 목록,수정,저장시
     */
   
    public class AdminReviewExpMasterParamToInputDB
    {
        public string P_CODE { get; set; }
        public string FROM_DATE { get; set; }
        public string TO_DATE { get; set; }
        public string IS_AUTH { get; set; }
    }

    public class AdminReviewExpMemberParamToInputDB
    {
        public int? MASTER_IDX { get; set; }
        public string M_ID { get; set; }
        public string M_NAME { get; set; }
    }

    public class AdminReviewExpMasterListRouteParam
    {
        public AdminReviewExpMasterListRouteParam()
        {
            PAGE = 1;
        }
     
        public int? PAGE { get; set; }
        public string SEARCH_KEY { get; set; }
        public string SEARCH_VALUE { get; set; }

        public string FROM_DATE { get; set; }
        public string TO_DATE { get; set; }

        public string IS_STATE_10 { get; set; }
        public string IS_STATE_20 { get; set; }
        public string IS_STATE_30 { get; set; }

        public string IS_AUTH_Y { get; set; }
        public string IS_AUTH_N { get; set; }
    }
    
    public class AdminReviewExpFindProductRouteParam
    {
        public AdminReviewExpFindProductRouteParam()
        {
            PAGE = 1;
            PAGE_SIZE = 10;
        }

        public int PAGE { get; set; }
        public int PAGE_SIZE { get; set; }

        public string SEARCH_KEY { get; set; }
        public string SEARCH_VALUE { get; set; }
    }
}
