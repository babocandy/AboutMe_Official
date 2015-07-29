using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Diagnostics;
using AboutMe.Web.Admin.Models;
using AboutMe.Web.Admin.Common.Filters;
using AboutMe.Web.Admin.Common;

namespace AboutMe.Web.Admin.Controllers
{
    public class AdminDisplayController : Controller
    {
        [CustomAuthorize]
        public ActionResult Index()
        {
            AdminDisplayWebMainViewModel model = new AdminDisplayWebMainViewModel();
            return View(model);
        }

        public ActionResult SaveWebMainBanner()
        {
            return View();
        }

        public ActionResult RemoveWebMainBanner()
        {
            return View();
        }

        public ActionResult MobileMain()
        {
            return View();
        }

        public ActionResult CartBanner()
        {
            return View();
        }

        public ActionResult GBNBanner()
        {
            return View();
        }

        public ActionResult ProductDetailBanner()
        {
            return View();
        }

        public ActionResult Popup()
        {
            return View();
        }
    }
}