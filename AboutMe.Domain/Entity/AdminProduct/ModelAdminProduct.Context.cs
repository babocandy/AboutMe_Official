﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AboutMe.Domain.Entity.AdminProduct
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;

    public partial class AdminProductEntities : DbContext
    {
        public AdminProductEntities()
            : base("name=AdminProductEntities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<TB_CATEGORY> TB_CATEGORY { get; set; }
        public virtual DbSet<TB_PRODUCT_INFO> TB_PRODUCT_INFO { get; set; }

        public virtual ObjectResult<SP_ADMIN_CATEGORY_NAME_SEL_Result> SP_ADMIN_CATEGORY_NAME_SEL(string dEPTH1_CODE, string cATE_GBN, string lIST_GBN)
        {
            var dEPTH1_CODEParameter = dEPTH1_CODE != null ?
                new ObjectParameter("DEPTH1_CODE", dEPTH1_CODE) :
                new ObjectParameter("DEPTH1_CODE", typeof(string));

            var cATE_GBNParameter = cATE_GBN != null ?
                new ObjectParameter("CATE_GBN", cATE_GBN) :
                new ObjectParameter("CATE_GBN", typeof(string));

            var lIST_GBNParameter = lIST_GBN != null ?
                new ObjectParameter("LIST_GBN", lIST_GBN) :
                new ObjectParameter("LIST_GBN", typeof(string));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_CATEGORY_NAME_SEL_Result>("SP_ADMIN_CATEGORY_NAME_SEL", dEPTH1_CODEParameter, cATE_GBNParameter, lIST_GBNParameter);
        }

        public virtual int SP_ADMIN_CATEGORY_ONE_DEL(Nullable<int> iDX)
        {
            var iDXParameter = iDX.HasValue ?
                new ObjectParameter("IDX", iDX) :
                new ObjectParameter("IDX", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_CATEGORY_ONE_DEL", iDXParameter);
        }

        public virtual ObjectResult<SP_ADMIN_CATEGORY_ONE_SEL_Result> SP_ADMIN_CATEGORY_ONE_SEL()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_CATEGORY_ONE_SEL_Result>("SP_ADMIN_CATEGORY_ONE_SEL");
        }

        public virtual int SP_ADMIN_CATEGORY_ONE_UPD(Nullable<int> iDX, string dEPTH1_NAME, string dISPLAY_YN, Nullable<int> rE_SORT)
        {
            var iDXParameter = iDX.HasValue ?
                new ObjectParameter("IDX", iDX) :
                new ObjectParameter("IDX", typeof(int));

            var dEPTH1_NAMEParameter = dEPTH1_NAME != null ?
                new ObjectParameter("DEPTH1_NAME", dEPTH1_NAME) :
                new ObjectParameter("DEPTH1_NAME", typeof(string));

            var dISPLAY_YNParameter = dISPLAY_YN != null ?
                new ObjectParameter("DISPLAY_YN", dISPLAY_YN) :
                new ObjectParameter("DISPLAY_YN", typeof(string));

            var rE_SORTParameter = rE_SORT.HasValue ?
                new ObjectParameter("RE_SORT", rE_SORT) :
                new ObjectParameter("RE_SORT", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_CATEGORY_ONE_UPD", iDXParameter, dEPTH1_NAMEParameter, dISPLAY_YNParameter, rE_SORTParameter);
        }

        public virtual ObjectResult<SP_ADMIN_CATEGORY_TWO_SEL_Result> SP_ADMIN_CATEGORY_TWO_SEL(string cATE_GBN, string dEPTH1_CODE)
        {
            var cATE_GBNParameter = cATE_GBN != null ?
                new ObjectParameter("CATE_GBN", cATE_GBN) :
                new ObjectParameter("CATE_GBN", typeof(string));

            var dEPTH1_CODEParameter = dEPTH1_CODE != null ?
                new ObjectParameter("DEPTH1_CODE", dEPTH1_CODE) :
                new ObjectParameter("DEPTH1_CODE", typeof(string));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_CATEGORY_TWO_SEL_Result>("SP_ADMIN_CATEGORY_TWO_SEL", cATE_GBNParameter, dEPTH1_CODEParameter);
        }

        public virtual ObjectResult<SP_ADMIN_CATEGORY_VIEW_Result> SP_ADMIN_CATEGORY_VIEW(Nullable<int> iDX)
        {
            var iDXParameter = iDX.HasValue ?
                new ObjectParameter("IDX", iDX) :
                new ObjectParameter("IDX", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_CATEGORY_VIEW_Result>("SP_ADMIN_CATEGORY_VIEW", iDXParameter);
        }

        public virtual ObjectResult<SP_ADMIN_CATEGORY_DEPTH_SEL_Result> SP_ADMIN_CATEGORY_DEPTH_SEL(string cATE_GBN, string dEPTH1_CODE, string dEPTH2_CODE)
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

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_CATEGORY_DEPTH_SEL_Result>("SP_ADMIN_CATEGORY_DEPTH_SEL", cATE_GBNParameter, dEPTH1_CODEParameter, dEPTH2_CODEParameter);
        }

        public virtual ObjectResult<SP_ADMIN_CATEGORY_DEPTH_SEL_ALL_Result> SP_ADMIN_CATEGORY_DEPTH_SEL_ALL(string cATE_GBN, string dEPTH1_CODE, string dEPTH2_CODE)
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

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_CATEGORY_DEPTH_SEL_ALL_Result>("SP_ADMIN_CATEGORY_DEPTH_SEL_ALL", cATE_GBNParameter, dEPTH1_CODEParameter, dEPTH2_CODEParameter);
        }

        public virtual int SP_ADMIN_CATEGORY_THREE_UPD(Nullable<int> iDX, string dEPTH2_NAME, string dISPLAY_YN, Nullable<int> rE_SORT)
        {
            var iDXParameter = iDX.HasValue ?
                new ObjectParameter("IDX", iDX) :
                new ObjectParameter("IDX", typeof(int));

            var dEPTH2_NAMEParameter = dEPTH2_NAME != null ?
                new ObjectParameter("DEPTH2_NAME", dEPTH2_NAME) :
                new ObjectParameter("DEPTH2_NAME", typeof(string));

            var dISPLAY_YNParameter = dISPLAY_YN != null ?
                new ObjectParameter("DISPLAY_YN", dISPLAY_YN) :
                new ObjectParameter("DISPLAY_YN", typeof(string));

            var rE_SORTParameter = rE_SORT.HasValue ?
                new ObjectParameter("RE_SORT", rE_SORT) :
                new ObjectParameter("RE_SORT", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_CATEGORY_THREE_UPD", iDXParameter, dEPTH2_NAMEParameter, dISPLAY_YNParameter, rE_SORTParameter);
        }

        public virtual int SP_ADMIN_CATEGORY_TWO_UPD(Nullable<int> iDX, string dEPTH2_NAME, string dISPLAY_YN, Nullable<int> rE_SORT)
        {
            var iDXParameter = iDX.HasValue ?
                new ObjectParameter("IDX", iDX) :
                new ObjectParameter("IDX", typeof(int));

            var dEPTH2_NAMEParameter = dEPTH2_NAME != null ?
                new ObjectParameter("DEPTH2_NAME", dEPTH2_NAME) :
                new ObjectParameter("DEPTH2_NAME", typeof(string));

            var dISPLAY_YNParameter = dISPLAY_YN != null ?
                new ObjectParameter("DISPLAY_YN", dISPLAY_YN) :
                new ObjectParameter("DISPLAY_YN", typeof(string));

            var rE_SORTParameter = rE_SORT.HasValue ?
                new ObjectParameter("RE_SORT", rE_SORT) :
                new ObjectParameter("RE_SORT", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_CATEGORY_TWO_UPD", iDXParameter, dEPTH2_NAMEParameter, dISPLAY_YNParameter, rE_SORTParameter);
        }

        public virtual int SP_ADMIN_CATEGORY_ONE_INS(string dEPTH1_NAME)
        {
            var dEPTH1_NAMEParameter = dEPTH1_NAME != null ?
                new ObjectParameter("DEPTH1_NAME", dEPTH1_NAME) :
                new ObjectParameter("DEPTH1_NAME", typeof(string));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_CATEGORY_ONE_INS", dEPTH1_NAMEParameter);
        }

        public virtual int SP_ADMIN_CATEGORY_THREE_INS(string cATE_GBN, string dEPTH1_CODE, string dEPTH2_CODE, string dEPTH3_NAME)
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

            var dEPTH3_NAMEParameter = dEPTH3_NAME != null ?
                new ObjectParameter("DEPTH3_NAME", dEPTH3_NAME) :
                new ObjectParameter("DEPTH3_NAME", typeof(string));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_CATEGORY_THREE_INS", cATE_GBNParameter, dEPTH1_CODEParameter, dEPTH2_CODEParameter, dEPTH3_NAMEParameter);
        }

        public virtual int SP_ADMIN_CATEGORY_TWO_INS(string cATE_GBN, string dEPTH1_CODE, string dEPTH2_NAME)
        {
            var cATE_GBNParameter = cATE_GBN != null ?
                new ObjectParameter("CATE_GBN", cATE_GBN) :
                new ObjectParameter("CATE_GBN", typeof(string));

            var dEPTH1_CODEParameter = dEPTH1_CODE != null ?
                new ObjectParameter("DEPTH1_CODE", dEPTH1_CODE) :
                new ObjectParameter("DEPTH1_CODE", typeof(string));

            var dEPTH2_NAMEParameter = dEPTH2_NAME != null ?
                new ObjectParameter("DEPTH2_NAME", dEPTH2_NAME) :
                new ObjectParameter("DEPTH2_NAME", typeof(string));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_CATEGORY_TWO_INS", cATE_GBNParameter, dEPTH1_CODEParameter, dEPTH2_NAMEParameter);
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

        public virtual int SP_ADMIN_LOG_INS(string uid, string memo, string comment, string url, string userip, string userAgent, string urlReFerrer)
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

            var userAgentParameter = userAgent != null ?
                new ObjectParameter("userAgent", userAgent) :
                new ObjectParameter("userAgent", typeof(string));

            var urlReFerrerParameter = urlReFerrer != null ?
                new ObjectParameter("urlReFerrer", urlReFerrer) :
                new ObjectParameter("urlReFerrer", typeof(string));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_LOG_INS", uidParameter, memoParameter, commentParameter, urlParameter, useripParameter, userAgentParameter, urlReFerrerParameter);
        }

        public virtual ObjectResult<SP_ADMIN_PRODUCT_CNT_Result> SP_ADMIN_PRODUCT_CNT(string sEARCH_KEY, string sEARCH_KEYWORD, string cATE_CODE, string iCON_YN, string sEARCH_DISPLAY_Y, string sEARCH_DISPLAY_N, string sOLDOUT_YN, string p_OUTLET_YN, string fROM_DATE, string tO_DATE)
        {
            var sEARCH_KEYParameter = sEARCH_KEY != null ?
                new ObjectParameter("SEARCH_KEY", sEARCH_KEY) :
                new ObjectParameter("SEARCH_KEY", typeof(string));

            var sEARCH_KEYWORDParameter = sEARCH_KEYWORD != null ?
                new ObjectParameter("SEARCH_KEYWORD", sEARCH_KEYWORD) :
                new ObjectParameter("SEARCH_KEYWORD", typeof(string));

            var cATE_CODEParameter = cATE_CODE != null ?
                new ObjectParameter("CATE_CODE", cATE_CODE) :
                new ObjectParameter("CATE_CODE", typeof(string));

            var iCON_YNParameter = iCON_YN != null ?
                new ObjectParameter("ICON_YN", iCON_YN) :
                new ObjectParameter("ICON_YN", typeof(string));

            var sEARCH_DISPLAY_YParameter = sEARCH_DISPLAY_Y != null ?
                new ObjectParameter("SEARCH_DISPLAY_Y", sEARCH_DISPLAY_Y) :
                new ObjectParameter("SEARCH_DISPLAY_Y", typeof(string));

            var sEARCH_DISPLAY_NParameter = sEARCH_DISPLAY_N != null ?
                new ObjectParameter("SEARCH_DISPLAY_N", sEARCH_DISPLAY_N) :
                new ObjectParameter("SEARCH_DISPLAY_N", typeof(string));

            var sOLDOUT_YNParameter = sOLDOUT_YN != null ?
                new ObjectParameter("SOLDOUT_YN", sOLDOUT_YN) :
                new ObjectParameter("SOLDOUT_YN", typeof(string));

            var p_OUTLET_YNParameter = p_OUTLET_YN != null ?
                new ObjectParameter("P_OUTLET_YN", p_OUTLET_YN) :
                new ObjectParameter("P_OUTLET_YN", typeof(string));

            var fROM_DATEParameter = fROM_DATE != null ?
                new ObjectParameter("FROM_DATE", fROM_DATE) :
                new ObjectParameter("FROM_DATE", typeof(string));

            var tO_DATEParameter = tO_DATE != null ?
                new ObjectParameter("TO_DATE", tO_DATE) :
                new ObjectParameter("TO_DATE", typeof(string));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_PRODUCT_CNT_Result>("SP_ADMIN_PRODUCT_CNT", sEARCH_KEYParameter, sEARCH_KEYWORDParameter, cATE_CODEParameter, iCON_YNParameter, sEARCH_DISPLAY_YParameter, sEARCH_DISPLAY_NParameter, sOLDOUT_YNParameter, p_OUTLET_YNParameter, fROM_DATEParameter, tO_DATEParameter);
        }

        public virtual ObjectResult<SP_ADMIN_PRODUCT_DETAIL_VIEW_Result> SP_ADMIN_PRODUCT_DETAIL_VIEW(string p_CODE)
        {
            var p_CODEParameter = p_CODE != null ?
                new ObjectParameter("P_CODE", p_CODE) :
                new ObjectParameter("P_CODE", typeof(string));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_PRODUCT_DETAIL_VIEW_Result>("SP_ADMIN_PRODUCT_DETAIL_VIEW", p_CODEParameter);
        }

        public virtual int SP_ADMIN_PRODUCT_DISPLAY_RE_SORT(Nullable<int> iDX, Nullable<int> rE_SORT, string cLICKCHK)
        {
            var iDXParameter = iDX.HasValue ?
                new ObjectParameter("IDX", iDX) :
                new ObjectParameter("IDX", typeof(int));

            var rE_SORTParameter = rE_SORT.HasValue ?
                new ObjectParameter("RE_SORT", rE_SORT) :
                new ObjectParameter("RE_SORT", typeof(int));

            var cLICKCHKParameter = cLICKCHK != null ?
                new ObjectParameter("CLICKCHK", cLICKCHK) :
                new ObjectParameter("CLICKCHK", typeof(string));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_PRODUCT_DISPLAY_RE_SORT", iDXParameter, rE_SORTParameter, cLICKCHKParameter);
        }

        public virtual int SP_ADMIN_PRODUCT_IMG_DEL(string p_CODE, string imgColumName)
        {
            var p_CODEParameter = p_CODE != null ?
                new ObjectParameter("P_CODE", p_CODE) :
                new ObjectParameter("P_CODE", typeof(string));

            var imgColumNameParameter = imgColumName != null ?
                new ObjectParameter("imgColumName", imgColumName) :
                new ObjectParameter("imgColumName", typeof(string));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_PRODUCT_IMG_DEL", p_CODEParameter, imgColumNameParameter);
        }

        public virtual int SP_ADMIN_PRODUCT_INS(string p_CATE_CODE, string c_CATE_CODE, string l_CATE_CODE, string p_CODE, string p_NAME, string p_SUB_TITLE, Nullable<int> p_COUNT, Nullable<int> sELLING_PRICE, Nullable<int> dISCOUNT_RATE, Nullable<int> dISCOUNT_PRICE, string p_INFO_DETAIL_WEB, string p_INFO_DETAIL_MOBILE, string mV_URL, string p_COMPONENT_INFO, string p_TAG, string mAIN_IMG, string oTHER_IMG1, string oTHER_IMG2, string oTHER_IMG3, string iCON_YN, string wITH_PRODUCT_LIST)
        {
            var p_CATE_CODEParameter = p_CATE_CODE != null ?
                new ObjectParameter("P_CATE_CODE", p_CATE_CODE) :
                new ObjectParameter("P_CATE_CODE", typeof(string));

            var c_CATE_CODEParameter = c_CATE_CODE != null ?
                new ObjectParameter("C_CATE_CODE", c_CATE_CODE) :
                new ObjectParameter("C_CATE_CODE", typeof(string));

            var l_CATE_CODEParameter = l_CATE_CODE != null ?
                new ObjectParameter("L_CATE_CODE", l_CATE_CODE) :
                new ObjectParameter("L_CATE_CODE", typeof(string));

            var p_CODEParameter = p_CODE != null ?
                new ObjectParameter("P_CODE", p_CODE) :
                new ObjectParameter("P_CODE", typeof(string));

            var p_NAMEParameter = p_NAME != null ?
                new ObjectParameter("P_NAME", p_NAME) :
                new ObjectParameter("P_NAME", typeof(string));

            var p_SUB_TITLEParameter = p_SUB_TITLE != null ?
                new ObjectParameter("P_SUB_TITLE", p_SUB_TITLE) :
                new ObjectParameter("P_SUB_TITLE", typeof(string));

            var p_COUNTParameter = p_COUNT.HasValue ?
                new ObjectParameter("P_COUNT", p_COUNT) :
                new ObjectParameter("P_COUNT", typeof(int));

            var sELLING_PRICEParameter = sELLING_PRICE.HasValue ?
                new ObjectParameter("SELLING_PRICE", sELLING_PRICE) :
                new ObjectParameter("SELLING_PRICE", typeof(int));

            var dISCOUNT_RATEParameter = dISCOUNT_RATE.HasValue ?
                new ObjectParameter("DISCOUNT_RATE", dISCOUNT_RATE) :
                new ObjectParameter("DISCOUNT_RATE", typeof(int));

            var dISCOUNT_PRICEParameter = dISCOUNT_PRICE.HasValue ?
                new ObjectParameter("DISCOUNT_PRICE", dISCOUNT_PRICE) :
                new ObjectParameter("DISCOUNT_PRICE", typeof(int));

            var p_INFO_DETAIL_WEBParameter = p_INFO_DETAIL_WEB != null ?
                new ObjectParameter("P_INFO_DETAIL_WEB", p_INFO_DETAIL_WEB) :
                new ObjectParameter("P_INFO_DETAIL_WEB", typeof(string));

            var p_INFO_DETAIL_MOBILEParameter = p_INFO_DETAIL_MOBILE != null ?
                new ObjectParameter("P_INFO_DETAIL_MOBILE", p_INFO_DETAIL_MOBILE) :
                new ObjectParameter("P_INFO_DETAIL_MOBILE", typeof(string));

            var mV_URLParameter = mV_URL != null ?
                new ObjectParameter("MV_URL", mV_URL) :
                new ObjectParameter("MV_URL", typeof(string));

            var p_COMPONENT_INFOParameter = p_COMPONENT_INFO != null ?
                new ObjectParameter("P_COMPONENT_INFO", p_COMPONENT_INFO) :
                new ObjectParameter("P_COMPONENT_INFO", typeof(string));

            var p_TAGParameter = p_TAG != null ?
                new ObjectParameter("P_TAG", p_TAG) :
                new ObjectParameter("P_TAG", typeof(string));

            var mAIN_IMGParameter = mAIN_IMG != null ?
                new ObjectParameter("MAIN_IMG", mAIN_IMG) :
                new ObjectParameter("MAIN_IMG", typeof(string));

            var oTHER_IMG1Parameter = oTHER_IMG1 != null ?
                new ObjectParameter("OTHER_IMG1", oTHER_IMG1) :
                new ObjectParameter("OTHER_IMG1", typeof(string));

            var oTHER_IMG2Parameter = oTHER_IMG2 != null ?
                new ObjectParameter("OTHER_IMG2", oTHER_IMG2) :
                new ObjectParameter("OTHER_IMG2", typeof(string));

            var oTHER_IMG3Parameter = oTHER_IMG3 != null ?
                new ObjectParameter("OTHER_IMG3", oTHER_IMG3) :
                new ObjectParameter("OTHER_IMG3", typeof(string));

            var iCON_YNParameter = iCON_YN != null ?
                new ObjectParameter("ICON_YN", iCON_YN) :
                new ObjectParameter("ICON_YN", typeof(string));

            var wITH_PRODUCT_LISTParameter = wITH_PRODUCT_LIST != null ?
                new ObjectParameter("WITH_PRODUCT_LIST", wITH_PRODUCT_LIST) :
                new ObjectParameter("WITH_PRODUCT_LIST", typeof(string));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_PRODUCT_INS", p_CATE_CODEParameter, c_CATE_CODEParameter, l_CATE_CODEParameter, p_CODEParameter, p_NAMEParameter, p_SUB_TITLEParameter, p_COUNTParameter, sELLING_PRICEParameter, dISCOUNT_RATEParameter, dISCOUNT_PRICEParameter, p_INFO_DETAIL_WEBParameter, p_INFO_DETAIL_MOBILEParameter, mV_URLParameter, p_COMPONENT_INFOParameter, p_TAGParameter, mAIN_IMGParameter, oTHER_IMG1Parameter, oTHER_IMG2Parameter, oTHER_IMG3Parameter, iCON_YNParameter, wITH_PRODUCT_LISTParameter);
        }

        public virtual ObjectResult<Nullable<int>> SP_ADMIN_PRODUCT_PCODE_CHK(string p_CODE)
        {
            var p_CODEParameter = p_CODE != null ?
                new ObjectParameter("P_CODE", p_CODE) :
                new ObjectParameter("P_CODE", typeof(string));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("SP_ADMIN_PRODUCT_PCODE_CHK", p_CODEParameter);
        }

        public virtual int SP_ADMIN_PRODUCT_PRICE_UPD(string p_CODE, Nullable<int> sELLING_PRICE, Nullable<int> dISCOUNT_PRICE, Nullable<int> dISCOUNT_RATE)
        {
            var p_CODEParameter = p_CODE != null ?
                new ObjectParameter("P_CODE", p_CODE) :
                new ObjectParameter("P_CODE", typeof(string));

            var sELLING_PRICEParameter = sELLING_PRICE.HasValue ?
                new ObjectParameter("SELLING_PRICE", sELLING_PRICE) :
                new ObjectParameter("SELLING_PRICE", typeof(int));

            var dISCOUNT_PRICEParameter = dISCOUNT_PRICE.HasValue ?
                new ObjectParameter("DISCOUNT_PRICE", dISCOUNT_PRICE) :
                new ObjectParameter("DISCOUNT_PRICE", typeof(int));

            var dISCOUNT_RATEParameter = dISCOUNT_RATE.HasValue ?
                new ObjectParameter("DISCOUNT_RATE", dISCOUNT_RATE) :
                new ObjectParameter("DISCOUNT_RATE", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_PRODUCT_PRICE_UPD", p_CODEParameter, sELLING_PRICEParameter, dISCOUNT_PRICEParameter, dISCOUNT_RATEParameter);
        }

        public virtual ObjectResult<SP_ADMIN_PRODUCT_LIST_Result> SP_ADMIN_PRODUCT_SEL(Nullable<int> pAGE, Nullable<int> pAGESIZE, string sEARCH_KEY, string sEARCH_KEYWORD, string cATE_CODE, string iCON_YN, string sEARCH_DISPLAY_Y, string sEARCH_DISPLAY_N, string sOLDOUT_YN, string p_OUTLET_YN, string fROM_DATE, string tO_DATE)
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

            var cATE_CODEParameter = cATE_CODE != null ?
                new ObjectParameter("CATE_CODE", cATE_CODE) :
                new ObjectParameter("CATE_CODE", typeof(string));

            var iCON_YNParameter = iCON_YN != null ?
                new ObjectParameter("ICON_YN", iCON_YN) :
                new ObjectParameter("ICON_YN", typeof(string));

            var sEARCH_DISPLAY_YParameter = sEARCH_DISPLAY_Y != null ?
                new ObjectParameter("SEARCH_DISPLAY_Y", sEARCH_DISPLAY_Y) :
                new ObjectParameter("SEARCH_DISPLAY_Y", typeof(string));

            var sEARCH_DISPLAY_NParameter = sEARCH_DISPLAY_N != null ?
                new ObjectParameter("SEARCH_DISPLAY_N", sEARCH_DISPLAY_N) :
                new ObjectParameter("SEARCH_DISPLAY_N", typeof(string));

            var sOLDOUT_YNParameter = sOLDOUT_YN != null ?
                new ObjectParameter("SOLDOUT_YN", sOLDOUT_YN) :
                new ObjectParameter("SOLDOUT_YN", typeof(string));

            var p_OUTLET_YNParameter = p_OUTLET_YN != null ?
                new ObjectParameter("P_OUTLET_YN", p_OUTLET_YN) :
                new ObjectParameter("P_OUTLET_YN", typeof(string));

            var fROM_DATEParameter = fROM_DATE != null ?
                new ObjectParameter("FROM_DATE", fROM_DATE) :
                new ObjectParameter("FROM_DATE", typeof(string));

            var tO_DATEParameter = tO_DATE != null ?
                new ObjectParameter("TO_DATE", tO_DATE) :
                new ObjectParameter("TO_DATE", typeof(string));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ADMIN_PRODUCT_LIST_Result>("SP_ADMIN_PRODUCT_SEL", pAGEParameter, pAGESIZEParameter, sEARCH_KEYParameter, sEARCH_KEYWORDParameter, cATE_CODEParameter, iCON_YNParameter, sEARCH_DISPLAY_YParameter, sEARCH_DISPLAY_NParameter, sOLDOUT_YNParameter, p_OUTLET_YNParameter, fROM_DATEParameter, tO_DATEParameter);
        }

        public virtual int SP_ADMIN_PRODUCT_UPD(Nullable<int> iDX, string p_CATE_CODE, string c_CATE_CODE, string l_CATE_CODE, string p_NAME, string p_SUB_TITLE, Nullable<int> p_COUNT, Nullable<int> sELLING_PRICE, Nullable<int> dISCOUNT_RATE, Nullable<int> dISCOUNT_PRICE, string sOLDOUT_YN, string p_INFO_DETAIL_WEB, string p_INFO_DETAIL_MOBILE, string mV_URL, string p_COMPONENT_INFO, string p_TAG, string mAIN_IMG, string oTHER_IMG1, string oTHER_IMG2, string oTHER_IMG3, string iCON_YN, string wITH_PRODUCT_LIST)
        {
            var iDXParameter = iDX.HasValue ?
                new ObjectParameter("IDX", iDX) :
                new ObjectParameter("IDX", typeof(int));

            var p_CATE_CODEParameter = p_CATE_CODE != null ?
                new ObjectParameter("P_CATE_CODE", p_CATE_CODE) :
                new ObjectParameter("P_CATE_CODE", typeof(string));

            var c_CATE_CODEParameter = c_CATE_CODE != null ?
                new ObjectParameter("C_CATE_CODE", c_CATE_CODE) :
                new ObjectParameter("C_CATE_CODE", typeof(string));

            var l_CATE_CODEParameter = l_CATE_CODE != null ?
                new ObjectParameter("L_CATE_CODE", l_CATE_CODE) :
                new ObjectParameter("L_CATE_CODE", typeof(string));

            var p_NAMEParameter = p_NAME != null ?
                new ObjectParameter("P_NAME", p_NAME) :
                new ObjectParameter("P_NAME", typeof(string));

            var p_SUB_TITLEParameter = p_SUB_TITLE != null ?
                new ObjectParameter("P_SUB_TITLE", p_SUB_TITLE) :
                new ObjectParameter("P_SUB_TITLE", typeof(string));

            var p_COUNTParameter = p_COUNT.HasValue ?
                new ObjectParameter("P_COUNT", p_COUNT) :
                new ObjectParameter("P_COUNT", typeof(int));

            var sELLING_PRICEParameter = sELLING_PRICE.HasValue ?
                new ObjectParameter("SELLING_PRICE", sELLING_PRICE) :
                new ObjectParameter("SELLING_PRICE", typeof(int));

            var dISCOUNT_RATEParameter = dISCOUNT_RATE.HasValue ?
                new ObjectParameter("DISCOUNT_RATE", dISCOUNT_RATE) :
                new ObjectParameter("DISCOUNT_RATE", typeof(int));

            var dISCOUNT_PRICEParameter = dISCOUNT_PRICE.HasValue ?
                new ObjectParameter("DISCOUNT_PRICE", dISCOUNT_PRICE) :
                new ObjectParameter("DISCOUNT_PRICE", typeof(int));

            var sOLDOUT_YNParameter = sOLDOUT_YN != null ?
                new ObjectParameter("SOLDOUT_YN", sOLDOUT_YN) :
                new ObjectParameter("SOLDOUT_YN", typeof(string));

            var p_INFO_DETAIL_WEBParameter = p_INFO_DETAIL_WEB != null ?
                new ObjectParameter("P_INFO_DETAIL_WEB", p_INFO_DETAIL_WEB) :
                new ObjectParameter("P_INFO_DETAIL_WEB", typeof(string));

            var p_INFO_DETAIL_MOBILEParameter = p_INFO_DETAIL_MOBILE != null ?
                new ObjectParameter("P_INFO_DETAIL_MOBILE", p_INFO_DETAIL_MOBILE) :
                new ObjectParameter("P_INFO_DETAIL_MOBILE", typeof(string));

            var mV_URLParameter = mV_URL != null ?
                new ObjectParameter("MV_URL", mV_URL) :
                new ObjectParameter("MV_URL", typeof(string));

            var p_COMPONENT_INFOParameter = p_COMPONENT_INFO != null ?
                new ObjectParameter("P_COMPONENT_INFO", p_COMPONENT_INFO) :
                new ObjectParameter("P_COMPONENT_INFO", typeof(string));

            var p_TAGParameter = p_TAG != null ?
                new ObjectParameter("P_TAG", p_TAG) :
                new ObjectParameter("P_TAG", typeof(string));

            var mAIN_IMGParameter = mAIN_IMG != null ?
                new ObjectParameter("MAIN_IMG", mAIN_IMG) :
                new ObjectParameter("MAIN_IMG", typeof(string));

            var oTHER_IMG1Parameter = oTHER_IMG1 != null ?
                new ObjectParameter("OTHER_IMG1", oTHER_IMG1) :
                new ObjectParameter("OTHER_IMG1", typeof(string));

            var oTHER_IMG2Parameter = oTHER_IMG2 != null ?
                new ObjectParameter("OTHER_IMG2", oTHER_IMG2) :
                new ObjectParameter("OTHER_IMG2", typeof(string));

            var oTHER_IMG3Parameter = oTHER_IMG3 != null ?
                new ObjectParameter("OTHER_IMG3", oTHER_IMG3) :
                new ObjectParameter("OTHER_IMG3", typeof(string));

            var iCON_YNParameter = iCON_YN != null ?
                new ObjectParameter("ICON_YN", iCON_YN) :
                new ObjectParameter("ICON_YN", typeof(string));

            var wITH_PRODUCT_LISTParameter = wITH_PRODUCT_LIST != null ?
                new ObjectParameter("WITH_PRODUCT_LIST", wITH_PRODUCT_LIST) :
                new ObjectParameter("WITH_PRODUCT_LIST", typeof(string));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_PRODUCT_UPD", iDXParameter, p_CATE_CODEParameter, c_CATE_CODEParameter, l_CATE_CODEParameter, p_NAMEParameter, p_SUB_TITLEParameter, p_COUNTParameter, sELLING_PRICEParameter, dISCOUNT_RATEParameter, dISCOUNT_PRICEParameter, sOLDOUT_YNParameter, p_INFO_DETAIL_WEBParameter, p_INFO_DETAIL_MOBILEParameter, mV_URLParameter, p_COMPONENT_INFOParameter, p_TAGParameter, mAIN_IMGParameter, oTHER_IMG1Parameter, oTHER_IMG2Parameter, oTHER_IMG3Parameter, iCON_YNParameter, wITH_PRODUCT_LISTParameter);
        }

        public virtual int SP_ADMIN_PRODUCT_BATCH_UPD(string p_CODE, string iCON_BATCH_CHK, string iCON_YN, string dISPLAY_YN, string sOLDOUT_YN, string p_OUTLET_YN)
        {
            var p_CODEParameter = p_CODE != null ?
                new ObjectParameter("P_CODE", p_CODE) :
                new ObjectParameter("P_CODE", typeof(string));

            var iCON_BATCH_CHKParameter = iCON_BATCH_CHK != null ?
                new ObjectParameter("ICON_BATCH_CHK", iCON_BATCH_CHK) :
                new ObjectParameter("ICON_BATCH_CHK", typeof(string));

            var iCON_YNParameter = iCON_YN != null ?
                new ObjectParameter("ICON_YN", iCON_YN) :
                new ObjectParameter("ICON_YN", typeof(string));

            var dISPLAY_YNParameter = dISPLAY_YN != null ?
                new ObjectParameter("DISPLAY_YN", dISPLAY_YN) :
                new ObjectParameter("DISPLAY_YN", typeof(string));

            var sOLDOUT_YNParameter = sOLDOUT_YN != null ?
                new ObjectParameter("SOLDOUT_YN", sOLDOUT_YN) :
                new ObjectParameter("SOLDOUT_YN", typeof(string));

            var p_OUTLET_YNParameter = p_OUTLET_YN != null ?
                new ObjectParameter("P_OUTLET_YN", p_OUTLET_YN) :
                new ObjectParameter("P_OUTLET_YN", typeof(string));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADMIN_PRODUCT_BATCH_UPD", p_CODEParameter, iCON_BATCH_CHKParameter, iCON_YNParameter, dISPLAY_YNParameter, sOLDOUT_YNParameter, p_OUTLET_YNParameter);
        }
    }
}
