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

    public class ReviewListJsonParam
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

    public class MyReviewExpParamOnSaveToDb
    {
        public int? ARTICLE_IDX { get; set; }
        public string M_ID { get; set; }
        public int? EXP_MASTER_IDX { get; set; }
        public string P_CODE { get; set; }
        public string TITLE { get; set; }
        public string SKIN_TYPE { get; set; }
        public string COMMENT { get; set; }
        public string TAG { get; set; }
        public string SUB_IMG_1 { get; set; }
        public string SUB_IMG_2 { get; set; }
        public string SUB_IMG_3 { get; set; }
        
    }


    /**
     * 체험단 리뷰 조회
     */
    public class ReviewExpListViewModel
    {

        public static string SORT_LASTEST = "0";
        public static string SORT_LIKE = "1";


        public List<SP_REVIEW_EXP_SEL_Result> Reviews { get; set; }

        public int PageNo { get; set; }
        public int PageSize { get { return 10; } }
        public int Total { get; set; }
        public SP_REVIEW_GET_PRODUCT_INFO_Result ProductInfo { get; set; }
        public string Pcode { get; set; }


        public List<SP_CATEGORY_DEPTH_SEL_Result> CategoryBeauty { get; set; }
        public string CategoryCodeHealth { get; set; }
        public List<SP_CATEGORY_DEPTH_SEL_Result> CategorySelShop { get; set; }


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
}
