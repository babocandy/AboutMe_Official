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
    using System.Collections.Generic;
    
    public partial class TB_COUPON_PRICE_DETAIL
    {
        public int IDX { get; set; }
        public string COUPON_MASTER_IDX { get; set; }
        public Nullable<int> COUPON_MONEY { get; set; }
        public Nullable<byte> COUPON_DISCOUNT_RATE { get; set; }
        public string COUPON_GBN { get; set; }
        public string ADMIN_MEMO { get; set; }
        public Nullable<System.DateTime> INS_DATE { get; set; }
    }
}
