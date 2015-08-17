using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


//using System.Reflection;
using System.Web.Routing;

using AboutMe.Common.Helper;
using AboutMe.Domain.Service.AdminProduct;
using AboutMe.Domain.Entity.AdminProduct;
using System.Data.Entity.Core.Objects;
using AboutMe.Common.Data;
using Newtonsoft.Json;
using AboutMe.Web.Admin.Common;
using AboutMe.Web.Admin.Common.Filters;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;


namespace AboutMe.Web.Admin.Controllers
{
    [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
    public class AdminProductController : BaseAdminController
    {
        #region 인터페이스
        private IAdminProductService _AdminProductService;
        public AdminProductController(IAdminProductService _adminProductService)
        {
            this._AdminProductService = _adminProductService;
        }
        #endregion

        #region 카테고리

        #region 카테고리 리스트
        public ActionResult Index(string cate_gbn, string depth1_code, string depth2_code)
        {
            if (cate_gbn == null)
            {
                cate_gbn = "PRODUCT_TYPE";
            }
            if (depth1_code == null)
            {
                depth1_code = "101";
            }
            if (depth2_code == null)
            {
                depth2_code = "101";
            }

            ViewBag.cate_gbn = cate_gbn;
            ViewBag.depth1_code = depth1_code;
            ViewBag.depth2_code = depth2_code;

            ViewData["category1"] = _AdminProductService.GetAdminCategoryDeptListAll("PRODUCT_TYPE", "", "").ToList();
            ViewData["category2"] = _AdminProductService.GetAdminCategoryDeptListAll("PRODUCT_TYPE", depth1_code, "").ToList();
            ViewData["category3"] = _AdminProductService.GetAdminCategoryDeptListAll("PRODUCT_TYPE", depth1_code, depth2_code).ToList();
            ViewData["categoryLine"] = _AdminProductService.GetAdminCategoryDeptListAll("LINE_TYPE", depth1_code, "").ToList();
            ViewData["categorySkin"] = _AdminProductService.GetAdminCategoryDeptListAll("SKIN_TYPE", depth1_code, "").ToList();
            return View();
        }
        #endregion

        #region 카테고리 등록
        
        // GET: AdminProduct/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string CATE_GBN, string DEPTH1_CODE, string DEPTH2_CODE, string DEPTH_NAME)
        {

            //System.Data.Entity.Core.Objects.ObjectParameter intResult;
            //int intResult = 0;
            try
            {
                // TODO: Add insert logic here

                //;
                if (String.IsNullOrEmpty(DEPTH1_CODE))
                { 
                 _AdminProductService.InsertAdminCategoryOne(DEPTH_NAME);
                }
                else if (CATE_GBN != "PRODUCT_TYPE") //피부타입 또는 라인타입은 무조건 여기
                {
                    _AdminProductService.InsertAdminCategoryTwo(CATE_GBN, DEPTH1_CODE, DEPTH_NAME);
                }
                else if ( (!String.IsNullOrEmpty(DEPTH1_CODE)) && (String.IsNullOrEmpty(DEPTH2_CODE)) )
                {
                    _AdminProductService.InsertAdminCategoryTwo(CATE_GBN, DEPTH1_CODE, DEPTH_NAME);
                }
                else if ((!String.IsNullOrEmpty(DEPTH1_CODE)) && (!String.IsNullOrEmpty(DEPTH2_CODE)) )
                {
                    _AdminProductService.InsertAdminCategoryThree(CATE_GBN, DEPTH1_CODE, DEPTH2_CODE, DEPTH_NAME);
                }
                else
                {
                    
                }

                //return RedirectToAction("Index");
                //Redirect("/AdminMember/Index");
                //return View(Index("" ,"", "","", 1, 10));
                //ViewBag.resultVal = i;
                //return RedirectToAction("Index", new { SearchCol = ViewBag.resultVal });
                //return RedirectToAction("Index");
                return View();
            }
            catch
            {
                return View();
            }
        }

        #endregion

        #region 카테고리 수정

        #region 카테고리 수정 1depth
        // POST
        [HttpPost]
        public ActionResult CategoryOneUpdate(List<TB_CATEGORY> tb_category)
        {
            
            try
            {
                foreach (TB_CATEGORY category in tb_category)
                {
                    //category.DEPTH1_NAME.ToString();
                    if (category.DISPLAY_YN != null)
                    { category.DISPLAY_YN = "Y"; }
                    else
                    { category.DISPLAY_YN = "N"; }
                    _AdminProductService.UpdateAdminCategoryOne(category.IDX, category.DEPTH1_NAME, category.DISPLAY_YN, category.RE_SORT);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region 카테고리 수정 2depth
        // POST
        [HttpPost]
        public ActionResult CategoryTwoUpdate(List<TB_CATEGORY> tb_category)
        {

            try
            {
                foreach (TB_CATEGORY category in tb_category)
                {
                    //category.DEPTH1_NAME.ToString();
                    if (category.DISPLAY_YN != null)
                    { category.DISPLAY_YN = "Y"; }
                    else
                    { category.DISPLAY_YN = "N"; }
                    _AdminProductService.UpdateAdminCategoryTwo(category.IDX, category.DEPTH2_NAME, category.DISPLAY_YN, category.RE_SORT);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region 카테고리 수정 3depth
        // POST
        [HttpPost]
        public ActionResult CategoryThreeUpdate(List<TB_CATEGORY> tb_category)
        {

            try
            {
                foreach (TB_CATEGORY category in tb_category)
                {
                    //category.DEPTH1_NAME.ToString();
                    if (category.DISPLAY_YN != null)
                    { category.DISPLAY_YN = "Y"; }
                    else
                    { category.DISPLAY_YN = "N"; }
                    _AdminProductService.UpdateAdminCategoryThree(category.IDX, category.DEPTH3_NAME, category.DISPLAY_YN, category.RE_SORT);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        #endregion

        // GET:  수정
        public ActionResult CategoryUpdate(int idx)
        {
            SP_ADMIN_CATEGORY_VIEW_Result categoryView = _AdminProductService.ViewAdminCategory(idx);
            return View(categoryView);
        }

        // POST
        [HttpPost]
        public ActionResult CategoryUpdate(int IDX, string DEPTH1_NAME, string DISPLAY_YN, int RE_SORT)
        {
            try
            {
                _AdminProductService.UpdateAdminCategoryOne(IDX, DEPTH1_NAME, DISPLAY_YN, RE_SORT);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // POST
        [HttpPost]
        //public ActionResult CategoryOneInsert(IEnumerable<string> IsEnabled, IEnumerable<string> DEPTH1_NAME)
        //public ActionResult CategoryOneInsert(IEnumerable<TB_CATEGORY> tb_category)
        //public ActionResult CategoryOneInsert(FormCollection tb_category)
        //public ActionResult CategoryOneInsert(List<TB_CATEGORY> tb_category)
        //public ActionResult CategoryOneInsert(IEnumerable<string> IsEnabled, IEnumerable<string> DEPTH1_NAME, IEnumerable<string> DEPTH1_CODE)
        //public ActionResult CategoryOneInsert(IEnumerable<SP_ADMIN_CATEGORY_ONE_SEL_Result> IsEnabled, IEnumerable<string> DEPTH1_NAME, IEnumerable<string> DEPTH1_CODE)
        //public ActionResult CategoryOneInsert(FormCollection form)
        //public ActionResult CategoryOneInsert(FormCollection form)
        public ActionResult CategoryOneInsert(List<TB_CATEGORY> tb_category)
        {

            foreach (TB_CATEGORY category in tb_category)
            {
              category.DEPTH1_NAME.ToString();
            }


            //foreach (var key in form.AllKeys)
            //{
            //    var value = form[key];
            //    // etc.
            //}

            //foreach (var key in form.Keys)
            //{
            //    var value = form[key.ToString()];
            //    // etc.
            //}

            //string IsEnabled = Request.Form["IsEnabled"];
            //string DEPTH1_NAME = Request.Form["DEPTH1_NAME"];

            //string[] AllStrings = form["IsEnabled"].Split(',');
            //foreach (string item in AllStrings)
            //{
            //    int value = int.Parse(item);
            //    // handle value
            //}

            //var allvalues = form["IsEnabled"].Split(',').Select(x => int.Parse(x));
          
            //if (tb_category.DEPTH1_CODE.Any(m => m.ToString()))  
            //{
            //   //How to get the selected Code & Name here
            //}
            //IsEnabled.Count(0).ToString();
            


            //foreach (TB_CATEGORY tb_category in tb_category)
            //{
            //    enabled.ToString();
            //}

            try
            {
                //_AdminProductService.UpdateAdminCategoryOne(IDX, DEPTH1_NAME, DISPLAY_YN, RE_SORT);
                //return RedirectToAction("Index");
                return View();
            }
            catch
            {
                return View();
            }
        }

        

        #endregion

        #region 카테고리 삭제
        
        // POST: AdminProduct/CategoryDelete
        public ActionResult CategoryDelete(int idx)
        {
            try
            {
                // TODO: Add delete logic here
                _AdminProductService.DeleteAdminCategoryOne(idx);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #endregion

        #region ajax partial category partialView 방식
        public PartialViewResult CategoryList()
        {
            #region ajax dropdownlist partialView 방식

            List<SP_ADMIN_CATEGORY_ONE_SEL_Result> CateCodeData = _AdminProductService.GetAdminCategoryOneList().ToList();
            List<SelectListItem> P_CATE_CODE = Utility01.GetDropDownList<SP_ADMIN_CATEGORY_ONE_SEL_Result>("DEPTH1_NAME", "DEPTH1_CODE", "001", CateCodeData.ToList());
            ViewData["P_CATE_CODE"] = P_CATE_CODE;

            return PartialView("_categoryList");

            #endregion

            #region 참고소스(지금은 사용안함)
            //var CateCodeData = new List<SP_ADMIN_CATEGORY_ONE_SEL_Result>();
            //List<SelectListItem> P_CATE_CODE = new List<SelectListItem>();

            //return PartialView("_categoryList", P_CATE_CODE);
            //ReturnArgs r = new ReturnArgs();
            //r.ViewString = this.RenderViewToString("_CaseManager");
            
            //string viewData1 = PartialView("_categoryList", P_CATE_CODE).ToString();
            //return Json(new { success = true, ViewString = viewData1 });
            //return Json(r);
            #endregion
        }
        #endregion

        #region ajax partial category 자바스크립트로 dropdownlist방식
        public ActionResult CategoryListJavascript(string CATE_GBN, string DEPTH1_CODE, string DEPTH2_CODE)
        {
            List<SP_ADMIN_CATEGORY_DEPTH_SEL_Result> CateCodeData = _AdminProductService.GetAdminCategoryDeptList(CATE_GBN, DEPTH1_CODE, DEPTH2_CODE).ToList();
            //ViewData["DEPTH_CATE_CODE"] = CateCodeData;
            return Json(new { success = true, msg = CateCodeData });
        }
        #endregion

        #endregion

        #region 상품
        
        #region 상품 리스트
        [ValidateInput(false)] //"<" 같은 위지윅 게시판의 html코드 값을 가져올때 false로 세팅
        public ActionResult ProductIndex(ProductSearch_Entity productSearch_Entity)
        {
            #region 초기화작업
            productSearch_Entity.iconYn = string.IsNullOrEmpty(Request.Form["iconYn"]) ? "" : Request.Form["iconYn"];
            productSearch_Entity.BatchIconYn = string.IsNullOrEmpty(Request.Form["BatchIconYn"]) ? "" : Request.Form["BatchIconYn"];
            this.ViewBag.cateCode1 = productSearch_Entity.cateCode.Length < 3 ? "" : productSearch_Entity.cateCode.Substring(0, 3);
            this.ViewBag.cateCode2 = productSearch_Entity.cateCode.Length < 3 ? "" : productSearch_Entity.cateCode.Substring(3, 3);
            this.ViewBag.cateCode3 = productSearch_Entity.cateCode.Length < 3 ? "" : productSearch_Entity.cateCode.Substring(6, 3);
            
            //상품코드 다중검색일때 스페이스를 ,로 변환한다.
            if ((!string.IsNullOrEmpty(productSearch_Entity.SearchKeyword)) && (productSearch_Entity.SearchKey == "P_CODE"))
            {
                productSearch_Entity.SearchKeyword = productSearch_Entity.SearchKeyword.Replace(" ", ",");
            }
            #endregion

            PRODUCT_INDEX_MODEL m = new PRODUCT_INDEX_MODEL();
            m.productSearch_Entity = productSearch_Entity;
            m.TotalCount = _AdminProductService.GetAdminProductCnt(productSearch_Entity);
            m.ProductList = _AdminProductService.GetAdminProductList(productSearch_Entity).ToList();
            return View(m);
        }
        #endregion

        #region 상품 할인정책 리스트
        [ValidateInput(false)] //"<" 같은 위지윅 게시판의 html코드 값을 가져올때 false로 세팅
        public ActionResult ProductPriceIndex(ProductSearch_Entity productSearch_Entity)
        {
            #region 초기화작업
            productSearch_Entity.iconYn = string.IsNullOrEmpty(Request.Form["iconYn"]) ? "" : Request.Form["iconYn"];
            this.ViewBag.cateCode1 = productSearch_Entity.cateCode.Length < 3 ? "" : productSearch_Entity.cateCode.Substring(0, 3);
            this.ViewBag.cateCode2 = productSearch_Entity.cateCode.Length < 3 ? "" : productSearch_Entity.cateCode.Substring(3, 3);
            this.ViewBag.cateCode3 = productSearch_Entity.cateCode.Length < 3 ? "" : productSearch_Entity.cateCode.Substring(6, 3);
            #endregion

            //상품코드 다중검색일때 스페이스를 ,로 변환한다.
            if ((!string.IsNullOrEmpty(productSearch_Entity.SearchKeyword)) && (productSearch_Entity.SearchKey == "P_CODE"))
            {
                productSearch_Entity.SearchKeyword = productSearch_Entity.SearchKeyword.Replace(" ", ",");
            }

            PRODUCT_INDEX_MODEL m = new PRODUCT_INDEX_MODEL();
            m.productSearch_Entity = productSearch_Entity;
            m.TotalCount = _AdminProductService.GetAdminProductCnt(productSearch_Entity);
            m.ProductList = _AdminProductService.GetAdminProductList(productSearch_Entity).ToList();
            return View(m);

        }
        #endregion

        #region 상품 등록

        public ActionResult ProductInsert()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)] //"<" 같은 위지윅 게시판의 html코드 값을 가져올때 false로 세팅
        [ValidateAntiForgeryToken]
       
        //public ActionResult ProductInsert(string P_CATE_CODE, string C_CATE_CODE, string L_CATE_CODE, string P_CODE, string P_NAME, Nullable<int> P_COUNT, Nullable<int> P_POINT, Nullable<int> P_PRICE, Nullable<int> SELLING_PRICE, Nullable<int> DISCOUNT_RATE, Nullable<int> DISCOUNT_P_POINT, Nullable<int> DISCOUNT_PRICE, string SOLDOUT_YN, string P_INFO_DETAIL_WEB, string P_INFO_DETAIL_MOBILE, string MV_URL, string P_COMPONENT_INFO, string P_TAG, string MAIN_IMG, string OTHER_IMG1, string OTHER_IMG2, string OTHER_IMG3, string OTHER_IMG4, string OTHER_IMG5, string DISPLAY_YN, string ICON_YN, string WITH_PRODUCT_LIST)
        public ActionResult ProductInsert(TB_PRODUCT_INFO tb_product_info, HttpPostedFileBase MAIN_IMG, HttpPostedFileBase OTHER_IMG1, HttpPostedFileBase OTHER_IMG2, HttpPostedFileBase OTHER_IMG3)
        {
            int intResult = -1; //결과값 0:정상, 나머지 오류
            if (ModelState.IsValid)
            {

                this.ViewBag.iconYn = Request.Form["iconYn"];
                tb_product_info.ICON_YN = string.IsNullOrEmpty(this.ViewBag.iconYn) ? "" : this.ViewBag.iconYn.Replace(",", "");
                string Product_path = AboutMe.Common.Helper.Config.GetConfigValue("ProductPhotoPath");

                #region 파일 업로드
                if (MAIN_IMG != null)
                {
                    ImageUpload imageUpload = new ImageUpload { UploadPath = Product_path, addMobileImage = true };

                    ImageResult imageResult = imageUpload.RenameUploadFile(MAIN_IMG);
                    if (imageResult.Success)
                    {
                        tb_product_info.MAIN_IMG = imageResult.ImageName;
                    }
                    else
                    {
                        return Content("<script language='javascript' type='text/javascript'>alert('대표 이미지가 업로드 되지 않았습니다. 에러메시지:" + imageResult.ErrorMessage + "');history.go(-1);</script>");
                    }
                }

                if (OTHER_IMG1 != null)
                {
                    ImageUpload imageUpload1 = new ImageUpload { UploadPath = Product_path, addMobileImage = true };
                    ImageResult imageResult1 = imageUpload1.RenameUploadFile(OTHER_IMG1);
                    if (imageResult1.Success)
                    {
                        tb_product_info.OTHER_IMG1 = imageResult1.ImageName;
                    }
                    else
                    {
                        return Content("<script language='javascript' type='text/javascript'>alert('추가1 이미지가 업로드 되지 않았습니다. 에러메시지:" + imageResult1.ErrorMessage + "');history.go(-1);</script>");
                    }

                }
                if (OTHER_IMG2 != null)
                {
                    ImageUpload imageUpload2 = new ImageUpload { UploadPath = Product_path, addMobileImage = true };
                    ImageResult imageResult2 = imageUpload2.RenameUploadFile(OTHER_IMG2);
                    if (imageResult2.Success)
                    {
                        tb_product_info.OTHER_IMG2 = imageResult2.ImageName;
                    }
                    else
                    {
                        return Content("<script language='javascript' type='text/javascript'>alert('추가2 이미지가 업로드 되지 않았습니다. 에러메시지:" + imageResult2.ErrorMessage + "');history.go(-1);</script>");
                    }
                }
                if (OTHER_IMG3 != null)
                {
                    ImageUpload imageUpload3 = new ImageUpload { UploadPath = Product_path, addMobileImage = true };
                    ImageResult imageResult3 = imageUpload3.RenameUploadFile(OTHER_IMG3);
                    if (imageResult3.Success)
                    {
                        tb_product_info.OTHER_IMG3 = imageResult3.ImageName;
                    }
                    else
                    {
                        return Content("<script language='javascript' type='text/javascript'>alert('추가3 이미지가 업로드 되지 않았습니다. 에러메시지:" + imageResult3.ErrorMessage + "');history.go(-1);</script>");
                    }
                }

               
                #endregion

                intResult = _AdminProductService.InsertAdminProduct(tb_product_info);

                #region 상품등록 로그 생성
                var serialised = "결과값 intResult :" + intResult;
                serialised = serialised + JsonConvert.SerializeObject(tb_product_info); //entity 클래스 값을 json 포맷으로 파싱
                AdminLog adminlog = new AdminLog();
                adminlog.AdminLogSave(serialised, "관리자상품등록");
                #endregion


                if (intResult != 0)
                {
                    return Content("<script language='javascript' type='text/javascript'>alert('상품정보가 등록되지 않았습니다.');history.go(-1);</script>");
                }
                else
                {
                    return RedirectToAction("ProductIndex", new { SearchCol = "" });
                }
            }
            else
            {
                return View();
            }
        }
        #endregion

        #region 상품코드(p_code) 유효성 체크
        [HttpPost]
        public ActionResult PcodeChk(string P_CODE)
        {
            int? pcodeCount = _AdminProductService.PcodeChkAdminProduct(P_CODE);
            if (pcodeCount == 0)
            {
                return Json(new { success = true, msg = "사용가능한 상품코드입니다." });
            }
            else
            {
                return Json(new { success = false, msg = "사용할 수 없는 상품코드입니다." });
            }
        }
        #endregion

        #region 상품 개별 이미지 DB에서 삭제
        [HttpPost]
        public ActionResult AjaxImageDel(string P_CODE, string imgColumName)
        {
            _AdminProductService.ImageDelAdminProduct(P_CODE, imgColumName);
            return Json(new { success = true, msg = "사용가능한 상품코드입니다." });
            
        }
        #endregion
        
        #region 상품 수정
        public ActionResult ProductUpdate(string pcode, ProductSearch_Entity Param)
        {
            PRODUCT_DETAIL_MODEL m = new PRODUCT_DETAIL_MODEL();
            m.pcode = pcode;
            m.productSearch_Entity = Param;
            m.ProductDetailView = _AdminProductService.ViewAdminProduct(pcode);
            return View(m);
        }

        [HttpPost, ValidateInput(false)] //"<" 같은 위지윅 게시판의 html코드 값을 가져올때 false로 세팅
        [ValidateAntiForgeryToken]
        public ActionResult ProductUpdate(ProductSearch_Entity Param, TB_PRODUCT_INFO tb_product_info, HttpPostedFileBase MAIN_IMG, HttpPostedFileBase OTHER_IMG1, HttpPostedFileBase OTHER_IMG2, HttpPostedFileBase OTHER_IMG3)
        {
            int intResult = -1; //결과값 0:정상, 나머지 오류
            if (ModelState.IsValid)
            {

                this.ViewBag.iconYn = Request.Form["iconYn"];
                tb_product_info.ICON_YN = string.IsNullOrEmpty(this.ViewBag.iconYn) ? "" : this.ViewBag.iconYn.Replace(",", "");
                
                string Product_path = AboutMe.Common.Helper.Config.GetConfigValue("ProductPhotoPath");

                #region 파일 업로드
                if (MAIN_IMG != null)
                {
                    ImageUpload imageUpload = new ImageUpload { UploadPath = Product_path, addMobileImage = true };
                    ImageResult imageResult = imageUpload.RenameUploadFile(MAIN_IMG);
                    if (imageResult.Success)
                    {
                        tb_product_info.MAIN_IMG = imageResult.ImageName;
                    }
                    else
                    {
                        return Content("<script language='javascript' type='text/javascript'>alert('대표 이미지가 업로드 되지 않았습니다. 에러메시지:"+imageResult.ErrorMessage+"');history.go(-1);</script>");
                    }
                }
                else
                {
                    tb_product_info.MAIN_IMG = tb_product_info.OLD_MAIN_IMG;
                }

                if (OTHER_IMG1 != null)
                {
                    ImageUpload imageUpload1 = new ImageUpload { UploadPath = Product_path, addMobileImage = true };
                    ImageResult imageResult1 = imageUpload1.RenameUploadFile(OTHER_IMG1);
                    if (imageResult1.Success)
                    {
                        tb_product_info.OTHER_IMG1 = imageResult1.ImageName;
                    }
                    else
                    {
                        return Content("<script language='javascript' type='text/javascript'>alert('추가1 이미지가 업로드 되지 않았습니다. 에러메시지:" + imageResult1.ErrorMessage + "');history.go(-1);</script>");
                    }

                }
                else
                {
                    tb_product_info.OTHER_IMG1 = tb_product_info.OLD_OTHER_IMG1;
                }
                if (OTHER_IMG2 != null)
                {
                    ImageUpload imageUpload2 = new ImageUpload { UploadPath = Product_path, addMobileImage = true };
                    ImageResult imageResult2 = imageUpload2.RenameUploadFile(OTHER_IMG2);
                    if (imageResult2.Success)
                    {
                        tb_product_info.OTHER_IMG2 = imageResult2.ImageName;
                    }
                    else
                    {
                        return Content("<script language='javascript' type='text/javascript'>alert('추가2 이미지가 업로드 되지 않았습니다. 에러메시지:" + imageResult2.ErrorMessage + "');history.go(-1);</script>");
                    }
                }
                else
                {
                    tb_product_info.OTHER_IMG2 = tb_product_info.OLD_OTHER_IMG2;
                }
                if (OTHER_IMG3 != null)
                {
                    ImageUpload imageUpload3 = new ImageUpload { UploadPath = Product_path, addMobileImage = true };
                    ImageResult imageResult3 = imageUpload3.RenameUploadFile(OTHER_IMG3);
                    if (imageResult3.Success)
                    {
                        tb_product_info.OTHER_IMG3 = imageResult3.ImageName;
                    }
                    else
                    {
                        return Content("<script language='javascript' type='text/javascript'>alert('추가3 이미지가 업로드 되지 않았습니다. 에러메시지:" + imageResult3.ErrorMessage + "');history.go(-1);</script>");
                    }
                }
                else
                {
                    tb_product_info.OTHER_IMG3 = tb_product_info.OLD_OTHER_IMG3;
                }
               
                #endregion

                intResult = _AdminProductService.UpdateAdminProduct(tb_product_info);

                #region 상품수정 로그 생성
                var serialised = "결과값 intResult :" + intResult;
                serialised = serialised + JsonConvert.SerializeObject(tb_product_info);
                AdminLog adminlog = new AdminLog();
                adminlog.AdminLogSave(serialised, "관리자상품수정");
                #endregion

                if (intResult != 0)
                {
                    return Content("<script language='javascript' type='text/javascript'>alert('상품정보가 변경되지 않았습니다.');history.go(-1);</script>");
                }
                else
                {
                    RouteValueDictionary param = ConvertRouteValue(Param);
                    return RedirectToAction("ProductIndex", param);
                }

                
            }
            else
            {
                return View();
            }
        }
        #endregion

        #region 상품 가격 일괄 수정
        public ActionResult ProductPriceUpdate(List<TB_PRODUCT_INFO> tb_product_info, ProductSearch_Entity Param)
        {
                foreach (TB_PRODUCT_INFO tb_product in tb_product_info)
                {
                    if (!string.IsNullOrEmpty(tb_product.P_CODE))
                    {
                        _AdminProductService.UpdateAdminProductPrice(tb_product);

                        #region 상품가격 일괄 수정 로그 생성
                        var serialised = JsonConvert.SerializeObject(tb_product);
                        serialised += JsonConvert.SerializeObject(Param);
                        AdminLog adminlog = new AdminLog();
                        adminlog.AdminLogSave(serialised, "관리자상품가격일괄수정");
                        #endregion
                    }
                }
                RouteValueDictionary param = ConvertRouteValue(Param);
                return RedirectToAction("ProductPriceIndex", Param);
        }
        #endregion

        #region 상품 정보 일괄 수정
        public ActionResult ProductBatchUpdate(List<TB_PRODUCT_INFO> tb_product_info, ProductSearch_Entity Param)
        {
            Param.iconYn = string.IsNullOrEmpty(Request.Form["iconYn"]) ? "" : Request.Form["iconYn"];
            Param.BatchIconYn = string.IsNullOrEmpty(Request.Form["BatchIconYn"]) ? "" : Request.Form["BatchIconYn"].Replace(",", "");
            this.ViewBag.IconBatchChk = string.IsNullOrEmpty(Request.Form["IconBatchChk"]) ? "" : Request.Form["IconBatchChk"];
                foreach (TB_PRODUCT_INFO tb_product in tb_product_info)
                {
                    if (!string.IsNullOrEmpty(tb_product.P_CODE))
                    {
                        tb_product.ICON_YN = Param.BatchIconYn;
                        tb_product.ICON_BATCH_CHK   = this.ViewBag.IconBatchChk; 

                        _AdminProductService.UpdateAdminProductBatch(tb_product);

                        #region 상품가격 일괄 수정 로그 생성
                        var serialised = JsonConvert.SerializeObject(tb_product);
                        serialised += JsonConvert.SerializeObject(Param);
                        AdminLog adminlog = new AdminLog();
                        adminlog.AdminLogSave(serialised, "관리자상품정보_일괄수정");
                        #endregion
                    }
                }

                RouteValueDictionary param = ConvertRouteValue(Param);
                return RedirectToAction("ProductIndex", Param);
        }
        #endregion

        #region 상품전시 순서 바꾸기
        public ActionResult display_re_sort(int IDX, int RE_SORT, string CLICKCHK, ProductSearch_Entity productSearch_Entity)
        {
            this.ViewBag.PageSize = productSearch_Entity.PageSize;
            this.ViewBag.SearchKey = productSearch_Entity.SearchKey;
            this.ViewBag.SearchKeyword = productSearch_Entity.SearchKeyword;
            this.ViewBag.cateCode = productSearch_Entity.cateCode;
            this.ViewBag.iconYn = productSearch_Entity.iconYn;
            this.ViewBag.searchDisplayY = productSearch_Entity.searchDisplayY;
            this.ViewBag.searchDisplayN = productSearch_Entity.searchDisplayN;
            this.ViewBag.soldoutYn = productSearch_Entity.soldoutYn;
            this.ViewBag.POutletYn = productSearch_Entity.POutletYn;


             try
            {
                _AdminProductService.UpdateAdminProductReSort(IDX, RE_SORT, CLICKCHK);

                #region 로그 생성
                var serialised = "idx:"+IDX.ToString();
                serialised += "RE_SORT:" + RE_SORT.ToString();
                serialised += "CLICKCHK:" + CLICKCHK.ToString();
                serialised += JsonConvert.SerializeObject(productSearch_Entity);
                AdminLog adminlog = new AdminLog();
                adminlog.AdminLogSave(serialised, "관리자_상품전시_순서_바꾸기");
                #endregion
            return RedirectToAction("ProductIndex");
            }
             catch
             {
                 return RedirectToAction("ProductIndex");
             }

        }
        #endregion

        #region 상품 엑셀 다운로드
        [ValidateInput(false)] //"<" 같은 위지윅 게시판의 html코드 값을 가져올때 false로 세팅
        public ActionResult ProductExcel(ProductSearch_Entity productSearch_Entity)
        {

            productSearch_Entity.Page = 1;
            productSearch_Entity.PageSize = 10000;
            
            var products = new System.Data.DataTable("Product");
            //헤더 구성
            products.Columns.Add("IDX", typeof(string));
            products.Columns.Add("P_CODE", typeof(string));
            products.Columns.Add("P_NAME", typeof(string));
            products.Columns.Add("MAIN_IMG", typeof(string));
            products.Columns.Add("MV_URL", typeof(string));
            products.Columns.Add("SELLING_PRICE", typeof(string));
            products.Columns.Add("DISCOUNT_PRICE", typeof(string));
            products.Columns.Add("DISCOUNT_RATE", typeof(string));
            
            var Data = _AdminProductService.GetAdminProductList(productSearch_Entity).ToList(); //데이타 가져오기
            if (Data != null && Data.Any())
            {

                foreach (var result in Data)
                {
                    products.Rows.Add(result.IDX, result.P_CODE, result.P_NAME, result.MAIN_IMG, result.MV_URL, result.SELLING_PRICE, result.DISCOUNT_PRICE, result.DISCOUNT_RATE);
                } //for
            } //if

            var grid = new GridView();
            grid.DataSource = products;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=AdminProduct.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "euc-kr";  //UTF-8 ,euc-kr
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("euc-kr");

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            //Response.Output.Write(sw.ToString());
            //Response.Flush();
            //Response.End();

            //return View("MyView");


            #region 로그 생성
            var serialised = JsonConvert.SerializeObject(productSearch_Entity);
            AdminLog adminlog = new AdminLog();
            adminlog.AdminLogSave(serialised, "관리자_상품엑셀다운");
            #endregion

            return Content(sw.ToString(), "application/ms-excel");

        }
        #endregion

        #region 팝업 상품 리스트
        [ValidateInput(false)] //"<" 같은 위지윅 게시판의 html코드 값을 가져올때 false로 세팅
        public ActionResult PopProductIndex(ProductSearch_Entity productSearch_Entity)
        {
            #region 초기화작업
            productSearch_Entity.iconYn = string.IsNullOrEmpty(Request.Form["iconYn"]) ? "" : Request.Form["iconYn"];
            productSearch_Entity.BatchIconYn = string.IsNullOrEmpty(Request.Form["BatchIconYn"]) ? "" : Request.Form["BatchIconYn"];
            this.ViewBag.cateCode1 = productSearch_Entity.cateCode.Length < 3 ? "" : productSearch_Entity.cateCode.Substring(0, 3);
            this.ViewBag.cateCode2 = productSearch_Entity.cateCode.Length < 3 ? "" : productSearch_Entity.cateCode.Substring(3, 3);
            this.ViewBag.cateCode3 = productSearch_Entity.cateCode.Length < 3 ? "" : productSearch_Entity.cateCode.Substring(6, 3);
            #endregion

            //상품코드 다중검색일때 스페이스를 ,로 변환한다.
            if ((!string.IsNullOrEmpty(productSearch_Entity.SearchKeyword)) && (productSearch_Entity.SearchKey == "P_CODE"))
            {
                productSearch_Entity.SearchKeyword = productSearch_Entity.SearchKeyword.Replace(" ", ",");
            }

            PRODUCT_INDEX_MODEL m = new PRODUCT_INDEX_MODEL();
            m.productSearch_Entity = productSearch_Entity;
            m.TotalCount = _AdminProductService.GetAdminProductCnt(productSearch_Entity);
            m.ProductList = _AdminProductService.GetAdminProductList(productSearch_Entity).ToList();
            return View(m);
        }
        #endregion

        #region 지워도 됨 기존소스
        //this.ViewBag.BatchIconYn = string.IsNullOrEmpty(Request.Form["BatchIconYn"]) ? "" : Request.Form["BatchIconYn"];
        //productSearch_Entity.cateCode = string.IsNullOrEmpty(productSearch_Entity.cateCode) ? "" : productSearch_Entity.cateCode;
        //productSearch_Entity.SearchKeyword = string.IsNullOrEmpty(productSearch_Entity.SearchKeyword) ? "" : productSearch_Entity.SearchKeyword.Replace(" ", ",");
        //this.ViewBag.BatchIconYn = Request.Form["BatchIconYn"];
        //this.ViewBag.iconYn = Request.Form["iconYn"];
        //this.ViewBag.PageSize = productSearch_Entity.PageSize;
        //this.ViewBag.SearchKey = productSearch_Entity.SearchKey;
        //this.ViewBag.SearchKeyword = productSearch_Entity.SearchKeyword;
        //this.ViewBag.searchStatus = productSearch_Entity.searchStatus;
        //this.ViewBag.cateCode = productSearch_Entity.cateCode;
        //this.ViewBag.cateCode = string.IsNullOrEmpty(this.ViewBag.cateCode) ? "" : this.ViewBag.cateCode;
        //if (string.IsNullOrEmpty(this.ViewBag.iconYn))
        //{
        //    this.ViewBag.iconYn = "";
        //}
        //if (string.IsNullOrEmpty(this.ViewBag.BatchIconYn))
        //{
        //    this.ViewBag.BatchIconYn = "";
        //}

        //this.ViewBag.searchDisplayY = productSearch_Entity.searchDisplayY;
        //this.ViewBag.searchDisplayN = productSearch_Entity.searchDisplayN;
        //if (string.IsNullOrEmpty(productSearch_Entity.searchDisplayY))
        //{
        //    this.ViewBag.searchDisplayY = "";
        //    productSearch_Entity.searchDisplayY = "";
        //}
        //if (string.IsNullOrEmpty(productSearch_Entity.searchDisplayN))
        //{
        //    this.ViewBag.searchDisplayN = "";
        //    productSearch_Entity.searchDisplayN = "";
        //}
        //this.ViewBag.soldoutYn = productSearch_Entity.soldoutYn;
        //this.ViewBag.POutletYn = productSearch_Entity.POutletYn;
        //this.ViewBag.FROM_DATE = productSearch_Entity.FROM_DATE;
        //if (string.IsNullOrEmpty(productSearch_Entity.FROM_DATE))
        //{
        //    this.ViewBag.FROM_DATE = "";
        //    productSearch_Entity.FROM_DATE = "";
        //}
        //this.ViewBag.TO_DATE = productSearch_Entity.TO_DATE;
        //if (string.IsNullOrEmpty(productSearch_Entity.TO_DATE))
        //{
        //    this.ViewBag.TO_DATE = "";
        //    productSearch_Entity.TO_DATE = "";
        //}

        //int TotalRecord = 0;
        //TotalRecord = _AdminProductService.GetAdminProductCnt(productSearch_Entity);

        //this.ViewBag.TotalRecord = TotalRecord;
        //this.ViewBag.Page = productSearch_Entity.Page;

        //ViewBag.PRODUCT_PATH = AboutMe.Common.Helper.Config.GetConfigValue("ProductPhotoPath"); //이미지디렉토리경로
        //return View(_AdminProductService.GetAdminProductList(productSearch_Entity).ToList());
        #endregion

        #endregion

        #region 사은품관리

        #region 사은품 리스트
        public ActionResult GiftIndex(SP_TB_FREE_GIFT_INFO_Param Param)
        {
            GIFT_INDEX_MODEL m = new GIFT_INDEX_MODEL();
            m.SearchOption = Param;
            m.TotalCount = _AdminProductService.GetAdminGiftCnt(Param);
            m.GiftList = _AdminProductService.GetAdminGiftList(Param).ToList();

            #region SMS TEST
            ////SMS 즉시 전송 샘플 
            //AdminSMSModel adminSMSModel = new AdminSMSModel();
            //adminSMSModel.SEND_MSG = "sms 내용"; //SMS 메시지
            //adminSMSModel.HANDPHONE = "01000000000"; //발신번호
            //AdminSMS adminsms = new AdminSMS();
            //adminsms.AdminSmsSend(adminSMSModel);


            ////SMS 예약 전송 샘플
            //AdminSMSModel adminSMSModel = new AdminSMSModel();
            //adminSMSModel.SMS_FLAG = "2"; //SMS 예약발송 플래그값:2
            //adminSMSModel.SEND_TIME = "2015-08-13 18:17:00"; //예약시간
            //adminSMSModel.SEND_MSG = "sms 내용";
            //adminSMSModel.HANDPHONE = "01000000000"; //발신번호
            //AdminSMS adminsms = new AdminSMS();
            //adminsms.AdminSmsSend(adminSMSModel);



            ////LMS 즉시 전송 샘플
            //AdminSMSModel adminSMSModel = new AdminSMSModel();
            //adminSMSModel.SMS_FLAG = "3";  //LMS 즉시발송 플래그값:3
            //adminSMSModel.TITLE = "장문테스트입니다.";
            //adminSMSModel.SEND_MSG = "LMS 내용";
            //adminSMSModel.HANDPHONE = "01000000000"; //발신번호
            //AdminSMS adminsms = new AdminSMS();
            //adminsms.AdminSmsSend(adminSMSModel);



            ////LMS 예약 전송 샘플
            //AdminSMSModel adminSMSModel = new AdminSMSModel();
            //adminSMSModel.SMS_FLAG = "4";  //LMS 예약발송 플래그값:4
            //adminSMSModel.SEND_TIME = "2015-08-13 18:17:00"; //예약시간
            //adminSMSModel.TITLE = "장문테스트입니다.";
            //adminSMSModel.SEND_MSG = "LMS 내용";
            //adminSMSModel.HANDPHONE = "01000000000"; //발신번호
            //AdminSMS adminsms = new AdminSMS();
            //adminsms.AdminSmsSend(adminSMSModel);
            #endregion

            return View(m);
        }
        #endregion

        #region 사은품 등록

        public ActionResult GiftInsert()
        {
            return View();
        }

        [HttpPost] 
        [ValidateAntiForgeryToken]
        public ActionResult GiftInsert(AdminGiftModel admin_gift_model, HttpPostedFileBase GIFT_IMG)
        {
            if (ModelState.IsValid)
            {
                string Product_path = AboutMe.Common.Helper.Config.GetConfigValue("GiftPhotoPath");
        
                #region 파일 업로드
                if (GIFT_IMG != null)
                {
                    ImageUpload imageUpload = new ImageUpload { UploadPath = Product_path };

                    ImageResult imageResult = imageUpload.RenameUploadFile(GIFT_IMG);
                    if (imageResult.Success)
                    {
                        admin_gift_model.GIFT_IMG = imageResult.ImageName;
                    }
                    else
                    {
                        admin_gift_model.GIFT_IMG = "";
                    }
                }
                #endregion

                _AdminProductService.InsertAdminGift(admin_gift_model);

                #region 사은품 등록 로그 생성
                var serialised = JsonConvert.SerializeObject(admin_gift_model); //entity 클래스 값을 json 포맷으로 파싱
                AdminLog adminlog = new AdminLog();
                adminlog.AdminLogSave(serialised, "관리자사은품등록");
                #endregion

                return RedirectToAction("GiftIndex", new { SearchCol = "" });
            }
            else
            {
                return View();
            }
        }
        #endregion

        #region 사은품 수정
        public ActionResult GiftUpdate(int idx, SP_TB_FREE_GIFT_INFO_Param Param)
        {
            GIFT_DETAIL_MODEL m = new GIFT_DETAIL_MODEL();
            m.GIFT_IDX = idx;
            m.SearchOption = Param;
            m.GiftDetailView = _AdminProductService.ViewAdminGift(idx);
            return View(m);
        }

        // POST
        [HttpPost] 
        [ValidateAntiForgeryToken]
        public ActionResult GiftUpdate(SP_TB_FREE_GIFT_INFO_Param Param, AdminGiftModel admin_gift_model, HttpPostedFileBase GIFT_IMG)
        {

            if (ModelState.IsValid)
            {
                string Gift_path = AboutMe.Common.Helper.Config.GetConfigValue("GiftPhotoPath");
                #region 파일 업로드
                if (GIFT_IMG != null)
                {
                    ImageUpload imageUpload = new ImageUpload { UploadPath = Gift_path };

                    ImageResult imageResult = imageUpload.RenameUploadFile(GIFT_IMG);
                    if (imageResult.Success)
                    {
                        admin_gift_model.GIFT_IMG = imageResult.ImageName;
                    }
                    else
                    {
                        admin_gift_model.GIFT_IMG = admin_gift_model.OLD_GIFT_IMG;
                    }
                }
                else
                {
                    admin_gift_model.GIFT_IMG = admin_gift_model.OLD_GIFT_IMG;
                }
                #endregion
                _AdminProductService.UpdateAdminGift(admin_gift_model);
         
                #region 사은품 수정 로그 생성
                var serialised = JsonConvert.SerializeObject(admin_gift_model);
                serialised += JsonConvert.SerializeObject(Param);
                AdminLog adminlog = new AdminLog();
                adminlog.AdminLogSave(serialised, "관리자사은품수정");
                #endregion

                RouteValueDictionary param = ConvertRouteValue(Param);
                return RedirectToAction("GiftIndex", param);
            }
            else
            {
                return View();
            }
        }
        #endregion

        #endregion

        #region SMS 발송
        //public void SMSIns(AdminSMSModel adminSMSModel)
        //{
        //    this._AdminProductService.InsertSMS(adminSMSModel);

        //    #region SMS 등록 로그 생성
        //    var serialised = JsonConvert.SerializeObject(adminSMSModel); //entity 클래스 값을 json 포맷으로 파싱
        //    AdminLog adminlog = new AdminLog();
        //    adminlog.AdminLogSave(serialised, "SMS 등록");
        //    #endregion
        //}
        #endregion
    }
}
