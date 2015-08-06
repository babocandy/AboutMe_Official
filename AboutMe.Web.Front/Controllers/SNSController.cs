using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AboutMe.Web.Front.Controllers
{
    public class SNSController : BaseFrontController
    {
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult SnsInProductDetail(string P_CODE)
        {
            return PartialView();
        }
    }
}