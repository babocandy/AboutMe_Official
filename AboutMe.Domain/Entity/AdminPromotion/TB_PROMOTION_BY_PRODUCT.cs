//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AboutMe.Domain.Entity.AdminPromotion
{
    using System;
    using System.Collections.Generic;
    
    public partial class TB_PROMOTION_BY_PRODUCT
    {
        public int IDX { get; set; }
        public string CD_PROMOTION_PRODUCT { get; set; }
        public string PMO_PRODUCT_NAME { get; set; }
        public string PMO_PRODUCT_CATEGORY { get; set; }
        public string PMO_PRODUCT_CATEGORY_NAME { get; set; }
        public string PMO_PRODUCT_MAIN_TITLE { get; set; }
        public string PMO_PRODUCT_SUB_TITLE { get; set; }
        public string PMO_PRODUCT_SHOPPING_TIP { get; set; }
        public string PMO_PRODUCT_MAIN_IMG { get; set; }
        public string PMO_PRODUCT_PC_MAINPG_IMG { get; set; }
        public string PMO_PRODUCT_PC_EVENT_MAINPG_IMG { get; set; }
        public string PMO_PRODUCT_PC_MAINPG_SMALL_IMG { get; set; }
        public string PMO_PRODUCT_MOBILE_MAIN_IMG { get; set; }
        public string PMO_PRODUCT_MOBILE_MAINPG_IMG { get; set; }
        public string PMO_PRODUCT_MOBILE_EVENT_MAINPG_IMG { get; set; }
        public string PMO_PRODUCT_RATE_OR_MONEY { get; set; }
        public Nullable<byte> PMO_PRODUCT_DISCOUNT_RATE { get; set; }
        public Nullable<int> PMO_PRODUCT_DISCOUNT_MONEY { get; set; }
        public Nullable<byte> PMO_ONEONE_MULTIPLE_CNT { get; set; }
        public Nullable<byte> PMO_SET_DISCOUNT_CNT { get; set; }
        public Nullable<System.DateTime> PMO_PRODUCT_DATE_FROM { get; set; }
        public Nullable<System.DateTime> PMO_PRODUCT_DATE_TO { get; set; }
        public string USABLE_YN { get; set; }
        public Nullable<System.DateTime> INS_DATE { get; set; }
    }
}
