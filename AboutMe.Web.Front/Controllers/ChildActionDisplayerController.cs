using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Domain.Service.Display;
using AboutMe.Web.Front.Models;
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

        [ChildActionOnly]
        public ActionResult MainBannerInWebMain()
        {
            
            BaseDisplayerViewModel model = new BaseDisplayerViewModel();
            model.List = _service.GetListDisplay(DisplayerCode.WEB_MAIN_BANNER);
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult MiddleBannerInWebMain()
        {
            BaseDisplayerViewModel model = new BaseDisplayerViewModel();
            model.List = _service.GetListDisplay(DisplayerCode.WEB_MAIN_MIDDLE_BANNER);
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult DisplayProductBeautyInWebMain()
        {
            BaseDisplayerViewModel model = new BaseDisplayerViewModel();
            model.List = _service.GetListDisplay(DisplayerCode.WEB_MAIN_PRODUCT_DISPLAY, DisplayerCode.SUB_KIND_10);

            List<SP_PRODUCT_DETAIL_VIEW_Result> plist = new List<SP_PRODUCT_DETAIL_VIEW_Result>();

            foreach (var item in model.List)
            {
                plist.Add(_service_pdt.ViewProduct(item.P_CODE));
            }

            model.PdtList = plist;

            return View(model);
        }

        [ChildActionOnly]
        public ActionResult DisplayProductHealthInWebMain()
        {
            BaseDisplayerViewModel model = new BaseDisplayerViewModel();
            model.List = _service.GetListDisplay(DisplayerCode.WEB_MAIN_PRODUCT_DISPLAY, DisplayerCode.SUB_KIND_10);

            List<SP_PRODUCT_DETAIL_VIEW_Result> plist = new List<SP_PRODUCT_DETAIL_VIEW_Result>();
            foreach (var item in model.List)
            {
                plist.Add(_service_pdt.ViewProduct(item.P_CODE));
            }

            model.PdtList = plist;

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
        public ActionResult GbnLink()
        {
            BaseDisplayerViewModel model = new BaseDisplayerViewModel();
            model.List = _service.GetListDisplay(DisplayerCode.GBN_LINK);
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult CartBanner()
        {
            BaseDisplayerViewModel model = new BaseDisplayerViewModel();
            model.List = _service.GetListDisplay(DisplayerCode.CART_WEB);
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
                plist.Add(_service_pdt.ViewProduct(item.P_CODE));
            }

            model.PdtList = plist;

            return View(model);
        }

        [ChildActionOnly]
        private SP_DISPLAY_SEL_Result GetOneDisplayResult(List<SP_DISPLAY_SEL_Result> list)
        {
            return list.Count > 0 ? list[0] : new SP_DISPLAY_SEL_Result();
        }
    }
}