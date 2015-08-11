using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Domain.Service.Review;
using System.Diagnostics;
using AboutMe.Web.Front.Models;
using AboutMe.Web.Front.Common.Filters;
using AboutMe.Web.Front.Common;
using AboutMe.Domain.Entity.Review;
using AboutMe.Common.Helper;
using AboutMe.Common.Data;

using AboutMe.Domain.Service.Product;
using AboutMe.Domain.Entity.Product;

namespace AboutMe.Web.Front.Controllers
{
    [RoutePrefix("MyPage/MyReviewExp")]
    [Route("{action=Index}")]
    public class MyReviewExpController : BaseFrontController
    {
        private IReviewService _service;
        private IProductService _service_p;

        public MyReviewExpController(IReviewService s, IProductService p)
        {
            this._service = s;
            _service_p = p;
        }

        // GET: MyExpReview
        [CustomAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        [CustomAuthorize]
        public ActionResult Write(int? EXP_MASTER_IDX, string P_CODE)
        {
            MyReviewExpWriteViewModel model = new MyReviewExpWriteViewModel();
            model.EXP_MASTER_IDX = EXP_MASTER_IDX;
            model.P_CODE = P_CODE;
            model.ProductInfo = _service.GetProductInfo(P_CODE);
            return View(model);
        }


        [HttpPost]
        [CustomAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult Write(MyReviewExpWriteViewModel model)
        {
            
            return View(model);
        }
    }
}