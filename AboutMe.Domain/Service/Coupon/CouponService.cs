using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Transactions;
using System.Data.Entity.Core.Objects;
using AboutMe.Domain.Entity.AdminCoupon;

namespace AboutMe.Domain.Service.Coupon
{
    public class CouponService : ICouponService
    {

        #region 마이페이지 ============================================================

        //다운로드 가능한 쿠폰 - pc버전 , 번호확인 필요한 쿠폰까지 모두 가져옴 COUPON_NUM_CHECK_TF
        public List<SP_COUPON_DOWNLOADABLE_LIST_Result> GetDownloadableCouponList(string M_Id)
        {

            List<SP_COUPON_DOWNLOADABLE_LIST_Result> lst = new List<SP_COUPON_DOWNLOADABLE_LIST_Result>();
            using (AdminCouponEntities AdmCouponContext = new AdminCouponEntities())
            {
                /**try {**/
                lst = AdmCouponContext.SP_COUPON_DOWNLOADABLE_LIST(M_Id).ToList();
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;

        }

        //다운로드 가능한 쿠폰 수량 가져오기  - pc버전 , 번호확인 필요한 쿠폰까지 모두 가져옴 COUPON_NUM_CHECK_TF
        public List<SP_ADMIN_COUPON_COMMON_CNT_Result> GetDownloadableCouponCnt(string M_Id)
        {

            List<SP_ADMIN_COUPON_COMMON_CNT_Result> lst = new List<SP_ADMIN_COUPON_COMMON_CNT_Result>();
            using (AdminCouponEntities AdmCouponContext = new AdminCouponEntities())
            {
                /**try {**/
                lst = AdmCouponContext.SP_COUPON_DOWNLOADABLE_CNT(M_Id).ToList();
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;

        }



        //다운로드 처리  - pc버전 , 쿠폰번호 인증이 필요없는 쿠폰
        //M_id =회원아이디, IdxCouponNumber = 쿠폰일련번호 , UpdateMethod = 'S' : 하나만 업데이트 할때 , 'A' = 모든 쿠폰을 다운로드 할 때
        public int  UpdateCouponDownload_Pc_Ver_And_NoNumberChk_Ver(string M_Id,int IdxCouponNumber,string UpdateMethod)
        {
            int ResulstCode = 1;


            if (UpdateMethod != "A") // 개별다운로드일때 
            {
                List<SP_COUPON_MASTER_INFO_SEL_Result> lst1 = GetCouponMasterInfo(M_Id, IdxCouponNumber);

                if (lst1.Count > 0)
                {
                    if (lst1[0].COUPON_NUM_CHECK_TF == "Y") //번호를 확인해야 하는 쿠폰이면 , 번호 인증후 다운로드 해야함
                    {
                        ResulstCode = -2;
                    }
                    else
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            try
                            {
                                using (AdminCouponEntities AdmCouponContext = new AdminCouponEntities())
                                {
                                    AdmCouponContext.SP_COUPON_DOWNLOAD_AT_PC_VERSION_UPDATE(M_Id, IdxCouponNumber, UpdateMethod);
                                }
                                scope.Complete();
                            }
                            catch (Exception ex)
                            {
                                Transaction.Current.Rollback();
                                scope.Dispose();
                                ResulstCode = -1;
                            }
                        }
                    }
                }
                else
                {
                    ResulstCode = -3; //존재하지 않는 쿠폰  
                }
            }
            else if (UpdateMethod == "A") // 모두 다운로드하려 할때   --- 번호인증을 해야하는 쿠폰은 SP에서 다운로드처리 못하게 되어있음
            {
                
                using (TransactionScope scope = new TransactionScope())
                {
                    try
                    {
                        using (AdminCouponEntities AdmCouponContext = new AdminCouponEntities())
                        {
                            AdmCouponContext.SP_COUPON_DOWNLOAD_AT_PC_VERSION_UPDATE(M_Id, IdxCouponNumber, UpdateMethod);
                        }
                        scope.Complete();
                    }
                    catch (Exception ex)
                    {
                        Transaction.Current.Rollback();
                        scope.Dispose();
                        ResulstCode = -1;
                    }
                }
                   


            }

            return ResulstCode;

        }

        //사용가능한 쿠폰 리스트 가져오기-PC버전
        public List<SP_COUPON_ISSUED_DETAIL_SEL_Result> GetCouponAvailableList(string M_ID, string SearchCol, string SearchKeyword, int Page, int PageSize)
        {

            List<SP_COUPON_ISSUED_DETAIL_SEL_Result> lst = new List<SP_COUPON_ISSUED_DETAIL_SEL_Result>();
            using (AdminCouponEntities AdmCouponContext = new AdminCouponEntities())
            {
                /**try {**/
                lst = AdmCouponContext.SP_COUPON_AVAILABLE_LIST_SEL(M_ID, Page, PageSize, SearchCol, SearchKeyword).ToList();
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }
            return lst;

        }

        //사용가능한 쿠폰리스트 COUNT 가져오기-PC 버전
        public int GettCouponAvailableListCnt(string M_ID, string SearchCol, string SearchKeyword)
        {

            List<SP_COUPON_COMMON_CNT_Result> lst = new List<SP_COUPON_COMMON_CNT_Result>();
            int list_cnt = 0;

            using (AdminCouponEntities AdmCouponContext = new AdminCouponEntities())
            {
                /**try {**/
                lst = AdmCouponContext.SP_COUPON_AVAILABLE_LIST_CNT(M_ID, SearchCol, SearchKeyword).ToList();
                if (lst != null && lst.Count > 0)
                    list_cnt = lst[0].CNT;
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return list_cnt;
        }




        //쿠폰별 상품 리스트 가져오기 --PC/모바일 공통
        public List<SP_COUPON_VIEW_PRODUCT_DETAIL_SEL_Result> GetCouponProductList(string M_ID,int IdxCouponNumber, string SearchCol, string SearchKeyword, int Page, int PageSize)
        {

            List<SP_COUPON_VIEW_PRODUCT_DETAIL_SEL_Result> lst = new List<SP_COUPON_VIEW_PRODUCT_DETAIL_SEL_Result>();
            using (AdminCouponEntities AdmCouponContext = new AdminCouponEntities())
            {
                /**try {**/
                lst = AdmCouponContext.SP_COUPON_PRODUCT_LIST_SEL(M_ID, IdxCouponNumber, Page, PageSize, SearchCol, SearchKeyword).ToList();
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;

        }

        //쿠폰별 상품 리스트 COUNT 가져오기 --PC모바일 공통
        public int GetCouponProductListCnt(string M_ID, int IdxCouponNumber, string SearchCol, string SearchKeyword)
        {

            List<SP_COUPON_COMMON_CNT_Result> lst = new List<SP_COUPON_COMMON_CNT_Result>();
            int list_cnt = 0;

            using (AdminCouponEntities AdmCouponContext = new AdminCouponEntities())
            {
                /**try {**/
                lst = AdmCouponContext.SP_COUPON_PRODUCT_LIST_CNT(M_ID,IdxCouponNumber, SearchCol, SearchKeyword).ToList();
                if (lst != null && lst.Count > 0)
                    list_cnt = lst[0].CNT;
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return list_cnt;

        }


        //다운로드 처리  - 모바일 버전 , 쿠폰번호 인증이 필요없는 쿠폰
        //M_id =회원아이디, IdxCouponNumber = 쿠폰일련번호 , UpdateMethod = 'S' : 하나만 업데이트 할때 , 'A' = 모든 쿠폰을 다운로드 할 때
        public int UpdateCouponDownload_Mobile_Ver_And_NoNumberChk_Ver(string M_Id, int IdxCouponNumber, string UpdateMethod)
        {
            int ResulstCode = 1;

            List<SP_COUPON_MASTER_INFO_SEL_Result> lst1 = GetCouponMasterInfo(M_Id, IdxCouponNumber);

            if (lst1.Count > 0)
            {
                if (lst1[0].COUPON_NUM_CHECK_TF == "Y") //번호를 확인해야 하는 쿠폰이면 , 번호 인증후 다운로드 해야함
                {
                    ResulstCode = -2;
                }
                else
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        try
                        {
                            using (AdminCouponEntities AdmCouponContext = new AdminCouponEntities())
                            {
                                //ObjectParameter objOutParam = new ObjectParameter("CD_PROMOTION_PRODUCT", typeof(string));//sp의 output parameter변수명을 동일하게 사용한다.
                                AdmCouponContext.SP_COUPON_DOWNLOAD_AT_PC_VERSION_UPDATE(M_Id, IdxCouponNumber, UpdateMethod);
                                //NewCdPromotionProduct = objOutParam.Value.ToString();
                            }
                            scope.Complete();
                        }
                        catch (Exception ex)
                        {
                            Transaction.Current.Rollback();
                            scope.Dispose();
                            ResulstCode = -1;
                        }
                    }
                }
            }
            else
            {
                ResulstCode = -3; //존재하지 않는 쿠폰  
            }

            return ResulstCode;

        }




        //사용완료,종료된 쿠폰 리스트 - PC,모바일 공통
        public List<SP_COUPON_ISSUED_DETAIL_SEL_Result> GetCouponClosedList(string M_ID,string UsableDevice ,string SearchCol, string SearchKeyword, int Page, int PageSize)
        {

            List<SP_COUPON_ISSUED_DETAIL_SEL_Result> lst = new List<SP_COUPON_ISSUED_DETAIL_SEL_Result>();
            using (AdminCouponEntities AdmCouponContext = new AdminCouponEntities())
            {
                /**try {**/
                lst = AdmCouponContext.SP_COUPON_CLOSED_LIST_SEL(M_ID, UsableDevice, Page, PageSize, SearchCol, SearchKeyword).ToList();
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }
            return lst;

        }

        //사용완료,종료된 쿠폰 리스트 COUNT - PC,모바일 공통
        public int GetCouponClosedListCnt(string M_ID, string UsableDevice , string SearchCol, string SearchKeyword)
        {

            List<SP_COUPON_COMMON_CNT_Result> lst = new List<SP_COUPON_COMMON_CNT_Result>();
            int list_cnt = 0;

            using (AdminCouponEntities AdmCouponContext = new AdminCouponEntities())
            {
                /**try {**/
                lst = AdmCouponContext.SP_COUPON_CLOSED_LIST_COUNT(M_ID,UsableDevice, 0,0,SearchCol, SearchKeyword).ToList();
                if (lst != null && lst.Count > 0)
                    list_cnt = lst[0].CNT;
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return list_cnt;
        }
        
        
        //쿠폰 마스터 정보 가져오기
        public List<SP_COUPON_MASTER_INFO_SEL_Result> GetCouponMasterInfo(string M_ID, int IdxCouponNumber)
        {

            List<SP_COUPON_MASTER_INFO_SEL_Result> lst = new List<SP_COUPON_MASTER_INFO_SEL_Result>();
            using (AdminCouponEntities AdmCouponContext = new AdminCouponEntities())
            {
                /**try {**/
                lst = AdmCouponContext.SP_COUPON_MASTER_INFO_SEL(M_ID, IdxCouponNumber).ToList();
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }
            return lst;

        }




        //==============================================================
        //==============================================================




        #endregion

        #region 상품상세페이지 ================================================
        //상품별, 회원별 사용가능 혹은 다운로드 가능한 쿠폰 TOP 1        
        //     ==> 사용가능한것 다운로드가능한것 모두 있을때는 사용가능한것 우선
        //     ==> pc, 모바일버전은 파라미터에 따라 
        //     ==> 번호인증 필요한 쿠폰까지    . 
        public List<SP_COUPON_TOP_1_BY_MEMBER_BY_PRD_SEL_Result> GetCouponTop1_ByMem_ByPrd(string UsableDeviceGbn, string PCode, string M_Id)
        {

            List<SP_COUPON_TOP_1_BY_MEMBER_BY_PRD_SEL_Result> lst = new List<SP_COUPON_TOP_1_BY_MEMBER_BY_PRD_SEL_Result>();
            using (AdminCouponEntities AdmCouponContext = new AdminCouponEntities())
            {
                /**try {**/
                lst = AdmCouponContext.SP_COUPON_TOP_1_BY_MEMBER_BY_PRD_SEL(UsableDeviceGbn,PCode,M_Id).ToList();
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;

        }


        //상품별 유효 쿠폰 정책이 있는지 (일반/브론즈 회원 사용 가능한) TOP 1    - pc/모바일 공통 .. 
        public List<SP_COUPON_TOP1_BY_PRD_SEL_NO_LOGIN_SEL_Result> GetCouponTop1_ByNoLogin_ByPrd(string UsableDeviceGbn, string PCode)
        {

            List<SP_COUPON_TOP1_BY_PRD_SEL_NO_LOGIN_SEL_Result> lst = new List<SP_COUPON_TOP1_BY_PRD_SEL_NO_LOGIN_SEL_Result>();
            using (AdminCouponEntities AdmCouponContext = new AdminCouponEntities())
            {
                /**try {**/
                lst = AdmCouponContext.SP_COUPON_TOP1_BY_PRD_SEL_NO_LOGIN_SEL(PCode,UsableDeviceGbn).ToList();
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;

        }

        #endregion 

        #region 회원가입시 쿠폰발행


        //회원가입시 가입쿠폰발행 
        public int InsCouponMakeOnMemberJoin(string M_Id)
        {

            int IsSuccess = 1;
            
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AdminCouponEntities AdmCouponContext = new AdminCouponEntities())
                    {                   
                        //마스터정보 insert
                        AdmCouponContext.SP_COUPON_MAKE_AT_MEM_JOIN_INSERT(M_Id);
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    Transaction.Current.Rollback();
                    scope.Dispose();
                    IsSuccess = -1;
                }

            }


            return IsSuccess;
        }


        #endregion 



    }
}
