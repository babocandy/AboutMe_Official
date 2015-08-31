using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Common.Helper;
using AboutMe.Web.Front.Common;

using AboutMe.Domain.Service.Order;
using AboutMe.Domain.Entity.Order;

using System.Text;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

using AboutMe.Web.Front.Common.Filters;

using INIpayNet;
using Newtonsoft.Json;

namespace AboutMe.Web.Front.Controllers
{
    [RoutePrefix("MyPage/MyOrder")]
    [Route("{action=index}")]
    [NoMemberAuthorize]
    public class MyOrderController : BaseFrontController
    {
        private IOrderService _orderservice;

        public MyOrderController(IOrderService _orderservice)
        {
            this._orderservice = _orderservice;
        }

        private string _inipay_setpath = AboutMe.Common.Helper.Config.GetConfigValue("INIpay.SetPath");
        private string _inipay_mid = AboutMe.Common.Helper.Config.GetConfigValue("INIpay.Mid");
        private string _inipay_admin = AboutMe.Common.Helper.Config.GetConfigValue("INIpay.Admin");

        private string _inipay_mobile_mid = AboutMe.Common.Helper.Config.GetConfigValue("INIpay.Mobile.Mid");
        private string _inipay_mobile_admin = AboutMe.Common.Helper.Config.GetConfigValue("INIpay.Mobile.Admin");
        private string _inipay_debug = AboutMe.Common.Helper.Config.GetConfigValue("INIpay.Debug");

        public ActionResult Index(string FromDate, string ToDate, int? page = 1)
        {
            if (page == null) page = 1;
            if (page <= 0) page = 1;
            int PageSize = 10;
            string OrderCode = _user_profile.NOMEMBER_ORDER_CODE;

            if (_user_profile.IS_LOGIN)
            {
                OrderCode = "";
            }

            MyORDER_INDEX M = new MyORDER_INDEX();
            M.Page = page;
            M.PageSize = PageSize;
            M.UserProfile = _user_profile;
            M.FromDate = FromDate;
            M.ToDate = ToDate;
            M.OrderCnt = _orderservice.MyOrderListCount(OrderCode, _user_profile.M_ID, FromDate, ToDate);
            M.OrderList = _orderservice.MyOrderList(OrderCode, _user_profile.M_ID, FromDate, ToDate, page, PageSize);
            return View(M);
        }

        public ActionResult OrderView(string OrderCode, string FromDate, string ToDate, int? page = 1)
        {
            if (page == null) page = 1;
            if (page <= 0) page = 1;
            string Mid = _user_profile.M_ID;
            if (!_user_profile.IS_LOGIN)
            {
                OrderCode = _user_profile.NOMEMBER_ORDER_CODE;
                Mid = "";
            }

            MyORDER_VIEW M = new MyORDER_VIEW();
            M.Page = page;
            M.FromDate = FromDate;
            M.ToDate = ToDate;
            M.UserProfile = _user_profile;
            M.DetailInfo = _orderservice.MyOrderDetailInfo(OrderCode, Mid);
            M.ProductList= _orderservice.MyOrderDetailProductList(OrderCode, Mid);
            return View(M);
        }

        //이니시스 결제 취소
        ///  이미 승인된 지불을 취소한다.
        ///  무통장입금 거래인 경우 상점 고객이 발급된 가상계좌로 결제 금액을 입금한 이후에는 결제 취소가 이 취소 모듈로 불가능합니다.  
        ///  상점에서 직접 환불 처리 하셔야 합니다. 
        /// pat_gbn 결제구분 (Web : 웹결제, MObile : 모바일결제)
        private INIPAYRESULT InipayCancelProcess(string Tid, string errMsg, string pat_gbn)
        {
            INIPAYRESULT cancel = new INIPAYRESULT();
            //###############################################################################
            //# 1. 객체 생성 #
            //################
            IINIpay INIpayCancel = new IINIpay("50");

            //###############################################################################
            //# 2. 인스턴스 초기화 /3. 거래 유형 설정 #
            //######################
            INIpayCancel.Initialize("cancel");

            //###############################################################################
            //# 4. 정보 설정 #
            //################
            INIpayCancel.SetPath(_inipay_setpath);
            if (pat_gbn.ToUpper() == "MOBILE")
            {
                INIpayCancel.SetField("mid", _inipay_mobile_mid);	               			// 상점아이디
                INIpayCancel.SetField("admin", _inipay_mobile_admin);						// 키패스워드(상점아이디에 따라 변경)
            }
            else
            {
                INIpayCancel.SetField("mid", _inipay_mid);	               			// 상점아이디
                INIpayCancel.SetField("admin", _inipay_admin);						// 키패스워드(상점아이디에 따라 변경)
            }
            INIpayCancel.SetField("tid", Tid);								    // 취소할 거래번호
            INIpayCancel.SetField("CancelMsg", errMsg);			                // 취소 사유
            INIpayCancel.SetField("debug", _inipay_debug);						// 로그모드(실서비스시에는 "false"로)

            //###############################################################################
            //# 5. 취소 요청 #
            //################
            INIpayCancel.StartAction();

            //###############################################################################
            //# 6. 취소 결과 #
            //################
            cancel.Resultcode = INIpayCancel.GetResult("resultcode");				// 결과코드 ("00"이면 취소 성공)
            cancel.ResultMsg = INIpayCancel.GetResult("resultmsg");					// 결과내용

            //###############################################################################
            //# 7. 인스턴스 해제 #
            //####################
            INIpayCancel.Destory();
            INIpayCancel = null;

            return cancel;
        }

        //결제 취소
        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult OrderCancel(string OrderCode)
        {
            string Mid = _user_profile.M_ID;
            string REG_IP = Request.ServerVariables["REMOTE_HOST"];
            StringBuilder SBuilder = new StringBuilder();

            if (!_user_profile.IS_LOGIN)
            {
                OrderCode = _user_profile.NOMEMBER_ORDER_CODE;
                Mid = "";
            }
            SP_MYPAGE_ORDERLIST_DETAIL_INFO_Result info = new SP_MYPAGE_ORDERLIST_DETAIL_INFO_Result();
            info = _orderservice.MyOrderDetailInfo(OrderCode, Mid);

            //상태 체크 : 10:결제대기,20:결제완료 (pay_gbn == 3 가상계좌일경우 결제완료되면 이니페이 관리자에서 처리해야함.)
            if (!(((info.PAY_GBN == "1" || info.PAY_GBN == "2" || info.PAY_GBN == "4") && info.ORDER_STATUS == "20") || (info.PAY_GBN == "3" && info.ORDER_STATUS == "10")))
            {
                SBuilder.Append("<script language='javascript'>alert('주문취소할 수 있는 상태가 아닙니다.');history.go(-1);</script>");
                return Content(SBuilder.ToString());
            }

            //이니시스 결제 취소
            if ((info.PAY_GBN == "1" || info.PAY_GBN == "2" || info.PAY_GBN == "3") && (!string.IsNullOrEmpty(info.PAT_TID)))  //1:신용카드 2:실시간계좌이체 3:가상계좌, 4:포인트결제
            {

                INIPAYRESULT InipayResult = InipayCancelProcess(info.PAT_TID, "주문자직접취소", info.PAT_GUBUN.ToUpper());
                if (InipayResult.Resultcode != "00")
                {
                    SBuilder.AppendFormat("<script language='javascript'>alert(\"PG사(이니시스) 결제 취소중 에러가 발생했습니다.\\n에러메시지:{0} \\n관리자에게 문의하시기 바랍니다.\");history.go(-1);</script>", InipayResult.ResultMsg);
                    return Content(SBuilder.ToString());
                }
                else
                {
                    _orderservice.OrderPartCancelInsert(info.ORDER_IDX, info.PAT_TID, info.PAT_TID, Convert.ToInt32(info.TOTAL_PAY_PRICE), 0, info.SENDER_EMAIL, 0, "8", Convert.ToInt32(info.TOTAL_PAY_PRICE), 0, Mid, REG_IP); //type 8 :주문자이니시스취소로그
                }

            }

            if (_user_profile.IS_LOGIN)
            {
                _orderservice.OrderMasterPointSet(info.ORDER_IDX, "90", _user_profile.M_ID);
            }
            _orderservice.MyOrderMasterStatusChange(info.ORDER_IDX, "90", Mid); //취소
            
            SBuilder.Append("<form name='mysubmitform' action='/MyPage/MyOrder' method='POST'>\n");
            SBuilder.Append("</form>\n");
            SBuilder.Append("<script language='javascript'>document.mysubmitform.submit();</script>\n");
            return Content(SBuilder.ToString());
        }

        //구매확정
        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult ConfirmSell(string OrderCode, int OrderDetailIdx, string FromDate, string ToDate, int page)
        {
            string Mid = _user_profile.M_ID;
            StringBuilder SBuilder = new StringBuilder();

            if (!_user_profile.IS_LOGIN)
            {
                OrderCode = _user_profile.NOMEMBER_ORDER_CODE;
                Mid = "";
            }
            List<SP_MYPAGE_ORDERLIST_DETAIL_PRODUCT_LIST_Result> lst = new List<SP_MYPAGE_ORDERLIST_DETAIL_PRODUCT_LIST_Result>();
            lst = _orderservice.MyOrderDetailProductList(OrderCode, Mid);

            SP_MYPAGE_ORDERLIST_DETAIL_PRODUCT_LIST_Result info = (from s in lst
                                                                   where s.ORDER_DETAIL_IDX == OrderDetailIdx
                                                                   select s).Single();
           
            //상태 체크 : 40:배송중,50:배송완료 일때만 변경가능
            if (!((info.ORDER_DETAIL_STATUS == "40") || (info.ORDER_DETAIL_STATUS == "50")))
            {
                SBuilder.Append("<script language='javascript'>alert('구매확정 할수 있는 상태가 아닙니다.');history.go(-1);</script>");
                return Content(SBuilder.ToString());
            }

            if (_user_profile.IS_LOGIN)
            {
                _orderservice.OrdeDetailPointSet(OrderDetailIdx, "60", "Y", Mid); //구매포인트 지급
            }
            _orderservice.MyOrderDetailStatusChange(OrderDetailIdx, "60", Mid); //구매확정
            return RedirectToAction("OrderView", new { OrderCode = OrderCode, FromDate = FromDate, ToDate = ToDate, page = page });
        }

        public ActionResult InipayEscrow(string ORDER_CODE)
        {
            string Mid = _user_profile.M_ID;
            if (!_user_profile.IS_LOGIN)
            {
                ORDER_CODE = _user_profile.NOMEMBER_ORDER_CODE;
                Mid = "";
            }

            SP_MYPAGE_ORDERLIST_DETAIL_INFO_Result Info = _orderservice.MyOrderDetailInfo(ORDER_CODE, Mid);
            INIPAYESCROW M = new INIPAYESCROW();
            M.ORDER_CODE = ORDER_CODE;
            M.tid = Info.PAT_TID;
            if (Info.PAY_GBN.ToUpper() == "MOBILE")
            {
                M.mid = _inipay_mobile_mid;
            }
            else
            {
                M.mid = _inipay_mid;
            }
            return View(M);
        }

        //에스크로 지급 승인
        [HttpPost]
        public ActionResult INIescrowConfirm(string ORDER_CODE, string encrypted, string sessionkey, string paymethod, string acceptmethod)
        {
            string Mid = _user_profile.M_ID;
            if (!_user_profile.IS_LOGIN)
            {
                ORDER_CODE = _user_profile.NOMEMBER_ORDER_CODE;
                Mid = "";
            }
            SP_MYPAGE_ORDERLIST_DETAIL_INFO_Result Info = _orderservice.MyOrderDetailInfo(ORDER_CODE, Mid);
            INIESCROW_CONFIRM EscrowResult = new INIESCROW_CONFIRM();
            EscrowResult.ORDER_CODE = ORDER_CODE;
            //상태 체크 //40:배송중 50:배송완료 이고 이니페이배송일자가 있을경우 노출
            if (!(Info.ESCROW_YN == "Y" && !string.IsNullOrEmpty(Info.INIESCROW_DELIVERY)))
            {
                EscrowResult.Resultcode = "09";
                EscrowResult.ResultMsg = "지급 승인 할 수 있는 상태가 아닙니다.";
                EscrowResult.CNF_Date = "";
                EscrowResult.CNF_Time = "";
                EscrowResult.DNY_Date = "";
                EscrowResult.DNY_Time = "";
            }
            else
            {
                EscrowResult = InipayEscrowConfirmProcess(Info.ORDER_IDX, Info.PAT_TID, Info.PAT_GUBUN, encrypted, sessionkey, paymethod, acceptmethod);
            }
            return RedirectToAction("INIescrowResult", EscrowResult);
        }

        public ActionResult INIescrowResult(INIESCROW_CONFIRM EscrowResult)
        {

            return View(EscrowResult);
        }


        //이니시스 에스크로 지급승인
        private INIESCROW_CONFIRM InipayEscrowConfirmProcess(int ORDER_IDX, string Tid, string pat_gbn, string encrypted, string sessionkey, string paymethod, string acceptmethod)
        {
            INIESCROW_CONFIRM result = new INIESCROW_CONFIRM();
            //###############################################################################
            //# 1. 객체 생성 #
            //################
            IINIpay INIpay = new IINIpay("50");

            //###############################################################################
            //# 2. 인스턴스 초기화 /3. 거래 유형 설정 #
            //######################
            INIpay.Initialize("escrow", "confirm");

            INIpay.SetPath(_inipay_setpath);
            //###############################################################################
            //# 4. 정보 설정 #
            //################
            if (pat_gbn.ToUpper() == "MOBILE")
            {
                INIpay.SetField("mid", _inipay_mobile_mid);	            // 상점아이디(모바일)
                INIpay.SetField("admin", _inipay_mobile_admin);			// 키패스워드(모바일)(상점아이디에 따라 변경)
            }
            else
            {
                INIpay.SetField("mid", _inipay_mid);	               		// 상점아이디
                INIpay.SetField("admin", _inipay_admin);					// 키패스워드(상점아이디에 따라 변경)
            }
            INIpay.SetField("debug", _inipay_debug);						// 로그모드(실서비스시에는 "false"로)
            INIpay.SetField("escrowtype", "confirm");
            INIpay.SetField("tid", Tid);				// 거래 tid
            INIpay.SetField("encrypted", encrypted);
            INIpay.SetField("sessionkey", sessionkey);
            INIpay.SetField("uip", Request.UserHostAddress);
            INIpay.StartAction();

            //###############################################################################
            //# 6.  결과 #
            //################
            result.Resultcode = INIpay.GetResult("resultcode");				// 결과코드 ("00"이면 취소 성공)
            result.ResultMsg = INIpay.GetResult("resultmsg");				// 결과내용
            result.CNF_Date = INIpay.GetResult("CNF_Date");
            result.CNF_Time = INIpay.GetResult("CNF_Time");
            result.DNY_Date = INIpay.GetResult("DNY_Date");
            result.DNY_Time = INIpay.GetResult("DNY_Time");

            //###############################################################################
            //# 7. 인스턴스 해제 #
            //####################
            INIpay.Destory();
            INIpay = null;

            if (result.Resultcode == "00")
            {
                string setdate = "";
                if (!string.IsNullOrEmpty(result.DNY_Date))
                {
                    setdate = result.DNY_Date + " " + result.DNY_Time;
                    //결과 저장
                    _orderservice.OrderEscrowResultSet(ORDER_IDX, "CANCEL", setdate);
                }
                else
                {
                    setdate = result.CNF_Date + " " + result.CNF_Time;
                    //결과 저장
                    _orderservice.OrderEscrowResultSet(ORDER_IDX, "CONFIRM", setdate);
                }

            }

            return result;
        }

    }
}