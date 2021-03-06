﻿using System;
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
        Int32 OrderStep1MyPoint(string M_ID);
        SP_ORDER_STEP2_RECENTADDR_INFO_Result OrderStep1RecentAddrInfo(string M_ID);
        SP_ORDER_STEP2_BASEADDR_INFO_Result OrderStep1BaseAddrInfo(string M_ID);
        void OrderStep1SaveMemberInfo(string M_ID, string POST, string ADDR1, string ADDR2, string TEL1, string TEL2, string TEL3, string HP1, string HP2, string HP3, string EMAIL1, string EMAIL2);
        List<SP_ORDER_STEP2_COUPON_SEARCH_Result> OrderStep1CouponSelectList(string M_ID, string P_CODE, string DEVICE_GBN = "P");
        void OrderStep1CouponApply(string M_ID, Int32? ORDER_DETAIL_IDX, Int32? COUPON_IDX);
        void OrderStep1SaveReceiverAddress(int ORDER_IDX, SENDER_RECEIVER_SAVE_Param Param);
        void OrderStep1PointApply(string M_ID, Int32 ORDER_IDX, Int32 USE_POINT);
        void OrderStep1TransCouponApply(string M_ID, Int32 ORDER_IDX, Int32 COUPON_IDX);
        string OrderPaySave(ORDER_PAY_PARAM Param);
        List<SP_ORDER_RESULT_PRODUCT_LIST_Result> OrderResultProductList(string ORDER_CODE, string M_ID, string SESSION_ID);
        SP_ORDER_RESULT_DETAIL_Result OrderResultDetailInfo(string ORDER_CODE, string M_ID, string SESSION_ID);
        string OrderNomeberLoginChk(string ORDER_CODE, string NOMEMBER_PASS);
        
        SP_MYPAGE_MAIN_STATUS_Result MyPageMainInfo(string Mid);
        List<SP_MYPAGE_ORDERLIST_Result> MyOrderList(string OrderCode, string Mid, string FromDate, string ToDate, int? Page, int PageSize);
        int MyOrderListCount(string OrderCode, string Mid, string FromDate, string ToDate);
        List<SP_MYPAGE_ORDERLIST_DETAIL_PRODUCT_LIST_Result> MyOrderDetailProductList(string OrderCode, string Mid);
        SP_MYPAGE_ORDERLIST_DETAIL_INFO_Result MyOrderDetailInfo(string OrderCode, string Mid);
        void MyOrderMasterStatusChange(int OrderIdx, string TobeStatus, string Mid);
        void MyOrderDetailStatusChange(int OrderDetailIdx, string TobeStatus, string Mid);
        void OrderPartCancelInsert(int ORDER_IDX, string PAT_TID, string OLD_PAT_TID, Int32 CANCEL_PRICE, Int32 REMAINS_PRICE, string EMAIL, Int32 PRTC_REMAINS, string PRTC_TYPE, Int32 PRTC_PRICE, int PRTC_CNT, string REG_ID, string REG_IP);
        
        SP_ORDER_GET_ORDERCODE_BY_TMP_ORDERIDX_Result OrderGetOrderCodeByOldOrderIdx(Int32 OldOrderIdx);
        string OrderConfigCheck(int ORDER_IDX);
        void OrderMasterPointSet(int ORDER_IDX, string TOBE_STATUS, string REG_ID);
        void OrdeDetailPointSet(int ORDER_DETAIL_IDX, string TOBE_STATUS, string MASTER_CONTROL, string REG_ID);

        void OrderEscrowResultSet(int ORDER_IDX, string GUBUN, string SET_DATE);
    }
}
