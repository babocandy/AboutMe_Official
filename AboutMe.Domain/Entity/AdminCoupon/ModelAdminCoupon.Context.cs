﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AboutMe.Domain.Entity.AdminCoupon
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class AdminCouponEntities : DbContext
    {
        public AdminCouponEntities()
            : base("name=AdminCouponEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TB_COUPON_KIND_CATEGORY> TB_COUPON_KIND_CATEGORY { get; set; }
        public virtual DbSet<TB_COUPON_MEMBER_GRADE> TB_COUPON_MEMBER_GRADE { get; set; }
        public virtual DbSet<TB_COUPON_MASTER> TB_COUPON_MASTER { get; set; }
        public virtual DbSet<TB_COUPON_PRODUCT> TB_COUPON_PRODUCT { get; set; }
        public virtual DbSet<TB_COUPON_ISSUED_DETAIL> TB_COUPON_ISSUED_DETAIL { get; set; }
    
        public virtual ObjectResult<SP_ADMIN_COUPON_COMMON_CNT_Result> SP_ADMIN_COUPON_COMMON_CNT()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_COUPON_COMMON_CNT_Result>("SP_ADMIN_COUPON_COMMON_CNT");
        }
    
        public virtual ObjectResult<SP_ADMIN_COUPON_COMMON_CNT_Result> SP_ADMIN_COUPON_MASTER_CNT(string sEARCH_KEY, string sEARCH_KEYWORD)
        {
            var sEARCH_KEYParameter = sEARCH_KEY != null ?
                new ObjectParameter("SEARCH_KEY", sEARCH_KEY) :
                new ObjectParameter("SEARCH_KEY", typeof(string));
    
            var sEARCH_KEYWORDParameter = sEARCH_KEYWORD != null ?
                new ObjectParameter("SEARCH_KEYWORD", sEARCH_KEYWORD) :
                new ObjectParameter("SEARCH_KEYWORD", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_COUPON_COMMON_CNT_Result>("SP_ADMIN_COUPON_MASTER_CNT", sEARCH_KEYParameter, sEARCH_KEYWORDParameter);
        }
    
        public virtual ObjectResult<SP_ADMIN_COUPON_MASTER_DETAIL_SEL_Result> SP_ADMIN_COUPON_MASTER_DETAIL_SEL(string cD_COUPON)
        {
            var cD_COUPONParameter = cD_COUPON != null ?
                new ObjectParameter("CD_COUPON", cD_COUPON) :
                new ObjectParameter("CD_COUPON", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_COUPON_MASTER_DETAIL_SEL_Result>("SP_ADMIN_COUPON_MASTER_DETAIL_SEL", cD_COUPONParameter);
        }
    
        public virtual ObjectResult<SP_ADMIN_COUPON_MASTER_DETAIL_SEL_Result> SP_ADMIN_COUPON_MASTER_SEL(Nullable<int> pAGE, Nullable<int> pAGESIZE, string sEARCH_KEY, string sEARCH_KEYWORD)
        {
            var pAGEParameter = pAGE.HasValue ?
                new ObjectParameter("PAGE", pAGE) :
                new ObjectParameter("PAGE", typeof(int));
    
            var pAGESIZEParameter = pAGESIZE.HasValue ?
                new ObjectParameter("PAGESIZE", pAGESIZE) :
                new ObjectParameter("PAGESIZE", typeof(int));
    
            var sEARCH_KEYParameter = sEARCH_KEY != null ?
                new ObjectParameter("SEARCH_KEY", sEARCH_KEY) :
                new ObjectParameter("SEARCH_KEY", typeof(string));
    
            var sEARCH_KEYWORDParameter = sEARCH_KEYWORD != null ?
                new ObjectParameter("SEARCH_KEYWORD", sEARCH_KEYWORD) :
                new ObjectParameter("SEARCH_KEYWORD", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_COUPON_MASTER_DETAIL_SEL_Result>("SP_ADMIN_COUPON_MASTER_SEL", pAGEParameter, pAGESIZEParameter, sEARCH_KEYParameter, sEARCH_KEYWORDParameter);
        }
    
        public virtual ObjectResult<SP_ADMIN_COUPON_PRODUCT_DEATIL_SEL_Result> SP_ADMIN_COUPON_PRODUCT_DEATIL_SEL(string cD_COUPON, string p_CODE)
        {
            var cD_COUPONParameter = cD_COUPON != null ?
                new ObjectParameter("CD_COUPON", cD_COUPON) :
                new ObjectParameter("CD_COUPON", typeof(string));
    
            var p_CODEParameter = p_CODE != null ?
                new ObjectParameter("P_CODE", p_CODE) :
                new ObjectParameter("P_CODE", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_COUPON_PRODUCT_DEATIL_SEL_Result>("SP_ADMIN_COUPON_PRODUCT_DEATIL_SEL", cD_COUPONParameter, p_CODEParameter);
        }
    
        public virtual ObjectResult<SP_ADMIN_COUPON_COMMON_CNT_Result> SP_ADMIN_COUPON_PRODUCT_CNT(string sEARCH_KEY, string sEARCH_KEYWORD, string cD_COUPON)
        {
            var sEARCH_KEYParameter = sEARCH_KEY != null ?
                new ObjectParameter("SEARCH_KEY", sEARCH_KEY) :
                new ObjectParameter("SEARCH_KEY", typeof(string));
    
            var sEARCH_KEYWORDParameter = sEARCH_KEYWORD != null ?
                new ObjectParameter("SEARCH_KEYWORD", sEARCH_KEYWORD) :
                new ObjectParameter("SEARCH_KEYWORD", typeof(string));
    
            var cD_COUPONParameter = cD_COUPON != null ?
                new ObjectParameter("CD_COUPON", cD_COUPON) :
                new ObjectParameter("CD_COUPON", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_COUPON_COMMON_CNT_Result>("SP_ADMIN_COUPON_PRODUCT_CNT", sEARCH_KEYParameter, sEARCH_KEYWORDParameter, cD_COUPONParameter);
        }
    
        public virtual ObjectResult<SP_ADMIN_COUPON_PRODUCT_DEATIL_SEL_Result> SP_ADMIN_COUPON_PRODUCT_SEL(Nullable<int> pAGE, Nullable<int> pAGESIZE, string sEARCH_KEY, string sEARCH_KEYWORD, string cD_COUPON)
        {
            var pAGEParameter = pAGE.HasValue ?
                new ObjectParameter("PAGE", pAGE) :
                new ObjectParameter("PAGE", typeof(int));
    
            var pAGESIZEParameter = pAGESIZE.HasValue ?
                new ObjectParameter("PAGESIZE", pAGESIZE) :
                new ObjectParameter("PAGESIZE", typeof(int));
    
            var sEARCH_KEYParameter = sEARCH_KEY != null ?
                new ObjectParameter("SEARCH_KEY", sEARCH_KEY) :
                new ObjectParameter("SEARCH_KEY", typeof(string));
    
            var sEARCH_KEYWORDParameter = sEARCH_KEYWORD != null ?
                new ObjectParameter("SEARCH_KEYWORD", sEARCH_KEYWORD) :
                new ObjectParameter("SEARCH_KEYWORD", typeof(string));
    
            var cD_COUPONParameter = cD_COUPON != null ?
                new ObjectParameter("CD_COUPON", cD_COUPON) :
                new ObjectParameter("CD_COUPON", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_COUPON_PRODUCT_DEATIL_SEL_Result>("SP_ADMIN_COUPON_PRODUCT_SEL", pAGEParameter, pAGESIZEParameter, sEARCH_KEYParameter, sEARCH_KEYWORDParameter, cD_COUPONParameter);
        }
    
        public virtual ObjectResult<SP_ADMIN_COUPON_PRODUCT_FOR_CREATE_DETAIL_SEL_Result> SP_ADMIN_COUPON_PRODUCT_FOR_CREATE_DETAIL_SEL(string cD_COUPON)
        {
            var cD_COUPONParameter = cD_COUPON != null ?
                new ObjectParameter("CD_COUPON", cD_COUPON) :
                new ObjectParameter("CD_COUPON", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_COUPON_PRODUCT_FOR_CREATE_DETAIL_SEL_Result>("SP_ADMIN_COUPON_PRODUCT_FOR_CREATE_DETAIL_SEL", cD_COUPONParameter);
        }
    
        public virtual ObjectResult<SP_ADMIN_COUPON_PRODUCT_FOR_CREATE_DETAIL_SEL_Result> SP_ADMIN_COUPON_PRODUCT_FOR_CREATE_SEL(Nullable<int> pAGE, Nullable<int> pAGESIZE, string sEARCH_KEY, string sEARCH_KEYWORD, string cD_COUPON)
        {
            var pAGEParameter = pAGE.HasValue ?
                new ObjectParameter("PAGE", pAGE) :
                new ObjectParameter("PAGE", typeof(int));
    
            var pAGESIZEParameter = pAGESIZE.HasValue ?
                new ObjectParameter("PAGESIZE", pAGESIZE) :
                new ObjectParameter("PAGESIZE", typeof(int));
    
            var sEARCH_KEYParameter = sEARCH_KEY != null ?
                new ObjectParameter("SEARCH_KEY", sEARCH_KEY) :
                new ObjectParameter("SEARCH_KEY", typeof(string));
    
            var sEARCH_KEYWORDParameter = sEARCH_KEYWORD != null ?
                new ObjectParameter("SEARCH_KEYWORD", sEARCH_KEYWORD) :
                new ObjectParameter("SEARCH_KEYWORD", typeof(string));
    
            var cD_COUPONParameter = cD_COUPON != null ?
                new ObjectParameter("CD_COUPON", cD_COUPON) :
                new ObjectParameter("CD_COUPON", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_COUPON_PRODUCT_FOR_CREATE_DETAIL_SEL_Result>("SP_ADMIN_COUPON_PRODUCT_FOR_CREATE_SEL", pAGEParameter, pAGESIZEParameter, sEARCH_KEYParameter, sEARCH_KEYWORDParameter, cD_COUPONParameter);
        }
    
        public virtual ObjectResult<SP_ADMIN_COUPON_COMMON_CNT_Result> SP_ADMIN_COUPON_PRODUCT_FOR_CREATE_CNT(string sEARCH_KEY, string sEARCH_KEYWORD, string cD_COUPON)
        {
            var sEARCH_KEYParameter = sEARCH_KEY != null ?
                new ObjectParameter("SEARCH_KEY", sEARCH_KEY) :
                new ObjectParameter("SEARCH_KEY", typeof(string));
    
            var sEARCH_KEYWORDParameter = sEARCH_KEYWORD != null ?
                new ObjectParameter("SEARCH_KEYWORD", sEARCH_KEYWORD) :
                new ObjectParameter("SEARCH_KEYWORD", typeof(string));
    
            var cD_COUPONParameter = cD_COUPON != null ?
                new ObjectParameter("CD_COUPON", cD_COUPON) :
                new ObjectParameter("CD_COUPON", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_COUPON_COMMON_CNT_Result>("SP_ADMIN_COUPON_PRODUCT_FOR_CREATE_CNT", sEARCH_KEYParameter, sEARCH_KEYWORDParameter, cD_COUPONParameter);
        }
    
        public virtual int SP_ADMIN_COUPON_PRODUCT_CREATE_INS(string cD_COUPON, string p_CODE)
        {
            var cD_COUPONParameter = cD_COUPON != null ?
                new ObjectParameter("CD_COUPON", cD_COUPON) :
                new ObjectParameter("CD_COUPON", typeof(string));
    
            var p_CODEParameter = p_CODE != null ?
                new ObjectParameter("P_CODE", p_CODE) :
                new ObjectParameter("P_CODE", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_COUPON_PRODUCT_CREATE_INS", cD_COUPONParameter, p_CODEParameter);
        }
    
        public virtual ObjectResult<SP_ADMIN_COUPON_COMMON_CNT_Result> SP_ADMIN_COUPON_ISSUED_CNT(string sEARCH_KEY, string sEARCH_KEYWORD, string cD_COUPON)
        {
            var sEARCH_KEYParameter = sEARCH_KEY != null ?
                new ObjectParameter("SEARCH_KEY", sEARCH_KEY) :
                new ObjectParameter("SEARCH_KEY", typeof(string));
    
            var sEARCH_KEYWORDParameter = sEARCH_KEYWORD != null ?
                new ObjectParameter("SEARCH_KEYWORD", sEARCH_KEYWORD) :
                new ObjectParameter("SEARCH_KEYWORD", typeof(string));
    
            var cD_COUPONParameter = cD_COUPON != null ?
                new ObjectParameter("CD_COUPON", cD_COUPON) :
                new ObjectParameter("CD_COUPON", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_COUPON_COMMON_CNT_Result>("SP_ADMIN_COUPON_ISSUED_CNT", sEARCH_KEYParameter, sEARCH_KEYWORDParameter, cD_COUPONParameter);
        }
    
        public virtual ObjectResult<SP_ADMIN_COUPON_MEMBER_GRADE_SEL_Result> SP_ADMIN_COUPON_MEMBER_GRADE_SEL(string cD_COUPON)
        {
            var cD_COUPONParameter = cD_COUPON != null ?
                new ObjectParameter("CD_COUPON", cD_COUPON) :
                new ObjectParameter("CD_COUPON", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_COUPON_MEMBER_GRADE_SEL_Result>("SP_ADMIN_COUPON_MEMBER_GRADE_SEL", cD_COUPONParameter);
        }
    
        public virtual ObjectResult<SP_ADMIN_COUPON_COMMON_CNT_Result> SP_ADMIN_COUPON_PRODUCT_USABLE_CNT_SEL(string cD_COUPON)
        {
            var cD_COUPONParameter = cD_COUPON != null ?
                new ObjectParameter("CD_COUPON", cD_COUPON) :
                new ObjectParameter("CD_COUPON", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_COUPON_COMMON_CNT_Result>("SP_ADMIN_COUPON_PRODUCT_USABLE_CNT_SEL", cD_COUPONParameter);
        }
    
        public virtual int SP_ADMIN_COUPON_ISSUE_WITH_NO_NUMCHECK_MANUAL_ENTIRE_INS(string cD_COUPON, string aDMIN_ID, ObjectParameter eXCUTE_RESULT)
        {
            var cD_COUPONParameter = cD_COUPON != null ?
                new ObjectParameter("CD_COUPON", cD_COUPON) :
                new ObjectParameter("CD_COUPON", typeof(string));
    
            var aDMIN_IDParameter = aDMIN_ID != null ?
                new ObjectParameter("ADMIN_ID", aDMIN_ID) :
                new ObjectParameter("ADMIN_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_COUPON_ISSUE_WITH_NO_NUMCHECK_MANUAL_ENTIRE_INS", cD_COUPONParameter, aDMIN_IDParameter, eXCUTE_RESULT);
        }
    
        public virtual ObjectResult<SP_ADMIN_COUPON_ISSUED_DETAIL_SEL_Result> SP_ADMIN_COUPON_ISSUED_DETAIL_SEL(Nullable<int> iDX_COUPON_NUMBER)
        {
            var iDX_COUPON_NUMBERParameter = iDX_COUPON_NUMBER.HasValue ?
                new ObjectParameter("IDX_COUPON_NUMBER", iDX_COUPON_NUMBER) :
                new ObjectParameter("IDX_COUPON_NUMBER", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_COUPON_ISSUED_DETAIL_SEL_Result>("SP_ADMIN_COUPON_ISSUED_DETAIL_SEL", iDX_COUPON_NUMBERParameter);
        }
    
        public virtual ObjectResult<SP_ADMIN_COUPON_ISSUED_DETAIL_SEL_Result> SP_ADMIN_COUPON_ISSUED_LIST_SEL(Nullable<int> pAGE, Nullable<int> pAGESIZE, string sEARCH_KEY, string sEARCH_KEYWORD, string cD_COUPON)
        {
            var pAGEParameter = pAGE.HasValue ?
                new ObjectParameter("PAGE", pAGE) :
                new ObjectParameter("PAGE", typeof(int));
    
            var pAGESIZEParameter = pAGESIZE.HasValue ?
                new ObjectParameter("PAGESIZE", pAGESIZE) :
                new ObjectParameter("PAGESIZE", typeof(int));
    
            var sEARCH_KEYParameter = sEARCH_KEY != null ?
                new ObjectParameter("SEARCH_KEY", sEARCH_KEY) :
                new ObjectParameter("SEARCH_KEY", typeof(string));
    
            var sEARCH_KEYWORDParameter = sEARCH_KEYWORD != null ?
                new ObjectParameter("SEARCH_KEYWORD", sEARCH_KEYWORD) :
                new ObjectParameter("SEARCH_KEYWORD", typeof(string));
    
            var cD_COUPONParameter = cD_COUPON != null ?
                new ObjectParameter("CD_COUPON", cD_COUPON) :
                new ObjectParameter("CD_COUPON", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_COUPON_ISSUED_DETAIL_SEL_Result>("SP_ADMIN_COUPON_ISSUED_LIST_SEL", pAGEParameter, pAGESIZEParameter, sEARCH_KEYParameter, sEARCH_KEYWORDParameter, cD_COUPONParameter);
        }
    
        public virtual int SP_COUPON_DOWNLOAD_AT_PC_VERSION_UPDATE(string m_ID, Nullable<int> iDX_COUPON_NUMBER, string mETHOD)
        {
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            var iDX_COUPON_NUMBERParameter = iDX_COUPON_NUMBER.HasValue ?
                new ObjectParameter("IDX_COUPON_NUMBER", iDX_COUPON_NUMBER) :
                new ObjectParameter("IDX_COUPON_NUMBER", typeof(int));
    
            var mETHODParameter = mETHOD != null ?
                new ObjectParameter("METHOD", mETHOD) :
                new ObjectParameter("METHOD", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_COUPON_DOWNLOAD_AT_PC_VERSION_UPDATE", m_IDParameter, iDX_COUPON_NUMBERParameter, mETHODParameter);
        }
    
        public virtual ObjectResult<SP_COUPON_COMMON_CNT_Result> SP_COUPON_AVAILABLE_LIST_CNT(string m_ID, string sEARCH_KEY, string sEARCH_KEYWORD)
        {
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            var sEARCH_KEYParameter = sEARCH_KEY != null ?
                new ObjectParameter("SEARCH_KEY", sEARCH_KEY) :
                new ObjectParameter("SEARCH_KEY", typeof(string));
    
            var sEARCH_KEYWORDParameter = sEARCH_KEYWORD != null ?
                new ObjectParameter("SEARCH_KEYWORD", sEARCH_KEYWORD) :
                new ObjectParameter("SEARCH_KEYWORD", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_COUPON_COMMON_CNT_Result>("SP_COUPON_AVAILABLE_LIST_CNT", m_IDParameter, sEARCH_KEYParameter, sEARCH_KEYWORDParameter);
        }
    
        public virtual ObjectResult<SP_COUPON_COMMON_CNT_Result> SP_COUPON_COMMON_CNT()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_COUPON_COMMON_CNT_Result>("SP_COUPON_COMMON_CNT");
        }
    
        public virtual ObjectResult<SP_COUPON_ISSUED_DETAIL_SEL_Result> SP_COUPON_ISSUED_DETAIL_SEL(Nullable<int> iDX_COUPON_NUMBER)
        {
            var iDX_COUPON_NUMBERParameter = iDX_COUPON_NUMBER.HasValue ?
                new ObjectParameter("IDX_COUPON_NUMBER", iDX_COUPON_NUMBER) :
                new ObjectParameter("IDX_COUPON_NUMBER", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_COUPON_ISSUED_DETAIL_SEL_Result>("SP_COUPON_ISSUED_DETAIL_SEL", iDX_COUPON_NUMBERParameter);
        }
    
        public virtual ObjectResult<SP_COUPON_DOWNLOADABLE_LIST_Result> SP_COUPON_DOWNLOADABLE_LIST(string m_ID)
        {
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_COUPON_DOWNLOADABLE_LIST_Result>("SP_COUPON_DOWNLOADABLE_LIST", m_IDParameter);
        }
    
        public virtual ObjectResult<SP_COUPON_ISSUED_DETAIL_SEL_Result> SP_COUPON_AVAILABLE_LIST_SEL(string m_ID, Nullable<int> pAGE, Nullable<int> pAGESIZE, string sEARCH_KEY, string sEARCH_KEYWORD)
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
    
            var sEARCH_KEYParameter = sEARCH_KEY != null ?
                new ObjectParameter("SEARCH_KEY", sEARCH_KEY) :
                new ObjectParameter("SEARCH_KEY", typeof(string));
    
            var sEARCH_KEYWORDParameter = sEARCH_KEYWORD != null ?
                new ObjectParameter("SEARCH_KEYWORD", sEARCH_KEYWORD) :
                new ObjectParameter("SEARCH_KEYWORD", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_COUPON_ISSUED_DETAIL_SEL_Result>("SP_COUPON_AVAILABLE_LIST_SEL", m_IDParameter, pAGEParameter, pAGESIZEParameter, sEARCH_KEYParameter, sEARCH_KEYWORDParameter);
        }
    
        public virtual ObjectResult<SP_COUPON_VIEW_PRODUCT_DETAIL_SEL_Result> SP_COUPON_VIEW_PRODUCT_DETAIL_SEL(string m_ID, Nullable<int> iDX_COUPON_SERIAL)
        {
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            var iDX_COUPON_SERIALParameter = iDX_COUPON_SERIAL.HasValue ?
                new ObjectParameter("IDX_COUPON_SERIAL", iDX_COUPON_SERIAL) :
                new ObjectParameter("IDX_COUPON_SERIAL", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_COUPON_VIEW_PRODUCT_DETAIL_SEL_Result>("SP_COUPON_VIEW_PRODUCT_DETAIL_SEL", m_IDParameter, iDX_COUPON_SERIALParameter);
        }
    
        public virtual ObjectResult<SP_COUPON_VIEW_PRODUCT_DETAIL_SEL_Result> SP_COUPON_PRODUCT_LIST_SEL(string m_ID, Nullable<int> iDX_COUPON_SERIAL, Nullable<int> pAGE, Nullable<int> pAGESIZE, string sEARCH_KEY, string sEARCH_KEYWORD)
        {
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            var iDX_COUPON_SERIALParameter = iDX_COUPON_SERIAL.HasValue ?
                new ObjectParameter("IDX_COUPON_SERIAL", iDX_COUPON_SERIAL) :
                new ObjectParameter("IDX_COUPON_SERIAL", typeof(int));
    
            var pAGEParameter = pAGE.HasValue ?
                new ObjectParameter("PAGE", pAGE) :
                new ObjectParameter("PAGE", typeof(int));
    
            var pAGESIZEParameter = pAGESIZE.HasValue ?
                new ObjectParameter("PAGESIZE", pAGESIZE) :
                new ObjectParameter("PAGESIZE", typeof(int));
    
            var sEARCH_KEYParameter = sEARCH_KEY != null ?
                new ObjectParameter("SEARCH_KEY", sEARCH_KEY) :
                new ObjectParameter("SEARCH_KEY", typeof(string));
    
            var sEARCH_KEYWORDParameter = sEARCH_KEYWORD != null ?
                new ObjectParameter("SEARCH_KEYWORD", sEARCH_KEYWORD) :
                new ObjectParameter("SEARCH_KEYWORD", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_COUPON_VIEW_PRODUCT_DETAIL_SEL_Result>("SP_COUPON_PRODUCT_LIST_SEL", m_IDParameter, iDX_COUPON_SERIALParameter, pAGEParameter, pAGESIZEParameter, sEARCH_KEYParameter, sEARCH_KEYWORDParameter);
        }
    
        public virtual ObjectResult<SP_COUPON_COMMON_CNT_Result> SP_COUPON_PRODUCT_LIST_CNT(string m_ID, Nullable<int> iDX_COUPON_SERIAL, string sEARCH_KEY, string sEARCH_KEYWORD)
        {
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            var iDX_COUPON_SERIALParameter = iDX_COUPON_SERIAL.HasValue ?
                new ObjectParameter("IDX_COUPON_SERIAL", iDX_COUPON_SERIAL) :
                new ObjectParameter("IDX_COUPON_SERIAL", typeof(int));
    
            var sEARCH_KEYParameter = sEARCH_KEY != null ?
                new ObjectParameter("SEARCH_KEY", sEARCH_KEY) :
                new ObjectParameter("SEARCH_KEY", typeof(string));
    
            var sEARCH_KEYWORDParameter = sEARCH_KEYWORD != null ?
                new ObjectParameter("SEARCH_KEYWORD", sEARCH_KEYWORD) :
                new ObjectParameter("SEARCH_KEYWORD", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_COUPON_COMMON_CNT_Result>("SP_COUPON_PRODUCT_LIST_CNT", m_IDParameter, iDX_COUPON_SERIALParameter, sEARCH_KEYParameter, sEARCH_KEYWORDParameter);
        }
    
        public virtual ObjectResult<SP_COUPON_MASTER_INFO_SEL_Result> SP_COUPON_MASTER_INFO_SEL(string m_ID, Nullable<int> iDX_COUPON_SERIAL)
        {
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            var iDX_COUPON_SERIALParameter = iDX_COUPON_SERIAL.HasValue ?
                new ObjectParameter("IDX_COUPON_SERIAL", iDX_COUPON_SERIAL) :
                new ObjectParameter("IDX_COUPON_SERIAL", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_COUPON_MASTER_INFO_SEL_Result>("SP_COUPON_MASTER_INFO_SEL", m_IDParameter, iDX_COUPON_SERIALParameter);
        }
    
        public virtual int SP_ADMIN_COUPON_MASTER_INS(string cOUPON_NAME, string cOUPON_AD_MSG, string cOUPON_USE_DESCRIPTION, string cOUPON_GBN, string cOUPON_GBN_M, string rATE_OR_MONEY, string sERVICE_LIFE_GBN, Nullable<System.DateTime> fIXED_PERIOD_FROM, Nullable<System.DateTime> fIXED_PERIOD_TO, Nullable<int> eXRIRED_DAY_FROM_ISSUE_DT, Nullable<System.DateTime> dOWNLOAD_DATE_FROM, Nullable<System.DateTime> dOWNLOAD_DATE_TO, string uSABLE_DEVICE_GBN, string pRODUCT_APP_SCOPE_GBN, string mEMBER_APP_SCOPE_GBN, string iSSUE_METHOD_GBN, string iSSUE_METHOD_WITH_AUTO, string cOUPON_NUM_CHECK_TF, Nullable<int> iSSUE_MAX_LIMIT, Nullable<System.DateTime> mASTER_FROM_DATE, Nullable<System.DateTime> mASTER_TO_DATE, string uSABLE_YN, Nullable<int> cOUPON_DISCOUNT_MONEY, Nullable<int> cOUPON_DISCOUNT_RATE, ObjectParameter cREATED_CD_COUPON)
        {
            var cOUPON_NAMEParameter = cOUPON_NAME != null ?
                new ObjectParameter("COUPON_NAME", cOUPON_NAME) :
                new ObjectParameter("COUPON_NAME", typeof(string));
    
            var cOUPON_AD_MSGParameter = cOUPON_AD_MSG != null ?
                new ObjectParameter("COUPON_AD_MSG", cOUPON_AD_MSG) :
                new ObjectParameter("COUPON_AD_MSG", typeof(string));
    
            var cOUPON_USE_DESCRIPTIONParameter = cOUPON_USE_DESCRIPTION != null ?
                new ObjectParameter("COUPON_USE_DESCRIPTION", cOUPON_USE_DESCRIPTION) :
                new ObjectParameter("COUPON_USE_DESCRIPTION", typeof(string));
    
            var cOUPON_GBNParameter = cOUPON_GBN != null ?
                new ObjectParameter("COUPON_GBN", cOUPON_GBN) :
                new ObjectParameter("COUPON_GBN", typeof(string));
    
            var cOUPON_GBN_MParameter = cOUPON_GBN_M != null ?
                new ObjectParameter("COUPON_GBN_M", cOUPON_GBN_M) :
                new ObjectParameter("COUPON_GBN_M", typeof(string));
    
            var rATE_OR_MONEYParameter = rATE_OR_MONEY != null ?
                new ObjectParameter("RATE_OR_MONEY", rATE_OR_MONEY) :
                new ObjectParameter("RATE_OR_MONEY", typeof(string));
    
            var sERVICE_LIFE_GBNParameter = sERVICE_LIFE_GBN != null ?
                new ObjectParameter("SERVICE_LIFE_GBN", sERVICE_LIFE_GBN) :
                new ObjectParameter("SERVICE_LIFE_GBN", typeof(string));
    
            var fIXED_PERIOD_FROMParameter = fIXED_PERIOD_FROM.HasValue ?
                new ObjectParameter("FIXED_PERIOD_FROM", fIXED_PERIOD_FROM) :
                new ObjectParameter("FIXED_PERIOD_FROM", typeof(System.DateTime));
    
            var fIXED_PERIOD_TOParameter = fIXED_PERIOD_TO.HasValue ?
                new ObjectParameter("FIXED_PERIOD_TO", fIXED_PERIOD_TO) :
                new ObjectParameter("FIXED_PERIOD_TO", typeof(System.DateTime));
    
            var eXRIRED_DAY_FROM_ISSUE_DTParameter = eXRIRED_DAY_FROM_ISSUE_DT.HasValue ?
                new ObjectParameter("EXRIRED_DAY_FROM_ISSUE_DT", eXRIRED_DAY_FROM_ISSUE_DT) :
                new ObjectParameter("EXRIRED_DAY_FROM_ISSUE_DT", typeof(int));
    
            var dOWNLOAD_DATE_FROMParameter = dOWNLOAD_DATE_FROM.HasValue ?
                new ObjectParameter("DOWNLOAD_DATE_FROM", dOWNLOAD_DATE_FROM) :
                new ObjectParameter("DOWNLOAD_DATE_FROM", typeof(System.DateTime));
    
            var dOWNLOAD_DATE_TOParameter = dOWNLOAD_DATE_TO.HasValue ?
                new ObjectParameter("DOWNLOAD_DATE_TO", dOWNLOAD_DATE_TO) :
                new ObjectParameter("DOWNLOAD_DATE_TO", typeof(System.DateTime));
    
            var uSABLE_DEVICE_GBNParameter = uSABLE_DEVICE_GBN != null ?
                new ObjectParameter("USABLE_DEVICE_GBN", uSABLE_DEVICE_GBN) :
                new ObjectParameter("USABLE_DEVICE_GBN", typeof(string));
    
            var pRODUCT_APP_SCOPE_GBNParameter = pRODUCT_APP_SCOPE_GBN != null ?
                new ObjectParameter("PRODUCT_APP_SCOPE_GBN", pRODUCT_APP_SCOPE_GBN) :
                new ObjectParameter("PRODUCT_APP_SCOPE_GBN", typeof(string));
    
            var mEMBER_APP_SCOPE_GBNParameter = mEMBER_APP_SCOPE_GBN != null ?
                new ObjectParameter("MEMBER_APP_SCOPE_GBN", mEMBER_APP_SCOPE_GBN) :
                new ObjectParameter("MEMBER_APP_SCOPE_GBN", typeof(string));
    
            var iSSUE_METHOD_GBNParameter = iSSUE_METHOD_GBN != null ?
                new ObjectParameter("ISSUE_METHOD_GBN", iSSUE_METHOD_GBN) :
                new ObjectParameter("ISSUE_METHOD_GBN", typeof(string));
    
            var iSSUE_METHOD_WITH_AUTOParameter = iSSUE_METHOD_WITH_AUTO != null ?
                new ObjectParameter("ISSUE_METHOD_WITH_AUTO", iSSUE_METHOD_WITH_AUTO) :
                new ObjectParameter("ISSUE_METHOD_WITH_AUTO", typeof(string));
    
            var cOUPON_NUM_CHECK_TFParameter = cOUPON_NUM_CHECK_TF != null ?
                new ObjectParameter("COUPON_NUM_CHECK_TF", cOUPON_NUM_CHECK_TF) :
                new ObjectParameter("COUPON_NUM_CHECK_TF", typeof(string));
    
            var iSSUE_MAX_LIMITParameter = iSSUE_MAX_LIMIT.HasValue ?
                new ObjectParameter("ISSUE_MAX_LIMIT", iSSUE_MAX_LIMIT) :
                new ObjectParameter("ISSUE_MAX_LIMIT", typeof(int));
    
            var mASTER_FROM_DATEParameter = mASTER_FROM_DATE.HasValue ?
                new ObjectParameter("MASTER_FROM_DATE", mASTER_FROM_DATE) :
                new ObjectParameter("MASTER_FROM_DATE", typeof(System.DateTime));
    
            var mASTER_TO_DATEParameter = mASTER_TO_DATE.HasValue ?
                new ObjectParameter("MASTER_TO_DATE", mASTER_TO_DATE) :
                new ObjectParameter("MASTER_TO_DATE", typeof(System.DateTime));
    
            var uSABLE_YNParameter = uSABLE_YN != null ?
                new ObjectParameter("USABLE_YN", uSABLE_YN) :
                new ObjectParameter("USABLE_YN", typeof(string));
    
            var cOUPON_DISCOUNT_MONEYParameter = cOUPON_DISCOUNT_MONEY.HasValue ?
                new ObjectParameter("COUPON_DISCOUNT_MONEY", cOUPON_DISCOUNT_MONEY) :
                new ObjectParameter("COUPON_DISCOUNT_MONEY", typeof(int));
    
            var cOUPON_DISCOUNT_RATEParameter = cOUPON_DISCOUNT_RATE.HasValue ?
                new ObjectParameter("COUPON_DISCOUNT_RATE", cOUPON_DISCOUNT_RATE) :
                new ObjectParameter("COUPON_DISCOUNT_RATE", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_COUPON_MASTER_INS", cOUPON_NAMEParameter, cOUPON_AD_MSGParameter, cOUPON_USE_DESCRIPTIONParameter, cOUPON_GBNParameter, cOUPON_GBN_MParameter, rATE_OR_MONEYParameter, sERVICE_LIFE_GBNParameter, fIXED_PERIOD_FROMParameter, fIXED_PERIOD_TOParameter, eXRIRED_DAY_FROM_ISSUE_DTParameter, dOWNLOAD_DATE_FROMParameter, dOWNLOAD_DATE_TOParameter, uSABLE_DEVICE_GBNParameter, pRODUCT_APP_SCOPE_GBNParameter, mEMBER_APP_SCOPE_GBNParameter, iSSUE_METHOD_GBNParameter, iSSUE_METHOD_WITH_AUTOParameter, cOUPON_NUM_CHECK_TFParameter, iSSUE_MAX_LIMITParameter, mASTER_FROM_DATEParameter, mASTER_TO_DATEParameter, uSABLE_YNParameter, cOUPON_DISCOUNT_MONEYParameter, cOUPON_DISCOUNT_RATEParameter, cREATED_CD_COUPON);
        }
    
        public virtual int SP_ADMIN_COUPON_MEM_GRADE_INS(string cD_COUPON, string m_GBN, string m_GRADE)
        {
            var cD_COUPONParameter = cD_COUPON != null ?
                new ObjectParameter("CD_COUPON", cD_COUPON) :
                new ObjectParameter("CD_COUPON", typeof(string));
    
            var m_GBNParameter = m_GBN != null ?
                new ObjectParameter("M_GBN", m_GBN) :
                new ObjectParameter("M_GBN", typeof(string));
    
            var m_GRADEParameter = m_GRADE != null ?
                new ObjectParameter("M_GRADE", m_GRADE) :
                new ObjectParameter("M_GRADE", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_COUPON_MEM_GRADE_INS", cD_COUPONParameter, m_GBNParameter, m_GRADEParameter);
        }
    
        public virtual int SP_ADMIN_COUPON_PRODUCT_CREATE_ALL_INS(string cD_COUPON)
        {
            var cD_COUPONParameter = cD_COUPON != null ?
                new ObjectParameter("CD_COUPON", cD_COUPON) :
                new ObjectParameter("CD_COUPON", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_COUPON_PRODUCT_CREATE_ALL_INS", cD_COUPONParameter);
        }
    
        public virtual int SP_ADMIN_COUPON_ISSUE_WITH_NO_NUMCHECK_MANUAL_SINGLE_INS(string cD_COUPON, Nullable<int> cOUPON_MONEY, Nullable<int> cOUPON_DISCOUNT_RATE_V, string m_ID, string aDMIN_ID, string iSSUED_MEMO, ObjectParameter eXCUTE_RESULT)
        {
            var cD_COUPONParameter = cD_COUPON != null ?
                new ObjectParameter("CD_COUPON", cD_COUPON) :
                new ObjectParameter("CD_COUPON", typeof(string));
    
            var cOUPON_MONEYParameter = cOUPON_MONEY.HasValue ?
                new ObjectParameter("COUPON_MONEY", cOUPON_MONEY) :
                new ObjectParameter("COUPON_MONEY", typeof(int));
    
            var cOUPON_DISCOUNT_RATE_VParameter = cOUPON_DISCOUNT_RATE_V.HasValue ?
                new ObjectParameter("COUPON_DISCOUNT_RATE_V", cOUPON_DISCOUNT_RATE_V) :
                new ObjectParameter("COUPON_DISCOUNT_RATE_V", typeof(int));
    
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            var aDMIN_IDParameter = aDMIN_ID != null ?
                new ObjectParameter("ADMIN_ID", aDMIN_ID) :
                new ObjectParameter("ADMIN_ID", typeof(string));
    
            var iSSUED_MEMOParameter = iSSUED_MEMO != null ?
                new ObjectParameter("ISSUED_MEMO", iSSUED_MEMO) :
                new ObjectParameter("ISSUED_MEMO", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_COUPON_ISSUE_WITH_NO_NUMCHECK_MANUAL_SINGLE_INS", cD_COUPONParameter, cOUPON_MONEYParameter, cOUPON_DISCOUNT_RATE_VParameter, m_IDParameter, aDMIN_IDParameter, iSSUED_MEMOParameter, eXCUTE_RESULT);
        }
    
        public virtual ObjectResult<SP_COUPON_TOP1_BY_PRD_SEL_NO_LOGIN_SEL_Result> SP_COUPON_TOP1_BY_PRD_SEL_NO_LOGIN_SEL(string p_CODE, string uSABLE_DEVICE_GBN)
        {
            var p_CODEParameter = p_CODE != null ?
                new ObjectParameter("P_CODE", p_CODE) :
                new ObjectParameter("P_CODE", typeof(string));
    
            var uSABLE_DEVICE_GBNParameter = uSABLE_DEVICE_GBN != null ?
                new ObjectParameter("USABLE_DEVICE_GBN", uSABLE_DEVICE_GBN) :
                new ObjectParameter("USABLE_DEVICE_GBN", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_COUPON_TOP1_BY_PRD_SEL_NO_LOGIN_SEL_Result>("SP_COUPON_TOP1_BY_PRD_SEL_NO_LOGIN_SEL", p_CODEParameter, uSABLE_DEVICE_GBNParameter);
        }
    
        public virtual ObjectResult<SP_COUPON_TOP_1_BY_MEMBER_BY_PRD_SEL_Result> SP_COUPON_TOP_1_BY_MEMBER_BY_PRD_SEL(string uSABLE_DEVICE_GBN, string p_CODE, string m_ID)
        {
            var uSABLE_DEVICE_GBNParameter = uSABLE_DEVICE_GBN != null ?
                new ObjectParameter("USABLE_DEVICE_GBN", uSABLE_DEVICE_GBN) :
                new ObjectParameter("USABLE_DEVICE_GBN", typeof(string));
    
            var p_CODEParameter = p_CODE != null ?
                new ObjectParameter("P_CODE", p_CODE) :
                new ObjectParameter("P_CODE", typeof(string));
    
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_COUPON_TOP_1_BY_MEMBER_BY_PRD_SEL_Result>("SP_COUPON_TOP_1_BY_MEMBER_BY_PRD_SEL", uSABLE_DEVICE_GBNParameter, p_CODEParameter, m_IDParameter);
        }
    
        public virtual ObjectResult<SP_ADMIN_COUPON_COMMON_CNT_Result> SP_COUPON_DOWNLOADABLE_CNT(string m_ID)
        {
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_COUPON_COMMON_CNT_Result>("SP_COUPON_DOWNLOADABLE_CNT", m_IDParameter);
        }
    }
}
