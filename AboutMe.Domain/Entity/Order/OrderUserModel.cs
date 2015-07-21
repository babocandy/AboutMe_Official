using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AboutMe.Domain.Entity.Common;

namespace AboutMe.Domain.Entity.Order
{
    public class ORDER_STEP1_MODEL
    {
        public Int32 OrderIdx { get; set; }
        public USER_PROFILE UserProfile { get; set; }
    }

    public class ORDER_STEP1_ORDERLIST
    {
        public Int32 OrderIdx { get; set; }
        public USER_PROFILE UserProfile { get; set; }
        public List<SP_ORDER_STEP2_PRODUCT_LIST_Result> OrderList { get; set; }
    }

    public class ORDER_STEP1_PRICEINFO
    {
        public Int32 OrderIdx { get; set; }
        public USER_PROFILE UserProfile { get; set; }
        public SP_ORDER_STEP2_PRICE_INFO_Result PriceInfo { get; set; }
    }
    public class ORDER_STEP1_DISCOUNTINFO
    {
        public Int32 OrderIdx { get; set; }
        public USER_PROFILE UserProfile { get; set; }
        public bool TransCouponDisabled { get; set; }  //무료배송쿠폰 사용가능여부 (결제금액이 30000만원이상일경우에만 활성화)
        public SP_ORDER_STEP2_DISCOUNT_INFO_Result DiscountInfo { get; set; }
        public List<SP_ORDER_STEP2_COUPON_SEARCH_Result> TransCouponList { get; set; }
    }
    public class ORDER_STEP1_ADDRESSINFO
    {
        public Int32 OrderIdx { get; set; }
        public USER_PROFILE UserProfile { get; set; }
        public SP_ORDER_STEP2_RECENTADDR_INFO_Result RecentAddrInfo { get; set; }
        public SP_ORDER_STEP2_BASEADDR_INFO_Result BaseAddrInfo { get; set; }
    }
    public class ORDER_STEP1_PAYINFO
    {
        public Int32 OrderIdx { get; set; }
        public USER_PROFILE UserProfile { get; set; }
        public SP_ORDER_STEP2_PRICE_INFO_Result PriceInfo { get; set; }
    }
}
