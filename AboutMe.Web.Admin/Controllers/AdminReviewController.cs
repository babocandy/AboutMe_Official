using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using System.Diagnostics;
using AboutMe.Web.Admin.Models;
using AboutMe.Web.Admin.Common.Filters;
using AboutMe.Web.Admin.Common;
using AboutMe.Common.Helper;
using AboutMe.Common.Data;

using AboutMe.Domain.Service.AdminReview;
using AboutMe.Domain.Entity.AdminReview;

namespace AboutMe.Web.Admin.Controllers
{
    [RoutePrefix("AdminContents/Review")]
    [Route("{action=Index}")]
    public class AdminReviewController : BaseAdminController
    {

        private IAdminReviewService _service;

        public AdminReviewController(IAdminReviewService s)
        {
            this._service = s;
        }

        [CustomAuthorize]
        public ActionResult Index(AdminReviewRouteParam p)
        {
            AdminReviewListViewModel model = new AdminReviewListViewModel();


            var tp = _service.ReviewPdtList(p);
            model.List = tp.Item1;//ReviewHelper.GetDataForAdminUser(tp.Item1);
            model.Total = tp.Item2;
            model.RouteParam = p;

            return View(model);
 
        }

        [CustomAuthorize]
        public ActionResult Update(int? id , AdminReviewRouteParam p)
        {
            
            var tp = _service.ReviewPdtInfo(id);

            AdminReviewUpdateViewModel model = new AdminReviewUpdateViewModel();
            model.Review = tp.Item1;// ReviewHelper.GetDataForAdminUserSave()[0];
            model.IDX = model.Review.IDX;
            model.COMMENT = model.Review.COMMENT;
            model.IS_BEST = model.Review.IS_BEST;
            model.IS_DISPLAY = model.Review.IS_DISPLAY;

            TempData["ReviewData"] = model.Review;

            return View(model);
        }


        [HttpPost]
        [CustomAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult Update(AdminReviewUpdateViewModel model)
        {
            model.Review = TempData["ReviewData"] as SP_ADMIN_REVIEW_PRODUCT_INFO_Result;
           TempData["ReviewData"] = model.Review;

 

            if (ModelState.IsValid)
            {
                AdminReviewSaveParam p = new AdminReviewSaveParam();
                p.IDX = model.IDX;
                //p.COMMENT = model.COMMENT;
                p.IS_DISPLAY = model.IS_DISPLAY;
                p.IS_BEST = model.IS_BEST;

                var tp = _service.ReviewPdtUpdate(p);

                TempData["ResultNum"] = tp.Item1;
                TempData["ResultMessage"] = tp.Item2;

                return View(model);
            }

            return View(model);
        }

        /**
         * 테마
         */
         [CustomAuthorize]
        public ActionResult Thema()
        {
            AdminReviewThemaInputViewModel model = new AdminReviewThemaInputViewModel();
            model.Thema = _service.ThemaList();
            return View(model);
        }

        [HttpPost]
        [CustomAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult SaveThema(AdminReviewThemaParamToInputDB p)
        {

            _service.ThemaUpdate(p);

            @TempData["ResultNum"] = "0";

            return Redirect( Request.UrlReferrer.ToString() );
        }
    }
}