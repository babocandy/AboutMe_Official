using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AboutMe.Web.Front.Controllers
{
    [RoutePrefix("MyPage/MyReview")]
    [Route("{action=Ready}")]
    public class MyReviewController : BaseFrontController
    {
        // GET: MyReview
        /*
        public ActionResult Index()
        {
            return View();
        }*/

        public ActionResult Ready()
        {
            return View();
        }

        public ActionResult Complete()
        {
            return View();
        }

        public ActionResult Write()
        {
            return View();
        }

        public ActionResult Update()
        {
            return View();
        }
    }
}