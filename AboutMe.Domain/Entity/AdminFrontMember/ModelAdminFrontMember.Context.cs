﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AboutMe.Domain.Entity.AdminFrontMember
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class AdminFrontMemberEntities : DbContext
    {
        public AdminFrontMemberEntities()
            : base("name=AdminFrontMemberEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
    
        public virtual int SP_ADMIN_FRONT_MEMBER_PWD_UPD(string m_ID, string m_PWD, ObjectParameter eRR_CODE)
        {
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            var m_PWDParameter = m_PWD != null ?
                new ObjectParameter("M_PWD", m_PWD) :
                new ObjectParameter("M_PWD", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_FRONT_MEMBER_PWD_UPD", m_IDParameter, m_PWDParameter, eRR_CODE);
        }
    
        public virtual ObjectResult<SP_ADMIN_FRONT_COMMON_CNT_Result> SP_ADMIN_FRONT_MEMBER_CNT(string dATE_FROM, string dATE_TO, string gRADE_LIST, string gBN, string iS_RETIRE, string sEARCH_COL, string sEARCH_KEYWORD)
        {
            var dATE_FROMParameter = dATE_FROM != null ?
                new ObjectParameter("DATE_FROM", dATE_FROM) :
                new ObjectParameter("DATE_FROM", typeof(string));
    
            var dATE_TOParameter = dATE_TO != null ?
                new ObjectParameter("DATE_TO", dATE_TO) :
                new ObjectParameter("DATE_TO", typeof(string));
    
            var gRADE_LISTParameter = gRADE_LIST != null ?
                new ObjectParameter("GRADE_LIST", gRADE_LIST) :
                new ObjectParameter("GRADE_LIST", typeof(string));
    
            var gBNParameter = gBN != null ?
                new ObjectParameter("GBN", gBN) :
                new ObjectParameter("GBN", typeof(string));
    
            var iS_RETIREParameter = iS_RETIRE != null ?
                new ObjectParameter("IS_RETIRE", iS_RETIRE) :
                new ObjectParameter("IS_RETIRE", typeof(string));
    
            var sEARCH_COLParameter = sEARCH_COL != null ?
                new ObjectParameter("SEARCH_COL", sEARCH_COL) :
                new ObjectParameter("SEARCH_COL", typeof(string));
    
            var sEARCH_KEYWORDParameter = sEARCH_KEYWORD != null ?
                new ObjectParameter("SEARCH_KEYWORD", sEARCH_KEYWORD) :
                new ObjectParameter("SEARCH_KEYWORD", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_FRONT_COMMON_CNT_Result>("SP_ADMIN_FRONT_MEMBER_CNT", dATE_FROMParameter, dATE_TOParameter, gRADE_LISTParameter, gBNParameter, iS_RETIREParameter, sEARCH_COLParameter, sEARCH_KEYWORDParameter);
        }
    
        public virtual ObjectResult<SP_ADMIN_FRONT_MEMBER_VIEW_Result> SP_ADMIN_FRONT_MEMBER_SEL(string dATE_FROM, string dATE_TO, string gRADE_LIST, string gBN, string iS_RETIRE, string sEARCH_COL, string sEARCH_KEYWORD, string sORT_COL, string sORT_DIR, Nullable<int> pAGE, Nullable<int> pAGESIZE)
        {
            var dATE_FROMParameter = dATE_FROM != null ?
                new ObjectParameter("DATE_FROM", dATE_FROM) :
                new ObjectParameter("DATE_FROM", typeof(string));
    
            var dATE_TOParameter = dATE_TO != null ?
                new ObjectParameter("DATE_TO", dATE_TO) :
                new ObjectParameter("DATE_TO", typeof(string));
    
            var gRADE_LISTParameter = gRADE_LIST != null ?
                new ObjectParameter("GRADE_LIST", gRADE_LIST) :
                new ObjectParameter("GRADE_LIST", typeof(string));
    
            var gBNParameter = gBN != null ?
                new ObjectParameter("GBN", gBN) :
                new ObjectParameter("GBN", typeof(string));
    
            var iS_RETIREParameter = iS_RETIRE != null ?
                new ObjectParameter("IS_RETIRE", iS_RETIRE) :
                new ObjectParameter("IS_RETIRE", typeof(string));
    
            var sEARCH_COLParameter = sEARCH_COL != null ?
                new ObjectParameter("SEARCH_COL", sEARCH_COL) :
                new ObjectParameter("SEARCH_COL", typeof(string));
    
            var sEARCH_KEYWORDParameter = sEARCH_KEYWORD != null ?
                new ObjectParameter("SEARCH_KEYWORD", sEARCH_KEYWORD) :
                new ObjectParameter("SEARCH_KEYWORD", typeof(string));
    
            var sORT_COLParameter = sORT_COL != null ?
                new ObjectParameter("SORT_COL", sORT_COL) :
                new ObjectParameter("SORT_COL", typeof(string));
    
            var sORT_DIRParameter = sORT_DIR != null ?
                new ObjectParameter("SORT_DIR", sORT_DIR) :
                new ObjectParameter("SORT_DIR", typeof(string));
    
            var pAGEParameter = pAGE.HasValue ?
                new ObjectParameter("PAGE", pAGE) :
                new ObjectParameter("PAGE", typeof(int));
    
            var pAGESIZEParameter = pAGESIZE.HasValue ?
                new ObjectParameter("PAGESIZE", pAGESIZE) :
                new ObjectParameter("PAGESIZE", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_FRONT_MEMBER_VIEW_Result>("SP_ADMIN_FRONT_MEMBER_SEL", dATE_FROMParameter, dATE_TOParameter, gRADE_LISTParameter, gBNParameter, iS_RETIREParameter, sEARCH_COLParameter, sEARCH_KEYWORDParameter, sORT_COLParameter, sORT_DIRParameter, pAGEParameter, pAGESIZEParameter);
        }
    
        public virtual int SP_ADMIN_FRONT_MEMBER_UPD(string m_ID, string m_GRADE, string m_MOBILE, string m_PHONE, string m_EMAIL, string m_ZIPCODE, string m_ADDR1, string m_ADDR2, string m_ISSMS, string m_ISEMAIL, string m_ISDM, string m_SKIN_TROUBLE_CD, string m_GBN, string m_STAFF_COMPANY, string m_STAFF_ID, string m_IS_RETIRE, string m_DEL_REASON, ObjectParameter eRR_CODE)
        {
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            var m_GRADEParameter = m_GRADE != null ?
                new ObjectParameter("M_GRADE", m_GRADE) :
                new ObjectParameter("M_GRADE", typeof(string));
    
            var m_MOBILEParameter = m_MOBILE != null ?
                new ObjectParameter("M_MOBILE", m_MOBILE) :
                new ObjectParameter("M_MOBILE", typeof(string));
    
            var m_PHONEParameter = m_PHONE != null ?
                new ObjectParameter("M_PHONE", m_PHONE) :
                new ObjectParameter("M_PHONE", typeof(string));
    
            var m_EMAILParameter = m_EMAIL != null ?
                new ObjectParameter("M_EMAIL", m_EMAIL) :
                new ObjectParameter("M_EMAIL", typeof(string));
    
            var m_ZIPCODEParameter = m_ZIPCODE != null ?
                new ObjectParameter("M_ZIPCODE", m_ZIPCODE) :
                new ObjectParameter("M_ZIPCODE", typeof(string));
    
            var m_ADDR1Parameter = m_ADDR1 != null ?
                new ObjectParameter("M_ADDR1", m_ADDR1) :
                new ObjectParameter("M_ADDR1", typeof(string));
    
            var m_ADDR2Parameter = m_ADDR2 != null ?
                new ObjectParameter("M_ADDR2", m_ADDR2) :
                new ObjectParameter("M_ADDR2", typeof(string));
    
            var m_ISSMSParameter = m_ISSMS != null ?
                new ObjectParameter("M_ISSMS", m_ISSMS) :
                new ObjectParameter("M_ISSMS", typeof(string));
    
            var m_ISEMAILParameter = m_ISEMAIL != null ?
                new ObjectParameter("M_ISEMAIL", m_ISEMAIL) :
                new ObjectParameter("M_ISEMAIL", typeof(string));
    
            var m_ISDMParameter = m_ISDM != null ?
                new ObjectParameter("M_ISDM", m_ISDM) :
                new ObjectParameter("M_ISDM", typeof(string));
    
            var m_SKIN_TROUBLE_CDParameter = m_SKIN_TROUBLE_CD != null ?
                new ObjectParameter("M_SKIN_TROUBLE_CD", m_SKIN_TROUBLE_CD) :
                new ObjectParameter("M_SKIN_TROUBLE_CD", typeof(string));
    
            var m_GBNParameter = m_GBN != null ?
                new ObjectParameter("M_GBN", m_GBN) :
                new ObjectParameter("M_GBN", typeof(string));
    
            var m_STAFF_COMPANYParameter = m_STAFF_COMPANY != null ?
                new ObjectParameter("M_STAFF_COMPANY", m_STAFF_COMPANY) :
                new ObjectParameter("M_STAFF_COMPANY", typeof(string));
    
            var m_STAFF_IDParameter = m_STAFF_ID != null ?
                new ObjectParameter("M_STAFF_ID", m_STAFF_ID) :
                new ObjectParameter("M_STAFF_ID", typeof(string));
    
            var m_IS_RETIREParameter = m_IS_RETIRE != null ?
                new ObjectParameter("M_IS_RETIRE", m_IS_RETIRE) :
                new ObjectParameter("M_IS_RETIRE", typeof(string));
    
            var m_DEL_REASONParameter = m_DEL_REASON != null ?
                new ObjectParameter("M_DEL_REASON", m_DEL_REASON) :
                new ObjectParameter("M_DEL_REASON", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_FRONT_MEMBER_UPD", m_IDParameter, m_GRADEParameter, m_MOBILEParameter, m_PHONEParameter, m_EMAILParameter, m_ZIPCODEParameter, m_ADDR1Parameter, m_ADDR2Parameter, m_ISSMSParameter, m_ISEMAILParameter, m_ISDMParameter, m_SKIN_TROUBLE_CDParameter, m_GBNParameter, m_STAFF_COMPANYParameter, m_STAFF_IDParameter, m_IS_RETIREParameter, m_DEL_REASONParameter, eRR_CODE);
        }
    
        public virtual ObjectResult<SP_ADMIN_FRONT_MEMBER_VIEW_Result> SP_ADMIN_FRONT_MEMBER_VIEW(string m_ID)
        {
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_FRONT_MEMBER_VIEW_Result>("SP_ADMIN_FRONT_MEMBER_VIEW", m_IDParameter);
        }
    
        public virtual int SP_ADMIN_FRONT_MEMBER_UPD_BASIC(string m_ID, string m_GRADE, string m_MOBILE, string m_PHONE, string m_EMAIL, string m_ZIPCODE, string m_ADDR1, string m_ADDR2, string m_ISSMS, string m_ISEMAIL, string m_ISDM, ObjectParameter eRR_CODE)
        {
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            var m_GRADEParameter = m_GRADE != null ?
                new ObjectParameter("M_GRADE", m_GRADE) :
                new ObjectParameter("M_GRADE", typeof(string));
    
            var m_MOBILEParameter = m_MOBILE != null ?
                new ObjectParameter("M_MOBILE", m_MOBILE) :
                new ObjectParameter("M_MOBILE", typeof(string));
    
            var m_PHONEParameter = m_PHONE != null ?
                new ObjectParameter("M_PHONE", m_PHONE) :
                new ObjectParameter("M_PHONE", typeof(string));
    
            var m_EMAILParameter = m_EMAIL != null ?
                new ObjectParameter("M_EMAIL", m_EMAIL) :
                new ObjectParameter("M_EMAIL", typeof(string));
    
            var m_ZIPCODEParameter = m_ZIPCODE != null ?
                new ObjectParameter("M_ZIPCODE", m_ZIPCODE) :
                new ObjectParameter("M_ZIPCODE", typeof(string));
    
            var m_ADDR1Parameter = m_ADDR1 != null ?
                new ObjectParameter("M_ADDR1", m_ADDR1) :
                new ObjectParameter("M_ADDR1", typeof(string));
    
            var m_ADDR2Parameter = m_ADDR2 != null ?
                new ObjectParameter("M_ADDR2", m_ADDR2) :
                new ObjectParameter("M_ADDR2", typeof(string));
    
            var m_ISSMSParameter = m_ISSMS != null ?
                new ObjectParameter("M_ISSMS", m_ISSMS) :
                new ObjectParameter("M_ISSMS", typeof(string));
    
            var m_ISEMAILParameter = m_ISEMAIL != null ?
                new ObjectParameter("M_ISEMAIL", m_ISEMAIL) :
                new ObjectParameter("M_ISEMAIL", typeof(string));
    
            var m_ISDMParameter = m_ISDM != null ?
                new ObjectParameter("M_ISDM", m_ISDM) :
                new ObjectParameter("M_ISDM", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_FRONT_MEMBER_UPD_BASIC", m_IDParameter, m_GRADEParameter, m_MOBILEParameter, m_PHONEParameter, m_EMAILParameter, m_ZIPCODEParameter, m_ADDR1Parameter, m_ADDR2Parameter, m_ISSMSParameter, m_ISEMAILParameter, m_ISDMParameter, eRR_CODE);
        }
    
        public virtual int SP_ADMIN_FRONT_MEMBER_UPD_PWD(string m_ID, string m_PWD, ObjectParameter eRR_CODE)
        {
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            var m_PWDParameter = m_PWD != null ?
                new ObjectParameter("M_PWD", m_PWD) :
                new ObjectParameter("M_PWD", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_FRONT_MEMBER_UPD_PWD", m_IDParameter, m_PWDParameter, eRR_CODE);
        }
    
        public virtual int SP_ADMIN_FRONT_MEMBER_UPD_RETIRE(string m_ID, string m_IS_RETIRE, string m_DEL_REASON, ObjectParameter eRR_CODE)
        {
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            var m_IS_RETIREParameter = m_IS_RETIRE != null ?
                new ObjectParameter("M_IS_RETIRE", m_IS_RETIRE) :
                new ObjectParameter("M_IS_RETIRE", typeof(string));
    
            var m_DEL_REASONParameter = m_DEL_REASON != null ?
                new ObjectParameter("M_DEL_REASON", m_DEL_REASON) :
                new ObjectParameter("M_DEL_REASON", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_FRONT_MEMBER_UPD_RETIRE", m_IDParameter, m_IS_RETIREParameter, m_DEL_REASONParameter, eRR_CODE);
        }
    
        public virtual int SP_ADMIN_FRONT_MEMBER_UPD_STAFF(string m_ID, string m_GBN, string m_STAFF_COMPANY, string m_STAFF_ID, ObjectParameter eRR_CODE)
        {
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            var m_GBNParameter = m_GBN != null ?
                new ObjectParameter("M_GBN", m_GBN) :
                new ObjectParameter("M_GBN", typeof(string));
    
            var m_STAFF_COMPANYParameter = m_STAFF_COMPANY != null ?
                new ObjectParameter("M_STAFF_COMPANY", m_STAFF_COMPANY) :
                new ObjectParameter("M_STAFF_COMPANY", typeof(string));
    
            var m_STAFF_IDParameter = m_STAFF_ID != null ?
                new ObjectParameter("M_STAFF_ID", m_STAFF_ID) :
                new ObjectParameter("M_STAFF_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_FRONT_MEMBER_UPD_STAFF", m_IDParameter, m_GBNParameter, m_STAFF_COMPANYParameter, m_STAFF_IDParameter, eRR_CODE);
        }
    
        public virtual ObjectResult<SP_ADMIN_FRONT_COMMON_CNT_Result> SP_ADMIN_MEMBER_STAFF_REQUST_CNT(string dATE_FROM, string dATE_TO, string sTATUS, string sEARCH_COL, string sEARCH_KEYWORD)
        {
            var dATE_FROMParameter = dATE_FROM != null ?
                new ObjectParameter("DATE_FROM", dATE_FROM) :
                new ObjectParameter("DATE_FROM", typeof(string));
    
            var dATE_TOParameter = dATE_TO != null ?
                new ObjectParameter("DATE_TO", dATE_TO) :
                new ObjectParameter("DATE_TO", typeof(string));
    
            var sTATUSParameter = sTATUS != null ?
                new ObjectParameter("STATUS", sTATUS) :
                new ObjectParameter("STATUS", typeof(string));
    
            var sEARCH_COLParameter = sEARCH_COL != null ?
                new ObjectParameter("SEARCH_COL", sEARCH_COL) :
                new ObjectParameter("SEARCH_COL", typeof(string));
    
            var sEARCH_KEYWORDParameter = sEARCH_KEYWORD != null ?
                new ObjectParameter("SEARCH_KEYWORD", sEARCH_KEYWORD) :
                new ObjectParameter("SEARCH_KEYWORD", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_FRONT_COMMON_CNT_Result>("SP_ADMIN_MEMBER_STAFF_REQUST_CNT", dATE_FROMParameter, dATE_TOParameter, sTATUSParameter, sEARCH_COLParameter, sEARCH_KEYWORDParameter);
        }
    
        public virtual ObjectResult<SP_ADMIN_MEMBER_STAFF_REQUST_VIEW_Result> SP_ADMIN_MEMBER_STAFF_REQUST_SEL(string dATE_FROM, string dATE_TO, string sTATUS, string sEARCH_COL, string sEARCH_KEYWORD, string sORT_COL, string sORT_DIR, Nullable<int> pAGE, Nullable<int> pAGESIZE)
        {
            var dATE_FROMParameter = dATE_FROM != null ?
                new ObjectParameter("DATE_FROM", dATE_FROM) :
                new ObjectParameter("DATE_FROM", typeof(string));
    
            var dATE_TOParameter = dATE_TO != null ?
                new ObjectParameter("DATE_TO", dATE_TO) :
                new ObjectParameter("DATE_TO", typeof(string));
    
            var sTATUSParameter = sTATUS != null ?
                new ObjectParameter("STATUS", sTATUS) :
                new ObjectParameter("STATUS", typeof(string));
    
            var sEARCH_COLParameter = sEARCH_COL != null ?
                new ObjectParameter("SEARCH_COL", sEARCH_COL) :
                new ObjectParameter("SEARCH_COL", typeof(string));
    
            var sEARCH_KEYWORDParameter = sEARCH_KEYWORD != null ?
                new ObjectParameter("SEARCH_KEYWORD", sEARCH_KEYWORD) :
                new ObjectParameter("SEARCH_KEYWORD", typeof(string));
    
            var sORT_COLParameter = sORT_COL != null ?
                new ObjectParameter("SORT_COL", sORT_COL) :
                new ObjectParameter("SORT_COL", typeof(string));
    
            var sORT_DIRParameter = sORT_DIR != null ?
                new ObjectParameter("SORT_DIR", sORT_DIR) :
                new ObjectParameter("SORT_DIR", typeof(string));
    
            var pAGEParameter = pAGE.HasValue ?
                new ObjectParameter("PAGE", pAGE) :
                new ObjectParameter("PAGE", typeof(int));
    
            var pAGESIZEParameter = pAGESIZE.HasValue ?
                new ObjectParameter("PAGESIZE", pAGESIZE) :
                new ObjectParameter("PAGESIZE", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_MEMBER_STAFF_REQUST_VIEW_Result>("SP_ADMIN_MEMBER_STAFF_REQUST_SEL", dATE_FROMParameter, dATE_TOParameter, sTATUSParameter, sEARCH_COLParameter, sEARCH_KEYWORDParameter, sORT_COLParameter, sORT_DIRParameter, pAGEParameter, pAGESIZEParameter);
        }
    
        public virtual int SP_ADMIN_MEMBER_STAFF_REQUST_UPD(Nullable<int> iDX, string sTATUS, string pROC_COMMENT, string pROC_ADM_ID, ObjectParameter eRR_CODE)
        {
            var iDXParameter = iDX.HasValue ?
                new ObjectParameter("IDX", iDX) :
                new ObjectParameter("IDX", typeof(int));
    
            var sTATUSParameter = sTATUS != null ?
                new ObjectParameter("STATUS", sTATUS) :
                new ObjectParameter("STATUS", typeof(string));
    
            var pROC_COMMENTParameter = pROC_COMMENT != null ?
                new ObjectParameter("PROC_COMMENT", pROC_COMMENT) :
                new ObjectParameter("PROC_COMMENT", typeof(string));
    
            var pROC_ADM_IDParameter = pROC_ADM_ID != null ?
                new ObjectParameter("PROC_ADM_ID", pROC_ADM_ID) :
                new ObjectParameter("PROC_ADM_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_MEMBER_STAFF_REQUST_UPD", iDXParameter, sTATUSParameter, pROC_COMMENTParameter, pROC_ADM_IDParameter, eRR_CODE);
        }
    
        public virtual ObjectResult<SP_ADMIN_MEMBER_STAFF_REQUST_VIEW_Result> SP_ADMIN_MEMBER_STAFF_REQUST_VIEW(Nullable<int> iDX)
        {
            var iDXParameter = iDX.HasValue ?
                new ObjectParameter("IDX", iDX) :
                new ObjectParameter("IDX", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_MEMBER_STAFF_REQUST_VIEW_Result>("SP_ADMIN_MEMBER_STAFF_REQUST_VIEW", iDXParameter);
        }
    
        public virtual ObjectResult<SP_ADMIN_MEMBER_STAFF_REQUST_REF_BASEDB_Result> SP_ADMIN_MEMBER_STAFF_REQUST_REF_BASEDB(string sTAFF_ID)
        {
            var sTAFF_IDParameter = sTAFF_ID != null ?
                new ObjectParameter("STAFF_ID", sTAFF_ID) :
                new ObjectParameter("STAFF_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_MEMBER_STAFF_REQUST_REF_BASEDB_Result>("SP_ADMIN_MEMBER_STAFF_REQUST_REF_BASEDB", sTAFF_IDParameter);
        }
    
        public virtual ObjectResult<SP_ADMIN_MEMBER_STAFF_REQUST_REF_REQUEST_Result> SP_ADMIN_MEMBER_STAFF_REQUST_REF_REQUEST(string sTAFF_ID)
        {
            var sTAFF_IDParameter = sTAFF_ID != null ?
                new ObjectParameter("STAFF_ID", sTAFF_ID) :
                new ObjectParameter("STAFF_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_MEMBER_STAFF_REQUST_REF_REQUEST_Result>("SP_ADMIN_MEMBER_STAFF_REQUST_REF_REQUEST", sTAFF_IDParameter);
        }
    
        public virtual ObjectResult<SP_ADMIN_MEMBER_STAFF_REQUST_REF_MEMBER_Result> SP_ADMIN_MEMBER_STAFF_REQUST_REF_MEMBER(string sTAFF_ID)
        {
            var sTAFF_IDParameter = sTAFF_ID != null ?
                new ObjectParameter("STAFF_ID", sTAFF_ID) :
                new ObjectParameter("STAFF_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_MEMBER_STAFF_REQUST_REF_MEMBER_Result>("SP_ADMIN_MEMBER_STAFF_REQUST_REF_MEMBER", sTAFF_IDParameter);
        }
    
        public virtual ObjectResult<SP_ZZ_MIGRATION_MEMBER_PWD_MD5_2_SHA256_SEL_Result> SP_ZZ_MIGRATION_MEMBER_PWD_MD5_2_SHA256_SEL()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ZZ_MIGRATION_MEMBER_PWD_MD5_2_SHA256_SEL_Result>("SP_ZZ_MIGRATION_MEMBER_PWD_MD5_2_SHA256_SEL");
        }
    
        public virtual int SP_ZZ_MIGRATION_MEMBER_PWD_MD5_2_SHA256_UPD(string m_ID, string m_PWD_SHA256)
        {
            var m_IDParameter = m_ID != null ?
                new ObjectParameter("M_ID", m_ID) :
                new ObjectParameter("M_ID", typeof(string));
    
            var m_PWD_SHA256Parameter = m_PWD_SHA256 != null ?
                new ObjectParameter("M_PWD_SHA256", m_PWD_SHA256) :
                new ObjectParameter("M_PWD_SHA256", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ZZ_MIGRATION_MEMBER_PWD_MD5_2_SHA256_UPD", m_IDParameter, m_PWD_SHA256Parameter);
        }
    
        public virtual ObjectResult<SP_ADMIN_FRONT_COMMON_CNT_Result> SP_ADMIN_MEMBER_STAFF_BASE_CNT(string dATE_FROM, string dATE_TO, string sEARCH_COL, string sEARCH_KEYWORD)
        {
            var dATE_FROMParameter = dATE_FROM != null ?
                new ObjectParameter("DATE_FROM", dATE_FROM) :
                new ObjectParameter("DATE_FROM", typeof(string));
    
            var dATE_TOParameter = dATE_TO != null ?
                new ObjectParameter("DATE_TO", dATE_TO) :
                new ObjectParameter("DATE_TO", typeof(string));
    
            var sEARCH_COLParameter = sEARCH_COL != null ?
                new ObjectParameter("SEARCH_COL", sEARCH_COL) :
                new ObjectParameter("SEARCH_COL", typeof(string));
    
            var sEARCH_KEYWORDParameter = sEARCH_KEYWORD != null ?
                new ObjectParameter("SEARCH_KEYWORD", sEARCH_KEYWORD) :
                new ObjectParameter("SEARCH_KEYWORD", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_FRONT_COMMON_CNT_Result>("SP_ADMIN_MEMBER_STAFF_BASE_CNT", dATE_FROMParameter, dATE_TOParameter, sEARCH_COLParameter, sEARCH_KEYWORDParameter);
        }
    
        public virtual int SP_ADMIN_MEMBER_STAFF_BASE_DEL(Nullable<int> iDX, ObjectParameter eRR_CODE)
        {
            var iDXParameter = iDX.HasValue ?
                new ObjectParameter("IDX", iDX) :
                new ObjectParameter("IDX", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_MEMBER_STAFF_BASE_DEL", iDXParameter, eRR_CODE);
        }
    
        public virtual int SP_ADMIN_MEMBER_STAFF_BASE_DUP_CHECK(string sTAFF_ID, ObjectParameter eRR_CODE)
        {
            var sTAFF_IDParameter = sTAFF_ID != null ?
                new ObjectParameter("STAFF_ID", sTAFF_ID) :
                new ObjectParameter("STAFF_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_MEMBER_STAFF_BASE_DUP_CHECK", sTAFF_IDParameter, eRR_CODE);
        }
    
        public virtual int SP_ADMIN_MEMBER_STAFF_BASE_INS(string sTAFF_COMPANY, string sTAFF_ID, string sTAFF_NAME, string wORK_TEMP_ID, ObjectParameter eRR_CODE)
        {
            var sTAFF_COMPANYParameter = sTAFF_COMPANY != null ?
                new ObjectParameter("STAFF_COMPANY", sTAFF_COMPANY) :
                new ObjectParameter("STAFF_COMPANY", typeof(string));
    
            var sTAFF_IDParameter = sTAFF_ID != null ?
                new ObjectParameter("STAFF_ID", sTAFF_ID) :
                new ObjectParameter("STAFF_ID", typeof(string));
    
            var sTAFF_NAMEParameter = sTAFF_NAME != null ?
                new ObjectParameter("STAFF_NAME", sTAFF_NAME) :
                new ObjectParameter("STAFF_NAME", typeof(string));
    
            var wORK_TEMP_IDParameter = wORK_TEMP_ID != null ?
                new ObjectParameter("WORK_TEMP_ID", wORK_TEMP_ID) :
                new ObjectParameter("WORK_TEMP_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_MEMBER_STAFF_BASE_INS", sTAFF_COMPANYParameter, sTAFF_IDParameter, sTAFF_NAMEParameter, wORK_TEMP_IDParameter, eRR_CODE);
        }
    
        public virtual ObjectResult<SP_ADMIN_MEMBER_STAFF_BASE_VIEW_Result> SP_ADMIN_MEMBER_STAFF_BASE_SEL(string dATE_FROM, string dATE_TO, string sEARCH_COL, string sEARCH_KEYWORD, string sORT_COL, string sORT_DIR, Nullable<int> pAGE, Nullable<int> pAGESIZE)
        {
            var dATE_FROMParameter = dATE_FROM != null ?
                new ObjectParameter("DATE_FROM", dATE_FROM) :
                new ObjectParameter("DATE_FROM", typeof(string));
    
            var dATE_TOParameter = dATE_TO != null ?
                new ObjectParameter("DATE_TO", dATE_TO) :
                new ObjectParameter("DATE_TO", typeof(string));
    
            var sEARCH_COLParameter = sEARCH_COL != null ?
                new ObjectParameter("SEARCH_COL", sEARCH_COL) :
                new ObjectParameter("SEARCH_COL", typeof(string));
    
            var sEARCH_KEYWORDParameter = sEARCH_KEYWORD != null ?
                new ObjectParameter("SEARCH_KEYWORD", sEARCH_KEYWORD) :
                new ObjectParameter("SEARCH_KEYWORD", typeof(string));
    
            var sORT_COLParameter = sORT_COL != null ?
                new ObjectParameter("SORT_COL", sORT_COL) :
                new ObjectParameter("SORT_COL", typeof(string));
    
            var sORT_DIRParameter = sORT_DIR != null ?
                new ObjectParameter("SORT_DIR", sORT_DIR) :
                new ObjectParameter("SORT_DIR", typeof(string));
    
            var pAGEParameter = pAGE.HasValue ?
                new ObjectParameter("PAGE", pAGE) :
                new ObjectParameter("PAGE", typeof(int));
    
            var pAGESIZEParameter = pAGESIZE.HasValue ?
                new ObjectParameter("PAGESIZE", pAGESIZE) :
                new ObjectParameter("PAGESIZE", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_MEMBER_STAFF_BASE_VIEW_Result>("SP_ADMIN_MEMBER_STAFF_BASE_SEL", dATE_FROMParameter, dATE_TOParameter, sEARCH_COLParameter, sEARCH_KEYWORDParameter, sORT_COLParameter, sORT_DIRParameter, pAGEParameter, pAGESIZEParameter);
        }
    
        public virtual int SP_ADMIN_MEMBER_STAFF_BASE_TMP_INS(string sTAFF_COMPANY, string sTAFF_ID, string sTAFF_NAME, string wORK_TEMP_ID, string aDM_ID, string iP, ObjectParameter eRR_CODE)
        {
            var sTAFF_COMPANYParameter = sTAFF_COMPANY != null ?
                new ObjectParameter("STAFF_COMPANY", sTAFF_COMPANY) :
                new ObjectParameter("STAFF_COMPANY", typeof(string));
    
            var sTAFF_IDParameter = sTAFF_ID != null ?
                new ObjectParameter("STAFF_ID", sTAFF_ID) :
                new ObjectParameter("STAFF_ID", typeof(string));
    
            var sTAFF_NAMEParameter = sTAFF_NAME != null ?
                new ObjectParameter("STAFF_NAME", sTAFF_NAME) :
                new ObjectParameter("STAFF_NAME", typeof(string));
    
            var wORK_TEMP_IDParameter = wORK_TEMP_ID != null ?
                new ObjectParameter("WORK_TEMP_ID", wORK_TEMP_ID) :
                new ObjectParameter("WORK_TEMP_ID", typeof(string));
    
            var aDM_IDParameter = aDM_ID != null ?
                new ObjectParameter("ADM_ID", aDM_ID) :
                new ObjectParameter("ADM_ID", typeof(string));
    
            var iPParameter = iP != null ?
                new ObjectParameter("IP", iP) :
                new ObjectParameter("IP", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_MEMBER_STAFF_BASE_TMP_INS", sTAFF_COMPANYParameter, sTAFF_IDParameter, sTAFF_NAMEParameter, wORK_TEMP_IDParameter, aDM_IDParameter, iPParameter, eRR_CODE);
        }
    
        public virtual ObjectResult<SP_ADMIN_MEMBER_STAFF_BASE_TMP_LIST_Result> SP_ADMIN_MEMBER_STAFF_BASE_TMP_LIST(string wORK_TEMP_ID)
        {
            var wORK_TEMP_IDParameter = wORK_TEMP_ID != null ?
                new ObjectParameter("WORK_TEMP_ID", wORK_TEMP_ID) :
                new ObjectParameter("WORK_TEMP_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_MEMBER_STAFF_BASE_TMP_LIST_Result>("SP_ADMIN_MEMBER_STAFF_BASE_TMP_LIST", wORK_TEMP_IDParameter);
        }
    
        public virtual ObjectResult<SP_ADMIN_MEMBER_STAFF_BASE_VIEW_Result> SP_ADMIN_MEMBER_STAFF_BASE_VIEW(string sTAFF_ID)
        {
            var sTAFF_IDParameter = sTAFF_ID != null ?
                new ObjectParameter("STAFF_ID", sTAFF_ID) :
                new ObjectParameter("STAFF_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_MEMBER_STAFF_BASE_VIEW_Result>("SP_ADMIN_MEMBER_STAFF_BASE_VIEW", sTAFF_IDParameter);
        }
    
        public virtual int SP_ADMIN_MEMBER_STAFF_BASE_TMP_UPD(Nullable<int> iDX, string aPP_RESULT)
        {
            var iDXParameter = iDX.HasValue ?
                new ObjectParameter("IDX", iDX) :
                new ObjectParameter("IDX", typeof(int));
    
            var aPP_RESULTParameter = aPP_RESULT != null ?
                new ObjectParameter("APP_RESULT", aPP_RESULT) :
                new ObjectParameter("APP_RESULT", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_MEMBER_STAFF_BASE_TMP_UPD", iDXParameter, aPP_RESULTParameter);
        }
    
        public virtual ObjectResult<SP_ADMIN_FRONT_COMMON_CNT_Result> SP_ADMIN_MEMBER_STAFF_BASE_TMP_CNT(string wORK_TEMP_ID, string aPP_RESULT)
        {
            var wORK_TEMP_IDParameter = wORK_TEMP_ID != null ?
                new ObjectParameter("WORK_TEMP_ID", wORK_TEMP_ID) :
                new ObjectParameter("WORK_TEMP_ID", typeof(string));
    
            var aPP_RESULTParameter = aPP_RESULT != null ?
                new ObjectParameter("APP_RESULT", aPP_RESULT) :
                new ObjectParameter("APP_RESULT", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_FRONT_COMMON_CNT_Result>("SP_ADMIN_MEMBER_STAFF_BASE_TMP_CNT", wORK_TEMP_IDParameter, aPP_RESULTParameter);
        }
    
        public virtual ObjectResult<SP_ADMIN_FRONT_COMMON_CNT_Result> SP_ADMIN_MEMBER_SLEEPING_CNT(string m_LASTVISITDATE, string sEARCH_COL, string sEARCH_KEYWORD)
        {
            var m_LASTVISITDATEParameter = m_LASTVISITDATE != null ?
                new ObjectParameter("M_LASTVISITDATE", m_LASTVISITDATE) :
                new ObjectParameter("M_LASTVISITDATE", typeof(string));
    
            var sEARCH_COLParameter = sEARCH_COL != null ?
                new ObjectParameter("SEARCH_COL", sEARCH_COL) :
                new ObjectParameter("SEARCH_COL", typeof(string));
    
            var sEARCH_KEYWORDParameter = sEARCH_KEYWORD != null ?
                new ObjectParameter("SEARCH_KEYWORD", sEARCH_KEYWORD) :
                new ObjectParameter("SEARCH_KEYWORD", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_FRONT_COMMON_CNT_Result>("SP_ADMIN_MEMBER_SLEEPING_CNT", m_LASTVISITDATEParameter, sEARCH_COLParameter, sEARCH_KEYWORDParameter);
        }
    
        public virtual ObjectResult<SP_ADMIN_FRONT_MEMBER_VIEW_Result> SP_ADMIN_MEMBER_SLEEPING_SEL(string m_LASTVISITDATE, string sEARCH_COL, string sEARCH_KEYWORD, string sORT_COL, string sORT_DIR, Nullable<int> pAGE, Nullable<int> pAGESIZE)
        {
            var m_LASTVISITDATEParameter = m_LASTVISITDATE != null ?
                new ObjectParameter("M_LASTVISITDATE", m_LASTVISITDATE) :
                new ObjectParameter("M_LASTVISITDATE", typeof(string));
    
            var sEARCH_COLParameter = sEARCH_COL != null ?
                new ObjectParameter("SEARCH_COL", sEARCH_COL) :
                new ObjectParameter("SEARCH_COL", typeof(string));
    
            var sEARCH_KEYWORDParameter = sEARCH_KEYWORD != null ?
                new ObjectParameter("SEARCH_KEYWORD", sEARCH_KEYWORD) :
                new ObjectParameter("SEARCH_KEYWORD", typeof(string));
    
            var sORT_COLParameter = sORT_COL != null ?
                new ObjectParameter("SORT_COL", sORT_COL) :
                new ObjectParameter("SORT_COL", typeof(string));
    
            var sORT_DIRParameter = sORT_DIR != null ?
                new ObjectParameter("SORT_DIR", sORT_DIR) :
                new ObjectParameter("SORT_DIR", typeof(string));
    
            var pAGEParameter = pAGE.HasValue ?
                new ObjectParameter("PAGE", pAGE) :
                new ObjectParameter("PAGE", typeof(int));
    
            var pAGESIZEParameter = pAGESIZE.HasValue ?
                new ObjectParameter("PAGESIZE", pAGESIZE) :
                new ObjectParameter("PAGESIZE", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_FRONT_MEMBER_VIEW_Result>("SP_ADMIN_MEMBER_SLEEPING_SEL", m_LASTVISITDATEParameter, sEARCH_COLParameter, sEARCH_KEYWORDParameter, sORT_COLParameter, sORT_DIRParameter, pAGEParameter, pAGESIZEParameter);
        }
    
        public virtual ObjectResult<SP_ADMIN_MEMBER_REPORT_GRADE_Result> SP_ADMIN_MEMBER_REPORT_GRADE()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_MEMBER_REPORT_GRADE_Result>("SP_ADMIN_MEMBER_REPORT_GRADE");
        }
    
        public virtual ObjectResult<SP_ADMIN_MEMBER_REPORT_MONTHLY_Result> SP_ADMIN_MEMBER_REPORT_MONTHLY(string yEAR)
        {
            var yEARParameter = yEAR != null ?
                new ObjectParameter("YEAR", yEAR) :
                new ObjectParameter("YEAR", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_MEMBER_REPORT_MONTHLY_Result>("SP_ADMIN_MEMBER_REPORT_MONTHLY", yEARParameter);
        }
    }
}
