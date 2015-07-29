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

using AboutMe.Domain.Entity.Review;

using AboutMe.Common.Helper;
using AboutMe.Common.Data;

namespace AboutMe.Web.Front.Controllers
{
    [RoutePrefix("MyPage/MyReview")]
    [Route("{action=Ready}")]
    public class MyReviewController : BaseFrontController
    {
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
            ViewBag.img_path_product = _img_path_product; //이미지디렉토리경로
            return View(_ReviewService.GetMyReviewReadyList(_user_profile.M_ID));
        }

        [HttpGet]
        [CustomAuthorize]
        public ActionResult Complete(int page = 1)
        {
            
            MyReviewCompleteViewModel model = new MyReviewCompleteViewModel();
            ViewBag.img_path_review = _img_path_review; //이미지디렉토리경로
            model.PageNo = page;


            model.Reviews = ReviewHelper.GetDataForDisplay(_ReviewService.GetMyReviewCompleteList(_user_profile.M_ID, page));
            model.Total = _ReviewService.GetMyReviewCompleteCnt(_user_profile.M_ID);

            return View(model);
        }

        [HttpGet]
        [CustomAuthorize]
        public ActionResult Write(int? ORDER_DETAIL_IDX, string P_CODE = null)
        {
            MyReviewInsertViewModel model = new MyReviewInsertViewModel();
            model.M_ID = _user_profile.M_ID;
            model.ORDER_DETAIL_IDX = ORDER_DETAIL_IDX;
            model.P_CODE = P_CODE;

            

            //상품정보
            if (P_CODE != null)
            {
                model.ProductInfo = _ReviewService.GetProductInfo(P_CODE);
            }
           // Debug.WriteLine("P_CODE: " + P_CODE);
            //Debug.WriteLine("model.ProductInfo: " + model.ProductInfo);

            return View(model);
        }

        [HttpPost]
        [CustomAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult Write(MyReviewInsertViewModel model)
        {

           // Debug.WriteLine("ModelState.IsValid - " + ModelState.IsValid);
            //Debug.WriteLine("OrderDetailIdx - " + model.ORDER_DETAIL_IDX);
            //Debug.WriteLine("Pcode - " + model.P_CODE);
            //Debug.WriteLine("Mid - " + model.M_ID);

            /**
             * 상품정보 
             */
            if (model.P_CODE != null)
            {
                model.ProductInfo = _ReviewService.GetProductInfo(model.P_CODE);
            }

            /**
             * 뷰티일때만 피부타입 유효성 체크
             */
            if (!ReviewHelper.IsBeauty(model.ProductInfo.C_CATE_CODE))
            {
                var valueToClean = ModelState["SKIN_TYPE"];
                valueToClean.Errors.Clear(); 
            }


           if (ModelState.IsValid){

               ImageUpload imageUpload = new ImageUpload { Width = 600, UploadPath = _img_path_review, addMobileImage = true };

               // rename, resize, and upload
               //return object that contains {bool Success,string ErrorMessage,string ImageName}
               Debug.WriteLine("model.UploadImage - " + model.UploadImage );
              
               if (model.UploadImage != null)
               {
                   ImageResult imageResult = imageUpload.RenameUploadFile( model.UploadImage );
                  // Debug.WriteLine("imageResult.Success - " + imageResult.Success);
                   //Debug.WriteLine("imageResult.ErrorMessage - " + imageResult.ErrorMessage);
                   if (imageResult.Success)
                   {
                       //Debug.WriteLine(" imageResult.ImageName - " + imageResult.ImageName);
                       model.ADD_IMAGE = imageResult.ImageName;
                       
                   }
               }

               Tuple<string, string> ret = _ReviewService.InsertMyReview(model.M_ID, model.ORDER_DETAIL_IDX, model.P_CODE, model.SKIN_TYPE, model.COMMENT, model.ADD_IMAGE);
               model.ResultMessage = ret.Item2;
               model.ResultNum = ret.Item1;

               return View(model);
           }

            ModelState.AddModelError("", "필수항목(*)들을 입력해주세요");

            if (model.ORDER_DETAIL_IDX == null )
            {
                 ModelState.AddModelError("", "*주문상세일련번호가 필요합니다.");
            }

            if (model.P_CODE == null && model.P_CODE == "")
            {
                ModelState.AddModelError("", "*상품코드가 필요합니다.");
            }

            if (model.M_ID == null && model.M_ID == "")
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