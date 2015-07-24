using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Common.Helper;
using AboutMe.Web.Mobile.Common;
using AboutMe.Domain.Entity.Common;


namespace AboutMe.Web.Mobile.Controllers
{
    public class BaseMobileController : Controller
    {
        //// GET: BaseMobile
        //public ActionResult Index()
        //{
        //    return View();
        //}

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {


            base.Initialize(requestContext);

            // MyInitialzie()


        }
    }


}