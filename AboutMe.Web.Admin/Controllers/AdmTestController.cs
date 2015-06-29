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
        //[CustomAuthorize(Roles = "S", Roles2 = "S,A,B")]
        //[CustomAuthorizeOther(Roles = UserType.User)]
     
       
        [CustomAuthorize()]
        public ActionResult Index()
        {
            return View();
        }
    }
}