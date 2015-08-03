﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using System.Diagnostics;
using AboutMe.Web.Admin.Models;
using AboutMe.Web.Admin.Common.Filters;
using AboutMe.Web.Admin.Common;
using AboutMe.Common.Helper;
using AboutMe.Common.Data;

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
        [CustomAuthorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}