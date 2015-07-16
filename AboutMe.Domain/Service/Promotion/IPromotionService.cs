using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.AdminPromotion;

namespace AboutMe.Domain.Service.Promotion
{
    public interface IPromotionService
    {


        List<SP_PROMOTION_BY_PRODUCT_TOP_1_DETAIL_SEL_Result> GetPromotionByProductTop1_Info(string PmoProductCategory);
        List<SP_PROMOTION_BY_PRODUCT_PRICE_LIST_SEL_Result> GetPromotionByProductList(string CdPromotionProduct);
    
    }

}
