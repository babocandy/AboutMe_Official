using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Common.Helper;
using AboutMe.Domain.Service.AdminCoupon;
using AboutMe.Domain.Entity.AdminCoupon;


using AboutMe.Domain.Service.BizPromotion;


using AboutMe.Web.Admin.Common.Filters;

namespace AboutMe.Web.Admin.Controllers
{
    public class AdminCouponController : BaseAdminController
    {

        private IBizPromotion _BizPromotionService;
        private IAdminCouponService _AdminCouponService;

        public AdminCouponController(IAdminCouponService _adminCouponService, IBizPromotion _bizPromotionService)
        {
            this._AdminCouponService = _adminCouponService;
            this._BizPromotionService = _bizPromotionService;
        }

        protected override void HandleUnknownAction(string actionName)
        {
            this.View(actionName).ExecuteResult(this.ControllerContext);
        }



        public class MyMultiModelForCreate
        {
            public TB_COUPON_MASTER inst_TB_COUPON_MASTER { get; set; }
            public List<SP_ADMIN_COUPON_MASTER_DETAIL_SEL_Result> inst_SP_ADMIN_COUPON_MASTER_DETAIL_SEL_Result { get; set; }
            public List<SP_ADMIN_COUPON_MEMBER_GRADE_SEL_Result> inst_SP_ADMIN_COUPON_MEMBER_GRADE_SEL_Result { get; set; }
            /**
            public TB_PROMOTION_BY_TOTAL_MEM_GRADE inst_TB_PROMOTION_BY_TOTAL_MEM_GRADE { get; set; }
            public List<SP_ADMIN_PROMOTION_BY_TOTAL_MEM_GRADE_WITH_CD_SEL_Result> inst_PROMOTION_BY_TOTAL_MEM_GRADE_WITH_CD_SEL_Result { get; set; }
            **/ 
             
        }


        public class MyMultiModelForProduct
        {
            public TB_COUPON_PRODUCT inst_TB_COUPON_PRODUCT { get; set; }
            public List<SP_ADMIN_COUPON_PRODUCT_DEATIL_SEL_Result> inst_SP_ADMIN_COUPON_PRODUCT_DETAIL_SEL_Result { get; set; }
            /**
            public TB_PROMOTION_BY_TOTAL_MEM_GRADE inst_TB_PROMOTION_BY_TOTAL_MEM_GRADE { get; set; }
            public List<SP_ADMIN_PROMOTION_BY_TOTAL_MEM_GRADE_WITH_CD_SEL_Result> inst_PROMOTION_BY_TOTAL_MEM_GRADE_WITH_CD_SEL_Result { get; set; }
            **/

        }


        public class MyMultiModelForProductForCreate
        {
            public TB_COUPON_PRODUCT inst_TB_COUPON_PRODUCT { get; set; }
            public List<SP_ADMIN_COUPON_PRODUCT_FOR_CREATE_DETAIL_SEL_Result> inst_SP_ADMIN_COUPON_PRODUCT_FOR_CREATE_DETAIL_SEL_Result { get; set; }
            /**
            public TB_PROMOTION_BY_TOTAL_MEM_GRADE inst_TB_PROMOTION_BY_TOTAL_MEM_GRADE { get; set; }
            public List<SP_ADMIN_PROMOTION_BY_TOTAL_MEM_GRADE_WITH_CD_SEL_Result> inst_PROMOTION_BY_TOTAL_MEM_GRADE_WITH_CD_SEL_Result { get; set; }
            **/

        }



        // GET: AdminCoupon
        [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult Index(string SearchCol = "", string SearchKeyword = "", string SortCol = "IDX", string SortDir = "DESC", int Page = 1, int PageSize = 10)
        {


            this.ViewBag.PageSize = PageSize;
            this.ViewBag.SearchCol = SearchCol;
            this.ViewBag.SearchKeyword = SearchKeyword;
            this.ViewBag.SortCol = SortCol;
            this.ViewBag.SortDir = SortDir;

            //프론트 상품상세페이지 영역 테스트 =================================================================================
            Dictionary<string, string> dic =
               _BizPromotionService.GetPromotionInfoForDetialPage("P", "S", "V", "test_v100s", "RAA00009", 40000);
            //==================================================================================================================
           
            //AdminMemberService srv =  new AdminMemberService();
            int TotalRecord = 0;
            TotalRecord = _AdminCouponService.GetAdminCouponListCnt(SearchCol, SearchKeyword);
            this.ViewBag.TotalRecord = TotalRecord;
            //this.ViewBag.MaxPage = (int)Math.Ceiling((double)count / page_size); //올림
            this.ViewBag.Page = Page;

            return View(_AdminCouponService.GetAdminCouponList(SearchCol, SearchKeyword, Page, PageSize).ToList());
        }

        [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult Create()
        {

            var mMyMultiModelForCreate = new MyMultiModelForCreate
            {
                inst_TB_COUPON_MASTER = new TB_COUPON_MASTER()
                //inst_TB_PROMOTION_BY_TOTAL_MEM_GRADE = new TB_PROMOTION_BY_TOTAL_MEM_GRADE()
            };

            return View(mMyMultiModelForCreate);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        //public ActionResult Create([Bind(Include = "ADM_ID,ADM_PASS,ADM_NAME,ADM_DEPT,POINT")] MyMultiModelForCreate.inst_TB_PROMOTION_BY_TOTAL  , string[] CheckMemGrade)
        public ActionResult Create([Bind(Prefix = "inst_TB_COUPON_MASTER", Exclude = "IDX,CD_COUPON,COUPON_NUM_CHECK_TF,ISSUE_MAX_LIMIT,INS_DATE")]  TB_COUPON_MASTER tb_coupon_master, string[] CheckMemGrade)
        {


            int is_success = 1;

            DateTime FixedPeriodFrom, FixedPeriodTo ;
            DateTime MasterFromDate, MasterToDate;

            bool DateValid = true;
            @TempData["jsMessage"] = "";

            if (ModelState.IsValid)
            {
                MasterFromDate= tb_coupon_master.MASTER_FROM_DATE.Value;
                MasterToDate= tb_coupon_master.MASTER_TO_DATE.Value;

                if (tb_coupon_master.SERVICE_LIFE_GBN == "A" && (tb_coupon_master.FIXED_PERIOD_FROM.HasValue && tb_coupon_master.FIXED_PERIOD_TO.HasValue)) //고정기간 기준이면
                {
                    FixedPeriodFrom= tb_coupon_master.FIXED_PERIOD_FROM.Value;
                    FixedPeriodTo = tb_coupon_master.FIXED_PERIOD_TO.Value;

                    if(FixedPeriodFrom < MasterFromDate) //고정기간의 시작일자가 정책유효기간보다 먼저이면 에러
                    {
                        DateValid = false;
                        @TempData["jsMessage"] = "<script language='javascript'>alert('고정기간의 시작일자는 정책유효기간보다 나중이어야 합니다.')</script>";
                    }
                    else if (FixedPeriodTo > MasterToDate)
                    {
                        DateValid = false;
                        @TempData["jsMessage"] = "<script language='javascript'>alert('고정기간의 종료일자는 정책유효기간 이내 이어야 합니다.')</script>";
                    }
                }


                if (DateValid == true)
                {
                    is_success = _AdminCouponService.InsAdminCouponMaster(tb_coupon_master, CheckMemGrade);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }

            return View();
            
            //return RedirectToAction("Index", new { CdPromotionTotal = _CdPromotionTotal });

        }


        [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult Update(string CdCoupon)
        {


            var mMyMultiModelForCreate = new MyMultiModelForCreate
            {
                 inst_TB_COUPON_MASTER = new TB_COUPON_MASTER()
                ,inst_SP_ADMIN_COUPON_MASTER_DETAIL_SEL_Result = new List<SP_ADMIN_COUPON_MASTER_DETAIL_SEL_Result>()
                ,inst_SP_ADMIN_COUPON_MEMBER_GRADE_SEL_Result =new List<SP_ADMIN_COUPON_MEMBER_GRADE_SEL_Result>()
                //inst_TB_PROMOTION_BY_TOTAL_MEM_GRADE = new TB_PROMOTION_BY_TOTAL_MEM_GRADE()
            };

            mMyMultiModelForCreate.inst_SP_ADMIN_COUPON_MEMBER_GRADE_SEL_Result = _AdminCouponService.GetAdminCouponMemberGradeList(CdCoupon);
            mMyMultiModelForCreate.inst_SP_ADMIN_COUPON_MASTER_DETAIL_SEL_Result = _AdminCouponService.GetAdminCouponDetail(CdCoupon);


            //int TotalRecord = 0;
            //TotalRecord = _AdminCouponService.GetAdminCouponIssuedListCnt("", "", CdCoupon);

            //ViewBag.IssuedCoponCount = TotalRecord; 

            return View(mMyMultiModelForCreate);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        //public ActionResult Create([Bind(Include = "ADM_ID,ADM_PASS,ADM_NAME,ADM_DEPT,POINT")] MyMultiModelForCreate.inst_TB_PROMOTION_BY_TOTAL  , string[] CheckMemGrade)
        public ActionResult Update([Bind(Prefix = "inst_SP_ADMIN_COUPON_MASTER_DETAIL_SEL_Result[0]", Include = "COUPON_NAME,FIXED_PERIOD_FROM,FIXED_PERIOD_TO,EXRIRED_DAY_FROM_ISSUE_DT,MASTER_FROM_DATE,MASTER_TO_DATE,USABLE_YN")]  TB_COUPON_MASTER tb_coupon_master, string[] CheckMemGrade, string CdCoupon
                            , string SERVICE_LIFE_GBN, string ORG_MASTER_FROM_DATE, string ORG_MASTER_TO_DATE, string ORG_FIXED_PERIOD_FROM, string ORG_FIXED_PERIOD_TO)
        {


            bool PostedDataValid = true;

            int TotalRecord = 0;
            TotalRecord = _AdminCouponService.GetAdminCouponIssuedListCnt("", "", CdCoupon);

            DateTime OrgMasterFromDate, OrgMasterToDate, InputMasterFromDate, InputMasterToDate ;
            DateTime OrgFixedPeriodFrom, OrgFixedPeriodTo, InputFixedPeriodFrom  , InputFixedPeriodTo;

            //DateTime TargetMasterFromDate, TargetMasterToDate;
            //DateTime TargetFixedPeriodFrom, TargetFixedPeriodTo;

            OrgMasterFromDate = Convert.ToDateTime(ORG_MASTER_FROM_DATE);
            OrgMasterToDate = Convert.ToDateTime(ORG_MASTER_TO_DATE); 
            InputMasterFromDate = tb_coupon_master.MASTER_FROM_DATE.Value;
            InputMasterToDate = tb_coupon_master.MASTER_TO_DATE.Value;


            //[발행된 쿠폰이 있거나 없거나 공통 START]========================================================================================================
            if (OrgMasterFromDate != InputMasterFromDate) // || OrgMasterToDate != InputMasterToDate)
            {
                if (InputMasterFromDate < DateTime.Now )
                {
                    PostedDataValid = false;
                    @TempData["jsMessage"] = "<script language='javascript'>alert('[정책유효기간 시작일시]를 현재 이전 으로 변경 할 수 없습니다.')</script>";
                }
            }

            if (PostedDataValid == true && OrgMasterToDate != InputMasterToDate) // || OrgMasterToDate != InputMasterToDate)
            {
                if ( InputMasterToDate < DateTime.Now)
                {
                    PostedDataValid = false;
                    @TempData["jsMessage"] = "<script language='javascript'>alert('[정책유효기간 종료일시]를 현재 이전 으로 변경 할 수 없습니다.')</script>";
                }
            }

            if (PostedDataValid == true && SERVICE_LIFE_GBN == "A") // 유효기간 기준이 '고정기간 기준'이면
            {
                OrgFixedPeriodFrom = Convert.ToDateTime(ORG_FIXED_PERIOD_FROM);
                OrgFixedPeriodTo = Convert.ToDateTime(ORG_FIXED_PERIOD_TO);
                InputFixedPeriodFrom = tb_coupon_master.FIXED_PERIOD_FROM.Value;
                InputFixedPeriodTo = tb_coupon_master.FIXED_PERIOD_TO.Value;
                //정책유효기간은 고정기간보다 넓은 범위내에서만 변경할 수 있다
                if (InputFixedPeriodFrom < InputMasterFromDate) //고정기간의 시작일자가 정책유효기간보다 먼저이면 에러
                {
                    PostedDataValid = false;
                    @TempData["jsMessage"] = "<script language='javascript'>alert('고정기간의 시작일자는 정책유효기간 시작일자보다 나중이어야 합니다.')</script>";
                }
                else if (InputFixedPeriodTo > InputMasterToDate)
                {
                    PostedDataValid = false;
                    @TempData["jsMessage"] = "<script language='javascript'>alert('고정기간의 종료일자는 정책유효기간 이내 이어야 합니다.')</script>";
                }
            }

            //[발행된 쿠폰이 있거나 없거나 END]==========================================================================================================


            if (PostedDataValid == true && TotalRecord > 0) //[이미 발행된 쿠폰이 있으면 START]======================================================================================
            {
                if(OrgMasterFromDate != InputMasterFromDate)
                {
                    PostedDataValid = false;
                    @TempData["jsMessage"] = "<script language='javascript'>alert('발행된 쿠폰이 있으면 [정책유효기간 시작일시]를 변경 할 수 없습니다.')</script>";
                }else if (OrgMasterToDate > InputMasterToDate) 
                {
                    PostedDataValid = false;
                    @TempData["jsMessage"] = "<script language='javascript'>alert('발행된 쿠폰이 있으면 [정책유효기간 종료일시]는 연장만 가능합니다.')</script>";
                }


                //[2]고정기간 변경 검증 ======================================================================
             
                else if (SERVICE_LIFE_GBN == "A") // 유효기간 기준이 '고정기간 기준'이면
                {
                    OrgFixedPeriodFrom = Convert.ToDateTime(ORG_FIXED_PERIOD_FROM);
                    OrgFixedPeriodTo = Convert.ToDateTime(ORG_FIXED_PERIOD_TO);
                    InputFixedPeriodFrom = tb_coupon_master.FIXED_PERIOD_FROM.Value;
                    InputFixedPeriodTo = tb_coupon_master.FIXED_PERIOD_TO.Value;

                    if (OrgFixedPeriodFrom != InputFixedPeriodFrom ) //고정기간 시작일자가 변경되었으면 
                    {
                         PostedDataValid = false;
                         @TempData["jsMessage"] = "<script language='javascript'>alert('발행된 쿠폰이 있으면 고정기간의 시작일시를 변경 할 수 없습니다.')</script>";
                    }
                    else if (InputFixedPeriodFrom < OrgFixedPeriodFrom)
                    {
                        PostedDataValid = false;
                        @TempData["jsMessage"] = "<script language='javascript'>alert('발행된 쿠폰이 있으면 고정기간의 만료일시를 연장만 할 수 있습니다.')</script>";
                    }      
                }
            } //발행된 쿠폰이 있으면 [END]======================================================================================================
       

           

            
            int is_success = 1;
       

            //if (ModelState.IsValid)
            //{

                if (PostedDataValid == true)
                {
                    is_success = _AdminCouponService.UpdateAdminCouponMaster(tb_coupon_master, CheckMemGrade,CdCoupon);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Update", new { CdCoupon = CdCoupon });
                }
            //}


                return RedirectToAction("Update", new { CdCoupon = CdCoupon });

            //return RedirectToAction("Index", new { CdPromotionTotal = _CdPromotionTotal });

        }



        [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult CouponProductList(string SearchCol = "", string SearchKeyword = "", string SortCol = "IDX"
            , string SortDir = "DESC", int Page = 1, int PageSize = 10 , string CdCoupon = "0")
        {


            this.ViewBag.PageSize = PageSize;
            this.ViewBag.SearchCol = SearchCol;
            this.ViewBag.SearchKeyword = SearchKeyword;
            this.ViewBag.SortCol = SortCol;
            this.ViewBag.SortDir = SortDir;



            //쿠폰마스터 정보를 ViewData에 저장 
            List<SP_ADMIN_COUPON_MASTER_DETAIL_SEL_Result> master_lst = _AdminCouponService.GetAdminCouponDetail(CdCoupon).ToList();
            ViewData["SP_ADMIN_COUPON_MASTER_DETAIL_SEL_Result"] = master_lst;


            //AdminMemberService srv =  new AdminMemberService();
            int TotalRecord = 0;
            TotalRecord = _AdminCouponService.GetAdminCouponProductListCnt(SearchCol, SearchKeyword,CdCoupon);
            this.ViewBag.TotalRecord = TotalRecord;
            //this.ViewBag.MaxPage = (int)Math.Ceiling((double)count / page_size); //올림
            this.ViewBag.Page = Page;



            var mMyMultiModelForProduct = new MyMultiModelForProduct
            {
                //inst_TB_COUPON_PRODUCT = new TB_COUPON_PRODUCT() 
                inst_SP_ADMIN_COUPON_PRODUCT_DETAIL_SEL_Result = new List<SP_ADMIN_COUPON_PRODUCT_DEATIL_SEL_Result>()
                //inst_TB_PROMOTION_BY_TOTAL_MEM_GRADE = new TB_PROMOTION_BY_TOTAL_MEM_GRADE()
            };
            
            mMyMultiModelForProduct.inst_SP_ADMIN_COUPON_PRODUCT_DETAIL_SEL_Result = _AdminCouponService.GetAdminCouponProeuctList(SearchCol, SearchKeyword, Page, PageSize, CdCoupon).ToList();

            return View(mMyMultiModelForProduct);

        }

        //CouponProductCreate

        //쿠폰 적용할 대상상품 리스트 -- 생성페이지
        [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult CouponProductCreate(string SearchCol = "", string SearchKeyword = "", string SortCol = "IDX"
        , string SortDir = "DESC", int Page = 1, int PageSize = 100, string CdCoupon = "0")
        {


            this.ViewBag.PageSize = PageSize;
            this.ViewBag.SearchCol = SearchCol;
            this.ViewBag.SearchKeyword = SearchKeyword;
            this.ViewBag.SortCol = SortCol;
            this.ViewBag.SortDir = SortDir;


            //쿠폰마스터 정보를 ViewData에 저장 
            List<SP_ADMIN_COUPON_MASTER_DETAIL_SEL_Result> master_lst = _AdminCouponService.GetAdminCouponDetail(CdCoupon).ToList();
            ViewData["SP_ADMIN_COUPON_MASTER_DETAIL_SEL_Result"] = master_lst;


            //AdminMemberService srv =  new AdminMemberService();
            int TotalRecord = 0;
            TotalRecord = _AdminCouponService.GetAdminCouponProductForCreateListCnt(SearchCol, SearchKeyword, CdCoupon);
            this.ViewBag.TotalRecord = TotalRecord;
            //this.ViewBag.MaxPage = (int)Math.Ceiling((double)count / page_size); //올림
            this.ViewBag.Page = Page;



            var mMyMultiModelForProductForCreate = new MyMultiModelForProductForCreate
            {
                //inst_TB_COUPON_PRODUCT = new TB_COUPON_PRODUCT() 
                inst_SP_ADMIN_COUPON_PRODUCT_FOR_CREATE_DETAIL_SEL_Result= new List<SP_ADMIN_COUPON_PRODUCT_FOR_CREATE_DETAIL_SEL_Result>()
                //inst_TB_PROMOTION_BY_TOTAL_MEM_GRADE = new TB_PROMOTION_BY_TOTAL_MEM_GRADE()
            };

            mMyMultiModelForProductForCreate.inst_SP_ADMIN_COUPON_PRODUCT_FOR_CREATE_DETAIL_SEL_Result 
                = _AdminCouponService.GetAdminCouponProductForCreateList(SearchCol, SearchKeyword, Page, 100, CdCoupon).ToList();

            return View(mMyMultiModelForProductForCreate);

        }




        //쿠폰 적용할 대상상품 리스트 -- 생성페이지


        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult CouponProductCreateProc(string CdCoupon, string[] Check_ins_cd_coupon)
        {


           // string[] CdCouponVsPcodeForIns = new string[CdCouponVsPCode.Length];

            List<Tuple<string,string>> ListCdCouponVsPcodeForIns = new List<Tuple<string,string>>() ; 

            string[] temp;
            string PCode = "";


            for (int i = 0; i < Check_ins_cd_coupon.Length; i++)
            {
                temp = Check_ins_cd_coupon[i].Split('$');
                PCode = temp[1] ;

                ListCdCouponVsPcodeForIns.Add(Tuple.Create(CdCoupon, PCode));
            }


            int IsSuccess = _AdminCouponService.InsAdminCouponProduct(ListCdCouponVsPcodeForIns);

            if(IsSuccess == 1)
            {
                return RedirectToAction("CouponProductList", new { CdCoupon = CdCoupon });
            }
            /**
            var mMyMultiModelForProductForCreate = new MyMultiModelForProductForCreate
            {
                //inst_TB_COUPON_PRODUCT = new TB_COUPON_PRODUCT() 
                inst_SP_ADMIN_COUPON_PRODUCT_FOR_CREATE_DETAIL_SEL_Result = new List<SP_ADMIN_COUPON_PRODUCT_FOR_CREATE_DETAIL_SEL_Result>()
                //inst_TB_PROMOTION_BY_TOTAL_MEM_GRADE = new TB_PROMOTION_BY_TOTAL_MEM_GRADE()
            };

            mMyMultiModelForProductForCreate.inst_SP_ADMIN_COUPON_PRODUCT_FOR_CREATE_DETAIL_SEL_Result
                = _AdminCouponService.GetAdminCouponProductForCreateList(SearchCol, SearchKeyword, Page, PageSize, CdCoupon).ToList();

            return View(mMyMultiModelForProductForCreate);
            **/

            return RedirectToAction("CouponProductListCreate", new { CdCoupon = CdCoupon });
        }


        //발행된 쿠폰리스트 
        [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult IssuedCouponList(string SearchCol = "", string SearchKeyword = "", string SortCol = "IDX"
            , string SortDir = "DESC", int Page = 1, int PageSize = 10 , string CdCoupon = "0")
        {


            this.ViewBag.PageSize = PageSize;
            this.ViewBag.SearchCol = SearchCol;
            this.ViewBag.SearchKeyword = SearchKeyword;
            this.ViewBag.SortCol = SortCol;
            this.ViewBag.SortDir = SortDir;


            //쿠폰마스터 정보를 ViewData에 저장 
            List<SP_ADMIN_COUPON_MASTER_DETAIL_SEL_Result> master_lst = _AdminCouponService.GetAdminCouponDetail(CdCoupon).ToList();
            ViewData["SP_ADMIN_COUPON_MASTER_DETAIL_SEL_Result"] = master_lst;


            //발행된 쿠폰의 COUNT 
            int TotalRecord = 0;
            TotalRecord = _AdminCouponService.GetAdminCouponIssuedListCnt(SearchCol, SearchKeyword,CdCoupon);
            this.ViewBag.TotalRecord = TotalRecord;
            //this.ViewBag.MaxPage = (int)Math.Ceiling((double)count / page_size); //올림
            this.ViewBag.Page = Page;


            //발행된 쿠폰리스트
            List<SP_ADMIN_COUPON_ISSUED_DETAIL_SEL_Result> sP_ADMIN_COUPON_ISSUED_DETAIL_SEL_Result = new List<SP_ADMIN_COUPON_ISSUED_DETAIL_SEL_Result>();
            sP_ADMIN_COUPON_ISSUED_DETAIL_SEL_Result = _AdminCouponService.GetAdminCouponIssuedList(SearchCol, SearchKeyword, Page, PageSize, CdCoupon).ToList();

            return View(sP_ADMIN_COUPON_ISSUED_DETAIL_SEL_Result);

        }


        //IssueExcute
        [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult IssueExcute( string CdCoupon)
        {

            //쿠폰마스터 정보를 ViewData에 저장 
            List<SP_ADMIN_COUPON_MASTER_DETAIL_SEL_Result> master_lst = _AdminCouponService.GetAdminCouponDetail(CdCoupon).ToList();
            ViewData["SP_ADMIN_COUPON_MASTER_DETAIL_SEL_Result"] = master_lst;


            //쿠폰발행가능 회원등급 리스트를 ViewData에 저장 
            List<SP_ADMIN_COUPON_MEMBER_GRADE_SEL_Result> grade_lst = _AdminCouponService.GetAdminCouponMemberGradeList(CdCoupon).ToList();
            ViewData["SP_ADMIN_COUPON_MEMBER_GRADE_SEL_Result"] = grade_lst;

            //쿠폰발행가능 상품카운트를  ViewBag에 저장
            int cnt  = _AdminCouponService.GetAdminCouponProductUsableCnt(CdCoupon);
            ViewBag.ProductCnt = cnt.ToString();


            return View();

        }


        
        //쿠폰 전체 대상 발행 / 번호 인증이 필요없는 쿠폰 / 일괄 발행 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult IssueExcuteEntire( string CdCoupon)
        {

            int ResultCode;
            string AdminId = "TestAdminID000";

            ResultCode = _AdminCouponService.InsAdminCouponIssue_WithNoNumcheck_ManualEntire(CdCoupon, AdminId);

            ViewBag.ResultCode = ResultCode.ToString();

            if (ResultCode == 0) // 정상수행
                return RedirectToAction("IssuedCouponList", new { CdCoupon = CdCoupon });
            else
                return RedirectToAction("IssueExcute", new { CdCoupon = CdCoupon });

        }


        //쿠폰발행 - 지불쿠폰 OR 배송쿠폰/인증번호 필요없는 쿠폰/수동발행/개별발행 INSERT(admin) 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult IssueExcuteSingle(string CdCoupon, [Bind(Include = "COUPON_MONEY,COUPON_DISCOUNT_RATE,M_ID,ISSUED_MEMO")]  TB_COUPON_ISSUED_DETAIL tb)
        {

            int ResultCode;
            string AdminId = "TestAdminID000";

            if (tb.COUPON_DISCOUNT_RATE == null)
                tb.COUPON_DISCOUNT_RATE = 0;

            if (tb.COUPON_MONEY == null)
                tb.COUPON_MONEY = 0;

            ResultCode = _AdminCouponService.InsAdminCouponIssue_WithNoNumcheck_Manual_Single(CdCoupon,tb.COUPON_MONEY.Value,tb.COUPON_DISCOUNT_RATE.Value,tb.M_ID, AdminId,tb.ISSUED_MEMO);

            ViewBag.ResultCode = ResultCode.ToString();

            if (ResultCode == 0) // 정상수행
                return RedirectToAction("IssuedCouponList", new { CdCoupon = CdCoupon });
            else
                return RedirectToAction("IssueExcute", new { CdCoupon = CdCoupon });

        }
        

    }
}