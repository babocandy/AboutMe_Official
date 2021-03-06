﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Domain.Service.Review;
using System.Diagnostics;

using AboutMe.Web.Mobile.Common.Filters;
using AboutMe.Web.Mobile.Common;
using AboutMe.Domain.Entity.Review;
using AboutMe.Common.Helper;
using AboutMe.Common.Data;

using AboutMe.Domain.Service.Product;
using AboutMe.Domain.Entity.Product;

namespace AboutMe.Web.Mobile.Controllers
{
    [RoutePrefix("MyPage/MyReview")]
    [Route("{action=Ready}")]
    public class MyReviewController : BaseMobileController
    {
        private IReviewService _ReviewService;
        private IProductService _service_pdt;

        public MyReviewController(IReviewService s, IProductService p)
        {
            this._ReviewService = s;
            _service_pdt = p;
        }

        /**
         * 작성가능한 리뷰 목록 조회
         */
        [CustomAuthorize]
        public ActionResult Ready()
        {
            List<Tuple<SP_PRODUCT_DETAIL_VIEW_Result, SP_REVIEW_PRODUCT_READY_SEL_Result>> list = new List<Tuple<SP_PRODUCT_DETAIL_VIEW_Result, SP_REVIEW_PRODUCT_READY_SEL_Result>>();

            var readyList = _ReviewService.GetMyReviewReadyList(_user_profile.M_ID);
            foreach (var item in readyList)
            {                
                var tp = new Tuple<SP_PRODUCT_DETAIL_VIEW_Result, SP_REVIEW_PRODUCT_READY_SEL_Result>( _service_pdt.ViewProduct(item.P_CODE) ,item);
                
                list.Add(tp);
            }

            return View(list);
        }


        /**
         * 
         * 마이리뷰 작성
         *
         */
        [HttpGet]
        [CustomAuthorize]
        public ActionResult Write(MyReviewPdtInputParam p)
        {
            MyReviewProductInputViewModel model = new MyReviewProductInputViewModel();
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
        public ActionResult Write(MyReviewProductInputViewModel model)
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
            if (ReviewHelper.CheckHealth(model.ProductInfo.C_CATE_CODE))
            {
                var valueToClean = ModelState["SKIN_TYPE"];
                valueToClean.Errors.Clear(); 
            }


           if (ModelState.IsValid){

               ImagePlainUpload imageUpload = new ImagePlainUpload { UploadPath = _img_path_review, IsThumbNail = true };
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

               Tuple<string, string> ret = _ReviewService.InsertMyReview(model);

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

        /**
         * 작성완료한 마이리뷰
         */
        [HttpGet]
        [CustomAuthorize]
        public ActionResult Complete(int page = 1)
        {
            MyReviewCompleteViewModel model = new MyReviewCompleteViewModel();

            model.PageNo = page;
            model.Reviews = _ReviewService.GetMyReviewCompleteList(_user_profile.M_ID, page);
            model.Total = _ReviewService.GetMyReviewCompleteCnt(_user_profile.M_ID);

            model.Pages = (int)Math.Ceiling( (float)model.Total / (float)model.PageSize );
            model.PrevPage = page - 1 < 1 ? 1 : page - 1;
            model.NextPage = page + 1 > model.Pages ? model.Pages : page + 1;

            return View(model);
        }

        /**
         * 
         * 나의리뷰 수정
         * 
         */
        [HttpGet]
        [CustomAuthorize]
        [Route("Update/{id:int}")]
        public ActionResult Update(int? id)
        {
            MyReviewProductInputViewModel model = new MyReviewProductInputViewModel();
            
            
            //상품리뷰 상세
            var detail = _ReviewService.GetReviewProductDetail(id);

            model.IDX = detail.IDX;
            model.COMMENT = detail.COMMENT;
            model.P_MAIN_IMG = detail.P_MAIN_IMG;
            model.P_NAME = detail.P_NAME;
            model.P_SUB_TITLE = detail.P_SUB_TITLE;
            model.C_CATE_CODE = detail.C_CATE_CODE;
            model.SKIN_TYPE = detail.SKIN_TYPE;
            model.SKIN_TYPE_LBL = detail.SKIN_TYPE_LBL;
            model.MEDIA_GBN = detail.MEDIA_GBN;
            model.IS_PHOTO = detail.IS_PHOTO;
            model.ADD_IMAGE = detail.ADD_IMAGE;
            model.ORDER_DETAIL_IDX = detail.ORDER_DETAIL_IDX;
       
            return View(model);
        }

        [HttpPost]
        [CustomAuthorize]
        [ValidateAntiForgeryToken]
        [Route("Update/{id:int}")]
        public ActionResult Update(MyReviewProductInputViewModel model)
        {
            if (ModelState.IsValid)
            {


                if (model.IS_PHOTO == "Y")
                {

                    ImagePlainUpload imageUpload = new ImagePlainUpload { UploadPath = _img_path_review, IsThumbNail = true };

                    if (model.UploadImage != null)
                    {
                        ImageResult imageResult = imageUpload.RenameUploadFile(model.UploadImage);
                        if (imageResult.Success)
                        {
                            model.ADD_IMAGE = imageResult.ImageName;
 
                        }
                    }
                }

                Tuple<string, string> ret = _ReviewService.UpdateMyReview(model);

                TempData["ResultNum"] = ret.Item1;
                TempData["ResultMessage"] = ret.Item2;

                return View(model);
            }

            ModelState.AddModelError("", "필수항목(*)들을 입력해주세요");

            return View(model);
        }

        /**
         * 작성준비 총수
         */
        [ChildActionOnly]
        public string GetReadyToal()
        {
            return string.Format("{0:#,##0}", _ReviewService.ReadyTotal(_user_profile.M_ID).ToString());
        }


        /**
         * 작성완료 총수
         */
        [ChildActionOnly]
        public string GetCompleteToal()
        {
            return string.Format("{0:#,##0}", _ReviewService.GetMyReviewCompleteCnt(_user_profile.M_ID).ToString());
        }
    }
}