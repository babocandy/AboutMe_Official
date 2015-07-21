using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AboutMe.Web.Admin.Controllers
{
    public class AdminReviewController : BaseAdminController
    {
        // GET: AdminReviewController
        public ActionResult Index()
        {
            return View();
        }
    }
}