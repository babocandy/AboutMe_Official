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
    }

    public class MyReviewUpdateViewModel
    {

        [Required(ErrorMessage = "*")]
        public string COMMENT { get; set; }

        public ReviewProductInfo ReviewPdtInfo { get; set; }
    }

    public class MyReviewCompleteViewModel
    {

        public List<ReviewProductInfo> Reviews { get; set; }

        public int PageNo { get; set; }
        public int PageSize { get { return 10; } }
        public int Total { get; set; }
        public SP_REVIEW_GET_PRODUCT_INFO_Result ProductInfo { get; set; }
        public string Pcode { get; set; }
    }



    public class ReviewProductListViewModel : MyReviewCompleteViewModel
    {
        public static string SORT_PHOTO = "0";
        public static string SORT_LASTEST = "1";

        public List<SP_CATEGORY_DEPTH_SEL_Result> CategoryBeauty { get; set; }
        public List<SP_CATEGORY_DEPTH_SEL_Result> CategorySelShop{ get; set; }
        public string CategoryCodeHealth { get; set; }



    }


}