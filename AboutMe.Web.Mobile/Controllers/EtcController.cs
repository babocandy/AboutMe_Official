using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AboutMe.Web.Mobile.Controllers
{
    public class EtcController : Controller
    {
        // GET: Etc
        public ActionResult Index()
        {
            return View();
        }

        //푸터 이용약관
        public ActionResult Agreement()
        {
            return View();
        }

        //푸터 개인정보취급방침
        public ActionResult Privacy()
        {
            return View();
        }

    
    }
}