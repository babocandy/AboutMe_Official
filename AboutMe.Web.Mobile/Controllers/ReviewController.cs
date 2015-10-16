/**
 * 
 *  moile Revew. Product + Experience
 *  writer      : dykim 
 *  create date : 2015.08.15
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

using AboutMe.Domain.Service.Review;
using AboutMe.Domain.Service.Product;

using AboutMe.Common.Data;
using AboutMe.Common.Helper;
using AboutMe.Web.Mobile.Common.Filters;
using AboutMe.Domain.Entity.Review;
using System.Text.RegularExpressions;

namespace AboutMe.Web.Mobile.Controllers
{
    [RoutePrefix("Review")]
    [Route("{action=Product}")]
    public class ReviewController : BaseMobileController
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
         * 상품리뷰 목록
         * 
         */
        public ActionResult Product(ReviewListMobileUrlParam p)
        {
            ReviewProductListViewModel model = new ReviewProductListViewModel();
            model.CategoryBeauty = _ProductService.GetCategoryDeptList("SKIN_TYPE", CategoryCode.BEAUTY, "");
            model.CategoryCodeHealth = CategoryCode.HEALTH_DEFAULT;
            model.CategorySelShop = _ProductService.GetCategoryDeptList("SKIN_TYPE", CategoryCode.SEL_SHOP, "");
            model.CategoryThema = _ReviewService.ThemaList();

            p.CATE = p.CATE == null ? CategoryCode.BEAUTY_MOBLE_PARAM : p.CATE;
            p.CATE_CODE = p.CATE_CODE == null ? CategoryCode.BEAUTY_DEFAULT : p.CATE_CODE;
            p.SORT = p.SORT == null ? ReviewProductListViewModel.SORT_PHOTO : p.SORT;


            var tp = _ReviewService.GetReviewProductListMobile(p);

            model.ReviewsMobile = tp.Item1;
            model.Pages = tp.Item2;
            model.Total = tp.Item3;
            model.MobileParam = p;
            model.PrevPage = p.PAGE - 1 < 1 ? 1 : p.PAGE -1;
            model.NextPage = p.PAGE + 1 > model.Pages ? model.Pages : p.PAGE  +1;

            return View(model);
        }




        /**
         * 체험단 리뷰 목록
         */
        public ActionResult Experience(ReviewListMobileUrlParam p)
        {

            ReviewExpListViewModel model = new ReviewExpListViewModel();

            model.CategoryBeauty = _ProductService.GetCategoryDeptList("SKIN_TYPE", CategoryCode.BEAUTY, "");
            model.CategoryCodeHealth = CategoryCode.HEALTH_DEFAULT;
            model.CategorySelShop = _ProductService.GetCategoryDeptList("SKIN_TYPE", CategoryCode.SEL_SHOP, "");


            p.CATE = p.CATE == null ? CategoryCode.BEAUTY_MOBLE_PARAM : p.CATE;
            p.CATE_CODE = p.CATE_CODE == null ? CategoryCode.BEAUTY_DEFAULT : p.CATE_CODE;
            p.SORT = p.SORT == null ? ReviewExpListViewModel.SORT_LASTEST : p.SORT;

           // Debug.WriteLine("ReviewListMobileUrlParam " + p );

            var tp = _ReviewService.GetReviewExpListMobile(p);
            model.ReviewsMobile = tp.Item1;
            model.Pages = tp.Item2;
            model.Total = tp.Item3;
            model.MobileParam = p;
            model.PrevPage = p.PAGE - 1 < 1 ? 1 : p.PAGE -1;
            model.NextPage = p.PAGE + 1 > model.Pages ? model.Pages : p.PAGE +1;

            return View(model);
        }

        public ActionResult ExperienceDetail(ReviewExpDetailParam p)
        {
           
            var model = _ReviewService.GetReviewExpDetail(p);
            if (model ==  null)
            {
                model = new SP_REVIEW_EXP_DETAIL_Result();
            }
            return View(model);
        }

        /**
         * 공통. 상품상세에서 상품, 구매, 체험단리뷰 함께. 상품리뷰가 첫번째로 보이지기 때문에 상품리뷰 데이타만 넘겨준다.
         */
        [ChildActionOnly]
        public ActionResult ReviewInShoppingDetail(string P_CODE, string P_CATE_CODE = "")
        {
            ReviewInProductDetailViewModel model = new ReviewInProductDetailViewModel();

            ReviewListJsonParamInShopping p = new ReviewListJsonParamInShopping();
            p.P_CODE = P_CODE;
            
            //var tp = _ReviewService.GetReviewProductListByProductCode(p); //(구)상품리뷰  2015.10.16
            var tp = _ReviewService.GetReviewFreeListByProductCode(p);

            //model.Reviews = tp.Item1;
            model.FreeReviews = tp.Item1;
            model.Total = tp.Item2;
            model.PageNo = 1;
            model.Pcode = P_CODE;
            model.PageSize = p.PAGE_SIZE?? 0;
            model.P_CATE_CODE = P_CATE_CODE;

            return View(model);
        }

        /**
         * 상품상세에서 상품리뷰 조회
         */
        [HttpPost]
        public JsonResult GetReviewProductListInShopping(ReviewListJsonParamInShopping p)
        {
            var tp = _ReviewService.GetReviewProductListByProductCode(p);
            var jsonData = new { Total = tp.Item2, Reviews = tp.Item1, Success = true, Postdata = p };

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

        [ChildActionOnly]
        public ActionResult ProductByTheMostReview()
        {
            ProductByTheMostReviewViewModel model = new ProductByTheMostReviewViewModel();
            model.ReviewDetail = _ReviewService.GetReviewDetailByMostReviewPdt();
            model.ReviewTotal = _ReviewService.GetReviewTotalByProductCode(model.ReviewDetail.P_CODE);
            model.ProductDetail = _ProductService.ViewProductMobile(model.ReviewDetail.P_CODE);
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
         * 
         * 상품리뷰 작성
         *
         */
        [HttpGet]
        [CustomAuthorize]
        public ActionResult FreeWrite(MyReviewPdtInputParam p)
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
        public ActionResult FreeWrite(MyReviewProductInputViewModel model)
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
                Debug.WriteLine("model.UploadImage - " + model.UploadImage);

                if (model.UploadImage != null)
                {
                    ImageResult imageResult = imageUpload.RenameUploadFile(model.UploadImage);
                    if (imageResult.Success)
                    {
                        //Debug.WriteLine(" imageResult.ImageName - " + imageResult.ImageName);
                        model.ADD_IMAGE = imageResult.ImageName;

                    }
                }

                //Tuple<string, string> ret = _ReviewService.InsertMyReview(model);
                Tuple<string, string> ret = _ReviewService.InsertFreeReview(model);

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
        /**
        [HttpGet]
        [CustomAuthorize]
        public ActionResult FreeComplete(int page = 1)
        {
            MyReviewCompleteViewModel model = new MyReviewCompleteViewModel();

            model.PageNo = page;
            model.Reviews = _ReviewService.GetMyReviewCompleteList(_user_profile.M_ID, page);
            model.Total = _ReviewService.GetMyReviewCompleteCnt(_user_profile.M_ID);

            model.Pages = (int)Math.Ceiling((float)model.Total / (float)model.PageSize);
            model.PrevPage = page - 1 < 1 ? 1 : page - 1;
            model.NextPage = page + 1 > model.Pages ? model.Pages : page + 1;

            return View(model);
        }
        **/

        /**
         * 
         * 나의리뷰 수정
         * 
         */
        [HttpGet]
        [CustomAuthorize]
        [Route("FreeUpdate/{id:int}")]
        public ActionResult FreeUpdate(int? id)
        {
            MyReviewProductInputViewModel model = new MyReviewProductInputViewModel();


            //상품리뷰 상세
            //var detail = _ReviewService.GetReviewProductDetail(id);
            var detail = _ReviewService.GetReviewFreeDetail(id);

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
            //model.ORDER_DETAIL_IDX = detail.ORDER_DETAIL_IDX;
            model.ORDER_DETAIL_IDX = 0;
            model.P_CODE = detail.P_CODE;

            return View(model);
        }

        [HttpPost]
        [CustomAuthorize]
        [ValidateAntiForgeryToken]
        [Route("FreeUpdate/{id:int}")]
        public ActionResult FreeUpdate(MyReviewProductInputViewModel model)
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

                //Tuple<string, string> ret = _ReviewService.UpdateMyReview(model);
                Tuple<string, string> ret = _ReviewService.UpdateFreeReview(model);

                TempData["ResultNum"] = ret.Item1;
                TempData["ResultMessage"] = ret.Item2;

                return View(model);
            }

            ModelState.AddModelError("", "필수항목(*)들을 입력해주세요");

            return View(model);
        }

        #endregion
    }
}