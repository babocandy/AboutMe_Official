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
    
    public partial class TB_PROMOTION_BY_PRODUCT_PRICE
    {
        public int IDX { get; set; }
        public string CD_PROMOTION_PRODUCT { get; set; }
        public string P_CODE { get; set; }
        public Nullable<int> P_DISCOUNT_PRICE { get; set; }
        public string PMO_PRODUCT_RATE_OR_MONEY { get; set; }
        public Nullable<byte> PMO_PRODUCT_DISCOUNT_RATE { get; set; }
        public Nullable<int> PMO_PRODUCT_DISCOUNT_MONEY { get; set; }
        public Nullable<int> PMO_PRICE { get; set; }
        public string PMO_ONE_ONE_P_CODE { get; set; }
        public Nullable<int> PMO_ONE_ONE_PRICE { get; set; }
        public string USABLE_YN { get; set; }
        public Nullable<System.DateTime> INS_DATE { get; set; }
    }
}