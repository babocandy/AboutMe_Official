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
        //쿠폰마스터 정보
        List<SP_ADMIN_COUPON_MASTER_DETAIL_SEL_Result> GetAdminCouponList(string SearchCol, string SearchKeyword, int Page, int PageSize);
        int GetAdminCouponListCnt(string SearchCol, string SearchKeyword);
        int InsAdminCouponMaster(TB_COUPON_MASTER Tb, string[] CheckMemGrade);
        List<SP_ADMIN_COUPON_MASTER_DETAIL_SEL_Result> GetAdminCouponDetail(string CdCoupon);
        int UpdateAdminCouponMaster(TB_COUPON_MASTER Tb, string[] CheckMemGrade, string CdCoupon);

        //쿠폰적용된 상품정보
        int GetAdminCouponProductListCnt(string SearchCol, string SearchKeyword, string CdCoupon);
        List<SP_ADMIN_COUPON_PRODUCT_DEATIL_SEL_Result> GetAdminCouponProeuctList(string SearchCol, string SearchKeyword, int Page, int PageSize, string CdCoupon);

        //쿠폰적용 대상 상품정보 
        List<SP_ADMIN_COUPON_PRODUCT_FOR_CREATE_DETAIL_SEL_Result> GetAdminCouponProductForCreateList(string SearchCol, string SearchKeyword, int Page, int PageSize, string CdCoupon);
        int GetAdminCouponProductForCreateListCnt(string SearchCol, string SearchKeyword, string CdCoupon);
        int InsAdminCouponProduct(List<Tuple<string, string>> ListCdCouponVsPcodeForIns);

        //발행된 쿠폰 정보
        List<SP_ADMIN_COUPON_ISSUED_DETAIL_SEL_Result> GetAdminCouponIssuedList(string SearchCol, string SearchKeyword, int Page, int PageSize, string CdCoupon);
        int GetAdminCouponIssuedListCnt(string SearchCol, string SearchKeyword, string CdCoupon);

        //발행가능 등급정보
        List<SP_ADMIN_COUPON_MEMBER_GRADE_SEL_Result> GetAdminCouponMemberGradeList(string CdCoupon);
        //쿠폰발행가능 상품카운트
        int GetAdminCouponProductUsableCnt(string CdCoupon);
        
        #region 쿠폰발행
        
        //쿠폰발행 - 지불쿠폰 OR 배송쿠폰/인증번호 필요없는 쿠폰/수동발행/일괄발행 INSERT(admin)
        int InsAdminCouponIssue_WithNoNumcheck_ManualEntire(string CdCoupon, string AdminId);

        //쿠폰발행 - 지불쿠폰 OR 배송쿠폰/인증번호 필요없는 쿠폰/수동발행/개별발행 INSERT(admin) 
        int InsAdminCouponIssue_WithNoNumcheck_Manual_Single(string CdCoupon, int CouponMoney, int CouponDiscountRate, string M_id, string AdminId, string IssuedMemo);
        
        #endregion

    }
}
