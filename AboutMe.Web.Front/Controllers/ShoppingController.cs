using System;
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
using AboutMe.Web.Front.Common;
using AboutMe.Web.Front.Common.Filters;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using AboutMe.Domain.Service.Product;
using AboutMe.Domain.Entity.Product;

namespace AboutMe.Web.Front.Controllers
{
    public class ShoppingController : BaseFrontController
    {
        // GET: AdminProduct
        private IProductService _ProductService;
        public ShoppingController(IProductService _productService)
        {
            this._ProductService = _productService;
        }


        // GET: Shopping
        #region 상품리스트
        public ActionResult Index()
        {
            int TotalRecord = 0;
            //TotalRecord = _ProductService.GetProductCnt();

            this.ViewBag.TotalRecord = TotalRecord;
            //this.ViewBag.Page = productSearch_Entity.Page;

            ViewBag.PRODUCT_PATH = AboutMe.Common.Helper.Config.GetConfigValue("ProductPhotoPath"); //이미지디렉토리경로

            return View(_ProductService.GetProductList().ToList());
            //return View();
        }
        #endregion

        #region 상품상세보기
        public ActionResult Detail(string pcode)
        {

            //상품코드
            this.ViewBag.P_CODE = pcode;

            SP_PRODUCT_DETAIL_VIEW_Result productView = _ProductService.ViewProduct(pcode);
            ViewBag.PRODUCT_PATH = AboutMe.Common.Helper.Config.GetConfigValue("ProductPhotoPath"); //이미지디렉토리경로
            return View(productView);
            //return View();
        }
        #endregion
    }
}