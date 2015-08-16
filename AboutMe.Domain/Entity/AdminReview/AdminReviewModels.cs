using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AboutMe.Domain.Entity.AdminReview
{
    /**
     * 상품리뷰 목록 조회
     */
    public class AdminReviewListViewModel
    {
        public AdminReviewRouteParam RouteParam { get; set; }
        public int? Total { get; set; }
        //public IList<AdminReviewUserModel> List { get; set; }
        public IList<SP_ADMIN_REVIEW_PRODUCT_SEL_Result> List { get; set; }
    }

    /**
     * 상품리뷰 상품찾기 
     */
    public class AdminReviewExpFindProductViewModel
    {
        public List<SP_ADMIN_REVIEW_EXP_FIND_PRODUCT_SEL_Result> List { get; set; }
        public int? Total { get; set; }
        public AdminReviewExpFindProductRouteParam RouteParam { get; set; }
    }


    /**
     * 상품리뷰 라운트 파람
       */
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

    /**
     * 상품리뷰 수정
     */
    public class AdminReviewInputViewModel 
    {
        public int IDX { get; set; }
        public string P_CODE { get; set; }
        public string P_NAME { get; set; }
        public string C_CATE_CODE { get; set; }
        public string IS_BEST { get; set; }
        public string CATE_GBN { get; set; }
        public string COMMENT { get; set; }
        public string IS_PHOTO { get; set; }
        public int VIEW_CNT { get; set; }
        public string M_GRADE { get; set; }
        public string MEDIA_GBN { get; set; }
        public string ADD_IMAGE { get; set; }
        public System.DateTime INS_DATE { get; set; }
        public string M_ID { get; set; }
        public string M_NAME { get; set; }
        public string P_MAIN_IMG { get; set; }
        public string IS_DISPLAY { get; set; }
        public string SKIN_TYPE { get; set; }
        public string MEDIA_GBN_LBL { get; set; }
        public string SKIN_TYPE_LBL { get; set; }
    }

    /**
     * 테마 카테고리
     */
    public class AdminReviewThemaInputViewModel
    {
        public List<SP_ADMIN_REVIEW_CATE_THEMA_SEL_Result> Thema { get; set; }
    }



    /**
     * 체험단리뷰 마스터 조회
     */
    public class AdminReviewExpMasterListViewModel
    {
        public AdminReviewExpMasterListRouteParam RouteParam { get; set; }
        public int? Total { get; set; }
        public IList<SP_ADMIN_REVIEW_EXP_MASTER_SEL_Result> List { get; set; }
    }



    /**
     * 체험단리뷰 마스터 수정,저장시
     */

    public class AdminReviewExpMasterInputViewModel
    {
        public AdminReviewExpMasterListRouteParam RouteParam { get; set; }

        //
        [Required(ErrorMessage = "*")]
        public string P_CODE { get; set; }

        [Required(ErrorMessage = "*")]
        public string FROM_DATE { get; set; }

        [Required(ErrorMessage = "*")]
        public string TO_DATE { get; set; }

        [Required(ErrorMessage = "*")]
        public string IS_AUTH { get; set; }

        [Required(ErrorMessage = "*")]
        public HttpPostedFileBase EXCEL_FILE { get; set; }
    }

    /**
     * 체험단 상세보기
     */
    public class AdminReviewExpMasterDetailViewModel
    {
        public AdminReviewExpMasterListRouteParam RouteParam { get; set; }

        public int? IDX { get; set; }
        //
        public SP_ADMIN_REVIEW_EXP_MASTER_DETAIL_Result Detail { get; set; }
        public IList<SP_ADMIN_REVIEW_EXP_MEMBER_SEL_Result> Members { get; set; }
    }


    /**
     *  회원입력
     */
    public class AdminReviewExpMemberParamToInputDB
    {
        public int? MASTER_IDX { get; set; }
        public string M_ID { get; set; }
        public string M_NAME { get; set; }
    }

    /**
     * 체험단 파람
     */
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
    
    /***
     * 체험단 상품 찾기
     */
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

    /**
     * 체험단 관련 리뷰 글들 조회
     */
    public class AdminReviewExpArticleListViewModel
    {
        public AdminReviewExpArticleListViewModel()
        {
            Total = 0;
        }

        public int? EXP_MASTER_IDX { get; set; }

        public List<SP_ADMIN_REVIEW_EXP_ARTICLE_SEL_Result> List { get; set; }
        public int? Total { get; set; }
        public SP_ADMIN_REVIEW_EXP_MASTER_DETAIL_Result MasterDetail { get; set; }

        public AdminReviewExpArticleRouteParam RouteParam { get; set; }
    }
    //AdminReviewExpArticleInputViewModel
    public partial class  SP_ADMIN_REVIEW_EXP_ARTICLE_DETAIL_Result
    {
        public AdminReviewExpArticleRouteParam RouteParam { get; set; }


        [AllowHtml]
        public string COMMENT_HTML { get; set; }
    }

    /**
     * 체험단 리뷰 글 라운트 파람
       */
    public class AdminReviewExpArticleRouteParam
    {
        public AdminReviewExpArticleRouteParam()
        {
            PAGE = 1;
            PAGE_SIZE = 10;
        }


        public int? EXP_MASTER_IDX { get; set; }

        public int? PAGE { get; set; }
        public int PAGE_SIZE { get; set; }

        public string SEARCH_KEY { get; set; }
        public string SEARCH_VALUE { get; set; }

    }


}
