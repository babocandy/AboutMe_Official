﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AboutMe.Domain.Entity.Order
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class OrderEntities : DbContext
    {
        public OrderEntities()
            : base("name=OrderEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
    
        public virtual ObjectResult<SP_ORDER_PAY_Result> SP_ORDER_PAY(Nullable<int> oRDER_IDX, string pAY_GBN, string cARD_GBN, string iNSTLMT_AT, string bANK_CODE, string pAT_TID, string rEAL_ACCOUNT_AT, string cASHRECEIPT_SE_CODE, string cASHRECEIPT_RESULT_CODE, string hTTP_USER_AGENT, string pAT_GUBUN, string sVR_DOMAIN, string vACT_Num, string vACT_BankCode, string vACT_Name, string vACT_InputName, string vACT_Date, string vACT_Time)
        {
            var oRDER_IDXParameter = oRDER_IDX.HasValue ?
                new ObjectParameter("ORDER_IDX", oRDER_IDX) :
                new ObjectParameter("ORDER_IDX", typeof(int));
    
            var pAY_GBNParameter = pAY_GBN != null ?
                new ObjectParameter("PAY_GBN", pAY_GBN) :
                new ObjectParameter("PAY_GBN", typeof(string));
    
            var cARD_GBNParameter = cARD_GBN != null ?
                new ObjectParameter("CARD_GBN", cARD_GBN) :
                new ObjectParameter("CARD_GBN", typeof(string));
    
            var iNSTLMT_ATParameter = iNSTLMT_AT != null ?
                new ObjectParameter("INSTLMT_AT", iNSTLMT_AT) :
                new ObjectParameter("INSTLMT_AT", typeof(string));
    
            var bANK_CODEParameter = bANK_CODE != null ?
                new ObjectParameter("BANK_CODE", bANK_CODE) :
                new ObjectParameter("BANK_CODE", typeof(string));
    
            var pAT_TIDParameter = pAT_TID != null ?
                new ObjectParameter("PAT_TID", pAT_TID) :
                new ObjectParameter("PAT_TID", typeof(string));
    
            var rEAL_ACCOUNT_ATParameter = rEAL_ACCOUNT_AT != null ?
                new ObjectParameter("REAL_ACCOUNT_AT", rEAL_ACCOUNT_AT) :
                new ObjectParameter("REAL_ACCOUNT_AT", typeof(string));
    
            var cASHRECEIPT_SE_CODEParameter = cASHRECEIPT_SE_CODE != null ?
                new ObjectParameter("CASHRECEIPT_SE_CODE", cASHRECEIPT_SE_CODE) :
                new ObjectParameter("CASHRECEIPT_SE_CODE", typeof(string));
    
            var cASHRECEIPT_RESULT_CODEParameter = cASHRECEIPT_RESULT_CODE != null ?
                new ObjectParameter("CASHRECEIPT_RESULT_CODE", cASHRECEIPT_RESULT_CODE) :
                new ObjectParameter("CASHRECEIPT_RESULT_CODE", typeof(string));
    
            var hTTP_USER_AGENTParameter = hTTP_USER_AGENT != null ?
                new ObjectParameter("HTTP_USER_AGENT", hTTP_USER_AGENT) :
                new ObjectParameter("HTTP_USER_AGENT", typeof(string));
    
            var pAT_GUBUNParameter = pAT_GUBUN != null ?
                new ObjectParameter("PAT_GUBUN", pAT_GUBUN) :
                new ObjectParameter("PAT_GUBUN", typeof(string));
    
            var sVR_DOMAINParameter = sVR_DOMAIN != null ?
                new ObjectParameter("SVR_DOMAIN", sVR_DOMAIN) :
                new ObjectParameter("SVR_DOMAIN", typeof(string));
    
            var vACT_NumParameter = vACT_Num != null ?
                new ObjectParameter("VACT_Num", vACT_Num) :
                new ObjectParameter("VACT_Num", typeof(string));
    
            var vACT_BankCodeParameter = vACT_BankCode != null ?
                new ObjectParameter("VACT_BankCode", vACT_BankCode) :
                new ObjectParameter("VACT_BankCode", typeof(string));
    
            var vACT_NameParameter = vACT_Name != null ?
                new ObjectParameter("VACT_Name", vACT_Name) :
                new ObjectParameter("VACT_Name", typeof(string));
    
            var vACT_InputNameParameter = vACT_InputName != null ?
                new ObjectParameter("VACT_InputName", vACT_InputName) :
                new ObjectParameter("VACT_InputName", typeof(string));
    
            var vACT_DateParameter = vACT_Date != null ?
                new ObjectParameter("VACT_Date", vACT_Date) :
                new ObjectParameter("VACT_Date", typeof(string));
    
            var vACT_TimeParameter = vACT_Time != null ?
                new ObjectParameter("VACT_Time", vACT_Time) :
                new ObjectParameter("VACT_Time", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ORDER_PAY_Result>("SP_ORDER_PAY", oRDER_IDXParameter, pAY_GBNParameter, cARD_GBNParameter, iNSTLMT_ATParameter, bANK_CODEParameter, pAT_TIDParameter, rEAL_ACCOUNT_ATParameter, cASHRECEIPT_SE_CODEParameter, cASHRECEIPT_RESULT_CODEParameter, hTTP_USER_AGENTParameter, pAT_GUBUNParameter, sVR_DOMAINParameter, vACT_NumParameter, vACT_BankCodeParameter, vACT_NameParameter, vACT_InputNameParameter, vACT_DateParameter, vACT_TimeParameter);
        }
    
        public virtual ObjectResult<SP_ORDER_STEP2_Result> SP_ORDER_STEP1(string m_ID, string sESSION_ID, string p_CODE_LIST, string p_COUNT_LIST)
        {
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            var sESSION_IDParameter = sESSION_ID != null ?
                new ObjectParameter("SESSION_ID", sESSION_ID) :
                new ObjectParameter("SESSION_ID", typeof(string));
    
            var p_CODE_LISTParameter = p_CODE_LIST != null ?
                new ObjectParameter("P_CODE_LIST", p_CODE_LIST) :
                new ObjectParameter("P_CODE_LIST", typeof(string));
    
            var p_COUNT_LISTParameter = p_COUNT_LIST != null ?
                new ObjectParameter("P_COUNT_LIST", p_COUNT_LIST) :
                new ObjectParameter("P_COUNT_LIST", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ORDER_STEP2_Result>("SP_ORDER_STEP1", m_IDParameter, sESSION_IDParameter, p_CODE_LISTParameter, p_COUNT_LISTParameter);
        }
    
        public virtual ObjectResult<SP_ORDER_STEP2_BASEADDR_INFO_Result> SP_ORDER_STEP2_BASEADDR_INFO(string m_ID)
        {
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ORDER_STEP2_BASEADDR_INFO_Result>("SP_ORDER_STEP2_BASEADDR_INFO", m_IDParameter);
        }
    
        public virtual ObjectResult<SP_ORDER_STEP2_DISCOUNT_INFO_Result> SP_ORDER_STEP2_DISCOUNT_INFO(Nullable<int> oRDER_IDX)
        {
            var oRDER_IDXParameter = oRDER_IDX.HasValue ?
                new ObjectParameter("ORDER_IDX", oRDER_IDX) :
                new ObjectParameter("ORDER_IDX", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ORDER_STEP2_DISCOUNT_INFO_Result>("SP_ORDER_STEP2_DISCOUNT_INFO", oRDER_IDXParameter);
        }
    
        public virtual ObjectResult<SP_ORDER_STEP2_PRICE_INFO_Result> SP_ORDER_STEP2_PRICE_INFO(Nullable<int> oRDER_IDX)
        {
            var oRDER_IDXParameter = oRDER_IDX.HasValue ?
                new ObjectParameter("ORDER_IDX", oRDER_IDX) :
                new ObjectParameter("ORDER_IDX", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ORDER_STEP2_PRICE_INFO_Result>("SP_ORDER_STEP2_PRICE_INFO", oRDER_IDXParameter);
        }
    
        public virtual ObjectResult<SP_ORDER_STEP2_PRODUCT_LIST_Result> SP_ORDER_STEP2_PRODUCT_LIST(Nullable<int> oRDER_IDX)
        {
            var oRDER_IDXParameter = oRDER_IDX.HasValue ?
                new ObjectParameter("ORDER_IDX", oRDER_IDX) :
                new ObjectParameter("ORDER_IDX", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ORDER_STEP2_PRODUCT_LIST_Result>("SP_ORDER_STEP2_PRODUCT_LIST", oRDER_IDXParameter);
        }
    
        public virtual ObjectResult<SP_ORDER_STEP2_RECENTADDR_INFO_Result> SP_ORDER_STEP2_RECENTADDR_INFO(string m_ID)
        {
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ORDER_STEP2_RECENTADDR_INFO_Result>("SP_ORDER_STEP2_RECENTADDR_INFO", m_IDParameter);
        }
    
        public virtual int SP_ORDER_STEP2_ADDR_SAVE(string m_ID, string sENDER_POST1, string sENDER_POST2, string sENDER_ADDR1, string sENDER_ADDR2, string sENDER_TEL1, string sENDER_TEL2, string sENDER_TEL3, string sENDER_HP1, string sENDER_HP2, string sENDER_HP3, string sENDER_EMAIL1, string sENDER_EMAIL2)
        {
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            var sENDER_POST1Parameter = sENDER_POST1 != null ?
                new ObjectParameter("SENDER_POST1", sENDER_POST1) :
                new ObjectParameter("SENDER_POST1", typeof(string));
    
            var sENDER_POST2Parameter = sENDER_POST2 != null ?
                new ObjectParameter("SENDER_POST2", sENDER_POST2) :
                new ObjectParameter("SENDER_POST2", typeof(string));
    
            var sENDER_ADDR1Parameter = sENDER_ADDR1 != null ?
                new ObjectParameter("SENDER_ADDR1", sENDER_ADDR1) :
                new ObjectParameter("SENDER_ADDR1", typeof(string));
    
            var sENDER_ADDR2Parameter = sENDER_ADDR2 != null ?
                new ObjectParameter("SENDER_ADDR2", sENDER_ADDR2) :
                new ObjectParameter("SENDER_ADDR2", typeof(string));
    
            var sENDER_TEL1Parameter = sENDER_TEL1 != null ?
                new ObjectParameter("SENDER_TEL1", sENDER_TEL1) :
                new ObjectParameter("SENDER_TEL1", typeof(string));
    
            var sENDER_TEL2Parameter = sENDER_TEL2 != null ?
                new ObjectParameter("SENDER_TEL2", sENDER_TEL2) :
                new ObjectParameter("SENDER_TEL2", typeof(string));
    
            var sENDER_TEL3Parameter = sENDER_TEL3 != null ?
                new ObjectParameter("SENDER_TEL3", sENDER_TEL3) :
                new ObjectParameter("SENDER_TEL3", typeof(string));
    
            var sENDER_HP1Parameter = sENDER_HP1 != null ?
                new ObjectParameter("SENDER_HP1", sENDER_HP1) :
                new ObjectParameter("SENDER_HP1", typeof(string));
    
            var sENDER_HP2Parameter = sENDER_HP2 != null ?
                new ObjectParameter("SENDER_HP2", sENDER_HP2) :
                new ObjectParameter("SENDER_HP2", typeof(string));
    
            var sENDER_HP3Parameter = sENDER_HP3 != null ?
                new ObjectParameter("SENDER_HP3", sENDER_HP3) :
                new ObjectParameter("SENDER_HP3", typeof(string));
    
            var sENDER_EMAIL1Parameter = sENDER_EMAIL1 != null ?
                new ObjectParameter("SENDER_EMAIL1", sENDER_EMAIL1) :
                new ObjectParameter("SENDER_EMAIL1", typeof(string));
    
            var sENDER_EMAIL2Parameter = sENDER_EMAIL2 != null ?
                new ObjectParameter("SENDER_EMAIL2", sENDER_EMAIL2) :
                new ObjectParameter("SENDER_EMAIL2", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ORDER_STEP2_ADDR_SAVE", m_IDParameter, sENDER_POST1Parameter, sENDER_POST2Parameter, sENDER_ADDR1Parameter, sENDER_ADDR2Parameter, sENDER_TEL1Parameter, sENDER_TEL2Parameter, sENDER_TEL3Parameter, sENDER_HP1Parameter, sENDER_HP2Parameter, sENDER_HP3Parameter, sENDER_EMAIL1Parameter, sENDER_EMAIL2Parameter);
        }
    
        public virtual ObjectResult<SP_ORDER_STEP2_COUPON_SEARCH_Result> SP_ORDER_STEP2_COUPON_SEARCH(string m_ID, string p_CODE, string dEVICE_GBN)
        {
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            var p_CODEParameter = p_CODE != null ?
                new ObjectParameter("P_CODE", p_CODE) :
                new ObjectParameter("P_CODE", typeof(string));
    
            var dEVICE_GBNParameter = dEVICE_GBN != null ?
                new ObjectParameter("DEVICE_GBN", dEVICE_GBN) :
                new ObjectParameter("DEVICE_GBN", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ORDER_STEP2_COUPON_SEARCH_Result>("SP_ORDER_STEP2_COUPON_SEARCH", m_IDParameter, p_CODEParameter, dEVICE_GBNParameter);
        }
    
        public virtual ObjectResult<SP_ORDER_MEMBER_SEL_Result> SP_ORDER_MEMBER_SEL(string m_ID)
        {
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ORDER_MEMBER_SEL_Result>("SP_ORDER_MEMBER_SEL", m_IDParameter);
        }
    
        public virtual int SP_ORDER_STEP2_COUPON_APPLY(string m_ID, Nullable<int> oRDER_DETAIL_IDX, Nullable<int> iDX_COUPON_NUMBER)
        {
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            var oRDER_DETAIL_IDXParameter = oRDER_DETAIL_IDX.HasValue ?
                new ObjectParameter("ORDER_DETAIL_IDX", oRDER_DETAIL_IDX) :
                new ObjectParameter("ORDER_DETAIL_IDX", typeof(int));
    
            var iDX_COUPON_NUMBERParameter = iDX_COUPON_NUMBER.HasValue ?
                new ObjectParameter("IDX_COUPON_NUMBER", iDX_COUPON_NUMBER) :
                new ObjectParameter("IDX_COUPON_NUMBER", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ORDER_STEP2_COUPON_APPLY", m_IDParameter, oRDER_DETAIL_IDXParameter, iDX_COUPON_NUMBERParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> SP_ORDER_STEP2_POINT_APPLY(string m_ID, Nullable<int> oRDER_IDX, Nullable<int> uSE_POINT)
        {
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            var oRDER_IDXParameter = oRDER_IDX.HasValue ?
                new ObjectParameter("ORDER_IDX", oRDER_IDX) :
                new ObjectParameter("ORDER_IDX", typeof(int));
    
            var uSE_POINTParameter = uSE_POINT.HasValue ?
                new ObjectParameter("USE_POINT", uSE_POINT) :
                new ObjectParameter("USE_POINT", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("SP_ORDER_STEP2_POINT_APPLY", m_IDParameter, oRDER_IDXParameter, uSE_POINTParameter);
        }
    
        public virtual int SP_ORDER_STEP2_TRANSCOUPON_APPLY(string m_ID, Nullable<int> oRDER_IDX, Nullable<int> iDX_COUPON_NUMBER)
        {
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            var oRDER_IDXParameter = oRDER_IDX.HasValue ?
                new ObjectParameter("ORDER_IDX", oRDER_IDX) :
                new ObjectParameter("ORDER_IDX", typeof(int));
    
            var iDX_COUPON_NUMBERParameter = iDX_COUPON_NUMBER.HasValue ?
                new ObjectParameter("IDX_COUPON_NUMBER", iDX_COUPON_NUMBER) :
                new ObjectParameter("IDX_COUPON_NUMBER", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ORDER_STEP2_TRANSCOUPON_APPLY", m_IDParameter, oRDER_IDXParameter, iDX_COUPON_NUMBERParameter);
        }
    
        public virtual int SP_ORDER_STEP2_SENDER_RECEIVER_SAVE(Nullable<int> oRDER_IDX, string sENDER_NAME, string sENDER_POST1, string sENDER_POST2, string sENDER_ADDR1, string sENDER_ADDR2, string sENDER_TEL1, string sENDER_TEL2, string sENDER_TEL3, string sENDER_HP1, string sENDER_HP2, string sENDER_HP3, string sENDER_EMAIL1, string sENDER_EMAIL2, string rECEIVER_NAME, string rECEIVER_POST1, string rECEIVER_POST2, string rECEIVER_ADDR1, string rECEIVER_ADDR2, string rECEIVER_TEL1, string rECEIVER_TEL2, string rECEIVER_TEL3, string rECEIVER_HP1, string rECEIVER_HP2, string rECEIVER_HP3, string oRDER_MEMO, string nOMEMBER_PASS)
        {
            var oRDER_IDXParameter = oRDER_IDX.HasValue ?
                new ObjectParameter("ORDER_IDX", oRDER_IDX) :
                new ObjectParameter("ORDER_IDX", typeof(int));
    
            var sENDER_NAMEParameter = sENDER_NAME != null ?
                new ObjectParameter("SENDER_NAME", sENDER_NAME) :
                new ObjectParameter("SENDER_NAME", typeof(string));
    
            var sENDER_POST1Parameter = sENDER_POST1 != null ?
                new ObjectParameter("SENDER_POST1", sENDER_POST1) :
                new ObjectParameter("SENDER_POST1", typeof(string));
    
            var sENDER_POST2Parameter = sENDER_POST2 != null ?
                new ObjectParameter("SENDER_POST2", sENDER_POST2) :
                new ObjectParameter("SENDER_POST2", typeof(string));
    
            var sENDER_ADDR1Parameter = sENDER_ADDR1 != null ?
                new ObjectParameter("SENDER_ADDR1", sENDER_ADDR1) :
                new ObjectParameter("SENDER_ADDR1", typeof(string));
    
            var sENDER_ADDR2Parameter = sENDER_ADDR2 != null ?
                new ObjectParameter("SENDER_ADDR2", sENDER_ADDR2) :
                new ObjectParameter("SENDER_ADDR2", typeof(string));
    
            var sENDER_TEL1Parameter = sENDER_TEL1 != null ?
                new ObjectParameter("SENDER_TEL1", sENDER_TEL1) :
                new ObjectParameter("SENDER_TEL1", typeof(string));
    
            var sENDER_TEL2Parameter = sENDER_TEL2 != null ?
                new ObjectParameter("SENDER_TEL2", sENDER_TEL2) :
                new ObjectParameter("SENDER_TEL2", typeof(string));
    
            var sENDER_TEL3Parameter = sENDER_TEL3 != null ?
                new ObjectParameter("SENDER_TEL3", sENDER_TEL3) :
                new ObjectParameter("SENDER_TEL3", typeof(string));
    
            var sENDER_HP1Parameter = sENDER_HP1 != null ?
                new ObjectParameter("SENDER_HP1", sENDER_HP1) :
                new ObjectParameter("SENDER_HP1", typeof(string));
    
            var sENDER_HP2Parameter = sENDER_HP2 != null ?
                new ObjectParameter("SENDER_HP2", sENDER_HP2) :
                new ObjectParameter("SENDER_HP2", typeof(string));
    
            var sENDER_HP3Parameter = sENDER_HP3 != null ?
                new ObjectParameter("SENDER_HP3", sENDER_HP3) :
                new ObjectParameter("SENDER_HP3", typeof(string));
    
            var sENDER_EMAIL1Parameter = sENDER_EMAIL1 != null ?
                new ObjectParameter("SENDER_EMAIL1", sENDER_EMAIL1) :
                new ObjectParameter("SENDER_EMAIL1", typeof(string));
    
            var sENDER_EMAIL2Parameter = sENDER_EMAIL2 != null ?
                new ObjectParameter("SENDER_EMAIL2", sENDER_EMAIL2) :
                new ObjectParameter("SENDER_EMAIL2", typeof(string));
    
            var rECEIVER_NAMEParameter = rECEIVER_NAME != null ?
                new ObjectParameter("RECEIVER_NAME", rECEIVER_NAME) :
                new ObjectParameter("RECEIVER_NAME", typeof(string));
    
            var rECEIVER_POST1Parameter = rECEIVER_POST1 != null ?
                new ObjectParameter("RECEIVER_POST1", rECEIVER_POST1) :
                new ObjectParameter("RECEIVER_POST1", typeof(string));
    
            var rECEIVER_POST2Parameter = rECEIVER_POST2 != null ?
                new ObjectParameter("RECEIVER_POST2", rECEIVER_POST2) :
                new ObjectParameter("RECEIVER_POST2", typeof(string));
    
            var rECEIVER_ADDR1Parameter = rECEIVER_ADDR1 != null ?
                new ObjectParameter("RECEIVER_ADDR1", rECEIVER_ADDR1) :
                new ObjectParameter("RECEIVER_ADDR1", typeof(string));
    
            var rECEIVER_ADDR2Parameter = rECEIVER_ADDR2 != null ?
                new ObjectParameter("RECEIVER_ADDR2", rECEIVER_ADDR2) :
                new ObjectParameter("RECEIVER_ADDR2", typeof(string));
    
            var rECEIVER_TEL1Parameter = rECEIVER_TEL1 != null ?
                new ObjectParameter("RECEIVER_TEL1", rECEIVER_TEL1) :
                new ObjectParameter("RECEIVER_TEL1", typeof(string));
    
            var rECEIVER_TEL2Parameter = rECEIVER_TEL2 != null ?
                new ObjectParameter("RECEIVER_TEL2", rECEIVER_TEL2) :
                new ObjectParameter("RECEIVER_TEL2", typeof(string));
    
            var rECEIVER_TEL3Parameter = rECEIVER_TEL3 != null ?
                new ObjectParameter("RECEIVER_TEL3", rECEIVER_TEL3) :
                new ObjectParameter("RECEIVER_TEL3", typeof(string));
    
            var rECEIVER_HP1Parameter = rECEIVER_HP1 != null ?
                new ObjectParameter("RECEIVER_HP1", rECEIVER_HP1) :
                new ObjectParameter("RECEIVER_HP1", typeof(string));
    
            var rECEIVER_HP2Parameter = rECEIVER_HP2 != null ?
                new ObjectParameter("RECEIVER_HP2", rECEIVER_HP2) :
                new ObjectParameter("RECEIVER_HP2", typeof(string));
    
            var rECEIVER_HP3Parameter = rECEIVER_HP3 != null ?
                new ObjectParameter("RECEIVER_HP3", rECEIVER_HP3) :
                new ObjectParameter("RECEIVER_HP3", typeof(string));
    
            var oRDER_MEMOParameter = oRDER_MEMO != null ?
                new ObjectParameter("ORDER_MEMO", oRDER_MEMO) :
                new ObjectParameter("ORDER_MEMO", typeof(string));
    
            var nOMEMBER_PASSParameter = nOMEMBER_PASS != null ?
                new ObjectParameter("NOMEMBER_PASS", nOMEMBER_PASS) :
                new ObjectParameter("NOMEMBER_PASS", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ORDER_STEP2_SENDER_RECEIVER_SAVE", oRDER_IDXParameter, sENDER_NAMEParameter, sENDER_POST1Parameter, sENDER_POST2Parameter, sENDER_ADDR1Parameter, sENDER_ADDR2Parameter, sENDER_TEL1Parameter, sENDER_TEL2Parameter, sENDER_TEL3Parameter, sENDER_HP1Parameter, sENDER_HP2Parameter, sENDER_HP3Parameter, sENDER_EMAIL1Parameter, sENDER_EMAIL2Parameter, rECEIVER_NAMEParameter, rECEIVER_POST1Parameter, rECEIVER_POST2Parameter, rECEIVER_ADDR1Parameter, rECEIVER_ADDR2Parameter, rECEIVER_TEL1Parameter, rECEIVER_TEL2Parameter, rECEIVER_TEL3Parameter, rECEIVER_HP1Parameter, rECEIVER_HP2Parameter, rECEIVER_HP3Parameter, oRDER_MEMOParameter, nOMEMBER_PASSParameter);
        }
    
        public virtual ObjectResult<SP_ORDER_RESULT_DETAIL_Result> SP_ORDER_RESULT_DETAIL(string oRDER_CODE, string m_ID, string sESSION_ID)
        {
            var oRDER_CODEParameter = oRDER_CODE != null ?
                new ObjectParameter("ORDER_CODE", oRDER_CODE) :
                new ObjectParameter("ORDER_CODE", typeof(string));
    
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            var sESSION_IDParameter = sESSION_ID != null ?
                new ObjectParameter("SESSION_ID", sESSION_ID) :
                new ObjectParameter("SESSION_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ORDER_RESULT_DETAIL_Result>("SP_ORDER_RESULT_DETAIL", oRDER_CODEParameter, m_IDParameter, sESSION_IDParameter);
        }
    
        public virtual ObjectResult<SP_ORDER_RESULT_PRODUCT_LIST_Result> SP_ORDER_RESULT_PRODUCT_LIST(string oRDER_CODE, string m_ID, string sESSION_ID)
        {
            var oRDER_CODEParameter = oRDER_CODE != null ?
                new ObjectParameter("ORDER_CODE", oRDER_CODE) :
                new ObjectParameter("ORDER_CODE", typeof(string));
    
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            var sESSION_IDParameter = sESSION_ID != null ?
                new ObjectParameter("SESSION_ID", sESSION_ID) :
                new ObjectParameter("SESSION_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ORDER_RESULT_PRODUCT_LIST_Result>("SP_ORDER_RESULT_PRODUCT_LIST", oRDER_CODEParameter, m_IDParameter, sESSION_IDParameter);
        }
    
        public virtual ObjectResult<SP_ORDER_NOMEMBER_LOGIN_Result> SP_ORDER_NOMEMBER_LOGIN(string oRDER_CODE, string nOMEMBER_PASS)
        {
            var oRDER_CODEParameter = oRDER_CODE != null ?
                new ObjectParameter("ORDER_CODE", oRDER_CODE) :
                new ObjectParameter("ORDER_CODE", typeof(string));
    
            var nOMEMBER_PASSParameter = nOMEMBER_PASS != null ?
                new ObjectParameter("NOMEMBER_PASS", nOMEMBER_PASS) :
                new ObjectParameter("NOMEMBER_PASS", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ORDER_NOMEMBER_LOGIN_Result>("SP_ORDER_NOMEMBER_LOGIN", oRDER_CODEParameter, nOMEMBER_PASSParameter);
        }
    
        public virtual ObjectResult<SP_MYPAGE_ORDERLIST_Result> SP_MYPAGE_ORDERLIST(string oRDER_CODE, string m_ID, string fROM_DATE, string tO_DATE, Nullable<int> pAGE, Nullable<int> pAGESIZE)
        {
            var oRDER_CODEParameter = oRDER_CODE != null ?
                new ObjectParameter("ORDER_CODE", oRDER_CODE) :
                new ObjectParameter("ORDER_CODE", typeof(string));
    
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            var fROM_DATEParameter = fROM_DATE != null ?
                new ObjectParameter("FROM_DATE", fROM_DATE) :
                new ObjectParameter("FROM_DATE", typeof(string));
    
            var tO_DATEParameter = tO_DATE != null ?
                new ObjectParameter("TO_DATE", tO_DATE) :
                new ObjectParameter("TO_DATE", typeof(string));
    
            var pAGEParameter = pAGE.HasValue ?
                new ObjectParameter("PAGE", pAGE) :
                new ObjectParameter("PAGE", typeof(int));
    
            var pAGESIZEParameter = pAGESIZE.HasValue ?
                new ObjectParameter("PAGESIZE", pAGESIZE) :
                new ObjectParameter("PAGESIZE", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_MYPAGE_ORDERLIST_Result>("SP_MYPAGE_ORDERLIST", oRDER_CODEParameter, m_IDParameter, fROM_DATEParameter, tO_DATEParameter, pAGEParameter, pAGESIZEParameter);
        }
    
        public virtual ObjectResult<SP_MYPAGE_ORDERLIST_CNT_Result> SP_MYPAGE_ORDERLIST_CNT(string oRDER_CODE, string m_ID, string fROM_DATE, string tO_DATE)
        {
            var oRDER_CODEParameter = oRDER_CODE != null ?
                new ObjectParameter("ORDER_CODE", oRDER_CODE) :
                new ObjectParameter("ORDER_CODE", typeof(string));
    
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            var fROM_DATEParameter = fROM_DATE != null ?
                new ObjectParameter("FROM_DATE", fROM_DATE) :
                new ObjectParameter("FROM_DATE", typeof(string));
    
            var tO_DATEParameter = tO_DATE != null ?
                new ObjectParameter("TO_DATE", tO_DATE) :
                new ObjectParameter("TO_DATE", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_MYPAGE_ORDERLIST_CNT_Result>("SP_MYPAGE_ORDERLIST_CNT", oRDER_CODEParameter, m_IDParameter, fROM_DATEParameter, tO_DATEParameter);
        }
    
        public virtual ObjectResult<SP_MYPAGE_ORDERLIST_DETAIL_INFO_Result> SP_MYPAGE_ORDERLIST_DETAIL_INFO(string oRDER_CODE, string m_ID)
        {
            var oRDER_CODEParameter = oRDER_CODE != null ?
                new ObjectParameter("ORDER_CODE", oRDER_CODE) :
                new ObjectParameter("ORDER_CODE", typeof(string));
    
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_MYPAGE_ORDERLIST_DETAIL_INFO_Result>("SP_MYPAGE_ORDERLIST_DETAIL_INFO", oRDER_CODEParameter, m_IDParameter);
        }
    
        public virtual ObjectResult<SP_MYPAGE_ORDERLIST_DETAIL_PRODUCT_LIST_Result> SP_MYPAGE_ORDERLIST_DETAIL_PRODUCT_LIST(string oRDER_CODE, string m_ID)
        {
            var oRDER_CODEParameter = oRDER_CODE != null ?
                new ObjectParameter("ORDER_CODE", oRDER_CODE) :
                new ObjectParameter("ORDER_CODE", typeof(string));
    
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_MYPAGE_ORDERLIST_DETAIL_PRODUCT_LIST_Result>("SP_MYPAGE_ORDERLIST_DETAIL_PRODUCT_LIST", oRDER_CODEParameter, m_IDParameter);
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
    }
}
