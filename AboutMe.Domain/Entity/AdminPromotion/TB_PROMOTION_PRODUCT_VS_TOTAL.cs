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
    
    public partial class TB_PROMOTION_PRODUCT_VS_TOTAL
    {
        public int IDX { get; set; }
        public string CD_PROMOTION_PRODUCT { get; set; }
        public string CD_PROMOTION_TOTAL { get; set; }
        public Nullable<int> PRODUCT_PRICE_IDX { get; set; }
        public string P_CODE { get; set; }
        public Nullable<System.DateTime> INS_DATE { get; set; }
        public string USABLE_YN { get; set; }
    }
}
