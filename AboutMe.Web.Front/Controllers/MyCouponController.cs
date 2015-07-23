using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Common.Helper;
using AboutMe.Domain.Service.Coupon;
using AboutMe.Domain.Entity.AdminCoupon;


using AboutMe.Web.Front.Common.Filters;

namespace AboutMe.Web.Front.Controllers
{
    [RoutePrefix("MyPage/MyCoupon")]
    [Route("{action=Downloadable}")]
    public class MyCouponController  : BaseFrontController
    {
        private ICouponService _CouponService;

        public MyCouponController(CouponService _CouponService)
        {
            this._CouponService = _CouponService;
        }

        protected override void HandleUnknownAction(string actionName)
        {
            this.View(actionName).ExecuteResult(this.ControllerContext);
        }


        // GET: MyCoupon
        [CustomAuthorize]
        public ActionResult Downloadable()
        {
            ViewBag.PRODUCT_PATH = AboutMe.Common.Helper.Config.GetConfigValue("ProductPhotoPath"); //이미지디렉토리경로

    
            string M_Id = ""; //회원아이디

            //M_Id = "aszx0505";
            M_Id = _user_profile.M_ID.ToString();

            //세트상품 프로모션 정보중 유효한 TOP 1 가져오기 
            List<SP_COUPON_DOWNLOADABLE_LIST_Result> lst = _CouponService.GetDownloadableCouponList(M_Id).ToList();

            return View(lst);

          
        }



        [CustomAuthorize(CustomRedirection="/MyPage/MyCoupon/Downloadable")]
        public ActionResult UpdateToUsable(int IdxCouponNumber = 0 , string UpdateMethod = "")
        {
            
            string M_Id = ""; //회원아이디
            int IsSuccess =1;

            //M_Id = "aszx0505";
            M_Id = _user_profile.M_ID.ToString();
            //세트상품 프로모션 정보중 유효한 TOP 1 가져오기 
            
            /**
            if (IdxCouponNumber == null)
            {
                IdxCouponNumber = 0;
            }
             * **/

            IsSuccess = _CouponService.UpdateCouponDownload_Pc_Ver_And_NoNumberChk_Ver(M_Id, IdxCouponNumber, UpdateMethod);

            return  RedirectToAction("Availablelist", "MyCoupon");
            //return View(lst);
        }

        [CustomAuthorize()]
        public ActionResult Availablelist(string SearchCol = "", string SearchKeyword = "", string SortCol = "IDX", string SortDir = "DESC", int Page = 1, int PageSize = 10)
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
            TotalRecord = _CouponService.GettCouponAvailableListCnt(M_Id, SearchCol, SearchKeyword);
            this.ViewBag.TotalRecord = TotalRecord;
            //this.ViewBag.MaxPage = (int)Math.Ceiling((double)count / page_size); //올림
            this.ViewBag.Page = Page;

            return View(_CouponService.GetCouponAvailableList(M_Id, SearchCol, SearchKeyword, Page, PageSize).ToList());
          
        }

   
        public ActionResult CouponProductPop(int IdxCouponNumber, string SearchCol = "", string SearchKeyword = "", string SortCol = "IDX", string SortDir = "DESC", int Page = 1, int PageSize = 10)
        {


            if (_user_profile.IS_LOGIN == true)
            {
                ViewBag.PRODUCT_PATH = AboutMe.Common.Helper.Config.GetConfigValue("ProductPhotoPath"); //이미지디렉토리경로


                //[1]쿠폰마스터 정보를 ViewData에 저장 
                List<SP_COUPON_MASTER_INFO_SEL_Result> master_lst = _CouponService.GetCouponMasterInfo("", IdxCouponNumber).ToList();
                ViewData["SP_COUPON_MASTER_INFO_SEL_Result"] = master_lst;


                //[2]쿠폰 상품정보 가져오기

                this.ViewBag.PageSize = PageSize;
                this.ViewBag.SearchCol = SearchCol;
                this.ViewBag.SearchKeyword = SearchKeyword;
                this.ViewBag.SortCol = SortCol;
                this.ViewBag.SortDir = SortDir;



                string M_Id = ""; //회원아이디

                //M_Id = "aszx0505";
                M_Id = _user_profile.M_ID.ToString(); //로그인정보에서 회원아이디 가져오기 


                int TotalRecord = 0;
                TotalRecord = _CouponService.GetCouponProductListCnt(M_Id, IdxCouponNumber, SearchCol, SearchKeyword);
                this.ViewBag.TotalRecord = TotalRecord;
                //this.ViewBag.MaxPage = (int)Math.Ceiling((double)count / page_size); //올림
                this.ViewBag.Page = Page;

                return View(_CouponService.GetCouponProductList(M_Id, IdxCouponNumber, SearchCol, SearchKeyword, Page, PageSize).ToList());
            }
            else 
            {
                 return View(); //alert 창 띄우고 자동 창닫기 처리 
            }


        }

    }
}