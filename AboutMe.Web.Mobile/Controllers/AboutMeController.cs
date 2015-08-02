using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AboutMe.Web.Mobile.Controllers
{
    public class AboutMeController : Controller
    {
        // GET: AboutMe
        public ActionResult Index()
        {
            return View();
        }

        //회사소개
        public ActionResult Brand()
        {
            return View();
        }

    }
}