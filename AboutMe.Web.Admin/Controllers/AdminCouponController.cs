using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Common.Helper;
using AboutMe.Domain.Service.AdminPromotion;
using AboutMe.Domain.Entity.AdminPromotion;


namespace AboutMe.Web.Admin.Controllers
{
    public class AdminCouponController : BaseAdminController
    {
        // GET: AdminCoupon
        public ActionResult Index()
        {
            return View();
        }
    }
}