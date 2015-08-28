using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboutMe.Domain.Entity.AdminPromotion
{
    #region 프로모션 상품가격등록 
    public class PromotionByProductReg
    {
        public string CD_PROMOTION_PRODUCT { get; set; }
        public string P_CODE { get; set; }
        public string CD_PROMOTION_TOTAL_LST { get; set; }
        public Nullable<int> P_DISCOUNT_PRICE { get; set; }
        public Nullable<int> PMO_PRICE { get; set; }
        public string USABLE_YN { get; set; }
    }
    #endregion
}
