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

using AboutMe.Domain.Service.Product;
using AboutMe.Domain.Entity.Product;

namespace AboutMe.Web.Front.Controllers
{
    [RoutePrefix("MyPage/MyReviewExp")]
    [Route("{action=Index}")]
    public class MyReviewExpController : BaseFrontController
    {
        private IReviewService _service;
        private IProductService _service_p;

        public MyReviewExpController(IReviewService s, IProductService p)
        {
            this._service = s;
            _service_p = p;
        }

        // GET: MyExpReview
        [CustomAuthorize]
        public ActionResult Index()
        {
            MyReviewExpListViewModel model = new MyReviewExpListViewModel();

            var tplist = new List< Tuple<SP_REVIEW_MY_EXP_MASTER_SEL_Result, SP_PRODUCT_DETAIL_VIEW_Result> >();
            var masterlist = _service.GetReviewExpMyMasterList(_user_profile.M_ID);

            foreach (var item in masterlist)
	        {
                var tp = new Tuple<SP_REVIEW_MY_EXP_MASTER_SEL_Result, SP_PRODUCT_DETAIL_VIEW_Result>(item, _service_p.ViewProduct(item.P_CODE));
                tplist.Add(tp);
	        }

            model.List = tplist;

            return View(model);
        }

        [CustomAuthorize]
        public ActionResult Write(int? EXP_MASTER_IDX, string P_CODE)
        {
            MyReviewExpWriteViewModel model = new MyReviewExpWriteViewModel();
            model.EXP_MASTER_IDX = EXP_MASTER_IDX;
            model.P_CODE = P_CODE;
            model.ProductInfo = _service.GetProductInfo(P_CODE);
            return View(model);
        }


        [HttpPost]
        [CustomAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult Write(MyReviewExpWriteViewModel model)
        {

            model.ProductInfo = _service.GetProductInfo(model.P_CODE);

            if (ModelState.IsValid)
            {
                MyReviewExpParamOnSaveToDb p = new MyReviewExpParamOnSaveToDb();

                p.M_ID = _user_profile.M_ID;                
                p.EXP_MASTER_IDX = model.EXP_MASTER_IDX;
                p.TITLE = model.TITLE;
                p.SKIN_TYPE = model.SKIN_TYPE;
                p.P_CODE = model.P_CODE;
                p.COMMENT = model.COMMENT;
                p.TAG = model.TAG;

                p.SUB_IMG_1 = model.SUB_IMG_1;
                p.SUB_IMG_2 = model.SUB_IMG_2;
                p.SUB_IMG_3 = model.SUB_IMG_3;

                Tuple<string, string> ret = _service.InsertMyReviewExp(p);

                TempData["ResultNum"] = ret.Item1;
                TempData["ResultMessage"] = ret.Item2;

                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "필수항목(*)들을 입력해주세요");
            return View(model);
        }


        [CustomAuthorize]
        public ActionResult Update(int? ARTICLE_IDX,  string P_CODE)
        {
            MyReviewExpUpdateViewModel model = new MyReviewExpUpdateViewModel();
            model.ARTICLE_IDX = ARTICLE_IDX;
            model.P_CODE = P_CODE;
            model.ProductInfo = _service.GetProductInfo(P_CODE);
            return View(model);
        }


        [HttpPost]
        [CustomAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult Update(MyReviewExpUpdateViewModel model)
        {
            model.ProductInfo = _service.GetProductInfo(model.P_CODE);

            if (ModelState.IsValid)
            {
                MyReviewExpParamOnSaveToDb p = new MyReviewExpParamOnSaveToDb();

                p.M_ID = _user_profile.M_ID;
                p.EXP_MASTER_IDX = model.EXP_MASTER_IDX;
                p.TITLE = model.TITLE;
                p.SKIN_TYPE = model.SKIN_TYPE;
                p.P_CODE = model.P_CODE;
                p.COMMENT = model.COMMENT;
                p.TAG = model.TAG;

                p.SUB_IMG_1 = model.SUB_IMG_1;
                p.SUB_IMG_2 = model.SUB_IMG_2;
                p.SUB_IMG_3 = model.SUB_IMG_3;

                //Tuple<string, string> ret = _service.InsertMyReviewExp(p);

                //TempData["ResultNum"] = ret.Item1;
                //TempData["ResultMessage"] = ret.Item2;

                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "필수항목(*)들을 입력해주세요");
            return View(model);
        }

    }
}