using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Collections;

using System.Net;
using AboutMe.Domain.Service.Order;
using AboutMe.Domain.Entity.Order;
using AboutMe.Domain.Entity.Cart;
using AboutMe.Web.Mobile.Common.Filters;

using AboutMe.Common.Helper;
using INIpayNet;

namespace AboutMe.Web.Mobile.Controllers
{
    public class OrderController : BaseMobileController
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
        private string _inipay_mid = AboutMe.Common.Helper.Config.GetConfigValue("INIpay.Mobile.Mid");
        private string _inipay_admin = AboutMe.Common.Helper.Config.GetConfigValue("INIpay.Mobile.Admin");
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

        private void filelog(string msg, bool isline=true)
        {
            string sourceRootDir = _inipay_setpath + "/ini_log";
            DateTime dt2 = new DateTime();
            dt2 = DateTime.Now;
            string now_dt = dt2.ToString("yyyyMMdd");
            string now_time = dt2.ToString("yyyy-MM-dd HHmms");


            string sourceDir = sourceRootDir + "/" + now_dt;
            string file_name = "gxen_" + now_dt + ".log";

            if (!Directory.Exists(sourceRootDir))
            {
                Directory.CreateDirectory(sourceRootDir);
            }

            if (!Directory.Exists(sourceDir))
            {
                Directory.CreateDirectory(sourceDir);
            }

            if (!System.IO.File.Exists(sourceDir + "/" + file_name))
            {
                using (System.IO.FileStream fs = System.IO.File.Create(sourceDir + "/" + file_name))
                {
                   
                }
            }

            using (FileStream fs = new FileStream(sourceDir + "/" + file_name, FileMode.Append, FileAccess.Write))
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fs, System.Text.Encoding.UTF8))
            {
                if (isline) {
                    file.WriteLine("---------"+now_time+" ------------------------------------------------------------------------------");
                }
                file.WriteLine("실행시간 : " + now_time);
                file.WriteLine(msg);
            }

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
         /*[OutputCache(NoStore = true, Duration = 0)]*/
        public ActionResult Step1(Int32 ORDER_IDX)
        {

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

        public ActionResult SaveMemberAddress(string SENDER_POST, string SENDER_ADDR1, string SENDER_ADDR2, 
            string SENDER_TEL1, string SENDER_TEL2, string SENDER_TEL3, string SENDER_HP1, string SENDER_HP2, string SENDER_HP3,
            string SENDER_EMAIL1, string SENDER_EMAIL2)
        {
            _orderservice.OrderStep1SaveMemberInfo(_user_profile.M_ID, SENDER_POST, SENDER_ADDR1, SENDER_ADDR2, SENDER_TEL1, SENDER_TEL2, SENDER_TEL3, SENDER_HP1, SENDER_HP2, SENDER_HP3, SENDER_EMAIL1, SENDER_EMAIL2);
            var jsonData = new { result = "true" };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Step1PayInfo(Int32 OrderIdx)
        {
            string M_ID = _user_profile.M_ID;

            SP_ORDER_STEP2_PRICE_INFO_Result PriceInfo = _orderservice.OrderStep1PriceInfo(OrderIdx);
            List<SP_ORDER_STEP2_PRODUCT_LIST_Result> OrderList = _orderservice.OrderStep1List(OrderIdx);
            string goodname = OrderList.FirstOrDefault().P_NAME;
            goodname = TagStrip(goodname);
            if (OrderList.Count() > 1) {
                goodname = goodname + "외 " + Convert.ToString(OrderList.Count() - 1) + "건";
            }

            string strHTTP_DOMAIN = AboutMe.Common.Helper.Config.GetConfigValue("MobileUrl"); ; 

            //string localurl = Request.Url.Authority;
            //string schema = Request.Url.Scheme;
            string p_next_url = strHTTP_DOMAIN + "/Order/InipayNext";      //2 Transaction 방식 (신용카드)
            string p_noti_url = strHTTP_DOMAIN + "/Order/InipayNoti";      //1 Transaction 방식 (가상계좌, 실시간계좌)
            string p_return_url = strHTTP_DOMAIN + "/Order/OrderReturn?ORDER_IDX=" + Convert.ToString(OrderIdx);  //1 Transaction 방식 (가상계좌, 실시간계좌)
            
            ORDER_STEP1_MOBILE_PAYINFO M = new ORDER_STEP1_MOBILE_PAYINFO
            {
                OrderIdx = OrderIdx,
                UserProfile = _user_profile,
                GoodName = goodname,
                PriceInfo = PriceInfo,
                Mid = _inipay_mid,
                TotalPrice = Convert.ToString(PriceInfo.TOTAL_PAY_PRICE),
                P_NEXT_URL = p_next_url,
                P_NOTI_URL = p_noti_url,
                P_RETURN_URL = p_return_url
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
            Param.SENDER_POST = TagStrip(Param.SENDER_POST);
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
            Param.RECEIVER_POST = TagStrip(Param.RECEIVER_POST);
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
            return View();
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

        public ActionResult OrderResult(OrderResult result)
        {
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
                string cur_domain = AboutMe.Common.Helper.Config.GetConfigValue("FrontUrl"); // HttpContext.Request.Url.GetLeftPart(UriPartial.Authority);  //도메인 ex http://www.aaa.co.kr:1234
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


        public SP_ORDER_GET_ORDERCODE_BY_TMP_ORDERIDX_Result SaveOrderDB(string CallFunction, INIPAYMOBILE_CALL_RETURN DataList)
        {
            string log = "";
            log = log + Newtonsoft.Json.JsonConvert.SerializeObject(DataList);
            filelog(CallFunction + "> SaveOrderDB  :  DataList = " + log, false);
            SP_ORDER_GET_ORDERCODE_BY_TMP_ORDERIDX_Result returnVal = new SP_ORDER_GET_ORDERCODE_BY_TMP_ORDERIDX_Result();

            //이니시스에서 noti 페이지를 여러번 호출하기 때문에 중복 저장을 막기위해 old_order_idx로 저장된 결제건이 있는지 체크
            returnVal = _orderservice.OrderGetOrderCodeByOldOrderIdx(Convert.ToInt32(DataList.P_OID));
            if (string.IsNullOrEmpty(returnVal.ORDER_CODE))
            {
                ORDER_PAY_PARAM Param = new ORDER_PAY_PARAM();
                Param.ORDER_IDX = Convert.ToInt32(DataList.P_OID);

                if (DataList.P_TYPE == "VBANK")
                {
                    Param.PAY_GBN = "3";  //가상계좌 //사용안함
                }
                else if (DataList.P_TYPE == "BANK") //실시간계좌이체
                {
                    Param.PAY_GBN = "2";  //CARD, ISP ...
                }
                else 
                {
                    Param.PAY_GBN = "1";  //CARD, ISP ...
                }

                Param.ESCROW_YN = DataList.P_NOTI;

                string INSTLMT_AT = "0"; //할부여부 ("0", "")

                if (!string.IsNullOrEmpty(DataList.P_RMESG2)) //할부개월수
                {
                    INSTLMT_AT = Convert.ToString(Convert.ToInt32(DataList.P_RMESG2));
                }

                Param.CARD_GBN = DataList.P_FN_CD1;
                Param.INSTLMT_AT = INSTLMT_AT; //할부여부
                Param.PAT_TID = DataList.P_TID;
                Param.REAL_ACCOUNT_AT = "0"; //실시간계좌 사용여부
                Param.BANK_CODE = DataList.P_CARD_PURCHASE_CODE; //은행코드

                Param.CASHRECEIPT_SE_CODE = "0";//현금영수증 발행
                Param.CASHRECEIPT_RESULT_CODE = "0";

                Param.HTTP_USER_AGENT = Request.UserAgent;
                Param.PAT_GUBUN = "Mobile";
                Param.SVR_DOMAIN = Request.Url.Authority;

                if (DataList.P_TYPE == "VBANK")
                {
                    Param.VACT_Num = DataList.P_VACT_NUM;
                    Param.VACT_BankCode = DataList.P_VACT_BANK_CODE;
                    Param.VACT_Name = DataList.P_VACT_NAME;
                    Param.VACT_Date = DataList.P_VACT_DATE;
                    Param.VACT_Time = DataList.P_VACT_TIME;
                }
                else
                {
                    Param.VACT_Num = "";
                    Param.VACT_BankCode = "";
                    Param.VACT_Name = "";
                    Param.VACT_Date = "";
                    Param.VACT_Time = "";
                }

                //로그남기기
                string save_param = String.Format("ORDER_IDX={17},PAY_GBN={0},ESCROW_YN={18}, CARD_GBN={1},INSTLMT_AT={2},BANK_CODE ={3},PAT_TID={4},REAL_ACCOUNT_AT={5},CASHRECEIPT_SE_CODE={6},CASHRECEIPT_RESULT_CODE={7},HTTP_USER_AGENT={8},PAT_GUBUN={9},SVR_DOMAIN={10},VACT_Num={11},VACT_BankCode={12},VACT_Name={13},VACT_InputName={14},VACT_Date={15},VACT_Time={16}"
                    , Param.PAY_GBN
                    , Param.CARD_GBN
                    , Param.INSTLMT_AT
                    , Param.BANK_CODE
                    , Param.PAT_TID
                    , Param.REAL_ACCOUNT_AT
                    , Param.CASHRECEIPT_SE_CODE
                    , Param.CASHRECEIPT_RESULT_CODE
                    , Param.HTTP_USER_AGENT
                    , Param.PAT_GUBUN
                    , Param.SVR_DOMAIN
                    , Param.VACT_Num
                    , Param.VACT_BankCode
                    , Param.VACT_Name
                    , Param.VACT_InputName
                    , Param.VACT_Date
                    , Param.VACT_Time
                    , Convert.ToString(Param.ORDER_IDX)
                    , Param.ESCROW_YN);

                filelog(CallFunction + "> SaveOrderDB : save_param : " + save_param, false);

                string order_code = "";
                //DB 저장
                order_code = _orderservice.OrderPaySave(Param);

                //실행결과
                OrderResult orderResult = new OrderResult();

                orderResult.Resultcode = "00";
                orderResult.ResultMsg = "";
                orderResult.ResultErrorCode = "";
                orderResult.ORDER_IDX = Param.ORDER_IDX;
                orderResult.PAY_GBN = Param.PAY_GBN;
                orderResult.ORDER_CODE = order_code;

                //로그남기기
                filelog(CallFunction + "> SaveOrderDB : Complete... order_code : " + order_code, false);

                //저장후 저장한 값을 리턴해주기위해 다시구한다.
                returnVal = _orderservice.OrderGetOrderCodeByOldOrderIdx(Convert.ToInt32(DataList.P_OID));

                //메일발송
                SendOrderResultMail(order_code);

            }
            else
            {
                //로그남기기
                filelog(CallFunction + "> SaveOrderDB : 이미 저장된 주문 : ORDER_CODE=" + returnVal.ORDER_CODE, false);
            }

            return returnVal;

        }

        // 2 Transction 방식시 : 사용자의 인증이 완료될 때 이 Url 로 인증결과를 전달합니다. 
        // card, isp, vbank (가상계좌) 시 호출
        public ActionResult InipayNext(INIPAYMOBILE_NEXT_RETURN inipay_param)
        {
            string log = "";
            log = log + Newtonsoft.Json.JsonConvert.SerializeObject(inipay_param);
            log = log + "\nquerystring : " + Request.QueryString;
            string[] keys = Request.Form.AllKeys;
            for (int i = 0; i < keys.Length; i++)
            {
                log = log + "\n" + keys[i] + ": " + Request.Form[keys[i]];
            }

            filelog("InipayNext 1 : ini_param : " + log);

            if (inipay_param.P_STATUS == "00") //성공시
            {
                string URL = inipay_param.P_REQ_URL;

                //이니시스에서 알려준 P_REQ_URL로 승인받았다고 소켓으로 알려주고 최종 결제 결과를 받아옴.
                System.Net.WebRequest webRequest = System.Net.WebRequest.Create(URL);
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                Stream reqStream = webRequest.GetRequestStream();
                string postData = "P_MID=" + _inipay_mid + "&P_TID=" + inipay_param.P_TID;
                byte[] postArray = Encoding.ASCII.GetBytes(postData);
                reqStream.Write(postArray, 0, postArray.Length);
                reqStream.Close();
                StreamReader sr = new StreamReader(webRequest.GetResponse().GetResponseStream(), Encoding.Default, true);
                string Result = sr.ReadToEnd();

                if (reqStream != null)
                {
                    reqStream.Close();
                }

                string[] MyArray = Result.Split('&');
                string ReturnStr = "";
                foreach (var item in MyArray)
                {
                    string[] paramArr = item.Split('=');
                    if (ReturnStr != "")
                    {
                        ReturnStr = ReturnStr + ", ";
                    }

                    if (paramArr[1].ToString() == "null")
                    {
                        paramArr[1] = "";
                    }

                    ReturnStr = ReturnStr + String.Format("\"{0}\":\"{1}\"", paramArr[0].ToString(), paramArr[1].ToString());
                }
                //로그남기기
                filelog("InipayNext 2 : ReturnStr Value : " + ReturnStr, false);
                
                OrderResult orderResult = new OrderResult();
                orderResult.Resultcode = "";
                orderResult.ResultMsg = "";
                orderResult.ResultErrorCode = "";
                orderResult.ORDER_IDX = 0;
                orderResult.PAY_GBN = "";
                orderResult.ORDER_CODE = "";

                if (!string.IsNullOrEmpty(ReturnStr))
                {
                    //DB저장
                    ReturnStr = "{" + ReturnStr + "}";
                    INIPAYMOBILE_CALL_RETURN DataList = JsonConvert.DeserializeObject<INIPAYMOBILE_CALL_RETURN>(ReturnStr);
                    if (DataList.P_STATUS == "00")
                    {
                        SP_ORDER_GET_ORDERCODE_BY_TMP_ORDERIDX_Result GetMasterInfo = SaveOrderDB("InipayNext 3", DataList);
                        orderResult.Resultcode = "00";
                        orderResult.ResultMsg = "";
                        orderResult.ResultErrorCode = "";
                        orderResult.ORDER_IDX = Convert.ToInt32(GetMasterInfo.ORDER_IDX);
                        orderResult.PAY_GBN = GetMasterInfo.PAY_GBN;
                        orderResult.ORDER_CODE = GetMasterInfo.ORDER_CODE;
                    }
                    else
                    {
                        orderResult.Resultcode = DataList.P_STATUS;
                        orderResult.ResultMsg = "P_REQ_URL 호출 결과 에러발생 : " + ReturnStr;

                        filelog("InipayNext 4 : P_REQ_URL 호출 결과 에러발생 : " + ReturnStr, false);
                    }
                }
                else
                {
                    orderResult.Resultcode = "08";
                    orderResult.ResultMsg = "P_REQ_URL 호출시 리턴값 없음. " + postData;
                    filelog("InipayNext 5 : Error : " + orderResult.ResultMsg, false);
                }

                StringBuilder SBuilder = new StringBuilder();
                SBuilder.Append("<form name='mysubmitform' action='/Order/OrderResult' method='POST'>\n");
                SBuilder.AppendFormat("<input type='hidden' name='{0}' value='{1}'>\n", "ORDER_IDX", orderResult.ORDER_IDX.ToString());
                SBuilder.AppendFormat("<input type='hidden' name='{0}' value='{1}'>\n", "PAY_GBN", orderResult.PAY_GBN);
                SBuilder.AppendFormat("<input type='hidden' name='{0}' value='{1}'>\n", "ORDER_CODE", orderResult.ORDER_CODE);
                SBuilder.AppendFormat("<input type='hidden' name='{0}' value='{1}'>\n", "Resultcode", orderResult.Resultcode);
                SBuilder.AppendFormat("<input type='hidden' name='{0}' value='{1}'>\n", "ResultMsg", orderResult.ResultMsg);
                SBuilder.AppendFormat("<input type='hidden' name='{0}' value='{1}'>\n", "ResultErrorCode", orderResult.ResultErrorCode);
                SBuilder.Append("</form>\n");
                SBuilder.Append("<script language='javascript'>document.mysubmitform.submit();</script>\n");
                return Content(SBuilder.ToString());
            }
            else
            {
                StringBuilder SBuilder = new StringBuilder();
                SBuilder.Append("<form name='mysubmitform' action='/Order/OrderResult' method='POST'>\n");
                SBuilder.AppendFormat("<input type='hidden' name='{0}' value='{1}'>\n", "ORDER_IDX", inipay_param.P_NOTI.ToString());
                SBuilder.AppendFormat("<input type='hidden' name='{0}' value='{1}'>\n", "Resultcode", inipay_param.P_STATUS);
                SBuilder.AppendFormat("<input type='hidden' name='{0}' value='{1}'>\n", "ResultMsg", inipay_param.P_RMESG1);
                SBuilder.AppendFormat("<input type='hidden' name='{0}' value='{1}'>\n", "ResultErrorCode", inipay_param.P_STATUS);
                SBuilder.Append("</form>\n");
                SBuilder.Append("<script language='javascript'>document.mysubmitform.submit();</script>\n");
                return Content(SBuilder.ToString());

            }

        }

        // (신용카드, 실시간계좌이체, 가상계좌 모두에서 여러번 호출됨.)
        //OK가 올때까지 24시간정도 10분간격으로 호출
        public void InipayNoti(INIPAYMOBILE_CALL_RETURN DataList)
        {
            string log = "";
            log = log + Newtonsoft.Json.JsonConvert.SerializeObject(DataList);
            log = log + "\nquerystring : " + Request.QueryString;
            string[] keys = Request.Form.AllKeys;
            for (int i = 0; i < keys.Length; i++)
            {
                log = log + "\n" + keys[i] + ": " + Request.Form[keys[i]];
            }
            
            filelog("InipayNoti 1 : " + log);
            
            if (DataList.P_TYPE == "VBANK") //결제수단이 가상계좌이며	
            {
                if (DataList.P_STATUS == "02")	//입금통보 "02" 
                {
                    Response.Write("OK");
                    Response.End();
                    return;
                }
            }

            if (DataList.P_STATUS.Equals("00"))
            {
                ORDER_PAY_PARAM Param = new ORDER_PAY_PARAM();

                SP_ORDER_GET_ORDERCODE_BY_TMP_ORDERIDX_Result GetMasterInfo = SaveOrderDB("InipayNoti 2", DataList);

                Response.Write("OK");
                Response.End();
                return;
            }
            else
            {
                Response.Write("OK");
                Response.End();
                return;
            }
        }

        //실시간계좌 이체시 호출됨
        //ORDER_IDX : 임시테이블 IDX
        public ActionResult OrderReturn(int ORDER_IDX)
        {
            string log = "";
            log = log + "ORDER_IDX="+Convert.ToString(ORDER_IDX);
            filelog("OrderReturn : " + log);

            SP_ORDER_GET_ORDERCODE_BY_TMP_ORDERIDX_Result returnVal = new SP_ORDER_GET_ORDERCODE_BY_TMP_ORDERIDX_Result();
            returnVal = _orderservice.OrderGetOrderCodeByOldOrderIdx(ORDER_IDX);

            OrderResult orderResult = new OrderResult();
            orderResult.Resultcode = "01";
            orderResult.ResultMsg = "";
            orderResult.ResultErrorCode = "";
            orderResult.ORDER_IDX = 0;
            orderResult.PAY_GBN = "";
            orderResult.ORDER_CODE = "";

            //비동기로 호출되므로 이미 저장 됐을수도 있고 저장 안되어있을수도 있음.
            if (!string.IsNullOrEmpty(returnVal.ORDER_CODE))
            {
                orderResult.Resultcode = "00";
                orderResult.ResultMsg = "";
                orderResult.ResultErrorCode = "";
                orderResult.ORDER_IDX = Convert.ToInt32(returnVal.ORDER_IDX);
                orderResult.PAY_GBN = returnVal.PAY_GBN;
                orderResult.ORDER_CODE = returnVal.ORDER_CODE;
            }

            StringBuilder SBuilder = new StringBuilder();
            SBuilder.Append("<form name='mysubmitform' action='/Order/OrderResult' method='POST'>\n");
            SBuilder.AppendFormat("<input type='hidden' name='{0}' value='{1}'>\n", "ORDER_IDX", orderResult.ORDER_IDX.ToString());
            SBuilder.AppendFormat("<input type='hidden' name='{0}' value='{1}'>\n", "PAY_GBN", orderResult.PAY_GBN);
            SBuilder.AppendFormat("<input type='hidden' name='{0}' value='{1}'>\n", "ORDER_CODE", orderResult.ORDER_CODE);
            SBuilder.AppendFormat("<input type='hidden' name='{0}' value='{1}'>\n", "Resultcode", orderResult.Resultcode);
            SBuilder.AppendFormat("<input type='hidden' name='{0}' value='{1}'>\n", "ResultMsg", orderResult.ResultMsg);
            SBuilder.AppendFormat("<input type='hidden' name='{0}' value='{1}'>\n", "ResultErrorCode", orderResult.ResultErrorCode);
            SBuilder.Append("</form>\n");
            SBuilder.Append("<script language='javascript'>document.mysubmitform.submit();</script>\n");
            return Content(SBuilder.ToString());
        }
        
    }
}