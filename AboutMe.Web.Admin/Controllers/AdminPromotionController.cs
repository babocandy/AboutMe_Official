﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Common.Helper;
using AboutMe.Domain.Service.AdminPromotion;
using AboutMe.Domain.Entity.AdminPromotion;

namespace AboutMe.Web.Admin.Controllers
{
    public class AdminPromotionController : BaseAdminController
    {


        private IAdminPromotionService _AdminPromotionService;

        public AdminPromotionController(IAdminPromotionService _adminPromotionService)
        {
            this._AdminPromotionService = _adminPromotionService;
        }

        protected override void HandleUnknownAction(string actionName)
        {
            this.View(actionName).ExecuteResult(this.ControllerContext);
        }


        public class MyMultiModelForCreate
        {
            public TB_PROMOTION_BY_TOTAL inst_TB_PROMOTION_BY_TOTAL {get; set;}
            public List<SP_ADMIN_PROMOTION_BY_TOTAL_DETAIL_SEL_Result> inst_PROMOTION_BY_TOTAL_DETAIL_SEL_Result { get; set; }
            public TB_PROMOTION_BY_TOTAL_MEM_GRADE inst_TB_PROMOTION_BY_TOTAL_MEM_GRADE { get; set; }
            public List<SP_ADMIN_PROMOTION_BY_TOTAL_MEM_GRADE_WITH_CD_SEL_Result> inst_PROMOTION_BY_TOTAL_MEM_GRADE_WITH_CD_SEL_Result { get; set; }
        }

        public class MyMultiModelForCreateProduct
        {
            public TB_PROMOTION_BY_PRODUCT inst_TB_PROMOTION_BY_PRODUCT { get; set; }
            public List<SP_ADMIN_PROMOTION_BY_PRODUCT_DETAIL_SEL_Result> inst_PROMOTION_BY_PRODUCT_DETAIL_SEL_Result { get; set; }
            
        }

        public class MyMultiModelForProductPricing
        {
            public TB_PROMOTION_BY_PRODUCT_PRICE inst_TB_PROMOTION_BY_PRODUCT_PRICE { get; set; }
            public List<SP_ADMIN_PROMOTION_BY_PRODUCT_PRICE_SEL_Result> inst_PROMOTION_BY_PRODUCT_PRICE_SEL_Result { get; set; }
            public TB_PROMOTION_PRODUCT_VS_TOTAL inst_TB_PROMOTION_BY_PRODUCT_VS_TOTAL { get; set; }
            public List<SP_ADMIN_PROMOTION_BY_TOTAL_ACTIVE_LIST_SEL_Result> inst_SP_ADMIN_PROMOTION_BY_TOTAL_ACTIVE_LIST_SEL_Result { get; set; }
            
        }





        #region 전체할인 ==================================================================================================

        // GET: AdminPromotion
        public ActionResult Index(string SearchCol = "", string SearchKeyword = "", string SortCol = "IDX", string SortDir = "DESC", int Page = 1, int PageSize = 10)
        {


            this.ViewBag.PageSize = PageSize;
            this.ViewBag.SearchCol = SearchCol;
            this.ViewBag.SearchKeyword = SearchKeyword;
            this.ViewBag.SortCol = SortCol;
            this.ViewBag.SortDir = SortDir;

            //AdminMemberService srv =  new AdminMemberService();
            int TotalRecord = 0;
            TotalRecord = _AdminPromotionService.GetAdminPromotionByTotalListCnt(SearchCol, SearchKeyword);
            this.ViewBag.TotalRecord = TotalRecord;
            //this.ViewBag.MaxPage = (int)Math.Ceiling((double)count / page_size); //올림
            this.ViewBag.Page = Page;


            return View(_AdminPromotionService.GetAdminPromotionByTotalList(SearchCol, SearchKeyword, Page, PageSize).ToList());
            //return View();
        }


        //[Bind(Exclude="Id")]Product productToCreate
        //public ActionResult Create([Bind(Include = "ID,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        public ActionResult Create()
        {
           
            var mMyMultiModelForCreate = new MyMultiModelForCreate
            {
                inst_TB_PROMOTION_BY_TOTAL = new TB_PROMOTION_BY_TOTAL(),
                inst_TB_PROMOTION_BY_TOTAL_MEM_GRADE = new TB_PROMOTION_BY_TOTAL_MEM_GRADE()
            };


            return View(mMyMultiModelForCreate);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ADM_ID,ADM_PASS,ADM_NAME,ADM_DEPT,POINT")] MyMultiModelForCreate.inst_TB_PROMOTION_BY_TOTAL  , string[] CheckMemGrade)
        public ActionResult Create([Bind(Prefix = "inst_TB_PROMOTION_BY_TOTAL", Include = "PMO_TOTAL_NAME,PMO_TOTAL_CATEGORY,PMO_TOTAL_RATE_OR_MONEY,PMO_TOTAL_DISCOUNT_RATE,PMO_TOTAL_DISCOUNT_MONEY,PMO_TOTAL_DATE_FROM,PMO_TOTAL_DATE_TO,USABLE_YN")]  TB_PROMOTION_BY_TOTAL tb_promotion_by_total, string[] CheckMemGrade)
        {
            
            
            int is_success = 1;

    
            if (ModelState.IsValid)
            {
                string StrDateFrom = "" ,StrDateTo = "";
                string PmoTotalCategory = "";
                DateTime DateFrom , DateTo ;

                DateFrom = tb_promotion_by_total.PMO_TOTAL_DATE_FROM.Value;
                DateTo= tb_promotion_by_total.PMO_TOTAL_DATE_TO.Value;
                PmoTotalCategory = tb_promotion_by_total.PMO_TOTAL_CATEGORY.ToString();

                if (DateTime.Compare(DateFrom, DateTo) < 0) //시작날짜가 끝날짜보다 먼저이면
                {
                    // 동일 날짜대에 같은 종류의 프로모션이 없거나, 지금 등록하려는 프로모션이 비활성화상태의 프로모션이면
                    if (_AdminPromotionService.GetAdminPromotionByTotalDupSel(PmoTotalCategory, DateFrom, DateTo) == 0 || tb_promotion_by_total.USABLE_YN == "N") 
                    {

                        is_success = _AdminPromotionService.InsAdminPromotionByTotal(tb_promotion_by_total, CheckMemGrade);

                        if (is_success == 1)
                        {
                            return RedirectToAction("Index", new { SearchCol = "", SearchKeyword = "" });
                        }
                        else
                        {
                            @TempData["jsMessage"] = "<script language='javascript'>alert('Data Insert과정에서 오류가 발생하였습니다.')</script>";
                        }
                    }
                    else
                    {
                        @TempData["jsMessage"] = "<script language='javascript'>alert('동일 날짜대에 같은 종류의 (활성화된)프로모션이 존재합니다.')</script>";
                    }
                }
                else
                {
                    @TempData["jsMessage"] = "<script language='javascript'>alert('프로모션 시작시간과 종료시간을 확인하세요.')</script>";
                }
            }

            return View();
        }


        
        public ActionResult Update (string CdPromotionTotal)
        {

            var mMyMultiModelForCreate = new MyMultiModelForCreate
            {
                inst_PROMOTION_BY_TOTAL_DETAIL_SEL_Result = new List<SP_ADMIN_PROMOTION_BY_TOTAL_DETAIL_SEL_Result>(),
                inst_TB_PROMOTION_BY_TOTAL_MEM_GRADE = new TB_PROMOTION_BY_TOTAL_MEM_GRADE()
            };


            string aas = "";
            if (TempData["jsMessage"] != null)
            {
                aas = TempData["jsMessage"].ToString();
            }
          

            mMyMultiModelForCreate.inst_PROMOTION_BY_TOTAL_DETAIL_SEL_Result = _AdminPromotionService.GetAdminPromotionByTotalDetail(CdPromotionTotal).ToList();
            mMyMultiModelForCreate.inst_PROMOTION_BY_TOTAL_MEM_GRADE_WITH_CD_SEL_Result = _AdminPromotionService.GetAdminPromotionByTotalMemGradeWithCd(CdPromotionTotal).ToList();
      
            return View(mMyMultiModelForCreate);
            
        }

      
        // , Include = "PMO_TOTAL_NAME,PMO_TOTAL_CATEGORY,PMO_TOTAL_DISCOUNT_RATE,PMO_TOTAL_DISCOUNT_MONEY,PMO_TOTAL_DATE_FROM,PMO_TOTAL_DATE_TO,USABLE_YN"

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update([Bind(Prefix = "inst_PROMOTION_BY_TOTAL_DETAIL_SEL_Result[0]", Include = "PMO_TOTAL_NAME,PMO_TOTAL_CATEGORY,PMO_TOTAL_DISCOUNT_RATE,PMO_TOTAL_DISCOUNT_MONEY,PMO_TOTAL_DATE_FROM,PMO_TOTAL_DATE_TO,USABLE_YN", Exclude = "PMO_TOTAL_RATE_OR_MONEY")]  TB_PROMOTION_BY_TOTAL tb_promotion_by_total, string[] CheckMemGrade, string CdPromotionTotal)
        {

            int is_success = 1;

            @TempData["jsMessage"] = "11";
            if (ModelState.IsValid)
            {
                string StrDateFrom = "", StrDateTo = "";
                string PmoTotalCategory = "";
                DateTime DateFrom, DateTo;

                DateFrom = tb_promotion_by_total.PMO_TOTAL_DATE_FROM.Value;
                DateTo = tb_promotion_by_total.PMO_TOTAL_DATE_TO.Value;
                PmoTotalCategory = tb_promotion_by_total.PMO_TOTAL_CATEGORY.ToString();

                if (DateTime.Compare(DateFrom, DateTo) < 0) //시작날짜가 끝날짜보다 먼저이면
                {
                    // 동일 날짜대에 같은 종류의 프로모션이 없거나, 지금 등록하려는 프로모션이 비활성화상태의 프로모션이면
                    if (_AdminPromotionService.GetAdminPromotionByTotalDupAtUpdateSel(PmoTotalCategory, DateFrom, DateTo,CdPromotionTotal) == 0 || tb_promotion_by_total.USABLE_YN == "N")
                    {

                        is_success = _AdminPromotionService.UpdateAdminPromotionByTotal(tb_promotion_by_total, CheckMemGrade,CdPromotionTotal);

                        if (is_success == 1)
                        {
                            return RedirectToAction("Index", new { SearchCol = "", SearchKeyword = "" });
                        }
                        else
                        {
                            @TempData["jsMessage"] = "<script language='javascript'>alert('Data Update과정에서 오류가 발생하였습니다.')</script>";
                        }
                    }
                    else
                    {
                        @TempData["jsMessage"] = "<script language='javascript'>alert('동일 날짜대에 같은 종류의 (활성화된)프로모션이 존재합니다.')</script>";
                    }
                }
                else 
                {
                    @TempData["jsMessage"] = "<script language='javascript'>alert('프로모션 시작시간과 종료시간을 확인하세요.')</script>";
                }
            }

            string _CdPromotionTotal = CdPromotionTotal;
            return RedirectToAction("Update", new { CdPromotionTotal = _CdPromotionTotal });

        }

        #endregion ===========================================================================================


        #region 상품별할인 ========================================================================================

        // GET: AdminPromotion
        public ActionResult PrdIndex(string SearchCol = "", string SearchKeyword = "", string SortCol = "IDX", string SortDir = "DESC", int Page = 1, int PageSize = 10)
        {


            this.ViewBag.PageSize = PageSize;
            this.ViewBag.SearchCol = SearchCol;
            this.ViewBag.SearchKeyword = SearchKeyword;
            this.ViewBag.SortCol = SortCol;
            this.ViewBag.SortDir = SortDir;

            //AdminMemberService srv =  new AdminMemberService();
            int TotalRecord = 0;
            TotalRecord = _AdminPromotionService.GetAdminPromotionByProductListCnt(SearchCol, SearchKeyword);
            this.ViewBag.TotalRecord = TotalRecord;
            //this.ViewBag.MaxPage = (int)Math.Ceiling((double)count / page_size); //올림
            this.ViewBag.Page = Page;


            return View(_AdminPromotionService.GetAdminPromotionByProductList(SearchCol, SearchKeyword, Page, PageSize).ToList());
            //return View();
        }



        //[Bind(Exclude="Id")]Product productToCreate
        //public ActionResult Create([Bind(Include = "ID,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        public ActionResult PrdCreate()
        {

            var mMyMultiModelForCreateProduct = new MyMultiModelForCreateProduct
            {
                inst_TB_PROMOTION_BY_PRODUCT = new TB_PROMOTION_BY_PRODUCT(),
            };


            return View(mMyMultiModelForCreateProduct);

        }
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ADM_ID,ADM_PASS,ADM_NAME,ADM_DEPT,POINT")] MyMultiModelForCreate.inst_TB_PROMOTION_BY_PRODUCT  , string[] CheckMemGrade)
        public ActionResult PrdCreate([Bind(Prefix = "inst_TB_PROMOTION_BY_PRODUCT", Include = "PMO_PRODUCT_NAME,PMO_PRODUCT_CATEGORY,PMO_PRODUCT_RATE_OR_MONEY,PMO_ONEONE_MULTIPLE_CNT,PMO_SET_DISCOUNT_CNT,PMO_PRODUCT_DISCOUNT_RATE,PMO_PRODUCT_DISCOUNT_MONEY,PMO_PRODUCT_DATE_FROM,PMO_PRODUCT_DATE_TO,USABLE_YN")]  TB_PROMOTION_BY_PRODUCT tb_promotion_by_product)
        {


            int is_success = 1;


            if (ModelState.IsValid)
            {
                string StrDateFrom = "", StrDateTo = "";
                string PmoProductCategory = "";
                DateTime DateFrom, DateTo;

                DateFrom = tb_promotion_by_product.PMO_PRODUCT_DATE_FROM.Value;
                DateTo = tb_promotion_by_product.PMO_PRODUCT_DATE_TO.Value;
                PmoProductCategory = tb_promotion_by_product.PMO_PRODUCT_CATEGORY.ToString();

                if (DateTime.Compare(DateFrom, DateTo) < 0) //시작날짜가 끝날짜보다 먼저이면
                {
                    // 동일 날짜대에 같은 종류의 프로모션이 없거나, 지금 등록하려는 프로모션이 비활성화상태의 프로모션이면
                    if (_AdminPromotionService.GetAdminPromotionByProductDupSel(PmoProductCategory, DateFrom, DateTo) == 0 || tb_promotion_by_product.USABLE_YN == "N")
                    {

                        is_success = _AdminPromotionService.InsAdminPromotionByProduct(tb_promotion_by_product);

                        if (is_success == 1)
                        {
                            return RedirectToAction("PrdIndex", new { SearchCol = "", SearchKeyword = "" });
                        }
                        else
                        {
                            @TempData["jsMessage"] = "-1"; //데이터 INSERT 과정에 에러발생
                        }
                    }
                    else
                    {
                        @TempData["jsMessage"] = "-2"; //<script language='javascript'>alert('동일 날짜대에 같은 종류의 (활성화된)프로모션이 존재합니다.')</script>
                    }
                }
                else
                {
                    @TempData["jsMessage"] = "-3"; //<script language='javascript'>alert('프로모션 시작시간과 종료시간을 확인하세요.')</script>
                }
            }

            return View();
        }


        // 상품별할인 프로모션에 소속된 상품(가격)리스트
        public ActionResult PrdPricingIndex(string CdPromotionProduct)
        {

            /**
            var mMyMultiModelForProductPricing = new MyMultiModelForProductPricing
            {
                inst_PROMOTION_BY_PRODUCT_PRICE_SEL_Result = new List<SP_ADMIN_PROMOTION_BY_PRODUCT_PRICE_SEL_Result>(),
                inst_PROMOTION_BY_PRODUCT_VS_TOTAL_SEL_Result = new List<SP_ADMIN_PROMOTION_BY_PRODUCT_VS_TOTAL_SEL_Result> ; 
            };
             ***/


            string aas = "";
            if (TempData["jsMessage"] != null)
            {
                aas = TempData["jsMessage"].ToString();
            }

            //프로모션 시작시간 가져오기 
            Nullable<DateTime> PmoProductDateFrom  = null ;
            List<SP_ADMIN_PROMOTION_BY_PRODUCT_DETAIL_SEL_Result> TotalLst  = _AdminPromotionService.GetAdminPromotionByProductDetail(CdPromotionProduct).ToList();
            if (TotalLst.Count > 0)
            {
                PmoProductDateFrom = Convert.ToDateTime(TotalLst[0].PMO_PRODUCT_DATE_FROM) ;
            }
            ViewData["PromotionStartTime"] = PmoProductDateFrom;



            //상품별로 적용된 전체할인 가능 정보 가져오기
            ViewData["SP_PROMOTION_BY_PRODUCT_VS_TOTAL_SEL_Result"] = _AdminPromotionService.GetAdminPromotionByProductVsTotalList(CdPromotionProduct).ToList();

            //전체 전체할인 프로모션중 usable = y인것만 가져오기
            ViewData["SP_ADMIN_PROMOTION_BY_TOTAL_ACTIVE_LIST_SEL_Result"] = _AdminPromotionService.GetAdminPromotionByTotalActiveList().ToList();

            /**
            mMyMultiModelForProductPricing.inst_PROMOTION_BY_PRODUCT_PRICE_SEL_Result= _AdminPromotionService.GetAdminPromotionByProductPricingList(CdPromotionProduct).ToList();
            mMyMultiModelForProductPricing.inst_PROMOTION_BY_PRODUCT_VS_TOTAL_SEL_Result = _AdminPromotionService.GetAdminPromotionByProductVsTotalList(CdPromotionProduct).ToList();
            **/

            List<SP_ADMIN_PROMOTION_BY_PRODUCT_PRICE_SEL_Result> lst = _AdminPromotionService.GetAdminPromotionByProductPricingList(CdPromotionProduct).ToList();
            return View(lst);
        }


        //가격정보 생성
        //[Bind(Exclude="Id")]Product productToCreate
        //public ActionResult Create([Bind(Include = "ID,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        public ActionResult PrdPricingCreate(string CdPromotionProduct)
        {

            var mMyMultiModelForProductPricing = new MyMultiModelForProductPricing
            {
                inst_TB_PROMOTION_BY_PRODUCT_PRICE = new TB_PROMOTION_BY_PRODUCT_PRICE(),
                inst_SP_ADMIN_PROMOTION_BY_TOTAL_ACTIVE_LIST_SEL_Result = _AdminPromotionService.GetAdminPromotionByTotalActiveList().ToList()
            };

            
            return View(mMyMultiModelForProductPricing);

        }


        //가격정보 생성
        //[Bind(Exclude="Id")]Product productToCreate
        //public ActionResult Create([Bind(Include = "ID,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PrdPricingCreate([Bind(Prefix = "inst_TB_PROMOTION_BY_PRODUCT_PRICE", Include = "PMO_PRICE,PMO_ONE_ONE_P_CODE,PMO_ONE_ONE_PRICE,USABLE_YN")]  TB_PROMOTION_BY_PRODUCT_PRICE tb_promotion_by_product_price, string CdPromotionProduct, string[] CheckCdPromotionTotal)
        {

          
            var mMyMultiModelForProductPricing = new MyMultiModelForProductPricing
            {
                inst_TB_PROMOTION_BY_PRODUCT_PRICE = new TB_PROMOTION_BY_PRODUCT_PRICE(),
                inst_SP_ADMIN_PROMOTION_BY_TOTAL_ACTIVE_LIST_SEL_Result = _AdminPromotionService.GetAdminPromotionByTotalActiveList().ToList()
            };
            

            int is_success = 1;
            int isPcodeExist = 0; //상품코드 유효성 검증여부 

            string _CdPromotionProduct = CdPromotionProduct;

            if (ModelState.IsValid)
            {
                isPcodeExist = _AdminPromotionService.GetAdminPromotionProductCodeCheck(tb_promotion_by_product_price.P_CODE);

                if (isPcodeExist <= 0) //입력된 상품코드가 존재하지 않거나 비전시상태이면
                {
                    //입력된 상품코드가 이미 (UsableYN =Y) 상태로 존재하거나, 지금 입력한 가격정책의 UsableYN = 'N'이면 
                    if (_AdminPromotionService.GetAdminPromotionByProductPricingAllDupSel(CdPromotionProduct, tb_promotion_by_product_price.P_CODE) == 0 || tb_promotion_by_product_price.USABLE_YN == "N")
                    {

                        is_success = _AdminPromotionService.InsAdminPromotionByProductPricing(tb_promotion_by_product_price, CdPromotionProduct, CheckCdPromotionTotal);
                        if (is_success == 1) // INSERT가 성공했으면 
                        {
                            return RedirectToAction("PrdPricingIndex", new { CdPromotionProduct = _CdPromotionProduct });
                        }
                        else
                        {
                            @TempData["jsMessage"] = "-1"; //데이터 INSERT 과정에 에러발생
                        }
                    }
                    else
                    {
                        @TempData["jsMessage"] = "-2"; //<script language='javascript'>alert('상품코드 중복이 있습니다. 동일 날짜대에 실행될 프로모션에 같은 상품이 존재합니다.')</script>
                    }
                }
                else
                {
                    @TempData["jsMessage"] = "-3"; //<script language='javascript'>alert('입력한 상품코드가 존재하지 않거나 비전시 상태입니다.')</script>
                }
            }


            return View(mMyMultiModelForProductPricing);

        }



        #endregion ===============================================================================================

        /**
                [HttpPost]
                [ValidateAntiForgeryToken]
                public ActionResult Create([Bind(Include = "ADM_ID,ADM_PASS,ADM_NAME,ADM_DEPT,POINT")] ADMIN_MEMBER AdminMember, HttpPostedFileBase ADM_PHOTO)
                {

                    if (ModelState.IsValid)
                    {

                        string AdmPhoto_path = AboutMe.Common.Helper.Config.GetConfigValue("PhotoBasePath");



                        string PhotoFileName = "";

                        //파일 업로드
                        if (ADM_PHOTO != null)
                        {
                            ADM_PHOTO.SaveAs(Server.MapPath(AdmPhoto_path) + ADM_PHOTO.FileName);
                            PhotoFileName = ADM_PHOTO.FileName;
                        }


                        _AdminMemberService.InsertAdminMemberTest(AdminMember.ADM_ID, AdminMember.ADM_NAME, PhotoFileName, AdminMember.ADM_PASS, AdminMember.ADM_DEPT, 100);
                        return RedirectToAction("Index", new { SearchCol = "", SearchKeyword = "" });
                    }
                    else
                    {

                        var DeptListData = new List<SP_ADM_ADMIN_DEPT_SEL_Result>();

                        DeptListData = _AdminMemberService.GetAdmDeptList();
                        List<SelectListItem> ADM_DEPT = new List<SelectListItem>();

                        ADM_DEPT = Utility01.GetDropDownList<SP_ADM_ADMIN_DEPT_SEL_Result>("DEPT_NAME", "DEPT_CODE", "001", DeptListData.ToList());
                        ViewData["ADM_DEPT"] = ADM_DEPT;

                        return View(AdminMember);
                    }



                }
         * **/


    }
}