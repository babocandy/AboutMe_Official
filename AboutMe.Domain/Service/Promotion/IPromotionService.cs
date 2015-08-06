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

        //상품별 프로모션카테고리별 프로모션 정책 TOP 1 가져오기  
        List<SP_PROMOTION_BY_PRODUCT_TOP_1_DETAIL_SEL_Result> GetPromotionByProductTop1_Info(string PmoProductCategory);
        List<SP_PROMOTION_BY_PRODUCT_PRICE_LIST_SEL_Result> GetPromotionByProductList(string CdPromotionProduct);

        //특정 상품에 적용된 모든 전체할인 프로모션정책을 가져온다 
        List<SP_PROMOTION_TOTAL_BY_PRODUCT_SEL_Result> GetPromotionTotalInfo_ByProduct(string PCode, string MGbn, string MGrade);
    
    }

}
