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
    
    public partial class SP_ADMIN_COUPON_ISSUED_DETAIL_SEL_Result
    {
        public int IDX { get; set; }
        public int IDX_COUPON_NUMBER { get; set; }
        public string CD_COUPON { get; set; }
        public string COUPON_VERIFI_NUMBER { get; set; }
        public Nullable<int> COUPON_MONEY_DETAIL_NO { get; set; }
        public Nullable<int> COUPON_MONEY { get; set; }
        public Nullable<int> COUPON_DISCOUNT_RATE { get; set; }
        public string CD_COUPON_KIND { get; set; }
        public string USE_STATUS { get; set; }
        public string USABLE_TF { get; set; }
        public string M_ID { get; set; }
        public Nullable<System.DateTime> ISSUE_DATE { get; set; }
        public Nullable<System.DateTime> USABLE_DATE_FROM { get; set; }
        public Nullable<System.DateTime> USABLE_DATE_TO { get; set; }
        public Nullable<int> ORDER_NO { get; set; }
        public Nullable<int> ORDER_DETAIL_NO { get; set; }
        public string ORDER_CANCEL_FL { get; set; }
        public string RE_ISSUE_FL { get; set; }
        public Nullable<System.DateTime> RE_ISSUE_DATE { get; set; }
        public Nullable<System.DateTime> DOWNLOAD_DATE_FROM { get; set; }
        public Nullable<System.DateTime> DOWNLOAD_DATE_TO { get; set; }
        public string ISSUED_GBN { get; set; }
        public string ISSUED_MEMO { get; set; }
    }
}
