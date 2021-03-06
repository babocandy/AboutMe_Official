﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Diagnostics;
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
            model.WebMainBannerList = _Service.GetDisplayerList(new DisplayerParam { KIND = DisplayerCode.WEB_MAIN_BANNER });

            //웹 메인 중간 배너
            model.WebMiddleBanner = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND = DisplayerCode.WEB_MAIN_MIDDLE_BANNER }));


            //웹 메인 중간 배너
            model.WebSideBanner = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND = DisplayerCode.WEB_MAIN_SIDE_BANNER }));

            //상품전시 그룹 1
            model.WebProductDisplay10 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND =DisplayerCode.WEB_MAIN_PRODUCT_DISPLAY, SUB_KIND= DisplayerCode.SUB_KIND_10, SEQ= 1 }));
            model.WebProductDisplay11 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND =DisplayerCode.WEB_MAIN_PRODUCT_DISPLAY, SUB_KIND= DisplayerCode.SUB_KIND_10, SEQ= 2 }));
            model.WebProductDisplay12 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND =DisplayerCode.WEB_MAIN_PRODUCT_DISPLAY, SUB_KIND= DisplayerCode.SUB_KIND_10, SEQ=3} ));
            model.WebProductDisplay13 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND =DisplayerCode.WEB_MAIN_PRODUCT_DISPLAY, SUB_KIND= DisplayerCode.SUB_KIND_10, SEQ=4} ));

            //상품전시 그룹 2
            model.WebProductDisplay20 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND =DisplayerCode.WEB_MAIN_PRODUCT_DISPLAY, SUB_KIND= DisplayerCode.SUB_KIND_20, SEQ=1} ));
            model.WebProductDisplay21 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND =DisplayerCode.WEB_MAIN_PRODUCT_DISPLAY, SUB_KIND= DisplayerCode.SUB_KIND_20, SEQ=2} ));
            model.WebProductDisplay22 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND =DisplayerCode.WEB_MAIN_PRODUCT_DISPLAY, SUB_KIND= DisplayerCode.SUB_KIND_20, SEQ=3} ));
            model.WebProductDisplay23 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND =DisplayerCode.WEB_MAIN_PRODUCT_DISPLAY, SUB_KIND= DisplayerCode.SUB_KIND_20, SEQ=4} ));
 
            return View(model);
        }

        /**
         * 동영상 관리
         */
        [CustomAuthorize]
        public ActionResult Movie()
        {
            AdminDisplayMovieViewModel model = new AdminDisplayMovieViewModel();

            model.Movie = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND = DisplayerCode.WEB_MAIN_MOVIE_LINK }));

            model.Product10 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND = DisplayerCode.WEB_MAIN_MOVIE_PRODUCT, SUB_KIND = DisplayerCode.SUB_KIND_10, SEQ = 1 }));
            model.Product11 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND = DisplayerCode.WEB_MAIN_MOVIE_PRODUCT, SUB_KIND = DisplayerCode.SUB_KIND_10, SEQ = 2 }));
            model.Product12 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND = DisplayerCode.WEB_MAIN_MOVIE_PRODUCT, SUB_KIND = DisplayerCode.SUB_KIND_10, SEQ = 3 }));
            model.Product13 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND = DisplayerCode.WEB_MAIN_MOVIE_PRODUCT, SUB_KIND = DisplayerCode.SUB_KIND_10, SEQ = 4 }));


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
            model.MobileMainBannerList = _Service.GetDisplayerList(new DisplayerParam { KIND =DisplayerCode.MOBILE_MAIN_BANNER});
            model.MobileTalkOnBeauty = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND =DisplayerCode.MOBILE_MAIN_TALK}));
            model.MobileBest = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND =DisplayerCode.MOBILE_MAIN_BEST}));

            return View(model);
        }

        /**
         * 모바일 쇼핑  전시
         */
        [CustomAuthorize]
        public ActionResult MobileShopping()
        {
            AdminDisplayMobileShoppingViewModel model = new AdminDisplayMobileShoppingViewModel();

            //웹메인 배너 조회
            model.BannerList = _Service.GetDisplayerList(new DisplayerParam { KIND = DisplayerCode.MOBILE_SHOPPING });


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
            model.Web = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND = DisplayerCode.CART_WEB }));

            //모바일
            model.Mobile = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND =DisplayerCode.CART_MOBILE}));

            //상품전시
            model.CommonDisplay10 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND =DisplayerCode.CART_PRODUCT_DISPLAY, SUB_KIND= DisplayerCode.SUB_KIND_10, SEQ=1}));
            model.CommonDisplay11 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND =DisplayerCode.CART_PRODUCT_DISPLAY, SUB_KIND=DisplayerCode.SUB_KIND_10, SEQ=2}));
            model.CommonDisplay12 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND =DisplayerCode.CART_PRODUCT_DISPLAY, SUB_KIND=DisplayerCode.SUB_KIND_10, SEQ=3}));
            model.CommonDisplay13 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND =DisplayerCode.CART_PRODUCT_DISPLAY, SUB_KIND=DisplayerCode.SUB_KIND_10, SEQ=4}));

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
            model.Banner10 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam {KIND= DisplayerCode.GBN_BANNER,SUB_KIND= DisplayerCode.SUB_KIND_10,SEQ= 1}));
            model.Banner11 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam {KIND=DisplayerCode.GBN_BANNER,SUB_KIND= DisplayerCode.SUB_KIND_10,SEQ= 2}));
            model.Banner12 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam {KIND=DisplayerCode.GBN_BANNER,SUB_KIND= DisplayerCode.SUB_KIND_10,SEQ= 3}));
            model.Banner13 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam {KIND=DisplayerCode.GBN_BANNER,SUB_KIND= DisplayerCode.SUB_KIND_10,SEQ= 4}));

            //링크
            model.Link10 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam {KIND=DisplayerCode.GBN_LINK, SUB_KIND=DisplayerCode.SUB_KIND_10,SEQ= 1}));
            model.Link11 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam {KIND=DisplayerCode.GBN_LINK, SUB_KIND=DisplayerCode.SUB_KIND_10,SEQ= 2}));

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
            model.Web10 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam {KIND=DisplayerCode.PDT_DETAIL_WEB, SUB_KIND=DisplayerCode.SUB_KIND_10,SEQ=  1}));
            model.Web11 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam {KIND=DisplayerCode.PDT_DETAIL_WEB, SUB_KIND=DisplayerCode.SUB_KIND_10,SEQ=  2}));
            model.Web12 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam {KIND=DisplayerCode.PDT_DETAIL_WEB, SUB_KIND=DisplayerCode.SUB_KIND_10,SEQ=  3}));

            //모바일
            model.Mobile10 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam {KIND=DisplayerCode.PDT_DETAIL_MOBILE, SUB_KIND=DisplayerCode.SUB_KIND_10,SEQ=  1}));
            model.Mobile11 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam {KIND=DisplayerCode.PDT_DETAIL_MOBILE, SUB_KIND=DisplayerCode.SUB_KIND_10,SEQ=  2}));
            model.Mobile12 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam {KIND=DisplayerCode.PDT_DETAIL_MOBILE, SUB_KIND=DisplayerCode.SUB_KIND_10,SEQ=  3}));

            return View(model);
        }


        /**
         * 체험단 배너 관리 전시
         */
        [CustomAuthorize]
        public ActionResult ExpBanner()
        {
            AdminDisplayExpViewModel model = new AdminDisplayExpViewModel();

            //배너
            model.Mobile = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND = DisplayerCode.EXP_MOBILE}));
            model.Web = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND = DisplayerCode.EXP_WEB }));

            return View(model);
        }


        /**
         * 한개짜리만 가져오기
         */
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
                ImagePlainUpload imageUpload = new ImagePlainUpload { UploadPath = _img_path_display };
                ImageResult imageResult = imageUpload.RenameUploadFile(param.IMG_FILE);

                if (imageResult.Success)
                {
                    imgName = imageResult.ImageName;
                }
            }

            if (!String.IsNullOrEmpty(param.URL) || !String.IsNullOrEmpty(imgName))
            {
                param.IMG_NAME = imgName;

                _Service.SaveImageTypeDisplayer(param);

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

            var tp = _Service.SaveProductTypeDisplayer(param);

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

            var tp = _Service.SaveLinkTypeDisplayer(param);

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




        /****************************************************
         * 
         *              팝업 관리 전시
         * 
         * 
         *********************************************************/

        //팝업조회
        [CustomAuthorize]
        public ActionResult Popup(PopupSearchParam parma)
        {
            AdminDisplayPopupListViewModel model = new AdminDisplayPopupListViewModel();
            model.PopupList = _Service.PopupSel(parma).Item1;
            model.Total = _Service.PopupSel(parma).Item2;
            model.SearchParam = parma;
            return View(model);
        }

        //팝업추가
        [CustomAuthorize]
        public ActionResult PopupAdd(PopupSearchParam parma)
        {
            AdminDisplayPopupInputViewModel model = new AdminDisplayPopupInputViewModel();
            model.SearchParam = parma;
            return View("PopupInput", model);
        }

        [HttpPost]
        [CustomAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult PopupAdd(AdminDisplayPopupInputViewModel model, PopupSearchParam parma)
        {
            model.SearchParam = parma;

            if (ModelState.IsValid)
            {
                if (model.MOBILE_IMG_FILE != null)
                {
                    ImagePlainUpload imageUpload = new ImagePlainUpload { UploadPath = _img_path_display };
                    ImageResult imageResult = imageUpload.RenameUploadFile(model.MOBILE_IMG_FILE);

                    if (imageResult.Success)
                    {
                        model.MOBILE_IMG_NAME = imageResult.ImageName;
                    }
                }

                if (model.WEB_IMG_FILE != null)
                {
                    ImagePlainUpload imageUpload = new ImagePlainUpload { UploadPath = _img_path_display };
                    ImageResult imageResult = imageUpload.RenameUploadFile(model.WEB_IMG_FILE);

                    if (imageResult.Success)
                    {
                        model.WEB_IMG_NAME = imageResult.ImageName;
                    }
                }

                var tp = _Service.PopupAdd(model);


                return RedirectToAction("Popup", "AdminDisplay");
            }

            ModelState.AddModelError("", "필수항목들(*)을 입력해주세요");
            return View("PopupInput", model);
        }




        //팝업수정
        [CustomAuthorize]
        public ActionResult PopupUpdate(int? id, PopupSearchParam parma)
        {
            AdminDisplayPopupInputViewModel model = new AdminDisplayPopupInputViewModel();

            var tp = _Service.PopupInfo(id);

            model.IDX = tp.Item1.IDX;
            model.IS_DISPLAY = tp.Item1.IS_DISPLAY;
            
            model.MEDIA_GBN = tp.Item1.MEDIA_GBN;
            
            model.POS_TOP = tp.Item1.POS_TOP;
            model.POS_LEFT = tp.Item1.POS_LEFT;
            
            model.SIZE_WIDTH = tp.Item1.SIZE_WIDTH;
            model.SIZE_HEIGHT = tp.Item1.SIZE_HEIGHT;
            
            model.MOBILE_IMG_NAME = tp.Item1.MOBILE_IMG;
            model.MOBILE_LINK = tp.Item1.MOBILE_LINK;

            model.WEB_IMG_NAME = tp.Item1.WEB_IMG;
            model.WEB_LINK = tp.Item1.WEB_LINK;
            model.WEB_TARGET = tp.Item1.WEB_TARGET;

            model.TITLE = tp.Item1.TITLE;

            model.DISPLAY_START = tp.Item1.DISPLAY_START != null ? tp.Item1.DISPLAY_START.Value.ToString("yyyy-MM-dd HH:mm") : "";
            model.DISPLAY_END = tp.Item1.DISPLAY_END != null ? tp.Item1.DISPLAY_END.Value.ToString("yyyy-MM-dd HH:mm") : "";


            model.SearchParam = parma;

            return View("PopupInput", model);
        }


        [HttpPost]
        [CustomAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult PopupUpdate(AdminDisplayPopupInputViewModel model, PopupSearchParam parma)
        {

            model.SearchParam = parma;

            if (ModelState.IsValid)
            {

                if (model.MOBILE_IMG_FILE != null)
                {
                    ImagePlainUpload imageUpload = new ImagePlainUpload { UploadPath = _img_path_display };
                    ImageResult imageResult = imageUpload.RenameUploadFile(model.MOBILE_IMG_FILE);

                    if (imageResult.Success)
                    {
                        model.MOBILE_IMG_NAME = imageResult.ImageName;
                    }
                }

                if (model.WEB_IMG_FILE != null)
                {
                    ImagePlainUpload imageUpload = new ImagePlainUpload { UploadPath = _img_path_display };
                    ImageResult imageResult = imageUpload.RenameUploadFile(model.WEB_IMG_FILE);

                    if (imageResult.Success)
                    {
                        model.WEB_IMG_NAME = imageResult.ImageName;
                    }
                }

                var tp = _Service.PopupUpdate(model);
                return RedirectToAction("Popup", "AdminDisplay", model.SearchParam);
            }

            ModelState.AddModelError("", "필수항목들(*)을 입력해주세요");
            return View("PopupInput", model);
         }


        //팝업 전시 여부 수정
        [HttpPost]
        [CustomAuthorize]
        public ActionResult PopupUpdateDisplay(AdminDisplayPopupInputViewModel model)
        {
            var tp = _Service.PopupUpdateDisplay(model);

            TempData["ResultNum"] = tp.Item1;
            TempData["ResultMessage"] = tp.Item2;

            return Redirect(Request.UrlReferrer.ToString());
        }

        //팝업삭제
        [HttpPost]
        [CustomAuthorize]
        public ActionResult PopupRemove(AdminDisplayPopupInputViewModel m)
        {
            var tp = _Service.PopupRemove(m);

            TempData["ResultNum"] = tp.Item1;
            TempData["ResultMessage"] = tp.Item2;

            return Redirect(Request.UrlReferrer.ToString());
        }

    }
}