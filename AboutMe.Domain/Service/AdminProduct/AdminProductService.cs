using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.AdminProduct;
using System.Data.Entity.Core.Objects;

using System.Transactions;
using AboutMe.Domain.Entity.AdminCoupon;

namespace AboutMe.Domain.Service.AdminProduct
{
    public class AdminProductService : IAdminProductService
    {

        #region 카테고리

        #region 1depth 카테고리 리스트
        public List<SP_ADMIN_CATEGORY_ONE_SEL_Result> GetAdminCategoryOneList()
        {

            List<SP_ADMIN_CATEGORY_ONE_SEL_Result> lst = new List<SP_ADMIN_CATEGORY_ONE_SEL_Result>();
            using (AdminProductEntities AdminProductContext = new AdminProductEntities())
            {
                /**try {**/
                lst = AdminProductContext.SP_ADMIN_CATEGORY_ONE_SEL().ToList();
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;

        }
        #endregion

        #region 1depth 카테고리 등록
        public void InsertAdminCategoryOne(string DEPTH1_NAME)
        {
            using (AdminProductEntities AdminProductContext = new AdminProductEntities())
            {
                AdminProductContext.SP_ADMIN_CATEGORY_ONE_INS(DEPTH1_NAME);
            }
        }
        #endregion

        #region 2depth 카테고리 등록
        public void InsertAdminCategoryTwo(string CATE_GBN, string DEPTH1_CODE, string DEPTH2_NAME)
        {
            using (AdminProductEntities AdminProductContext = new AdminProductEntities())
            {
                AdminProductContext.SP_ADMIN_CATEGORY_TWO_INS(CATE_GBN, DEPTH1_CODE, DEPTH2_NAME);
            }
        }
        #endregion

        #region 3depth 카테고리 등록
        public void InsertAdminCategoryThree(string CATE_GBN, string DEPTH1_CODE, string DEPTH2_CODE, string DEPTH3_NAME)
        {
            using (AdminProductEntities AdminProductContext = new AdminProductEntities())
            {
                AdminProductContext.SP_ADMIN_CATEGORY_THREE_INS(CATE_GBN, DEPTH1_CODE, DEPTH2_CODE, DEPTH3_NAME);
            }
        }
        #endregion

        #region 카테고리 VIEW

        public SP_ADMIN_CATEGORY_VIEW_Result ViewAdminCategory(int IDX)
        {

            SP_ADMIN_CATEGORY_VIEW_Result categoryView;
            
            using (AdminProductEntities AdminProductContext = new AdminProductEntities())
            {
                categoryView = AdminProductContext.SP_ADMIN_CATEGORY_VIEW(IDX).FirstOrDefault();
            }
            return categoryView;
        }

        #endregion

        #region 카테고리 수정 1depth
        public void UpdateAdminCategoryOne(Nullable<int> IDX, string DEPTH1_NAME, string DISPLAY_YN, Nullable<int> RE_SORT)
        {
            using (AdminProductEntities AdminProductContext = new AdminProductEntities())
            {
                AdminProductContext.SP_ADMIN_CATEGORY_ONE_UPD(IDX, DEPTH1_NAME, DISPLAY_YN, RE_SORT);
            }
        }
        #endregion

        #region 카테고리 수정 2depth
        public void UpdateAdminCategoryTwo(Nullable<int> IDX, string DEPTH2_NAME, string DISPLAY_YN, Nullable<int> RE_SORT)
        {
            using (AdminProductEntities AdminProductContext = new AdminProductEntities())
            {
                AdminProductContext.SP_ADMIN_CATEGORY_TWO_UPD(IDX, DEPTH2_NAME, DISPLAY_YN, RE_SORT);
            }
        }
        #endregion

        #region 카테고리 수정 3depth
        public void UpdateAdminCategoryThree(Nullable<int> IDX, string DEPTH3_NAME, string DISPLAY_YN, Nullable<int> RE_SORT)
        {
            using (AdminProductEntities AdminProductContext = new AdminProductEntities())
            {
                AdminProductContext.SP_ADMIN_CATEGORY_THREE_UPD(IDX, DEPTH3_NAME, DISPLAY_YN, RE_SORT);
            }
        }
        #endregion


        #region 카테고리 삭제
        public void DeleteAdminCategoryOne(Nullable<int> IDX)
        {
            using (AdminProductEntities AdminProductContext = new AdminProductEntities())
            {
                AdminProductContext.SP_ADMIN_CATEGORY_ONE_DEL(IDX);
            }
        }
        #endregion

        #region depth별 카테고리 리스트
        public List<SP_ADMIN_CATEGORY_DEPTH_SEL_Result> GetAdminCategoryDeptList(string CATE_GBN, string DEPTH1_CODE, string DEPTH2_CODE)
        {

            List<SP_ADMIN_CATEGORY_DEPTH_SEL_Result> lst = new List<SP_ADMIN_CATEGORY_DEPTH_SEL_Result>();
            using (AdminProductEntities AdminProductContext = new AdminProductEntities())
            {
                /**try {**/
                lst = AdminProductContext.SP_ADMIN_CATEGORY_DEPTH_SEL(CATE_GBN, DEPTH1_CODE, DEPTH2_CODE).ToList();
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;

        }
        #endregion

        #region depth별 카테고리 리스트 DISPLAY_YN = N 포함
        public List<SP_ADMIN_CATEGORY_DEPTH_SEL_ALL_Result> GetAdminCategoryDeptListAll(string CATE_GBN, string DEPTH1_CODE, string DEPTH2_CODE)
        {

            List<SP_ADMIN_CATEGORY_DEPTH_SEL_ALL_Result> lst = new List<SP_ADMIN_CATEGORY_DEPTH_SEL_ALL_Result>();
            using (AdminProductEntities AdminProductContext = new AdminProductEntities())
            {
                /**try {**/
                lst = AdminProductContext.SP_ADMIN_CATEGORY_DEPTH_SEL_ALL(CATE_GBN, DEPTH1_CODE, DEPTH2_CODE).ToList();
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;

        }
        #endregion

        #endregion

        #region 상품

        #region 상품 리스트
        public List<SP_ADMIN_PRODUCT_LIST_Result> GetAdminProductList(ProductSearch_Entity productSearch_Entity)
        {

            List<SP_ADMIN_PRODUCT_LIST_Result> lst = new List<SP_ADMIN_PRODUCT_LIST_Result>();
            using (AdminProductEntities AdminProductContext = new AdminProductEntities())
            {
                /**try {**/
                lst = AdminProductContext.SP_ADMIN_PRODUCT_SEL(productSearch_Entity.Page, productSearch_Entity.PageSize, productSearch_Entity.SearchKey, productSearch_Entity.SearchKeyword, productSearch_Entity.cateCode, productSearch_Entity.iconYn, productSearch_Entity.searchDisplayY, productSearch_Entity.searchDisplayN, productSearch_Entity.soldoutYn, productSearch_Entity.POutletYn, productSearch_Entity.FROM_DATE, productSearch_Entity.TO_DATE).ToList();
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;

        }
        #endregion

        #region 상품 카운트
        public int GetAdminProductCnt(ProductSearch_Entity productSearch_Entity)
        {
            List<SP_ADMIN_PRODUCT_CNT_Result> lst = new List<SP_ADMIN_PRODUCT_CNT_Result>();
            int productCount = -1;

            using (AdminProductEntities AdminProductContext = new AdminProductEntities())
            {

                lst = AdminProductContext.SP_ADMIN_PRODUCT_CNT(productSearch_Entity.SearchKey, productSearch_Entity.SearchKeyword, productSearch_Entity.cateCode, productSearch_Entity.iconYn, productSearch_Entity.searchDisplayY, productSearch_Entity.searchDisplayN, productSearch_Entity.soldoutYn, productSearch_Entity.POutletYn, productSearch_Entity.FROM_DATE, productSearch_Entity.TO_DATE).ToList();
                if (lst != null && lst.Count > 0)
                    productCount = lst[0].COUNT;
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return productCount;

        }
        #endregion

        #region 상품 등록 [Transactions]
        public void InsertAdminProduct(TB_PRODUCT_INFO tb_product_info)
        {
            int IsSuccess = 1;
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AdminProductEntities AdminProductContext = new AdminProductEntities())
                    {
                        AdminProductContext.SP_ADMIN_PRODUCT_INS(tb_product_info.P_CATE_CODE, tb_product_info.C_CATE_CODE, tb_product_info.L_CATE_CODE, tb_product_info.P_CODE, tb_product_info.P_NAME, tb_product_info.P_SUB_TITLE, tb_product_info.P_COUNT, tb_product_info.SELLING_PRICE, tb_product_info.DISCOUNT_RATE, tb_product_info.DISCOUNT_PRICE, tb_product_info.P_INFO_DETAIL_WEB, tb_product_info.P_INFO_DETAIL_MOBILE, tb_product_info.MV_URL, tb_product_info.P_COMPONENT_INFO, tb_product_info.P_TAG, tb_product_info.MAIN_IMG, tb_product_info.OTHER_IMG1, tb_product_info.OTHER_IMG2, tb_product_info.OTHER_IMG3, tb_product_info.ICON_YN, tb_product_info.WITH_PRODUCT_LIST);
                    }

                    //쿠폰 table에 상품 추가 
                    using (AdminCouponEntities AdmCouponContext = new AdminCouponEntities())
                    {
                        AdmCouponContext.SP_ADMIN_COUPON_PRODUCT_CREATE_INS_ON_ADDING_PRODUCT(tb_product_info.P_CODE);
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


        }
        #endregion

        #region 상품코드 유효성 체크

        public int? PcodeChkAdminProduct(string PCODE)
        {
            int? pcodeCount = -1;

            using (AdminProductEntities AdminProductContext = new AdminProductEntities())
            {
                pcodeCount = AdminProductContext.SP_ADMIN_PRODUCT_PCODE_CHK(PCODE).FirstOrDefault();
            }
            return pcodeCount;
        }

        #endregion

        #region 상품 이미지 개별 삭제

        public void ImageDelAdminProduct(string P_CODE, string imgColumName)
        {
            using (AdminProductEntities AdminProductContext = new AdminProductEntities())
            {
                AdminProductContext.SP_ADMIN_PRODUCT_IMG_DEL(P_CODE, imgColumName);
            }
        }

        #endregion

        #region 상품 VIEW

        public SP_ADMIN_PRODUCT_DETAIL_VIEW_Result ViewAdminProduct(string PCODE)
        {

            SP_ADMIN_PRODUCT_DETAIL_VIEW_Result productView;

            using (AdminProductEntities AdminProductContext = new AdminProductEntities())
            {
                productView = AdminProductContext.SP_ADMIN_PRODUCT_DETAIL_VIEW(PCODE).FirstOrDefault();
            }
            return productView;
        }

        #endregion

        #region 상품 수정
        //public void UpdateAdminProduct(int IDX, string P_CATE_CODE, string C_CATE_CODE, string L_CATE_CODE, string P_CODE, string P_NAME, Nullable<int> P_COUNT, Nullable<int> P_POINT, Nullable<int> P_PRICE, Nullable<int> SELLING_PRICE, Nullable<int> DISCOUNT_RATE, Nullable<int> DISCOUNT_P_POINT, Nullable<int> DISCOUNT_PRICE, string SOLDOUT_YN, string P_INFO_DETAIL_WEB, string P_INFO_DETAIL_MOBILE, string MV_URL, string P_COMPONENT_INFO, string P_TAG, string MAIN_IMG, string OTHER_IMG1, string OTHER_IMG2, string OTHER_IMG3, string OTHER_IMG4, string OTHER_IMG5, string DISPLAY_YN, string ICON_YN, string WITH_PRODUCT_LIST)
        public void UpdateAdminProduct(TB_PRODUCT_INFO tb_product_info)
        {
            using (AdminProductEntities AdminProductContext = new AdminProductEntities())
            {

                AdminProductContext.SP_ADMIN_PRODUCT_UPD(tb_product_info.IDX, tb_product_info.P_CATE_CODE, tb_product_info.C_CATE_CODE, tb_product_info.L_CATE_CODE, tb_product_info.P_NAME, tb_product_info.P_SUB_TITLE, tb_product_info.P_COUNT, tb_product_info.SELLING_PRICE, tb_product_info.DISCOUNT_RATE, tb_product_info.DISCOUNT_PRICE, tb_product_info.SOLDOUT_YN, tb_product_info.P_INFO_DETAIL_WEB, tb_product_info.P_INFO_DETAIL_MOBILE, tb_product_info.MV_URL, tb_product_info.P_COMPONENT_INFO, tb_product_info.P_TAG, tb_product_info.MAIN_IMG, tb_product_info.OTHER_IMG1, tb_product_info.OTHER_IMG2, tb_product_info.OTHER_IMG3, tb_product_info.ICON_YN, tb_product_info.WITH_PRODUCT_LIST);
            }
        }
        #endregion

        #region 상품 가격 일괄 수정
        public void UpdateAdminProductPrice(TB_PRODUCT_INFO tb_product_info)
        {
            using (AdminProductEntities AdminProductContext = new AdminProductEntities())
            {

                AdminProductContext.SP_ADMIN_PRODUCT_PRICE_UPD(tb_product_info.P_CODE, tb_product_info.SELLING_PRICE, tb_product_info.DISCOUNT_PRICE, tb_product_info.DISCOUNT_RATE);
            }
        }
        #endregion

        #region 상품 정보 일괄 수정
        public void UpdateAdminProductBatch(TB_PRODUCT_INFO tb_product_info)
        {
            using (AdminProductEntities AdminProductContext = new AdminProductEntities())
            {

                AdminProductContext.SP_ADMIN_PRODUCT_BATCH_UPD(tb_product_info.P_CODE, tb_product_info.ICON_BATCH_CHK, tb_product_info.ICON_YN, tb_product_info.DISPLAY_YN, tb_product_info.SOLDOUT_YN, tb_product_info.P_OUTLET_YN);
            }
        }
        #endregion

        #region 상품전시 순서 바꾸기
        public void UpdateAdminProductReSort(int IDX, int RE_SORT, string CLICKCHK)
        {
            using (AdminProductEntities AdminProductContext = new AdminProductEntities())
            {
                AdminProductContext.SP_ADMIN_PRODUCT_DISPLAY_RE_SORT(IDX, RE_SORT, CLICKCHK);
            }
        }
        #endregion 
        
        #endregion

        #region 사은품

        #region 사은품 리스트
        public List<SP_ADMIN_GIFT_SEL_Result> GetAdminGiftList(SP_TB_FREE_GIFT_INFO_Param Param)
        {

            List<SP_ADMIN_GIFT_SEL_Result> lst = new List<SP_ADMIN_GIFT_SEL_Result>();
            using (AdminProductEntities AdminProductContext = new AdminProductEntities())
            {
                lst = AdminProductContext.SP_ADMIN_GIFT_SEL(
                    Param.PAGE
                    , Param.PAGESIZE
                    , Param.SEARCH_KEY
                    , Param.SEARCH_KEYWORD
                    , Param.SEARCH_DISPLAY_YN).ToList();
            }

            return lst;

        }
        #endregion

        #region 사은품 카운트
        public int GetAdminGiftCnt(SP_TB_FREE_GIFT_INFO_Param Param)
        {
            List<SP_ADMIN_GIFT_CNT_Result> lst = new List<SP_ADMIN_GIFT_CNT_Result>();
            int giftCount = -1;

            using (AdminProductEntities AdminProductContext = new AdminProductEntities())
            {

                lst = AdminProductContext.SP_ADMIN_GIFT_CNT(
                    Param.SEARCH_KEY
                    , Param.SEARCH_KEYWORD
                    , Param.SEARCH_DISPLAY_YN
                    ).ToList();
                if (lst != null && lst.Count > 0)
                    giftCount = lst[0].COUNT;
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return giftCount;

        }
        #endregion

        #region 사은품 등록
        public void InsertAdminGift(AdminGiftModel gift_info_model)
        {

            using (AdminProductEntities AdminProductContext = new AdminProductEntities())
            {

                AdminProductContext.SP_ADMIN_GIFT_INS(
                    gift_info_model.GIFT_NAME
                   , gift_info_model.GIFT_COUNT
                   , gift_info_model.START_PRICE
                   , gift_info_model.END_PRICE
                   , gift_info_model.GIFT_IMG
                   , gift_info_model.DISPLAY_YN);

            }
        }
        #endregion

        #region 사은품 VIEW
        public SP_ADMIN_GIFT_DETAIL_VIEW_Result ViewAdminGift(int idx)
        {

            SP_ADMIN_GIFT_DETAIL_VIEW_Result giftView;

            using (AdminProductEntities AdminProductContext = new AdminProductEntities())
            {
                giftView = AdminProductContext.SP_ADMIN_GIFT_DETAIL_VIEW(idx).FirstOrDefault();
            }
            return giftView;
        }
        #endregion

        #region 사은품 수정
        public void UpdateAdminGift(AdminGiftModel gift_update_model)
        {
            using (AdminProductEntities AdminProductContext = new AdminProductEntities())
            {

                AdminProductContext.SP_ADMIN_GIFT_UPD(
                    gift_update_model.IDX
                   , gift_update_model.GIFT_NAME
                   , gift_update_model.GIFT_COUNT
                   , gift_update_model.GIFT_IMG
                   , gift_update_model.DISPLAY_YN
                    );
            }
        }
        #endregion

       

        #endregion

    }
}
