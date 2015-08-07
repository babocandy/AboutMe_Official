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
using AboutMe.Domain.Service.BizPromotion;

namespace AboutMe.Web.Front.Controllers
{
    public class ChildActionPromotionController : BaseFrontController
    {
        private IBizPromotion _BizPromotionService;
        public ChildActionPromotionController(IBizPromotion _bizPromotionService)
        {
            this._BizPromotionService = _bizPromotionService;
        }

        #region 상품 상세의 타임세일 배너
        [ChildActionOnly]
        public ActionResult ProductDetailTimeSale(string P_CODE)
        {
            this.ViewBag.P_CODE = P_CODE; //상품코드
            return View();
        }
        #endregion

        #region 상품 상세의 세트상품 배너
        [ChildActionOnly]
        public ActionResult ProductDetailSetList(string P_CODE)
        {
            this.ViewBag.P_CODE = P_CODE; //상품코드
            return View();
        }
        #endregion

        #region 상품 상세의 임직원가, 쿠폰, 포인트
        [ChildActionOnly]
        public ActionResult ProductDetailSalePrice(string UsableDeviceGbn, string P_CODE, int ResultPrice)
        {
            this.ViewBag.P_CODE = P_CODE; //상품코드
            return View();
        }
        #endregion


    }
}