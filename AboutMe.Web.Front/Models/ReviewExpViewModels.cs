using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using AboutMe.Domain.Entity.Review;
using AboutMe.Domain.Entity.Product;
using AboutMe.Common.Helper;
using System.Diagnostics;
using System;
using System.Web.Mvc;

using AboutMe.Domain.Service.Product;


namespace AboutMe.Web.Front.Models
{

    public class MyReviewExpListViewModel
    {
        //public List<SP_REVIEW_MY_EXP_MASTER_SEL_Result> List { get; set; }

       // public List<SP_PRODUCT_DETAIL_VIEW_Result> List { get; set; }

        public List< Tuple<SP_REVIEW_MY_EXP_MASTER_SEL_Result, SP_PRODUCT_DETAIL_VIEW_Result> > List { get; set; }
    }
    /**
     * 체험단 글쓰기
     */
     public class MyReviewExpWriteViewModel
    {
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

     /**
      * 체험단 글쓰기
      */
     public class MyReviewExpUpdateViewModel : MyReviewExpWriteViewModel
     {
         public int? ARTICLE_IDX { get; set; }
     }
}