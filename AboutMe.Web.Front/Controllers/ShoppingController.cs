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
        
        private IProductService _ProductService;
        public ShoppingController(IProductService _productService)
        {
            this._ProductService = _productService;
        }


        #region 상품리스트
        public ActionResult Index(IEnumerable<string> P_CATE_CODE_3DEPTH, Product_front_search_entity product_front_search_entity)
        {

            #region 초기화작업
            product_front_search_entity.P_CATE_CODE_3DEPTH = Request.Form["P_CATE_CODE_3DEPTH"];
            if (!string.IsNullOrEmpty(product_front_search_entity.P_CATE_CODE_3DEPTH))  //다중검색 3depth 할경우 넣어준다
            {
                product_front_search_entity.P_CATE_CODE_3DEPTH = product_front_search_entity.P_CATE_CODE_3DEPTH;
            }
            if (string.IsNullOrEmpty(product_front_search_entity.P_CATE_CODE))
            {
                product_front_search_entity.P_CATE_CODE = "101100100"; //뷰티 default
            }
            if (string.IsNullOrEmpty(product_front_search_entity.C_CATE_CODE))
            {
                product_front_search_entity.C_CATE_CODE = ""; //피부고민 DEFAULT
            }
            if (string.IsNullOrEmpty(product_front_search_entity.L_CATE_CODE))
            {
                product_front_search_entity.L_CATE_CODE = ""; //라인 default
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
            this.ViewBag.PAGESIZE = product_front_search_entity.PAGESIZE;                       //페이지 사이즈
            this.ViewBag.SORT_GBN = product_front_search_entity.SORT_GBN;                       //정렬순서
            this.ViewBag.P_CATE_CODE = product_front_search_entity.P_CATE_CODE;                 //P_CATE_CODE
            this.ViewBag.C_CATE_CODE = product_front_search_entity.C_CATE_CODE;                 //C_CATE_CODE
            this.ViewBag.L_CATE_CODE = product_front_search_entity.L_CATE_CODE;                 //L_CATE_CODE
            this.ViewBag.P_CATE_CODE_3DEPTH = product_front_search_entity.P_CATE_CODE_3DEPTH;   //P_CATE_CODE_3DEPTH
            this.ViewBag.DEPTH_NAME3 = Request["DEPTH_NAME3"];                                  //뷰티3DEPTH NAME

            ViewBag.PRODUCT_PATH = AboutMe.Common.Helper.Config.GetConfigValue("ProductPhotoPath"); //이미지디렉토리경로

            //this.ViewBag.CATE_CODE = product_front_search_entity.P_CATE_CODE;//선택한 카테고리 코드값

            ViewData["CateCodeP"] = _ProductService.GetCategoryDeptList("PRODUCT_TYPE", product_front_search_entity.P_CATE_CODE.Substring(0, 3), "").ToList(); //유형별
            ViewData["CateCodeC"] = _ProductService.GetCategoryDeptList("SKIN_TYPE", product_front_search_entity.P_CATE_CODE.Substring(0, 3), "").ToList(); //피부타입별
            ViewData["CateCodeL"] = _ProductService.GetCategoryDeptList("LINE_TYPE", product_front_search_entity.P_CATE_CODE.Substring(0, 3), "").ToList(); //라인별

            if (product_front_search_entity.P_CATE_CODE.Substring(0, 3) == "101") //뷰티인경우 3depth도 보여준다
            {
                ViewData["3DEPTH"] = _ProductService.GetCategoryDeptList("PRODUCT_TYPE", product_front_search_entity.P_CATE_CODE.Substring(0, 3), product_front_search_entity.P_CATE_CODE.Substring(3, 3)).ToList(); //뷰티 3depth
            }

            return View(_ProductService.GetProductList(product_front_search_entity).ToList());
            //return View();
        }
        #endregion

        #region 상품상세보기
        public ActionResult Detail(string pcode, Product_front_search_entity product_front_search_entity)
        {
            #region 상품상세정보
            //상품코드
            this.ViewBag.P_CODE = pcode;

            SP_PRODUCT_DETAIL_VIEW_Result productView = _ProductService.ViewProduct(pcode);
            ViewBag.PRODUCT_PATH = AboutMe.Common.Helper.Config.GetConfigValue("ProductPhotoPath"); //이미지디렉토리경로

            this.ViewBag.PAGE = product_front_search_entity.PAGE;                               //페이지
            this.ViewBag.PAGESIZE = product_front_search_entity.PAGESIZE;                       //페이지 사이즈
            this.ViewBag.SORT_GBN = product_front_search_entity.SORT_GBN;                       //정렬순서
            this.ViewBag.P_CATE_CODE = product_front_search_entity.P_CATE_CODE;                 //P_CATE_CODE
            this.ViewBag.C_CATE_CODE = product_front_search_entity.C_CATE_CODE;                 //C_CATE_CODE
            this.ViewBag.L_CATE_CODE = product_front_search_entity.L_CATE_CODE;                 //L_CATE_CODE
            this.ViewBag.P_CATE_CODE_3DEPTH = product_front_search_entity.P_CATE_CODE_3DEPTH;   //P_CATE_CODE_3DEPTH
            this.ViewBag.DEPTH_NAME3 = product_front_search_entity.DEPTH_NAME3;                 //뷰티3DEPTH NAME

            this.ViewBag.IS_LOGIN = MemberInfo.IsMemberLogin();  //로그인 여부  T/F
            this.ViewBag.M_ID = MemberInfo.GetMemberId();  //로그인 계정
            #endregion 

            #region 연관상품 가져오기
            ViewData["WITH_PRODUCT_LIST"] = "";
            this.ViewBag.withCnt = 0;
            string _with_product_list = productView.WITH_PRODUCT_LIST;
            if (!string.IsNullOrEmpty(_with_product_list))
            {
                ViewData["WITH_PRODUCT_LIST"] = _ProductService.GetProductWithList(_with_product_list).ToList(); //연관상품 리스트
                this.ViewBag.withCnt = -1;
            }
            #endregion 

            #region 로그
            UserLog userlog = new UserLog();
            userlog.UserLogSave("P_CODE:"+pcode, "상품상세정보");
            #endregion

            return View(productView);
        }
        #endregion
    }
}