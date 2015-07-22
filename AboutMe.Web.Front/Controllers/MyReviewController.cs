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
using AboutMe.Web.Front.Models;
using AboutMe.Domain.Entity.Review;


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
            return View(_ReviewService.GetMyReviewReadyList(_user_profile.M_ID));
        }

        [HttpGet]
        [CustomAuthorize]
        public ActionResult Complete(int page = 1)
        {
            MyReviewCompleteViewModel model = new MyReviewCompleteViewModel();
            model.PageNo = page;
            model.CompleteList = _ReviewService.GetMyReviewCompleteList(_user_profile.M_ID, page);           
            model.TotalItem = _ReviewService.GetMyReviewCompleteCnt(_user_profile.M_ID);

            return View(model);
        }

        [HttpGet]
        [CustomAuthorize]
        public ActionResult Write(int? OrderDetailIdx, string Pcode = null)
        {
            MyReviewInsertViewModel model = new MyReviewInsertViewModel();
            model.Mid = _user_profile.M_ID;
            model.OrderDetailIdx = OrderDetailIdx;
            model.Pcode = Pcode;

            //상품정보
            if (Pcode != null)
            {
                SP_REVIEW_GET_PRODUCT_INFO_Result productInfo = _ReviewService.GetProductInfo(Pcode);
                ViewBag.Pname = productInfo.P_NAME;
                ViewBag.Pimg = AboutMe.Common.Helper.Config.GetConfigValue("ProductPhotoPath") + productInfo.MAIN_IMG;
                ViewBag.PsubTitle = productInfo.P_SUB_TITLE;
            }

            return View(model);
        }

        [HttpPost]
        [CustomAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult Write(MyReviewInsertViewModel model)
        {

            Debug.WriteLine("ModelState.IsValid - " + ModelState.IsValid);
            Debug.WriteLine("OrderDetailIdx - " + model.OrderDetailIdx);
            Debug.WriteLine("Pcode - " + model.Pcode);
            Debug.WriteLine("Mid - " + model.Mid);

            //상품정보
            if (model.Pcode != null)
            {
                SP_REVIEW_GET_PRODUCT_INFO_Result productInfo = _ReviewService.GetProductInfo(model.Pcode);
                ViewBag.Pname = productInfo.P_NAME;
                ViewBag.Pimg = AboutMe.Common.Helper.Config.GetConfigValue("ProductPhotoPath") + productInfo.MAIN_IMG;
                ViewBag.PsubTitle = productInfo.P_SUB_TITLE;
            }

           if (ModelState.IsValid){

               Tuple<string, string> ret =  _ReviewService.InsertMyReview(model.Mid, model.OrderDetailIdx, model.Pcode, model.SkinType, model.Comment, model.AddImage);
               model.ResultMessage = ret.Item2;
               model.ResultNum = ret.Item1;

               return View(model);
           }

            ModelState.AddModelError("", "필수항목(*)들을 입력해주세요");

            if (model.OrderDetailIdx == null )
            {
                 ModelState.AddModelError("", "*주문상세일련번호가 필요합니다.");
            }

            if (model.Pcode == null && model.Pcode == "")
            {
                ModelState.AddModelError("", "*상품코드가 필요합니다.");
            }

            if (model.Mid == null && model.Mid == "")
            {
                ModelState.AddModelError("", "*회원아이디가 필요합니다.");
            }

            return View(model);
        }


        public ActionResult Update()
        {
            return View();
        }
    }
}