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

        #region PC버전 - 마이페이지
        
        //다운로드 가능 쿠폰리스트 - PC버전 
        List<SP_COUPON_DOWNLOADABLE_LIST_Result> GetDownloadableCouponList(string M_Id);

        //다운로드 가능한 쿠폰 수량 가져오기  - pc버전 , 번호확인 필요한 쿠폰까지 모두 가져옴 COUPON_NUM_CHECK_TF
        List<SP_ADMIN_COUPON_COMMON_CNT_Result> GetDownloadableCouponCnt(string M_Id);

        //다운로드 처리  - pc버전 , 번호 인증 필요없는 쿠폰
        int UpdateCouponDownload_Pc_Ver_And_NoNumberChk_Ver(string M_Id, int IdxCouponNumber, string UpdateMethod);

        //사용가능한 쿠폰 리스트 가져오기 -PC
        List<SP_COUPON_ISSUED_DETAIL_SEL_Result> GetCouponAvailableList(string M_ID, string SearchCol, string SearchKeyword, int Page, int PageSize);

        //사용가능한 쿠폰리스트 COUNT 가져오기 -PC
        int GettCouponAvailableListCnt(string M_ID, string SearchCol, string SearchKeyword);

        //쿠폰별 상품 리스트 가져오기 --PC 모바일 공통
        List<SP_COUPON_VIEW_PRODUCT_DETAIL_SEL_Result> GetCouponProductList(string M_ID, int IdxCouponNumber, string SearchCol, string SearchKeyword, int Page, int PageSize);

        //쿠폰별 상품 리스트 COUNT 가져오기 --PC 모바일 공통
        int GetCouponProductListCnt(string M_ID, int IdxCouponNumber, string SearchCol, string SearchKeyword);

        //쿠폰 마스터 정보 가져오기 --PC 모바일 공통
        List<SP_COUPON_MASTER_INFO_SEL_Result> GetCouponMasterInfo(string M_ID, int IdxCouponNumber);

        //사용완료,종료된 쿠폰 리스트 - PC,모바일 공통
        List<SP_COUPON_ISSUED_DETAIL_SEL_Result> GetCouponClosedList(string M_ID, string UsableDevice, string SearchCol, string SearchKeyword, int Page, int PageSize);

        //사용완료,종료된 쿠폰 리스트 COUNT - PC,모바일 공통
        int GetCouponClosedListCnt(string M_ID, string UsableDevice, string SearchCol, string SearchKeyword);


        #endregion


        #region PC버전 상세페이지 

        //상품별, 회원별 사용가능 혹은 다운로드 가능한 쿠폰 TOP 1    - pc/모바일 공통 
        List<SP_COUPON_TOP_1_BY_MEMBER_BY_PRD_SEL_Result> GetCouponTop1_ByMem_ByPrd(string UsableDeviceGbn, string PCode, string M_Id);


        //상품별 유효 쿠폰 정책이 있는지 (일반/브론즈 회원 사용 가능한) TOP 1    - pc/모바일 공통 .. 
        List<SP_COUPON_TOP1_BY_PRD_SEL_NO_LOGIN_SEL_Result> GetCouponTop1_ByNoLogin_ByPrd(string UsableDeviceGbn, string PCode);

        #endregion 


        #region 모바일 버전 - 마이페이지 

        //다운로드 처리  - pc버전 , 번호 인증 필요없는 쿠폰
        int UpdateCouponDownload_Mobile_Ver_And_NoNumberChk_Ver(string M_Id, int IdxCouponNumber, string UpdateMethod);


        #endregion 

        #region 회원가입시 쿠폰발행

        //회원가입시 가입쿠폰발행 
        int InsCouponMakeOnMemberJoin(string M_Id);
        
        #endregion

    }
}
