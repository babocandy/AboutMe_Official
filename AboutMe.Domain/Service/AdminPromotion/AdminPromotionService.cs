using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Transactions;
using System.Data.Entity.Core.Objects;
using AboutMe.Domain.Entity.AdminPromotion;

namespace AboutMe.Domain.Service.AdminPromotion
{
    public class AdminPromotionService : IAdminPromotionService
    {

        #region 전체할인 프로모션 관리 ------------------------------------------------------------------------------------------------

        //전체할인 프로모션 리스트 가져오기
        public List<SP_ADMIN_PROMOTION_BY_TOTAL_DETAIL_SEL_Result> GetAdminPromotionByTotalList(string SearchCol, string SearchKeyword, int Page, int PageSize)
        {

            List<SP_ADMIN_PROMOTION_BY_TOTAL_DETAIL_SEL_Result> lst = new List<SP_ADMIN_PROMOTION_BY_TOTAL_DETAIL_SEL_Result>();
            using (AdminPromotion3Entities AdmPromotionContext = new AdminPromotion3Entities())
            {
                /**try {**/
                lst = AdmPromotionContext.SP_ADMIN_PROMOTION_BY_TOTAL_SEL(Page,PageSize,SearchCol,SearchKeyword).ToList();
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;

        }

        //전체할인 프로모션 row수 가져오기 
        public int GetAdminPromotionByTotalListCnt(string SearchCol, string SearchKeyword)
        {

            List<SP_ADMIN_PROMOTION_COMMON_CNT_Result> lst = new List<SP_ADMIN_PROMOTION_COMMON_CNT_Result>();
            int list_cnt = 0;

            using (AdminPromotion3Entities AdmPromotionContext = new AdminPromotion3Entities())
            {
                /**try {**/
                lst = AdmPromotionContext.SP_ADMIN_PROMOTION_BY_TOTAL_CNT(SearchCol, SearchKeyword).ToList();
                if (lst != null && lst.Count > 0)
                    list_cnt = lst[0].CNT;
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return list_cnt;

        }


        //전체할인 프로모션 중복여부 확인 
        public int GetAdminPromotionByTotalDupSel(string PmoTotalCategory, DateTime PmoTotalDateFrom , DateTime PmoTotalDateTo)
        {

            //PmoTotalDateFrom


            List<SP_ADMIN_PROMOTION_COMMON_CNT_Result> lst = new List<SP_ADMIN_PROMOTION_COMMON_CNT_Result>();
            int list_cnt = 0;

            using (AdminPromotion3Entities AdmPromotionContext = new AdminPromotion3Entities())
            {
                /**try {**/
                lst = AdmPromotionContext.SP_ADMIN_PROMOTION_BY_TOTAL_DUP_SEL(PmoTotalCategory,PmoTotalDateFrom,PmoTotalDateTo).ToList();
                if (lst != null && lst.Count > 0)
                    list_cnt = lst[0].CNT;
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return list_cnt;

        }



        //전체할인 프로모션 중복여부 확인 
        public int GetAdminPromotionByTotalDupAtUpdateSel(string PmoTotalCategory, DateTime PmoTotalDateFrom, DateTime PmoTotalDateTo,string CdPromotionTotal)
        {

            //PmoTotalDateFrom

            List<SP_ADMIN_PROMOTION_COMMON_CNT_Result> lst = new List<SP_ADMIN_PROMOTION_COMMON_CNT_Result>();
            int list_cnt = 0;

            using (AdminPromotion3Entities AdmPromotionContext = new AdminPromotion3Entities())
            {
                /**try {**/
                lst = AdmPromotionContext.SP_ADMIN_PROMOTION_BY_TOTAL_DUP_AT_UPDATE_SEL(PmoTotalCategory, PmoTotalDateFrom, PmoTotalDateTo,CdPromotionTotal).ToList();
                if (lst != null && lst.Count > 0)
                    list_cnt = lst[0].CNT;
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return list_cnt;

        }



       

        //전체할인 프로모션 신규생성
        public int InsAdminPromotionByTotal(TB_PROMOTION_BY_TOTAL Tb , string[] CheckMemGrade)
        {

            int IsSuccess = 1;
            string NewCdPromotionTotal = ""; //신규생성된 프로모션코드

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AdminPromotion3Entities AdmPromotionContext = new AdminPromotion3Entities())
                    {
                        ObjectParameter objOutParam = new ObjectParameter("CD_PROMOTION_TOTAL", typeof(string));//sp의 output parameter변수명을 동일하게 사용한다.
                        AdmPromotionContext.SP_ADMIN_PROMOTION_BY_TOTAL_INS(Tb.PMO_TOTAL_NAME,Tb.PMO_TOTAL_CATEGORY ,Tb.PMO_TOTAL_RATE_OR_MONEY, Tb.PMO_TOTAL_DISCOUNT_RATE, Tb.PMO_TOTAL_DISCOUNT_MONEY,
                            Tb.PMO_TOTAL_DATE_FROM, Tb.PMO_TOTAL_DATE_TO, Tb.USABLE_YN, objOutParam);

                        NewCdPromotionTotal = objOutParam.Value.ToString();
                    }


                    using (AdminPromotion3Entities AdmPromotionContext = new AdminPromotion3Entities())
                    {
                        for (int i = 0; i < CheckMemGrade.Length; i++)
                        {
                             string[] MemGradeInfo =  CheckMemGrade[i].Split('$');
                             AdmPromotionContext.SP_ADMIN_PROMOTION_BY_TOTAL_MEM_GRADE_INS(NewCdPromotionTotal, MemGradeInfo[0], MemGradeInfo[1], "Y");
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


        //전체할인 프로모션 상세정보 가져오기
        public List<SP_ADMIN_PROMOTION_BY_TOTAL_DETAIL_SEL_Result> GetAdminPromotionByTotalDetail(string CdPromotionTotal)
        {

            List<SP_ADMIN_PROMOTION_BY_TOTAL_DETAIL_SEL_Result> lst = new List<SP_ADMIN_PROMOTION_BY_TOTAL_DETAIL_SEL_Result>();
            using (AdminPromotion3Entities AdmPromotionContext = new AdminPromotion3Entities())
            {
                /**try {**/
                lst = AdmPromotionContext.SP_ADMIN_PROMOTION_BY_TOTAL_DETAIL_SEL(CdPromotionTotal).ToList();
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;

        }



        //전체할인 프로모션 관련 회원등급정보 가져오기
        public List<SP_ADMIN_PROMOTION_BY_TOTAL_MEM_GRADE_WITH_CD_SEL_Result> GetAdminPromotionByTotalMemGradeWithCd(string CdPromotionTotal)
        {

            List<SP_ADMIN_PROMOTION_BY_TOTAL_MEM_GRADE_WITH_CD_SEL_Result> lst = new List<SP_ADMIN_PROMOTION_BY_TOTAL_MEM_GRADE_WITH_CD_SEL_Result>();
            using (AdminPromotion3Entities AdmPromotionContext = new AdminPromotion3Entities())
            {
                /**try {**/
                lst = AdmPromotionContext.SP_ADMIN_PROMOTION_BY_TOTAL_MEM_GRADE_WITH_CD_SEL(CdPromotionTotal).ToList();
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;

        }




        //전체할인 프로모션 수정
        public int UpdateAdminPromotionByTotal(TB_PROMOTION_BY_TOTAL Tb, string[] CheckMemGrade , string CdPromotionTotal )
        {

            int IsSuccess = 1;
          

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AdminPromotion3Entities AdmPromotionContext = new AdminPromotion3Entities())
                    {
                        AdmPromotionContext.SP_ADMIN_PROMOTION_BY_TOTAL_UPDATE(Tb.PMO_TOTAL_NAME, Tb.PMO_TOTAL_DISCOUNT_RATE, Tb.PMO_TOTAL_DISCOUNT_MONEY,
                            Tb.PMO_TOTAL_DATE_FROM, Tb.PMO_TOTAL_DATE_TO, Tb.USABLE_YN, CdPromotionTotal);
                    }


                    using (AdminPromotion3Entities AdmPromotionContext = new AdminPromotion3Entities())
                    {
                        AdmPromotionContext.SP_ADMIN_PROMOTION_BY_TOTAL_MEM_GRADE_DEL( CdPromotionTotal);
                    }

                    if (CheckMemGrade != null)
                    {
                        using (AdminPromotion3Entities AdmPromotionContext = new AdminPromotion3Entities())
                        {
                            for (int i = 0; i < CheckMemGrade.Length; i++)
                            {
                                string[] MemGradeInfo = CheckMemGrade[i].Split('$');
                                AdmPromotionContext.SP_ADMIN_PROMOTION_BY_TOTAL_MEM_GRADE_INS(CdPromotionTotal, MemGradeInfo[0], MemGradeInfo[1], "Y");
                            }
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


        //전체할인 USABLE_YN = Y 인 리스트 가져오기 

        public List<SP_ADMIN_PROMOTION_BY_TOTAL_ACTIVE_LIST_SEL_Result> GetAdminPromotionByTotalActiveList()
        {

            List<SP_ADMIN_PROMOTION_BY_TOTAL_ACTIVE_LIST_SEL_Result> lst = new List<SP_ADMIN_PROMOTION_BY_TOTAL_ACTIVE_LIST_SEL_Result>();
            using (AdminPromotion3Entities AdmPromotionContext = new AdminPromotion3Entities())
            {

                lst = AdmPromotionContext.SP_ADMIN_PROMOTION_BY_TOTAL_ACTIVE_LIST_SEL().ToList();

            }

            return lst;

        }



        #endregion

        #region 상품별할인 프로모션관리 ---------------------------------------------------------------------------------------

        //상품별 할인 프로모션 리스트 가져오기
        public List<SP_ADMIN_PROMOTION_BY_PRODUCT_DETAIL_SEL_Result> GetAdminPromotionByProductList(string SearchCol, string SearchKeyword, int Page, int PageSize)
        {

            List<SP_ADMIN_PROMOTION_BY_PRODUCT_DETAIL_SEL_Result> lst = new List<SP_ADMIN_PROMOTION_BY_PRODUCT_DETAIL_SEL_Result>();
            using (AdminPromotion3Entities AdmPromotionContext = new AdminPromotion3Entities())
            {
                /**try {**/
                lst = AdmPromotionContext.SP_ADMIN_PROMOTION_BY_PRODUCT_SEL(Page, PageSize, SearchCol, SearchKeyword).ToList();
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;

        }

        //상품별할인 프로모션 row수 가져오기 
        public int GetAdminPromotionByProductListCnt(string SearchCol, string SearchKeyword)
        {

            List<SP_ADMIN_PROMOTION_COMMON_CNT_Result> lst = new List<SP_ADMIN_PROMOTION_COMMON_CNT_Result>();
            int list_cnt = 0;

            using (AdminPromotion3Entities AdmPromotionContext = new AdminPromotion3Entities())
            {
                /**try {**/
                lst = AdmPromotionContext.SP_ADMIN_PROMOTION_BY_PRODUCT_CNT(SearchCol, SearchKeyword).ToList();
                if (lst != null && lst.Count > 0)
                    list_cnt = lst[0].CNT;
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return list_cnt;

        }

        //상품별 할인 프로모션 Detail 가져오기
        public List<SP_ADMIN_PROMOTION_BY_PRODUCT_DETAIL_SEL_Result> GetAdminPromotionByProductDetail(string CdPromotionProduct)
        {

            List<SP_ADMIN_PROMOTION_BY_PRODUCT_DETAIL_SEL_Result> lst = new List<SP_ADMIN_PROMOTION_BY_PRODUCT_DETAIL_SEL_Result>();
            using (AdminPromotion3Entities AdmPromotionContext = new AdminPromotion3Entities())
            {
                /**try {**/
                lst = AdmPromotionContext.SP_ADMIN_PROMOTION_BY_PRODUCT_DETAIL_SEL(CdPromotionProduct).ToList();
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;

        }

        //상품별할인 프로모션 신규생성
        public int InsAdminPromotionByProduct(TB_PROMOTION_BY_PRODUCT Tb)
        {

            int IsSuccess = 1;
            string NewCdPromotionProduct = ""; //신규생성된 프로모션코드

            using (TransactionScope scope = new TransactionScope())
            {
                try{
              
                    using (AdminPromotion3Entities AdmPromotionContext = new AdminPromotion3Entities())
                    {
                        ObjectParameter objOutParam = new ObjectParameter("CD_PROMOTION_PRODUCT", typeof(string));//sp의 output parameter변수명을 동일하게 사용한다.
                        AdmPromotionContext.SP_ADMIN_PROMOTION_BY_PRODUCT_INS(Tb.PMO_PRODUCT_NAME, Tb.PMO_PRODUCT_CATEGORY,Tb.PMO_PRODUCT_MAIN_TITLE,Tb.PMO_PRODUCT_SUB_TITLE,Tb.PMO_PRODUCT_SHOPPING_TIP 
                            ,Tb.PMO_PRODUCT_MAIN_IMG
                            ,Tb.PMO_PRODUCT_PC_MAINPG_IMG
                            ,Tb.PMO_PRODUCT_PC_MAINPG_SMALL_IMG
                            ,Tb.PMO_PRODUCT_PC_EVENT_MAINPG_IMG
                            ,Tb.PMO_PRODUCT_MOBILE_MAIN_IMG
                            ,Tb.PMO_PRODUCT_MOBILE_MAINPG_IMG
                            ,Tb.PMO_PRODUCT_MOBILE_EVENT_MAINPG_IMG
                            ,Tb.PMO_PRODUCT_RATE_OR_MONEY, Tb.PMO_PRODUCT_DISCOUNT_RATE, Tb.PMO_PRODUCT_DISCOUNT_MONEY,
                            Tb.PMO_SET_DISCOUNT_CNT,Tb.PMO_ONEONE_MULTIPLE_CNT,
                            Tb.PMO_PRODUCT_DATE_FROM, Tb.PMO_PRODUCT_DATE_TO, Tb.USABLE_YN, objOutParam);

                        NewCdPromotionProduct = objOutParam.Value.ToString();
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


        //상품별 할인 프로모션 중복여부 확인 
        public int GetAdminPromotionByProductDupSel(string PmoProductCategory, DateTime PmoProductDateFrom, DateTime PmoProductDateTo)
        {

            //PmoProductDateFrom


            List<SP_ADMIN_PROMOTION_COMMON_CNT_Result> lst = new List<SP_ADMIN_PROMOTION_COMMON_CNT_Result>();
            int list_cnt = 0;

            using (AdminPromotion3Entities AdmPromotionContext = new AdminPromotion3Entities())
            {
                /**try {**/
                lst = AdmPromotionContext.SP_ADMIN_PROMOTION_BY_PRODUCT_DUP_SEL(PmoProductCategory, PmoProductDateFrom, PmoProductDateTo).ToList();
                if (lst != null && lst.Count > 0)
                    list_cnt = lst[0].CNT;
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return list_cnt;

        }

        //상품별할인 프로모션 정보수정
        public int UpdateAdminPromotionByProduct(TB_PROMOTION_BY_PRODUCT Tb, string CdPromotionProduct)
        {

            int IsSuccess = 1;
            //string NewCdPromotionProduct = ""; //신규생성된 프로모션코드

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AdminPromotion3Entities AdmPromotionContext = new AdminPromotion3Entities())
                    {
                        //ObjectParameter objOutParam = new ObjectParameter("CD_PROMOTION_PRODUCT", typeof(string));//sp의 output parameter변수명을 동일하게 사용한다.
                        
                        AdmPromotionContext.SP_ADMIN_PROMOTION_BY_PRODUCT_UPDATE(Tb.PMO_PRODUCT_NAME, Tb.PMO_PRODUCT_MAIN_TITLE, Tb.PMO_PRODUCT_SUB_TITLE, Tb.PMO_PRODUCT_SHOPPING_TIP
                            , Tb.PMO_PRODUCT_MAIN_IMG
                            , Tb.PMO_PRODUCT_PC_MAINPG_IMG
                            , Tb.PMO_PRODUCT_PC_MAINPG_SMALL_IMG
                            , Tb.PMO_PRODUCT_PC_EVENT_MAINPG_IMG
                            , Tb.PMO_PRODUCT_MOBILE_MAIN_IMG
                            , Tb.PMO_PRODUCT_MOBILE_MAINPG_IMG
                            , Tb.PMO_PRODUCT_MOBILE_EVENT_MAINPG_IMG
                            , Tb.PMO_PRODUCT_RATE_OR_MONEY, Tb.PMO_PRODUCT_DISCOUNT_RATE, Tb.PMO_PRODUCT_DISCOUNT_MONEY,
                            Tb.PMO_SET_DISCOUNT_CNT, Tb.PMO_ONEONE_MULTIPLE_CNT,
                            Tb.PMO_PRODUCT_DATE_FROM, Tb.PMO_PRODUCT_DATE_TO, Tb.USABLE_YN, CdPromotionProduct);

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



        //상품별 할인 프로모션 기간 정보 수정시 ....타프로모션에 중복된 상품이 있는지의 여부 확인 
        public int GetAdminPromotionByProductForUpdateAllProductCheckDupSel(string CdPromotionProduct, DateTime TargetPmoProductDateFrom, DateTime TargetPmoProductDateTo)
        {

            //PmoProductDateFrom


            List<SP_ADMIN_PROMOTION_COMMON_CNT_Result> lst = new List<SP_ADMIN_PROMOTION_COMMON_CNT_Result>();
            int list_cnt = 0;

            using (AdminPromotion3Entities AdmPromotionContext = new AdminPromotion3Entities())
            {
                /**try {**/
                lst = AdmPromotionContext.SP_ADMIN_PROMOTION_UPDATE_ALL_PRODUCT_PRICE_DUP_SEL(CdPromotionProduct, TargetPmoProductDateFrom, TargetPmoProductDateTo).ToList();
                if (lst != null && lst.Count > 0)
                    list_cnt = lst[0].CNT;
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return list_cnt;

        }



        //상품별 할인 프로모션 중복여부 확인 
        public int GetAdminPromotionByProductForUpdateDupSel(string PmoProductCategory, DateTime TargetPmoProductDateFrom, DateTime TargetPmoProductDateTo,string CdPromotionProduct)
        {

            //PmoProductDateFrom


            List<SP_ADMIN_PROMOTION_COMMON_CNT_Result> lst = new List<SP_ADMIN_PROMOTION_COMMON_CNT_Result>();
            int list_cnt = 0;

            using (AdminPromotion3Entities AdmPromotionContext = new AdminPromotion3Entities())
            {
                /**try {**/
                lst = AdmPromotionContext.SP_ADMIN_PROMOTION_BY_PRODUCT_FOR_UPDATE_DUP_SEL(PmoProductCategory, TargetPmoProductDateFrom, TargetPmoProductDateTo, CdPromotionProduct).ToList();
                if (lst != null && lst.Count > 0)
                    list_cnt = lst[0].CNT;
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return list_cnt;

        }

        #region 상품별 Pricing ==================================================================
       
        // 상품별할인 프로모션에 소속된 상품(가격)리스트
        public List<SP_ADMIN_PROMOTION_BY_PRODUCT_PRICE_SEL_Result> GetAdminPromotionByProductPricingList(string CdPromotionProduct)
        {

            List<SP_ADMIN_PROMOTION_BY_PRODUCT_PRICE_SEL_Result> lst = new List<SP_ADMIN_PROMOTION_BY_PRODUCT_PRICE_SEL_Result>();
            using (AdminPromotion3Entities AdmPromotionContext = new AdminPromotion3Entities())
            {
                /**try {**/
                lst = AdmPromotionContext.SP_ADMIN_PROMOTION_BY_PRODUCT_PRICE_SEL(CdPromotionProduct).ToList();
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;

        }

        //상품별 할인 프로모션에 소속된 상품(가격)의 전체할인 적용여부 정보    
        public List<SP_ADMIN_PROMOTION_BY_PRODUCT_VS_TOTAL_SEL_Result> GetAdminPromotionByProductVsTotalList(string CdPromotionProduct)
        {

            List<SP_ADMIN_PROMOTION_BY_PRODUCT_VS_TOTAL_SEL_Result> lst = new List<SP_ADMIN_PROMOTION_BY_PRODUCT_VS_TOTAL_SEL_Result>();
            using (AdminPromotion3Entities AdmPromotionContext = new AdminPromotion3Entities())
            {
               
                lst = AdmPromotionContext.SP_ADMIN_PROMOTION_BY_PRODUCT_VS_TOTAL_SEL(CdPromotionProduct).ToList();
            
            }

            return lst;

        }


        //상품별 할인 프로모션 중복여부 확인 - 타 프로모션에 활성화된 상품이 있는지 모두 SCAN
        public int GetAdminPromotionByProductPricingAllDupSel(string CdPromotionProduct , string Pcode)
        {

            //PmoProductDateFrom


            List<SP_ADMIN_PROMOTION_COMMON_CNT_Result> lst = new List<SP_ADMIN_PROMOTION_COMMON_CNT_Result>();
            int list_cnt = 0;

            using (AdminPromotion3Entities AdmPromotionContext = new AdminPromotion3Entities())
            {
                /**try {**/
                lst = AdmPromotionContext.SP_ADMIN_PROMOTION_ALL_PRODUCT_PRICE_DUP_SEL(Pcode,CdPromotionProduct).ToList();
                if (lst != null && lst.Count > 0)
                    list_cnt = lst[0].CNT;
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return list_cnt;

        }



        //상품별할인 프로모션 신규생성
        public int InsAdminPromotionByProductPricing(TB_PROMOTION_BY_PRODUCT_PRICE Tb, string CdPromotionProduct, string[] CheckCdPromotiontTotal)
        {

            int IsSuccess = 1;
            int NewPricingIdx = 0 ; //신규생성된 가격 일련번호

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {

                    using (AdminPromotion3Entities AdmPromotionContext = new AdminPromotion3Entities())
                    {
                        ObjectParameter objOutParam = new ObjectParameter("OUTPUT_PRODUCT_PRICE_IDX", typeof(int));//sp의 output parameter변수명을 동일하게 사용한다.
                        AdmPromotionContext.SP_ADMIN_PROMOTION_PRODUCT_PRICE_INS(
                             CdPromotionProduct
                            ,Tb.P_CODE
                            ,Tb.P_DISCOUNT_PRICE
                            ,Tb.PMO_PRODUCT_RATE_OR_MONEY
                            ,Tb.PMO_PRICE
                            ,Tb.PMO_ONE_ONE_P_CODE
                            ,Tb.PMO_ONE_ONE_PRICE
                            ,Tb.USABLE_YN
                            , objOutParam);
                        NewPricingIdx = int.Parse(objOutParam.Value.ToString());
                    }

                   

                    if (CheckCdPromotiontTotal != null)
                    {
                        using (AdminPromotion3Entities AdmPromotionContext = new AdminPromotion3Entities())
                        {
                            for (int i = 0; i < CheckCdPromotiontTotal.Length; i++)
                            {
                                AdmPromotionContext.SP_ADMIN_PROMOTION_PRODUCT_PRICE_VS_TOTAL_INS(CdPromotionProduct, CheckCdPromotiontTotal[i],NewPricingIdx,Tb.P_CODE,Tb.USABLE_YN);
                            }
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



        //상품별할인 프로모션 신규생성 - 복수의 상품코드 일괄처리 
        public int InsAdminPromotionByProductPricingMultiple(List<PromotionByProductReg> Tb_PmoProdEntity, string CdPromotionProduct)
        {

            int IsSuccess = 1;
            int NewPricingIdx = 0; //신규생성된 가격 일련번호

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    foreach (PromotionByProductReg Tb in Tb_PmoProdEntity)
                    {

                        using (AdminPromotion3Entities AdmPromotionContext = new AdminPromotion3Entities())
                        {
                            ObjectParameter objOutParam = new ObjectParameter("OUTPUT_PRODUCT_PRICE_IDX", typeof(int));//sp의 output parameter변수명을 동일하게 사용한다.
                            AdmPromotionContext.SP_ADMIN_PROMOTION_PRODUCT_PRICE_INS(
                                 CdPromotionProduct
                                , Tb.P_CODE
                                , Tb.P_DISCOUNT_PRICE
                                , "M"
                                , Tb.PMO_PRICE
                                , Tb.P_CODE //Tb.PMO_ONE_ONE_P_CODE
                                , Tb.PMO_PRICE //Tb.PMO_ONE_ONE_PRICE
                                , Tb.USABLE_YN
                                , objOutParam);
                            NewPricingIdx = int.Parse(objOutParam.Value.ToString());
                        }

                        string[] CheckCdPromotiontTotal = Tb.CD_PROMOTION_TOTAL_LST.Split(',');

                        if (CheckCdPromotiontTotal != null)
                        {
                            using (AdminPromotion3Entities AdmPromotionContext = new AdminPromotion3Entities())
                            {
                                for (int i = 0; i < CheckCdPromotiontTotal.Length; i++)
                                {
                                    if (CheckCdPromotiontTotal[i].Trim() != "")
                                    {
                                        AdmPromotionContext.SP_ADMIN_PROMOTION_PRODUCT_PRICE_VS_TOTAL_INS(CdPromotionProduct, CheckCdPromotiontTotal[i], NewPricingIdx, Tb.P_CODE, Tb.USABLE_YN);
                                    }
                                }
                            }
                        }
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    Transaction.Current.Rollback();
                    scope.Dispose();
                    IsSuccess = -1;
                    
                    //에러를 강제로 발생시킨다.
                    throw new Exception("Transction Error", ex);

                    //LogHelper.Error("TEST EXCEPTION", ex);

                }

            }


            return IsSuccess;
        }


        //상품코드 유효성 Check
        public int GetAdminPromotionProductCodeCheck(string Pcode)
        {

            List<SP_ADMIN_PROMOTION_COMMON_CNT_Result> lst = new List<SP_ADMIN_PROMOTION_COMMON_CNT_Result>();
            int list_cnt = 0;

            using (AdminPromotion3Entities AdmPromotionContext = new AdminPromotion3Entities())
            {
                /**try {**/
                lst = AdmPromotionContext.SP_ADMIN_PROMOTION_PRODUCT_CODE_CHECK_SEL(Pcode).ToList();
                if (lst != null && lst.Count > 0)
                    list_cnt = lst[0].CNT;
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return list_cnt;
        }



        //상품별할인 상품별 정보 업데이트
        //public int UpdateAdminPromotionByProductPricing(TB_PROMOTION_BY_PRODUCT_PRICE Tb, string CdPromotionProduct, string[] CheckCdPromotiontTotal)
        public int UpdateAdminPromotionByProductPricing(string UsableYN, string CdPromotionProduct, string[] CheckCdPromotiontTotal , string Pcode , int idx )
        {

            int IsSuccess = 1;
          

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {

                    using (AdminPromotion3Entities AdmPromotionContext = new AdminPromotion3Entities())
                    {
                        AdmPromotionContext.SP_ADMIN_PROMOTION_BY_PRODUCT_PRICE_UPDATE(
                              CdPromotionProduct
                              , UsableYN
                              , Pcode
                          
                              , idx
                            );
                      
                    }


                    /**
                    if (CheckCdPromotiontTotal != null)
                    {
                        using (AdminPromotion3Entities AdmPromotionContext = new AdminPromotion3Entities())
                        {
                            for (int i = 0; i < CheckCdPromotiontTotal.Length; i++)
                            {
                                AdmPromotionContext.SP_ADMIN_PROMOTION_PRODUCT_PRICE_VS_TOTAL_INS(CdPromotionProduct, CheckCdPromotiontTotal[i], NewPricingIdx, Tb.P_CODE, Tb.USABLE_YN);
                            }
                        }
                    }
                     * **/

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



        //상품별 할인 상품정보 (수정시) 프로모션 중복여부 확인 - 타 프로모션에 활성화된 상품이 있는지 모두 SCAN
        public int GetAdminPromotionByProductPricing_IDX_AllDupSel(string CdPromotionProduct, string Pcode, int idx)
        {

            //PmoProductDateFrom


            List<SP_ADMIN_PROMOTION_COMMON_CNT_Result> lst = new List<SP_ADMIN_PROMOTION_COMMON_CNT_Result>();
            int list_cnt = 0;

            using (AdminPromotion3Entities AdmPromotionContext = new AdminPromotion3Entities())
            {
                /**try {**/
                lst = AdmPromotionContext.SP_ADMIN_PROMOTION_ALL_PRODUCT_PRICE_IDX_DUP_SEL(Pcode, CdPromotionProduct, idx).ToList();
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





        #endregion ----------------------------------------------------------------------------------------------------------


    }
}
