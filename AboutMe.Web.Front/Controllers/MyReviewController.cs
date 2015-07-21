using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Domain.Service.Review;

using System.Diagnostics;
using AboutMe.Web.Front.Models;
using AboutMe.Web.Front.Common.Filters;
using AboutMe.Web.Front.Common;


namespace AboutMe.Web.Front.Controllers
{
    [RoutePrefix("MyPage/MyReview")]
    [Route("{action=Ready}")]
    public class MyReviewController : BaseFrontController
    {
        // GET: AdminProduct
        private IReviewService _ReviewService;

        public MyReviewController(IReviewService _reviewService)
        {
            this._ReviewService = _reviewService;
        }

        // GET: MyReview
        /*
        public ActionResult Index()
        {
            return View();
        }*/

        /**
         * 작성가능한 리뷰 목록 조회
         */
        [CustomAuthorize]
        public ActionResult Ready()
        {
            ViewBag.PRODUCT_PATH = AboutMe.Common.Helper.Config.GetConfigValue("ProductPhotoPath"); //이미지디렉토리경로
            return View(_ReviewService.GetReadyList(_user_profile.M_ID));
        }

        public ActionResult Complete()
        {
            return View();
        }

        public ActionResult Write()
        {
            return View();
        }

        public ActionResult Update()
        {
            return View();
        }
    }
}