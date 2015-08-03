using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Domain.Entity.AdminReview;

namespace AboutMe.Web.Admin.Models
{
    public class AdminReviewListViewModel
    {

        public AdminReviewRouteParam RouteParam { get; set; }
        public int? Total { get; set; }
        public IList<AdminReviewUserModel> List { get; set; }
    }

    public class AdminReviewDetailViewModel
    {

        public int? IDX { get; set; }
        public string COMMENT { get; set; }
        public string IS_DISPLAY{get;set;}
        public string IS_BEST{get;set;}

        public AdminReviewUserModel Review { get; set; }
    }
}