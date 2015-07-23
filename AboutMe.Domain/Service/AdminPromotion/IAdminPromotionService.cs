using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.AdminPromotion;

namespace AboutMe.Domain.Service.AdminPromotion
{
    public interface IAdminPromotionService
    {
        #region 전체할인 프로모션 관리 -------------------------------------------------------

        List<SP_ADMIN_PROMOTION_BY_TOTAL_DETAIL_SEL_Result> GetAdminPromotionByTotalList(string SearchCol, string SearchKeyword, int Page, int PageSize);
        int GetAdminPromotionByTotalListCnt(string SearchCol, string SearchKeyword);

        int InsAdminPromotionByTotal(TB_PROMOTION_BY_TOTAL Tb, string[] CheckMemGrade);
        int GetAdminPromotionByTotalDupSel(string PmoTotalCategory, DateTime PmoTotalDateFrom, DateTime PmoTotalDateTo);

        List<SP_ADMIN_PROMOTION_BY_TOTAL_DETAIL_SEL_Result> GetAdminPromotionByTotalDetail(string CdPromotionTotal);
        List<SP_ADMIN_PROMOTION_BY_TOTAL_MEM_GRADE_WITH_CD_SEL_Result> GetAdminPromotionByTotalMemGradeWithCd(string CdPromotionTotal);

        int GetAdminPromotionByTotalDupAtUpdateSel(string PmoTotalCategory, DateTime PmoTotalDateFrom, DateTime PmoTotalDateTo, string CdPromotionTotal);

        int UpdateAdminPromotionByTotal(TB_PROMOTION_BY_TOTAL Tb, string[] CheckMemGrade, string CdPromotionTotal);

        List<SP_ADMIN_PROMOTION_BY_TOTAL_ACTIVE_LIST_SEL_Result> GetAdminPromotionByTotalActiveList();
        
        #endregion --------------------------------------------------------------------------


        #region 상품별 할인 프로모션 관리 -------------------------------------------------------

        List<SP_ADMIN_PROMOTION_BY_PRODUCT_DETAIL_SEL_Result> GetAdminPromotionByProductList(string SearchCol, string SearchKeyword, int Page, int PageSize);
        int GetAdminPromotionByProductListCnt(string SearchCol, string SearchKeyword);
        List<SP_ADMIN_PROMOTION_BY_PRODUCT_DETAIL_SEL_Result> GetAdminPromotionByProductDetail(string CdPromotionProduct);

        int InsAdminPromotionByProduct(TB_PROMOTION_BY_PRODUCT Tb);
        int GetAdminPromotionByProductDupSel(string PmoProductCategory, DateTime PmoProductDateFrom, DateTime PmoProductDateTo);

        List<SP_ADMIN_PROMOTION_BY_PRODUCT_PRICE_SEL_Result> GetAdminPromotionByProductPricingList(string CdPromotionProduct);
        List<SP_ADMIN_PROMOTION_BY_PRODUCT_VS_TOTAL_SEL_Result> GetAdminPromotionByProductVsTotalList(string CdPromotionProduct);

        int GetAdminPromotionByProductPricingAllDupSel(string CdPromotionProduct, string Pcode);
        int InsAdminPromotionByProductPricing(TB_PROMOTION_BY_PRODUCT_PRICE Tb, string CdPromotionProduct, string[] CheckCdPromotiontTotal);
        int GetAdminPromotionProductCodeCheck(string Pcode);


        int UpdateAdminPromotionByProduct(TB_PROMOTION_BY_PRODUCT Tb, string CdPromotionProduct);
        int GetAdminPromotionByProductForUpdateAllProductCheckDupSel(string CdPromotionProduct, DateTime TargetPmoProductDateFrom, DateTime TargetPmoProductDateTo);
        int GetAdminPromotionByProductForUpdateDupSel(string PmoProductCategory, DateTime TargetPmoProductDateFrom, DateTime TargetPmoProductDateTo, string CdPromotionProduct);


        //int UpdateAdminPromotionByProductPricing(string UsableYN, string CdPromotionProduct, string[] CheckCdPromotiontTotal);

        int UpdateAdminPromotionByProductPricing(string UsableYN, string CdPromotionProduct, string[] CheckCdPromotiontTotal, string Pcode, int idx);
        int GetAdminPromotionByProductPricing_IDX_AllDupSel(string CdPromotionProduct, string Pcode, int idx);

        #endregion --------------------------------------------------------------------------
    }

}
