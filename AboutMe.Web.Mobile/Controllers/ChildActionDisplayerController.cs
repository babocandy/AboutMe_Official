using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Domain.Service.Display;
using AboutMe.Web.Mobile.Models;
using AboutMe.Common.Data;

using AboutMe.Domain.Service.Product;
using AboutMe.Domain.Entity.Product;

namespace AboutMe.Web.Mobile.Controllers
{
    public class ChildActionDisplayerController : BaseMobileController
    {
        private IDisplayService _service;
        private IProductService _service_pdt;

        public ChildActionDisplayerController(IDisplayService s, IProductService p)
        {
            _service = s;
            _service_pdt = p;
        }



        [ChildActionOnly]
        public ActionResult MainBannerInMain()
        {
            
            BaseDisplayerViewModel model = new BaseDisplayerViewModel();
            model.List = _service.GetListDisplay(DisplayerCode.MOBILE_MAIN_BANNER);
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult TalkBannerInMain()
        {
            BaseDisplayerViewModel model = new BaseDisplayerViewModel();
            model.List = _service.GetListDisplay(DisplayerCode.MOBILE_MAIN_TALK);
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult BestBannerInMain()
        {
            BaseDisplayerViewModel model = new BaseDisplayerViewModel();
            model.List = _service.GetListDisplay(DisplayerCode.MOBILE_MAIN_BEST);
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult CartBanner()
        {
            BaseDisplayerViewModel model = new BaseDisplayerViewModel();
            model.List = _service.GetListDisplay(DisplayerCode.CART_MOBILE);
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
        public ActionResult BannerInProductDetail()
        {
            BaseDisplayerViewModel model = new BaseDisplayerViewModel();
            model.List = _service.GetListDisplay(DisplayerCode.PDT_DETAIL_MOBILE);
            return View(model);
        }

        /*


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
        */
    }
}