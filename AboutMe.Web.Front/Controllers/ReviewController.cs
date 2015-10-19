using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

using AboutMe.Web.Front.Models;
using AboutMe.Web.Front.Common.Filters;
using AboutMe.Web.Front.Common;

using AboutMe.Domain.Service.Review;
using AboutMe.Domain.Service.Product;

using AboutMe.Common.Data;
using AboutMe.Common.Helper;
using AboutMe.Domain.Entity.Review;
using System.Text.RegularExpressions;

namespace AboutMe.Web.Front.Controllers
{
    [RoutePrefix("Review")]
    [Route("{action=FreeReview}")]
    public class ReviewController : BaseFrontController
    {
        private IReviewService _ReviewService;
        private IProductService _ProductService;

        public ReviewController(IReviewService _reviewService, IProductService _productService)
        {
            this._ReviewService = _reviewService;
            this._ProductService = _productService;
        }

        /**
         * 
         *  구매리뷰 (구-상품리뷰) 목록
         * 
         */
        public ActionResult Product()
        {
            ReviewProductListViewModel model = new ReviewProductListViewModel();
            model.CategoryBeauty = _ProductService.GetCategoryDeptList("SKIN_TYPE", CategoryCode.BEAUTY, "");
            model.CategoryCodeHealth = CategoryCode.HEALTH_DEFAULT;
            model.CategorySelShop = _ProductService.GetCategoryDeptList("SKIN_TYPE", CategoryCode.SEL_SHOP, "");
            model.CategoryThema = _ReviewService.ThemaList();
            

            var tp = _ReviewService.GetReviewProductList(null, CategoryCode.BEAUTY_DEFAULT, ReviewProductListViewModel.SORT_PHOTO);
            model.Reviews = tp.Item1;
            model.Total = tp.Item2;


            return View(model);
        }


        /**
         * 
         * 구매리뷰 (구-상품리뷰) 목록 - JSON
         * 
         */
        [HttpPost]
        public JsonResult GetReviewProductList(ReviewListJsonParam param)   
        {
            ReviewProductListViewModel model = new ReviewProductListViewModel();
            var tp = _ReviewService.GetReviewProductList(param.TAIL_IDX, param.CATEGORY_CODE, param.SORT);
            model.Reviews = tp.Item1;//ReviewHelper.GetDataForUser(tp.Item1);
            model.Total = tp.Item2;

            var jsonData = new { Total = model.Total, Reviews = model.Reviews, Success = true, Postdata = param };

            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        /**
         * 상품상세에서 구매리뷰 (구-상품리뷰) 조회
         */
        [HttpPost]
        public JsonResult GetReviewProductListInShopping(ReviewListJsonParamInShopping p)
        {
            var tp = _ReviewService.GetReviewProductListByProductCode(p);
            var jsonData = new { Total = tp.Item2, Reviews = tp.Item1, Success = true, Postdata = p };

            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }



        /**
         * 체험단 리뷰 목록
         */
        public ActionResult Experience()
        {

            ReviewExpListViewModel model = new ReviewExpListViewModel();

            model.CategoryBeauty = _ProductService.GetCategoryDeptList("SKIN_TYPE", CategoryCode.BEAUTY, "");
            model.CategoryCodeHealth = CategoryCode.HEALTH_DEFAULT;
            model.CategorySelShop = _ProductService.GetCategoryDeptList("PRODUCT_TYPE", CategoryCode.SEL_SHOP, "");

            var tp = _ReviewService.GetReviewExpList(null, CategoryCode.BEAUTY_DEFAULT, ReviewExpListViewModel.SORT_LASTEST);
            model.Reviews = tp.Item1;// ReviewHelper.GetDataForUser(tp.Item1);
            model.Total = tp.Item2;

            return View(model);
        }


        /**
         * 체험단 리뷰 목록 - JSON
         */
        [HttpPost]
        public JsonResult GetReviewExpList(ReviewListJsonParam param)
        {
            ReviewExpListViewModel model = new ReviewExpListViewModel();
            var tp = _ReviewService.GetReviewExpList(param.TAIL_IDX, param.CATEGORY_CODE, param.SORT);
            model.Reviews = tp.Item1;
            model.Total = tp.Item2;

            var jsonData = new { Total = model.Total, Reviews = model.Reviews, Success = true, Postdata = param };

            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        /**
         * 체험단 리뷰 상세. 상품관련 이벤트 기획전 데이타 포함
         */
        [HttpPost]
        public JsonResult GetReviewExpDetail(ReviewExpDetailParam p)
        {
            var pp = _ReviewService.GetReviewExpDetail(p);
            var jsonData = new { Detail = pp, Success = true, Postdata = p };

            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        /**
         * 상품상세에서 체험단리뷰 조회
         */
        [HttpPost]
        public JsonResult GetReviewExpListInShopping(ReviewListJsonParamInShopping p)
        {
            var tp = _ReviewService.GetReviewExpByProductCode(p);
            var jsonData = new { Total = tp.Item2, Reviews = tp.Item1, Success = true, Postdata = p };

            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        /**
         * 공통. 상품상세에서 상품, 체험단리뷰 함께. (신)상품리뷰가 첫번째로 보이지기 때문에 상품리뷰 데이타만 넘겨준다.
         *  ==> 2015.10.12 (신)상품리뷰데이터을 가져오는것으로 변경함  구-상품리뷰는 구매리뷰로 변경됨
         */
        [ChildActionOnly]
        public ActionResult ReviewInShoppingDetail(string P_CODE, string P_CATE_CODE = "")
        {
            ReviewInProductDetailViewModel model = new ReviewInProductDetailViewModel();
            ReviewListJsonParamInShopping p = new ReviewListJsonParamInShopping();
            p.P_CODE = P_CODE;
            
            //var tp = _ReviewService.GetReviewProductListByProductCode(p); // 2015.10.12 아래로 변경
            var tp = _ReviewService.GetReviewFreeListByProductCode(p);

            //model.Reviews = tp.Item1; 2015.10.12 아래로 변경 
            model.FreeReviews = tp.Item1; 
            model.Total = tp.Item2;
            model.PageNo = 1;
            model.Pcode = P_CODE;
            model.PageSize = p.PAGE_SIZE??0;
            model.P_CATE_CODE = P_CATE_CODE;
            //model.ProductDetail = _ProductService.ViewProduct(P_CODE);
            return View(model);
        }



        #region (신)상품리뷰 Version 2 By 송선우 ########################################################

        /**
         * 공통. 상품리뷰 데이터
         */
        /**
        [ChildActionOnly]
        public ActionResult FreeReviewInShoppingDetail(string P_CODE, string P_CATE_CODE = "")
        {
            ReviewInProductDetailViewModel model = new ReviewInProductDetailViewModel();
            ReviewListJsonParamInShopping p = new ReviewListJsonParamInShopping();
            p.P_CODE = P_CODE;
            var tp = _ReviewService.GetReviewFreeListByProductCode(p);

            model.FreeReviews = tp.Item1;
            model.Total = tp.Item2;
            model.PageNo = 1;
            model.Pcode = P_CODE;
            model.PageSize = p.PAGE_SIZE ?? 0;
            model.P_CATE_CODE = P_CATE_CODE;
            //model.ProductDetail = _ProductService.ViewProduct(P_CODE);
            return View(model);
        }
         * **/


        /**
         * 
         *  (신-상품리뷰) 목록
         * 
         */
        public ActionResult FreeReview()
        {
            ReviewProductListViewModel model = new ReviewProductListViewModel();
            model.CategoryBeauty = _ProductService.GetCategoryDeptList("SKIN_TYPE", CategoryCode.BEAUTY, "");
            model.CategoryCodeHealth = CategoryCode.HEALTH_DEFAULT;
            model.CategorySelShop = _ProductService.GetCategoryDeptList("SKIN_TYPE", CategoryCode.SEL_SHOP, "");
            model.CategoryThema = _ReviewService.ThemaList();


            var tp = _ReviewService.GetReviewFreeList(null, CategoryCode.BEAUTY_DEFAULT, ReviewProductListViewModel.SORT_PHOTO);
            model.FreeReviews = tp.Item1;
            model.Total = tp.Item2;


            return View(model);
        }



        /**
         * 
         * 신-상품리뷰 목록 - JSON
         * 
         */
        [HttpPost]
        public JsonResult GetReviewFreeList(ReviewListJsonParam param)
        {
            ReviewProductListViewModel model = new ReviewProductListViewModel();
            var tp = _ReviewService.GetReviewFreeList(param.TAIL_IDX, param.CATEGORY_CODE, param.SORT);
            model.FreeReviews = tp.Item1;//ReviewHelper.GetDataForUser(tp.Item1);
            model.Total = tp.Item2;

            var jsonData = new { Total = model.Total, FreeReviews = model.FreeReviews, Success = true, Postdata = param };

            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        /**
         * 신-상품상세에서 상품리뷰 조회
         */
        [HttpPost]
        public JsonResult GetReviewFreeListInShopping(ReviewListJsonParamInShopping p)
        {
            var tp = _ReviewService.GetReviewFreeListByProductCode(p);
            var jsonData = new { Total = tp.Item2, FreeReviews = tp.Item1, Success = true, Postdata = p };

            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }


        /** 
         * (신)상품리뷰 작성 작성
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


            if (ModelState.IsValid)
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


                Tuple<string, string> ret = _ReviewService.InsertFreeReview(model);

                TempData["ResultNum"] = ret.Item1;
                TempData["ResultMessage"] = ret.Item2;

                return View(model);
            }

            ModelState.AddModelError("", "필수항목(*)들을 입력해주세요");

            return View(model);
        }



        /**
         * 신-상품리뷰 수정
         */
        [HttpGet]
        [CustomAuthorize]
        [Route("Update/{id:int}")]
        public ActionResult Update(int? id)
        {
            MyReviewProductInputViewModel model = new MyReviewProductInputViewModel();


            //상품리뷰 상세
            var detail = _ReviewService.GetReviewFreeDetail(id);

            model.IDX = detail.IDX;
            model.COMMENT = detail.COMMENT;
            model.P_MAIN_IMG = detail.P_MAIN_IMG;
            model.P_NAME = detail.P_NAME;
            model.P_SUB_TITLE = detail.P_SUB_TITLE;
            model.C_CATE_CODE = detail.C_CATE_CODE;
            model.SKIN_TYPE = detail.SKIN_TYPE;
            model.SKIN_TYPE_LBL = detail.SKIN_TYPE_LBL;
            model.IS_PHOTO = detail.IS_PHOTO;
            model.ADD_IMAGE = detail.ADD_IMAGE;
            model.ORDER_DETAIL_IDX = 1;

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


                Tuple<string, string> ret = _ReviewService.UpdateFreeReview(model);

                TempData["ResultNum"] = ret.Item1;
                TempData["ResultMessage"] = ret.Item2;

                return View(model);
            }

            ModelState.AddModelError("", "필수항목(*)들을 입력해주세요");

            return View(model);
        }
        
        #endregion
        /*
         * 
  Regex urlregex = new Regex(@"(#)((?:[A-Za-z0-9-_]*))", RegexOptions.IgnoreCase RegexOptions.Compiled);
            return urlregex.Replace(urlText, "<a href=\"$1$2\" style=\"color: #f68b1f;\">$1$2</a>");
         * 
       private string replaceHtml(string s){
           
           var result = s;
           string pattern = @"<[^>]*?>|<[^>]*>";

           Regex rgx = new Regex(pattern);
           result = rgx.Replace(result, String.Empty);
           //result = result.Replace(@"&amp;amp;", @"&amp;");
           result = result.Replace(@"&amp;", @"&");
           result = result.Replace(@"&pound;", @"£");
           result = result.Replace(@"&#163;", @"£");
           result = result.Replace(@"&quot;", @"""");
           result = result.Replace(@"&apos;", @"'");
           //result = result.Replace(@"&", @"&amp;");
           //result = result.Replace(@"£", @"&pound;");
           //result = result.Replace(@"""", @"&quot;");
           //result = result.Replace(@"'", @"&apos;");
            
           return result;

       }
       */
    }
}