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
using AboutMe.Domain.Service.BizPromotion;

using AboutMe.Domain.Service.Promotion;
using AboutMe.Domain.Entity.AdminPromotion;

namespace AboutMe.Web.Front.Controllers
{
    public class ChildActionPromotionController : BaseFrontController
    {
        private IBizPromotion _BizPromotionService;
        private IPromotionService _PromotionService;

        public ChildActionPromotionController(IBizPromotion bizPromotionService, IPromotionService PromotionService)
        {
            this._BizPromotionService = bizPromotionService;
            this._PromotionService = PromotionService;
        }


        #region 상품 상세의 타임세일 배너
        [ChildActionOnly]
        public ActionResult ProductDetailTimeSale(string P_CODE)
        {
            this.ViewBag.P_CODE = P_CODE; //상품코드
            return View();
        }
        #endregion

        #region 상품 상세의 세트상품 리스트
        [ChildActionOnly]
        public ActionResult ProductDetailSetList(string P_CODE, string CD_PROMOTION_PRODUCT)
        {
            this.ViewBag.P_CODE = P_CODE; //상품코드
            ViewBag.PRODUCT_PATH = AboutMe.Common.Helper.Config.GetConfigValue("ProductPhotoPath"); //이미지디렉토리경로

            List<SP_PROMOTION_BY_PRODUCT_SET_RELATED_SEL_Result> lst = _PromotionService.GetPromotionByProduct_SET_RelatedList(CD_PROMOTION_PRODUCT, P_CODE).ToList();


            return View(lst);
        }
        #endregion

        #region 상품 상세의 임직원가, 쿠폰, 포인트
        [ChildActionOnly]
        public ActionResult ProductDetailSalePrice(string UsableDeviceGbn, string P_CODE, int ResultPrice)
        {
            string M_id = "", MGrade = "", MGbn = "";

            this.ViewBag.M_id = M_id = MemberInfo.GetMemberId();
            this.ViewBag.MGrade = MGrade = MemberInfo.GetMemberGrade();
            this.ViewBag.MGbn = MGbn = MemberInfo.GetMemberGBN();


            ///////////////////////////////////////////////////////////////////////////////////////////////
            //
            //
            // 등급을 VIP 로 강제지정
            MGrade = "V"; //주의!!!!!!!!!!!!!!!! 등급이 VIP가 아니더라도 무조건 VIP할인가가 있으면 보여주기로 한다
            //
            //
            ///////////////////////////////////////////////////////////////////////////////////////////////

            Dictionary<string, string> dict =
             _BizPromotionService.GetPromotionInfoForDetialPage(UsableDeviceGbn,MGbn, MGrade, M_id, P_CODE, ResultPrice);


            //로그인 안했을 때 ======================================================================================

            //--즉시할인(쿠폰)
            ViewBag.NoLogin_Coupon_RateOrMoney = dict["NoLogin_Coupon_RateOrMoney"]; // 로긴 안했을때 - 쿠폰 할인방법 - 정률:R / 정액 :M
            ViewBag.NoLogin_Coupon_Discount_Rate = dict["NoLogin_Coupon_Discount_Rate"];// 로긴 안했을때 - 쿠폰 할인율(정률할인시)
            ViewBag.NoLogin_Coupon_Discount_Money = dict["NoLogin_Coupon_Discount_Money"];// 로긴 안했을때 - 쿠폰 할인액(정액할인시)
           
            //--포인트
            ViewBag.NoLogin_Point = dict["NoLogin_Point"];// 로긴 안했을때 - 포인트 적립액



            //로그인 했을 때 ========================================================================================
       
            //--임직원가 , VIP할인가 구현 
            ViewBag.Login_Promotion_00 = dict["Login_Promotion_00"]; //로긴시 - 임직원할인 유무 -- 있으면 '00'
            ViewBag.Login_Promotion_03 = dict["Login_Promotion_03"];  //로긴시 - 등급할인 유무 -- 있으면 등급   B=브론즈,S=실버,G=골드,V=VIP , 없으면 공백

                  
            ViewBag.Login_Promotion_00_result_price = dict["Login_Promotion_00_result_price"]; //로긴시 - 임직원할인가 적용가 
            ViewBag.Login_Promotion_03_result_price = dict["Login_Promotion_03_result_price"]; //로긴시 - 임직원할인가에서 등급할인까지 적용된 가격 

            //--즉시할인(쿠폰)
            ViewBag.Login_Coupon_RateOrMoney = dict["Login_Coupon_RateOrMoney"]; //로긴시 - 쿠폰 할인방법 - 정률:R / 정액 :M
            ViewBag.Login_Coupon_Discount_Rate = dict["Login_Coupon_Discount_Rate"]; // 로긴시 - 쿠폰 할인율(정률할인시)
            ViewBag.Login_Coupon_Discount_Money = dict["Login_Coupon_Discount_Money"]; // 로긴시 - 쿠폰 할인액(정액할인시)
            ViewBag.Login_Coupon_Idx_Coupon_Number = dict["Login_Coupon_Idx_Coupon_Number"];// 로긴시 - 쿠폰 번호
            ViewBag.Login_Coupon_Name = dict["Login_Coupon_Name"]; //로긴시 = 쿠폰명
            ViewBag.Login_Coupon_USE_STATUS = dict["Login_Coupon_USE_STATUS"]; //발행(다운로드가능) = 1 , 사용가능(인증,다운로드완료)= 5 , 사용완료 = 10  
            ViewBag.Login_Coupon_COUPON_NUM_CHECK_TF = dict["Login_Coupon_COUPON_NUM_CHECK_TF"]; //Y이면 쿠폰번호 인증필요한 쿠폰

            //--포인트
            ViewBag.Login_Point = dict["Login_Point"]; //로긴시 – 해당상품 구매시의 포인트



            
            this.ViewBag.P_CODE = P_CODE; //상품코드
            return View();
        }
        #endregion


        #region  메인페이지의 타임세일배너

        [ChildActionOnly]
        public ActionResult MainPgTimeSaleBanner()
        {
            ViewBag.PromotionPhotoPath = AboutMe.Common.Helper.Config.GetConfigValue("PromotionPhotoPath"); //이미지디렉토리경로

            List<SP_PROMOTION_BY_PRODUCT_TOP_1_DETAIL_SEL_Result> lst = new List<SP_PROMOTION_BY_PRODUCT_TOP_1_DETAIL_SEL_Result>();
            //타임세일 프로모션 정보중 유효한 TOP 1 가져오기 
            lst = _PromotionService.GetPromotionByProductTop1_Info("03").ToList();

  
            return View(lst);
        }

        #endregion 


        
        #region  이벤트페이지의 타임세일배너

        [ChildActionOnly]
        public ActionResult EventPgTimeSaleBanner()
        {
            ViewBag.PromotionPhotoPath = AboutMe.Common.Helper.Config.GetConfigValue("PromotionPhotoPath"); //이미지디렉토리경로

            List<SP_PROMOTION_BY_PRODUCT_TOP_1_DETAIL_SEL_Result> lst = new List<SP_PROMOTION_BY_PRODUCT_TOP_1_DETAIL_SEL_Result>();

            //타임세일 프로모션 정보중 유효한 TOP 1 가져오기 
            lst = _PromotionService.GetPromotionByProductTop1_Info("03").ToList();

            return View(lst);
        }

        #endregion 



    }
}