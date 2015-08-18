using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Domain.Service.Display;
using AboutMe.Common.Data;

using AboutMe.Domain.Service.Product;
using AboutMe.Domain.Entity.Product;
using AboutMe.Domain.Entity.Display;

namespace AboutMe.Web.Front.Controllers
{
    public class ChildActionDisplayerController : BaseFrontController
    {
        private IDisplayService _service;
        private IProductService _service_pdt;

        public ChildActionDisplayerController(IDisplayService s, IProductService p)
        {
            _service = s;
            _service_pdt = p;
        }

        [ChildActionOnly]
        public ActionResult BannerInProductDetail()
        {
            BaseDisplayerViewModel model = new BaseDisplayerViewModel();
            model.List = _service.GetListDisplay(DisplayerCode.PDT_DETAIL_WEB);
            return View(model);
        }

        /**
         * 메인배너 
         */
        [ChildActionOnly]
        public ActionResult MainBannerInMain()
        {
            
            BaseDisplayerViewModel model = new BaseDisplayerViewModel();
            model.List = _service.GetListDisplay(DisplayerCode.WEB_MAIN_BANNER);
            return View(model);
        }

        /*
         * 중간배너
         */
        [ChildActionOnly]
        public ActionResult MiddleBannerInMain()
        {
            BaseDisplayerViewModel model = new BaseDisplayerViewModel();
            model.List = _service.GetListDisplay(DisplayerCode.WEB_MAIN_MIDDLE_BANNER);
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult DisplayProductBeautyInMain()
        {
            ProductDisplayMainViewModel model = new ProductDisplayMainViewModel();
            model.Header = GetOneDisplayResult(_service.GetListDisplay(DisplayerCode.WEB_MAIN_PRODUCT_DISPLAY, DisplayerCode.SUB_KIND_10, 1));

            var p = _service.GetListDisplay(DisplayerCode.WEB_MAIN_PRODUCT_DISPLAY, DisplayerCode.SUB_KIND_10, 2);
            if (p.Count > 0)
            {
                model.Product_1 = _service_pdt.ViewProduct(p[0].P_CODE);
            }

            p = _service.GetListDisplay(DisplayerCode.WEB_MAIN_PRODUCT_DISPLAY, DisplayerCode.SUB_KIND_10, 3);
            if (p.Count > 0)
            {
                model.Product_2 = _service_pdt.ViewProduct(p[0].P_CODE);
            }

            p = _service.GetListDisplay(DisplayerCode.WEB_MAIN_PRODUCT_DISPLAY, DisplayerCode.SUB_KIND_10, 4);
            if (p.Count > 0)
            {
                model.Product_3 = _service_pdt.ViewProduct(p[0].P_CODE);
            }
            
            
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult DisplayProductHealthInMain()
        {
            ProductDisplayMainViewModel model = new ProductDisplayMainViewModel();
            model.Header = GetOneDisplayResult(_service.GetListDisplay(DisplayerCode.WEB_MAIN_PRODUCT_DISPLAY, DisplayerCode.SUB_KIND_20, 1));

            var p = _service.GetListDisplay(DisplayerCode.WEB_MAIN_PRODUCT_DISPLAY, DisplayerCode.SUB_KIND_20, 2);
            if (p.Count > 0)
            {
                model.Product_1 = _service_pdt.ViewProduct(p[0].P_CODE);
            }

            p = _service.GetListDisplay(DisplayerCode.WEB_MAIN_PRODUCT_DISPLAY, DisplayerCode.SUB_KIND_20, 3);
            if (p.Count > 0)
            {
                model.Product_2 = _service_pdt.ViewProduct(p[0].P_CODE);
            }

            p = _service.GetListDisplay(DisplayerCode.WEB_MAIN_PRODUCT_DISPLAY, DisplayerCode.SUB_KIND_20, 4);
            if (p.Count > 0)
            {
                model.Product_3 = _service_pdt.ViewProduct(p[0].P_CODE);
            }


            return View(model);
        }

        [ChildActionOnly]
        public ActionResult GbnBanner()
        {
            BaseDisplayerViewModel model = new BaseDisplayerViewModel();
            model.List = _service.GetListDisplay(DisplayerCode.GBN_BANNER);
            return View(model);
        }


        [ChildActionOnly]
        public ActionResult GbnLink1()
        {
            BaseDisplayerViewModel model = new BaseDisplayerViewModel();
            model.One = GetOneDisplayResult( _service.GetListDisplay(DisplayerCode.GBN_LINK, DisplayerCode.SUB_KIND_10, 1) );
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult GbnLink2()
        {
            BaseDisplayerViewModel model = new BaseDisplayerViewModel();
            model.One = GetOneDisplayResult(_service.GetListDisplay(DisplayerCode.GBN_LINK, DisplayerCode.SUB_KIND_10, 2));
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult CartBanner()
        {
            BaseDisplayerViewModel model = new BaseDisplayerViewModel();
            model.One = GetOneDisplayResult(_service.GetListDisplay(DisplayerCode.CART_WEB));
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult CartRelatePorducts()
        {
            BaseDisplayerViewModel model = new BaseDisplayerViewModel();
            model.List = _service.GetListDisplay(DisplayerCode.CART_PRODUCT_DISPLAY);

            List<SP_PRODUCT_DETAIL_VIEW_Result> plist = new List<SP_PRODUCT_DETAIL_VIEW_Result>();
            foreach (var item in model.List)
            {
                if (_service_pdt.ViewProduct(item.P_CODE) != null)
                {
                    plist.Add(_service_pdt.ViewProduct(item.P_CODE));
                }
                
            }

            model.PdtList = plist;

            return View(model);
        }


        [ChildActionOnly]
        private SP_DISPLAY_SEL_Result GetOneDisplayResult(List<SP_DISPLAY_SEL_Result> list)
        {
            return list.Count > 0 ? list[0] : new SP_DISPLAY_SEL_Result();
        }


        [ChildActionOnly]
        public ActionResult PopupMgr()
        {
            PopupMgrViewModel model = new PopupMgrViewModel();
            model.List = _service.GetListPopupWeb();
            return PartialView(model);
        }

        public ActionResult PopupWindow(int? idx)
        {
            PopupWindowViewModel model = new PopupWindowViewModel();
            model.Detail = _service.GePopupDetail(idx);
            return View(model);
        }
    }
}