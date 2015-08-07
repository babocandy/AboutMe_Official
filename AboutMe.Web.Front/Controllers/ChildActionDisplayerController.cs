using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AboutMe.Web.Front.Controllers
{
    public class ChildActionDisplayerController : BaseFrontController
    {
        [ChildActionOnly]
        public ActionResult BannerInProductDetail()
        {
            return View();
        }
    }
}