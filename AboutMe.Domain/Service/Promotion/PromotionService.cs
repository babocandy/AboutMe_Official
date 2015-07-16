using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Transactions;
using System.Data.Entity.Core.Objects;
using AboutMe.Domain.Entity.AdminPromotion;

namespace AboutMe.Domain.Service.Promotion
{
    public class PromotionService : IPromotionService
    {


        //상품별 프로모션 정책 TOP 1 가져오기  
        public List<SP_PROMOTION_BY_PRODUCT_TOP_1_DETAIL_SEL_Result> GetPromotionByProductTop1_Info(string PmoProductCategory)
        {

            List<SP_PROMOTION_BY_PRODUCT_TOP_1_DETAIL_SEL_Result> lst = new List<SP_PROMOTION_BY_PRODUCT_TOP_1_DETAIL_SEL_Result>();
            using (AdminPromotion3Entities AdmPromotionContext = new AdminPromotion3Entities())
            {
                /**try {**/
                lst = AdmPromotionContext.SP_PROMOTION_BY_PRODUCT_TOP_1_DETAIL_SEL(PmoProductCategory).ToList();
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;

        }


        //전체할인 프로모션 리스트 가져오기
        public List<SP_PROMOTION_BY_PRODUCT_PRICE_LIST_SEL_Result> GetPromotionByProductList(string CdPromotionProduct)
        {

            List<SP_PROMOTION_BY_PRODUCT_PRICE_LIST_SEL_Result> lst = new List<SP_PROMOTION_BY_PRODUCT_PRICE_LIST_SEL_Result>();
            using (AdminPromotion3Entities AdmPromotionContext = new AdminPromotion3Entities())
            {
                /**try {**/
                lst = AdmPromotionContext.SP_PROMOTION_BY_PRODUCT_PRICE_LIST_SEL(CdPromotionProduct).ToList();
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;

        }



    }
}
