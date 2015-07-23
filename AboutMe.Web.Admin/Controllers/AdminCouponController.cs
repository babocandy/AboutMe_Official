using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Common.Helper;
using AboutMe.Domain.Service.AdminCoupon;
using AboutMe.Domain.Entity.AdminCoupon;


namespace AboutMe.Web.Admin.Controllers
{
    public class AdminCouponController : BaseAdminController
    {
        
        private IAdminCouponService _AdminCouponService;

        public AdminCouponController(IAdminCouponService _adminCouponService)
        {
            this._AdminCouponService = _adminCouponService;
        }

        protected override void HandleUnknownAction(string actionName)
        {
            this.View(actionName).ExecuteResult(this.ControllerContext);
        }



        public class MyMultiModelForCreate
        {
            public TB_COUPON_MASTER inst_TB_COUPON_MASTER { get; set; }
            public List<SP_ADMIN_COUPON_MASTER_DETAIL_SEL_Result> inst_SP_ADMIN_COUPON_MASTER_DETAIL_SEL_Result { get; set; }
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
        public ActionResult Index(string SearchCol = "", string SearchKeyword = "", string SortCol = "IDX", string SortDir = "DESC", int Page = 1, int PageSize = 10)
        {


            this.ViewBag.PageSize = PageSize;
            this.ViewBag.SearchCol = SearchCol;
            this.ViewBag.SearchKeyword = SearchKeyword;
            this.ViewBag.SortCol = SortCol;
            this.ViewBag.SortDir = SortDir;

            //AdminMemberService srv =  new AdminMemberService();
            int TotalRecord = 0;
            TotalRecord = _AdminCouponService.GetAdminCouponListCnt(SearchCol, SearchKeyword);
            this.ViewBag.TotalRecord = TotalRecord;
            //this.ViewBag.MaxPage = (int)Math.Ceiling((double)count / page_size); //올림
            this.ViewBag.Page = Page;

            return View(_AdminCouponService.GetAdminCouponList(SearchCol, SearchKeyword, Page, PageSize).ToList());
        }


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
        //public ActionResult Create([Bind(Include = "ADM_ID,ADM_PASS,ADM_NAME,ADM_DEPT,POINT")] MyMultiModelForCreate.inst_TB_PROMOTION_BY_TOTAL  , string[] CheckMemGrade)
        public ActionResult Create([Bind(Prefix = "inst_TB_COUPON_MASTER", Exclude = "IDX,CD_COUPON,COUPON_NUM_CHECK_TF,ISSUE_MAX_LIMIT,INS_DATE")]  TB_COUPON_MASTER tb_coupon_master, string[] CheckMemGrade)
        {


            int is_success = 1;

            if (ModelState.IsValid)
            {
                is_success= _AdminCouponService.InsAdminCouponMaster(tb_coupon_master, CheckMemGrade);

                return RedirectToAction("Index");
            }

            return View();
            
            //return RedirectToAction("Index", new { CdPromotionTotal = _CdPromotionTotal });

        }




        public ActionResult CouponProductList(string SearchCol = "", string SearchKeyword = "", string SortCol = "IDX"
            , string SortDir = "DESC", int Page = 1, int PageSize = 10 , string CdCoupon = "0")
        {


            this.ViewBag.PageSize = PageSize;
            this.ViewBag.SearchCol = SearchCol;
            this.ViewBag.SearchKeyword = SearchKeyword;
            this.ViewBag.SortCol = SortCol;
            this.ViewBag.SortDir = SortDir;



            //쿠폰마스터 정보를 ViewData에 저장 
            List<SP_ADMIN_COUPON_MASTER_DETAIL_SEL_Result> master_lst = _AdminCouponService.GetAdminCouponList(CdCoupon).ToList();
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
        public ActionResult CouponProductCreate(string SearchCol = "", string SearchKeyword = "", string SortCol = "IDX"
        , string SortDir = "DESC", int Page = 1, int PageSize = 10, string CdCoupon = "0")
        {


            this.ViewBag.PageSize = PageSize;
            this.ViewBag.SearchCol = SearchCol;
            this.ViewBag.SearchKeyword = SearchKeyword;
            this.ViewBag.SortCol = SortCol;
            this.ViewBag.SortDir = SortDir;


            //쿠폰마스터 정보를 ViewData에 저장 
            List<SP_ADMIN_COUPON_MASTER_DETAIL_SEL_Result> master_lst = _AdminCouponService.GetAdminCouponList(CdCoupon).ToList();
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
                = _AdminCouponService.GetAdminCouponProductForCreateList(SearchCol, SearchKeyword, Page, PageSize, CdCoupon).ToList();

            return View(mMyMultiModelForProductForCreate);

        }




        //쿠폰 적용할 대상상품 리스트 -- 생성페이지


        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public ActionResult IssuedCouponList(string SearchCol = "", string SearchKeyword = "", string SortCol = "IDX"
            , string SortDir = "DESC", int Page = 1, int PageSize = 10 , string CdCoupon = "0")
        {


            this.ViewBag.PageSize = PageSize;
            this.ViewBag.SearchCol = SearchCol;
            this.ViewBag.SearchKeyword = SearchKeyword;
            this.ViewBag.SortCol = SortCol;
            this.ViewBag.SortDir = SortDir;


            //쿠폰마스터 정보를 ViewData에 저장 
            List<SP_ADMIN_COUPON_MASTER_DETAIL_SEL_Result> master_lst = _AdminCouponService.GetAdminCouponList(CdCoupon).ToList();
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

        public ActionResult IssueExcute( string CdCoupon)
        {

            //쿠폰마스터 정보를 ViewData에 저장 
            List<SP_ADMIN_COUPON_MASTER_DETAIL_SEL_Result> master_lst = _AdminCouponService.GetAdminCouponList(CdCoupon).ToList();
            ViewData["SP_ADMIN_COUPON_MASTER_DETAIL_SEL_Result"] = master_lst;


            //쿠폰발행가능 회원등급 리스트를 ViewData에 저장 
            List<SP_ADMIN_COUPON_MEMBER_GRADE_SEL_Result> grade_lst = _AdminCouponService.GetAdminCouponMemberGradeList(CdCoupon).ToList();
            ViewData["SP_ADMIN_COUPON_MEMBER_GRADE_SEL_Result"] = grade_lst;

            //쿠폰발행가능 상품카운트를  ViewBag에 저장
            int cnt  = _AdminCouponService.GetAdminCouponProductUsableCnt(CdCoupon);
            ViewBag.ProductCnt = cnt.ToString();


            return View();

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ADM_ID,ADM_PASS,ADM_NAME,ADM_DEPT,POINT")] MyMultiModelForCreate.inst_TB_PROMOTION_BY_TOTAL  , string[] CheckMemGrade)

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
        

    }
}