using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AboutMe.Web.Front.Controllers
{
    [RouteArea("MyPage")]
    [RoutePrefix("MyReview")]
    [Route("{action=Index}")]
    public class MyReviewController : Controller
    {
        // GET: MyReview
        public ActionResult Index()
        {
            return View();
        }
    }
}