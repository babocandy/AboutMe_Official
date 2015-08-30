using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Net;

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
        private string _inipay_mobile_mid = AboutMe.Common.Helper.Config.GetConfigValue("INIpay.Mobile.Mid");
        private string _inipay_mobile_admin = AboutMe.Common.Helper.Config.GetConfigValue("INIpay.Mobile.Admin");
        private string _inipay_debug = AboutMe.Common.Helper.Config.GetConfigValue("INIpay.Debug");

        public OrderController(IAdminOrderService _adminorderservice)
        {
            this._adminorderservice = _adminorderservice;
        }

        //HTML태그제거
        private String TagStrip(String text)
        {
            if (string.IsNullOrEmpty(text))
            {

                return String.Empty;
            }
            else
            {
                return System.Text.RegularExpressions.Regex.Replace(text, @"<(.|\n)*?>", String.Empty);
            }
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
        
        //이니시스 에스크로 배송(송장) 번호 등록
        public string InisysDeliveryUpdate(SP_ADMIN_ORDERLIST_DETAIL_BODY_Result orderInfo, string DELIVERY_NUM)
        {

            //상품명구하기

            List<SP_ADMIN_ORDERLIST_DETAIL_LIST_Result> OrderList = _adminorderservice.OrderDetailList(orderInfo.ORDER_IDX);
            string goodname = OrderList.FirstOrDefault().P_NAME;
            goodname = TagStrip(goodname);
            if (OrderList.Count() > 1)
            {
                goodname = goodname + "외 " + Convert.ToString(OrderList.Count() - 1) + "건";
            }


            INIESCROW_DELIVERY_RESULT EscrowResult = new INIESCROW_DELIVERY_RESULT();
            //###############################################################################
            //# 1. 객체 생성 #
            //################
            IINIpay INIpay = new IINIpay("50");

            //###############################################################################
            //# 2. 인스턴스 초기화 /3. 거래 유형 설정 #
            //######################
            INIpay.Initialize("escrow");

            INIpay.SetPath(_inipay_setpath);
            //###############################################################################
            //# 4. 정보 설정 #
            //################
            INIpay.SetField("type", "escrow");	          			        // 수정금지
            //INIpay.SetField("pgid", "INInetRPAY");	          			// 서브 PG아이디 , 수정금지 INIpayRPAY
            if (orderInfo.PAT_GUBUN.ToUpper() == "MOBILE")
            {
                INIpay.SetField("mid", _inipay_mobile_mid);	            // 상점아이디(모바일)
                INIpay.SetField("admin", _inipay_mobile_admin);			// 키패스워드(모바일)(상점아이디에 따라 변경)
            }
            else
            {
                INIpay.SetField("mid", _inipay_mid);	               		// 상점아이디
                INIpay.SetField("admin", _inipay_admin);					// 키패스워드(상점아이디에 따라 변경)
            }
            INIpay.SetField("escrowtype", "dlv");
            INIpay.SetField("tid", orderInfo.PAT_TID);				// 거래 tid
            INIpay.SetField("oid", Convert.ToString(orderInfo.OLD_ORDER_IDX));         //old order idx
            INIpay.SetField("soid", "1");
            INIpay.SetField("dlv_invoice", DELIVERY_NUM);     //송장번호
            INIpay.SetField("dlv_report", "I");        //I:배송등록, U:배송수정
            INIpay.SetField("dlv_name ", AdminUserInfo.GetAdmId());                      //배송등록자
            INIpay.SetField("dlv_excode", "cjgls");                     //택배사코드 "cjgls : CJ대한통운"
            INIpay.SetField("dlv_exname", "CJ대한통운");					//택배사명 "CJ대한통운"

            INIpay.SetField("dlv_invoiceday", DateTime.Today.ToShortDateString());  //배송등록 확인일시
            if (orderInfo.TRANS_PRICE > 0)
            {
                INIpay.SetField("dlv_charge", "BH");  //배송비 지급방법 (배송비 지급방법 (SH : 판매자부담, BH : 구매자부담 )
            }
            else
            {
                INIpay.SetField("dlv_charge", "SH");  //배송비 지급방법 (배송비 지급방법 (SH : 판매자부담, BH : 구매자부담 )
            }

            INIpay.SetField("dlv_sendname", orderInfo.ORDER_NAME);
            INIpay.SetField("dlv_sendpost", orderInfo.SENDER_POST);
            INIpay.SetField("dlv_sendaddr1", orderInfo.SENDER_ADDR1);
            INIpay.SetField("dlv_sendaddr2", orderInfo.SENDER_ADDR2);
            INIpay.SetField("dlv_sendtel", orderInfo.SENDER_HP);
            INIpay.SetField("dlv_recvname", orderInfo.RECEIVER_NAME);
            INIpay.SetField("dlv_recvpost", orderInfo.RECEIVER_POST);
            INIpay.SetField("dlv_recvaddr", orderInfo.RECEIVER_ADDR1 + " " + orderInfo.RECEIVER_ADDR2);
            INIpay.SetField("dlv_recvtel", orderInfo.RECEIVER_HP);
            INIpay.SetField("dlv_goods", goodname);
            INIpay.SetField("price", Convert.ToString(orderInfo.REAL_PAY_PRICE));

            //###############################################################################
            //# 5. 취소 요청 #
            //################
            INIpay.StartAction();

            //###############################################################################
            //# 6.  결과 #
            //################
            EscrowResult.tid = INIpay.GetResult("tid");	                            //신거래ID
            EscrowResult.resultcode = INIpay.GetResult("resultcode");				// 결과코드 ("00"이면 부분취소 성공)
            EscrowResult.resultmsg = INIpay.GetResult("resultmsg");					// 결과내용

            EscrowResult.DLV_Date = INIpay.GetResult("DLV_Date");	        //처리날자
            EscrowResult.DLV_Time = INIpay.GetResult("DLV_Time");	        //처리시간

           
            if (EscrowResult.resultcode == "00")
            {
                string setdate = EscrowResult.DLV_Date+" "+EscrowResult.DLV_Time;
                //결과 저장
                _adminorderservice.OrderEscrowResultSet(orderInfo.ORDER_IDX, "DELIVERY", setdate);
            }


            //###############################################################################
            //# 7. 인스턴스 해제 #
            //####################
            INIpay.Destory();
            INIpay = null;

            return EscrowResult.resultcode;
        }


        //송장번호 변경
        [HttpPost]
        public ActionResult OrderDetailDeliveryUpdate(int ORDER_IDX, int ORDER_DETAIL_IDX, SP_TB_ADMIN_ORDER_Param SEARCH_OPTION, string DELIVERY_NUM)
        {
            string REG_ID = AdminUserInfo.GetAdmId();
            string resultcode = "00";
            //에스크로일경우 이니시스에 배송등록실행
            SP_ADMIN_ORDERLIST_DETAIL_BODY_Result orderInfo = _adminorderservice.OrderDetailBodyInfo(ORDER_IDX);
            if (orderInfo.ESCROW_YN == "Y" && string.IsNullOrEmpty(orderInfo.INIESCROW_DELIVERY))
            {
                resultcode = InisysDeliveryUpdate(orderInfo, DELIVERY_NUM);
            }

            RouteValueDictionary param = ConvertRouteValue(SEARCH_OPTION);
            param.Add("ORDER_IDX", ORDER_IDX);

            if (resultcode == "00")
            { 
                _adminorderservice.OrderDetailDeliveryUpdate(ORDER_DETAIL_IDX, DELIVERY_NUM, REG_ID);
            }
            return RedirectToAction("Detail", param );
        }

        //주문상세 상태 변경
        [HttpPost]
        public ActionResult OrderDetailStatusUpdate(int ORDER_IDX, int ORDER_DETAIL_IDX, SP_TB_ADMIN_ORDER_Param SEARCH_OPTION, string TOBE_STATUS)
        {
            string REG_ID = AdminUserInfo.GetAdmId();
            RouteValueDictionary param = ConvertRouteValue(SEARCH_OPTION);
            param.Add("ORDER_IDX", ORDER_IDX);

            if (TOBE_STATUS == "90") //취소일경우
            {
                SP_ADMIN_ORDER_PART_CANCEL_TOP_SELECT_Result top1 = _adminorderservice.OrderPartCancelTopSelect(ORDER_IDX);
                Int32 remaind_price = Convert.ToInt32(top1.PAY_PRICE);

                //order_detail_idx의 실제 결제 금액 구하기
                int real_pay_price = 0;
                List<SP_ADMIN_ORDERLIST_DETAIL_LIST_Result> pList = _adminorderservice.OrderDetailList(ORDER_IDX);
                SP_ADMIN_ORDERLIST_DETAIL_LIST_Result Qry = (from list in pList
                                                             where list.ORDER_DETAIL_IDX == ORDER_DETAIL_IDX
                                                             select list
                           ).FirstOrDefault();

                if (Qry != null) real_pay_price = Convert.ToInt16(Qry.REAL_PAY_PRICE);
                
                if (real_pay_price > 0 && remaind_price > 0)
                {
                    Int32 confirm_price = remaind_price - real_pay_price; ////승인요청금액 ([이전승인금액 - 취소할 금액])
                    INIREPAY_OK_MODEL Param2 = new INIREPAY_OK_MODEL();
                    Param2.ORDER_IDX = ORDER_IDX;
                    Param2.oldtid = top1.TID;
                    Param2.buyeremail = top1.EMAIL;
                    Param2.confirm_price = Convert.ToString(confirm_price);
                    Param2.price = Convert.ToString(real_pay_price); //취소할금액

                    INIREPAY_RESULT repay = InipayReplayOk(Param2);

                    if (repay.ResultCode == "00") //성공
                    {
                        string REG_IP = Request.ServerVariables["REMOTE_HOST"];
                        _adminorderservice.OrdeDetailPointSet(ORDER_DETAIL_IDX, TOBE_STATUS, "Y", REG_ID);
                        _adminorderservice.OrderPartCancelInsert(ORDER_IDX, repay.TID, top1.TID, Convert.ToInt32(real_pay_price), Convert.ToInt32(confirm_price), top1.EMAIL, Convert.ToInt32(repay.PRTC_Remains), repay.PRTC_Type, Convert.ToInt32(repay.PRTC_Price), Convert.ToInt16(repay.PRTC_Cnt), REG_ID, REG_IP);
                        _adminorderservice.OrderDetailStatusUpdate(ORDER_DETAIL_IDX, TOBE_STATUS, REG_ID);
                        return RedirectToAction("Detail", param);
                    }
                    else
                    {
                        return Content("<script>alert(\"PG사 취소시 에러가 발생했습니다.[error : " + repay.ResultMsg + " tid:" + top1.TID + "]\"); history.go(-1);</script>");
                    }

                }
                else
                {
                    _adminorderservice.OrdeDetailPointSet(ORDER_DETAIL_IDX, TOBE_STATUS, "Y", REG_ID);
                    _adminorderservice.OrderDetailStatusUpdate(ORDER_DETAIL_IDX, TOBE_STATUS, REG_ID);
                    return RedirectToAction("Detail", param);
                }
            }
            else
            {
                _adminorderservice.OrdeDetailPointSet(ORDER_DETAIL_IDX, TOBE_STATUS, "Y", REG_ID);
                _adminorderservice.OrderDetailStatusUpdate(ORDER_DETAIL_IDX, TOBE_STATUS, REG_ID);
                return RedirectToAction("Detail", param);
            }
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
            string PAT_GBN = _adminorderservice.OrderFindPatgbn(Param.oldtid).ToUpper();
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
            INIpayCancel.SetField("type", "repay");	          			        // 수정금지
            INIpayCancel.SetField("pgid", "INInetRPAY");	          			// 서브 PG아이디 , 수정금지 INIpayRPAY
            if (PAT_GBN == "MOBILE")
            {
                INIpayCancel.SetField("mid", _inipay_mobile_mid);	            // 상점아이디(모바일)
                INIpayCancel.SetField("admin", _inipay_mobile_admin);			// 키패스워드(모바일)(상점아이디에 따라 변경)
            }
            else
            {
                INIpayCancel.SetField("mid", _inipay_mid);	               		// 상점아이디
                INIpayCancel.SetField("admin", _inipay_admin);					// 키패스워드(상점아이디에 따라 변경)
            }
            INIpayCancel.SetField("PRTC_TID", Param.oldtid);					// 원거래 TID
            INIpayCancel.SetField("uip", Request.UserHostAddress);				// 사용자 IP
            INIpayCancel.SetField("currency", "WON");
            INIpayCancel.SetField("PRTC_Price", Param.price);
            INIpayCancel.SetField("PRTC_Remains", Param.confirm_price);
            INIpayCancel.SetField("buyeremail", Param.buyeremail);
            INIpayCancel.SetField("MReserved1", "");                            //"예비1"
            INIpayCancel.SetField("PRTC_NoAcctFNBC", "");                      //국민은행 부분취소 환불계좌번호
            INIpayCancel.SetField("PRTC_NmAcctFNBC", "");                      //국민은행 부분취소 환불계좌주명
            INIpayCancel.SetField("debug", _inipay_debug);						// 로그모드(실서비스시에는 "false"로)

            //###############################################################################
            //# 5. 취소 요청 #
            //################
            INIpayCancel.StartAction();

            //###############################################################################
            //# 6. 취소 결과 #
            //################
            cancel.ResultCode = INIpayCancel.GetResult("resultcode");				// 결과코드 ("00"이면 부분취소 성공)
            cancel.ResultMsg = INIpayCancel.GetResult("resultmsg");					// 결과내용
            cancel.TID = INIpayCancel.GetResult("tid");	                            //신거래ID
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
                INIPAYRESULT InipayResult = InipayCancelProcess(info.PAT_TID, "관리자 전체 취소", info.PAT_GUBUN.ToUpper());
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

            _adminorderservice.OrderMasterPointSet(ORDER_IDX, "90", REG_ID);
            _adminorderservice.OrderMasterStatusUpdate(ORDER_IDX, "90", REG_ID); //취소
            RouteValueDictionary param = ConvertRouteValue(SEARCH_OPTION);
            param.Add("ORDER_IDX", ORDER_IDX);

            return RedirectToAction("Detail", param);

        }

        public ActionResult DeliveryExcelDownload(string OrderIdxStr)
        {
            List<SP_ADMIN_ORDER_TO_DELIVERYEXCEL_Result> lst = new List<SP_ADMIN_ORDER_TO_DELIVERYEXCEL_Result>();
            lst = _adminorderservice.OrderDeliveryExcelList(OrderIdxStr).ToList();
            /*
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
            */
            string uploadPath = @"\Upload\DeliveryExcel\";
            DateTime dt2 = new DateTime();
            dt2 = DateTime.Now;
            string now_dt = dt2.ToString("yyyyMMddHHmms");

            string source = Server.MapPath(uploadPath+"delivery_templet.xls");
            string file_name = "delivery_templet_" + now_dt + ".xls";
            string create_path = uploadPath + @"Download\" + now_dt;
            string dest = create_path + @"\" + file_name;
            string sql = "";

            if (!Directory.Exists(Server.MapPath(uploadPath)))
            {
                Directory.CreateDirectory(Server.MapPath(uploadPath));
            }

            if (!Directory.Exists(Server.MapPath(create_path)))
            {
                Directory.CreateDirectory(Server.MapPath(create_path));
            }

            System.IO.File.Copy(source, Server.MapPath(dest));
            string excelConnectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0}; Extended Properties=""Excel 8.0;HDR=Yes"";", Server.MapPath(dest));

            OleDbConnection con = new System.Data.OleDb.OleDbConnection(excelConnectionString);
            con.Open();
            try
            {
                foreach (var row in lst)
                {
                    string delivery_num = (string.IsNullOrEmpty(row.DELIVERY_NUM) ? "" : row.DELIVERY_NUM.ToString().Trim());

                    sql = "INSERT INTO [Sheet1$] ([ORDER IDX],[ORDER DETAIL IDX],[주문번호],[주문일자],[주문자명],[임직원여부],[상품코드],[상품명],[개수],[결제구분],[결제액],[상태],[프로모션종류],[프로모션명],[사은품명],[송장번호]) ";
                    sql = sql + String.Format(" VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}');"
                                    , row.ORDER_IDX.ToString().Trim(), row.ORDER_DETAIL_IDX.ToString().Trim(), row.ORDER_CODE.ToString().Trim(), row.ORDER_DATE.ToString().Trim(), row.ORDER_NAME.ToString().Trim()
                                    , row.EMP_YN.ToString().Trim(), row.P_CODE.ToString().Trim(), row.P_NAME.ToString().Trim(), row.P_COUNT.ToString().Trim(), row.PAY_NM.ToString().Trim()
                                    , row.REAL_PAY_PRICE.ToString().Trim(), row.ORDER_STATUS_NM.ToString().Trim(), row.PROMOTION_NM.ToString().Trim(), row.PMO_PRODUCT_NAME.ToString().Trim()
                                    , row.FREEGIFT_NAME.ToString().Trim(), delivery_num);

                    OleDbCommand Com = new OleDbCommand(sql, con);
                    Com.ExecuteNonQuery();

                }
            }
            finally
            {
                con.Close();                
            }

            return FileDownload(Server.MapPath(dest));

        }

        public ActionResult FileDownload(string filepath)
        {

            var filename = Path.GetFileName(filepath);

            FileStream stream = new FileStream(filepath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
            try
            {
                long bytesToRead = stream.Length;

                Response.ContentType = @"application\octet-stream";
                Response.AddHeader("Content-Transfer-Encoding", "binary");
                Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", Server.UrlPathEncode(filename).Replace("+", "%20")));
                Response.AddHeader("Content-Length", Convert.ToString(stream.Length));

                while (bytesToRead > 0)
                {
                    if (Response.IsClientConnected)
                    {
                        byte[] buffer = new Byte[10000];
                        int length = stream.Read(buffer, 0, 10000);
                        Response.OutputStream.Write(buffer, 0, length);
                        Response.Flush();
                        bytesToRead = bytesToRead - length;
                    }
                    else
                    {
                        bytesToRead = -1;
                    }
                }

                Response.End();
                return View();
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }


        public ActionResult Delivery()
        {
            object result = TempData["UploadResult"];
           // List<DELIVERTY_EXCEL_RESULT> lst = JsonConvert.DeserializeObject<List<DELIVERTY_EXCEL_RESULT>>(result);
            return View(result);
        }
        
        [HttpPost]
        public ActionResult DeliveryExcelUpdate(IEnumerable<HttpPostedFileBase> ExcelUploadFile)
        {
            //저장경로
            DateTime dt2 = new DateTime();
            dt2 = DateTime.Now;
            string now_dt = dt2.ToString("yyyyMMddHHmms");
            string uploadPath = @"\Upload\DeliveryExcel\Upload\";
            string sDirectory = uploadPath + now_dt;
            string REG_ID = AdminUserInfo.GetAdmId();

            List<DELIVERTY_EXCEL_RESULT> result = new List<DELIVERTY_EXCEL_RESULT>();

            if (!Directory.Exists(Server.MapPath(uploadPath)))
            {
                Directory.CreateDirectory(Server.MapPath(uploadPath));
            }


            HttpPostedFileBase file =  ExcelUploadFile.FirstOrDefault();
            if (file.ContentLength > 0)
            {
                string FileExt = string.Empty;
                string FilePath = string.Empty;
                string FileName = string.Empty;
                   
                FileName = Path.GetFileName(file.FileName);
                FilePath = Path.Combine(sDirectory, Path.GetFileName(file.FileName));
                FileExt = System.IO.Path.GetExtension(file.FileName);
                
                if (!Directory.Exists(Server.MapPath(sDirectory)))
                {
                    Directory.CreateDirectory(Server.MapPath(sDirectory));
                }

                file.SaveAs(Server.MapPath(FilePath));

                
                string excelConnectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0}; Extended Properties=""Excel 8.0;HDR=Yes"";", Server.MapPath(FilePath));
                OleDbConnection con = new System.Data.OleDb.OleDbConnection(excelConnectionString);
                OleDbDataAdapter cmd = new System.Data.OleDb.OleDbDataAdapter("select * from [Sheet1$] ", con);

                con.Open();
                try
                {
                    System.Data.DataSet excelDataSet = new DataSet();
                    cmd.Fill(excelDataSet);
                    DataTable data = excelDataSet.Tables[0];

                    DataRow[] arrdata = data.Select();
                    int r = 0;
                    int cnt = 0;
                    int succ_cnt = 0;
                    foreach (DataRow rw in arrdata)
                    {
                        object[] cval = rw.ItemArray;
                        r++;
                        cnt++;
                        if (r > 0)
                        {
                            string order_idx = cval[0].ToString().Trim();
                            string order_detail_idx = cval[1].ToString().Trim();
                            string order_code = cval[2].ToString().Trim();
                            string order_date = cval[3].ToString().Trim();
                            string order_name = cval[4].ToString().Trim();
                            string emp_yn = cval[5].ToString().Trim();
                            string p_code = cval[6].ToString().Trim();
                            string p_name = cval[7].ToString().Trim();
                            string p_count = cval[8].ToString().Trim();
                            string pay_nm = cval[9].ToString().Trim();
                            string real_pay_price = cval[10].ToString().Trim();
                            string order_status_nm = cval[11].ToString().Trim();
                            string promotion_nm = cval[12].ToString().Trim();
                            string pmo_product_name = cval[13].ToString().Trim();
                            string freegift_name = cval[14].ToString().Trim();
                            string delivery_num = cval[15].ToString().Trim();

                            if (!string.IsNullOrEmpty(delivery_num))
                            {
                                if (!string.IsNullOrEmpty(order_detail_idx))
                                {
                                    DELIVERTY_EXCEL_RESULT dv = new DELIVERTY_EXCEL_RESULT();
                                    dv.ORDER_IDX = order_idx;
                                    dv.ORDER_DETAIL_IDX = order_detail_idx;
                                    dv.ORDER_CODE = order_code;
                                    dv.ORDER_DATE = order_date;
                                    dv.ORDER_NAME = order_name;
                                    dv.EMP_YN = emp_yn;
                                    dv.P_CODE = p_code;
                                    dv.P_NAME = p_name;
                                    dv.P_COUNT = p_count;
                                    dv.PAY_NM = pay_nm;
                                    dv.REAL_PAY_PRICE = real_pay_price;
                                    dv.ORDER_STATUS_NM = order_status_nm;
                                    dv.PROMOTION_NM = promotion_nm;
                                    dv.PMO_PRODUCT_NAME = pmo_product_name;
                                    dv.FREEGIFT_NAME = freegift_name;
                                    dv.DELIVERY_NUM = delivery_num;
                                    result.Add(dv);

                                    string resultcode = "00";
                                    //에스크로일경우 이니시스에 배송등록실행
                                    SP_ADMIN_ORDERLIST_DETAIL_BODY_Result orderInfo = _adminorderservice.OrderDetailBodyInfo(Convert.ToInt32(order_idx));
                                    if (orderInfo.ESCROW_YN == "Y" && string.IsNullOrEmpty(orderInfo.INIESCROW_DELIVERY))
                                    {
                                        resultcode = InisysDeliveryUpdate(orderInfo, delivery_num);
                                    }

                                    if (resultcode == "00")
                                    {
                                        _adminorderservice.OrderDetailDeliveryUpdate(Convert.ToInt16(order_detail_idx), delivery_num, REG_ID);
                                    }

                                   
                                }
                            }

                        } //if end

                    } //end foreach
                }
                finally
                {
                    con.Close();
                }
            } //end if
            TempData["UploadResult"] = result;
            return RedirectToAction("Delivery");
          
        }

        //회원정보 > 주문내역
        public ActionResult OrderMemberList(string M_ID, int Page = 1)
        {
            int PageSize = 10;

            ORDER_MEMBER_MODEL M = new ORDER_MEMBER_MODEL
            {
                Page = Page,
                PageSize = PageSize,
                M_ID = M_ID,
                OrderCount = _adminorderservice.OrderMemberListCount(M_ID),
                OrderList = _adminorderservice.OrderMemberList(M_ID, Page, PageSize)
            };
            return View(M);
        }


        //에스크로 지불거절 확인
        [HttpPost]
        public ActionResult InipayEscrowDenyConfirm(int ORDER_IDX, SP_TB_ADMIN_ORDER_Param SEARCH_OPTION)
        {
            string REG_ID = AdminUserInfo.GetAdmId();
            SP_ADMIN_ORDERLIST_DETAIL_BODY_Result orderInfo = _adminorderservice.OrderDetailBodyInfo(ORDER_IDX);

            //###############################################################################
            //# 1. 객체 생성 #
            //################
            IINIpay INIpay = new IINIpay("50");


            //###############################################################################
            //# 2. 인스턴스 초기화 /3. 거래 유형 설정 #
            //######################
            INIpay.Initialize("escrow", "dcnf");										 // 고정 (절대 수정 불가)

            //###############################################################################
            //# 4. 정보 설정 #
            //################
            INIpay.SetPath(_inipay_setpath);
            INIpay.SetField("escrowtype", "dcnf");										// 고정 (절대 수정 불가)
            if (orderInfo.PAT_GUBUN.ToUpper() == "MOBILE")
            {
                INIpay.SetField("mid", _inipay_mobile_mid);	            // 상점아이디(모바일)
                INIpay.SetField("admin", _inipay_mobile_admin);			// 키패스워드(모바일)(상점아이디에 따라 변경)
            }
            else
            {
                INIpay.SetField("mid", _inipay_mid);	               		// 상점아이디
                INIpay.SetField("admin", _inipay_admin);					// 키패스워드(상점아이디에 따라 변경)
            }

            INIpay.SetField("tid", orderInfo.PAT_TID);								// 거래아이디
            INIpay.SetField("DCNF_Name", REG_ID);				            	// 거절확인자
            INIpay.SetField("uip", Request.UserHostAddress);							// 사용자 IP
            INIpay.SetField("debug", "true");											// 로그모드("true"로 설정하면 상세한 로그를 남김)


            //###############################################################################
            //# 5. 요청 처리 #
            //################
            INIpay.StartAction();


            //###############################################################################
            //6. 결과 정보 #
            //###############################################################################


            string result_tid = INIpay.GetResult("tid");							//거래번호
            string resultcode = INIpay.GetResult("resultcode");					//결과코드 ("00"이면 처리성공)
            string resultmsg = INIpay.GetResult("resultmsg");					//결과내용
            string DCNF_Date = INIpay.GetResult("DCNF_Date");					//처리 날짜
            string DCNF_Time = INIpay.GetResult("DCNF_Time");					//처리 시각

            //###############################################################################
            //# 7. 인스턴스 해제 #
            //####################
            INIpay.Destory();
            INIpay = null;

            if (resultcode == "00")
            {
                string setdate = DCNF_Date + " " + DCNF_Time;
                _adminorderservice.OrderEscrowResultSet(ORDER_IDX, "CANCEL_OK", setdate);

                RouteValueDictionary param = ConvertRouteValue(SEARCH_OPTION);
                param.Add("ORDER_IDX", ORDER_IDX);
                return RedirectToAction("Detail", param);
            }
            else
            {
                string result = "<script language='javascript'>alert('에러코드 : "+resultcode+", 에러메시지 : "+resultmsg+"');history.go(-1);</script>";
                return Content(result);
            }
        }
    }
}