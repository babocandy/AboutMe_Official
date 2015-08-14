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
            model.PrevPage = p.PAGE - 1 < 1 ? 1 : p.PAGE;
            model.NextPage = p.PAGE + 1 > model.Pages ? model.Pages : p.PAGE;

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

            Debug.WriteLine("ReviewListMobileUrlParam " + p );

            var tp = _ReviewService.GetReviewExpListMobile(p);
            model.ReviewsMobile = tp.Item1;
            model.Pages = tp.Item2;
            model.Total = tp.Item3;
            model.MobileParam = p;
            model.PrevPage = p.PAGE - 1 < 1 ? 1 : p.PAGE;
            model.NextPage = p.PAGE + 1 > model.Pages ? model.Pages : p.PAGE;

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




    }
}