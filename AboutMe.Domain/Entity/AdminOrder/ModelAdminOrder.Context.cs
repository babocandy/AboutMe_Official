﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AboutMe.Domain.Entity.AdminOrder
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class AdminOrderEntities : DbContext
    {
        public AdminOrderEntities()
            : base("name=AdminOrderEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
    
        public virtual int SP_ADMIN_ORDER_DETAIL_DELIVERY_NUM_INPUT(Nullable<int> oRDER_DETAIL_IDX, string dELIVERY_NUM, string rEG_ID)
        {
            var oRDER_DETAIL_IDXParameter = oRDER_DETAIL_IDX.HasValue ?
                new ObjectParameter("ORDER_DETAIL_IDX", oRDER_DETAIL_IDX) :
                new ObjectParameter("ORDER_DETAIL_IDX", typeof(int));
    
            var dELIVERY_NUMParameter = dELIVERY_NUM != null ?
                new ObjectParameter("DELIVERY_NUM", dELIVERY_NUM) :
                new ObjectParameter("DELIVERY_NUM", typeof(string));
    
            var rEG_IDParameter = rEG_ID != null ?
                new ObjectParameter("REG_ID", rEG_ID) :
                new ObjectParameter("REG_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_ORDER_DETAIL_DELIVERY_NUM_INPUT", oRDER_DETAIL_IDXParameter, dELIVERY_NUMParameter, rEG_IDParameter);
        }
    
        public virtual int SP_ADMIN_ORDER_DETAIL_STATUS_CHANGE(Nullable<int> oRDER_DETAIL_IDX, string tOBE_STATUS, string rEG_ID)
        {
            var oRDER_DETAIL_IDXParameter = oRDER_DETAIL_IDX.HasValue ?
                new ObjectParameter("ORDER_DETAIL_IDX", oRDER_DETAIL_IDX) :
                new ObjectParameter("ORDER_DETAIL_IDX", typeof(int));
    
            var tOBE_STATUSParameter = tOBE_STATUS != null ?
                new ObjectParameter("TOBE_STATUS", tOBE_STATUS) :
                new ObjectParameter("TOBE_STATUS", typeof(string));
    
            var rEG_IDParameter = rEG_ID != null ?
                new ObjectParameter("REG_ID", rEG_ID) :
                new ObjectParameter("REG_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_ORDER_DETAIL_STATUS_CHANGE", oRDER_DETAIL_IDXParameter, tOBE_STATUSParameter, rEG_IDParameter);
        }
    
        public virtual ObjectResult<SP_ADMIN_ORDER_LIST_Result> SP_ADMIN_ORDER_LIST(string fROM_DATE, string tO_DATE, string mEMBER_GBN, Nullable<bool> mEMBER_GRADE_B, Nullable<bool> mEMBER_GRADE_S, Nullable<bool> mEMBER_GRADE_G, Nullable<bool> mEMBER_GRADE_V, string pAY_GBN, Nullable<bool> pAT_GUBUN_W, Nullable<bool> pAT_GUBUN_M, Nullable<bool> dETAIL_STATUS_20, Nullable<bool> dETAIL_STATUS_10, Nullable<bool> dETAIL_STATUS_30, Nullable<bool> dETAIL_STATUS_40, Nullable<bool> dETAIL_STATUS_50, Nullable<bool> dETAIL_STATUS_60, Nullable<bool> dETAIL_STATUS_90, Nullable<bool> dETAIL_STATUS_80, Nullable<bool> dETAIL_STATUS_70, string dELIVERY_FROM_DATE, string dELIVERY_TO_DATE, string sEARCH_COL, string sEARCH_KEYWORD, Nullable<int> pAGE, Nullable<int> pAGESIZE)
        {
            var fROM_DATEParameter = fROM_DATE != null ?
                new ObjectParameter("FROM_DATE", fROM_DATE) :
                new ObjectParameter("FROM_DATE", typeof(string));
    
            var tO_DATEParameter = tO_DATE != null ?
                new ObjectParameter("TO_DATE", tO_DATE) :
                new ObjectParameter("TO_DATE", typeof(string));
    
            var mEMBER_GBNParameter = mEMBER_GBN != null ?
                new ObjectParameter("MEMBER_GBN", mEMBER_GBN) :
                new ObjectParameter("MEMBER_GBN", typeof(string));
    
            var mEMBER_GRADE_BParameter = mEMBER_GRADE_B.HasValue ?
                new ObjectParameter("MEMBER_GRADE_B", mEMBER_GRADE_B) :
                new ObjectParameter("MEMBER_GRADE_B", typeof(bool));
    
            var mEMBER_GRADE_SParameter = mEMBER_GRADE_S.HasValue ?
                new ObjectParameter("MEMBER_GRADE_S", mEMBER_GRADE_S) :
                new ObjectParameter("MEMBER_GRADE_S", typeof(bool));
    
            var mEMBER_GRADE_GParameter = mEMBER_GRADE_G.HasValue ?
                new ObjectParameter("MEMBER_GRADE_G", mEMBER_GRADE_G) :
                new ObjectParameter("MEMBER_GRADE_G", typeof(bool));
    
            var mEMBER_GRADE_VParameter = mEMBER_GRADE_V.HasValue ?
                new ObjectParameter("MEMBER_GRADE_V", mEMBER_GRADE_V) :
                new ObjectParameter("MEMBER_GRADE_V", typeof(bool));
    
            var pAY_GBNParameter = pAY_GBN != null ?
                new ObjectParameter("PAY_GBN", pAY_GBN) :
                new ObjectParameter("PAY_GBN", typeof(string));
    
            var pAT_GUBUN_WParameter = pAT_GUBUN_W.HasValue ?
                new ObjectParameter("PAT_GUBUN_W", pAT_GUBUN_W) :
                new ObjectParameter("PAT_GUBUN_W", typeof(bool));
    
            var pAT_GUBUN_MParameter = pAT_GUBUN_M.HasValue ?
                new ObjectParameter("PAT_GUBUN_M", pAT_GUBUN_M) :
                new ObjectParameter("PAT_GUBUN_M", typeof(bool));
    
            var dETAIL_STATUS_20Parameter = dETAIL_STATUS_20.HasValue ?
                new ObjectParameter("DETAIL_STATUS_20", dETAIL_STATUS_20) :
                new ObjectParameter("DETAIL_STATUS_20", typeof(bool));
    
            var dETAIL_STATUS_10Parameter = dETAIL_STATUS_10.HasValue ?
                new ObjectParameter("DETAIL_STATUS_10", dETAIL_STATUS_10) :
                new ObjectParameter("DETAIL_STATUS_10", typeof(bool));
    
            var dETAIL_STATUS_30Parameter = dETAIL_STATUS_30.HasValue ?
                new ObjectParameter("DETAIL_STATUS_30", dETAIL_STATUS_30) :
                new ObjectParameter("DETAIL_STATUS_30", typeof(bool));
    
            var dETAIL_STATUS_40Parameter = dETAIL_STATUS_40.HasValue ?
                new ObjectParameter("DETAIL_STATUS_40", dETAIL_STATUS_40) :
                new ObjectParameter("DETAIL_STATUS_40", typeof(bool));
    
            var dETAIL_STATUS_50Parameter = dETAIL_STATUS_50.HasValue ?
                new ObjectParameter("DETAIL_STATUS_50", dETAIL_STATUS_50) :
                new ObjectParameter("DETAIL_STATUS_50", typeof(bool));
    
            var dETAIL_STATUS_60Parameter = dETAIL_STATUS_60.HasValue ?
                new ObjectParameter("DETAIL_STATUS_60", dETAIL_STATUS_60) :
                new ObjectParameter("DETAIL_STATUS_60", typeof(bool));
    
            var dETAIL_STATUS_90Parameter = dETAIL_STATUS_90.HasValue ?
                new ObjectParameter("DETAIL_STATUS_90", dETAIL_STATUS_90) :
                new ObjectParameter("DETAIL_STATUS_90", typeof(bool));
    
            var dETAIL_STATUS_80Parameter = dETAIL_STATUS_80.HasValue ?
                new ObjectParameter("DETAIL_STATUS_80", dETAIL_STATUS_80) :
                new ObjectParameter("DETAIL_STATUS_80", typeof(bool));
    
            var dETAIL_STATUS_70Parameter = dETAIL_STATUS_70.HasValue ?
                new ObjectParameter("DETAIL_STATUS_70", dETAIL_STATUS_70) :
                new ObjectParameter("DETAIL_STATUS_70", typeof(bool));
    
            var dELIVERY_FROM_DATEParameter = dELIVERY_FROM_DATE != null ?
                new ObjectParameter("DELIVERY_FROM_DATE", dELIVERY_FROM_DATE) :
                new ObjectParameter("DELIVERY_FROM_DATE", typeof(string));
    
            var dELIVERY_TO_DATEParameter = dELIVERY_TO_DATE != null ?
                new ObjectParameter("DELIVERY_TO_DATE", dELIVERY_TO_DATE) :
                new ObjectParameter("DELIVERY_TO_DATE", typeof(string));
    
            var sEARCH_COLParameter = sEARCH_COL != null ?
                new ObjectParameter("SEARCH_COL", sEARCH_COL) :
                new ObjectParameter("SEARCH_COL", typeof(string));
    
            var sEARCH_KEYWORDParameter = sEARCH_KEYWORD != null ?
                new ObjectParameter("SEARCH_KEYWORD", sEARCH_KEYWORD) :
                new ObjectParameter("SEARCH_KEYWORD", typeof(string));
    
            var pAGEParameter = pAGE.HasValue ?
                new ObjectParameter("PAGE", pAGE) :
                new ObjectParameter("PAGE", typeof(int));
    
            var pAGESIZEParameter = pAGESIZE.HasValue ?
                new ObjectParameter("PAGESIZE", pAGESIZE) :
                new ObjectParameter("PAGESIZE", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_ORDER_LIST_Result>("SP_ADMIN_ORDER_LIST", fROM_DATEParameter, tO_DATEParameter, mEMBER_GBNParameter, mEMBER_GRADE_BParameter, mEMBER_GRADE_SParameter, mEMBER_GRADE_GParameter, mEMBER_GRADE_VParameter, pAY_GBNParameter, pAT_GUBUN_WParameter, pAT_GUBUN_MParameter, dETAIL_STATUS_20Parameter, dETAIL_STATUS_10Parameter, dETAIL_STATUS_30Parameter, dETAIL_STATUS_40Parameter, dETAIL_STATUS_50Parameter, dETAIL_STATUS_60Parameter, dETAIL_STATUS_90Parameter, dETAIL_STATUS_80Parameter, dETAIL_STATUS_70Parameter, dELIVERY_FROM_DATEParameter, dELIVERY_TO_DATEParameter, sEARCH_COLParameter, sEARCH_KEYWORDParameter, pAGEParameter, pAGESIZEParameter);
        }
    
        public virtual int SP_ADMIN_ORDER_MANAGER_MEMO_EDIT(Nullable<int> oRDER_IDX, string mANAGER_MEMO)
        {
            var oRDER_IDXParameter = oRDER_IDX.HasValue ?
                new ObjectParameter("ORDER_IDX", oRDER_IDX) :
                new ObjectParameter("ORDER_IDX", typeof(int));
    
            var mANAGER_MEMOParameter = mANAGER_MEMO != null ?
                new ObjectParameter("MANAGER_MEMO", mANAGER_MEMO) :
                new ObjectParameter("MANAGER_MEMO", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_ORDER_MANAGER_MEMO_EDIT", oRDER_IDXParameter, mANAGER_MEMOParameter);
        }
    
        public virtual int SP_ADMIN_ORDER_MASTER_STATUS_CHANGE(Nullable<int> oRDER_IDX, string tOBE_STATUS, string rEG_ID)
        {
            var oRDER_IDXParameter = oRDER_IDX.HasValue ?
                new ObjectParameter("ORDER_IDX", oRDER_IDX) :
                new ObjectParameter("ORDER_IDX", typeof(int));
    
            var tOBE_STATUSParameter = tOBE_STATUS != null ?
                new ObjectParameter("TOBE_STATUS", tOBE_STATUS) :
                new ObjectParameter("TOBE_STATUS", typeof(string));
    
            var rEG_IDParameter = rEG_ID != null ?
                new ObjectParameter("REG_ID", rEG_ID) :
                new ObjectParameter("REG_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_ORDER_MASTER_STATUS_CHANGE", oRDER_IDXParameter, tOBE_STATUSParameter, rEG_IDParameter);
        }
    
        public virtual int SP_ADMIN_ORDER_RECEIVER_EDIT(Nullable<int> oRDER_IDX, string rECEIVER_NAME, string rECEIVER_POST, string rECEIVER_ADDR1, string rECEIVER_ADDR2, string rECEIVER_TEL, string rECEIVER_HP, string rEG_ID)
        {
            var oRDER_IDXParameter = oRDER_IDX.HasValue ?
                new ObjectParameter("ORDER_IDX", oRDER_IDX) :
                new ObjectParameter("ORDER_IDX", typeof(int));
    
            var rECEIVER_NAMEParameter = rECEIVER_NAME != null ?
                new ObjectParameter("RECEIVER_NAME", rECEIVER_NAME) :
                new ObjectParameter("RECEIVER_NAME", typeof(string));
    
            var rECEIVER_POSTParameter = rECEIVER_POST != null ?
                new ObjectParameter("RECEIVER_POST", rECEIVER_POST) :
                new ObjectParameter("RECEIVER_POST", typeof(string));
    
            var rECEIVER_ADDR1Parameter = rECEIVER_ADDR1 != null ?
                new ObjectParameter("RECEIVER_ADDR1", rECEIVER_ADDR1) :
                new ObjectParameter("RECEIVER_ADDR1", typeof(string));
    
            var rECEIVER_ADDR2Parameter = rECEIVER_ADDR2 != null ?
                new ObjectParameter("RECEIVER_ADDR2", rECEIVER_ADDR2) :
                new ObjectParameter("RECEIVER_ADDR2", typeof(string));
    
            var rECEIVER_TELParameter = rECEIVER_TEL != null ?
                new ObjectParameter("RECEIVER_TEL", rECEIVER_TEL) :
                new ObjectParameter("RECEIVER_TEL", typeof(string));
    
            var rECEIVER_HPParameter = rECEIVER_HP != null ?
                new ObjectParameter("RECEIVER_HP", rECEIVER_HP) :
                new ObjectParameter("RECEIVER_HP", typeof(string));
    
            var rEG_IDParameter = rEG_ID != null ?
                new ObjectParameter("REG_ID", rEG_ID) :
                new ObjectParameter("REG_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_ORDER_RECEIVER_EDIT", oRDER_IDXParameter, rECEIVER_NAMEParameter, rECEIVER_POSTParameter, rECEIVER_ADDR1Parameter, rECEIVER_ADDR2Parameter, rECEIVER_TELParameter, rECEIVER_HPParameter, rEG_IDParameter);
        }
    
        public virtual ObjectResult<SP_ADMIN_ORDER_STATUS_Result> SP_ADMIN_ORDER_STATUS(string fROM_DATE, string tO_DATE, string mEMBER_GBN, Nullable<bool> mEMBER_GRADE_B, Nullable<bool> mEMBER_GRADE_S, Nullable<bool> mEMBER_GRADE_G, Nullable<bool> mEMBER_GRADE_V, string pAY_GBN, Nullable<bool> pAT_GUBUN_W, Nullable<bool> pAT_GUBUN_M, Nullable<bool> dETAIL_STATUS_20, Nullable<bool> dETAIL_STATUS_10, Nullable<bool> dETAIL_STATUS_30, Nullable<bool> dETAIL_STATUS_40, Nullable<bool> dETAIL_STATUS_50, Nullable<bool> dETAIL_STATUS_60, Nullable<bool> dETAIL_STATUS_90, Nullable<bool> dETAIL_STATUS_80, Nullable<bool> dETAIL_STATUS_70, string dELIVERY_FROM_DATE, string dELIVERY_TO_DATE, string sEARCH_COL, string sEARCH_KEYWORD)
        {
            var fROM_DATEParameter = fROM_DATE != null ?
                new ObjectParameter("FROM_DATE", fROM_DATE) :
                new ObjectParameter("FROM_DATE", typeof(string));
    
            var tO_DATEParameter = tO_DATE != null ?
                new ObjectParameter("TO_DATE", tO_DATE) :
                new ObjectParameter("TO_DATE", typeof(string));
    
            var mEMBER_GBNParameter = mEMBER_GBN != null ?
                new ObjectParameter("MEMBER_GBN", mEMBER_GBN) :
                new ObjectParameter("MEMBER_GBN", typeof(string));
    
            var mEMBER_GRADE_BParameter = mEMBER_GRADE_B.HasValue ?
                new ObjectParameter("MEMBER_GRADE_B", mEMBER_GRADE_B) :
                new ObjectParameter("MEMBER_GRADE_B", typeof(bool));
    
            var mEMBER_GRADE_SParameter = mEMBER_GRADE_S.HasValue ?
                new ObjectParameter("MEMBER_GRADE_S", mEMBER_GRADE_S) :
                new ObjectParameter("MEMBER_GRADE_S", typeof(bool));
    
            var mEMBER_GRADE_GParameter = mEMBER_GRADE_G.HasValue ?
                new ObjectParameter("MEMBER_GRADE_G", mEMBER_GRADE_G) :
                new ObjectParameter("MEMBER_GRADE_G", typeof(bool));
    
            var mEMBER_GRADE_VParameter = mEMBER_GRADE_V.HasValue ?
                new ObjectParameter("MEMBER_GRADE_V", mEMBER_GRADE_V) :
                new ObjectParameter("MEMBER_GRADE_V", typeof(bool));
    
            var pAY_GBNParameter = pAY_GBN != null ?
                new ObjectParameter("PAY_GBN", pAY_GBN) :
                new ObjectParameter("PAY_GBN", typeof(string));
    
            var pAT_GUBUN_WParameter = pAT_GUBUN_W.HasValue ?
                new ObjectParameter("PAT_GUBUN_W", pAT_GUBUN_W) :
                new ObjectParameter("PAT_GUBUN_W", typeof(bool));
    
            var pAT_GUBUN_MParameter = pAT_GUBUN_M.HasValue ?
                new ObjectParameter("PAT_GUBUN_M", pAT_GUBUN_M) :
                new ObjectParameter("PAT_GUBUN_M", typeof(bool));
    
            var dETAIL_STATUS_20Parameter = dETAIL_STATUS_20.HasValue ?
                new ObjectParameter("DETAIL_STATUS_20", dETAIL_STATUS_20) :
                new ObjectParameter("DETAIL_STATUS_20", typeof(bool));
    
            var dETAIL_STATUS_10Parameter = dETAIL_STATUS_10.HasValue ?
                new ObjectParameter("DETAIL_STATUS_10", dETAIL_STATUS_10) :
                new ObjectParameter("DETAIL_STATUS_10", typeof(bool));
    
            var dETAIL_STATUS_30Parameter = dETAIL_STATUS_30.HasValue ?
                new ObjectParameter("DETAIL_STATUS_30", dETAIL_STATUS_30) :
                new ObjectParameter("DETAIL_STATUS_30", typeof(bool));
    
            var dETAIL_STATUS_40Parameter = dETAIL_STATUS_40.HasValue ?
                new ObjectParameter("DETAIL_STATUS_40", dETAIL_STATUS_40) :
                new ObjectParameter("DETAIL_STATUS_40", typeof(bool));
    
            var dETAIL_STATUS_50Parameter = dETAIL_STATUS_50.HasValue ?
                new ObjectParameter("DETAIL_STATUS_50", dETAIL_STATUS_50) :
                new ObjectParameter("DETAIL_STATUS_50", typeof(bool));
    
            var dETAIL_STATUS_60Parameter = dETAIL_STATUS_60.HasValue ?
                new ObjectParameter("DETAIL_STATUS_60", dETAIL_STATUS_60) :
                new ObjectParameter("DETAIL_STATUS_60", typeof(bool));
    
            var dETAIL_STATUS_90Parameter = dETAIL_STATUS_90.HasValue ?
                new ObjectParameter("DETAIL_STATUS_90", dETAIL_STATUS_90) :
                new ObjectParameter("DETAIL_STATUS_90", typeof(bool));
    
            var dETAIL_STATUS_80Parameter = dETAIL_STATUS_80.HasValue ?
                new ObjectParameter("DETAIL_STATUS_80", dETAIL_STATUS_80) :
                new ObjectParameter("DETAIL_STATUS_80", typeof(bool));
    
            var dETAIL_STATUS_70Parameter = dETAIL_STATUS_70.HasValue ?
                new ObjectParameter("DETAIL_STATUS_70", dETAIL_STATUS_70) :
                new ObjectParameter("DETAIL_STATUS_70", typeof(bool));
    
            var dELIVERY_FROM_DATEParameter = dELIVERY_FROM_DATE != null ?
                new ObjectParameter("DELIVERY_FROM_DATE", dELIVERY_FROM_DATE) :
                new ObjectParameter("DELIVERY_FROM_DATE", typeof(string));
    
            var dELIVERY_TO_DATEParameter = dELIVERY_TO_DATE != null ?
                new ObjectParameter("DELIVERY_TO_DATE", dELIVERY_TO_DATE) :
                new ObjectParameter("DELIVERY_TO_DATE", typeof(string));
    
            var sEARCH_COLParameter = sEARCH_COL != null ?
                new ObjectParameter("SEARCH_COL", sEARCH_COL) :
                new ObjectParameter("SEARCH_COL", typeof(string));
    
            var sEARCH_KEYWORDParameter = sEARCH_KEYWORD != null ?
                new ObjectParameter("SEARCH_KEYWORD", sEARCH_KEYWORD) :
                new ObjectParameter("SEARCH_KEYWORD", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_ORDER_STATUS_Result>("SP_ADMIN_ORDER_STATUS", fROM_DATEParameter, tO_DATEParameter, mEMBER_GBNParameter, mEMBER_GRADE_BParameter, mEMBER_GRADE_SParameter, mEMBER_GRADE_GParameter, mEMBER_GRADE_VParameter, pAY_GBNParameter, pAT_GUBUN_WParameter, pAT_GUBUN_MParameter, dETAIL_STATUS_20Parameter, dETAIL_STATUS_10Parameter, dETAIL_STATUS_30Parameter, dETAIL_STATUS_40Parameter, dETAIL_STATUS_50Parameter, dETAIL_STATUS_60Parameter, dETAIL_STATUS_90Parameter, dETAIL_STATUS_80Parameter, dETAIL_STATUS_70Parameter, dELIVERY_FROM_DATEParameter, dELIVERY_TO_DATEParameter, sEARCH_COLParameter, sEARCH_KEYWORDParameter);
        }
    
        public virtual ObjectResult<SP_ADMIN_ORDERLIST_DETAIL_BODY_Result> SP_ADMIN_ORDERLIST_DETAIL_BODY(Nullable<int> oRDER_IDX)
        {
            var oRDER_IDXParameter = oRDER_IDX.HasValue ?
                new ObjectParameter("ORDER_IDX", oRDER_IDX) :
                new ObjectParameter("ORDER_IDX", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_ORDERLIST_DETAIL_BODY_Result>("SP_ADMIN_ORDERLIST_DETAIL_BODY", oRDER_IDXParameter);
        }
    
        public virtual ObjectResult<SP_ADMIN_ORDERLIST_DETAIL_LIST_Result> SP_ADMIN_ORDERLIST_DETAIL_LIST(Nullable<int> oRDER_IDX)
        {
            var oRDER_IDXParameter = oRDER_IDX.HasValue ?
                new ObjectParameter("ORDER_IDX", oRDER_IDX) :
                new ObjectParameter("ORDER_IDX", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_ORDERLIST_DETAIL_LIST_Result>("SP_ADMIN_ORDERLIST_DETAIL_LIST", oRDER_IDXParameter);
        }
    
        public virtual ObjectResult<string> SP_ADMIN_ORDERLIST_DETAIL_LOG(Nullable<int> oRDER_IDX)
        {
            var oRDER_IDXParameter = oRDER_IDX.HasValue ?
                new ObjectParameter("ORDER_IDX", oRDER_IDX) :
                new ObjectParameter("ORDER_IDX", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("SP_ADMIN_ORDERLIST_DETAIL_LOG", oRDER_IDXParameter);
        }
    
        public virtual ObjectResult<SP_ADMIN_ORDER_LIST_Cnt> SP_ADMIN_ORDER_LIST_CNT(string fROM_DATE, string tO_DATE, string mEMBER_GBN, Nullable<bool> mEMBER_GRADE_B, Nullable<bool> mEMBER_GRADE_S, Nullable<bool> mEMBER_GRADE_G, Nullable<bool> mEMBER_GRADE_V, string pAY_GBN, Nullable<bool> pAT_GUBUN_W, Nullable<bool> pAT_GUBUN_M, Nullable<bool> dETAIL_STATUS_20, Nullable<bool> dETAIL_STATUS_10, Nullable<bool> dETAIL_STATUS_30, Nullable<bool> dETAIL_STATUS_40, Nullable<bool> dETAIL_STATUS_50, Nullable<bool> dETAIL_STATUS_60, Nullable<bool> dETAIL_STATUS_90, Nullable<bool> dETAIL_STATUS_80, Nullable<bool> dETAIL_STATUS_70, string dELIVERY_FROM_DATE, string dELIVERY_TO_DATE, string sEARCH_COL, string sEARCH_KEYWORD)
        {
            var fROM_DATEParameter = fROM_DATE != null ?
                new ObjectParameter("FROM_DATE", fROM_DATE) :
                new ObjectParameter("FROM_DATE", typeof(string));
    
            var tO_DATEParameter = tO_DATE != null ?
                new ObjectParameter("TO_DATE", tO_DATE) :
                new ObjectParameter("TO_DATE", typeof(string));
    
            var mEMBER_GBNParameter = mEMBER_GBN != null ?
                new ObjectParameter("MEMBER_GBN", mEMBER_GBN) :
                new ObjectParameter("MEMBER_GBN", typeof(string));
    
            var mEMBER_GRADE_BParameter = mEMBER_GRADE_B.HasValue ?
                new ObjectParameter("MEMBER_GRADE_B", mEMBER_GRADE_B) :
                new ObjectParameter("MEMBER_GRADE_B", typeof(bool));
    
            var mEMBER_GRADE_SParameter = mEMBER_GRADE_S.HasValue ?
                new ObjectParameter("MEMBER_GRADE_S", mEMBER_GRADE_S) :
                new ObjectParameter("MEMBER_GRADE_S", typeof(bool));
    
            var mEMBER_GRADE_GParameter = mEMBER_GRADE_G.HasValue ?
                new ObjectParameter("MEMBER_GRADE_G", mEMBER_GRADE_G) :
                new ObjectParameter("MEMBER_GRADE_G", typeof(bool));
    
            var mEMBER_GRADE_VParameter = mEMBER_GRADE_V.HasValue ?
                new ObjectParameter("MEMBER_GRADE_V", mEMBER_GRADE_V) :
                new ObjectParameter("MEMBER_GRADE_V", typeof(bool));
    
            var pAY_GBNParameter = pAY_GBN != null ?
                new ObjectParameter("PAY_GBN", pAY_GBN) :
                new ObjectParameter("PAY_GBN", typeof(string));
    
            var pAT_GUBUN_WParameter = pAT_GUBUN_W.HasValue ?
                new ObjectParameter("PAT_GUBUN_W", pAT_GUBUN_W) :
                new ObjectParameter("PAT_GUBUN_W", typeof(bool));
    
            var pAT_GUBUN_MParameter = pAT_GUBUN_M.HasValue ?
                new ObjectParameter("PAT_GUBUN_M", pAT_GUBUN_M) :
                new ObjectParameter("PAT_GUBUN_M", typeof(bool));
    
            var dETAIL_STATUS_20Parameter = dETAIL_STATUS_20.HasValue ?
                new ObjectParameter("DETAIL_STATUS_20", dETAIL_STATUS_20) :
                new ObjectParameter("DETAIL_STATUS_20", typeof(bool));
    
            var dETAIL_STATUS_10Parameter = dETAIL_STATUS_10.HasValue ?
                new ObjectParameter("DETAIL_STATUS_10", dETAIL_STATUS_10) :
                new ObjectParameter("DETAIL_STATUS_10", typeof(bool));
    
            var dETAIL_STATUS_30Parameter = dETAIL_STATUS_30.HasValue ?
                new ObjectParameter("DETAIL_STATUS_30", dETAIL_STATUS_30) :
                new ObjectParameter("DETAIL_STATUS_30", typeof(bool));
    
            var dETAIL_STATUS_40Parameter = dETAIL_STATUS_40.HasValue ?
                new ObjectParameter("DETAIL_STATUS_40", dETAIL_STATUS_40) :
                new ObjectParameter("DETAIL_STATUS_40", typeof(bool));
    
            var dETAIL_STATUS_50Parameter = dETAIL_STATUS_50.HasValue ?
                new ObjectParameter("DETAIL_STATUS_50", dETAIL_STATUS_50) :
                new ObjectParameter("DETAIL_STATUS_50", typeof(bool));
    
            var dETAIL_STATUS_60Parameter = dETAIL_STATUS_60.HasValue ?
                new ObjectParameter("DETAIL_STATUS_60", dETAIL_STATUS_60) :
                new ObjectParameter("DETAIL_STATUS_60", typeof(bool));
    
            var dETAIL_STATUS_90Parameter = dETAIL_STATUS_90.HasValue ?
                new ObjectParameter("DETAIL_STATUS_90", dETAIL_STATUS_90) :
                new ObjectParameter("DETAIL_STATUS_90", typeof(bool));
    
            var dETAIL_STATUS_80Parameter = dETAIL_STATUS_80.HasValue ?
                new ObjectParameter("DETAIL_STATUS_80", dETAIL_STATUS_80) :
                new ObjectParameter("DETAIL_STATUS_80", typeof(bool));
    
            var dETAIL_STATUS_70Parameter = dETAIL_STATUS_70.HasValue ?
                new ObjectParameter("DETAIL_STATUS_70", dETAIL_STATUS_70) :
                new ObjectParameter("DETAIL_STATUS_70", typeof(bool));
    
            var dELIVERY_FROM_DATEParameter = dELIVERY_FROM_DATE != null ?
                new ObjectParameter("DELIVERY_FROM_DATE", dELIVERY_FROM_DATE) :
                new ObjectParameter("DELIVERY_FROM_DATE", typeof(string));
    
            var dELIVERY_TO_DATEParameter = dELIVERY_TO_DATE != null ?
                new ObjectParameter("DELIVERY_TO_DATE", dELIVERY_TO_DATE) :
                new ObjectParameter("DELIVERY_TO_DATE", typeof(string));
    
            var sEARCH_COLParameter = sEARCH_COL != null ?
                new ObjectParameter("SEARCH_COL", sEARCH_COL) :
                new ObjectParameter("SEARCH_COL", typeof(string));
    
            var sEARCH_KEYWORDParameter = sEARCH_KEYWORD != null ?
                new ObjectParameter("SEARCH_KEYWORD", sEARCH_KEYWORD) :
                new ObjectParameter("SEARCH_KEYWORD", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_ORDER_LIST_Cnt>("SP_ADMIN_ORDER_LIST_CNT", fROM_DATEParameter, tO_DATEParameter, mEMBER_GBNParameter, mEMBER_GRADE_BParameter, mEMBER_GRADE_SParameter, mEMBER_GRADE_GParameter, mEMBER_GRADE_VParameter, pAY_GBNParameter, pAT_GUBUN_WParameter, pAT_GUBUN_MParameter, dETAIL_STATUS_20Parameter, dETAIL_STATUS_10Parameter, dETAIL_STATUS_30Parameter, dETAIL_STATUS_40Parameter, dETAIL_STATUS_50Parameter, dETAIL_STATUS_60Parameter, dETAIL_STATUS_90Parameter, dETAIL_STATUS_80Parameter, dETAIL_STATUS_70Parameter, dELIVERY_FROM_DATEParameter, dELIVERY_TO_DATEParameter, sEARCH_COLParameter, sEARCH_KEYWORDParameter);
        }
    
        public virtual int SP_ADMIN_ORDER_PART_CANCEL_INSERT(Nullable<int> oRDER_IDX, string pAT_TID, string oLD_PAT_TID, Nullable<long> cANCEL_PRICE, Nullable<long> rEMAINS_PRICE, string eMAIL, Nullable<long> pRTC_Remains, string pRTC_Type, Nullable<long> pRTC_Price, Nullable<int> pRTC_Cnt, string rEG_ID, string rEG_IP)
        {
            var oRDER_IDXParameter = oRDER_IDX.HasValue ?
                new ObjectParameter("ORDER_IDX", oRDER_IDX) :
                new ObjectParameter("ORDER_IDX", typeof(int));
    
            var pAT_TIDParameter = pAT_TID != null ?
                new ObjectParameter("PAT_TID", pAT_TID) :
                new ObjectParameter("PAT_TID", typeof(string));
    
            var oLD_PAT_TIDParameter = oLD_PAT_TID != null ?
                new ObjectParameter("OLD_PAT_TID", oLD_PAT_TID) :
                new ObjectParameter("OLD_PAT_TID", typeof(string));
    
            var cANCEL_PRICEParameter = cANCEL_PRICE.HasValue ?
                new ObjectParameter("CANCEL_PRICE", cANCEL_PRICE) :
                new ObjectParameter("CANCEL_PRICE", typeof(long));
    
            var rEMAINS_PRICEParameter = rEMAINS_PRICE.HasValue ?
                new ObjectParameter("REMAINS_PRICE", rEMAINS_PRICE) :
                new ObjectParameter("REMAINS_PRICE", typeof(long));
    
            var eMAILParameter = eMAIL != null ?
                new ObjectParameter("EMAIL", eMAIL) :
                new ObjectParameter("EMAIL", typeof(string));
    
            var pRTC_RemainsParameter = pRTC_Remains.HasValue ?
                new ObjectParameter("PRTC_Remains", pRTC_Remains) :
                new ObjectParameter("PRTC_Remains", typeof(long));
    
            var pRTC_TypeParameter = pRTC_Type != null ?
                new ObjectParameter("PRTC_Type", pRTC_Type) :
                new ObjectParameter("PRTC_Type", typeof(string));
    
            var pRTC_PriceParameter = pRTC_Price.HasValue ?
                new ObjectParameter("PRTC_Price", pRTC_Price) :
                new ObjectParameter("PRTC_Price", typeof(long));
    
            var pRTC_CntParameter = pRTC_Cnt.HasValue ?
                new ObjectParameter("PRTC_Cnt", pRTC_Cnt) :
                new ObjectParameter("PRTC_Cnt", typeof(int));
    
            var rEG_IDParameter = rEG_ID != null ?
                new ObjectParameter("REG_ID", rEG_ID) :
                new ObjectParameter("REG_ID", typeof(string));
    
            var rEG_IPParameter = rEG_IP != null ?
                new ObjectParameter("REG_IP", rEG_IP) :
                new ObjectParameter("REG_IP", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_ORDER_PART_CANCEL_INSERT", oRDER_IDXParameter, pAT_TIDParameter, oLD_PAT_TIDParameter, cANCEL_PRICEParameter, rEMAINS_PRICEParameter, eMAILParameter, pRTC_RemainsParameter, pRTC_TypeParameter, pRTC_PriceParameter, pRTC_CntParameter, rEG_IDParameter, rEG_IPParameter);
        }
    
        public virtual ObjectResult<SP_ADMIN_ORDER_PART_CANCEL_SELECT_Result> SP_ADMIN_ORDER_PART_CANCEL_SELECT(Nullable<int> oRDER_IDX)
        {
            var oRDER_IDXParameter = oRDER_IDX.HasValue ?
                new ObjectParameter("ORDER_IDX", oRDER_IDX) :
                new ObjectParameter("ORDER_IDX", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_ORDER_PART_CANCEL_SELECT_Result>("SP_ADMIN_ORDER_PART_CANCEL_SELECT", oRDER_IDXParameter);
        }
    
        public virtual ObjectResult<SP_ADMIN_ORDER_PART_CANCEL_TOP_SELECT_Result> SP_ADMIN_ORDER_PART_CANCEL_TOP_SELECT(Nullable<int> oRDER_IDX)
        {
            var oRDER_IDXParameter = oRDER_IDX.HasValue ?
                new ObjectParameter("ORDER_IDX", oRDER_IDX) :
                new ObjectParameter("ORDER_IDX", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_ORDER_PART_CANCEL_TOP_SELECT_Result>("SP_ADMIN_ORDER_PART_CANCEL_TOP_SELECT", oRDER_IDXParameter);
        }
    
        public virtual ObjectResult<SP_ADMIN_ORDER_TO_DELIVERYEXCEL_Result> SP_ADMIN_ORDER_TO_DELIVERYEXCEL(string oRDER_IDX_LIST)
        {
            var oRDER_IDX_LISTParameter = oRDER_IDX_LIST != null ?
                new ObjectParameter("ORDER_IDX_LIST", oRDER_IDX_LIST) :
                new ObjectParameter("ORDER_IDX_LIST", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_ORDER_TO_DELIVERYEXCEL_Result>("SP_ADMIN_ORDER_TO_DELIVERYEXCEL", oRDER_IDX_LISTParameter);
        }
    
        public virtual ObjectResult<string> SP_ADMIN_ORDER_FIND_PAT_GBN(string pAT_TID)
        {
            var pAT_TIDParameter = pAT_TID != null ?
                new ObjectParameter("PAT_TID", pAT_TID) :
                new ObjectParameter("PAT_TID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("SP_ADMIN_ORDER_FIND_PAT_GBN", pAT_TIDParameter);
        }
    
        public virtual ObjectResult<SP_ADMIN_ORDER_MEMBER_STATUS_Result> SP_ADMIN_ORDER_MEMBER_STATUS(string m_ID)
        {
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_ORDER_MEMBER_STATUS_Result>("SP_ADMIN_ORDER_MEMBER_STATUS", m_IDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> SP_ADMIN_ORDER_MEMBER_CNT(string m_ID)
        {
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("SP_ADMIN_ORDER_MEMBER_CNT", m_IDParameter);
        }
    
        public virtual ObjectResult<SP_ADMIN_ORDER_LIST_Result> SP_ADMIN_ORDER_MEMBER_LIST(string m_ID, Nullable<int> pAGE, Nullable<int> pAGESIZE)
        {
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            var pAGEParameter = pAGE.HasValue ?
                new ObjectParameter("PAGE", pAGE) :
                new ObjectParameter("PAGE", typeof(int));
    
            var pAGESIZEParameter = pAGESIZE.HasValue ?
                new ObjectParameter("PAGESIZE", pAGESIZE) :
                new ObjectParameter("PAGESIZE", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_ORDER_LIST_Result>("SP_ADMIN_ORDER_MEMBER_LIST", m_IDParameter, pAGEParameter, pAGESIZEParameter);
        }
    
        public virtual int SP_ADMIN_ORDER_ESCROW_SET(Nullable<int> oRDER_IDX, string gUBUN, string sET_DATE)
        {
            var oRDER_IDXParameter = oRDER_IDX.HasValue ?
                new ObjectParameter("ORDER_IDX", oRDER_IDX) :
                new ObjectParameter("ORDER_IDX", typeof(int));
    
            var gUBUNParameter = gUBUN != null ?
                new ObjectParameter("GUBUN", gUBUN) :
                new ObjectParameter("GUBUN", typeof(string));
    
            var sET_DATEParameter = sET_DATE != null ?
                new ObjectParameter("SET_DATE", sET_DATE) :
                new ObjectParameter("SET_DATE", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_ORDER_ESCROW_SET", oRDER_IDXParameter, gUBUNParameter, sET_DATEParameter);
        }
    
        public virtual int SP_ADMIN_ORDER_DETAIL_POINT_SET(Nullable<int> oRDER_DETAIL_IDX, string tOBE_STATUS, string mASTER_CONTROL, string rEG_ID)
        {
            var oRDER_DETAIL_IDXParameter = oRDER_DETAIL_IDX.HasValue ?
                new ObjectParameter("ORDER_DETAIL_IDX", oRDER_DETAIL_IDX) :
                new ObjectParameter("ORDER_DETAIL_IDX", typeof(int));
    
            var tOBE_STATUSParameter = tOBE_STATUS != null ?
                new ObjectParameter("TOBE_STATUS", tOBE_STATUS) :
                new ObjectParameter("TOBE_STATUS", typeof(string));
    
            var mASTER_CONTROLParameter = mASTER_CONTROL != null ?
                new ObjectParameter("MASTER_CONTROL", mASTER_CONTROL) :
                new ObjectParameter("MASTER_CONTROL", typeof(string));
    
            var rEG_IDParameter = rEG_ID != null ?
                new ObjectParameter("REG_ID", rEG_ID) :
                new ObjectParameter("REG_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_ORDER_DETAIL_POINT_SET", oRDER_DETAIL_IDXParameter, tOBE_STATUSParameter, mASTER_CONTROLParameter, rEG_IDParameter);
        }
    
        public virtual int SP_ADMIN_ORDER_MASTER_POINT_SET(Nullable<int> oRDER_IDX, string tOBE_STATUS, string rEG_ID)
        {
            var oRDER_IDXParameter = oRDER_IDX.HasValue ?
                new ObjectParameter("ORDER_IDX", oRDER_IDX) :
                new ObjectParameter("ORDER_IDX", typeof(int));
    
            var tOBE_STATUSParameter = tOBE_STATUS != null ?
                new ObjectParameter("TOBE_STATUS", tOBE_STATUS) :
                new ObjectParameter("TOBE_STATUS", typeof(string));
    
            var rEG_IDParameter = rEG_ID != null ?
                new ObjectParameter("REG_ID", rEG_ID) :
                new ObjectParameter("REG_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_ORDER_MASTER_POINT_SET", oRDER_IDXParameter, tOBE_STATUSParameter, rEG_IDParameter);
        }
    }
}
