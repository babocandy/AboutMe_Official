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
using AboutMe.Web.Admin.Common;

namespace AboutMe.Web.Admin.Controllers
{
    public class AdminProductController : BaseAdminController
    {

        // GET: AdminProduct
        private IAdminProductService _AdminProductService;
        public AdminProductController(IAdminProductService _adminProductService)
        {
            this._AdminProductService = _adminProductService;
        }

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
                return RedirectToAction("Index");
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
        public ActionResult ProductIndex(ProductSearch_Entity productSearch_Entity)
        {

            this.ViewBag.PageSize = productSearch_Entity.PageSize;
            this.ViewBag.SearchKey = productSearch_Entity.SearchKey;
            this.ViewBag.SearchKeyword = productSearch_Entity.SearchKeyword;
            this.ViewBag.cateCode = productSearch_Entity.cateCode;
            this.ViewBag.iconYn = productSearch_Entity.iconYn;
            this.ViewBag.searchDisplayYn = productSearch_Entity.searchDisplayYn;

            int TotalRecord = 0;
            TotalRecord = _AdminProductService.GetAdminProductCnt(productSearch_Entity);
            
            this.ViewBag.TotalRecord = TotalRecord;
            this.ViewBag.Page = productSearch_Entity.Page;

            ProductSearch_Entity e = new ProductSearch_Entity();

            return View(_AdminProductService.GetAdminProductList(productSearch_Entity).ToList());
            
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
        public ActionResult ProductInsert(TB_PRODUCT_INFO tb_product_info, HttpPostedFileBase MAIN_IMG, HttpPostedFileBase OTHER_IMG1, HttpPostedFileBase OTHER_IMG2, HttpPostedFileBase OTHER_IMG3, HttpPostedFileBase OTHER_IMG4, HttpPostedFileBase OTHER_IMG5)
        {
            if (ModelState.IsValid)
            {
                string Product_path = AboutMe.Common.Helper.Config.GetConfigValue("ProductPhotoPath");

                #region 파일 업로드
                if (MAIN_IMG != null)
                {
                    //MAIN_IMG.SaveAs(Server.MapPath(Product_path) + MAIN_IMG.FileName);
                    //ImageUpload imageUpload = new ImageUpload { Width = 600, UploadPath = Product_path, addMobileImage = true, fileType="file"};
                    ImageUpload imageUpload = new ImageUpload { Width = 600, UploadPath = Product_path, addMobileImage = true };

                    // rename, resize, and upload
                    //return object that contains {bool Success,string ErrorMessage,string ImageName}
                    ImageResult imageResult = imageUpload.RenameUploadFile(MAIN_IMG);
                    if (imageResult.Success)
                    {
                        tb_product_info.MAIN_IMG = imageResult.ImageName;
                    }
                    else
                    {
                        //ViewBag.Error = imageResult.ErrorMessage;
                        tb_product_info.MAIN_IMG = "";
                    }
                }

                if (OTHER_IMG1 != null)
                {
                    //OTHER_IMG1.SaveAs(Server.MapPath(Product_path) + OTHER_IMG1.FileName);
                    //tb_product_info.OTHER_IMG1 = OTHER_IMG1.FileName;

                    ImageUpload imageUpload1 = new ImageUpload { Width = 600, UploadPath = Product_path, addMobileImage = true };
                    ImageResult imageResult1 = imageUpload1.RenameUploadFile(OTHER_IMG1);
                    if (imageResult1.Success)
                    {
                        tb_product_info.OTHER_IMG1 = imageResult1.ImageName;
                    }
                    else
                    {
                        tb_product_info.OTHER_IMG1 = "";
                    }

                }
                if (OTHER_IMG2 != null)
                {
                    //OTHER_IMG2.SaveAs(Server.MapPath(Product_path) + OTHER_IMG2.FileName);
                    //tb_product_info.OTHER_IMG2 = OTHER_IMG2.FileName;

                    ImageUpload imageUpload2 = new ImageUpload { Width = 600, UploadPath = Product_path, addMobileImage = true };
                    ImageResult imageResult2 = imageUpload2.RenameUploadFile(OTHER_IMG2);
                    if (imageResult2.Success)
                    {
                        tb_product_info.OTHER_IMG2 = imageResult2.ImageName;
                    }
                    else
                    {
                        tb_product_info.OTHER_IMG2 = "";
                    }
                }
                if (OTHER_IMG3 != null)
                {
                    ImageUpload imageUpload3 = new ImageUpload { Width = 600, UploadPath = Product_path, addMobileImage = true };
                    ImageResult imageResult3 = imageUpload3.RenameUploadFile(OTHER_IMG3);
                    if (imageResult3.Success)
                    {
                        tb_product_info.OTHER_IMG3 = imageResult3.ImageName;
                    }
                    else
                    {
                        tb_product_info.OTHER_IMG3 = "";
                    }
                }
                if (OTHER_IMG4 != null)
                {
                    ImageUpload imageUpload4 = new ImageUpload { Width = 600, UploadPath = Product_path, addMobileImage = true };
                    ImageResult imageResult4 = imageUpload4.RenameUploadFile(OTHER_IMG4);
                    if (imageResult4.Success)
                    {
                        tb_product_info.OTHER_IMG4 = imageResult4.ImageName;
                    }
                    else
                    {
                        tb_product_info.OTHER_IMG4 = "";
                    }
                }
                if (OTHER_IMG5 != null)
                {
                    ImageUpload imageUpload5 = new ImageUpload { Width = 600, UploadPath = Product_path, addMobileImage = true };
                    ImageResult imageResult5 = imageUpload5.RenameUploadFile(OTHER_IMG5);
                    if (imageResult5.Success)
                    {
                        tb_product_info.OTHER_IMG5 = imageResult5.ImageName;
                    }
                    else
                    {
                        tb_product_info.OTHER_IMG5 = "";
                    }
                }
                #endregion

                //_AdminProductService.InsertAdminProduct(P_CATE_CODE, C_CATE_CODE, L_CATE_CODE, P_CODE, P_NAME, P_COUNT, P_POINT, P_PRICE, SELLING_PRICE, DISCOUNT_RATE, DISCOUNT_P_POINT, DISCOUNT_PRICE, SOLDOUT_YN, P_INFO_DETAIL_WEB, P_INFO_DETAIL_MOBILE, MV_URL, P_COMPONENT_INFO, P_TAG, MAIN_IMG, OTHER_IMG1, OTHER_IMG2, OTHER_IMG3, OTHER_IMG4, OTHER_IMG5, DISPLAY_YN, ICON_YN, WITH_PRODUCT_LIST);
                _AdminProductService.InsertAdminProduct(tb_product_info);
                
                #region 상품등록 로그 생성
                var serialised = JsonConvert.SerializeObject(tb_product_info); //entity 클래스 값을 json 포맷으로 파싱
                AdminLog adminlog = new AdminLog();
                adminlog.AdminLogSave(serialised, "관리자상품등록");
                #endregion

                return RedirectToAction("ProductIndex", new { SearchCol = "" });
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
        public ActionResult ProductUpdate(string pcode)
        {
            SP_ADMIN_PRODUCT_DETAIL_VIEW_Result productView = _AdminProductService.ViewAdminProduct(pcode);
            ViewBag.PRODUCT_PATH = AboutMe.Common.Helper.Config.GetConfigValue("ProductPhotoPath"); //이미지디렉토리경로
            return View(productView);
        }

        // POST
        [HttpPost, ValidateInput(false)] //"<" 같은 위지윅 게시판의 html코드 값을 가져올때 false로 세팅
        [ValidateAntiForgeryToken]
        public ActionResult ProductUpdate(TB_PRODUCT_INFO tb_product_info, HttpPostedFileBase MAIN_IMG, HttpPostedFileBase OTHER_IMG1, HttpPostedFileBase OTHER_IMG2, HttpPostedFileBase OTHER_IMG3, HttpPostedFileBase OTHER_IMG4, HttpPostedFileBase OTHER_IMG5)
        {
            
            if (ModelState.IsValid)
            {
                string Product_path = AboutMe.Common.Helper.Config.GetConfigValue("ProductPhotoPath");

                #region 파일 업로드
                if (MAIN_IMG != null)
                {
                    ImageUpload imageUpload = new ImageUpload { Width = 600, UploadPath = Product_path, addMobileImage = true };
                    ImageResult imageResult = imageUpload.RenameUploadFile(MAIN_IMG);
                    if (imageResult.Success)
                    {
                        tb_product_info.MAIN_IMG = imageResult.ImageName;
                    }
                    else
                    {
                        tb_product_info.MAIN_IMG = tb_product_info.OLD_MAIN_IMG;
                    }
                }
                else
                {
                    tb_product_info.MAIN_IMG = tb_product_info.OLD_MAIN_IMG;
                }

                if (OTHER_IMG1 != null)
                {
                    ImageUpload imageUpload1 = new ImageUpload { Width = 600, UploadPath = Product_path, addMobileImage = true };
                    ImageResult imageResult1 = imageUpload1.RenameUploadFile(OTHER_IMG1);
                    if (imageResult1.Success)
                    {
                        tb_product_info.OTHER_IMG1 = imageResult1.ImageName;
                    }
                    else
                    {
                        tb_product_info.OTHER_IMG1 = tb_product_info.OLD_OTHER_IMG1;
                    }

                }
                else
                {
                    tb_product_info.OTHER_IMG1 = tb_product_info.OLD_OTHER_IMG1;
                }
                if (OTHER_IMG2 != null)
                {
                    ImageUpload imageUpload2 = new ImageUpload { Width = 600, UploadPath = Product_path, addMobileImage = true };
                    ImageResult imageResult2 = imageUpload2.RenameUploadFile(OTHER_IMG2);
                    if (imageResult2.Success)
                    {
                        tb_product_info.OTHER_IMG2 = imageResult2.ImageName;
                    }
                    else
                    {
                        tb_product_info.OTHER_IMG2 = tb_product_info.OLD_OTHER_IMG2;
                    }
                }
                else
                {
                    tb_product_info.OTHER_IMG2 = tb_product_info.OLD_OTHER_IMG2;
                }
                if (OTHER_IMG3 != null)
                {
                    ImageUpload imageUpload3 = new ImageUpload { Width = 600, UploadPath = Product_path, addMobileImage = true };
                    ImageResult imageResult3 = imageUpload3.RenameUploadFile(OTHER_IMG3);
                    if (imageResult3.Success)
                    {
                        tb_product_info.OTHER_IMG3 = imageResult3.ImageName;
                    }
                    else
                    {
                        tb_product_info.OTHER_IMG3 = tb_product_info.OLD_OTHER_IMG3;
                    }
                }
                else
                {
                    tb_product_info.OTHER_IMG3 = tb_product_info.OLD_OTHER_IMG3;
                }
                if (OTHER_IMG4 != null)
                {
                    ImageUpload imageUpload4 = new ImageUpload { Width = 600, UploadPath = Product_path, addMobileImage = true };
                    ImageResult imageResult4 = imageUpload4.RenameUploadFile(OTHER_IMG4);
                    if (imageResult4.Success)
                    {
                        tb_product_info.OTHER_IMG4 = imageResult4.ImageName;
                    }
                    else
                    {
                        tb_product_info.OTHER_IMG4 = tb_product_info.OLD_OTHER_IMG4;
                    }
                }
                else
                {
                    tb_product_info.OTHER_IMG4 = tb_product_info.OLD_OTHER_IMG4;
                }
                if (OTHER_IMG5 != null)
                {
                    ImageUpload imageUpload5 = new ImageUpload { Width = 600, UploadPath = Product_path, addMobileImage = true };
                    ImageResult imageResult5 = imageUpload5.RenameUploadFile(OTHER_IMG5);
                    if (imageResult5.Success)
                    {
                        tb_product_info.OTHER_IMG5 = imageResult5.ImageName;
                    }
                    else
                    {
                        tb_product_info.OTHER_IMG5 = tb_product_info.OLD_OTHER_IMG5;
                    }
                }
                else
                {
                    tb_product_info.OTHER_IMG5 = tb_product_info.OLD_OTHER_IMG5;
                }
                #endregion

                _AdminProductService.UpdateAdminProduct(tb_product_info);

                #region 상품수정 로그 생성
                var serialised = JsonConvert.SerializeObject(tb_product_info);
                AdminLog adminlog = new AdminLog();
                adminlog.AdminLogSave(serialised, "관리자상품수정");
                #endregion

                return RedirectToAction("ProductIndex");
            }
            else
            {
                return View();
            }
        }
        #endregion
        
        #endregion

    }
}
