﻿using System;
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
        //전체할인 프로모션 리스트 가져오기
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

        //전체할인 프로모션 row수 가져오기 
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
            string NewCdPromotionProduct = ""; //신규생성된 프로모션코드

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {

                    using (AdminCouponEntities AdmCouponContext = new AdminCouponEntities())
                    {
                        //ObjectParameter objOutParam = new ObjectParameter("CD_PROMOTION_PRODUCT", typeof(string));//sp의 output parameter변수명을 동일하게 사용한다.

                        AdmCouponContext.SP_ADMIN_COUPON_MASTER_INS
                            ( Tb.COUPON_NAME,Tb.COUPON_AD_MSG,Tb.COUPON_USE_DESCRIPTION,Tb.COUPON_GBN
                              ,Tb.COUPON_GBN_M,Tb.RATE_OR_MONEY,Tb.SERVICE_LIFE_GBN,Tb.FIXED_PERIOD_FROM
                              ,Tb.FIXED_PERIOD_TO,Tb.EXRIRED_DAY_FROM_ISSUE_DT,Tb.DOWNLOAD_DATE_FROM,Tb.DOWNLOAD_DATE_TO
                              ,Tb.USABLE_DEVICE_GBN,Tb.PRODUCT_APP_SCOPE_GBN,Tb.PRODUCT_APP_SCOPE_GBN,Tb.ISSUE_METHOD_GBN
                              ,Tb.ISSUE_METHOD_WITH_AUTO,Tb.COUPON_NUM_CHECK_TF,Tb.ISSUE_MAX_LIMIT,Tb.MASTER_FROM_DATE,Tb.MASTER_TO_DATE,Tb.USABLE_YN
                              ,Tb.COUPON_DISCOUNT_MONEY,Tb.COUPON_DISCOUNT_RATE
                            );

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

    }
}