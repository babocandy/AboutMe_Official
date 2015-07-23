using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using AboutMe.Domain.Entity.AdminCoupon;


namespace AboutMe.Domain.Service.Coupon
{
    public interface ICouponService
    {

        #region 마이페이지
        
        //다운로드 가능 쿠폰리스트
        List<SP_COUPON_DOWNLOADABLE_LIST_Result> GetDownloadableCouponList(string M_Id);

        //다운로드 처리  - pc버전 , 번호 인증 필요없는 쿠폰
        int UpdateCouponDownload_Pc_Ver_And_NoNumberChk_Ver(string M_Id, int IdxCouponNumber, string UpdateMethod);

        //사용가능한 쿠폰 리스트 가져오기 
        List<SP_COUPON_ISSUED_DETAIL_SEL_Result> GetCouponAvailableList(string M_ID, string SearchCol, string SearchKeyword, int Page, int PageSize);

        //사용가능한 쿠폰리스트 COUNT 가져오기 
        int GettCouponAvailableListCnt(string M_ID, string SearchCol, string SearchKeyword);

        //쿠폰별 상품 리스트 가져오기
        List<SP_COUPON_VIEW_PRODUCT_DETAIL_SEL_Result> GetCouponProductList(string M_ID, int IdxCouponNumber, string SearchCol, string SearchKeyword, int Page, int PageSize);

        //쿠폰별 상품 리스트 COUNT 가져오기 
        int GetCouponProductListCnt(string M_ID, int IdxCouponNumber, string SearchCol, string SearchKeyword);

        //쿠폰 마스터 정보 가져오기
        List<SP_COUPON_MASTER_INFO_SEL_Result> GetCouponMasterInfo(string M_ID, int IdxCouponNumber);

        #endregion
    }
}
