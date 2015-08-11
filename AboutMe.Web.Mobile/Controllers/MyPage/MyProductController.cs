using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Common.Helper;
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

namespace AboutMe.Web.Mobile.Controllers.MyPage
{

    [RoutePrefix("MyPage/MyProduct")]
    [Route("{action=Index}")]
    public class MyProductController : BaseMobileController
    {

        #region 인터페이스
        private IProductService _ProductService;
        public MyProductController(IProductService _productService)
        {
            this._ProductService = _productService;
        }
        #endregion


        [CustomAuthorize]
        public ActionResult Index(Product_front_search_entity product_front_search_entity)
        {

            string cate_code = MemberInfo.GetMemberSkinTroubleCD(); //사용자 피부타입 코드

            SP_CATEGORY_NAME_INFO_VIEW_Result productView = _ProductService.GetCategoryNameInfo(cate_code);

            int TotalRecord = 0;
            TotalRecord = _ProductService.GetProductMypageMobileSkinTypeCnt(cate_code);
            this.ViewBag.TotalRecord = TotalRecord;                                             //총카운트
            this.ViewBag.PAGE = product_front_search_entity.PAGE;                               //페이지
            this.ViewBag.PAGESIZE = product_front_search_entity.PAGESIZE;                       //페이지사이즈
            this.ViewBag.SORT_GBN = product_front_search_entity.SORT_GBN;                       //정렬순서
            this.ViewBag.DEPTH1_NAME = productView.DEPTH1_NAME;                                 //카테고리 depth1 name
            this.ViewBag.DEPTH2_NAME = productView.DEPTH2_NAME;                                 //카테고리 depth2 name
            this.ViewBag.CATE_CODE = cate_code;                                                 //사용자 피부타입 코드
                       
            ViewBag.PRODUCT_PATH = AboutMe.Common.Helper.Config.GetConfigValue("ProductPhotoPath"); //상품이미지디렉토리경로
            return View(_ProductService.GetProductMypageMobileSkinTypeList(cate_code, product_front_search_entity).ToList());
        }
    }
}