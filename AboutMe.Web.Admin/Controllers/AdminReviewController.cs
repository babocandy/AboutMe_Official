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
            model.List = tp.Item1;
            model.Total = tp.Item2;
            model.RouteParam = p;

            return View(model);
 
        }

        [CustomAuthorize]
        [Route("Update/{id:int}")]
        public ActionResult Update(int? id , AdminReviewRouteParam p)
        {
            
            var tp = _service.ReviewPdtInfo(id);

            AdminReviewInputViewModel model = new AdminReviewInputViewModel();
            model.IDX = tp.Item1.IDX;
            model.COMMENT = tp.Item1.COMMENT;
           // model.COMMENT_HTML  = tp.Item1.COMMENT;
            model.IS_BEST = tp.Item1.IS_BEST;
            model.IS_DISPLAY = tp.Item1.IS_DISPLAY;
            model.P_CODE = tp.Item1.P_CODE;
            model.P_NAME = tp.Item1.P_NAME;
            model.MEDIA_GBN_LBL = tp.Item1.MEDIA_GBN_LBL;
            model.M_ID = tp.Item1.M_ID;
            model.M_NAME = tp.Item1.M_NAME;
            model.ADD_IMAGE = tp.Item1.ADD_IMAGE;
            model.SKIN_TYPE = tp.Item1.SKIN_TYPE;
            model.SKIN_TYPE_LBL = tp.Item1.SKIN_TYPE_LBL;
            model.INS_DATE = tp.Item1.INS_DATE;
            model.IS_MOST_CNT = tp.Item1.IS_MOST_CNT;
            model.IS_PHOTO = tp.Item1.IS_PHOTO;

            Debug.WriteLine(" tp.Item1.COMMENT" + tp.Item1.COMMENT);


            return View(model);
        }


        [HttpPost]
        [CustomAuthorize]
        [ValidateAntiForgeryToken]
        [Route("Update/{id:int}")]
        public ActionResult Update(AdminReviewInputViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tp = _service.ReviewPdtUpdate(model);
               
                TempData["ResultNum"] = tp.Item1;
                TempData["ResultMessage"] = tp.Item2;

                return View(model);
            }

            TempData["ResultNum"] = "1";
            TempData["ResultMessage"] = "수정실패했습니다.";
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
         public ActionResult SaveThema(SP_ADMIN_REVIEW_CATE_THEMA_SEL_Result p)
        {

            _service.ThemaUpdate(p);

            @TempData["ResultNum"] = "0";

            return Redirect( Request.UrlReferrer.ToString() );
        }
    }
}