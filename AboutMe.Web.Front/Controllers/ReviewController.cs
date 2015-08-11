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
        /*// GET: Review
        public ActionResult Index()
        {
            return View();
        }*/

        public ActionResult Product()
        {
            ReviewProductListViewModel model = new ReviewProductListViewModel();
            model.CategoryBeauty = _ProductService.GetCategoryDeptList("SKIN_TYPE", CategoryCode.BEAUTY, "");
            model.CategoryCodeHealth = CategoryCode.HEALTH_DEFAULT;
            model.CategorySelShop = _ProductService.GetCategoryDeptList("SKIN_TYPE", CategoryCode.SEL_SHOP, "");
            model.CategoryThema = _ReviewService.ThemaList();
            

            var tp = _ReviewService.GetReviewProductList(null, CategoryCode.BEAUTY_DEFAULT, ReviewProductListViewModel.SORT_PHOTO);
            model.Reviews = tp.Item1;// ReviewHelper.GetDataForUser(tp.Item1);
            model.Total = tp.Item2;


            return View(model);
        }



        [HttpPost]
        public JsonResult GetReviewProductList(ReviewProductListParam param)   
        {
            ReviewProductListViewModel model = new ReviewProductListViewModel();
            var tp = _ReviewService.GetReviewProductList(param.TAIL_IDX, param.CATEGORY_CODE, param.SORT);
            model.Reviews = tp.Item1;//ReviewHelper.GetDataForUser(tp.Item1);
            model.Total = tp.Item2;

            var jsonData = new { Total = model.Total, Reviews = model.Reviews, Success = true, Postdata = new { TAIL_IDX = param.TAIL_IDX, CATEGORY_CODE = param.CATEGORY_CODE, SORT = param.SORT } };

            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        [ChildActionOnly]
        public ActionResult ProductInShoppingDetail(string P_CODE)
        {
            MyReviewInProductDetailViewModel model = new MyReviewInProductDetailViewModel();
            
            var tp = _ReviewService.GetReviewProductListByProductCode(P_CODE);
            model.Reviews = tp.Item1;
            model.Total = tp.Item2;
            model.PageNo = 1;
            model.Pcode = P_CODE;

            return View(model);
        }


        [HttpPost]
        public JsonResult GetReviewProductListInShopping(ReviewProductListParamInShopping param)
        {
           // MyReviewInProductDetailViewModel model = new MyReviewInProductDetailViewModel();

            var tp = _ReviewService.GetReviewProductListByProductCode(param.P_CODE, param.PAGE_NO);
            //model.Reviews = ReviewHelper.GetDataForUser(tp.Item1);
           // model.Reviews = tp.Item1;
            //model.Total = tp.Item2;
           // model.Pcode = param.P_CODE;

            var jsonData = new { Total = tp.Item2, Reviews = tp.Item1, Success = true, Postdata = new { PAGE_NO = param.PAGE_NO, P_CODE = param.P_CODE } };

            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

    }
}