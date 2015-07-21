using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

using AboutMe.Domain.Service.Order;
using AboutMe.Domain.Entity.Order;
using AboutMe.Domain.Entity.Cart;
using AboutMe.Web.Front.Common.Filters;

namespace AboutMe.Web.Front.Controllers
{
    public class OrderController : BaseFrontController
    {
        private IOrderService _orderservice;

        public OrderController(IOrderService _orderservice)
        {
            this._orderservice = _orderservice;
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
            this.TempData["Order_Idx"] = Order_Idx;
            return Content("<form name='mysubmitform' action='/Order/Step1' method='POST'><input type='hidden' name='ORDER_IDX' value='" + Order_Idx.ToString() + "'></form> <script language='javascript'>document.mysubmitform.submit();</script>");

        }

        [HttpPost]
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
            bool coupon_disable = true;

            if (priceInfo.TOTAL_PAY_PRICE >= 30000)
            {
                coupon_disable = true;
            }
            else
            {
                coupon_disable = false;
            }

            ORDER_STEP1_DISCOUNTINFO M = new ORDER_STEP1_DISCOUNTINFO
            {
                OrderIdx = OrderIdx,
                UserProfile = _user_profile,
                TransCouponDisabled = coupon_disable,
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
            else
            {
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
            
            ORDER_STEP1_PAYINFO M = new ORDER_STEP1_PAYINFO
            {
                OrderIdx = OrderIdx,
                UserProfile = _user_profile,
                PriceInfo = _orderservice.OrderStep1PriceInfo(OrderIdx)
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
        public ActionResult SelectCouponOption(string P_CODE, string COUPON_CD)
        {
            string str = "<option RATE_OR_MONEY='' COUPON_MONEY='' COUPON_DISCOUNT_RATE=''>쿠폰을 선택하세요</option>\n";
            List<SP_ORDER_STEP2_COUPON_SEARCH_Result> TransCouponList = _orderservice.OrderStep1CouponSelectList(_user_profile.M_ID, P_CODE, "P");
            //var jsonData = new { result = "true", list= TransCouponList};
            foreach (SP_ORDER_STEP2_COUPON_SEARCH_Result item in TransCouponList)
            {
                string seltxt = "";
                if (item.IDX_COUPON_NUMBER.ToString() == COUPON_CD)
                {
                    seltxt = "selected='selected'";
                }
                str += "<option value='" + item.IDX_COUPON_NUMBER + "' "+seltxt+" RATE_OR_MONEY='" + item.RATE_OR_MONEY + "' COUPON_MONEY='" + item.COUPON_MONEY + "'" + " COUPON_DISCOUNT_RATE='" + item.COUPON_DISCOUNT_RATE + "'>" + item.COUPON_NAME + "</option>\n";
            }
            return Content(str);
        }

        
    }
}