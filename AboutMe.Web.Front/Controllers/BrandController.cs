using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

using AboutMe.Domain.Service.Magazine;
using AboutMe.Domain.Entity.Magazine;
using AboutMe.Web.Front.Common.Filters;

namespace AboutMe.Web.Front.Controllers
{
    public class BrandController : BaseFrontController
    {
        private IMagazineService _magazineservice;
        private string _magazineUploadPath = AboutMe.Common.Helper.Config.GetConfigValue("MagazineUploadPath");

        public BrandController(IMagazineService _magazineservice)
        {
            this._magazineservice = _magazineservice;
        }
        // GET: Brand 소개
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Magazine()
        {
            //_magazineservice.
            return View();
        }
    }
}