﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AboutMe.Domain.Entity.Review
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ReviewEntities : DbContext
    {
        public ReviewEntities()
            : base("name=ReviewEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
    
        public virtual int SP_REVIEW_PRODUCT_INS(string m_ID, Nullable<int> oRDER_DETAIL_IDX, string p_CODE, string sKIN_TYPE, string cOMMENT, string aDD_IMAGE, string mEDIA_GBN, ObjectParameter rET_NUM, ObjectParameter rET_MESSAGE)
        {
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            var oRDER_DETAIL_IDXParameter = oRDER_DETAIL_IDX.HasValue ?
                new ObjectParameter("ORDER_DETAIL_IDX", oRDER_DETAIL_IDX) :
                new ObjectParameter("ORDER_DETAIL_IDX", typeof(int));
    
            var p_CODEParameter = p_CODE != null ?
                new ObjectParameter("P_CODE", p_CODE) :
                new ObjectParameter("P_CODE", typeof(string));
    
            var sKIN_TYPEParameter = sKIN_TYPE != null ?
                new ObjectParameter("SKIN_TYPE", sKIN_TYPE) :
                new ObjectParameter("SKIN_TYPE", typeof(string));
    
            var cOMMENTParameter = cOMMENT != null ?
                new ObjectParameter("COMMENT", cOMMENT) :
                new ObjectParameter("COMMENT", typeof(string));
    
            var aDD_IMAGEParameter = aDD_IMAGE != null ?
                new ObjectParameter("ADD_IMAGE", aDD_IMAGE) :
                new ObjectParameter("ADD_IMAGE", typeof(string));
    
            var mEDIA_GBNParameter = mEDIA_GBN != null ?
                new ObjectParameter("MEDIA_GBN", mEDIA_GBN) :
                new ObjectParameter("MEDIA_GBN", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_REVIEW_PRODUCT_INS", m_IDParameter, oRDER_DETAIL_IDXParameter, p_CODEParameter, sKIN_TYPEParameter, cOMMENTParameter, aDD_IMAGEParameter, mEDIA_GBNParameter, rET_NUM, rET_MESSAGE);
        }
    
        public virtual ObjectResult<SP_REVIEW_PRODUCT_READY_SEL_Result> SP_REVIEW_PRODUCT_READY_SEL(string m_ID, ObjectParameter rET_NUM, ObjectParameter rET_MESSAGE)
        {
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_REVIEW_PRODUCT_READY_SEL_Result>("SP_REVIEW_PRODUCT_READY_SEL", m_IDParameter, rET_NUM, rET_MESSAGE);
        }
    
        public virtual int SP_REVIEW_PRODUCT_UPD(Nullable<int> iDX, string cOMMENT, string aDD_IMAGE, ObjectParameter rET_NUM, ObjectParameter rET_MESSAGE)
        {
            var iDXParameter = iDX.HasValue ?
                new ObjectParameter("IDX", iDX) :
                new ObjectParameter("IDX", typeof(int));
    
            var cOMMENTParameter = cOMMENT != null ?
                new ObjectParameter("COMMENT", cOMMENT) :
                new ObjectParameter("COMMENT", typeof(string));
    
            var aDD_IMAGEParameter = aDD_IMAGE != null ?
                new ObjectParameter("ADD_IMAGE", aDD_IMAGE) :
                new ObjectParameter("ADD_IMAGE", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_REVIEW_PRODUCT_UPD", iDXParameter, cOMMENTParameter, aDD_IMAGEParameter, rET_NUM, rET_MESSAGE);
        }
    
        public virtual ObjectResult<SP_REVIEW_GET_PRODUCT_INFO_Result> SP_REVIEW_GET_PRODUCT_INFO(string p_CODE)
        {
            var p_CODEParameter = p_CODE != null ?
                new ObjectParameter("P_CODE", p_CODE) :
                new ObjectParameter("P_CODE", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_REVIEW_GET_PRODUCT_INFO_Result>("SP_REVIEW_GET_PRODUCT_INFO", p_CODEParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> SP_REVIEW_PRODUCT_COMPLETE_CNT(string m_ID)
        {
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("SP_REVIEW_PRODUCT_COMPLETE_CNT", m_IDParameter);
        }
    
        public virtual ObjectResult<SP_REVIEW_PRODUCT_COMPLETE_SEL_Result> SP_REVIEW_PRODUCT_COMPLETE_SEL(string m_ID, Nullable<int> pAGE, Nullable<int> pAGESIZE, ObjectParameter rET_NUM, ObjectParameter rET_MESSAGE)
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
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_REVIEW_PRODUCT_COMPLETE_SEL_Result>("SP_REVIEW_PRODUCT_COMPLETE_SEL", m_IDParameter, pAGEParameter, pAGESIZEParameter, rET_NUM, rET_MESSAGE);
        }
    
        public virtual ObjectResult<SP_REVIEW_PRODUCT_SEL_Result> SP_REVIEW_PRODUCT_SEL(Nullable<int> tAIL_IDX, string cATE_CODE, string sORT, ObjectParameter tOTAL)
        {
            var tAIL_IDXParameter = tAIL_IDX.HasValue ?
                new ObjectParameter("TAIL_IDX", tAIL_IDX) :
                new ObjectParameter("TAIL_IDX", typeof(int));
    
            var cATE_CODEParameter = cATE_CODE != null ?
                new ObjectParameter("CATE_CODE", cATE_CODE) :
                new ObjectParameter("CATE_CODE", typeof(string));
    
            var sORTParameter = sORT != null ?
                new ObjectParameter("SORT", sORT) :
                new ObjectParameter("SORT", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_REVIEW_PRODUCT_SEL_Result>("SP_REVIEW_PRODUCT_SEL", tAIL_IDXParameter, cATE_CODEParameter, sORTParameter, tOTAL);
        }
    
        public virtual ObjectResult<SP_REVIEW_PRODUCT_IN_SHOPPING_DETAIL_Result> SP_REVIEW_PRODUCT_IN_SHOPPING_DETAIL(string p_CODE, Nullable<int> pAGE, Nullable<int> pAGESIZE, ObjectParameter tOTAL)
        {
            var p_CODEParameter = p_CODE != null ?
                new ObjectParameter("P_CODE", p_CODE) :
                new ObjectParameter("P_CODE", typeof(string));
    
            var pAGEParameter = pAGE.HasValue ?
                new ObjectParameter("PAGE", pAGE) :
                new ObjectParameter("PAGE", typeof(int));
    
            var pAGESIZEParameter = pAGESIZE.HasValue ?
                new ObjectParameter("PAGESIZE", pAGESIZE) :
                new ObjectParameter("PAGESIZE", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_REVIEW_PRODUCT_IN_SHOPPING_DETAIL_Result>("SP_REVIEW_PRODUCT_IN_SHOPPING_DETAIL", p_CODEParameter, pAGEParameter, pAGESIZEParameter, tOTAL);
        }
    
        public virtual ObjectResult<SP_REVIEW_PRODUCT_INFO_Result> SP_REVIEW_PRODUCT_INFO(Nullable<int> iDX, ObjectParameter rET_NUM, ObjectParameter rET_MESSAGE)
        {
            var iDXParameter = iDX.HasValue ?
                new ObjectParameter("IDX", iDX) :
                new ObjectParameter("IDX", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_REVIEW_PRODUCT_INFO_Result>("SP_REVIEW_PRODUCT_INFO", iDXParameter, rET_NUM, rET_MESSAGE);
        }
    
        public virtual ObjectResult<SP_REVIEW_CATE_THEMA_SEL_Result> SP_REVIEW_CATE_THEMA_SEL()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_REVIEW_CATE_THEMA_SEL_Result>("SP_REVIEW_CATE_THEMA_SEL");
        }
    
        public virtual ObjectResult<Nullable<int>> SP_REVIEW_PRODUCT_READY_CNT(string m_ID)
        {
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("SP_REVIEW_PRODUCT_READY_CNT", m_IDParameter);
        }
    
        public virtual int SP_REVIEW_MY_EXP_INS(string m_ID, Nullable<int> eXP_MASTER_IDX, string p_CODE, string tITLE, string sKIN_TYPE, string sUB_IMG_1, string sUB_IMG_2, string sUB_IMG_3, string tAG, string cOMMENT, ObjectParameter rET_NUM, ObjectParameter rET_MESSAGE)
        {
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            var eXP_MASTER_IDXParameter = eXP_MASTER_IDX.HasValue ?
                new ObjectParameter("EXP_MASTER_IDX", eXP_MASTER_IDX) :
                new ObjectParameter("EXP_MASTER_IDX", typeof(int));
    
            var p_CODEParameter = p_CODE != null ?
                new ObjectParameter("P_CODE", p_CODE) :
                new ObjectParameter("P_CODE", typeof(string));
    
            var tITLEParameter = tITLE != null ?
                new ObjectParameter("TITLE", tITLE) :
                new ObjectParameter("TITLE", typeof(string));
    
            var sKIN_TYPEParameter = sKIN_TYPE != null ?
                new ObjectParameter("SKIN_TYPE", sKIN_TYPE) :
                new ObjectParameter("SKIN_TYPE", typeof(string));
    
            var sUB_IMG_1Parameter = sUB_IMG_1 != null ?
                new ObjectParameter("SUB_IMG_1", sUB_IMG_1) :
                new ObjectParameter("SUB_IMG_1", typeof(string));
    
            var sUB_IMG_2Parameter = sUB_IMG_2 != null ?
                new ObjectParameter("SUB_IMG_2", sUB_IMG_2) :
                new ObjectParameter("SUB_IMG_2", typeof(string));
    
            var sUB_IMG_3Parameter = sUB_IMG_3 != null ?
                new ObjectParameter("SUB_IMG_3", sUB_IMG_3) :
                new ObjectParameter("SUB_IMG_3", typeof(string));
    
            var tAGParameter = tAG != null ?
                new ObjectParameter("TAG", tAG) :
                new ObjectParameter("TAG", typeof(string));
    
            var cOMMENTParameter = cOMMENT != null ?
                new ObjectParameter("COMMENT", cOMMENT) :
                new ObjectParameter("COMMENT", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_REVIEW_MY_EXP_INS", m_IDParameter, eXP_MASTER_IDXParameter, p_CODEParameter, tITLEParameter, sKIN_TYPEParameter, sUB_IMG_1Parameter, sUB_IMG_2Parameter, sUB_IMG_3Parameter, tAGParameter, cOMMENTParameter, rET_NUM, rET_MESSAGE);
        }
    
        public virtual ObjectResult<SP_REVIEW_MY_EXP_MASTER_SEL_Result> SP_REVIEW_MY_EXP_MASTER_SEL(string m_ID)
        {
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_REVIEW_MY_EXP_MASTER_SEL_Result>("SP_REVIEW_MY_EXP_MASTER_SEL", m_IDParameter);
        }
    
        public virtual int SP_REVIEW_MY_EXP_UPD(Nullable<int> iDX, string m_ID, Nullable<int> eXP_MASTER_IDX, string p_CODE, string tITLE, string sKIN_TYPE, string sUB_IMG_1, string sUB_IMG_2, string sUB_IMG_3, string tAG, string cOMMENT, ObjectParameter rET_NUM, ObjectParameter rET_MESSAGE)
        {
            var iDXParameter = iDX.HasValue ?
                new ObjectParameter("IDX", iDX) :
                new ObjectParameter("IDX", typeof(int));
    
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            var eXP_MASTER_IDXParameter = eXP_MASTER_IDX.HasValue ?
                new ObjectParameter("EXP_MASTER_IDX", eXP_MASTER_IDX) :
                new ObjectParameter("EXP_MASTER_IDX", typeof(int));
    
            var p_CODEParameter = p_CODE != null ?
                new ObjectParameter("P_CODE", p_CODE) :
                new ObjectParameter("P_CODE", typeof(string));
    
            var tITLEParameter = tITLE != null ?
                new ObjectParameter("TITLE", tITLE) :
                new ObjectParameter("TITLE", typeof(string));
    
            var sKIN_TYPEParameter = sKIN_TYPE != null ?
                new ObjectParameter("SKIN_TYPE", sKIN_TYPE) :
                new ObjectParameter("SKIN_TYPE", typeof(string));
    
            var sUB_IMG_1Parameter = sUB_IMG_1 != null ?
                new ObjectParameter("SUB_IMG_1", sUB_IMG_1) :
                new ObjectParameter("SUB_IMG_1", typeof(string));
    
            var sUB_IMG_2Parameter = sUB_IMG_2 != null ?
                new ObjectParameter("SUB_IMG_2", sUB_IMG_2) :
                new ObjectParameter("SUB_IMG_2", typeof(string));
    
            var sUB_IMG_3Parameter = sUB_IMG_3 != null ?
                new ObjectParameter("SUB_IMG_3", sUB_IMG_3) :
                new ObjectParameter("SUB_IMG_3", typeof(string));
    
            var tAGParameter = tAG != null ?
                new ObjectParameter("TAG", tAG) :
                new ObjectParameter("TAG", typeof(string));
    
            var cOMMENTParameter = cOMMENT != null ?
                new ObjectParameter("COMMENT", cOMMENT) :
                new ObjectParameter("COMMENT", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_REVIEW_MY_EXP_UPD", iDXParameter, m_IDParameter, eXP_MASTER_IDXParameter, p_CODEParameter, tITLEParameter, sKIN_TYPEParameter, sUB_IMG_1Parameter, sUB_IMG_2Parameter, sUB_IMG_3Parameter, tAGParameter, cOMMENTParameter, rET_NUM, rET_MESSAGE);
        }
    
        public virtual ObjectResult<SP_REVIEW_MY_EXP_DETAIL_Result> SP_REVIEW_MY_EXP_DETAIL(Nullable<int> iDX)
        {
            var iDXParameter = iDX.HasValue ?
                new ObjectParameter("IDX", iDX) :
                new ObjectParameter("IDX", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_REVIEW_MY_EXP_DETAIL_Result>("SP_REVIEW_MY_EXP_DETAIL", iDXParameter);
        }
    
        public virtual ObjectResult<SP_REVIEW_EXP_SEL_Result> SP_REVIEW_EXP_SEL(Nullable<int> tAIL_IDX, string cATE_CODE, string sORT, ObjectParameter tOTAL)
        {
            var tAIL_IDXParameter = tAIL_IDX.HasValue ?
                new ObjectParameter("TAIL_IDX", tAIL_IDX) :
                new ObjectParameter("TAIL_IDX", typeof(int));
    
            var cATE_CODEParameter = cATE_CODE != null ?
                new ObjectParameter("CATE_CODE", cATE_CODE) :
                new ObjectParameter("CATE_CODE", typeof(string));
    
            var sORTParameter = sORT != null ?
                new ObjectParameter("SORT", sORT) :
                new ObjectParameter("SORT", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_REVIEW_EXP_SEL_Result>("SP_REVIEW_EXP_SEL", tAIL_IDXParameter, cATE_CODEParameter, sORTParameter, tOTAL);
        }
    
        public virtual ObjectResult<SP_REVIEW_EXP_DETAIL_Result> SP_REVIEW_EXP_DETAIL(Nullable<int> iDX, string p_CODE)
        {
            var iDXParameter = iDX.HasValue ?
                new ObjectParameter("IDX", iDX) :
                new ObjectParameter("IDX", typeof(int));
    
            var p_CODEParameter = p_CODE != null ?
                new ObjectParameter("P_CODE", p_CODE) :
                new ObjectParameter("P_CODE", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_REVIEW_EXP_DETAIL_Result>("SP_REVIEW_EXP_DETAIL", iDXParameter, p_CODEParameter);
        }
    
        public virtual ObjectResult<SP_REVIEW_EXP_IN_SHOPPING_DETAIL_Result> SP_REVIEW_EXP_IN_SHOPPING_DETAIL(string p_CODE, Nullable<int> pAGE, Nullable<int> pAGESIZE, ObjectParameter tOTAL)
        {
            var p_CODEParameter = p_CODE != null ?
                new ObjectParameter("P_CODE", p_CODE) :
                new ObjectParameter("P_CODE", typeof(string));
    
            var pAGEParameter = pAGE.HasValue ?
                new ObjectParameter("PAGE", pAGE) :
                new ObjectParameter("PAGE", typeof(int));
    
            var pAGESIZEParameter = pAGESIZE.HasValue ?
                new ObjectParameter("PAGESIZE", pAGESIZE) :
                new ObjectParameter("PAGESIZE", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_REVIEW_EXP_IN_SHOPPING_DETAIL_Result>("SP_REVIEW_EXP_IN_SHOPPING_DETAIL", p_CODEParameter, pAGEParameter, pAGESIZEParameter, tOTAL);
        }
    
        public virtual ObjectResult<SP_REVIEW_PRODUCT_MOBILE_SEL_Result> SP_REVIEW_PRODUCT_MOBILE_SEL(Nullable<int> pAGE, Nullable<int> pAGESIZE, string cATE_CODE, string sORT, ObjectParameter tOTAL_PAGE, ObjectParameter tOTAL_ITEM)
        {
            var pAGEParameter = pAGE.HasValue ?
                new ObjectParameter("PAGE", pAGE) :
                new ObjectParameter("PAGE", typeof(int));
    
            var pAGESIZEParameter = pAGESIZE.HasValue ?
                new ObjectParameter("PAGESIZE", pAGESIZE) :
                new ObjectParameter("PAGESIZE", typeof(int));
    
            var cATE_CODEParameter = cATE_CODE != null ?
                new ObjectParameter("CATE_CODE", cATE_CODE) :
                new ObjectParameter("CATE_CODE", typeof(string));
    
            var sORTParameter = sORT != null ?
                new ObjectParameter("SORT", sORT) :
                new ObjectParameter("SORT", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_REVIEW_PRODUCT_MOBILE_SEL_Result>("SP_REVIEW_PRODUCT_MOBILE_SEL", pAGEParameter, pAGESIZEParameter, cATE_CODEParameter, sORTParameter, tOTAL_PAGE, tOTAL_ITEM);
        }
    
        public virtual ObjectResult<SP_REVIEW_EXP_MOBILE_SEL_Result> SP_REVIEW_EXP_MOBILE_SEL(Nullable<int> pAGE, Nullable<int> pAGESIZE, string cATE_CODE, string sORT, ObjectParameter tOTAL_PAGE, ObjectParameter tOTAL_ITEM)
        {
            var pAGEParameter = pAGE.HasValue ?
                new ObjectParameter("PAGE", pAGE) :
                new ObjectParameter("PAGE", typeof(int));
    
            var pAGESIZEParameter = pAGESIZE.HasValue ?
                new ObjectParameter("PAGESIZE", pAGESIZE) :
                new ObjectParameter("PAGESIZE", typeof(int));
    
            var cATE_CODEParameter = cATE_CODE != null ?
                new ObjectParameter("CATE_CODE", cATE_CODE) :
                new ObjectParameter("CATE_CODE", typeof(string));
    
            var sORTParameter = sORT != null ?
                new ObjectParameter("SORT", sORT) :
                new ObjectParameter("SORT", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_REVIEW_EXP_MOBILE_SEL_Result>("SP_REVIEW_EXP_MOBILE_SEL", pAGEParameter, pAGESIZEParameter, cATE_CODEParameter, sORTParameter, tOTAL_PAGE, tOTAL_ITEM);
        }
    
        public virtual ObjectResult<SP_REVIEW_PRODCUT_DETAIL_BY_MOST_REVIEW_PDT_Result> SP_REVIEW_PRODCUT_DETAIL_BY_MOST_REVIEW_PDT()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_REVIEW_PRODCUT_DETAIL_BY_MOST_REVIEW_PDT_Result>("SP_REVIEW_PRODCUT_DETAIL_BY_MOST_REVIEW_PDT");
        }
    
        public virtual ObjectResult<Nullable<int>> SP_REVIEW_PRODCUT_TOTAL_BY_PCODE(string p_CODE)
        {
            var p_CODEParameter = p_CODE != null ?
                new ObjectParameter("P_CODE", p_CODE) :
                new ObjectParameter("P_CODE", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("SP_REVIEW_PRODCUT_TOTAL_BY_PCODE", p_CODEParameter);
        }
    
        public virtual ObjectResult<SP_REVIEW_FREE_SEL_Result> SP_REVIEW_FREE_SEL(Nullable<int> tAIL_IDX, string cATE_CODE, string sORT, ObjectParameter tOTAL)
        {
            var tAIL_IDXParameter = tAIL_IDX.HasValue ?
                new ObjectParameter("TAIL_IDX", tAIL_IDX) :
                new ObjectParameter("TAIL_IDX", typeof(int));
    
            var cATE_CODEParameter = cATE_CODE != null ?
                new ObjectParameter("CATE_CODE", cATE_CODE) :
                new ObjectParameter("CATE_CODE", typeof(string));
    
            var sORTParameter = sORT != null ?
                new ObjectParameter("SORT", sORT) :
                new ObjectParameter("SORT", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_REVIEW_FREE_SEL_Result>("SP_REVIEW_FREE_SEL", tAIL_IDXParameter, cATE_CODEParameter, sORTParameter, tOTAL);
        }
    
        public virtual ObjectResult<SP_REVIEW_FREE_IN_SHOPPING_DETAIL_Result> SP_REVIEW_FREE_IN_SHOPPING_DETAIL(string p_CODE, Nullable<int> pAGE, Nullable<int> pAGESIZE, ObjectParameter tOTAL)
        {
            var p_CODEParameter = p_CODE != null ?
                new ObjectParameter("P_CODE", p_CODE) :
                new ObjectParameter("P_CODE", typeof(string));
    
            var pAGEParameter = pAGE.HasValue ?
                new ObjectParameter("PAGE", pAGE) :
                new ObjectParameter("PAGE", typeof(int));
    
            var pAGESIZEParameter = pAGESIZE.HasValue ?
                new ObjectParameter("PAGESIZE", pAGESIZE) :
                new ObjectParameter("PAGESIZE", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_REVIEW_FREE_IN_SHOPPING_DETAIL_Result>("SP_REVIEW_FREE_IN_SHOPPING_DETAIL", p_CODEParameter, pAGEParameter, pAGESIZEParameter, tOTAL);
        }
    
        public virtual int SP_REVIEW_FREE_INS(string m_ID, string p_CODE, string sKIN_TYPE, string cOMMENT, string aDD_IMAGE, string mEDIA_GBN, ObjectParameter rET_NUM, ObjectParameter rET_MESSAGE)
        {
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            var p_CODEParameter = p_CODE != null ?
                new ObjectParameter("P_CODE", p_CODE) :
                new ObjectParameter("P_CODE", typeof(string));
    
            var sKIN_TYPEParameter = sKIN_TYPE != null ?
                new ObjectParameter("SKIN_TYPE", sKIN_TYPE) :
                new ObjectParameter("SKIN_TYPE", typeof(string));
    
            var cOMMENTParameter = cOMMENT != null ?
                new ObjectParameter("COMMENT", cOMMENT) :
                new ObjectParameter("COMMENT", typeof(string));
    
            var aDD_IMAGEParameter = aDD_IMAGE != null ?
                new ObjectParameter("ADD_IMAGE", aDD_IMAGE) :
                new ObjectParameter("ADD_IMAGE", typeof(string));
    
            var mEDIA_GBNParameter = mEDIA_GBN != null ?
                new ObjectParameter("MEDIA_GBN", mEDIA_GBN) :
                new ObjectParameter("MEDIA_GBN", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_REVIEW_FREE_INS", m_IDParameter, p_CODEParameter, sKIN_TYPEParameter, cOMMENTParameter, aDD_IMAGEParameter, mEDIA_GBNParameter, rET_NUM, rET_MESSAGE);
        }
    
        public virtual ObjectResult<SP_REVIEW_FREE_INFO_Result> SP_REVIEW_FREE_INFO(Nullable<int> iDX, ObjectParameter rET_NUM, ObjectParameter rET_MESSAGE)
        {
            var iDXParameter = iDX.HasValue ?
                new ObjectParameter("IDX", iDX) :
                new ObjectParameter("IDX", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_REVIEW_FREE_INFO_Result>("SP_REVIEW_FREE_INFO", iDXParameter, rET_NUM, rET_MESSAGE);
        }
    
        public virtual int SP_REVIEW_FREE_UPD(Nullable<int> iDX, string cOMMENT, string aDD_IMAGE, ObjectParameter rET_NUM, ObjectParameter rET_MESSAGE)
        {
            var iDXParameter = iDX.HasValue ?
                new ObjectParameter("IDX", iDX) :
                new ObjectParameter("IDX", typeof(int));
    
            var cOMMENTParameter = cOMMENT != null ?
                new ObjectParameter("COMMENT", cOMMENT) :
                new ObjectParameter("COMMENT", typeof(string));
    
            var aDD_IMAGEParameter = aDD_IMAGE != null ?
                new ObjectParameter("ADD_IMAGE", aDD_IMAGE) :
                new ObjectParameter("ADD_IMAGE", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_REVIEW_FREE_UPD", iDXParameter, cOMMENTParameter, aDD_IMAGEParameter, rET_NUM, rET_MESSAGE);
        }
    }
}
