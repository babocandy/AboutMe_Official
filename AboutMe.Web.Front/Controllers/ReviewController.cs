using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

using AboutMe.Web.Front.Models;
using AboutMe.Domain.Service.Review;
using AboutMe.Domain.Service.Product;

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
            model.CategoryBeauty = _ProductService.GetCategoryDeptList("SKIN_TYPE", "101", "");
            model.CategorySelShop = _ProductService.GetCategoryDeptList("SKIN_TYPE", "103", "");
            model.CategoryCodeHealth = "102100100";

            model.Products = _ReviewService.GetReviewProductList(null, model.DefaultCategoryCode, model.DefaultSort);



            return View(model);
        }

        public ActionResult Experience()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetReviewProductList(ReviewProductListParam param)   
        {
            ReviewProductListViewModel model = new ReviewProductListViewModel();
            model.Products = _ReviewService.GetReviewProductList(param.TAIL_IDX, param.CATEGORY_CODE, param.SORT);

            var jsonData = new { Products = model.Products, Success = true, Postdata = new { TAIL_IDX = param.TAIL_IDX, CATEGORY_CODE = param.CATEGORY_CODE, SORT = param.SORT } };

            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        [ChildActionOnly]
        public ActionResult InShoppingDetail(string P_CODE)
        {

            return View();
        }
    }
}