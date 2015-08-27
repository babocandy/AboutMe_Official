using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using System.ComponentModel.DataAnnotations;

using AboutMe.Domain.Entity.Review;
using AboutMe.Domain.Entity.Product;


using System.Text.RegularExpressions;

namespace AboutMe.Domain.Entity.Review
{
    /*
     * 공통
     */
    public class ReviewListJsonParam
    {
        public int? TAIL_IDX { get; set; }
        public string CATEGORY_CODE { get; set; }
        public string SORT { get; set; }
    }


    public class ReviewListJsonParamInShopping
    {
        public ReviewListJsonParamInShopping()
        {
            PAGE_NO = 1;
            PAGE_SIZE = 10;
        }
        public string P_CODE { get; set; }
        public int? PAGE_NO { get; set; }
        public int? PAGE_SIZE { get; set; }
    }

    /**
     * 상품 리뷰 작성시 사용하는 파람객체
     */
    public class MyReviewPdtInputParam
    {
        //public int? IDX { get; set; }
        public int? ORDER_DETAIL_IDX { get; set; }
        public string P_CODE { get; set; }
    }


    /**
     * 작성완료 상품리뷰 목록 뷰모델
     */
    public class MyReviewCompleteViewModel
    {
        public MyReviewCompleteViewModel(){
            PageSize = 10;
        }

        public List<SP_REVIEW_PRODUCT_COMPLETE_SEL_Result> Reviews { get; set; }

        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public SP_REVIEW_GET_PRODUCT_INFO_Result ProductInfo { get; set; }
        public string Pcode { get; set; }

        //모바일용
        public int Pages{ get; set; }
        public int PrevPage { get; set; }
        public int NextPage { get; set; }
    }

    //상품리뷰 목록
    public partial class ReviewProductListViewModel
    {
        public static string SORT_PHOTO = "0";
        public static string SORT_LASTEST = "1";


        public List<SP_REVIEW_PRODUCT_SEL_Result> Reviews { get; set; }

        public int PageNo { get; set; }
        public int PageSize { get { return 10; } }
        public int Total { get; set; }
        public SP_REVIEW_GET_PRODUCT_INFO_Result ProductInfo { get; set; }
        public string Pcode { get; set; }


        public List<SP_CATEGORY_DEPTH_SEL_Result> CategoryBeauty { get; set; }
        public string CategoryCodeHealth { get; set; }
        public List<SP_CATEGORY_DEPTH_SEL_Result> CategorySelShop { get; set; }

        public List<SP_REVIEW_CATE_THEMA_SEL_Result> CategoryThema { get; set; }

        //모바일용
        public List<SP_REVIEW_PRODUCT_MOBILE_SEL_Result> ReviewsMobile { get; set; }
        public int? Pages { get; set; }
        public ReviewListMobileUrlParam MobileParam { get; set; }
        public int? PrevPage { get; set; }
        public int? NextPage { get; set; }
    }

    /**
     * 나의 상품리뷰 등록,수정
     */
    public class MyReviewProductInputViewModel
    {
        public int? IDX { get; set; }
        public string M_ID { get; set; }

        public int? ORDER_DETAIL_IDX { get; set; }

        public string P_CODE { get; set; }

        [Required(ErrorMessage = "*")]
        public string SKIN_TYPE { get; set; }

        [Required(ErrorMessage = "*")]
        public string COMMENT { get; set; }

        public string ADD_IMAGE { get; set; }


        [Display(Name = "Upload File")]
        public HttpPostedFileBase UploadImage { get; set; }

        public SP_REVIEW_GET_PRODUCT_INFO_Result ProductInfo { get; set; }
        
        //public SP_REVIEW_PRODUCT_INFO_Result ReviewPdtInfo { get; set; }

        public string P_MAIN_IMG{ get; set; }
        public string P_NAME{ get; set; }
        public string P_SUB_TITLE{ get; set; }
        public string SKIN_TYPE_LBL{ get; set; }
        public string C_CATE_CODE { get; set; }

        public string MEDIA_GBN { get; set; }
    }

    public class ReviewInProductDetailViewModel
    {
        public ReviewInProductDetailViewModel()
        {
            PageSize = 10;
        }

        public List<SP_REVIEW_PRODUCT_IN_SHOPPING_DETAIL_Result> Reviews { get; set; }

        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public SP_REVIEW_GET_PRODUCT_INFO_Result ProductInfo { get; set; }
        public string Pcode { get; set; }
    }

    /**
     * 체험단 리뷰 조회
     */
    public class ReviewExpListViewModel 
    {

        public static string SORT_LASTEST = "0";
        public static string SORT_LIKE = "1";

        //pc용
        public List<SP_REVIEW_EXP_SEL_Result> Reviews { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get { return 10; } }
        public int Total { get; set; }
        public SP_REVIEW_GET_PRODUCT_INFO_Result ProductInfo { get; set; }
        public string Pcode { get; set; }


        public List<SP_CATEGORY_DEPTH_SEL_Result> CategoryBeauty { get; set; }
        public string CategoryCodeHealth { get; set; }
        public List<SP_CATEGORY_DEPTH_SEL_Result> CategorySelShop { get; set; }


        //모바일용
        public List<SP_REVIEW_EXP_MOBILE_SEL_Result> ReviewsMobile { get; set; }
        public int? Pages { get; set; }
        public ReviewListMobileUrlParam MobileParam { get; set; }
        public int? PrevPage { get; set; }
        public int? NextPage { get; set; }

    }

    public partial  class SP_REVIEW_EXP_SEL_Result
    {

        public string COMMENT_TEXT { 
            get {

                string _commentText = COMMENT;
                

                string textOnly = string.Empty;
                Regex tagRemove = new Regex(@"<[^>]*(>|$)");
                Regex compressSpaces = new Regex(@"[\s\r\n]+");
                Regex etc = new Regex(@"&(nbsp|amp|quot|lt|gt);");
                textOnly = tagRemove.Replace(_commentText, string.Empty);
                textOnly = compressSpaces.Replace(textOnly, " ");
                textOnly = etc.Replace(textOnly, " ");

                return textOnly;
            }
        }
    }

    public partial class SP_REVIEW_EXP_IN_SHOPPING_DETAIL_Result 
    {
        public string COMMENT_TEXT
        {
            get
            {

                string _commentText = COMMENT;


                string textOnly = string.Empty;
                Regex tagRemove = new Regex(@"<[^>]*(>|$)");
                Regex compressSpaces = new Regex(@"[\s\r\n]+");
                Regex etc = new Regex(@"&(nbsp|amp|quot|lt|gt);");
                textOnly = tagRemove.Replace(_commentText, string.Empty);
                textOnly = compressSpaces.Replace(textOnly, " ");
                textOnly = etc.Replace(textOnly, " ");

                return textOnly;
            }
        }
    }

    public partial class SP_REVIEW_EXP_MOBILE_SEL_Result
    {
        public string COMMENT_TEXT
        {
            get
            {

                string _commentText = COMMENT;


                string textOnly = string.Empty;
                Regex tagRemove = new Regex(@"<[^>]*(>|$)");
                Regex compressSpaces = new Regex(@"[\s\r\n]+");
                Regex etc = new Regex(@"&(nbsp|amp|quot|lt|gt);");
                textOnly = tagRemove.Replace(_commentText, string.Empty);
                textOnly = compressSpaces.Replace(textOnly, " ");
                textOnly = etc.Replace(textOnly, " ");

                return textOnly;
            }
        }
    }

    /**
     *  체험단 나의리뷰 목록
     */
    public class MyReviewExpListViewModel
    {
        public List<Tuple<SP_REVIEW_MY_EXP_MASTER_SEL_Result, SP_PRODUCT_DETAIL_VIEW_Result>> List { get; set; }
    }

    /**
     *  체험단 나의리뷰 저장,수정
     */
    public class MyReviewExpInputViewModel
    {
        public string M_ID { get; set; }

        public int? ARTICLE_IDX { get; set; }
        public int? EXP_MASTER_IDX { get; set; }
        public string P_CODE { get; set; }

        [Required(ErrorMessage = "*")]
        public string TITLE { get; set; }

        [Required(ErrorMessage = "*")]
        public string TAG { get; set; }

        public string SUB_IMG_1 { get; set; }
        public string SUB_IMG_2 { get; set; }
        public string SUB_IMG_3 { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "*")]
        public string COMMENT { get; set; }

        [Required(ErrorMessage = "*")]
        public string SKIN_TYPE { get; set; }

        public SP_REVIEW_GET_PRODUCT_INFO_Result ProductInfo { get; set; }
    }

    public class ReviewExpDetailParam
    {
        public int? ARTICLE_IDX{ get; set; }
        public string P_CODE { get; set; }
    }

    public partial class SP_REVIEW_EXP_DETAIL_Result
    {
        public string INS_DATE_LBL
        {
            get
            {
                return INS_DATE.Value.ToString("yyyy.MM.dd");
            }
        }
    }




/**
 * 
 * 모바일 관련
 */

    public class ReviewListMobileUrlParam
    {
        public ReviewListMobileUrlParam()
        {
            PAGE = 1;
            PAGE_SIZE = 10;

        }
        public string CATE{ get; set; }
        public string CATE_CODE { get; set; }

        public int? PAGE { get; set; }
        public int? PAGE_SIZE { get; set; }
        public string SORT { get; set; }
    }

    //상품리뷰 목록


}
