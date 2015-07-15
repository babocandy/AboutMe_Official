using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AboutMe.Domain.Entity.AdminOrder;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Domain.Service.AdminOrder
{
    public class AdminOrderService : IAdminOrderService
    {
        #region List Order
        public List<SP_ADMIN_ORDER_LIST_Result> OrderList(SP_TB_ADMIN_ORDER_Param Param) 
        {
            List<SP_ADMIN_ORDER_LIST_Result> lst = new List<SP_ADMIN_ORDER_LIST_Result>();
            using (AdminOrderEntities EfContext = new AdminOrderEntities())
            {
                lst = EfContext.SP_ADMIN_ORDER_LIST(Param.FROM_DATE, Param.TO_DATE, Param.MEMBER_GBN, Param.MEMBER_GRADE_B, Param.MEMBER_GRADE_S, Param.MEMBER_GRADE_G
                    , Param.MEMBER_GRADE_V, Param.PAY_GBN, Param.PAT_GUBUN_W, Param.PAT_GUBUN_M, Param.DETAIL_STATUS_20, Param.DETAIL_STATUS_10, Param.DETAIL_STATUS_30
                    , Param.DETAIL_STATUS_40, Param.DETAIL_STATUS_50, Param.DETAIL_STATUS_60, Param.DETAIL_STATUS_90, Param.DETAIL_STATUS_80, Param.DETAIL_STATUS_70
                    , Param.DELIVERY_FROM_DATE, Param.DELIVERY_TO_DATE, Param.SEARCH_COL, Param.SEARCH_KEYWORD, Param.PAGE, Param.PAGESIZE).ToList();
            }
            return lst;
        }
        #endregion

        #region List Count
        public int OrderListCount(SP_TB_ADMIN_ORDER_Param Param)
        {
            SP_ADMIN_ORDER_LIST_Cnt result;
            using (AdminOrderEntities EfContext = new AdminOrderEntities())
            {
                result = EfContext.SP_ADMIN_ORDER_LIST_CNT(Param.FROM_DATE, Param.TO_DATE, Param.MEMBER_GBN, Param.MEMBER_GRADE_B, Param.MEMBER_GRADE_S, Param.MEMBER_GRADE_G
                    , Param.MEMBER_GRADE_V, Param.PAY_GBN, Param.PAT_GUBUN_W, Param.PAT_GUBUN_M, Param.DETAIL_STATUS_20, Param.DETAIL_STATUS_10, Param.DETAIL_STATUS_30
                    , Param.DETAIL_STATUS_40, Param.DETAIL_STATUS_50, Param.DETAIL_STATUS_60, Param.DETAIL_STATUS_90, Param.DETAIL_STATUS_80, Param.DETAIL_STATUS_70
                    , Param.DELIVERY_FROM_DATE, Param.DELIVERY_TO_DATE, Param.SEARCH_COL, Param.SEARCH_KEYWORD).FirstOrDefault();
            }
            return result.COUNT;
        }
        #endregion

        #region List 상태별 개수
        public SP_ADMIN_ORDER_STATUS_Result OrderStatusCnt(SP_TB_ADMIN_ORDER_Param Param)
        {
            SP_ADMIN_ORDER_STATUS_Result result = new SP_ADMIN_ORDER_STATUS_Result();
            using (AdminOrderEntities EfContext = new AdminOrderEntities())
            {
                result = EfContext.SP_ADMIN_ORDER_STATUS(Param.FROM_DATE, Param.TO_DATE, Param.MEMBER_GBN, Param.MEMBER_GRADE_B, Param.MEMBER_GRADE_S, Param.MEMBER_GRADE_G
                    , Param.MEMBER_GRADE_V, Param.PAY_GBN, Param.PAT_GUBUN_W, Param.PAT_GUBUN_M, Param.DETAIL_STATUS_20, Param.DETAIL_STATUS_10, Param.DETAIL_STATUS_30
                    , Param.DETAIL_STATUS_40, Param.DETAIL_STATUS_50, Param.DETAIL_STATUS_60, Param.DETAIL_STATUS_90, Param.DETAIL_STATUS_80, Param.DETAIL_STATUS_70
                    , Param.DELIVERY_FROM_DATE, Param.DELIVERY_TO_DATE, Param.SEARCH_COL, Param.SEARCH_KEYWORD).FirstOrDefault();
            }
            return result;
        }
        #endregion


        #region Detail List
        public List<SP_ADMIN_ORDERLIST_DETAIL_LIST_Result> OrderDetailList(int ORDER_IDX)
        {
            List<SP_ADMIN_ORDERLIST_DETAIL_LIST_Result> lst = new List<SP_ADMIN_ORDERLIST_DETAIL_LIST_Result>();
            using (AdminOrderEntities EfContext = new AdminOrderEntities())
            {
                lst = EfContext.SP_ADMIN_ORDERLIST_DETAIL_LIST(ORDER_IDX).ToList();
            }
            return lst;
        }
        #endregion
        
        #region Detail 정보
        public SP_ADMIN_ORDERLIST_DETAIL_BODY_Result OrderDetailBodyInfo(int ORDER_IDX)
        {
            SP_ADMIN_ORDERLIST_DETAIL_BODY_Result result = new SP_ADMIN_ORDERLIST_DETAIL_BODY_Result();
            using (AdminOrderEntities EfContext = new AdminOrderEntities())
            {
                result = EfContext.SP_ADMIN_ORDERLIST_DETAIL_BODY(ORDER_IDX).FirstOrDefault();
            }
            return result;
        }
        #endregion

        #region 주문 Log List
        public List<string> OrderDetailLogList(int ORDER_IDX)
        {
            List<string> lst = new List<string>();
            using (AdminOrderEntities EfContext = new AdminOrderEntities())
            {
                lst = EfContext.SP_ADMIN_ORDERLIST_DETAIL_LOG(ORDER_IDX).ToList();
            }
            return lst;
        }
        #endregion


        #region 배송지정보변경
        public void OrderReceiverUpdate(int ORDER_IDX, string RECEIVER_NAME, string RECEIVER_POST, string RECEIVER_ADDR1, string RECEIVER_ADDR2, string RECEIVER_TEL, string RECEIVER_HP, string REG_ID)
        {
            using (AdminOrderEntities EfContext = new AdminOrderEntities())
            {
                EfContext.SP_ADMIN_ORDER_RECEIVER_EDIT(ORDER_IDX, RECEIVER_NAME, RECEIVER_POST, RECEIVER_ADDR1, RECEIVER_ADDR2, RECEIVER_TEL, RECEIVER_HP, REG_ID);
            }
        }
        #endregion

        #region 관리자메모변경
        public void AdminMemoUpdate(int ORDER_IDX, string MANAGER_MEMO)
        {
            using (AdminOrderEntities EfContext = new AdminOrderEntities())
            {
                EfContext.SP_ADMIN_ORDER_MANAGER_MEMO_EDIT(ORDER_IDX, MANAGER_MEMO);
            }
        }
        #endregion
        
        #region 송장번호변경
        public void OrderDetailDeliveryUpdate(int ORDER_DETAIL_IDX, string DELIVERY_NUM, string REG_ID)
        {
            using (AdminOrderEntities EfContext = new AdminOrderEntities())
            {
                EfContext.SP_ADMIN_ORDER_DETAIL_DELIVERY_NUM_INPUT(ORDER_DETAIL_IDX, DELIVERY_NUM, REG_ID);
            }
        }
        #endregion
        
        #region 주문상세상태변경
        public void OrderDetailStatusUpdate(int ORDER_DETAIL_IDX, string TOBE_STATUS, string REG_ID)
        {
            using (AdminOrderEntities EfContext = new AdminOrderEntities())
            {
                EfContext.SP_ADMIN_ORDER_DETAIL_STATUS_CHANGE(ORDER_DETAIL_IDX, TOBE_STATUS, REG_ID);
            }
        }
        #endregion

        
    }
}
