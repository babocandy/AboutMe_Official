using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AboutMe.Web.Front.Controllers
{
    public class CustomErrorController : Controller
    {
        // GET: CustomError
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Err500()
        {
            return View();
        }
    }
}