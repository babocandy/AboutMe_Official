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


        //상품별 프로모션카테고리별 프로모션 정책 TOP 1 가져오기  
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


        //특정프로모션에 소속된 상품리스트 가져오기 
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


    #region 상품상세 페이지 -pc 모바일 공통 ==================================================

        //특정 상품에 적용된 모든 전체할인 프로모션정책을 가져온다  (상품별 프로모션에 소속된 상품과 그렇지 않은 상품 모두)
        public List<SP_PROMOTION_TOTAL_BY_PRODUCT_DUMMY_ALL_SEL_Result> GetPromotionTotalInfo_ByProduct(string PCode, string MGbn, string MGrade)
        {

            List<SP_PROMOTION_TOTAL_BY_PRODUCT_DUMMY_ALL_SEL_Result> lst = new List<SP_PROMOTION_TOTAL_BY_PRODUCT_DUMMY_ALL_SEL_Result>();
            using (AdminPromotion3Entities AdmPromotionContext = new AdminPromotion3Entities())
            {
                /**try {**/
                lst = AdmPromotionContext.SP_PROMOTION_TOTAL_BY_PRODUCT_ALL_SEL(PCode,MGbn,MGrade).ToList();
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;

        }

        //상세페이지의 세트상품 리스트 - 특정상품과 연관된 세트상품리스트 보여주기 
        public List<SP_PROMOTION_BY_PRODUCT_SET_RELATED_SEL_Result> GetPromotionByProduct_SET_RelatedList(string CdPromotionProduct, string PCode)
        {

            List<SP_PROMOTION_BY_PRODUCT_SET_RELATED_SEL_Result> lst = new List<SP_PROMOTION_BY_PRODUCT_SET_RELATED_SEL_Result>();
            using (AdminPromotion3Entities AdmPromotionContext = new AdminPromotion3Entities())
            {
                /**try {**/
                lst = AdmPromotionContext.SP_PROMOTION_BY_PRODUCT_SET_RELATED_SEL(CdPromotionProduct , PCode).ToList();
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;
        }



        //회원등급별 포인트 적립률 가져오기 
        public List<SP_PROMOTION_POINT_SAVE_RATE_BY_MEMGRADE_SEL_Result> GetPointSaveRateByMemGrade(string MGbn, string MGrade)
        {

            List<SP_PROMOTION_POINT_SAVE_RATE_BY_MEMGRADE_SEL_Result> lst = new List<SP_PROMOTION_POINT_SAVE_RATE_BY_MEMGRADE_SEL_Result>();
            using (AdminPromotion3Entities AdmPromotionContext = new AdminPromotion3Entities())
            {
                /**try {**/
                lst = AdmPromotionContext.SP_PROMOTION_POINT_SAVE_RATE_BY_MEMGRADE_SEL(MGbn,MGrade).ToList();
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;
        }





    #endregion 



    }
}
