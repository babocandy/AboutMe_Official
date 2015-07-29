using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using AboutMe.Domain.Entity.Review;
using AboutMe.Domain.Entity.Product;
using AboutMe.Common.Helper;
using System.Diagnostics;
using System;
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

        public List<ReviewProductDisplay> Reviews{ get; set; }

        public int PageNo { get; set; }
        public int PageSize { get { return 10; } }
        public int Total { get; set; }
        public SP_REVIEW_GET_PRODUCT_INFO_Result ProductInfo { get; set; }
        public string Pcode { get; set; }
    }



    public class ReviewProductListViewModel : MyReviewCompleteViewModel
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

    }


}