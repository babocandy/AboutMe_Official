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
        void InsertAdminCategoryOne(string DEPTH1_NAME);
        //카테고리 등록 2DEPTH
        void InsertAdminCategoryTwo(string CATE_GBN, string DEPTH1_CODE, string DEPTH2_NAME);
        //카테고리 등록 3DEPTH
        void InsertAdminCategoryThree(string CATE_GBN, string DEPTH1_CODE, string DEPTH2_CODE, string DEPTH3_NAME);
        //카테고리 내용보기
        SP_ADMIN_CATEGORY_VIEW_Result ViewAdminCategory(int IDX);
        //카테고리 수정 1depth
        void UpdateAdminCategoryOne(Nullable<int> IDX, string DEPTH1_NAME, string DISPLAY_YN, Nullable<int> RE_SORT);
        //카테고리 수정 2depth
        void UpdateAdminCategoryTwo(Nullable<int> IDX, string DEPTH2_NAME, string DISPLAY_YN, Nullable<int> RE_SORT);
        //카테고리 수정 3depth
        void UpdateAdminCategoryThree(Nullable<int> IDX, string DEPTH3_NAME, string DISPLAY_YN, Nullable<int> RE_SORT);
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
        //void InsertAdminProduct(string P_CATE_CODE, string C_CATE_CODE, string L_CATE_CODE, string P_CODE, string P_NAME, Nullable<int> P_COUNT, Nullable<int> P_POINT, Nullable<int> P_PRICE, Nullable<int> SELLING_PRICE, Nullable<int> DISCOUNT_RATE, Nullable<int> DISCOUNT_P_POINT, Nullable<int> DISCOUNT_PRICE, string SOLDOUT_YN, string P_INFO_DETAIL_WEB, string P_INFO_DETAIL_MOBILE, string MV_URL, string P_COMPONENT_INFO, string P_TAG, string MAIN_IMG, string OTHER_IMG1, string OTHER_IMG2, string OTHER_IMG3, string OTHER_IMG4, string OTHER_IMG5, string DISPLAY_YN, string ICON_YN, string WITH_PRODUCT_LIST);                            
        void InsertAdminProduct(TB_PRODUCT_INFO tb_product_info);
        //상품코드 유효성 체크
        int? PcodeChkAdminProduct(string pcode);
        //상품이미지 개별 삭제
        void ImageDelAdminProduct(string P_CODE, string imgColumName);
        //상품 보기
        SP_ADMIN_PRODUCT_DETAIL_VIEW_Result ViewAdminProduct(string PCODE);
        //상품 수정
        //void UpdateAdminProduct(int IDX, string P_CATE_CODE, string C_CATE_CODE, string L_CATE_CODE, string P_CODE, string P_NAME, Nullable<int> P_COUNT, Nullable<int> P_POINT, Nullable<int> P_PRICE, Nullable<int> SELLING_PRICE, Nullable<int> DISCOUNT_RATE, Nullable<int> DISCOUNT_P_POINT, Nullable<int> DISCOUNT_PRICE, string SOLDOUT_YN, string P_INFO_DETAIL_WEB, string P_INFO_DETAIL_MOBILE, string MV_URL, string P_COMPONENT_INFO, string P_TAG, string MAIN_IMG, string OTHER_IMG1, string OTHER_IMG2, string OTHER_IMG3, string OTHER_IMG4, string OTHER_IMG5, string DISPLAY_YN, string ICON_YN, string WITH_PRODUCT_LIST);
        void UpdateAdminProduct(TB_PRODUCT_INFO tb_product_info);

        //void InsertAdminCategoryOne(string DEPTH1_NAME);
        //List<SP_ADM_ADMIN_DEPT_SEL_Result> GetAdmDeptList();
        #endregion

    }
}
