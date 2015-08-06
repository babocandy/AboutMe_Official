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
    public class ShoppingController : BaseMobileController
    {
        #region 인터페이스
        private IProductService _ProductService;
        public ShoppingController(IProductService _productService)
        {
            this._ProductService = _productService;
        }
        #endregion

        #region 상품리스트[모바일]
        public ActionResult Index(Product_front_search_entity product_front_search_entity)
        {

            #region 초기화작업
            if (string.IsNullOrEmpty(product_front_search_entity.P_CATE_CODE_3DEPTH))  //3depth 
            {
                product_front_search_entity.P_CATE_CODE_3DEPTH = "";
            }
            if (string.IsNullOrEmpty(product_front_search_entity.P_CATE_CODE))
            {
                product_front_search_entity.P_CATE_CODE = "101100100"; //뷰티 default
            }
            if (string.IsNullOrEmpty(product_front_search_entity.SORT_GBN))
            {
                product_front_search_entity.SORT_GBN = "NEW";
            }
            product_front_search_entity.P_OUTLET_YN = "N"; //아울렛 상품 제외
            int TotalRecord = 0;
            #endregion

            TotalRecord = _ProductService.GetProductCnt(product_front_search_entity);
            this.ViewBag.TotalRecord = TotalRecord;                                             //총카운트
            this.ViewBag.PAGE = product_front_search_entity.PAGE;                               //페이지
            this.ViewBag.SORT_GBN = product_front_search_entity.SORT_GBN;                       //정렬순서
            this.ViewBag.P_CATE_CODE = product_front_search_entity.P_CATE_CODE;                 //P_CATE_CODE
            this.ViewBag.P_CATE_CODE_3DEPTH = product_front_search_entity.P_CATE_CODE_3DEPTH;   //P_CATE_CODE_3DEPTH
            this.ViewBag.DEPTH_NAME3 = product_front_search_entity.DEPTH_NAME3;                 //뷰티3DEPTH NAME

            ViewBag.PRODUCT_PATH = AboutMe.Common.Helper.Config.GetConfigValue("ProductPhotoPath"); //이미지디렉토리경로

            ViewData["CateCodeP"] = _ProductService.GetCategoryDeptList("PRODUCT_TYPE", product_front_search_entity.P_CATE_CODE.Substring(0, 3), "").ToList(); //유형별
            
            if (product_front_search_entity.P_CATE_CODE.Substring(0, 3) == "101") //뷰티인경우 3depth도 보여준다
            {
                ViewData["3DEPTH"] = _ProductService.GetCategoryDeptList("PRODUCT_TYPE", product_front_search_entity.P_CATE_CODE.Substring(0, 3), product_front_search_entity.P_CATE_CODE.Substring(3, 3)).ToList(); //뷰티 3depth
            }

            return View(_ProductService.GetProductMobileList(product_front_search_entity).ToList());
            //return View();
        }
        #endregion

        #region 상품상세보기[모바일]
        public ActionResult Detail(string pcode, Product_front_search_entity product_front_search_entity)
        {
            //상품코드
            this.ViewBag.P_CODE = pcode;

            this.ViewBag.PAGE = product_front_search_entity.PAGE;                               //페이지
            this.ViewBag.SORT_GBN = product_front_search_entity.SORT_GBN;                       //정렬순서
            this.ViewBag.P_CATE_CODE = product_front_search_entity.P_CATE_CODE;                 //P_CATE_CODE
            this.ViewBag.P_CATE_CODE_3DEPTH = product_front_search_entity.P_CATE_CODE_3DEPTH;   //P_CATE_CODE_3DEPTH
            this.ViewBag.DEPTH_NAME3 = product_front_search_entity.DEPTH_NAME3;                 //뷰티3DEPTH NAME


            SP_PRODUCT_MOBILE_DETAIL_VIEW_Result productView = _ProductService.ViewProductMobile(pcode);
            ViewBag.PRODUCT_PATH = AboutMe.Common.Helper.Config.GetConfigValue("ProductPhotoPath"); //이미지디렉토리경로

            this.ViewBag.IS_LOGIN = MemberInfo.IsMemberLogin();  //로그인 여부  T/F
            this.ViewBag.M_ID = MemberInfo.GetMemberId();  //로그인 계정

            return View(productView);
        }
        #endregion

        #region 상품 메인
        public ActionResult Main(Product_front_search_entity product_front_search_entity)
        {
            ViewBag.PRODUCT_PATH = AboutMe.Common.Helper.Config.GetConfigValue("ProductPhotoPath"); //이미지디렉토리경로

            ViewData["CateCodeB"] = _ProductService.GetProductMainList("101").ToList(); //뷰티 지난달 판매량순 top 10
            ViewData["CateCodeH"] = _ProductService.GetProductMainList("102").ToList(); //헬스 지난달 판매량순 top 10
            ViewData["CateCodeS"] = _ProductService.GetProductMainList("103").ToList(); //셀렉 지난달 판매량순 top 10

            return View();
        }
        #endregion

    }
}