using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AboutMe.Domain.Entity.Order;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Domain.Service.Order
{
    public class OrderService : IOrderService
    {
        #region Insert
        public Int32 InsertOrderStep1(string M_ID, string SESSION_ID, string P_CODE_LIST, string P_COUNT_LIST)
        {
            SP_ORDER_STEP2_Result result;
            using (OrderEntities EfContext = new OrderEntities())
            {
                result = EfContext.SP_ORDER_STEP1(M_ID, SESSION_ID, P_CODE_LIST, P_COUNT_LIST).FirstOrDefault();
            }
            return result.ORDER_IDX;
        }
        #endregion

        #region Step1 Order List
        public List<SP_ORDER_STEP2_PRODUCT_LIST_Result> OrderStep1List(Int32 OrderIdx)
        {
            List<SP_ORDER_STEP2_PRODUCT_LIST_Result> lst = new List<SP_ORDER_STEP2_PRODUCT_LIST_Result>();
            using (OrderEntities EfContext = new OrderEntities())
            {
                lst = EfContext.SP_ORDER_STEP2_PRODUCT_LIST(OrderIdx).ToList();
            }

            return lst;
        }
        #endregion

        #region Step1 Order Price Info
        public SP_ORDER_STEP2_PRICE_INFO_Result OrderStep1PriceInfo(Int32 OrderIdx)
        {
            SP_ORDER_STEP2_PRICE_INFO_Result info = new SP_ORDER_STEP2_PRICE_INFO_Result();
            using (OrderEntities EfContext = new OrderEntities())
            {
                info = EfContext.SP_ORDER_STEP2_PRICE_INFO(OrderIdx).FirstOrDefault();
            }
            return info;
        }
        #endregion

        #region Step1 Order Discount Info
        public SP_ORDER_STEP2_DISCOUNT_INFO_Result OrderStep1DiscountInfo(Int32 OrderIdx)
        {
            SP_ORDER_STEP2_DISCOUNT_INFO_Result info = new SP_ORDER_STEP2_DISCOUNT_INFO_Result();
            using (OrderEntities EfContext = new OrderEntities())
            {
                info = EfContext.SP_ORDER_STEP2_DISCOUNT_INFO(OrderIdx).FirstOrDefault();
            }
            return info;
        }
        #endregion

        #region Step1 Order 최근배송지
        public SP_ORDER_STEP2_RECENTADDR_INFO_Result OrderStep1RecentAddrInfo(string M_ID)
        {
            SP_ORDER_STEP2_RECENTADDR_INFO_Result info;
            using (OrderEntities EfContext = new OrderEntities())
            {
                info = EfContext.SP_ORDER_STEP2_RECENTADDR_INFO(M_ID).FirstOrDefault();
            }
            return info;
        }
        #endregion

        #region Step1 Order 회원 주소
        public SP_ORDER_STEP2_BASEADDR_INFO_Result OrderStep1BaseAddrInfo(string M_ID)
        {
            SP_ORDER_STEP2_BASEADDR_INFO_Result info;
            using (OrderEntities EfContext = new OrderEntities())
            {
                info = EfContext.SP_ORDER_STEP2_BASEADDR_INFO(M_ID).FirstOrDefault();
            }
            return info;
        }
        #endregion

        #region Step1 Order 회원 주소 저장
        public void OrderStep1SaveMemberInfo(string M_ID, string POST1, string POST2, string ADDR1, string ADDR2, string TEL1, string TEL2, string TEL3, string HP1, string HP2, string HP3, string EMAIL1, string EMAIL2)
        {
            using (OrderEntities EfContext = new OrderEntities())
            {
                EfContext.SP_ORDER_STEP2_ADDR_SAVE(M_ID, POST1, POST2, ADDR1, ADDR2, TEL1, TEL2, TEL3, HP1, HP2, HP3, EMAIL1, EMAIL2);
            }
        }
        #endregion

        #region Step1 상품별 사용가능 쿠폰 목록
        public List<SP_ORDER_STEP2_COUPON_SEARCH_Result> OrderStep1CouponSelectList(string M_ID, string P_CODE, string DEVICE_GBN = "P")
        {
            List<SP_ORDER_STEP2_COUPON_SEARCH_Result> lst = new List<SP_ORDER_STEP2_COUPON_SEARCH_Result>();
            using (OrderEntities EfContext = new OrderEntities())
            {
                lst = EfContext.SP_ORDER_STEP2_COUPON_SEARCH(M_ID, P_CODE, DEVICE_GBN).ToList();
            }
            return lst;
        }
        #endregion

    }
}
