using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AboutMe.Web.Front.Controllers
{
    [RoutePrefix("Review")]
    [Route("{action=Product}")]
    public class ReviewController : BaseFrontController
    {
        /*// GET: Review
        public ActionResult Index()
        {
            return View();
        }*/

        public ActionResult Product()
        {
            return View();
        }

        public ActionResult Experience()
        {
            return View();
        }
    }
}