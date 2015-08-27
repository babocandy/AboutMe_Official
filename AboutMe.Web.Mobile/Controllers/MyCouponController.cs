using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Common.Helper;
using AboutMe.Domain.Service.Coupon;
using AboutMe.Domain.Entity.AdminCoupon;


using AboutMe.Web.Mobile.Common.Filters;

namespace AboutMe.Web.Mobile.Controllers
{
    [RoutePrefix("MyPage/MyCoupon")]
    [Route("{action=Downloadable}")]
    public class MyCouponController : BaseMobileController
    {
        // GET: MyCoupon
        public ActionResult Index()
        {
            return View();
        }

        private ICouponService _CouponService;

        public MyCouponController(CouponService _CouponService)
        {
            this._CouponService = _CouponService;
        }

        protected override void HandleUnknownAction(string actionName)
        {
            this.View(actionName).ExecuteResult(this.ControllerContext);
        }

        //상품상세페이지 등에서 쿠폰다운로드 버튼을 클릭했을때 , 해당쿠폰을 다운로드 처리 // 모바일전용버전+모바일-pc혼용버전, 번호인증 필요없는 쿠폰 
        [CustomAuthorize]
        [HttpPost]
        public JsonResult UpdateToUsableWithAjax(int IdxCouponNumber = 0, string UpdateMethod = "")
        {
            int _ResultCode = 1;
            string _Msg = "";

            string M_Id = ""; //회원아이디
            //M_Id = "aszx0505";
            M_Id = _user_profile.M_ID.ToString();

            _ResultCode = _CouponService.UpdateCouponDownload_Mobile_Ver_And_NoNumberChk_Ver(M_Id, IdxCouponNumber, UpdateMethod);


            switch (_ResultCode)
            {
                case 1:
                    _Msg = "다운로드 되었습니다.";
                    break;
                case -1:
                    _Msg = "실행과정에서 오류가 발행하였습니다.";
                    break;
                case -2:
                    _Msg = "번호인증이 필요한 쿠폰이라 다운로드 받을 수 없습니다";
                    break;
                case -3:
                    _Msg = "존재하지 않는 쿠폰입니다";
                    break;
                default:
                    _Msg = "시스템 오류";
                    break;
            }

            var jsonData = new { ResultCode = _ResultCode, Msg = _Msg };
            return Json(jsonData, JsonRequestBehavior.AllowGet);

            //var jsonData = new { Total = model.Total, Reviews = model.Reviews, Success = true, Postdata = new { TAIL_IDX = param.TAIL_IDX, CATEGORY_CODE = param.CATEGORY_CODE, SORT = param.SORT } 

            //return View(lst);
        }



        // 다운로드가능한 쿠폰리스트가져오기 
        [CustomAuthorize]
        public ActionResult Downloadable(string SearchCol = "", string SearchKeyword = "", int Page = 1, int PageSize = 10)
        {
            this.ViewBag.PageSize = PageSize;
            this.ViewBag.SearchCol = SearchCol;
            this.ViewBag.SearchKeyword = SearchKeyword;

            string M_Id = ""; //회원아이디
            int TotalRecord = 0;

            //M_Id = "aszx0505";
            M_Id = _user_profile.M_ID.ToString();

            TotalRecord = _CouponService.GettCouponAvailableListCnt(M_Id, SearchCol, SearchKeyword);
            
            this.ViewBag.TotalRecord = TotalRecord;
            this.ViewBag.Page = Page;

            //다운로드 가능한 쿠폰리스트
            List<SP_COUPON_DOWNLOADABLE_LIST_Result> lst = _CouponService.GetDownloadableCouponMobileList(M_Id,SearchCol,SearchKeyword,Page,PageSize).ToList();

            return View(lst);

        }


        //쿠폰 다운로드 버튼을 클릭했을 때 다운로드 처리 // PC버전, 번호인증 필요없는 쿠폰 
        [CustomAuthorize(CustomRedirection = "/MyPage/MyCoupon/Downloadable")]
        public ActionResult UpdateToUsable(int IdxCouponNumber = 0, string UpdateMethod = "")
        {

            string M_Id = ""; //회원아이디
            int IsSuccess = 1;

            //M_Id = "aszx0505";
            M_Id = _user_profile.M_ID.ToString();
            //세트상품 프로모션 정보중 유효한 TOP 1 가져오기 

            /**
            if (IdxCouponNumber == null)
            {
                IdxCouponNumber = 0;
            }
             * **/

            IsSuccess = _CouponService.UpdateCouponDownload_Mobile_Ver_And_NoNumberChk_Ver(M_Id, IdxCouponNumber, UpdateMethod);

            return RedirectToAction("Availablelist", "MyCoupon");
            //return View(lst);
        }


        [CustomAuthorize]
        public ActionResult Availablelist(string SearchCol = "", string SearchKeyword = "", int Page = 1, int PageSize = 10)
        {


            this.ViewBag.PageSize = PageSize;
            this.ViewBag.SearchCol = SearchCol;
            this.ViewBag.SearchKeyword = SearchKeyword;
            //this.ViewBag.SortCol = SortCol;
           // this.ViewBag.SortDir = SortDir;



            string M_Id = ""; //회원아이디

            //M_Id = "aszx0505";
            M_Id = _user_profile.M_ID.ToString(); //로그인정보에서 회원아이디 가져오기 


            int TotalRecord = 0;
            TotalRecord = _CouponService.GettCouponAvailableOnMobileListCnt(M_Id, SearchCol, SearchKeyword);
            this.ViewBag.TotalRecord = TotalRecord;
            //this.ViewBag.MaxPage = (int)Math.Ceiling((double)count / page_size); //올림
            this.ViewBag.Page = Page;

            return View(_CouponService.GetCouponAvailableOnMobileList(M_Id, SearchCol, SearchKeyword, Page, PageSize).ToList());

        }


        //종료 혹은 사용완료된 쿠폰 리스트 
        [CustomAuthorize]
        public ActionResult ClosedCouponlist(string SearchCol = "", string SearchKeyword = "", string SortCol = "IDX", string SortDir = "DESC", int Page = 1, int PageSize = 10)
        {


            this.ViewBag.PageSize = PageSize;
            this.ViewBag.SearchCol = SearchCol;
            this.ViewBag.SearchKeyword = SearchKeyword;
            this.ViewBag.SortCol = SortCol;
            this.ViewBag.SortDir = SortDir;



            string M_Id = ""; //회원아이디

            //M_Id = "aszx0505";
            M_Id = _user_profile.M_ID.ToString(); //로그인정보에서 회원아이디 가져오기 


            int TotalRecord = 0;
            TotalRecord = _CouponService.GetCouponClosedListCnt(M_Id, "M", SearchCol, SearchKeyword);
            this.ViewBag.TotalRecord = TotalRecord;
            //this.ViewBag.MaxPage = (int)Math.Ceiling((double)count / page_size); //올림
            this.ViewBag.Page = Page;

            return View(_CouponService.GetCouponClosedList(M_Id, "M", SearchCol, SearchKeyword, Page, PageSize).ToList());

        }



    }
}