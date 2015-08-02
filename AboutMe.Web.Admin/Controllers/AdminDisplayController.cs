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
            model.WebMainBannerList = _Service.GetDisplayerList(new DisplayerParam { KIND = DisplayerCodes.WEB_MAIN_BANNER });

            //웹 메인 중간 배너
            model.WebMiddleBanner = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND = DisplayerCodes.WEB_MAIN_MIDDLE_BANNER }));

            //상품전시 그룹 1
            model.WebProductDisplay10 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND =DisplayerCodes.WEB_MAIN_PRODUCT_DISPLAY, SUB_KIND= DisplayerCodes.SUB_KIND_10, SEQ= 1 }));
            model.WebProductDisplay11 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND =DisplayerCodes.WEB_MAIN_PRODUCT_DISPLAY, SUB_KIND= DisplayerCodes.SUB_KIND_10, SEQ= 2 }));
            model.WebProductDisplay12 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND =DisplayerCodes.WEB_MAIN_PRODUCT_DISPLAY, SUB_KIND= DisplayerCodes.SUB_KIND_10, SEQ=3} ));
            model.WebProductDisplay13 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND =DisplayerCodes.WEB_MAIN_PRODUCT_DISPLAY, SUB_KIND= DisplayerCodes.SUB_KIND_10, SEQ=4} ));

            //상품전시 그룹 2
            model.WebProductDisplay20 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND =DisplayerCodes.WEB_MAIN_PRODUCT_DISPLAY, SUB_KIND= DisplayerCodes.SUB_KIND_20, SEQ=1} ));
            model.WebProductDisplay21 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND =DisplayerCodes.WEB_MAIN_PRODUCT_DISPLAY, SUB_KIND= DisplayerCodes.SUB_KIND_20, SEQ=2} ));
            model.WebProductDisplay22 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND =DisplayerCodes.WEB_MAIN_PRODUCT_DISPLAY, SUB_KIND= DisplayerCodes.SUB_KIND_20, SEQ=3} ));
            model.WebProductDisplay23 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND =DisplayerCodes.WEB_MAIN_PRODUCT_DISPLAY, SUB_KIND= DisplayerCodes.SUB_KIND_20, SEQ=4} ));
 
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
            model.MobileMainBannerList = _Service.GetDisplayerList(new DisplayerParam { KIND =DisplayerCodes.MOBILE_MAIN_BANNER});
            model.MobileTalkOnBeauty = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND =DisplayerCodes.MOBILE_MAIN_TALK}));
            model.MobileBest = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND =DisplayerCodes.MOBILE_MAIN_BEST}));

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
            model.Web = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND = DisplayerCodes.CART_WEB }));

            //모바일
            model.Mobile = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND =DisplayerCodes.CART_MOBILE}));

            //상품전시
            model.CommonDisplay10 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND =DisplayerCodes.CART_PRODUCT_DISPLAY, SUB_KIND= DisplayerCodes.SUB_KIND_10, SEQ=1}));
            model.CommonDisplay11 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND =DisplayerCodes.CART_PRODUCT_DISPLAY, SUB_KIND=DisplayerCodes.SUB_KIND_10, SEQ=2}));
            model.CommonDisplay12 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND =DisplayerCodes.CART_PRODUCT_DISPLAY, SUB_KIND=DisplayerCodes.SUB_KIND_10, SEQ=3}));
            model.CommonDisplay13 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam { KIND =DisplayerCodes.CART_PRODUCT_DISPLAY, SUB_KIND=DisplayerCodes.SUB_KIND_10, SEQ=4}));

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
            model.Banner10 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam {KIND= DisplayerCodes.GBN_BANNER,SUB_KIND= DisplayerCodes.SUB_KIND_10,SEQ= 1}));
            model.Banner11 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam {KIND=DisplayerCodes.GBN_BANNER,SUB_KIND= DisplayerCodes.SUB_KIND_10,SEQ= 2}));
            model.Banner12 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam {KIND=DisplayerCodes.GBN_BANNER,SUB_KIND= DisplayerCodes.SUB_KIND_10,SEQ= 3}));
            model.Banner13 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam {KIND=DisplayerCodes.GBN_BANNER,SUB_KIND= DisplayerCodes.SUB_KIND_10,SEQ= 4}));

            //링크
            model.Link10 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam {KIND=DisplayerCodes.GBN_LINK, SUB_KIND=DisplayerCodes.SUB_KIND_10,SEQ= 1}));
            model.Link11 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam {KIND=DisplayerCodes.GBN_LINK, SUB_KIND=DisplayerCodes.SUB_KIND_10,SEQ= 2}));

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
            model.Web10 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam {KIND=DisplayerCodes.PDT_DETAIL_WEB, SUB_KIND=DisplayerCodes.SUB_KIND_10,SEQ=  1}));
            model.Web11 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam {KIND=DisplayerCodes.PDT_DETAIL_WEB, SUB_KIND=DisplayerCodes.SUB_KIND_10,SEQ=  2}));
            model.Web12 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam {KIND=DisplayerCodes.PDT_DETAIL_WEB, SUB_KIND=DisplayerCodes.SUB_KIND_10,SEQ=  3}));

            //모바일
            model.Mobile10 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam {KIND=DisplayerCodes.PDT_DETAIL_MOBILE, SUB_KIND=DisplayerCodes.SUB_KIND_10,SEQ=  1}));
            model.Mobile11 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam {KIND=DisplayerCodes.PDT_DETAIL_MOBILE, SUB_KIND=DisplayerCodes.SUB_KIND_10,SEQ=  2}));
            model.Mobile12 = GetOneDisplayResult(_Service.GetDisplayerList(new DisplayerParam {KIND=DisplayerCodes.PDT_DETAIL_MOBILE, SUB_KIND=DisplayerCodes.SUB_KIND_10,SEQ=  3}));

            return View(model);
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




        /**
         * 팝업 관리 전시
         */

        //팝업조회
        [CustomAuthorize]
        public ActionResult Popup(PopupSearchParam parma)
        {
            AdminDisplayPopupListViewModel model = new AdminDisplayPopupListViewModel();
            model.PopupList = PopupHelper.GetDataForDisplay(_Service.PopupSel(parma).Item1);
            model.Total = _Service.PopupSel(parma).Item2;
            model.SearchParam = parma;
            return View(model);
        }

        //팝업추가
        [CustomAuthorize]
        public ActionResult PopupAdd(PopupSearchParam parma)
        {
            AdminDisplayPopupSaveViewModel model = new AdminDisplayPopupSaveViewModel();
            model.SearchParam = parma;
            TempData["SearchParam"] = parma;
            return View(model);
        }

        [HttpPost]
        [CustomAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult PopupAdd(AdminDisplayPopupSaveViewModel model)
        {

            model.SearchParam = TempData["SearchParam"] as PopupSearchParam;
            TempData["SearchParam"] = model.SearchParam;

            if (ModelState.IsValid)
            {
                //

                PopupParam p = transViewModelToPopupParam(model);


                var tp = _Service.PopupAdd(p);


                return RedirectToAction("Popup", "AdminDisplay");
            }

            ModelState.AddModelError("", "필수항목들(*)을 입력해주세요");
            return View(model);
        }

        //팝업 전시 수정
        [HttpPost]
        [CustomAuthorize]
        public ActionResult PopupUpdateDisplay(PopupParam param)
        {
            var tp = _Service.PopupUpdateDisplay(param);
            
            TempData["ResultNum"] = tp.Item1;
            TempData["ResultMessage"] = tp.Item2;

            return Redirect(Request.UrlReferrer.ToString());
        }


        //팝업수정
        [CustomAuthorize]
        public ActionResult PopupUpdate(int? id, PopupSearchParam parma)
        {
            AdminDisplayPopupSaveViewModel model = new AdminDisplayPopupSaveViewModel();

            var tp = _Service.PopupInfo(id);

            model.IDX = tp.Item1.IDX.ToString();
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

            TempData["SearchParam"] = parma;
            model.SearchParam = parma;

            return View(model);
        }


        [HttpPost]
        [CustomAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult PopupUpdate(AdminDisplayPopupSaveViewModel model)
        {

            model.SearchParam = TempData["SearchParam"] as PopupSearchParam;
           
            TempData["SearchParam"] = model.SearchParam;

            Debug.WriteLine("idx: " + model.IDX);
            Debug.WriteLine("TITLE: " + model.TITLE);

            if (ModelState.IsValid)
            {

                PopupParam p = transViewModelToPopupParam(model);

             

                var tp = _Service.PopupUpdate(p);

                Debug.WriteLine(tp.Item1);
                Debug.WriteLine(tp.Item2);

               // model.SearchParam.IDX = null;
                return RedirectToAction("Popup", "AdminDisplay", model.SearchParam);
            }

            ModelState.AddModelError("", "필수항목들(*)을 입력해주세요");
            return View(model);
         }


        //팝업삭제
        [HttpPost]
        [CustomAuthorize]
        public ActionResult PopupRemove(PopupParam param)
        {
            var tp = _Service.PopupRemove(param);

            TempData["ResultNum"] = tp.Item1;
            TempData["ResultMessage"] = tp.Item2;

            return Redirect(Request.UrlReferrer.ToString());
        }

        private PopupParam transViewModelToPopupParam(AdminDisplayPopupSaveViewModel model)
        {
            PopupParam p = new PopupParam();

            p.IDX = Convert.ToInt32( model.IDX );
            p.MEDIA_GBN = model.MEDIA_GBN;
            p.TITLE = model.TITLE;

            p.IS_DISPLAY = model.IS_DISPLAY;
            p.DISPLAY_START = model.DISPLAY_START;
            p.DISPLAY_END = model.DISPLAY_END;

            p.POS_TOP = model.POS_TOP;
            p.POS_LEFT = model.POS_LEFT;
            p.SIZE_WIDTH = model.SIZE_WIDTH;
            p.SIZE_HEIGHT = model.SIZE_HEIGHT;

            p.WEB_IMG_NAME = model.WEB_IMG_NAME;
            p.WEB_LINK = model.WEB_LINK;
            p.WEB_TARGET = model.WEB_TARGET;

            p.MOBILE_IMG_NAME = model.MOBILE_IMG_NAME;
            p.MOBILE_LINK = model.MOBILE_LINK;

            if (model.MOBILE_IMG_FILE != null)
            {
                ImageUpload imageUpload = new ImageUpload { UploadPath = _img_path_display, addMobileImage = false };
                ImageResult imageResult = imageUpload.RenameUploadFile(model.MOBILE_IMG_FILE);

                if (imageResult.Success)
                {
                    p.MOBILE_IMG_NAME = imageResult.ImageName;
                }
            }

            if (model.WEB_IMG_FILE != null)
            {
                ImageUpload imageUpload = new ImageUpload { UploadPath = _img_path_display, addMobileImage = false };
                ImageResult imageResult = imageUpload.RenameUploadFile(model.WEB_IMG_FILE);

                if (imageResult.Success)
                {
                    p.WEB_IMG_NAME = imageResult.ImageName;
                }
            }


            return p;
        }
    }
}