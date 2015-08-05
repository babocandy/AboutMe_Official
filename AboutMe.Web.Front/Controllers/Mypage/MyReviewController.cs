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
           // ViewBag.img_path_review = _img_path_review; //이미지디렉토리경로
            model.PageNo = page;


            model.Reviews = ReviewHelper.GetDataForUser(_ReviewService.GetMyReviewCompleteList(_user_profile.M_ID, page));
            model.Total = _ReviewService.GetMyReviewCompleteCnt(_user_profile.M_ID);

            return View(model);
        }

        [HttpGet]
        [CustomAuthorize]
        //public ActionResult Write(int? ORDER_DETAIL_IDX, string P_CODE = null)
        public ActionResult Write(MyReviewPdtInputParam p)
        {
            MyReviewInsertViewModel model = new MyReviewInsertViewModel();
            model.M_ID = _user_profile.M_ID;
            model.ORDER_DETAIL_IDX = p.ORDER_DETAIL_IDX;
            model.P_CODE = p.P_CODE;

            //상품정보
            if (p.P_CODE != null)
            {
                model.ProductInfo = _ReviewService.GetProductInfo(p.P_CODE);
            }

            return View(model);
        }

        [HttpPost]
        [CustomAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult Write(MyReviewInsertViewModel model)
        {
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
            if (!ReviewHelper.CheckBeauty(model.ProductInfo.C_CATE_CODE))
            {
                var valueToClean = ModelState["SKIN_TYPE"];
                valueToClean.Errors.Clear(); 
            }


           if (ModelState.IsValid){

               ImageUpload imageUpload = new ImageUpload { Width = 600, UploadPath = _img_path_review, addMobileImage = true };
               Debug.WriteLine("model.UploadImage - " + model.UploadImage );
              
               if (model.UploadImage != null)
               {
                   ImageResult imageResult = imageUpload.RenameUploadFile( model.UploadImage );
                   if (imageResult.Success)
                   {
                       //Debug.WriteLine(" imageResult.ImageName - " + imageResult.ImageName);
                       model.ADD_IMAGE = imageResult.ImageName;
                       
                   }
               }

               MyReviewPdtParamOnSaveToDb p = new MyReviewPdtParamOnSaveToDb();
               p.M_ID = model.M_ID;
               p.ORDER_DETAIL_IDX =  model.ORDER_DETAIL_IDX;
               p.P_CODE = model.P_CODE;
               p.SKIN_TYPE = model.SKIN_TYPE;
               p.COMMENT = model.COMMENT;
               p.ADD_IMAGE =  model.ADD_IMAGE;

               Tuple<string, string> ret = _ReviewService.InsertMyReview(p);

               TempData["ResultNum"] = ret.Item1;
               TempData["ResultMessage"] = ret.Item2;

               return View(model);
           }

            ModelState.AddModelError("", "필수항목(*)들을 입력해주세요");
            /*
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
            }*/

            return View(model);
        }

        [HttpGet]
        [CustomAuthorize]
        [Route("Update/{id:int}")]
        public ActionResult Update(int? id)
        {
            MyReviewUpdateViewModel model = new MyReviewUpdateViewModel();

            model.ReviewPdtInfo = ReviewHelper.GetDataForUserOnUpdate( _ReviewService.ReviewProductInfo(id) );
            TempData["MyReveiwUpdateData"] = model.ReviewPdtInfo;
            return View(model);
        }

        [HttpPost]
        [CustomAuthorize]
        [ValidateAntiForgeryToken]
        [Route("Update/{id:int}")]
        public ActionResult Update(MyReviewUpdateViewModel model)
        {
            Debug.WriteLine("Update");

            model.ReviewPdtInfo = TempData["MyReveiwUpdateData"] as ReviewProductInfo;
            TempData["MyReveiwUpdateData"] = model.ReviewPdtInfo;

            if (ModelState.IsValid)
            {
                MyReviewPdtParamOnSaveToDb p = new MyReviewPdtParamOnSaveToDb();
                p.IDX = model.ReviewPdtInfo.IDX;
                p.COMMENT = model.COMMENT;
               

                Tuple<string, string> ret = _ReviewService.UpdateMyReview(p);

                TempData["ResultNum"] = ret.Item1;
                TempData["ResultMessage"] = ret.Item2;

                return View(model);
            }

            ModelState.AddModelError("", "필수항목(*)들을 입력해주세요");

            return View(model);
        }
    }
}