using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.Order;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Domain.Service.Order
{
    public interface IOrderService
    {
        Int32 InsertOrderStep1(string M_ID, string SESSION_ID, string P_CODE_LIST, string P_COUNT_LIST);
        List<SP_ORDER_STEP2_PRODUCT_LIST_Result> OrderStep1List(Int32 OrderIdx);
        SP_ORDER_STEP2_PRICE_INFO_Result OrderStep1PriceInfo(Int32 OrderIdx);
        SP_ORDER_STEP2_DISCOUNT_INFO_Result OrderStep1DiscountInfo(Int32 OrderIdx);
        SP_ORDER_STEP2_RECENTADDR_INFO_Result OrderStep1RecentAddrInfo(string M_ID);
        SP_ORDER_STEP2_BASEADDR_INFO_Result OrderStep1BaseAddrInfo(string M_ID);
        void OrderStep1SaveMemberInfo(string M_ID, string POST1, string POST2, string ADDR1, string ADDR2, string TEL1, string TEL2, string TEL3, string HP1, string HP2, string HP3, string EMAIL1, string EMAIL2);
        List<SP_ORDER_STEP2_COUPON_SEARCH_Result> OrderStep1CouponSelectList(string M_ID, string P_CODE, string DEVICE_GBN = "P");
    }
}
