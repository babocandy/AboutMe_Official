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

        #region Step1 나의 포인트
        public Int32 OrderStep1MyPoint(string M_ID)
        {
            SP_ORDER_MEMBER_SEL_Result info;
            using (OrderEntities EfContext = new OrderEntities())
            {
                info = EfContext.SP_ORDER_MEMBER_SEL(M_ID).FirstOrDefault();
            }
            return info.M_POINT;
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

        #region Step1 상품별 쿠폰 사용
        public void OrderStep1CouponApply(string M_ID, Int32? ORDER_DETAIL_IDX, Int32? COUPON_IDX)
        {
            using (OrderEntities EfContext = new OrderEntities())
            {
                EfContext.SP_ORDER_STEP2_COUPON_APPLY(M_ID, ORDER_DETAIL_IDX, COUPON_IDX);
            }
        }
        #endregion

        #region Step1 보유포인트 사용
        public void OrderStep1PointApply(string M_ID, Int32 ORDER_IDX, Int32 USE_POINT)
        {
            using (OrderEntities EfContext = new OrderEntities())
            {
                EfContext.SP_ORDER_STEP2_POINT_APPLY(M_ID, ORDER_IDX, USE_POINT);
            }
        }
        #endregion

        #region Step1 배송쿠폰 사용
        public void OrderStep1TransCouponApply(string M_ID, Int32 ORDER_IDX, Int32 COUPON_IDX)
        {
            using (OrderEntities EfContext = new OrderEntities())
            {
                EfContext.SP_ORDER_STEP2_TRANSCOUPON_APPLY(M_ID, ORDER_IDX, COUPON_IDX);
            }
        }
        #endregion

        #region Step1 주문자, 배송지정보 저장
        public void OrderStep1SaveReceiverAddress(int ORDER_IDX, SENDER_RECEIVER_SAVE_Param Param)
        {
            using (OrderEntities EfContext = new OrderEntities())
            {
                EfContext.SP_ORDER_STEP2_SENDER_RECEIVER_SAVE(ORDER_IDX, Param.SENDER_NAME, Param.SENDER_POST1, Param.SENDER_POST2, Param.SENDER_ADDR1,
                    Param.SENDER_ADDR2, Param.SENDER_TEL1, Param.SENDER_TEL2, Param.SENDER_TEL3, Param.SENDER_HP1, Param.SENDER_HP2, Param.SENDER_HP3,
                    Param.SENDER_EMAIL1, Param.SENDER_EMAIL2, Param.RECEIVER_NAME, Param.RECEIVER_POST1, Param.RECEIVER_POST2, Param.RECEIVER_ADDR1,
                    Param.RECEIVER_ADDR2, Param.RECEIVER_TEL1, Param.RECEIVER_TEL2, Param.RECEIVER_TEL3, Param.RECEIVER_HP1, Param.RECEIVER_HP2,
                    Param.RECEIVER_HP3, Param.ORDER_MEMO, Param.NOMEMBER_PASS);
            }
        }
        #endregion

        #region Step1 결제결과 DB저장
        public string OrderPaySave(ORDER_PAY_PARAM Param)
        {
            SP_ORDER_PAY_Result result = new SP_ORDER_PAY_Result();
            using (OrderEntities EfContext = new OrderEntities())
            {
               result = EfContext.SP_ORDER_PAY(Param.ORDER_IDX, Param.PAY_GBN, Param.CARD_GBN, Param.INSTLMT_AT, Param.BANK_CODE, Param.PAT_TID, Param.REAL_ACCOUNT_AT, Param.CASHRECEIPT_SE_CODE, Param.CASHRECEIPT_RESULT_CODE, Param.HTTP_USER_AGENT, Param.PAT_GUBUN, Param.SVR_DOMAIN).FirstOrDefault(); ;
            }
            return result.ORDER_CODE;
        }
        #endregion

        #region 주문결과 목록
        public List<SP_ORDER_RESULT_PRODUCT_LIST_Result> OrderResultProductList(string ORDER_CODE, string M_ID, string SESSION_ID)
        {
            List<SP_ORDER_RESULT_PRODUCT_LIST_Result> lst = new List<SP_ORDER_RESULT_PRODUCT_LIST_Result>();
            using (OrderEntities EfContext = new OrderEntities())
            {
                lst = EfContext.SP_ORDER_RESULT_PRODUCT_LIST(ORDER_CODE, M_ID, SESSION_ID).ToList();
            }

            return lst;
        }
        #endregion

        #region 주문결과 상세
        public SP_ORDER_RESULT_DETAIL_Result OrderResultDetailInfo(string ORDER_CODE, string M_ID, string SESSION_ID)
        {
            SP_ORDER_RESULT_DETAIL_Result info = new SP_ORDER_RESULT_DETAIL_Result();
            using (OrderEntities EfContext = new OrderEntities())
            {
                info = EfContext.SP_ORDER_RESULT_DETAIL(ORDER_CODE, M_ID, SESSION_ID).FirstOrDefault();
            }
            return info;
        }
        #endregion

        #region 비회원 주문조회 로그인
        public string OrderNomeberLoginChk(string ORDER_CODE, string NOMEMBER_PASS)
        {
            SP_ORDER_NOMEMBER_LOGIN_Result info = new SP_ORDER_NOMEMBER_LOGIN_Result();
            using (OrderEntities EfContext = new OrderEntities())
            {
                info = EfContext.SP_ORDER_NOMEMBER_LOGIN(ORDER_CODE, NOMEMBER_PASS).FirstOrDefault();
            }
            return info.ORDER_CODE;
        }
        #endregion

        #region My Order List
        public List<SP_MYPAGE_ORDERLIST_Result> MyOrderList(string OrderCode, string Mid, string FromDate, string ToDate, int? Page, int PageSize)
        {
            List<SP_MYPAGE_ORDERLIST_Result> lst = new List<SP_MYPAGE_ORDERLIST_Result>();
            using (OrderEntities EfContext = new OrderEntities())
            {
                lst = EfContext.SP_MYPAGE_ORDERLIST(OrderCode, Mid,FromDate, ToDate, Page, PageSize).ToList();
            }

            return lst;
        }
        #endregion

        #region My Order List Count
        public int MyOrderListCount(string OrderCode, string Mid, string FromDate, string ToDate)
        {
            SP_MYPAGE_ORDERLIST_CNT_Result result = new SP_MYPAGE_ORDERLIST_CNT_Result();
            using (OrderEntities EfContext = new OrderEntities())
            {
                result = EfContext.SP_MYPAGE_ORDERLIST_CNT(OrderCode, Mid, FromDate, ToDate).FirstOrDefault();
            }
            return result.COUNT;
        }
        #endregion
    }
}
