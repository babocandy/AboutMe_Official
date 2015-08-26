using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

using AboutMe.Web.Front.Models;
using AboutMe.Domain.Service.Review;
using AboutMe.Domain.Service.Product;

using AboutMe.Common.Data;
using AboutMe.Common.Helper;
using AboutMe.Domain.Entity.Review;
using System.Text.RegularExpressions;

namespace AboutMe.Web.Front.Controllers
{
    [RoutePrefix("Review")]
    [Route("{action=Product}")]
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
         * 상품리뷰 목록
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
         * 상품리뷰 목록 - JSON
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
         * 체험단 리뷰 목록
         */
        public ActionResult Experience()
        {

            ReviewExpListViewModel model = new ReviewExpListViewModel();

            model.CategoryBeauty = _ProductService.GetCategoryDeptList("SKIN_TYPE", CategoryCode.BEAUTY, "");
            model.CategoryCodeHealth = CategoryCode.HEALTH_DEFAULT;
            model.CategorySelShop = _ProductService.GetCategoryDeptList("SKIN_TYPE", CategoryCode.SEL_SHOP, "");

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
         * 공통. 상품상세에서 상품, 체험단리뷰 함께. 상품리뷰가 첫번째로 보이지기 때문에 상품리뷰 데이타만 넘겨준다.
         */
        [ChildActionOnly]
        public ActionResult ReviewInShoppingDetail(string P_CODE)
        {
            ReviewInProductDetailViewModel model = new ReviewInProductDetailViewModel();
            ReviewListJsonParamInShopping p = new ReviewListJsonParamInShopping();
            p.P_CODE = P_CODE;
            var tp = _ReviewService.GetReviewProductListByProductCode(p);

            model.Reviews = tp.Item1;
            model.Total = tp.Item2;
            model.PageNo = 1;
            model.Pcode = P_CODE;
            model.PageSize = p.PAGE_SIZE??0;

            return View(model);
        }

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