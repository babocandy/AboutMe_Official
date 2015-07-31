using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Diagnostics;
using AboutMe.Web.Admin.Models;
using AboutMe.Web.Admin.Common.Filters;
using AboutMe.Web.Admin.Common;
using AboutMe.Domain.Entity.AdminDisplay;
using AboutMe.Domain.Service.AdminDisplay;
using AboutMe.Common.Helper;
using AboutMe.Common.Data;


namespace AboutMe.Web.Admin.Controllers
{
    public class AdminDisplayController : BaseAdminController
    {
        private IAdminDisplayService _Service;
        public AdminDisplayController(IAdminDisplayService service)
        {
            this._Service = service;
        }

        /**
         * 웹메인
         */
        [CustomAuthorize]
        public ActionResult Index()
        {
            AdminDisplayWebMainViewModel model = new AdminDisplayWebMainViewModel();

            //웹메인 배너 조회
            model.WebMainBannerList = _Service.GetDisplayerList(DisplayerCodes.WEB_MAIN_BANNER);

            //웹 메인 중간 배너
            model.WebMiddleBanner = GetOneDisplayResult(_Service.GetDisplayerList(DisplayerCodes.WEB_MAIN_MIDDLE_BANNER));

            //상품전시 그룹 1
            model.WebProductDisplay10 = GetOneDisplayResult(_Service.GetDisplayerList(DisplayerCodes.WEB_MAIN_PRODUCT_DISPLAY, DisplayerCodes.SUB_KIND_10, 1));
            model.WebProductDisplay11 = GetOneDisplayResult(_Service.GetDisplayerList(DisplayerCodes.WEB_MAIN_PRODUCT_DISPLAY, DisplayerCodes.SUB_KIND_10, 2));
            model.WebProductDisplay12 = GetOneDisplayResult(_Service.GetDisplayerList(DisplayerCodes.WEB_MAIN_PRODUCT_DISPLAY, DisplayerCodes.SUB_KIND_10, 3));
            model.WebProductDisplay13 = GetOneDisplayResult(_Service.GetDisplayerList(DisplayerCodes.WEB_MAIN_PRODUCT_DISPLAY, DisplayerCodes.SUB_KIND_10, 4));

            //상품전시 그룹 2
            model.WebProductDisplay20 = GetOneDisplayResult(_Service.GetDisplayerList(DisplayerCodes.WEB_MAIN_PRODUCT_DISPLAY, DisplayerCodes.SUB_KIND_20, 1));
            model.WebProductDisplay21 = GetOneDisplayResult(_Service.GetDisplayerList(DisplayerCodes.WEB_MAIN_PRODUCT_DISPLAY, DisplayerCodes.SUB_KIND_20, 2));
            model.WebProductDisplay22 = GetOneDisplayResult(_Service.GetDisplayerList(DisplayerCodes.WEB_MAIN_PRODUCT_DISPLAY, DisplayerCodes.SUB_KIND_20, 3));
            model.WebProductDisplay23 = GetOneDisplayResult(_Service.GetDisplayerList(DisplayerCodes.WEB_MAIN_PRODUCT_DISPLAY, DisplayerCodes.SUB_KIND_20, 4));
 
            return View(model);
        }


        /**
         * 모바일 메인 전시
         */
        [CustomAuthorize]
        public ActionResult MobileMain()
        {
            AdminDisplayMobileMainViewModel model = new AdminDisplayMobileMainViewModel();

            //웹메인 배너 조회
            model.MobileMainBannerList = _Service.GetDisplayerList(DisplayerCodes.MOBILE_MAIN_BANNER);
            model.MobileTalkOnBeauty = GetOneDisplayResult(_Service.GetDisplayerList(DisplayerCodes.MOBILE_MAIN_TALK));
            model.MobileBest = GetOneDisplayResult(_Service.GetDisplayerList(DisplayerCodes.MOBILE_MAIN_BEST));

            return View(model);
        }

        /**
         * 장바구니 배너 관리 전시
         */
        [CustomAuthorize]
        public ActionResult Cart()
        {
            AdminDisplayCartViewModel model = new AdminDisplayCartViewModel();

            //웹
            model.Web = GetOneDisplayResult(_Service.GetDisplayerList(DisplayerCodes.CART_WEB));

            //모바일
            model.Mobile = GetOneDisplayResult(_Service.GetDisplayerList(DisplayerCodes.CART_MOBILE));

            //상품전시
            model.CommonDisplay10 = GetOneDisplayResult(_Service.GetDisplayerList(DisplayerCodes.CART_PRODUCT_DISPLAY, DisplayerCodes.SUB_KIND_10, 1));
            model.CommonDisplay11 = GetOneDisplayResult(_Service.GetDisplayerList(DisplayerCodes.CART_PRODUCT_DISPLAY, DisplayerCodes.SUB_KIND_10, 2));
            model.CommonDisplay12 = GetOneDisplayResult(_Service.GetDisplayerList(DisplayerCodes.CART_PRODUCT_DISPLAY, DisplayerCodes.SUB_KIND_10, 3));
            model.CommonDisplay13 = GetOneDisplayResult(_Service.GetDisplayerList(DisplayerCodes.CART_PRODUCT_DISPLAY, DisplayerCodes.SUB_KIND_10, 4));

            return View(model);
        }

        /**
         * GBN 배너 관리 전시
         */
        [CustomAuthorize]
        public ActionResult GBNBanner()
        {
            AdminDisplayGBNViewModel model = new AdminDisplayGBNViewModel();
            
            //배너
            model.Banner10 = GetOneDisplayResult(_Service.GetDisplayerList(DisplayerCodes.GBN_BANNER, DisplayerCodes.SUB_KIND_10, 1));
            model.Banner11 = GetOneDisplayResult(_Service.GetDisplayerList(DisplayerCodes.GBN_BANNER, DisplayerCodes.SUB_KIND_10, 2));
            model.Banner12 = GetOneDisplayResult(_Service.GetDisplayerList(DisplayerCodes.GBN_BANNER, DisplayerCodes.SUB_KIND_10, 3));
            model.Banner13 = GetOneDisplayResult(_Service.GetDisplayerList(DisplayerCodes.GBN_BANNER, DisplayerCodes.SUB_KIND_10, 4));

            //링크
            model.Link10 = GetOneDisplayResult(_Service.GetDisplayerList(DisplayerCodes.GBN_LINK, DisplayerCodes.SUB_KIND_10, 1));
            model.Link11 = GetOneDisplayResult(_Service.GetDisplayerList(DisplayerCodes.GBN_LINK, DisplayerCodes.SUB_KIND_10, 2));

            return View(model);
        }

        /**
         * 상세 배너 관리 전시
         */
        [CustomAuthorize]
        public ActionResult ProductDetailBanner()
        {
            AdminDisplayProductBannerViewModel model = new AdminDisplayProductBannerViewModel();

            //웹
            model.Web10 = GetOneDisplayResult(_Service.GetDisplayerList(DisplayerCodes.PDT_DETAIL_WEB, DisplayerCodes.SUB_KIND_10, 1));
            model.Web11 = GetOneDisplayResult(_Service.GetDisplayerList(DisplayerCodes.PDT_DETAIL_WEB, DisplayerCodes.SUB_KIND_10, 2));
            model.Web12 = GetOneDisplayResult(_Service.GetDisplayerList(DisplayerCodes.PDT_DETAIL_WEB, DisplayerCodes.SUB_KIND_10, 3));

            //모바일
            model.Mobile10 = GetOneDisplayResult(_Service.GetDisplayerList(DisplayerCodes.PDT_DETAIL_MOBILE, DisplayerCodes.SUB_KIND_10, 1));
            model.Mobile11 = GetOneDisplayResult(_Service.GetDisplayerList(DisplayerCodes.PDT_DETAIL_MOBILE, DisplayerCodes.SUB_KIND_10, 2));
            model.Mobile12 = GetOneDisplayResult(_Service.GetDisplayerList(DisplayerCodes.PDT_DETAIL_MOBILE, DisplayerCodes.SUB_KIND_10, 3));

            return View(model);
        }

        public ActionResult Popup()
        {
            return View();
        }


        [ChildActionOnly]
        private SP_ADMIN_DISPLAY_SEL_Result GetOneDisplayResult(List<SP_ADMIN_DISPLAY_SEL_Result> list)
        {
            return list.Count > 0 ? list[0] : new SP_ADMIN_DISPLAY_SEL_Result();
        }

        /**
         * 이미지 타입 전신물 저장
         */
        [HttpPost]
        [CustomAuthorize]
        public ActionResult SaveImageTypeDisplayer(DisplayerParam param)
        {

            string imgName = param.IMG_NAME;

            if (param.IMG_FILE != null)
            {
                ImageUpload imageUpload = new ImageUpload { UploadPath = _img_path_display, addMobileImage = false };
                ImageResult imageResult = imageUpload.RenameUploadFile(param.IMG_FILE);

                if (imageResult.Success)
                {
                    imgName = imageResult.ImageName;
                }
            }

            if (!String.IsNullOrEmpty(param.URL) || !String.IsNullOrEmpty(imgName))
            {
                _Service.SaveImageTypeDisplayer(param.IDX, param.KIND, param.SUB_KIND, param.URL, imgName, param.SEQ);

            }

            return Redirect(Request.UrlReferrer.ToString());
        }

        /**
         * 상품 타입 전신물 저장
         */
        [HttpPost]
        [CustomAuthorize]
        public ActionResult SaveProductTypeDisplayer(DisplayerParam param)
        {

            var tp = _Service.SaveProductTypeDisplayer(param.IDX, param.KIND, param.SUB_KIND, param.P_CODE, param.SEQ);

            TempData["ResultNum"] = tp.Item1;
            TempData["ResultMessage"] = tp.Item2;

            return Redirect(Request.UrlReferrer.ToString());
        }


        /**
         * 링크 타입 전신물 저장
         */
        [HttpPost]
        [CustomAuthorize]
        public ActionResult SaveLinkTypeDisplayer(DisplayerParam param)
        {

            var tp = _Service.SaveLinkTypeDisplayer(param.IDX, param.KIND, param.SUB_KIND, param.SEQ, param.TITLE1, param.TITLE2, param.URL);

            TempData["ResultNum"] = tp.Item1;
            TempData["ResultMessage"] = tp.Item2;

            return Redirect(Request.UrlReferrer.ToString());
        }


        /**
         * 전시물 삭제
         */
        [HttpPost]
        [CustomAuthorize]
        public ActionResult RemoveDisplayer(DisplayerParam param)
        {
            if (param.IDX != null)
            {
                _Service.RemoveDisplayer(param.IDX);
            }

            return Redirect(Request.UrlReferrer.ToString());
        }

        /**
         * 전시물의 이미지만 삭제
         */
        [HttpPost]
        [CustomAuthorize]
        public ActionResult RemoveOnlyImageInDisplayer(DisplayerParam param)
        {
            if (param.IDX != null)
            {
                _Service.RemoveOnlyImageInDisplayer(param.IDX);
            }

            return Redirect(Request.UrlReferrer.ToString());
        }

    }
}