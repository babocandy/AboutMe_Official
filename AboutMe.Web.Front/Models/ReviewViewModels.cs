using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using AboutMe.Domain.Entity.Review;
using AboutMe.Domain.Entity.Product;

namespace AboutMe.Web.Front.Models
{
    public class MyReviewInsertViewModel
    {
        [Required]
        public string M_ID { get; set; }

        [Required]
        public int? ORDER_DETAIL_IDX { get; set; }

        [Required]
        public string P_CODE { get; set; }

        [Required(ErrorMessage = "*")]
        public string SKIN_TYPE { get; set; }

        [Required(ErrorMessage = "*")]
        public string COMMENT { get; set; }        
        public string ADD_IMAGE { get; set; }

        [Display(Name = "Upload File")]
        public HttpPostedFileBase UploadImage { get; set; }
        public SP_REVIEW_GET_PRODUCT_INFO_Result ProductInfo { get; set; }

        public string ResultNum { get; set; }
        public string ResultMessage { get; set; }
    }

    public class MyReviewCompleteViewModel
    {
        public IList<SP_REVIEW_PRODUCT_COMPLETE_SEL_Result> CompleteList { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get { return 10; } }
        public int TotalItem { get; set; }
        public SP_REVIEW_GET_PRODUCT_INFO_Result ProductInfo { get; set; }
    }

    public class ReviewProductListViewModel
    {
        public List<SP_CATEGORY_DEPTH_SEL_Result> CategoryBeauty { get; set; }
        public List<SP_CATEGORY_DEPTH_SEL_Result> CategorySelShop{ get; set; }
        public string CategoryCodeHealth { get; set; }
        
        public string DefaultCategoryCode
        {
            get
            {
                return "101100100"; //뷰티전체
            }
        }

        public string DefaultSort
        {
            get
            {
                return "0"; //포토순으로
            }
        }

        public List<SP_REVIEW_PRODUCT_COMPLETE_SEL_Result> Products { get; set; }



    }

    // public class ReviewProductListViewModel
    public class ReviewProductListParam
    {
        public int? TAIL_IDX { get; set; }
        public string CATEGORY_CODE { get; set; }
        public string SORT { get; set; }
    }
}