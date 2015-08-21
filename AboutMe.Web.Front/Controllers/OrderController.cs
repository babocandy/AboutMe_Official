using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using Newtonsoft.Json;
using System.Collections;

using AboutMe.Domain.Service.Order;
using AboutMe.Domain.Entity.Order;
using AboutMe.Domain.Entity.Cart;
using AboutMe.Web.Front.Common.Filters;

using AboutMe.Common.Helper;
using INIpayNet;

namespace AboutMe.Web.Front.Controllers
{
    public class OrderController : BaseFrontController
    {
        private IOrderService _orderservice;

        public OrderController(IOrderService _orderservice)
        {
            this._orderservice = _orderservice;
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

        //이니페이에서사용
        private Hashtable hstCode_PayMethod = new Hashtable();
        private string _inipay_setpath = AboutMe.Common.Helper.Config.GetConfigValue("INIpay.SetPath");
        private string _inipay_mid = AboutMe.Common.Helper.Config.GetConfigValue("INIpay.Mid");
        private string _inipay_admin = AboutMe.Common.Helper.Config.GetConfigValue("INIpay.Admin");
        private string _inipay_debug = AboutMe.Common.Helper.Config.GetConfigValue("INIpay.Debug");

		//**** 지불수단별로 PGID를 다르게 표시한다 (2003.12.19: ts@inicis.com)        *
		//***************************************************************************
		//*** 하단의 Code_PayMethod 함수는 지불수단별로 TID를 별도로 표시하도록 하며,***
		//*** 임의로 수정하는 경우 지불 실패가 발생 될수 있으므로 절대로 수정***********
		//*** 하지 않도록 하시기 바랍니다. ********************************************
		//*** 임의로 수정하여 발생된 문제에 대해서는 (주)이니시스에 책임이 *************
		//*** 없으니 주의 하시기 바랍니다. ********************************************
		//***************************************************************************
		private void HashData()
		{
			hstCode_PayMethod["Card"]		= "CARD";
			hstCode_PayMethod["VCard"]		= "ISP_";
			hstCode_PayMethod["Account"]	= "ACCT";
			hstCode_PayMethod["DirectBank"] = "DBNK";
			hstCode_PayMethod["INIcard"]	= "INIC";
			hstCode_PayMethod["OCBPoint"]	= "OCBP";
			hstCode_PayMethod["MDX"]		= "MDX_";
			hstCode_PayMethod["HPP"]		= "HPP_";
			hstCode_PayMethod["Nemo"]		= "NEMO";
			hstCode_PayMethod["ArsBill"]	= "ARSB";
			hstCode_PayMethod["PhoneBill"]	= "PHNB";
			hstCode_PayMethod["Ars1588Bill"]= "1588";
			hstCode_PayMethod["VBank"]		= "VBNK";
			hstCode_PayMethod["Culture"]	= "CULT";
			hstCode_PayMethod["CMS"]		= "CMS_";
			hstCode_PayMethod["AUTH"]		= "AUTH";
		}


        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult InsertOrderStep1(string OrderList)
        {
            List<CART_INSERT> DataList = JsonConvert.DeserializeObject<List<CART_INSERT>>(OrderList);
            string P_CODE_LIST = "";
            string P_COUNT_LIST = "";

            string M_ID = _user_profile.M_ID;
            string SESSION_ID = _user_profile.SESSION_ID;

            foreach (CART_INSERT pData in DataList)
            {
                if (!string.IsNullOrEmpty(P_CODE_LIST))
                {
                    P_CODE_LIST += ",";
                    P_COUNT_LIST += ",";
                }
                P_CODE_LIST += pData.p_code;
                P_COUNT_LIST += pData.p_count;
            }
            Int32 Order_Idx = _orderservice.InsertOrderStep1(_user_profile.M_ID, _user_profile.SESSION_ID, P_CODE_LIST, P_COUNT_LIST);
            //this.TempData["Order_Idx"] = Order_Idx;
            return Content("<form name='mysubmitform' action='/Order/Step1' method='POST'><input type='hidden' name='ORDER_IDX' value='" + Order_Idx.ToString() + "'></form> <script language='javascript'>document.mysubmitform.submit();</script>");
        }

        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Step1(Int32 ORDER_IDX)
        {
            //ViewBag.Order_Idx = ORDER_IDX;
            ORDER_STEP1_MODEL M = new ORDER_STEP1_MODEL
            {
                OrderIdx = ORDER_IDX,
                UserProfile = _user_profile
            };
            return View(M);
        }

        public ActionResult Step1ProductList(Int32 OrderIdx)
        {
            ORDER_STEP1_ORDERLIST M = new ORDER_STEP1_ORDERLIST
            {
                OrderIdx = OrderIdx,
                UserProfile = _user_profile,
                OrderList = _orderservice.OrderStep1List(OrderIdx)

            };
            return PartialView(M);
        }
        
        public ActionResult Step1PriceInfo(Int32 OrderIdx)
        {
            ORDER_STEP1_PRICEINFO M = new ORDER_STEP1_PRICEINFO
            {
                OrderIdx = OrderIdx,
                UserProfile = _user_profile,
                PriceInfo = _orderservice.OrderStep1PriceInfo(OrderIdx)

            };
            return PartialView(M);
        }

        public ActionResult Step1DiscountInfo(Int32 OrderIdx)
        {
            SP_ORDER_STEP2_PRICE_INFO_Result priceInfo = _orderservice.OrderStep1PriceInfo(OrderIdx);
            string coupon_disable = "disabled";

            if (priceInfo.TOTAL_PAY_PRICE >= 30000)
            {
                coupon_disable = "disabled";
            }
            else
            {
                coupon_disable = "";
            }

            ORDER_STEP1_DISCOUNTINFO M = new ORDER_STEP1_DISCOUNTINFO
            {
                OrderIdx = OrderIdx,
                UserProfile = _user_profile,
                MyPoint = _orderservice.OrderStep1MyPoint(_user_profile.M_ID),
                TransCouponDisabled = coupon_disable,
                PriceInfo = _orderservice.OrderStep1PriceInfo(OrderIdx),
                DiscountInfo = _orderservice.OrderStep1DiscountInfo(OrderIdx),
                TransCouponList = _orderservice.OrderStep1CouponSelectList(_user_profile.M_ID,"TRANS","P")
            };
            return PartialView(M);
        }

        public ActionResult Step1AddressInfo(Int32 OrderIdx)
        {
            string M_ID = _user_profile.M_ID;
            SP_ORDER_STEP2_RECENTADDR_INFO_Result info = new SP_ORDER_STEP2_RECENTADDR_INFO_Result();
            SP_ORDER_STEP2_BASEADDR_INFO_Result baseinfo = new SP_ORDER_STEP2_BASEADDR_INFO_Result();

            if (_user_profile.IS_LOGIN == true)
            {
                info = _orderservice.OrderStep1RecentAddrInfo(M_ID);
                baseinfo = _orderservice.OrderStep1BaseAddrInfo(M_ID);
            }

            ORDER_STEP1_ADDRESSINFO M = new ORDER_STEP1_ADDRESSINFO
            {
                OrderIdx = OrderIdx,
                UserProfile = _user_profile,
                RecentAddrInfo = info,
                BaseAddrInfo= baseinfo
            };
            return PartialView(M);
        }

        public ActionResult SaveMemberAddress(string SENDER_POST1, string SENDER_POST2, string SENDER_ADDR1, string SENDER_ADDR2, 
            string SENDER_TEL1, string SENDER_TEL2, string SENDER_TEL3, string SENDER_HP1, string SENDER_HP2, string SENDER_HP3,
            string SENDER_EMAIL1, string SENDER_EMAIL2)
        {
            _orderservice.OrderStep1SaveMemberInfo(_user_profile.M_ID, SENDER_POST1, SENDER_POST2, SENDER_ADDR1, SENDER_ADDR2, SENDER_TEL1, SENDER_TEL2, SENDER_TEL3, SENDER_HP1, SENDER_HP2, SENDER_HP3, SENDER_EMAIL1, SENDER_EMAIL2);
            var jsonData = new { result = "true" };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Step1PayInfo(Int32 OrderIdx)
        {
            string M_ID = _user_profile.M_ID;
            INISYSPAY_MODEL InisysInfo = new INISYSPAY_MODEL();
            SP_ORDER_STEP2_PRICE_INFO_Result PriceInfo = _orderservice.OrderStep1PriceInfo(OrderIdx);
            if (PriceInfo.TOTAL_PAY_PRICE > 0)
            {
                InisysInfo = GetIniPayInitialize(PriceInfo.TOTAL_PAY_PRICE.ToString());
            }
            List<SP_ORDER_STEP2_PRODUCT_LIST_Result> OrderList = _orderservice.OrderStep1List(OrderIdx);
            string goodname = OrderList.FirstOrDefault().P_NAME;
            goodname = TagStrip(goodname);
            if (OrderList.Count() > 1) {
                goodname = goodname + "외 " + Convert.ToString(OrderList.Count() - 1) + "건";
            }
            ORDER_STEP1_PAYINFO M = new ORDER_STEP1_PAYINFO
            {
                OrderIdx = OrderIdx,
                UserProfile = _user_profile,
                GoodName = goodname,
                PriceInfo = PriceInfo,
                InisysStart = InisysInfo
            };
            return PartialView(M);
        }

        public ActionResult CouponPop(Int32 OrderIdx)
        {
            string M_ID = _user_profile.M_ID;
            //ViewBag.Order_Idx = ORDER_IDX;
            ORDER_STEP1_ORDERLIST M = new ORDER_STEP1_ORDERLIST
            {
                OrderIdx = OrderIdx,
                UserProfile = _user_profile,
                OrderList = _orderservice.OrderStep1List(OrderIdx)
            };
            return View(M);
        }

        public ActionResult ProductCouponList(string P_CODE)
        {
            List<SP_ORDER_STEP2_COUPON_SEARCH_Result> TransCouponList = _orderservice.OrderStep1CouponSelectList(_user_profile.M_ID, P_CODE, "P");
            var jsonData = new { result = "true", list= TransCouponList};
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SelectCouponOption(string P_CODE, int? COUPON_IDX)
        {
            string str = "<option RATE_OR_MONEY='' COUPON_MONEY='' COUPON_DISCOUNT_RATE='' value=''>쿠폰을 선택하세요</option>\n";
            List<SP_ORDER_STEP2_COUPON_SEARCH_Result> TransCouponList = _orderservice.OrderStep1CouponSelectList(_user_profile.M_ID, P_CODE, "P");
            //var jsonData = new { result = "true", list= TransCouponList};
            foreach (SP_ORDER_STEP2_COUPON_SEARCH_Result item in TransCouponList)
            {
                string seltxt = "";
                string coupon_desc = item.COUPON_NAME;
                if (!string.IsNullOrEmpty(item.BENEFIT)) {
                    coupon_desc = coupon_desc + "(" + item.BENEFIT + ")";
                }
                if (!string.IsNullOrEmpty(COUPON_IDX.ToString()))
                {
                    if (item.IDX_COUPON_NUMBER == COUPON_IDX)
                    {
                        seltxt = "selected='selected'";
                    }
                }
                str += "<option value='" + item.IDX_COUPON_NUMBER + "' " + seltxt + " RATE_OR_MONEY='" + item.RATE_OR_MONEY + "' COUPON_MONEY='" + item.COUPON_MONEY + "'" + " COUPON_DISCOUNT_RATE='" + item.COUPON_DISCOUNT_RATE + "'>" + coupon_desc + "</option>\n";
            }
            return Content(str);
        }

        
        [HttpPost]
        public ActionResult ApplyCoupon(string data)
        {
            List<COUPON_APPLY_MODEL> DataList = JsonConvert.DeserializeObject<List<COUPON_APPLY_MODEL>>(data);
            foreach (COUPON_APPLY_MODEL item in DataList)
            {
                Int32? o_idx = (string.IsNullOrEmpty(item.ORDER_DETAIL_IDX) ? 0 : Convert.ToInt32(item.ORDER_DETAIL_IDX));
                Int32? c_idx = (string.IsNullOrEmpty(item.COUPON_IDX) ? 0 : Convert.ToInt32(item.COUPON_IDX));

                _orderservice.OrderStep1CouponApply(_user_profile.M_ID, o_idx, c_idx);
            }
            var jsonData = new { result="true" };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ApplyPoint(int ORDER_IDX, int USE_POINT)
        {
            _orderservice.OrderStep1PointApply(_user_profile.M_ID, ORDER_IDX, USE_POINT);
            var jsonData = new { result = "true" };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public ActionResult ApplyTransCoupon(Int32 ORDER_IDX, Int32 COUPON_IDX)
        {
            _orderservice.OrderStep1TransCouponApply(_user_profile.M_ID, ORDER_IDX, COUPON_IDX);
            var jsonData = new { result = "true" };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult SaveReceiverAddress(Int32 ORDER_IDX, SENDER_RECEIVER_SAVE_Param Param)
        {
            
            Param.ORDER_MEMO = TagStrip(Param.ORDER_MEMO);
            Param.SENDER_NAME = TagStrip(Param.SENDER_NAME);
            Param.SENDER_POST1 = TagStrip(Param.SENDER_POST1);
            Param.SENDER_POST2 = TagStrip(Param.SENDER_POST2);
            Param.SENDER_ADDR1 = TagStrip(Param.SENDER_ADDR1);
            Param.SENDER_ADDR2 = TagStrip(Param.SENDER_ADDR2);
            Param.SENDER_TEL1 = TagStrip(Param.SENDER_TEL1);
            Param.SENDER_TEL2 = TagStrip(Param.SENDER_TEL2);
            Param.SENDER_TEL3 = TagStrip(Param.SENDER_TEL3);
            Param.SENDER_HP1 = TagStrip(Param.SENDER_HP1);
            Param.SENDER_HP2 = TagStrip(Param.SENDER_HP2);
            Param.SENDER_HP3 = TagStrip(Param.SENDER_HP3);
            Param.SENDER_EMAIL1 = TagStrip(Param.SENDER_EMAIL1);
            Param.SENDER_EMAIL2 = TagStrip(Param.SENDER_EMAIL2);

            Param.RECEIVER_NAME = TagStrip(Param.RECEIVER_NAME);
            Param.RECEIVER_POST1 = TagStrip(Param.RECEIVER_POST1);
            Param.RECEIVER_POST2 = TagStrip(Param.RECEIVER_POST2);
            Param.RECEIVER_ADDR1 = TagStrip(Param.RECEIVER_ADDR1);
            Param.RECEIVER_ADDR2 = TagStrip(Param.RECEIVER_ADDR2);
            Param.RECEIVER_TEL1 = TagStrip(Param.RECEIVER_TEL1);
            Param.RECEIVER_TEL2 = TagStrip(Param.RECEIVER_TEL2);
            Param.RECEIVER_TEL3 = TagStrip(Param.RECEIVER_TEL3);
            Param.RECEIVER_HP1 = TagStrip(Param.RECEIVER_HP1);
            Param.RECEIVER_HP2 = TagStrip(Param.RECEIVER_HP2);
            Param.RECEIVER_HP3 = TagStrip(Param.RECEIVER_HP3);

            _orderservice.OrderStep1SaveReceiverAddress(ORDER_IDX, Param);
            var jsonData = new { result = "true" };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        

        public ActionResult IniPayTest()
        {
            INISYSPAY_MODEL INISYSRESULT = new INISYSPAY_MODEL();

            //###############################################################################
            //# 1. 객체 생성 #
            //################
            IINIpay INIpay = new IINIpay("50");

            //###############################################################################
            //# 2. 인스턴스 초기화 /  3. 체크 유형 설정 #
            //######################
            //
            INIpay.Initialize("chkfake");

            INIpay.SetPath(_inipay_setpath);			// 이니페이 설치 디렉토리 C:\\INIpayNet50

            //###############################################################################
            //# 5. 암호화 처리 필드 세팅        #
            //###################################
            INIpay.SetField("mid", _inipay_mid);		// 상점 아이디 
            INIpay.SetField("admin", _inipay_admin);			// 상점 키패스워드
            INIpay.SetField("price", "1004");			// 결제 금액
            INIpay.SetField("nointerest", "no");		// 무이자 설정 세팅 
            INIpay.SetField("quotabase", "lumpsum:00:02:03:04:05:06:07:08:09:10:11:");//할부 개월 및 카드사별 무이자 세팅
            INIpay.SetField("currency", "WON");			// 통화단위
            INIpay.SetField("debug", _inipay_debug);	// 로그모드("true"로 설정하면 상세한 로그를 남김)

            //INIpay.SetField("test", "true");			 //Test

            //###############################################################################
            //# 5. 체크 처리를 위한 암호화 처리 #
            //###################################
            INIpay.StartAction();

            //###############################################################################
            //6. 암호화  결과 #
            //###############################################################################
            INISYSRESULT.resultcode = INIpay.GetResult("resultcode");		//결과코드 성공이면 '00' 실패 '01'
            INISYSRESULT.resultmsg = INIpay.GetResult("resultmsg");		//결과메세지 
            INISYSRESULT.rn_value = INIpay.GetResult("rn");				// 암호화 결과값
            INISYSRESULT.return_enc = INIpay.GetResult("return_enc");		// 암호화 결과값
            INISYSRESULT.ini_certid = INIpay.GetResult("ini_certid");		// 암호화 결과값
            INISYSRESULT.ini_encfield = INISYSRESULT.return_enc;

            //###############################################################################
            //7. RN 값 세션에 저장 #
            //###############################################################################
            Session["INI_RN"] = INISYSRESULT.rn_value; //	//RN값 => 결제 처리 페이지에서 체크 하기 위해 세션에 저장 (또는 DB에 저장)하여 다음 결제 처리 페이지 에서 체크)
            Session["INI_PRICE"] = "1004"; //결제 금액 =>  결제 처리 페이지에서 체크 하기 위해 세션에 저장 (또는 DB에 저장)하여 다음 결제 처리 페이지 에서

            //###############################################################################
            //# 8. 인스턴스 해제 #
            //###############################################################################
            INIpay.Destory();
            INIpay = null;

            //###############################################################################
            //# 9. 결제 페이지 생성 성공 유무에 대한 처리  #
            //###############################################################################
            if (!INISYSRESULT.resultcode.Equals("00"))
            {
                INISYSRESULT.err_msg = "결제 페이지 생성에 문제 발생<BR> ERR:" + INISYSRESULT.resultmsg;
            }

            return View(INISYSRESULT);
        }
        
        private INISYSPAY_MODEL GetIniPayInitialize(string payprice)
        {
            INISYSPAY_MODEL INISYSRESULT = new INISYSPAY_MODEL();

            //###############################################################################
            //# 1. 객체 생성 #
            //################
            IINIpay INIpay = new IINIpay("50");

            //###############################################################################
            //# 2. 인스턴스 초기화 /  3. 체크 유형 설정 #
            //######################
            //
            INIpay.Initialize("chkfake");

            INIpay.SetPath(_inipay_setpath);			// 이니페이 설치 디렉토리 C:\\INIpayNet50

            //###############################################################################
            //# 5. 암호화 처리 필드 세팅        #
            //###################################
            INIpay.SetField("mid", _inipay_mid);		// 상점 아이디 
            INIpay.SetField("admin", _inipay_admin);			// 상점 키패스워드
            INIpay.SetField("price", payprice);			// 결제 금액
            INIpay.SetField("nointerest", "no");		// 무이자 설정 세팅 
            INIpay.SetField("quotabase", "lumpsum:00:02:03:04:05:06:07:08:09:10:11:");//할부 개월 및 카드사별 무이자 세팅
            INIpay.SetField("currency", "WON");			// 통화단위
            INIpay.SetField("debug", _inipay_debug);			// 로그모드("true"로 설정하면 상세한 로그를 남김)

            //INIpay.SetField("test", "true");			 //Test

            //###############################################################################
            //# 5. 체크 처리를 위한 암호화 처리 #
            //###################################
            INIpay.StartAction();

            //###############################################################################
            //6. 암호화  결과 #
            //###############################################################################
            INISYSRESULT.resultcode = INIpay.GetResult("resultcode");		//결과코드 성공이면 '00' 실패 '01'
            INISYSRESULT.resultmsg = INIpay.GetResult("resultmsg");		//결과메세지 
            INISYSRESULT.rn_value = INIpay.GetResult("rn");				// 암호화 결과값
            INISYSRESULT.return_enc = INIpay.GetResult("return_enc");		// 암호화 결과값
            INISYSRESULT.ini_certid = INIpay.GetResult("ini_certid");		// 암호화 결과값
            INISYSRESULT.ini_encfield = INISYSRESULT.return_enc;

            //###############################################################################
            //7. RN 값 세션에 저장 #
            //###############################################################################
            Session["INI_RN"] = INISYSRESULT.rn_value; //	//RN값 => 결제 처리 페이지에서 체크 하기 위해 세션에 저장 (또는 DB에 저장)하여 다음 결제 처리 페이지 에서 체크)
            Session["INI_PRICE"] = payprice; //결제 금액 =>  결제 처리 페이지에서 체크 하기 위해 세션에 저장 (또는 DB에 저장)하여 다음 결제 처리 페이지 에서

            //###############################################################################
            //# 8. 인스턴스 해제 #
            //###############################################################################
            INIpay.Destory();
            INIpay = null;

            //###############################################################################
            //# 9. 결제 페이지 생성 성공 유무에 대한 처리  #
            //###############################################################################
            if (!INISYSRESULT.resultcode.Equals("00"))
            {
                INISYSRESULT.err_msg = "결제 페이지 생성에 문제 발생<BR> ERR:" + INISYSRESULT.resultmsg;
            }

            return INISYSRESULT;
        }

        //이니시스 결제 취소
        private INIPAYRESULT InipayCancelProcess(string Tid, string errMsg, string oid)
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

        //결제완료
        [HttpPost, ValidateInput(false)]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult OrderProcess(INISYSPAY_PARAM FrmData)
        {
            INISYSPAY_RESULT PayResult = new INISYSPAY_RESULT();
            INIPAYRESULT Result = new INIPAYRESULT();
            OrderResult orderResult = new OrderResult();
            HashData();

            FrmData.goodname = TagStrip(FrmData.goodname);
            FrmData.buyername = TagStrip(FrmData.buyername);
            FrmData.buyertel = TagStrip(FrmData.buyertel);
            FrmData.buyeremail = TagStrip(FrmData.buyeremail);
            FrmData.goodname = TagStrip(FrmData.goodname);

            //###############################################################################
            //# 1. 객체 생성 #
            //################
            IINIpay INIpay = new IINIpay("50");

            //###############################################################################
            //# 2. 인스턴스 초기화  / 3.거래 유형 설정#
            //######################
            INIpay.Initialize("securepay");

            //###############################################################################
            //# 4. 지불 정보 설정 #
            //###############################################################################

            string strpaymethod = FrmData.paymethod;

            if (hstCode_PayMethod.ContainsKey(strpaymethod))
            {
                strpaymethod = hstCode_PayMethod[strpaymethod].ToString();
            }
            string strprice = Session["INI_PRICE"].ToString();
            INIpay.SetPath(_inipay_setpath);			                    // 이니페이 설치 디렉토리 C:\\INIpayNet50
            INIpay.SetField("pgid", "INInet" + strpaymethod);				// PG ID (고정)
            INIpay.SetField("spgip", "203.238.3.10");						// 예비 PG IP (고정)
            INIpay.SetField("uid", FrmData.uid);					       // INIpay User ID(이니시스 내부변수 수정불가, 상점사용 user id 를 사용하지 마세요)
            INIpay.SetField("mid", _inipay_mid);						    // 상점아이디
            INIpay.SetField("rn", Session["INI_RN"].ToString());           // 결제 요청  페이지에서  세션에 저장 (또는 DB에 저장)한 것을 체크 하기 위해  결제 처리 페이지 에서 세팅)
            INIpay.SetField("price", strprice);								// 가격

            /**************************************************************************************************
            '* admin 은 키패스워드 변수명입니다. 수정하시면 안됩니다. 1111의 부분만 수정해서 사용하시기 바랍니다.
            '* 키패스워드는 상점관리자 페이지(https://iniweb.inicis.com)의 비밀번호가 아닙니다. 주의해 주시기 바랍니다.
            '* 키패스워드는 숫자 4자리로만 구성됩니다. 이 값은 키파일 발급시 결정됩니다. 
            '* 키패스워드 값을 확인하시려면 상점측에 발급된 키파일 안의 readme.txt 파일을 참조해 주십시오.
            '**************************************************************************************************/
            INIpay.SetField("admin", _inipay_admin);			//키패스워드(상점아이디에 따라 변경)

            INIpay.SetField("goodname", FrmData.goodname);		// 상품명
            INIpay.SetField("currency", "WON");					// 화폐단위
            INIpay.SetField("buyername", FrmData.buyername);	// 이용자 이름
            INIpay.SetField("buyertel", FrmData.buyertel);		// 이용자 이동전화
            INIpay.SetField("buyeremail", FrmData.buyeremail);	// 이용자 이메일
            INIpay.SetField("paymethod", FrmData.paymethod);	// 지불방법
            INIpay.SetField("encrypted", FrmData.encrypted);	// 암호문
            INIpay.SetField("sessionkey", FrmData.sessionkey);	// 암호문
            INIpay.SetField("url", "http://www.aboutme.com");	// 홈페이지 주소
            INIpay.SetField("debug", _inipay_debug);			// 로그모드(실서비스시에는 "false"로)
            INIpay.SetField("merchantreserved1", "예비1");	    // 예비필드1
            INIpay.SetField("merchantreserved2", "예비2");	    // 예비필드2  
            INIpay.SetField("merchantreserved3", "예비3");	    // 예비필드3

            ////*-----------------------------------------------------------------*
            // 수취인 정보 *                                                                    
            //-----------------------------------------------------------------*
            // 실물배송을 하는 상점의 경우에 사용되는 필드들이며       *
            // 아래의 값들은 INIsecurepaystart.aspx 페이지에서 포스트 되도록  *
            // 필드를 만들어 주도록 하십시요                          *
            // 컨텐츠 제공업체의 경우 삭제하셔도 무방합니다           *
            //-----------------------------------------------------------------*

            //INIpay.SetField("recvname", Request.Params["recvname"]);		 //수취인명
            //INIpay.SetField("recvtel", Request.Params["recvtel"]);			 //수취인 전화번호
            //INIpay.SetField("recvaddr", Request.Params["recvaddr"]);		 //수취인 주소
            //INIpay.SetField("recvpostnum", Request.Params["recvpostnum"]);	 //수취인 우편번호
            //INIpay.SetField("recvmsg", Request.Params["recvmsg"]);			 //수취인 전달 메세지

            //INIpay.SetField("tax", Request.Params["tax"]);		 //tax
            //INIpay.SetField("taxfree", Request.Params["taxfree"]);		 //taxfree
            //###############################################################################
            //# 5. 지불 요청 #
            //################
            INIpay.StartAction();		                                      //지불처리


            //###############################################################################
            //6. 지불 결과 #
            //###############################################################################
            //-------------------------------------------------------------------------------
            // 가.모든 결제 수단에 공통되는 결제 결과 내용
            //-------------------------------------------------------------------------------
            PayResult.Tid = INIpay.GetResult("Tid");							// 거래번호
            PayResult.Resultcode = INIpay.GetResult("Resultcode");				// 결과코드 ("00"이면 지불성공)
            PayResult.ResultMsg = INIpay.GetResult("ResultMsg");				// 결과내용 : resultMsg 는 결제실패시 추적할수 있는 단서입니다. 반드시 결과페이지에 출력하시기 바랍니다.
            PayResult.PayMethod = INIpay.GetResult("PayMethod");				// 지불방법 (매뉴얼 참조)
            PayResult.Moid = INIpay.GetResult("Moid");	                        // 상점 사용 주문번호

            //**********************************************************************************
            //* 결제결과금액 =>원상품가격과  결제결과금액과 비교하여 금액이 동일하지 않다면  
            //* 결제 금액의 위변조가 의심됨으로 정상적인 처리가 되지않도록 처리 바랍니다. (해당 거래 취소 처리) 
            //**********************************************************************************
            PayResult.GoodsPrice = INIpay.GetResult("TotPrice");                 //결제결과금액

            //원결제금액
            if (!strprice.Equals(PayResult.GoodsPrice))
            {
                // 결제금액 위변조가 된것입니다.
                //Response.Write("결재 금액 위변조");
                // 에러 처리 코드를 넣어 주시기 바랍니다.
            }

            //-------------------------------------------------------------------------------
            // 나. 신용카드,ISP,핸드폰, 전화 결제, 은행계좌이체, OK CASH BAG Point 결제시에만 결제 결과 내용  (무통장입금 , 문화 상품권 , 네모 제외) 
            //-------------------------------------------------------------------------------
            PayResult.ApplDate = INIpay.GetResult("ApplDate");		//이니시스 승인날짜
            PayResult.ApplTime = INIpay.GetResult("ApplTime");		//이니시스 승인시각

            //-------------------------------------------------------------------------------
            // 다. 신용카드  결제수단을 이용시에만  결제결과 내용
            //-------------------------------------------------------------------------------
            PayResult.ApplNum = INIpay.GetResult("ApplNum");			//신용카드 승인번호
            PayResult.CARD_Quota = INIpay.GetResult("CARD_Quota");		//할부기간
            PayResult.CARD_Interest = INIpay.GetResult("CARD_Interest");	//무이자할부 여부("1"이면 무이자할부)
            PayResult.CARD_Num = INIpay.GetResult("CARD_Num");			//카드번호 12자리
            PayResult.CARD_Code = INIpay.GetResult("CARD_Code");		//신용카드사 코드 메뉴얼이나 샘플폴더 안의 //카드사_은행_코드.txt// 파일을 참고하세요
            PayResult.CARD_BankCode = INIpay.GetResult("CARD_BankCode");	//신용카드 발급사(은행) 코드 (매뉴얼 참조)
            PayResult.CARD_AuthType = INIpay.GetResult("CARD_AuthType");	//본인인증 수행여부 ("00"이면 수행)
            PayResult.EventCode = INIpay.GetResult("EventCode");		//각종 이벤트 적용 여부

            //아래 내용은 "신용카드 및 OK CASH BAG 복합결제" 또는"신용카드 지불시에 OK CASH BAG적립"시에 추가되는 내용
            PayResult.OCB_ApplTime = INIpay.GetResult("OCB_ApplTime");		//OK Cashbag 적립 승인번호
            PayResult.OCB_SaveApplNum = INIpay.GetResult("OCB_SaveApplNum");	//OK Cashbag 적립 승인번호
            PayResult.OCB_PayApplNum = INIpay.GetResult("OCB_PayApplNum");	//OK Cashbag 사용 승인번호
            PayResult.OCB_ApplDate = INIpay.GetResult("OCB_ApplDate");		//OK Cashbag 승인일시
            PayResult.OCB_Num = INIpay.GetResult("OCB_Num");			//OK Cashbag 번호
            PayResult.CARD_ApplPrice = INIpay.GetResult("CARD_ApplPrice");	//OK Cashbag 복합결재시 신용카드 지불금액
            PayResult.OCB_PayPrice = INIpay.GetResult("OCB_PayPrice");		//OK Cashbag 복합결재시 포인트 지불금액


            //-------------------------------------------------------------------------------
            // 라. 은행계좌이체 결제수단을 이용시에만  결제결과 내용
            //	오직 은행계좌시에만 실시 현금 영수증 발행이 가능하며, 가상계좌는 상점관리자 화면이나, 독립적인 현금영수증 발행(이니시스 기술자료실) 모듈을 사용하세요
            //-------------------------------------------------------------------------------
            PayResult.ACCT_BankCode = INIpay.GetResult("ACCT_BankCode");	//은행코드
            PayResult.rcash_rslt = INIpay.GetResult("rcash_rslt");		//현금영주증 결과코드 ("0000"이면 지불성공)
            PayResult.ruseopt = INIpay.GetResult("ruseopt");			//현금영수증 발행구분코드 


            //-------------------------------------------------------------------------------
            // 마. 무통장 입금(가상계좌) 결제수단을 이용시 결과 내용
            //-------------------------------------------------------------------------------
            PayResult.VACT_Num = INIpay.GetResult("VACT_Num"); 		// 입금 계좌 번호
            PayResult.VACT_BankCode = INIpay.GetResult("VACT_BankCode"); 	// 입금 은행 코드
            PayResult.VACT_Date = INIpay.GetResult("VACT_Date"); 		// 입금 예정 날짜
            PayResult.VACT_Time = INIpay.GetResult("VACT_Time"); 		// 입금 예정 시간 [ 20061018 ]
            PayResult.VACT_InputName = INIpay.GetResult("VACT_InputName"); 	// 송금자명
            PayResult.VACT_Name = INIpay.GetResult("VACT_Name");		// 예금주명

            //-------------------------------------------------------------------------------
            // 바. 핸드폰, 전화결제시에만  결제 결과 내용 ( "실패 내역 자세히 보기"에서 필요 , 상점에서는 필요없는 정보임)
            //-------------------------------------------------------------------------------
            PayResult.HPP_GWCode = INIpay.GetResult("HPP_GWCode");		//핸드폰,전화결제시 gateway


            //-------------------------------------------------------------------------------
            // 사. 핸드폰 결제수단을 이용시에만  결제 결과 내용
            //-------------------------------------------------------------------------------
            PayResult.HPP_Num = INIpay.GetResult("HPP_Num");			//핸드폰 결제에 사용된 휴대폰번호


            //-------------------------------------------------------------------------------
            // 아. ARS 전화 결제수단을 이용시에만  결제 결과 내용
            //-------------------------------------------------------------------------------
            PayResult.ARSB_Num = INIpay.GetResult("ARSB_Num");			//전화결제에  사용된 전화번호

            // 자. 받는 전화 결제수단을 이용시에만  결제 결과 내용
            //-------------------------------------------------------------------------------
            PayResult.PHNB_Num = INIpay.GetResult("PHNB_Num");			//전화결제에  사용된 전화번호

            //-------------------------------------------------------------------------------
            // 차. 문화 상품권 결제수단을 이용시에만  결제 결과 내용	
            //-------------------------------------------------------------------------------
            PayResult.CULT_UserID = INIpay.GetResult("CULT_UserID");		//문화상품권 ID

            //-------------------------------------------------------------------------------
            // 카. 현금영수증 발급 결과코드 (은행계좌이체시에만 리턴);
            //-------------------------------------------------------------------------------
            PayResult.CSHR_ResultCode = INIpay.GetResult("CSHR_ResultCode");	// 결과코드
            PayResult.CSHR_Type = INIpay.GetResult("CSHR_Type");		//발급구분코드

            //-------------------------------------------------------------------------------
            // 파. 틴캐시 결제수단을 이용시에만 결제 결과 내용
            //-------------------------------------------------------------------------------
            PayResult.TEEN_Remains = INIpay.GetResult("TEEN_Remains");		//틴캐시 잔액
            PayResult.TEEN_UserID = INIpay.GetResult("TEEN_UserID");		//틴캐시 ID

            //-------------------------------------------------------------------------------
            // 타.스마트문상(게임 문화 상품권) 결제수단을 이용시에만 결제 결과 내용
            //-------------------------------------------------------------------------------
            PayResult.GAMG_Cnt = INIpay.GetResult("GAMG_Cnt");			//카드 사용 갯수

            //-------------------------------------------------------------------------------
            // 하. 도서문화 상품권 결제수단을 이용시에만 결제 결과 내용
            //-------------------------------------------------------------------------------
            PayResult.BCSH_UserID = INIpay.GetResult("BCSH_UserID");		//문화상품권 ID

            //-------------------------------------------------------------------------------
            // * . 모든 결제 수단에 대해 결제 실패시에만 결제 결과 데이터 				
            //-------------------------------------------------------------------------------
            PayResult.ResultErrorCode = INIpay.GetResult("ResultErrorCode");	//결제실패시 상세에러코드 

            INIpay.Destory();
            INIpay = null;

            Result.Resultcode = PayResult.Resultcode;
            Result.ResultMsg = PayResult.ResultMsg;
            Result.ResultErrorCode = PayResult.ResultErrorCode;
            
            /*###############################################################################
            # 지불결과 DB 연동 부분 #
            # 지불결과를 디비처리하시고 디비처리시 실패가 나시면 이니시스에 취소요청을 합니다
            ###############################################################################*/
            string PAY_GBN = "";
            string order_code = "";
            // * 데이터베이스 처리부분 삽입
            // * 처리 실패시 아래 주석부분을 풀면, 이니시스에 해당 거래를 취소요청합니다
            if (PayResult.Resultcode == "00") //00성공
            {
                ORDER_PAY_PARAM Param = new ORDER_PAY_PARAM();
                Param.ORDER_IDX = Convert.ToInt32(FrmData.oid);
                Param.CARD_GBN = PayResult.CARD_Code;
                Param.INSTLMT_AT = PayResult.CARD_Interest; //할부여부
                Param.PAT_TID = PayResult.Tid;
                if (FrmData.PAY_GBN == "2") ///실시간계좌
                {
                    Param.REAL_ACCOUNT_AT = "1";
                    Param.BANK_CODE = PayResult.ACCT_BankCode; //은행코드
                    PAY_GBN = "2";
                    Param.ESCROW_YN = "N";
                }
                else if (FrmData.PAY_GBN == "20") //실시간계좌이체 (에스크로)
                {
                    Param.REAL_ACCOUNT_AT = "1";
                    Param.BANK_CODE = PayResult.ACCT_BankCode; //은행코드
                    PAY_GBN = "2";
                    Param.ESCROW_YN = "Y";
                }
                else //카드
                {
                    Param.REAL_ACCOUNT_AT = "0";
                    Param.BANK_CODE = PayResult.CARD_BankCode; //은행코드
                    Param.ESCROW_YN = "N";
                    PAY_GBN = "1";
                }

                if (PayResult.rcash_rslt == "0000") //현금영수증 발행
                {
                    Param.CASHRECEIPT_SE_CODE = "1";
                    Param.CASHRECEIPT_RESULT_CODE = PayResult.ruseopt;
                }
                else
                {
                    Param.CASHRECEIPT_SE_CODE = "0";
                    Param.CASHRECEIPT_RESULT_CODE = "0";
                }
                Param.HTTP_USER_AGENT = Request.UserAgent;
                Param.PAT_GUBUN = "Web";
                Param.SVR_DOMAIN = HttpContext.Request.Url.Host;

                Param.VACT_Num = PayResult.VACT_Num;
                Param.VACT_BankCode = PayResult.VACT_BankCode;
                Param.VACT_Name = PayResult.VACT_Name;
                Param.VACT_Date = PayResult.VACT_Date;
                Param.VACT_Time = PayResult.VACT_Time;
                Param.PAY_GBN = PAY_GBN;
                Param.ORDER_STATUS_VALUE = "20"; //결제완료
                try
                {
                    //DB 저장
                    order_code = _orderservice.OrderPaySave(Param);

                    //실행결과
                    orderResult.Resultcode = Result.Resultcode;
                    orderResult.ResultMsg = Result.ResultMsg;
                    orderResult.ResultErrorCode = Result.ResultErrorCode;
                    orderResult.ORDER_IDX = Convert.ToInt32(FrmData.oid);
                    orderResult.PAY_GBN = PAY_GBN;
                    orderResult.ORDER_CODE = order_code;

                    //메일발송
                    if (PayResult.Resultcode == "00" && !string.IsNullOrEmpty(orderResult.ORDER_CODE))
                    {
                        SendOrderResultMail(orderResult.ORDER_CODE);
                    }

                }
                catch (Exception e)
                {
                    //Rollback

                    string errMsg = "AboutMe DB 입력시 에러"; //취소사유
                    INIPAYRESULT cancel = InipayCancelProcess(PayResult.Tid, errMsg, PayResult.Moid);
                    if (cancel.Resultcode.Equals("00"))
                    {
                        Result.Resultcode = "01";
                        Result.ResultMsg = "AboutMe DB입력중 에러(err : " + e.Message + ")";
                    }
                    else
                    {
                        Result.Resultcode = "09";
                        Result.ResultMsg = "AboutMe DB입력중 에러(err : " + e.Message + ")로 Inisys 취소중 에러발생! ";
                    }

                    orderResult.Resultcode = Result.Resultcode;
                    orderResult.ResultMsg = Result.ResultMsg;
                    orderResult.ResultErrorCode = Result.ResultErrorCode;
                    orderResult.ORDER_IDX = Convert.ToInt32(FrmData.oid);
                    orderResult.PAY_GBN = PAY_GBN;
                    orderResult.ORDER_CODE = order_code;
                }
            }
            else
            {
                orderResult.Resultcode = Result.Resultcode;
                orderResult.ResultMsg = Result.ResultMsg;
                orderResult.ResultErrorCode = Result.ResultErrorCode;
                orderResult.ORDER_IDX = Convert.ToInt32(FrmData.oid);
                orderResult.PAY_GBN = "";
                orderResult.ORDER_CODE ="";
            }
            
           

            StringBuilder SBuilder = new StringBuilder();
            SBuilder.Append("<form name='mysubmitform' action='/Order/OrderResult' method='POST'>\n");
            SBuilder.AppendFormat("<input type='hidden' name='{0}' value='{1}'>\n","ORDER_IDX" , orderResult.ORDER_IDX.ToString() );
            SBuilder.AppendFormat("<input type='hidden' name='{0}' value='{1}'>\n", "PAY_GBN", PAY_GBN);
            SBuilder.AppendFormat("<input type='hidden' name='{0}' value='{1}'>\n", "ORDER_CODE", orderResult.ORDER_CODE);
            SBuilder.AppendFormat("<input type='hidden' name='{0}' value='{1}'>\n", "Resultcode", orderResult.Resultcode);
            SBuilder.AppendFormat("<input type='hidden' name='{0}' value='{1}'>\n", "ResultMsg", orderResult.ResultMsg);
            SBuilder.AppendFormat("<input type='hidden' name='{0}' value='{1}'>\n", "ResultErrorCode", orderResult.ResultErrorCode);
            SBuilder.Append("</form>\n");
            SBuilder.Append("<script language='javascript'>document.mysubmitform.submit();</script>\n");
            return Content(SBuilder.ToString());
        }

        public ActionResult OrderResult(OrderResult result) {
            ORDER_STEP2_MODEL M = new ORDER_STEP2_MODEL();
            M.OrderResult = result;
            M.UserProfile = _user_profile;
            M.ProductList = _orderservice.OrderResultProductList(result.ORDER_CODE, _user_profile.M_ID, _user_profile.SESSION_ID);
            M.DetailInfo = _orderservice.OrderResultDetailInfo(result.ORDER_CODE, _user_profile.M_ID, _user_profile.SESSION_ID);
            return View(M);
        }

        //주문완료 메일발송
        private void SendOrderResultMail(string ORDER_CODE)
        {
            List<SP_ORDER_RESULT_PRODUCT_LIST_Result> ProductList = _orderservice.OrderResultProductList(ORDER_CODE, _user_profile.M_ID, _user_profile.SESSION_ID);
            SP_ORDER_RESULT_DETAIL_Result DetailInfo = _orderservice.OrderResultDetailInfo(ORDER_CODE, _user_profile.M_ID, _user_profile.SESSION_ID);
            string productImgPath = AboutMe.Common.Helper.Config.GetConfigValue("ProductPhotoPath");

            if (DetailInfo != null)
            {
                string mail_skin_path = System.AppDomain.CurrentDomain.BaseDirectory + "aboutCom\\MailSkin\\"; //메일스킨 경로
                string cur_domain = HttpContext.Request.Url.GetLeftPart(UriPartial.Authority);  //도메인 ex http://www.aaa.co.kr:1234
                string skin_body = Utility01.GetTextResourceFile(mail_skin_path + "mail_orderend.html");  
                skin_body = skin_body.Replace("$$DOMAIN$$", cur_domain);  //도메인
                skin_body = skin_body.Replace("$$ORDER_NAME$$", DetailInfo.ORDER_NAME);
                
                //주문목록
                StringBuilder ListBuilder = new StringBuilder();
                foreach(SP_ORDER_RESULT_PRODUCT_LIST_Result item in ProductList)
                {
                    string imgUrl1 = cur_domain+productImgPath + "R120_" + item.P_IMG1_S;
                    string sellingPrice = "";
                    if (item.SELLING_PRICE != item.DISCOUNT_PRICE)
                    {
                        sellingPrice = String.Format("<p style=\"margin:0 0 3px 0;font-size:13px;color:#696361;text-decoration:line-through;\">{0}원</p>", String.Format("{0:#,#0.}", item.SELLING_PRICE));
                    }
                    string oneplus = "";
                    if (item.PROMOTION_TYPE == "02") //1+1
                    {
                        oneplus = "(1+1상품)";
                    }
                    ListBuilder.Append("<tr>");
                    ListBuilder.AppendFormat("<td width=\"136\" height=\"128\" bgcolor=\"#ffffff\" align=\"right\" style=\"padding:0 17px 0 0;border-bottom:1px solid #efe8e6;\"><img src=\"{0}\" alt=\"\" /></td>", imgUrl1);
                    ListBuilder.AppendFormat("<td width=\"185\" bgcolor=\"#ffffff\" align=\"left\" style=\"padding:0;font:11px/1.2 Dotum,'돋움';color:#bbb5b4;border-bottom:1px solid #efe8e6;\"><p style=\"margin:13px 0 4px;padding:0;font-size:14px;color:#1b1818;\">{0}</p><p style=\"margin:0;padding:0;\">{1}</p></td>", item.P_NAME, item.P_SUB_TITLE);
                    ListBuilder.AppendFormat("<td width=\"121\" bgcolor=\"#ffffff\" align=\"center\" style=\"font:16px/1.3 Dotum,'돋움';color:#1b1818;border-bottom:1px solid #efe8e6;\">{0}<b style=\"font-family:'Verdana';\">{1}</b>원</td>", sellingPrice, String.Format("{0:#,#0.}", item.DISCOUNT_PRICE));
                    ListBuilder.AppendFormat("<td width=\"70\" bgcolor=\"#ffffff\" align=\"center\" style=\"font:16px/1.3 Dotum,'돋움';color:#1b1818;border-bottom:1px solid #efe8e6;\"><b>{0}</b> {1}</td>", Convert.ToString(item.P_COUNT), oneplus);
                    ListBuilder.AppendFormat("<td width=\"130\" bgcolor=\"#ffffff\" align=\"center\" style=\"font:bold 16px/1.3 Dotum,'돋움';color:#9aca3c;border-bottom:1px solid #efe8e6;letter-spacing:1px;\">ⓟ&nbsp;<b>{0}</b></td>", String.Format("{0:#,#0.}", item.POINT_ACCML));
                    ListBuilder.AppendFormat("<td width=\"120\" bgcolor=\"#ffffff\" align=\"center\" style=\"font:16px/1.3 Dotum,'돋움';color:#252626;border-bottom:1px solid #efe8e6;\"><b style=\"font-family:'Verdana';\">{0}</b>원</td>", String.Format("{0:#,#0.}", item.COUPON_DISCOUNT_PRICE));
                    ListBuilder.Append("</tr>");	
                }
                skin_body = skin_body.Replace("$$PRODUCT_LIST$$", ListBuilder.ToString());
   														
                skin_body = skin_body.Replace("$$TOTAL_SUM_PRICE$$", String.Format("{0:#,#0.}", DetailInfo.TOTAL_PRICE + DetailInfo.TRANS_PRICE));
                skin_body = skin_body.Replace("$$TOTAL_PRICE$$", String.Format("{0:#,#0.}", DetailInfo.TOTAL_PRICE));
                skin_body = skin_body.Replace("$$TRANS_PRICE$$", String.Format("{0:#,#0.}", DetailInfo.TRANS_PRICE));

                //전체할인금액
                skin_body = skin_body.Replace("$$DISCOUNT_AMT$$", String.Format("{0:#,#0.}", DetailInfo.DISCOUNT_AMT));

                //임직원할인
                string empPriceStr = "";
                if (_user_profile.IS_LOGIN == true && _user_profile.M_GBN == "S")
                {
                    empPriceStr = "<tr><td width=\"100\" style=\"padding:0 0 8px 0;font:14px/1.2 Dotum,'돋움';color:#696361;letter-spacing:-1px;\">임직원 할인</td>";
                    empPriceStr += "<td align=\"right\" style=\"padding:0 0 8px 3px;font:14px/1.2 Dotum,'돋움';color:#696361;letter-spacing:-1px;\"><b>"+String.Format("{0:#,#0.}", DetailInfo.EMP_DISCOUNT_AMT)+"</b> 원</td></tr>";
                }
                skin_body = skin_body.Replace("$$EMP_DISCOUNT$$", empPriceStr);

                skin_body = skin_body.Replace("$$COUPON_DISCOUNT_AMT$$", String.Format("{0:#,#0.}", DetailInfo.COUPON_DISCOUNT_AMT));
                skin_body = skin_body.Replace("$$POINT_USE_PRICE$$", String.Format("{0:#,#0.}", DetailInfo.POINT_USE_PRICE));
                skin_body = skin_body.Replace("$$GRADE_DISCOUNT_AMT$$", String.Format("{0:#,#0.}", DetailInfo.GRADE_DISCOUNT_AMT));
                skin_body = skin_body.Replace("$$TOTAL_PAY_PRICE$$", String.Format("{0:#,#0.}", DetailInfo.TOTAL_PAY_PRICE));
                skin_body = skin_body.Replace("$$ACCML_POINT$$", String.Format("{0:#,#0.}", DetailInfo.ACCML_POINT));
                
                //주문자 & 배송지정보
                skin_body = skin_body.Replace("$$SENDER_POST$$",  DetailInfo.SENDER_POST);
                skin_body = skin_body.Replace("$$SENDER_ADDR1$$",  DetailInfo.SENDER_ADDR1);
                skin_body = skin_body.Replace("$$SENDER_ADDR2$$",  DetailInfo.SENDER_ADDR2);
                skin_body = skin_body.Replace("$$SENDER_TEL$$",  DetailInfo.SENDER_TEL);
                skin_body = skin_body.Replace("$$SENDER_HP$$",  DetailInfo.SENDER_HP);
                skin_body = skin_body.Replace("$$SENDER_EMAIL$$",  DetailInfo.SENDER_EMAIL);
                skin_body = skin_body.Replace("$$RECEIVER_NAME$$",  DetailInfo.RECEIVER_NAME);
                skin_body = skin_body.Replace("$$RECEIVER_POST$$",  DetailInfo.RECEIVER_POST);
                skin_body = skin_body.Replace("$$RECEIVER_ADDR1$$",  DetailInfo.RECEIVER_ADDR1);
                skin_body = skin_body.Replace("$$RECEIVER_ADDR2$$",  DetailInfo.RECEIVER_ADDR2);
                skin_body = skin_body.Replace("$$RECEIVER_HP$$",  DetailInfo.RECEIVER_HP);
                skin_body = skin_body.Replace("$$RECEIVER_TEL$$",  DetailInfo.RECEIVER_TEL);
                skin_body = skin_body.Replace("$$ORDER_MEMO$$",  DetailInfo.ORDER_MEMO);

                skin_body = skin_body.Replace("$$ORDER_CODE$$",  DetailInfo.ORDER_CODE);
                skin_body = skin_body.Replace("$$PAT_TID$$",  DetailInfo.PAT_TID);
                skin_body = skin_body.Replace("$$PAY_GBN_NM$$",  DetailInfo.PAY_GBN_NM);
                if (!string.IsNullOrEmpty(DetailInfo.NOMEMBER_PASS))
                {
                    skin_body = skin_body.Replace("$$NOMEMBER_PASS_TEXT$$",  "비회원비밀번호");
                    skin_body = skin_body.Replace("$$NOMEMBER_PASS$$",  DetailInfo.NOMEMBER_PASS);
                }
                else
                {
                    skin_body = skin_body.Replace("$$NOMEMBER_PASS_TEXT$$",  "");
                    skin_body = skin_body.Replace("$$NOMEMBER_PASS$$", "");
                }

                //가상계좌정보
                if (DetailInfo.PAY_GBN == "3") //가상계좌
                { 
                    StringBuilder VactBuilder = new StringBuilder();
                    VactBuilder.Append("<tr>");
					VactBuilder.Append("<td width=\"130\" bgcolor=\"#faf7f6\" align=\"left\" valign=\"top\" style=\"padding:15px 0 15px 30px;font:12px/1.2 Dotum,'돋움';color:#1b1818;\">가상계좌번호</td>");
					VactBuilder.AppendFormat("<td width=\"220\" bgcolor=\"#faf7f6\" align=\"left\" style=\"padding:15px 0 15px 10px;font:12px/1.2 Dotum,'돋움';color:#252626;\">{0}</td>",DetailInfo.VACT_Num);
					VactBuilder.Append("<td width=\"130\" bgcolor=\"#faf7f6\" align=\"left\" valign=\"top\" style=\"padding:15px 0 15px 30px;font:12px/1.2 Dotum,'돋움';color:#1b1818;border-left:1px solid #ffffff;\">입금은행</td>");
				    VactBuilder.AppendFormat("<td width=\"220\" bgcolor=\"#faf7f6\" align=\"left\" style=\"padding:15px 0 15px 10px;font:12px/1.2 Dotum,'돋움';color:#252626;\">{0}</td>",DetailInfo.VACT_BankName);
					VactBuilder.Append("</tr>");
                    VactBuilder.Append("<tr>");
                    VactBuilder.Append("<td width=\"130\" bgcolor=\"#faf7f6\" align=\"left\" valign=\"top\" style=\"padding:15px 0 15px 30px;font:12px/1.2 Dotum,'돋움';color:#1b1818;\">예금주명</td>");
					VactBuilder.AppendFormat("<td width=\"220\" bgcolor=\"#faf7f6\" align=\"left\" style=\"padding:15px 0 15px 10px;font:12px/1.2 Dotum,'돋움';color:#252626;\">{0}</td>",DetailInfo.VACT_Num);
                    VactBuilder.Append("<td width=\"130\" bgcolor=\"#faf7f6\" align=\"left\" valign=\"top\" style=\"padding:15px 0 15px 30px;font:12px/1.2 Dotum,'돋움';color:#1b1818;border-left:1px solid #ffffff;\">입금예정일</td>");
				    VactBuilder.AppendFormat("<td width=\"220\" bgcolor=\"#faf7f6\" align=\"left\" style=\"padding:15px 0 15px 10px;font:12px/1.2 Dotum,'돋움';color:#252626;\">{0}까지</td>",DetailInfo.VACT_Date);
			        VactBuilder.Append("</tr>");
                    skin_body = skin_body.Replace("$$VACT_INFO$$",  VactBuilder.ToString());
                }
                else
                {
                    skin_body = skin_body.Replace("$$VACT_INFO$$","");
                }

                string M_EMAIL = DetailInfo.SENDER_EMAIL; //수신자이메일

                string MAIL_SUBJECT = "[AboutMe] " + DetailInfo.ORDER_NAME+ "님이 결제하신 주문이 완료되었습니다.";
                string MAIL_BODY = skin_body;

                //메일 발송을 위한 발송정보 준비 ----------------------------------------------------
                string MAIL_SENDER_EMAIL = Config.GetConfigValue("MAIL_SENDER_EMAIL"); //noreply@cstone.co.kr
                string MAIL_SENDER_PW = Config.GetConfigValue("MAIL_SENDER_PW"); //cstonedev12
                string MAIL_SENDER_SMTP_SERVER = Config.GetConfigValue("MAIL_SENDER_SMTP_SERVER"); //smtp.gmail.com
                string MAIL_SENDER_SMTP_PORT = Config.GetConfigValue("MAIL_SENDER_SMTP_PORT"); //587
                string MAIL_SENDER_SMTP_TIMEOUT = Config.GetConfigValue("MAIL_SENDER_SMTP_TIMEOUT"); //20000

                //메일 발송
                MailSender mObj = new MailSender();
                mObj.MailSendAction(MAIL_SENDER_EMAIL, MAIL_SENDER_PW, MAIL_SENDER_SMTP_SERVER, MAIL_SENDER_SMTP_PORT, MAIL_SENDER_SMTP_TIMEOUT, M_EMAIL, MAIL_SUBJECT, MAIL_BODY);
            }
            
        }
    }
}