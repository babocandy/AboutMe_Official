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
                var pdt = _service_p.ViewProduct(item.P_CODE);
                if (pdt == null) pdt = new SP_PRODUCT_DETAIL_VIEW_Result();

                var tp = new Tuple<SP_REVIEW_MY_EXP_MASTER_SEL_Result, SP_PRODUCT_DETAIL_VIEW_Result>(item, pdt);
                tplist.Add(tp);
	        }

            model.List = tplist;

            return View(model);
        }

        [CustomAuthorize]
        public ActionResult Write(int? EXP_MASTER_IDX, string P_CODE)
        {
            MyReviewExpInputViewModel model = new MyReviewExpInputViewModel();
            model.EXP_MASTER_IDX = EXP_MASTER_IDX;
            model.P_CODE = P_CODE;
            model.ProductInfo = _service.GetProductInfo(P_CODE);
            return View("Input", model);
        }


        [HttpPost]
        [CustomAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult Write(MyReviewExpInputViewModel model)
        {

            model.ProductInfo = _service.GetProductInfo(model.P_CODE);

            if (ModelState.IsValid)
            {

                var uploader = new ImagePlainUpload();

                if (model.SUB_IMG_1 != null) uploader.SaveImageByFileName(_img_path_review, model.SUB_IMG_1);
                if (model.SUB_IMG_2 != null) uploader.SaveImageByFileName(_img_path_review, model.SUB_IMG_2);
                if (model.SUB_IMG_3 != null) uploader.SaveImageByFileName(_img_path_review, model.SUB_IMG_3);


                model.M_ID = _user_profile.M_ID;

                Tuple<string, string> ret = _service.InsertMyReviewExp(model);

                TempData["ResultNum"] = ret.Item1;
                TempData["ResultMessage"] = ret.Item2;

                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "필수항목(*)들을 입력해주세요");
            return View("Input", model);
        }


        [CustomAuthorize]
        public ActionResult Update(int? ARTICLE_IDX, int? EXP_MASTER_IDX,  string P_CODE)
        {
            SP_REVIEW_MY_EXP_DETAIL_Result detail = _service.GetMyReviewExpDetail(ARTICLE_IDX);

            MyReviewExpInputViewModel model = new MyReviewExpInputViewModel();

            model.EXP_MASTER_IDX = EXP_MASTER_IDX;
            model.ARTICLE_IDX = ARTICLE_IDX;
            model.P_CODE = P_CODE;

            model.TITLE = detail.TITLE;
            model.TAG = detail.TAG;
            model.SKIN_TYPE = detail.SKIN_TYPE;
            model.SUB_IMG_1 = detail.SUB_IMG_1;
            model.SUB_IMG_2 = detail.SUB_IMG_2;
            model.SUB_IMG_3 = detail.SUB_IMG_3;
            model.COMMENT = detail.COMMENT;



            model.ProductInfo = _service.GetProductInfo(P_CODE);

            return View("Input", model);
        }


        [HttpPost]
        [CustomAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult Update(MyReviewExpInputViewModel model)
        {
            model.ProductInfo = _service.GetProductInfo(model.P_CODE);

            if (ModelState.IsValid)
            {
                /*
                MyReviewExpParamOnSaveToDb p = new MyReviewExpParamOnSaveToDb();

                p.M_ID = _user_profile.M_ID;
                p.ARTICLE_IDX = model.ARTICLE_IDX;
                p.EXP_MASTER_IDX = model.EXP_MASTER_IDX;
                p.TITLE = model.TITLE;
                p.SKIN_TYPE = model.SKIN_TYPE;
                p.P_CODE = model.P_CODE;
                p.COMMENT = model.COMMENT;
                p.TAG = model.TAG;

                Debug.WriteLine("1,"+model.SUB_IMG_1);
                Debug.WriteLine("2," + model.SUB_IMG_2);
                Debug.WriteLine("3," + model.SUB_IMG_3);


                p.SUB_IMG_1 = model.SUB_IMG_1;
                p.SUB_IMG_2 = model.SUB_IMG_2;
                p.SUB_IMG_3 = model.SUB_IMG_3;

                 */

                var uploader = new ImagePlainUpload();
                if (model.SUB_IMG_1 != null) uploader.SaveImageByFileName(_img_path_review, model.SUB_IMG_1);
                if (model.SUB_IMG_2 != null) uploader.SaveImageByFileName(_img_path_review, model.SUB_IMG_2);
                if (model.SUB_IMG_3 != null) uploader.SaveImageByFileName(_img_path_review, model.SUB_IMG_3);

                model.M_ID = _user_profile.M_ID;

                Tuple<string, string> ret = _service.UpdateMyReviewExp(model);

                TempData["ResultNum"] = ret.Item1;
                TempData["ResultMessage"] = ret.Item2;

                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "필수항목(*)들을 입력해주세요");
            return View("Input", model);
        }

    }
}