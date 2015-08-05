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
using AboutMe.Web.Mobile.Common;
using AboutMe.Web.Mobile.Common.Filters;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using AboutMe.Domain.Service.Product;
using AboutMe.Domain.Entity.Product;

namespace AboutMe.Web.Mobile.Controllers
{
    public class SearchController : BaseMobileController
    {
        private IProductService _ProductService;
        public SearchController(IProductService _productService)
        {
            this._ProductService = _productService;
        }

        #region 상품검색
        public ActionResult Index(Product_front_search_entity product_front_search_entity)
        {
            #region 초기화작업
            int TotalRecord = 0;
            #endregion

            TotalRecord = _ProductService.GetProductSearchCnt(product_front_search_entity);
            this.ViewBag.TotalRecord = TotalRecord;                                             //총카운트
            this.ViewBag.PAGE = product_front_search_entity.PAGE;                               //페이지
            this.ViewBag.SORT_GBN = product_front_search_entity.SORT_GBN;                       //정렬순서
            this.ViewBag.SEARCH_KEYWORD = product_front_search_entity.SEARCH_KEYWORD;           //검색어

            ViewBag.PRODUCT_PATH = AboutMe.Common.Helper.Config.GetConfigValue("ProductPhotoPath"); //이미지디렉토리경로

            return View(_ProductService.GetProductSearchList(product_front_search_entity).ToList());
        }
        #endregion
    }
}