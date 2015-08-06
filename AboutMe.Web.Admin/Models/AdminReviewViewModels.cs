using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

using AboutMe.Domain.Entity.AdminReview;
using AboutMe.Domain.Entity.AdminProduct;

namespace AboutMe.Web.Admin.Models
{
    public class AdminReviewListViewModel
    {

        public AdminReviewRouteParam RouteParam { get; set; }
        public int? Total { get; set; }
        //public IList<AdminReviewUserModel> List { get; set; }
        public IList<SP_ADMIN_REVIEW_PRODUCT_SEL_Result> List { get; set; }
    }

    public class AdminReviewUpdateViewModel
    {

        public int? IDX { get; set; }
        public string COMMENT { get; set; }
        public string IS_DISPLAY{get;set;}
        public string IS_BEST{get;set;}

        public SP_ADMIN_REVIEW_PRODUCT_INFO_Result Review { get; set; }
    }

    public class AdminReviewThemaInputViewModel
    {
        public List<SP_ADMIN_REVIEW_CATE_THEMA_SEL_Result> Thema { get; set; }
    }

    public class AdminReviewExpMasterListViewModel
    {
        public AdminReviewExpMasterListRouteParam RouteParam { get; set; }
        public int? Total { get; set; }
        public IList<SP_ADMIN_REVIEW_EXP_MASTER_SEL_Result> List { get; set; }
    }

    public class AdminReviewExpCreateViewModel
    {
       //
        [Required(ErrorMessage = "*")]
        public string P_CODE { get; set; }

        [Required(ErrorMessage = "*")]
        public string FROM_DATE { get; set; }

        [Required(ErrorMessage = "*")]
        public string TO_DATE { get; set; }

        [Required(ErrorMessage = "*")]
        public string IS_AUTH { get; set; }
    }

    public class AdminReviewExpFindProductViewModel
    {
        public List<SP_ADMIN_PRODUCT_LIST_Result> List { get; set; }
        public int? Total { get; set; }
    }
}