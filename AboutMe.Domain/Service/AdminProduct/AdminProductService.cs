using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.AdminProduct;
using System.Data.Entity.Core.Objects;


namespace AboutMe.Domain.Service.AdminProduct
{
    public class AdminProductService : IAdminProductService
    {

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

        #region 1depth 카테고리 등록 OUTPUT PARAMETER적용
        public int InsertAdminCategoryOne(string DEPTH1_NAME)
        {

            int i = 3 ;
            using (AdminProductEntities AdminProductContext = new AdminProductEntities())
            {

                ObjectParameter objParam = new ObjectParameter("INTRESULT", typeof(Int32));//sp의 output parameter변수명을 동일하게 사용한다.
                i = AdminProductContext.SP_ADMIN_CATEGORY_ONE_INS(DEPTH1_NAME, objParam);
                
            }
            return i;
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

        #region 카테고리 수정
        public void UpdateAdminCategoryOne(Nullable<int> IDX, string DEPTH1_NAME, string DISPLAY_YN, Nullable<int> RE_SORT)
        {
            using (AdminProductEntities AdminProductContext = new AdminProductEntities())
            {
                AdminProductContext.SP_ADMIN_CATEGORY_ONE_UPD(IDX, DEPTH1_NAME, DISPLAY_YN, RE_SORT);
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




        #region 상품 리스트
        public List<SP_ADMIN_PRODUCT_SEL_Result> GetAdminProductList()
        {

            List<SP_ADMIN_PRODUCT_SEL_Result> lst = new List<SP_ADMIN_PRODUCT_SEL_Result>();
            using (AdminProductEntities AdminProductContext = new AdminProductEntities())
            {
                /**try {**/
                lst = AdminProductContext.SP_ADMIN_PRODUCT_SEL().ToList();
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;

        }
        #endregion

        #region 상품 등록
        public void InsertAdminProduct(string P_CATE_CODE, string C_CATE_CODE, string L_CATE_CODE, string P_CODE, string P_NAME, Nullable<int> P_COUNT, Nullable<int> P_POINT, Nullable<int> P_PRICE, Nullable<int> SELLING_PRICE, Nullable<int> DISCOUNT_RATE, Nullable<int> DISCOUNT_P_POINT, Nullable<int> DISCOUNT_PRICE, string SOLDOUT_YN, string P_INFO_DETAIL_WEB, string P_INFO_DETAIL_MOBILE, string MV_URL, string P_COMPONENT_INFO, string P_TAG, string MAIN_IMG, string OTHER_IMG1, string OTHER_IMG2, string OTHER_IMG3, string OTHER_IMG4, string OTHER_IMG5, string DISPLAY_YN, string ICON_YN, string WITH_PRODUCT_LIST)
        {

            using (AdminProductEntities AdminProductContext = new AdminProductEntities())
            {

                AdminProductContext.SP_ADMIN_PRODUCT_INS(P_CATE_CODE, C_CATE_CODE, L_CATE_CODE, P_CODE, P_NAME, P_COUNT, P_POINT, P_PRICE, SELLING_PRICE, DISCOUNT_RATE, DISCOUNT_P_POINT, DISCOUNT_PRICE, SOLDOUT_YN, P_INFO_DETAIL_WEB, P_INFO_DETAIL_MOBILE, MV_URL, P_COMPONENT_INFO, P_TAG, MAIN_IMG, OTHER_IMG1, OTHER_IMG2, OTHER_IMG3, OTHER_IMG4, OTHER_IMG5, DISPLAY_YN, ICON_YN, WITH_PRODUCT_LIST);

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
        public void UpdateAdminProduct(int IDX, string P_CATE_CODE, string C_CATE_CODE, string L_CATE_CODE, string P_CODE, string P_NAME, Nullable<int> P_COUNT, Nullable<int> P_POINT, Nullable<int> P_PRICE, Nullable<int> SELLING_PRICE, Nullable<int> DISCOUNT_RATE, Nullable<int> DISCOUNT_P_POINT, Nullable<int> DISCOUNT_PRICE, string SOLDOUT_YN, string P_INFO_DETAIL_WEB, string P_INFO_DETAIL_MOBILE, string MV_URL, string P_COMPONENT_INFO, string P_TAG, string MAIN_IMG, string OTHER_IMG1, string OTHER_IMG2, string OTHER_IMG3, string OTHER_IMG4, string OTHER_IMG5, string DISPLAY_YN, string ICON_YN, string WITH_PRODUCT_LIST)
        {
            using (AdminProductEntities AdminProductContext = new AdminProductEntities())
            {
                AdminProductContext.SP_ADMIN_PRODUCT_UPD(IDX, P_CATE_CODE, C_CATE_CODE, L_CATE_CODE, P_CODE, P_NAME, P_COUNT, P_POINT, P_PRICE, SELLING_PRICE, DISCOUNT_RATE, DISCOUNT_P_POINT, DISCOUNT_PRICE, SOLDOUT_YN, P_INFO_DETAIL_WEB, P_INFO_DETAIL_MOBILE, MV_URL, P_COMPONENT_INFO, P_TAG, MAIN_IMG, OTHER_IMG1, OTHER_IMG2, OTHER_IMG3, OTHER_IMG4, OTHER_IMG5, DISPLAY_YN, ICON_YN, WITH_PRODUCT_LIST);
            }
        }
        #endregion

       

    }
}
