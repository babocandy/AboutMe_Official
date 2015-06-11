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
        //카테고리 리스트
        List<SP_ADMIN_CATEGORY_ONE_SEL_Result> GetAdminCategoryOneList();
        //카테고리 등록
        int InsertAdminCategoryOne(string DEPTH1_NAME);
        //카테고리 내용보기
        SP_ADMIN_CATEGORY_VIEW_Result ViewAdminCategory(int IDX);
        //카테고리 수정
        void UpdateAdminCategoryOne(Nullable<int> IDX, string DEPTH1_NAME, string DISPLAY_YN, Nullable<int> RE_SORT);
        //카테고리 삭제
        void DeleteAdminCategoryOne(Nullable<int> IDX);

        //상품 리스트
        List<SP_ADMIN_PRODUCT_SEL_Result> GetAdminProductList();
        //상품 등록
        void InsertAdminProduct(string P_CATE_CODE, string C_CATE_CODE, string L_CATE_CODE, string P_CODE, string P_NAME, Nullable<int> P_COUNT, Nullable<int> P_POINT, Nullable<int> P_PRICE, Nullable<int> SELLING_PRICE, Nullable<int> DISCOUNT_RATE, Nullable<int> DISCOUNT_P_POINT, Nullable<int> DISCOUNT_PRICE, string SOLDOUT_YN, string P_INFO_DETAIL_WEB, string P_INFO_DETAIL_MOBILE, string MV_URL, string P_COMPONENT_INFO, string P_TAG, string MAIN_IMG, string OTHER_IMG1, string OTHER_IMG2, string OTHER_IMG3, string OTHER_IMG4, string OTHER_IMG5, string DISPLAY_YN, string ICON_YN, string WITH_PRODUCT_LIST);                            
        //상품 보기
        SP_ADMIN_PRODUCT_DETAIL_VIEW_Result ViewAdminProduct(string PCODE);
        //상품 수정
        void UpdateAdminProduct(int IDX, string P_CATE_CODE, string C_CATE_CODE, string L_CATE_CODE, string P_CODE, string P_NAME, Nullable<int> P_COUNT, Nullable<int> P_POINT, Nullable<int> P_PRICE, Nullable<int> SELLING_PRICE, Nullable<int> DISCOUNT_RATE, Nullable<int> DISCOUNT_P_POINT, Nullable<int> DISCOUNT_PRICE, string SOLDOUT_YN, string P_INFO_DETAIL_WEB, string P_INFO_DETAIL_MOBILE, string MV_URL, string P_COMPONENT_INFO, string P_TAG, string MAIN_IMG, string OTHER_IMG1, string OTHER_IMG2, string OTHER_IMG3, string OTHER_IMG4, string OTHER_IMG5, string DISPLAY_YN, string ICON_YN, string WITH_PRODUCT_LIST);

        //void InsertAdminCategoryOne(string DEPTH1_NAME);
        //List<SP_ADM_ADMIN_DEPT_SEL_Result> GetAdmDeptList();
    }
}
