﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AboutMe.Domain.Entity.Product
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ProductEntities : DbContext
    {
        public ProductEntities()
            : base("name=ProductEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TB_LOG_USER> TB_LOG_USER { get; set; }
    
        public virtual ObjectResult<SP_PRODUCT_CNT_Result> SP_PRODUCT_CNT()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_PRODUCT_CNT_Result>("SP_PRODUCT_CNT");
        }
    
        public virtual ObjectResult<SP_PRODUCT_SEL_Result> SP_PRODUCT_SEL()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_PRODUCT_SEL_Result>("SP_PRODUCT_SEL");
        }
    
        public virtual ObjectResult<SP_PRODUCT_DETAIL_VIEW_Result> SP_PRODUCT_DETAIL_VIEW(string p_CODE)
        {
            var p_CODEParameter = p_CODE != null ?
                new ObjectParameter("P_CODE", p_CODE) :
                new ObjectParameter("P_CODE", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_PRODUCT_DETAIL_VIEW_Result>("SP_PRODUCT_DETAIL_VIEW", p_CODEParameter);
        }
    
        public virtual int SP_USER_LOG_INS(string uid, string memo, string comment, string url, string userip)
        {
            var uidParameter = uid != null ?
                new ObjectParameter("uid", uid) :
                new ObjectParameter("uid", typeof(string));
    
            var memoParameter = memo != null ?
                new ObjectParameter("memo", memo) :
                new ObjectParameter("memo", typeof(string));
    
            var commentParameter = comment != null ?
                new ObjectParameter("comment", comment) :
                new ObjectParameter("comment", typeof(string));
    
            var urlParameter = url != null ?
                new ObjectParameter("url", url) :
                new ObjectParameter("url", typeof(string));
    
            var useripParameter = userip != null ?
                new ObjectParameter("userip", userip) :
                new ObjectParameter("userip", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_USER_LOG_INS", uidParameter, memoParameter, commentParameter, urlParameter, useripParameter);
        }
    
        public virtual ObjectResult<SP_CATEGORY_DEPTH_SEL_Result> SP_CATEGORY_DEPTH_SEL(string cATE_GBN, string dEPTH1_CODE, string dEPTH2_CODE)
        {
            var cATE_GBNParameter = cATE_GBN != null ?
                new ObjectParameter("CATE_GBN", cATE_GBN) :
                new ObjectParameter("CATE_GBN", typeof(string));
    
            var dEPTH1_CODEParameter = dEPTH1_CODE != null ?
                new ObjectParameter("DEPTH1_CODE", dEPTH1_CODE) :
                new ObjectParameter("DEPTH1_CODE", typeof(string));
    
            var dEPTH2_CODEParameter = dEPTH2_CODE != null ?
                new ObjectParameter("DEPTH2_CODE", dEPTH2_CODE) :
                new ObjectParameter("DEPTH2_CODE", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_CATEGORY_DEPTH_SEL_Result>("SP_CATEGORY_DEPTH_SEL", cATE_GBNParameter, dEPTH1_CODEParameter, dEPTH2_CODEParameter);
        }
    }
}
