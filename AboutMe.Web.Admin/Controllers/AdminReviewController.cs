using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Domain.Service.Review;

namespace AboutMe.Web.Admin.Controllers
{
    public class AdminReviewController : BaseAdminController
    {

        private IReviewService _ReviewService;

        public AdminReviewController(IReviewService _adminPointService)
        {
            this._ReviewService = _adminPointService;
        }

        // GET: AdminReviewController
        public ActionResult Index()
        {
            return View();
        }
    }
}