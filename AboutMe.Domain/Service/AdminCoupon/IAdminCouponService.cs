using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using AboutMe.Domain.Entity.AdminCoupon;


namespace AboutMe.Domain.Service.AdminCoupon
{
    public interface IAdminCouponService
    {
       //전체할인 프로모션 리스트 가져오기
        List<SP_ADMIN_COUPON_MASTER_DETAIL_SEL_Result> GetAdminCouponList(string SearchCol, string SearchKeyword, int Page, int PageSize);
        int GetAdminCouponListCnt(string SearchCol, string SearchKeyword);
        int InsAdminCouponMaster(TB_COUPON_MASTER Tb, string[] CheckMemGrade);

        //쿠폰적용된 상품정보
        int GetAdminCouponProductListCnt(string SearchCol, string SearchKeyword, string CdCoupon);
        List<SP_ADMIN_COUPON_PRODUCT_DEATIL_SEL_Result> GetAdminCouponProeuctList(string SearchCol, string SearchKeyword, int Page, int PageSize, string CdCoupon);

        //쿠폰적용 대상 상품정보 
        List<SP_ADMIN_COUPON_PRODUCT_FOR_CREATE_DETAIL_SEL_Result> GetAdminCouponProductForCreateList(string SearchCol, string SearchKeyword, int Page, int PageSize, string CdCoupon);
        int GetAdminCouponProductForCreateListCnt(string SearchCol, string SearchKeyword, string CdCoupon);
        int InsAdminCouponProduct(List<Tuple<string, string>> ListCdCouponVsPcodeForIns);

    }
}
