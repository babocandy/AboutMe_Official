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

        //다운로드 가능한 쿠폰 - pc버전 
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


        //다운로드 처리  - pc버전 , 번호 인증 필요없는 쿠폰
        public int  UpdateCouponDownload_Pc_Ver_And_NoNumberChk_Ver(string M_Id,int IdxCouponNumber,string UpdateMethod)
        {
            int IsSuccess = 1;
       
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
                    IsSuccess = -1;
                }
            }

            return IsSuccess;

        }

        //사용가능한 쿠폰 리스트 가져오기
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

        //사용가능한 쿠폰리스트 COUNT 가져오기 
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


        //쿠폰별 상품 리스트 가져오기
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

        //쿠폰별 상품 리스트 COUNT 가져오기 
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



    }
}
