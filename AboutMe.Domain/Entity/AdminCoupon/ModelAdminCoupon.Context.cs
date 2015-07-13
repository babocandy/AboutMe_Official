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
        public virtual DbSet<TB_COUPON_PRODUCT> TB_COUPON_PRODUCT { get; set; }
        public virtual DbSet<TB_COUPON_PRICE_DETAIL> TB_COUPON_PRICE_DETAIL { get; set; }
        public virtual DbSet<TB_COUPON_ISSUED_DETAIL> TB_COUPON_ISSUED_DETAIL { get; set; }
        public virtual DbSet<TB_COUPON_MASTER> TB_COUPON_MASTER { get; set; }
    
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
    
        public virtual int SP_ADMIN_COUPON_MASTER_INS(string cOUPON_NAME, string cOUPON_AD_MSG, string cOUPON_USE_DESCRIPTION, string cOUPON_GBN, string cOUPON_GBN_M, string rATE_OR_MONEY, string sERVICE_LIFE_GBN, Nullable<System.DateTime> fIXED_PERIOD_FROM, Nullable<System.DateTime> fIXED_PERIOD_TO, Nullable<int> eXRIRED_DAY_FROM_ISSUE_DT, Nullable<System.DateTime> dOWNLOAD_DATE_FROM, Nullable<System.DateTime> dOWNLOAD_DATE_TO, string uSABLE_DEVICE_GBN, string pRODUCT_APP_SCOPE_GBN, string mEMBER_APP_SCOPE_GBN, string iSSUE_METHOD_GBN, string iSSUE_METHOD_WITH_AUTO, string cOUPON_NUM_CHECK_TF, Nullable<int> iSSUE_MAX_LIMIT, Nullable<System.DateTime> mASTER_FROM_DATE, Nullable<System.DateTime> mASTER_TO_DATE, string uSABLE_YN)
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
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_COUPON_MASTER_INS", cOUPON_NAMEParameter, cOUPON_AD_MSGParameter, cOUPON_USE_DESCRIPTIONParameter, cOUPON_GBNParameter, cOUPON_GBN_MParameter, rATE_OR_MONEYParameter, sERVICE_LIFE_GBNParameter, fIXED_PERIOD_FROMParameter, fIXED_PERIOD_TOParameter, eXRIRED_DAY_FROM_ISSUE_DTParameter, dOWNLOAD_DATE_FROMParameter, dOWNLOAD_DATE_TOParameter, uSABLE_DEVICE_GBNParameter, pRODUCT_APP_SCOPE_GBNParameter, mEMBER_APP_SCOPE_GBNParameter, iSSUE_METHOD_GBNParameter, iSSUE_METHOD_WITH_AUTOParameter, cOUPON_NUM_CHECK_TFParameter, iSSUE_MAX_LIMITParameter, mASTER_FROM_DATEParameter, mASTER_TO_DATEParameter, uSABLE_YNParameter);
        }
    }
}
