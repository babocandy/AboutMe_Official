using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AboutMe.Web.Front.Controllers
{
    public class ChildActionDisplayerController : BaseFrontController
    {

        public ChildActionDisplayerController()
        {

        }

        [ChildActionOnly]
        public ActionResult BannerInProductDetail()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult MainBannerInWebMain()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult MiddleBannerInWebMain()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult DisplayProductInWebMain()
        {
            return View();
        }
    }
}