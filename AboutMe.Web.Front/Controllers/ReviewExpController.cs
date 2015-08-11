using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

using AboutMe.Web.Front.Models;
using AboutMe.Domain.Service.Review;
using AboutMe.Domain.Service.Product;

using AboutMe.Common.Data;
using AboutMe.Common.Helper;
using AboutMe.Domain.Entity.Review;


namespace AboutMe.Web.Front.Controllers
{
    [RoutePrefix("Review")]
    [Route("{action=Experience}")]
    public class ReviewExpController : BaseFrontController
    {

        private IReviewService _service;
        private IProductService _service_p;

        public ReviewExpController(IReviewService r, IProductService p)
        {
            this._service = r;
            this._service_p = p;
        }

        /**
         * 체험단 메인
         */
        public ActionResult Experience()
        {
            return View();
        }

    }
}