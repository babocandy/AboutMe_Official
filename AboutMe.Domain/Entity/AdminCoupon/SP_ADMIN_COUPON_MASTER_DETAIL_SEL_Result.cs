//------------------------------------------------------------------------------
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
    
    public partial class SP_ADMIN_COUPON_MASTER_DETAIL_SEL_Result
    {
        public int IDX { get; set; }
        public string CD_COUPON { get; set; }
        public string COUPON_NAME { get; set; }
        public string COUPON_AD_MSG { get; set; }
        public string COUPON_USE_DESCRIPTION { get; set; }
        public string COUPON_GBN { get; set; }
        public string COUPON_GBN_M { get; set; }
        public string RATE_OR_MONEY { get; set; }
        public string SERVICE_LIFE_GBN { get; set; }
        public Nullable<System.DateTime> FIXED_PERIOD_FROM { get; set; }
        public Nullable<System.DateTime> FIXED_PERIOD_TO { get; set; }
        public Nullable<int> EXRIRED_DAY_FROM_ISSUE_DT { get; set; }
        public Nullable<System.DateTime> DOWNLOAD_DATE_FROM { get; set; }
        public string USABLE_DEVICE_GBN { get; set; }
        public string PRODUCT_APP_SCOPE_GBN { get; set; }
        public string MEMBER_APP_SCOPE_GBN { get; set; }
        public string ISSUE_METHOD_GBN { get; set; }
        public string ISSUE_METHOD_WITH_AUTO { get; set; }
        public string COUPON_NUM_CHECK_TF { get; set; }
        public Nullable<int> ISSUE_MAX_LIMIT { get; set; }
        public Nullable<System.DateTime> MASTER_FROM_DATE { get; set; }
        public Nullable<System.DateTime> MASTER_TO_DATE { get; set; }
        public string USABLE_YN { get; set; }
        public Nullable<System.DateTime> INS_DATE { get; set; }
        public Nullable<int> COUPON_DISCOUNT_MONEY { get; set; }
        public Nullable<int> COUPON_DISCOUNT_RATE { get; set; }
    }
}
