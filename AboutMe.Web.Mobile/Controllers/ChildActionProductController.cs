﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using AboutMe.Common.Helper;
using AboutMe.Domain.Service.AdminProduct;
using AboutMe.Domain.Entity.AdminProduct;
using System.Data.Entity.Core.Objects;
using AboutMe.Common.Data;
using Newtonsoft.Json;
using AboutMe.Web.Mobile.Common;
using AboutMe.Web.Mobile.Common.Filters;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using AboutMe.Domain.Service.Product;
using AboutMe.Domain.Entity.Product;


namespace AboutMe.Web.Mobile.Controllers
{
    public class ChildActionProductController : BaseMobileController
    {

        private IProductService _ProductService;
        public ChildActionProductController(IProductService _productService)
        {
            this._ProductService = _productService;
        }

        #region HEADER의 카테고리 리스트
        [ChildActionOnly]
        public ActionResult HeaderCategoryMenu()
        {
            ViewData["category1"] = _ProductService.GetCategoryDeptList("PRODUCT_TYPE", "101", "").ToList(); //뷰티
            ViewData["category2"] = _ProductService.GetCategoryDeptList("PRODUCT_TYPE", "102", "").ToList(); //헬스
            ViewData["category3"] = _ProductService.GetCategoryDeptList("PRODUCT_TYPE", "103", "").ToList(); //수입브랜드(셀렉샵)
            return View();
        }
        #endregion


    }
}