using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.AdminOrder;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Domain.Service.AdminOrder
{
    public interface IAdminOrderService
    {
        //Order List
        List<SP_ADMIN_ORDER_LIST_Result> OrderList(SP_TB_ADMIN_ORDER_Param param);
        int OrderListCount(SP_TB_ADMIN_ORDER_Param Param);
        SP_ADMIN_ORDER_STATUS_Result OrderStatusCnt(SP_TB_ADMIN_ORDER_Param param);

        //Order Detail
        List<SP_ADMIN_ORDERLIST_DETAIL_LIST_Result> OrderDetailList(int ORDER_IDX);
        SP_ADMIN_ORDERLIST_DETAIL_BODY_Result OrderDetailBodyInfo(int ORDER_IDX);
        List<string> OrderDetailLogList(int ORDER_IDX);
        void OrderReceiverUpdate(int ORDER_IDX, string RECEIVER_NAME, string RECEIVER_POST, string RECEIVER_ADDR1, string RECEIVER_ADDR2, string RECEIVER_TEL, string RECEIVER_HP, string REG_ID);
        void AdminMemoUpdate(int ORDER_IDX, string MANAGER_MEMO);
        void OrderDetailDeliveryUpdate(int ORDER_DETAIL_IDX, string DELIVERY_NUM, string REG_ID);
        void OrderDetailStatusUpdate(int ORDER_DETAIL_IDX, string TOBE_STATUS, string REG_ID);
    }


}
