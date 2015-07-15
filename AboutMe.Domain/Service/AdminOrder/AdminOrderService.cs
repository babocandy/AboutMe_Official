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
    }
}
