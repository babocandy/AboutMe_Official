using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AboutMe.Web.Front.Controllers.Mypage
{
    [RoutePrefix("MyPage/MyPoint")]
    [Route("{action=Index}")]
    public class MyPointController : BaseFrontController
    {
        // GET: MyPoint
        public ActionResult Index()
        {
            return View();
        }
    }
}