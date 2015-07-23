﻿using System;
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

using AboutMe.Common.Helper;
using AboutMe.Common.Data;

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
                model.ProductInfo = _ReviewService.GetProductInfo(Pcode);
                /*ViewBag.Pname = productInfo.P_NAME;
                ViewBag.Pimg = _img_path_product + productInfo.MAIN_IMG;
                ViewBag.PsubTitle = productInfo.P_SUB_TITLE;
                ViewBag.PcateCode = productInfo.P_CATE_CODE;*/
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

            /**
             * 상품정보 
             */
            if (model.Pcode != null)
            {
                model.ProductInfo = _ReviewService.GetProductInfo(model.Pcode);
            }

            /**
             * 뷰티일때만 피부타입 유효성 체크
             */
            if (ReviewHelper.IsBeauty(model.ProductInfo.P_CATE_CODE))
            {
                var valueToClean = ModelState["SkinType"];
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
                   Debug.WriteLine("imageResult.Success - " + imageResult.Success);
                   Debug.WriteLine("imageResult.ErrorMessage - " + imageResult.ErrorMessage);
                   if (imageResult.Success)
                   {
                       Debug.WriteLine(" imageResult.ImageName - " + imageResult.ImageName);
                       model.AddImage = imageResult.ImageName;
                       
                   }
               }

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