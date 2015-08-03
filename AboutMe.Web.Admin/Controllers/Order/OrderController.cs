using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

using System.Reflection;
using System.Web.Routing;
using Newtonsoft.Json;
using AboutMe.Common.Data;
using AboutMe.Common.Helper;
using AboutMe.Domain.Service.AdminOrder;
using AboutMe.Domain.Entity.AdminOrder;
using AboutMe.Domain.Entity.Order;

using AboutMe.Web.Admin.Common.Filters;
using AboutMe.Web.Admin.Common;

using INIpayNet;

namespace AboutMe.Web.Admin.Controllers.Order
{
    [CustomAuthorize]
    public class OrderController : BaseAdminController
    {
        private IAdminOrderService _adminorderservice;

        private string _inipay_setpath = AboutMe.Common.Helper.Config.GetConfigValue("INIpay.SetPath");
        private string _inipay_mid = AboutMe.Common.Helper.Config.GetConfigValue("INIpay.Mid");
        private string _inipay_admin = AboutMe.Common.Helper.Config.GetConfigValue("INIpay.Admin");
        private string _inipay_debug = AboutMe.Common.Helper.Config.GetConfigValue("INIpay.Debug");

        public OrderController(IAdminOrderService _adminorderservice)
        {
            this._adminorderservice = _adminorderservice;
        }

        public ActionResult Index(SP_TB_ADMIN_ORDER_Param Param) {
            ORDER_INDEX_MODEL m = new ORDER_INDEX_MODEL();
            m.SearchOption = Param;
            m.TotalCount = _adminorderservice.OrderListCount(Param);
            m.OrderList = _adminorderservice.OrderList(Param);
            m.OrderStatusCnt = _adminorderservice.OrderStatusCnt(Param);
            return View(m);
        }

        public ActionResult Detail(int ORDER_IDX, SP_TB_ADMIN_ORDER_Param SEARCH_OPTION)
        {
            ORDER_DETAIL_MODEL m = new ORDER_DETAIL_MODEL();
            m.ORDER_IDX = ORDER_IDX;
            m.SearchOption = SEARCH_OPTION;
            m.OrderDetailList = _adminorderservice.OrderDetailList(ORDER_IDX);
            m.BodyInfo = _adminorderservice.OrderDetailBodyInfo(ORDER_IDX);
            m.LogList = _adminorderservice.OrderDetailLogList(ORDER_IDX);
            return View(m);
        }

        [ChildActionOnly]
        public ActionResult OrderPaging(int TotalRecord, int RecordPerPage, int PagePerBlock, int CurrentPage, string QueryStr)
        {
            PagingProps p_value = new PagingProps(TotalRecord, RecordPerPage, PagePerBlock, CurrentPage, QueryStr);
            return PartialView(p_value);
        }

        [HttpPost]
        public ActionResult DeliveryUpdate(int ORDER_IDX, SP_TB_ADMIN_ORDER_Param SEARCH_OPTION, string RECEIVER_NAME, string RECEIVER_POST, string RECEIVER_ADDR1, string RECEIVER_ADDR2, string RECEIVER_TEL, string RECEIVER_HP)
        {
            string REG_ID = AdminUserInfo.GetAdmId();

            RouteValueDictionary param = ConvertRouteValue(SEARCH_OPTION);
            param.Add("ORDER_IDX", ORDER_IDX);

            _adminorderservice.OrderReceiverUpdate(ORDER_IDX, RECEIVER_NAME, RECEIVER_POST, RECEIVER_ADDR1, RECEIVER_ADDR2, RECEIVER_TEL, RECEIVER_HP, REG_ID);
            return RedirectToAction("Detail", param );
        }
        
        [HttpPost]
        public ActionResult AdminMemoUpdate(int ORDER_IDX, SP_TB_ADMIN_ORDER_Param SEARCH_OPTION, string MANAGER_MEMO)
        {
            RouteValueDictionary param = ConvertRouteValue(SEARCH_OPTION);
            param.Add("ORDER_IDX", ORDER_IDX);

            _adminorderservice.AdminMemoUpdate(ORDER_IDX, MANAGER_MEMO);
            return RedirectToAction("Detail", param );
        }
        
        //송장번호 변경
        [HttpPost]
        public ActionResult OrderDetailDeliveryUpdate(int ORDER_IDX, int ORDER_DETAIL_IDX, SP_TB_ADMIN_ORDER_Param SEARCH_OPTION, string DELIVERY_NUM)
        {
            string REG_ID = AdminUserInfo.GetAdmId();
            RouteValueDictionary param = ConvertRouteValue(SEARCH_OPTION);
            param.Add("ORDER_IDX", ORDER_IDX);

            _adminorderservice.OrderDetailDeliveryUpdate(ORDER_DETAIL_IDX, DELIVERY_NUM, REG_ID);
            return RedirectToAction("Detail", param );
        }

        //주문상세 상태 변경
        [HttpPost]
        public ActionResult OrderDetailStatusUpdate(int ORDER_IDX, int ORDER_DETAIL_IDX, SP_TB_ADMIN_ORDER_Param SEARCH_OPTION, string TOBE_STATUS)
        {
            string REG_ID = AdminUserInfo.GetAdmId();
            RouteValueDictionary param = ConvertRouteValue(SEARCH_OPTION);
            param.Add("ORDER_IDX", ORDER_IDX);

            _adminorderservice.OrderDetailStatusUpdate(ORDER_DETAIL_IDX, TOBE_STATUS, REG_ID);
            return RedirectToAction("Detail", param);
        }


        //주문Master 상태 변경
        [HttpPost]
        public ActionResult OrderMasterStatusUpdate(int ORDER_IDX, SP_TB_ADMIN_ORDER_Param SEARCH_OPTION, string TOBE_STATUS, string RedirectAction="")
        {
            SP_ADMIN_ORDERLIST_DETAIL_BODY_Result Info = _adminorderservice.OrderDetailBodyInfo(ORDER_IDX);
            string result = "";
            if (Info.ORDER_STATUS == TOBE_STATUS)
            {
                result = "<script language='javascript'>alert('이미 "+Info.ORDER_STATUS_NM+" 상태입니다.상태를 변경할 수 없습니다.');history.go(-1);</script>";
                return Content(result);
            }
            else
            {
                string REG_ID = AdminUserInfo.GetAdmId();
                RouteValueDictionary param = ConvertRouteValue(SEARCH_OPTION);
                param.Add("ORDER_IDX", ORDER_IDX);

                _adminorderservice.OrderMasterStatusUpdate(ORDER_IDX, TOBE_STATUS, REG_ID);
                return RedirectToAction(RedirectAction, param);
            }
        }

         //주문Master 상태 일괄 변경
        [HttpPost]
        public ActionResult MutiStatusUpdate(string tobe_status, string data)
        {
            List<int> DataList = JsonConvert.DeserializeObject<List<int>>(data);
            string REG_ID = AdminUserInfo.GetAdmId();
            foreach (int orderidx in DataList)
            {
                SP_ADMIN_ORDERLIST_DETAIL_BODY_Result Info = _adminorderservice.OrderDetailBodyInfo(orderidx);
                if (Info.ORDER_STATUS != tobe_status && Info.ORDER_STATUS != "99") //기타는 변경안함.
                {
                    _adminorderservice.OrderMasterStatusUpdate(orderidx, tobe_status, REG_ID);
                }
            }

            var jsonData = new { result = "true"};

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        //이니시스 부분취소 모듈 팝업창
        public ActionResult INIrepay(int ORDER_IDX)
        {
            SP_ADMIN_ORDERLIST_DETAIL_BODY_Result Info = _adminorderservice.OrderDetailBodyInfo(ORDER_IDX);
            INIREPAY_MODEL M = new INIREPAY_MODEL();
            M.ORDER_IDX = ORDER_IDX;
            M.MasterInfo = Info;
            M.Top1Info = _adminorderservice.OrderPartCancelTopSelect(ORDER_IDX);
            M.RepayList = _adminorderservice.OrderPartCancelList(ORDER_IDX);
            return View(M);
        }


        //이니시스재승인(부분취소) 모듈 취소
        private INIREPAY_RESULT InipayReplayOk(INIREPAY_OK_MODEL Param)
        {
            INIREPAY_RESULT cancel = new INIREPAY_RESULT();
            //###############################################################################
            //# 1. 객체 생성 #
            //################
            IINIpay INIpayCancel = new IINIpay("50");

            //###############################################################################
            //# 2. 인스턴스 초기화 /3. 거래 유형 설정 #
            //######################
            INIpayCancel.Initialize("repay");

            INIpayCancel.SetPath(_inipay_setpath);
            //###############################################################################
            //# 4. 정보 설정 #
            //################
            INIpayCancel.SetField("pgid", "INInetREPA_");	          			// 서브 PG아이디 , 수정금지
            INIpayCancel.SetField("subpgip", "203.238.3.10");	          			// 서브 PG아이디 , 수정금지
            INIpayCancel.SetField("mid", _inipay_mid);	               			// 상점아이디
            INIpayCancel.SetField("admin", _inipay_admin);						// 키패스워드(상점아이디에 따라 변경)
            INIpayCancel.SetField("oldtid", Param.oldtid);					    // 원거래 TID
            INIpayCancel.SetField("currency", "WON");
            INIpayCancel.SetField("price", Param.price);
            INIpayCancel.SetField("confirm_price", Param.confirm_price);
            INIpayCancel.SetField("buyeremail", Param.buyeremail);
            INIpayCancel.SetField("no_acct", "");                               //국민은행 부분취소 환불계좌번호
            INIpayCancel.SetField("nm_acct", "");                               //국민은행 부분취소 환불계좌주명
            INIpayCancel.SetField("debug", _inipay_debug);						// 로그모드(실서비스시에는 "false"로)

            //###############################################################################
            //# 5. 취소 요청 #
            //################
            INIpayCancel.StartAction();

            //###############################################################################
            //# 6. 취소 결과 #
            //################
            cancel.ResultCode = INIpayCancel.GetResult("ResultCode");				// 결과코드 ("00"이면 부분취소 성공)
            cancel.ResultMsg = INIpayCancel.GetResult("ResultMsg");					// 결과내용
            cancel.TID = INIpayCancel.GetResult("TID");	                            //신거래ID
            cancel.PRTC_TID = INIpayCancel.GetResult("PRTC_TID");	                //원거래ID
            cancel.PRTC_Remains = INIpayCancel.GetResult("PRTC_Remains");	        //최종결제금액
            cancel.PRTC_Price = INIpayCancel.GetResult("PRTC_Price");	            //부분취소금액
            cancel.PRTC_Cnt = INIpayCancel.GetResult("PRTC_Cnt");	                //요청횟수(최대9회)
            cancel.PRTC_Type = INIpayCancel.GetResult("PRTC_Type");	                //요청횟수(최대9회)
            cancel.ORDER_IDX = Param.ORDER_IDX;
            //###############################################################################
            //# 7. 인스턴스 해제 #
            //####################
            INIpayCancel.Destory();
            INIpayCancel = null;

            return cancel;
        }

        //이니시스 부분취소
        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult INIrepayProcess(INIREPAY_OK_MODEL Param)
        {
            string REG_ID = AdminUserInfo.GetAdmId();
            string REG_IP = Request.ServerVariables["REMOTE_HOST"];
            StringBuilder SBuilder = new StringBuilder();

            INIREPAY_RESULT repay = InipayReplayOk(Param);
            if (repay.ResultCode == "00") //성공
            {
                _adminorderservice.OrderPartCancelInsert(Param.ORDER_IDX, repay.TID, Param.oldtid, Convert.ToInt32(Param.price), Convert.ToInt32(Param.confirm_price), Param.buyeremail, Convert.ToInt32(repay.PRTC_Remains), repay.PRTC_Type, Convert.ToInt32(repay.PRTC_Price), Convert.ToInt16(repay.PRTC_Cnt), REG_ID, REG_IP);
            }
            return View("INIrepayResult",repay);
            //return RedirectToAction("INIrepayResult", new { RESULT = repay });
        }


        //이니시스 결제 전체 취소
        ///  이미 승인된 지불을 취소한다.
        ///  무통장입금 거래인 경우 상점 고객이 발급된 가상계좌로 결제 금액을 입금한 이후에는 결제 취소가 이 취소 모듈로 불가능합니다.  
        ///  상점에서 직접 환불 처리 하셔야 합니다. 

        private INIPAYRESULT InipayCancelProcess(string Tid, string errMsg)
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
            INIpayCancel.SetField("mid", _inipay_mid);	               			// 상점아이디
            INIpayCancel.SetField("admin", _inipay_admin);						// 키패스워드(상점아이디에 따라 변경)
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

        //결제 전체 취소
        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult OrderCancel(int ORDER_IDX, SP_TB_ADMIN_ORDER_Param SEARCH_OPTION)
        {
            string REG_ID = AdminUserInfo.GetAdmId();
            string REG_IP = Request.ServerVariables["REMOTE_HOST"];
            StringBuilder SBuilder = new StringBuilder();

            SP_ADMIN_ORDERLIST_DETAIL_BODY_Result info = _adminorderservice.OrderDetailBodyInfo(ORDER_IDX);
            
            //상태 체크 : 10:결제대기,20:결제완료 (pay_gbn == 3 가상계좌일경우 결제완료되면 이니페이 관리자에서 처리해야함.)
            if (!(((info.PAY_GBN == "1" || info.PAY_GBN == "2" || info.PAY_GBN == "4") && info.ORDER_STATUS == "20") || (info.PAY_GBN == "3" && info.ORDER_STATUS == "10")))
            {
                SBuilder.Append("<script language='javascript'>alert('주문취소할 수 있는 상태가 아닙니다.');history.go(-1);</script>");
                return Content(SBuilder.ToString());
            }
            
            if (info.ORDER_STATUS == "90")
            {
                SBuilder.Append("<script language='javascript'>alert('이미 취소상태입니다.');history.go(-1);</script>");
                return Content(SBuilder.ToString());
            }
            //이니시스 결제 취소
            if ((info.PAY_GBN == "1" || info.PAY_GBN == "2" || info.PAY_GBN == "3") && (!string.IsNullOrEmpty(info.PAT_TID)))  //1:신용카드 2:실시간계좌이체 3:가상계좌, 4:포인트결제
            {
                INIPAYRESULT InipayResult = InipayCancelProcess(info.PAT_TID, "관리자 전체 취소");
                if (InipayResult.Resultcode != "00")
                {
                    SBuilder.AppendFormat("<script language='javascript'>alert(\"PG사(이니시스) 결제 취소중 에러가 발생했습니다.\\n에러메시지:{0} \");history.go(-1);</script>", InipayResult.ResultMsg);
                    return Content(SBuilder.ToString());
                }
                else
                {
                    _adminorderservice.OrderPartCancelInsert(ORDER_IDX, info.PAT_TID, info.PAT_TID, Convert.ToInt32(info.REAL_PAY_PRICE), 0, info.SENDER_EMAIL, 0, "9", Convert.ToInt32(info.REAL_PAY_PRICE), 0, REG_ID, REG_IP); //type 9 :관리자이니시스취소로그
                }
            }

            _adminorderservice.OrderMasterStatusUpdate(ORDER_IDX, "90", REG_ID); //취소
            RouteValueDictionary param = ConvertRouteValue(SEARCH_OPTION);
            param.Add("ORDER_IDX", ORDER_IDX);

            return RedirectToAction("Detail", param);

        }

        public ActionResult DeliveryExcelDownload(string OrderIdxStr)
        {
            List<SP_ADMIN_ORDER_TO_DELIVERYEXCEL_Result> lst = new List<SP_ADMIN_ORDER_TO_DELIVERYEXCEL_Result>();
            lst = _adminorderservice.OrderDeliveryExcelList(OrderIdxStr).ToList();
            HttpContext.Response.Clear();
            HttpContext.Response.Charset = "";

            // set MIME type to be Excel file. 
            HttpContext.Response.ContentType = "application/vnd.ms-excel";

            // add a header to response to force download (specifying filename) 
            HttpContext.Response.AddHeader("Content-Disposition", "attachment; filename=\"delivery_excel_" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls\"");

            // Send the data. Tab delimited, with newlines. 
            HttpContext.Response.ContentEncoding = System.Text.Encoding.GetEncoding("ks_c_5601-1987");

            string colum_title = "ORDER_IDX \t ORDER_DETAIL_IDX \t ORDER_CODE \t ORDER_DATE  \t ORDER_NAME  \t EMP_YN \t P_CODE \t P_NAME  \t P_COUNT \t PAY_NM  \t REAL_PAY_PRICE \t ORDER_STATUS_NM \t PROMOTION_NM \t PMO_PRODUCT_NAME \t FREEGIFT_NAME \t DELIVERY_NUM \n";
            HttpContext.Response.Write(colum_title);

            foreach (var row in lst)
            {
                string delivery_num = ( string.IsNullOrEmpty(row.DELIVERY_NUM) ? "" : row.DELIVERY_NUM.ToString().Trim());
                HttpContext.Response.Write(String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}\n"
                                , row.ORDER_IDX.ToString().Trim(), row.ORDER_DETAIL_IDX.ToString().Trim(), row.ORDER_CODE.ToString().Trim(), row.ORDER_DATE.ToString().Trim(), row.ORDER_NAME.ToString().Trim()
                                , row.EMP_YN.ToString().Trim(), row.P_CODE.ToString().Trim(), row.P_NAME.ToString().Trim(), row.P_COUNT.ToString().Trim(), row.PAY_NM.ToString().Trim()
                                , row.REAL_PAY_PRICE.ToString().Trim(), row.ORDER_STATUS_NM.ToString().Trim(), row.PROMOTION_NM.ToString().Trim(), row.PMO_PRODUCT_NAME.ToString().Trim()
                                , row.FREEGIFT_NAME.ToString().Trim(), delivery_num));
            }
            Response.Flush();
            HttpContext.Response.End();

            return View();
        }
    }
}