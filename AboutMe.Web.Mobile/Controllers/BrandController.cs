using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

using AboutMe.Domain.Service.Magazine;
using AboutMe.Domain.Entity.Magazine;
using AboutMe.Domain.Service.Shopinfo;
using AboutMe.Domain.Entity.Shopinfo;
using AboutMe.Web.Mobile.Common.Filters;

namespace AboutMe.Web.Mobile.Controllers
{
    public class BrandController : BaseMobileController
    {
        private IMagazineService _magazineservice;
        private IShopinfoService _shopinfoservice;
        private string _magazineUploadPath = AboutMe.Common.Helper.Config.GetConfigValue("MagazineUploadPath");

        public BrandController(IMagazineService _magazineservice, IShopinfoService _shopinfoservice)
        {
            this._magazineservice = _magazineservice;
            this._shopinfoservice = _shopinfoservice;
        }
        // GET: Brand 소개
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Magazine()
        {
            List<SP_MAGAZINE_LIST_Result> list = new List<SP_MAGAZINE_LIST_Result>();
            list = _magazineservice.MagazineList();
            return View(list);
        }


        public ActionResult Shopinfo()
        {
            List<SP_SHOPINFO_LIST_Result> list = new List<SP_SHOPINFO_LIST_Result>();
            list = _shopinfoservice.ShopinfoList();
            return View(list);
        }
    }
}