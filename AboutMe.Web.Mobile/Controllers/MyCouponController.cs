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

    }
}