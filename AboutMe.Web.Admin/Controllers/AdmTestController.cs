using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Web.Admin.Common;
using AboutMe.Web.Admin.Common.Filters;

namespace AboutMe.Web.Admin.Controllers
{
    public class AdmTestController : BaseAdminController
    {
        // GET: AdmTest
        //[CustomAuthorize(Roles = "S")]
        //[CustomAuthorizeOther(Roles = UserType.User)]


        [CustomAuthorize(Roles = "S")]
        public ActionResult Index()
        {
            return View();
        }

        public RedirectResult Test001()
        {
            
            return Redirect("/test1.aspx");
        }
    }
}