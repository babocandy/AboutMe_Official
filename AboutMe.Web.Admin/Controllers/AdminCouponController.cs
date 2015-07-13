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
        public ActionResult Create([Bind(Prefix = "inst_TB_COUPON_MASTER", Exclude = "CD_COUPON,INS_DATE")]  TB_COUPON_MASTER tb_coupon_master, string[] CheckMemGrade)
        {


            int is_success = 1;


            if (ModelState.IsValid)
            {
                _AdminCouponService.InsAdminCouponMaster(tb_coupon_master, CheckMemGrade);
            }

            return View();

        }

    }
}