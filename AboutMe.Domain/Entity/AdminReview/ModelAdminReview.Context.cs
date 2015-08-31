﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AboutMe.Domain.Entity.AdminReview
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class AdminReviewEntities : DbContext
    {
        public AdminReviewEntities()
            : base("name=AdminReviewEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
    
        public virtual ObjectResult<SP_ADMIN_REVIEW_PRODUCT_SEL_Result> SP_ADMIN_REVIEW_PRODUCT_SEL(Nullable<int> pAGE, Nullable<int> pAGESIZE, string sEARCH_KEY, string sEARCH_VALUE, string mEDIA_GBN_W, string mEDIA_GBN_M, Nullable<System.DateTime> sEL_DATE_FROM, Nullable<System.DateTime> sEL_DATE_TO, string iS_PHOTO_Y, string iS_PHOTO_N, string iS_DISPLAY_Y, string iS_DISPLAY_N, ObjectParameter tOTAL)
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
    
            var sEARCH_VALUEParameter = sEARCH_VALUE != null ?
                new ObjectParameter("SEARCH_VALUE", sEARCH_VALUE) :
                new ObjectParameter("SEARCH_VALUE", typeof(string));
    
            var mEDIA_GBN_WParameter = mEDIA_GBN_W != null ?
                new ObjectParameter("MEDIA_GBN_W", mEDIA_GBN_W) :
                new ObjectParameter("MEDIA_GBN_W", typeof(string));
    
            var mEDIA_GBN_MParameter = mEDIA_GBN_M != null ?
                new ObjectParameter("MEDIA_GBN_M", mEDIA_GBN_M) :
                new ObjectParameter("MEDIA_GBN_M", typeof(string));
    
            var sEL_DATE_FROMParameter = sEL_DATE_FROM.HasValue ?
                new ObjectParameter("SEL_DATE_FROM", sEL_DATE_FROM) :
                new ObjectParameter("SEL_DATE_FROM", typeof(System.DateTime));
    
            var sEL_DATE_TOParameter = sEL_DATE_TO.HasValue ?
                new ObjectParameter("SEL_DATE_TO", sEL_DATE_TO) :
                new ObjectParameter("SEL_DATE_TO", typeof(System.DateTime));
    
            var iS_PHOTO_YParameter = iS_PHOTO_Y != null ?
                new ObjectParameter("IS_PHOTO_Y", iS_PHOTO_Y) :
                new ObjectParameter("IS_PHOTO_Y", typeof(string));
    
            var iS_PHOTO_NParameter = iS_PHOTO_N != null ?
                new ObjectParameter("IS_PHOTO_N", iS_PHOTO_N) :
                new ObjectParameter("IS_PHOTO_N", typeof(string));
    
            var iS_DISPLAY_YParameter = iS_DISPLAY_Y != null ?
                new ObjectParameter("IS_DISPLAY_Y", iS_DISPLAY_Y) :
                new ObjectParameter("IS_DISPLAY_Y", typeof(string));
    
            var iS_DISPLAY_NParameter = iS_DISPLAY_N != null ?
                new ObjectParameter("IS_DISPLAY_N", iS_DISPLAY_N) :
                new ObjectParameter("IS_DISPLAY_N", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_REVIEW_PRODUCT_SEL_Result>("SP_ADMIN_REVIEW_PRODUCT_SEL", pAGEParameter, pAGESIZEParameter, sEARCH_KEYParameter, sEARCH_VALUEParameter, mEDIA_GBN_WParameter, mEDIA_GBN_MParameter, sEL_DATE_FROMParameter, sEL_DATE_TOParameter, iS_PHOTO_YParameter, iS_PHOTO_NParameter, iS_DISPLAY_YParameter, iS_DISPLAY_NParameter, tOTAL);
        }
    
        public virtual ObjectResult<SP_ADMIN_REVIEW_PRODUCT_INFO_Result> SP_ADMIN_REVIEW_PRODUCT_INFO(Nullable<int> iDX, ObjectParameter rET_NUM, ObjectParameter rET_MESSAGE)
        {
            var iDXParameter = iDX.HasValue ?
                new ObjectParameter("IDX", iDX) :
                new ObjectParameter("IDX", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_REVIEW_PRODUCT_INFO_Result>("SP_ADMIN_REVIEW_PRODUCT_INFO", iDXParameter, rET_NUM, rET_MESSAGE);
        }
    
        public virtual int SP_ADMIN_REVIEW_PRODUCT_UPDATE(Nullable<int> iDX, string iS_BEST, string iS_DISPLAY, string iS_MOST_CNT, ObjectParameter rET_NUM, ObjectParameter rET_MESSAGE)
        {
            var iDXParameter = iDX.HasValue ?
                new ObjectParameter("IDX", iDX) :
                new ObjectParameter("IDX", typeof(int));
    
            var iS_BESTParameter = iS_BEST != null ?
                new ObjectParameter("IS_BEST", iS_BEST) :
                new ObjectParameter("IS_BEST", typeof(string));
    
            var iS_DISPLAYParameter = iS_DISPLAY != null ?
                new ObjectParameter("IS_DISPLAY", iS_DISPLAY) :
                new ObjectParameter("IS_DISPLAY", typeof(string));
    
            var iS_MOST_CNTParameter = iS_MOST_CNT != null ?
                new ObjectParameter("IS_MOST_CNT", iS_MOST_CNT) :
                new ObjectParameter("IS_MOST_CNT", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_REVIEW_PRODUCT_UPDATE", iDXParameter, iS_BESTParameter, iS_DISPLAYParameter, iS_MOST_CNTParameter, rET_NUM, rET_MESSAGE);
        }
    
        public virtual ObjectResult<SP_ADMIN_REVIEW_CATE_THEMA_SEL_Result> SP_ADMIN_REVIEW_CATE_THEMA_SEL()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_REVIEW_CATE_THEMA_SEL_Result>("SP_ADMIN_REVIEW_CATE_THEMA_SEL");
        }
    
        public virtual int SP_ADMIN_REVIEW_CATE_THEMA_UPD(Nullable<int> iDX, string tITLE, string iS_DISPLAY, string tAG)
        {
            var iDXParameter = iDX.HasValue ?
                new ObjectParameter("IDX", iDX) :
                new ObjectParameter("IDX", typeof(int));
    
            var tITLEParameter = tITLE != null ?
                new ObjectParameter("TITLE", tITLE) :
                new ObjectParameter("TITLE", typeof(string));
    
            var iS_DISPLAYParameter = iS_DISPLAY != null ?
                new ObjectParameter("IS_DISPLAY", iS_DISPLAY) :
                new ObjectParameter("IS_DISPLAY", typeof(string));
    
            var tAGParameter = tAG != null ?
                new ObjectParameter("TAG", tAG) :
                new ObjectParameter("TAG", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_REVIEW_CATE_THEMA_UPD", iDXParameter, tITLEParameter, iS_DISPLAYParameter, tAGParameter);
        }
    
        public virtual int SP_ADMIN_REVIEW_EXP_MASTER_INS(string p_CODE, string fROM_DATE, string tO_DATE, string iS_AUTH, ObjectParameter mASTER_IDX)
        {
            var p_CODEParameter = p_CODE != null ?
                new ObjectParameter("P_CODE", p_CODE) :
                new ObjectParameter("P_CODE", typeof(string));
    
            var fROM_DATEParameter = fROM_DATE != null ?
                new ObjectParameter("FROM_DATE", fROM_DATE) :
                new ObjectParameter("FROM_DATE", typeof(string));
    
            var tO_DATEParameter = tO_DATE != null ?
                new ObjectParameter("TO_DATE", tO_DATE) :
                new ObjectParameter("TO_DATE", typeof(string));
    
            var iS_AUTHParameter = iS_AUTH != null ?
                new ObjectParameter("IS_AUTH", iS_AUTH) :
                new ObjectParameter("IS_AUTH", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_REVIEW_EXP_MASTER_INS", p_CODEParameter, fROM_DATEParameter, tO_DATEParameter, iS_AUTHParameter, mASTER_IDX);
        }
    
        public virtual int SP_ADMIN_REVIEW_EXP_MEMBER_INS(Nullable<int> eXP_MASTER_IDX, string m_ID, string m_NAME, ObjectParameter rET_NUM, ObjectParameter rET_MESSAGE)
        {
            var eXP_MASTER_IDXParameter = eXP_MASTER_IDX.HasValue ?
                new ObjectParameter("EXP_MASTER_IDX", eXP_MASTER_IDX) :
                new ObjectParameter("EXP_MASTER_IDX", typeof(int));
    
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            var m_NAMEParameter = m_NAME != null ?
                new ObjectParameter("M_NAME", m_NAME) :
                new ObjectParameter("M_NAME", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_REVIEW_EXP_MEMBER_INS", eXP_MASTER_IDXParameter, m_IDParameter, m_NAMEParameter, rET_NUM, rET_MESSAGE);
        }
    
        public virtual ObjectResult<SP_ADMIN_REVIEW_EXP_MASTER_SEL_Result> SP_ADMIN_REVIEW_EXP_MASTER_SEL(Nullable<int> pAGE, Nullable<int> pAGESIZE, string sEARCH_KEY, string sEARCH_VALUE, string fROM_DATE, string tO_DATE, string iS_STATE_10, string iS_STATE_20, string iS_STATE_30, string iS_AUTH_Y, string iS_AUTH_N, ObjectParameter tOTAL)
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
    
            var sEARCH_VALUEParameter = sEARCH_VALUE != null ?
                new ObjectParameter("SEARCH_VALUE", sEARCH_VALUE) :
                new ObjectParameter("SEARCH_VALUE", typeof(string));
    
            var fROM_DATEParameter = fROM_DATE != null ?
                new ObjectParameter("FROM_DATE", fROM_DATE) :
                new ObjectParameter("FROM_DATE", typeof(string));
    
            var tO_DATEParameter = tO_DATE != null ?
                new ObjectParameter("TO_DATE", tO_DATE) :
                new ObjectParameter("TO_DATE", typeof(string));
    
            var iS_STATE_10Parameter = iS_STATE_10 != null ?
                new ObjectParameter("IS_STATE_10", iS_STATE_10) :
                new ObjectParameter("IS_STATE_10", typeof(string));
    
            var iS_STATE_20Parameter = iS_STATE_20 != null ?
                new ObjectParameter("IS_STATE_20", iS_STATE_20) :
                new ObjectParameter("IS_STATE_20", typeof(string));
    
            var iS_STATE_30Parameter = iS_STATE_30 != null ?
                new ObjectParameter("IS_STATE_30", iS_STATE_30) :
                new ObjectParameter("IS_STATE_30", typeof(string));
    
            var iS_AUTH_YParameter = iS_AUTH_Y != null ?
                new ObjectParameter("IS_AUTH_Y", iS_AUTH_Y) :
                new ObjectParameter("IS_AUTH_Y", typeof(string));
    
            var iS_AUTH_NParameter = iS_AUTH_N != null ?
                new ObjectParameter("IS_AUTH_N", iS_AUTH_N) :
                new ObjectParameter("IS_AUTH_N", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_REVIEW_EXP_MASTER_SEL_Result>("SP_ADMIN_REVIEW_EXP_MASTER_SEL", pAGEParameter, pAGESIZEParameter, sEARCH_KEYParameter, sEARCH_VALUEParameter, fROM_DATEParameter, tO_DATEParameter, iS_STATE_10Parameter, iS_STATE_20Parameter, iS_STATE_30Parameter, iS_AUTH_YParameter, iS_AUTH_NParameter, tOTAL);
        }
    
        public virtual ObjectResult<SP_ADMIN_REVIEW_EXP_FIND_PRODUCT_SEL_Result> SP_ADMIN_REVIEW_EXP_FIND_PRODUCT_SEL(Nullable<int> pAGE, Nullable<int> pAGESIZE, string sEARCH_KEY, string sEARCH_VALUE, ObjectParameter tOTAL)
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
    
            var sEARCH_VALUEParameter = sEARCH_VALUE != null ?
                new ObjectParameter("SEARCH_VALUE", sEARCH_VALUE) :
                new ObjectParameter("SEARCH_VALUE", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_REVIEW_EXP_FIND_PRODUCT_SEL_Result>("SP_ADMIN_REVIEW_EXP_FIND_PRODUCT_SEL", pAGEParameter, pAGESIZEParameter, sEARCH_KEYParameter, sEARCH_VALUEParameter, tOTAL);
        }
    
        public virtual ObjectResult<SP_ADMIN_REVIEW_EXP_MASTER_DETAIL_Result> SP_ADMIN_REVIEW_EXP_MASTER_DETAIL(Nullable<int> iDX)
        {
            var iDXParameter = iDX.HasValue ?
                new ObjectParameter("IDX", iDX) :
                new ObjectParameter("IDX", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_REVIEW_EXP_MASTER_DETAIL_Result>("SP_ADMIN_REVIEW_EXP_MASTER_DETAIL", iDXParameter);
        }
    
        public virtual int SP_ADMIN_REVIEW_EXP_MEMBER_DEL(string m_ID, Nullable<int> eXP_MASTER_IDX, ObjectParameter rET_NUM, ObjectParameter rET_MESSAGE)
        {
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            var eXP_MASTER_IDXParameter = eXP_MASTER_IDX.HasValue ?
                new ObjectParameter("EXP_MASTER_IDX", eXP_MASTER_IDX) :
                new ObjectParameter("EXP_MASTER_IDX", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_REVIEW_EXP_MEMBER_DEL", m_IDParameter, eXP_MASTER_IDXParameter, rET_NUM, rET_MESSAGE);
        }
    
        public virtual ObjectResult<SP_ADMIN_REVIEW_EXP_MEMBER_SEL_Result> SP_ADMIN_REVIEW_EXP_MEMBER_SEL(Nullable<int> eXP_MASTER_IDX)
        {
            var eXP_MASTER_IDXParameter = eXP_MASTER_IDX.HasValue ?
                new ObjectParameter("EXP_MASTER_IDX", eXP_MASTER_IDX) :
                new ObjectParameter("EXP_MASTER_IDX", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_REVIEW_EXP_MEMBER_SEL_Result>("SP_ADMIN_REVIEW_EXP_MEMBER_SEL", eXP_MASTER_IDXParameter);
        }
    
        public virtual int SP_ADMIN_REVIEW_EXP_MASTER_UDATE(Nullable<int> iDX, string iS_AUTH)
        {
            var iDXParameter = iDX.HasValue ?
                new ObjectParameter("IDX", iDX) :
                new ObjectParameter("IDX", typeof(int));
    
            var iS_AUTHParameter = iS_AUTH != null ?
                new ObjectParameter("IS_AUTH", iS_AUTH) :
                new ObjectParameter("IS_AUTH", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_REVIEW_EXP_MASTER_UDATE", iDXParameter, iS_AUTHParameter);
        }
    
        public virtual ObjectResult<SP_ADMIN_REVIEW_EXP_ARTICLE_DETAIL_Result> SP_ADMIN_REVIEW_EXP_ARTICLE_DETAIL(Nullable<int> iDX)
        {
            var iDXParameter = iDX.HasValue ?
                new ObjectParameter("IDX", iDX) :
                new ObjectParameter("IDX", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_REVIEW_EXP_ARTICLE_DETAIL_Result>("SP_ADMIN_REVIEW_EXP_ARTICLE_DETAIL", iDXParameter);
        }
    
        public virtual ObjectResult<SP_ADMIN_REVIEW_EXP_ARTICLE_SEL_Result> SP_ADMIN_REVIEW_EXP_ARTICLE_SEL(Nullable<int> eXP_MASTER_IDX, Nullable<int> pAGE, Nullable<int> pAGESIZE, string sEARCH_KEY, string sEARCH_VALUE, ObjectParameter tOTAL, ObjectParameter rET_NUM, ObjectParameter rET_MESSAGE)
        {
            var eXP_MASTER_IDXParameter = eXP_MASTER_IDX.HasValue ?
                new ObjectParameter("EXP_MASTER_IDX", eXP_MASTER_IDX) :
                new ObjectParameter("EXP_MASTER_IDX", typeof(int));
    
            var pAGEParameter = pAGE.HasValue ?
                new ObjectParameter("PAGE", pAGE) :
                new ObjectParameter("PAGE", typeof(int));
    
            var pAGESIZEParameter = pAGESIZE.HasValue ?
                new ObjectParameter("PAGESIZE", pAGESIZE) :
                new ObjectParameter("PAGESIZE", typeof(int));
    
            var sEARCH_KEYParameter = sEARCH_KEY != null ?
                new ObjectParameter("SEARCH_KEY", sEARCH_KEY) :
                new ObjectParameter("SEARCH_KEY", typeof(string));
    
            var sEARCH_VALUEParameter = sEARCH_VALUE != null ?
                new ObjectParameter("SEARCH_VALUE", sEARCH_VALUE) :
                new ObjectParameter("SEARCH_VALUE", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_REVIEW_EXP_ARTICLE_SEL_Result>("SP_ADMIN_REVIEW_EXP_ARTICLE_SEL", eXP_MASTER_IDXParameter, pAGEParameter, pAGESIZEParameter, sEARCH_KEYParameter, sEARCH_VALUEParameter, tOTAL, rET_NUM, rET_MESSAGE);
        }
    
        public virtual int SP_ADMIN_REVIEW_EXP_ARTICLE_UPDATE(Nullable<int> iDX, string iS_DISPLAY)
        {
            var iDXParameter = iDX.HasValue ?
                new ObjectParameter("IDX", iDX) :
                new ObjectParameter("IDX", typeof(int));
    
            var iS_DISPLAYParameter = iS_DISPLAY != null ?
                new ObjectParameter("IS_DISPLAY", iS_DISPLAY) :
                new ObjectParameter("IS_DISPLAY", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_REVIEW_EXP_ARTICLE_UPDATE", iDXParameter, iS_DISPLAYParameter);
        }
    }
}
