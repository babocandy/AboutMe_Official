using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.AdminProduct;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Domain.Service.AdminProduct
{
    public interface IAdminProductService
    {
        #region 카테고리 
        //카테고리 리스트
        List<SP_ADMIN_CATEGORY_ONE_SEL_Result> GetAdminCategoryOneList();
        //카테고리 등록 1DEPTH
        int InsertAdminCategoryOne(string DEPTH1_NAME);
        //카테고리 등록 2DEPTH
        int InsertAdminCategoryTwo(string CATE_GBN, string DEPTH1_CODE, string DEPTH2_NAME);
        //카테고리 등록 3DEPTH
        int InsertAdminCategoryThree(string CATE_GBN, string DEPTH1_CODE, string DEPTH2_CODE, string DEPTH3_NAME);
        //카테고리 내용보기
        SP_ADMIN_CATEGORY_VIEW_Result ViewAdminCategory(int IDX);
        //카테고리 수정 1depth
        int UpdateAdminCategoryOne(Nullable<int> IDX, string DEPTH1_NAME, string DISPLAY_YN, Nullable<int> RE_SORT);
        //카테고리 수정 2depth
        int UpdateAdminCategoryTwo(Nullable<int> IDX, string DEPTH2_NAME, string DISPLAY_YN, Nullable<int> RE_SORT);
        //카테고리 수정 3depth
        int UpdateAdminCategoryThree(Nullable<int> IDX, string DEPTH3_NAME, string DISPLAY_YN, Nullable<int> RE_SORT);
        //카테고리 삭제
        void DeleteAdminCategoryOne(Nullable<int> IDX);
        //카테고리별 서브리스트 가져오기
        List<SP_ADMIN_CATEGORY_DEPTH_SEL_Result> GetAdminCategoryDeptList(string CATE_GBN, string DEPTH1_CODE, string DEPTH2_CODE);
        //카테고리별 서브리스트 가져오기 DISPLAY_YN = N 포함
        List<SP_ADMIN_CATEGORY_DEPTH_SEL_ALL_Result> GetAdminCategoryDeptListAll(string CATE_GBN, string DEPTH1_CODE, string DEPTH2_CODE);
        #endregion

        #region 상품
        //상품 리스트
        List<SP_ADMIN_PRODUCT_LIST_Result> GetAdminProductList(ProductSearch_Entity productSearch_Entity);
        //상품 카운트
        int GetAdminProductCnt(ProductSearch_Entity productSearch_Entity);
        //상품 등록
        int InsertAdminProduct(TB_PRODUCT_INFO tb_product_info);
        //상품코드 유효성 체크
        int? PcodeChkAdminProduct(string pcode);
        //상품이미지 개별 삭제
        void ImageDelAdminProduct(string P_CODE, string imgColumName);
        //상품 보기
        SP_ADMIN_PRODUCT_DETAIL_VIEW_Result ViewAdminProduct(string PCODE);
        //상품 수정
        int UpdateAdminProduct(TB_PRODUCT_INFO tb_product_info);

        //상품가격 일괄수정
        int UpdateAdminProductPrice(List<PRODUCT_PRICE_BATCH_MODEL> product_price_batch, ProductSearch_Entity Param);
        //상품정보 일괄수정
        int UpdateAdminProductBatch(List<PRODUCT_INFO_BATCH_MODEL> product_info_batch, ProductSearch_Entity Param);
        //상품전시 순서 바꾸기
        int UpdateAdminProductReSort(int IDX, int RE_SORT, string CLICKCHK, string catecode);

        //void InsertAdminCategoryOne(string DEPTH1_NAME);
        //List<SP_ADM_ADMIN_DEPT_SEL_Result> GetAdmDeptList();
        #endregion

        #region 사은품

        //사은품 리스트
        List<SP_ADMIN_GIFT_SEL_Result> GetAdminGiftList(SP_TB_FREE_GIFT_INFO_Param Param);
        //사은품 카운트
        int GetAdminGiftCnt(SP_TB_FREE_GIFT_INFO_Param Param);
        //사은품 등록
        int InsertAdminGift(AdminGiftModel gift_info_model);
        //사은품 보기
        SP_ADMIN_GIFT_DETAIL_VIEW_Result ViewAdminGift(int idx);
        //사은품 수정
        int UpdateAdminGift(AdminGiftModel gift_update_model);
        
        #endregion

       #region SMS 등록
        //void InsertSMS(AdminSMSModel adminSMSModel);
       #endregion
    }
}
