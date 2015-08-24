using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Transactions;
using System.Data.Entity.Core.Objects;
using AboutMe.Domain.Entity.AdminCoupon;

namespace AboutMe.Domain.Service.AdminCoupon
{
    public class AdminCouponService : IAdminCouponService
    {

        #region 쿠폰마스터 정보 

        //쿠폰마스터 리스트 가져오기
        public List<SP_ADMIN_COUPON_MASTER_DETAIL_SEL_Result> GetAdminCouponList(string SearchCol, string SearchKeyword, int Page, int PageSize)
        {

            List<SP_ADMIN_COUPON_MASTER_DETAIL_SEL_Result> lst = new List<SP_ADMIN_COUPON_MASTER_DETAIL_SEL_Result>();
            using (AdminCouponEntities AdmCouponContext = new AdminCouponEntities())
            {
                /**try {**/
                lst = AdmCouponContext.SP_ADMIN_COUPON_MASTER_SEL(Page, PageSize, SearchCol, SearchKeyword).ToList();
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;

        }

        //쿠폰마스터  row수 가져오기 
        public int GetAdminCouponListCnt(string SearchCol, string SearchKeyword)
        {

            List<SP_ADMIN_COUPON_COMMON_CNT_Result> lst = new List<SP_ADMIN_COUPON_COMMON_CNT_Result>();
            int list_cnt = 0;

            using (AdminCouponEntities AdmCouponContext = new AdminCouponEntities())
            {
                /**try {**/
                lst = AdmCouponContext.SP_ADMIN_COUPON_MASTER_CNT(SearchCol, SearchKeyword).ToList();
                if (lst != null && lst.Count > 0)
                    list_cnt = lst[0].CNT;
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return list_cnt;

        }



        //쿠폰정책 신규생성
        public int InsAdminCouponMaster(TB_COUPON_MASTER Tb, string[] CheckMemGrade)
        {

            int IsSuccess = 1;
            string NewCdCoupon = ""; //신규생성된 쿠폰코드

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {

                    using (AdminCouponEntities AdmCouponContext = new AdminCouponEntities())
                    {
                        ObjectParameter objOutParam = new ObjectParameter("CREATED_CD_COUPON", typeof(string));//sp의 output parameter변수명을 동일하게 사용한다.

                        //마스터정보 insert
                        AdmCouponContext.SP_ADMIN_COUPON_MASTER_INS
                            ( Tb.COUPON_NAME,Tb.COUPON_AD_MSG,Tb.COUPON_USE_DESCRIPTION,Tb.COUPON_GBN
                              ,Tb.COUPON_GBN_M,Tb.RATE_OR_MONEY,Tb.SERVICE_LIFE_GBN,Tb.FIXED_PERIOD_FROM
                              ,Tb.FIXED_PERIOD_TO,Tb.EXRIRED_DAY_FROM_ISSUE_DT,Tb.DOWNLOAD_DATE_FROM,Tb.DOWNLOAD_DATE_TO
                              ,Tb.USABLE_DEVICE_GBN,Tb.PRODUCT_APP_SCOPE_GBN,Tb.MEMBER_APP_SCOPE_GBN,Tb.ISSUE_METHOD_GBN
                              ,Tb.ISSUE_METHOD_WITH_AUTO,Tb.COUPON_NUM_CHECK_TF,Tb.ISSUE_MAX_LIMIT,Tb.MASTER_FROM_DATE,Tb.MASTER_TO_DATE,Tb.USABLE_YN
                              , Tb.COUPON_DISCOUNT_MONEY, Tb.COUPON_DISCOUNT_RATE, objOutParam
                            );

                        NewCdCoupon = objOutParam.Value.ToString();
                      
                        //적용 회원등급 insert
                        for (int i = 0; i < CheckMemGrade.Length; i++)
                        {
                             string[] MemGradeInfo = CheckMemGrade[i].Split('$');
                             AdmCouponContext.SP_ADMIN_COUPON_MEM_GRADE_INS(NewCdCoupon, MemGradeInfo[0], MemGradeInfo[1]);
                        }

                        //전체상품 대상의 쿠폰이라면 지금 모든 상품을 대상으로 사용할 수 있게 INSERT 처리 
                        if(Tb.PRODUCT_APP_SCOPE_GBN == "B" )
                        {
                            AdmCouponContext.SP_ADMIN_COUPON_PRODUCT_CREATE_ALL_INS(NewCdCoupon);
                        }


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


        //쿠폰정책 수정
        public int UpdateAdminCouponMaster(TB_COUPON_MASTER Tb, string[] CheckMemGrade , string CdCoupon)
        {

            int IsSuccess = 1;
       
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AdminCouponEntities AdmCouponContext = new AdminCouponEntities())
                    {
                      
                        //마스터정보 insert
                        AdmCouponContext.SP_ADMIN_COUPON_MASTER_UPD
                            (CdCoupon, Tb.COUPON_NAME, Tb.COUPON_AD_MSG, Tb.COUPON_USE_DESCRIPTION, Tb.FIXED_PERIOD_FROM, Tb.FIXED_PERIOD_TO, Tb.EXRIRED_DAY_FROM_ISSUE_DT
                            , Tb.MASTER_FROM_DATE, Tb.MASTER_TO_DATE, Tb.USABLE_YN);


                        //적용 회원등급 삭제
                        AdmCouponContext.SP_ADMIN_COUPON_MEMBER_GRADE_DEL(CdCoupon);

                        //적용 회원등급 insert
                        for (int i = 0; i < CheckMemGrade.Length; i++)
                        {
                            string[] MemGradeInfo = CheckMemGrade[i].Split('$');
                            AdmCouponContext.SP_ADMIN_COUPON_MEM_GRADE_INS(CdCoupon, MemGradeInfo[0], MemGradeInfo[1]);
                        }

                        //전체상품 대상의 쿠폰이라면 지금 모든 상품을 대상으로 사용할 수 있게 INSERT 처리 
                        /**
                        if (Tb.PRODUCT_APP_SCOPE_GBN == "B")
                        {
                            AdmCouponContext.SP_ADMIN_COUPON_PRODUCT_CREATE_ALL_INS(NewCdCoupon);
                        }
                        **/
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




       
        //쿠폰마스터 상세정보 가져오기
        public List<SP_ADMIN_COUPON_MASTER_DETAIL_SEL_Result> GetAdminCouponDetail(string CdCoupon)
        {

            List<SP_ADMIN_COUPON_MASTER_DETAIL_SEL_Result> lst = new List<SP_ADMIN_COUPON_MASTER_DETAIL_SEL_Result>();
            using (AdminCouponEntities AdmCouponContext = new AdminCouponEntities())
            {
                /**try {**/
                lst = AdmCouponContext.SP_ADMIN_COUPON_MASTER_DETAIL_SEL(CdCoupon).ToList();
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;

        }

        #endregion 


        #region 쿠폰 적용된 상품  ==================================================================


        //쿠폰적용된 상품 리스트 가져오기
        public List<SP_ADMIN_COUPON_PRODUCT_DEATIL_SEL_Result> GetAdminCouponProeuctList(string SearchCol, string SearchKeyword, int Page, int PageSize, string CdCoupon)
        {

            List<SP_ADMIN_COUPON_PRODUCT_DEATIL_SEL_Result> lst = new List<SP_ADMIN_COUPON_PRODUCT_DEATIL_SEL_Result>();
            using (AdminCouponEntities AdmCouponContext = new AdminCouponEntities())
            {
                /**try {**/
                lst = AdmCouponContext.SP_ADMIN_COUPON_PRODUCT_SEL(Page, PageSize, SearchCol, SearchKeyword,CdCoupon).ToList();
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;

        }

        //쿠폰적용된 상품 row수 가져오기 
        public int GetAdminCouponProductListCnt(string SearchCol, string SearchKeyword, string CdCoupon)
        {

            List<SP_ADMIN_COUPON_COMMON_CNT_Result> lst = new List<SP_ADMIN_COUPON_COMMON_CNT_Result>();
            int list_cnt = 0;

            using (AdminCouponEntities AdmCouponContext = new AdminCouponEntities())
            {
                /**try {**/
                lst = AdmCouponContext.SP_ADMIN_COUPON_PRODUCT_CNT(SearchCol, SearchKeyword,CdCoupon).ToList();
                if (lst != null && lst.Count > 0)
                    list_cnt = lst[0].CNT;
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return list_cnt;

        }



        #endregion




        #region 쿠폰 적용 대상 상품  ==================================================================


        //쿠폰적용 대상상품 리스트 가져오기
        public List<SP_ADMIN_COUPON_PRODUCT_FOR_CREATE_DETAIL_SEL_Result> GetAdminCouponProductForCreateList(string SearchCol, string SearchKeyword, int Page, int PageSize, string CdCoupon)
        {

            List<SP_ADMIN_COUPON_PRODUCT_FOR_CREATE_DETAIL_SEL_Result> lst = new List<SP_ADMIN_COUPON_PRODUCT_FOR_CREATE_DETAIL_SEL_Result>();
            using (AdminCouponEntities AdmCouponContext = new AdminCouponEntities())
            {
                /**try {**/
                lst = AdmCouponContext.SP_ADMIN_COUPON_PRODUCT_FOR_CREATE_SEL(Page, PageSize, SearchCol, SearchKeyword, CdCoupon).ToList();
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;

        }

        //쿠폰적용 대상상품 row수 가져오기 
        public int GetAdminCouponProductForCreateListCnt(string SearchCol, string SearchKeyword, string CdCoupon)
        {

            List<SP_ADMIN_COUPON_COMMON_CNT_Result> lst = new List<SP_ADMIN_COUPON_COMMON_CNT_Result>();
            int list_cnt = 0;

            using (AdminCouponEntities AdmCouponContext = new AdminCouponEntities())
            {
                /**try {**/
                lst = AdmCouponContext.SP_ADMIN_COUPON_PRODUCT_FOR_CREATE_CNT(SearchCol, SearchKeyword, CdCoupon).ToList();
                if (lst != null && lst.Count > 0)
                    list_cnt = lst[0].CNT;
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return list_cnt;

        }



        //쿠폰 상품 적용 
        public int InsAdminCouponProduct(List<Tuple<string, string>> ListCdCouponVsPcodeForIns)
        {

            int IsSuccess = 1;
            //string NewCdPromotionProduct = ""; //신규생성된 프로모션코드

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {

                    using (AdminCouponEntities AdmCouponContext = new AdminCouponEntities())
                    {
                        //ObjectParameter objOutParam = new ObjectParameter("CD_PROMOTION_PRODUCT", typeof(string));//sp의 output parameter변수명을 동일하게 사용한다.

                        foreach (var TupleItem in ListCdCouponVsPcodeForIns )
                        {

                            AdmCouponContext.SP_ADMIN_COUPON_PRODUCT_CREATE_INS(TupleItem.Item1,TupleItem.Item2);

                        }

                       
                        //NewCdPromotionProduct = objOutParam.Value.ToString();
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


        #region 발행된 쿠폰 ==========================================================================



        //발행된 쿠폰 리스트 가져오기
        public List<SP_ADMIN_COUPON_ISSUED_DETAIL_SEL_Result> GetAdminCouponIssuedList(string SearchCol, string SearchKeyword, int Page, int PageSize, string CdCoupon)
        {

            List<SP_ADMIN_COUPON_ISSUED_DETAIL_SEL_Result> lst = new List<SP_ADMIN_COUPON_ISSUED_DETAIL_SEL_Result>();
            using (AdminCouponEntities AdmCouponContext = new AdminCouponEntities())
            {
                /**try {**/
                lst = AdmCouponContext.SP_ADMIN_COUPON_ISSUED_LIST_SEL(Page, PageSize, SearchCol, SearchKeyword,CdCoupon).ToList();
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;

        }

        //쿠폰적용 대상상품 row수 가져오기 
        public int GetAdminCouponIssuedListCnt(string SearchCol, string SearchKeyword, string CdCoupon)
        {

            List<SP_ADMIN_COUPON_COMMON_CNT_Result> lst = new List<SP_ADMIN_COUPON_COMMON_CNT_Result>();
            int list_cnt = 0;

            using (AdminCouponEntities AdmCouponContext = new AdminCouponEntities())
            {
                /**try {**/
                lst = AdmCouponContext.SP_ADMIN_COUPON_ISSUED_CNT(SearchCol, SearchKeyword, CdCoupon).ToList();
                if (lst != null && lst.Count > 0)
                    list_cnt = lst[0].CNT;
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return list_cnt;

        }

        #endregion 


        #region 쿠폰 발행 =============================================================================================


        //쿠폰발행대상 회원등급 리스트 가져오기
        public List<SP_ADMIN_COUPON_MEMBER_GRADE_SEL_Result> GetAdminCouponMemberGradeList(string CdCoupon)
        {

            List<SP_ADMIN_COUPON_MEMBER_GRADE_SEL_Result> lst = new List<SP_ADMIN_COUPON_MEMBER_GRADE_SEL_Result>();
            using (AdminCouponEntities AdmCouponContext = new AdminCouponEntities())
            {
                /**try {**/
                lst = AdmCouponContext.SP_ADMIN_COUPON_MEMBER_GRADE_SEL(CdCoupon).ToList();
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;

        }


        //쿠폰발행대상 상품 Count 가져오기
        public int GetAdminCouponProductUsableCnt(string CdCoupon)
        {

            List<SP_ADMIN_COUPON_COMMON_CNT_Result> lst = new List<SP_ADMIN_COUPON_COMMON_CNT_Result>();
            int list_cnt = 0;

            using (AdminCouponEntities AdmCouponContext = new AdminCouponEntities())
            {
                /**try {**/
                lst = AdmCouponContext.SP_ADMIN_COUPON_PRODUCT_USABLE_CNT_SEL(CdCoupon).ToList();
                if (lst != null && lst.Count > 0)
                    list_cnt = lst[0].CNT;
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return list_cnt;

        }



        //쿠폰발행 - 지불쿠폰 OR 배송쿠폰/인증번호 필요없는 쿠폰/수동발행/일괄발행 INSERT(admin)
        public int InsAdminCouponIssue_WithNoNumcheck_ManualEntire(string CdCoupon, string AdminId)
        {

            int ResultCode = -999; //실행결과코드

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AdminCouponEntities AdmCouponContext = new AdminCouponEntities())
                    {
                        ObjectParameter objOutParam01 = new ObjectParameter("EXCUTE_RESULT", typeof(int));//sp의 output parameter변수명을 동일하게 사용한다.
                        AdmCouponContext.SP_ADMIN_COUPON_ISSUE_WITH_NO_NUMCHECK_MANUAL_ENTIRE_INS(CdCoupon, AdminId, objOutParam01);
                        ResultCode = Convert.ToInt32(objOutParam01.Value.ToString());
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    Transaction.Current.Rollback();
                    scope.Dispose();
                }
            }

            return ResultCode;
        }




        //쿠폰발행 - 지불쿠폰 OR 배송쿠폰/인증번호 필요없는 쿠폰/수동발행/개별발행 INSERT(admin) 
        public int InsAdminCouponIssue_WithNoNumcheck_Manual_Single(string CdCoupon, int CouponMoney, int CouponDiscountRate, string M_id, string AdminId, string IssuedMemo)
        {

            int ResultCode = -999; //실행결과코드

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AdminCouponEntities AdmCouponContext = new AdminCouponEntities())
                    {
                        ObjectParameter objOutParam01 = new ObjectParameter("EXCUTE_RESULT", typeof(int));//sp의 output parameter변수명을 동일하게 사용한다.
                        AdmCouponContext.SP_ADMIN_COUPON_ISSUE_WITH_NO_NUMCHECK_MANUAL_SINGLE_INS(CdCoupon, CouponMoney, CouponDiscountRate, M_id, AdminId, IssuedMemo, objOutParam01);
                        ResultCode = Convert.ToInt32(objOutParam01.Value.ToString());
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    Transaction.Current.Rollback();
                    scope.Dispose();
                }
            }

            return ResultCode;
        }



        //새로운 상품 등록시 , '상품적용범위' 가 전체상품인 쿠폰마스터에 이 상품을 추가
        public int InsAdminCouponProductCreateOnAddingPrd(string P_Code)
        {
            int IsSuccess = 1;
  
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AdminCouponEntities AdmCouponContext = new AdminCouponEntities())
                    {                      
                        //마스터정보 insert
                        AdmCouponContext.SP_ADMIN_COUPON_PRODUCT_CREATE_INS_ON_ADDING_PRODUCT(P_Code);
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



        //발행된 쿠폰의 사용상태 변경 
        public void UpdateCouponUsableTf(int IdxCouponNumber , string UsableTf )
        {
            using (AdminCouponEntities AdmCouponContext = new AdminCouponEntities())
            {                      
                AdmCouponContext.SP_ADMIN_COUPON_USABLE_CHANGE_UPT(IdxCouponNumber, UsableTf);
            }
        }



        #endregion

    }
}
