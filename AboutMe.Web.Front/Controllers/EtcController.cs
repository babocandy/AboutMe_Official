using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AboutMe.Web.Front.Controllers
{
    public class EtcController : Controller
    {
        // GET: Etc
        //약관동의 팝업
        public ActionResult Agreement()
        {
            return View();
        }

        //개인정보취급방침 팝업
        public ActionResult Privacy()
        {
            return View();
        }
    
    }//class
}//namespace