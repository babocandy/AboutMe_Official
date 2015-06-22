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
        //public void InsertAdminProduct(string P_CATE_CODE, string C_CATE_CODE, string L_CATE_CODE, string P_CODE, string P_NAME, Nullable<int> P_COUNT, Nullable<int> P_POINT, Nullable<int> P_PRICE, Nullable<int> SELLING_PRICE, Nullable<int> DISCOUNT_RATE, Nullable<int> DISCOUNT_P_POINT, Nullable<int> DISCOUNT_PRICE, string SOLDOUT_YN, string P_INFO_DETAIL_WEB, string P_INFO_DETAIL_MOBILE, string MV_URL, string P_COMPONENT_INFO, string P_TAG, string MAIN_IMG, string OTHER_IMG1, string OTHER_IMG2, string OTHER_IMG3, string OTHER_IMG4, string OTHER_IMG5, string DISPLAY_YN, string ICON_YN, string WITH_PRODUCT_LIST)
        public void InsertAdminProduct(TB_PRODUCT_INFO tb_product_info)
        {

            using (AdminProductEntities AdminProductContext = new AdminProductEntities())
            {

                AdminProductContext.SP_ADMIN_PRODUCT_INS(tb_product_info.P_CATE_CODE, tb_product_info.C_CATE_CODE, tb_product_info.L_CATE_CODE, tb_product_info.P_CODE, tb_product_info.P_NAME, tb_product_info.P_COUNT, tb_product_info.SELLING_PRICE, tb_product_info.DISCOUNT_RATE, tb_product_info.DISCOUNT_PRICE, tb_product_info.P_INFO_DETAIL_WEB, tb_product_info.P_INFO_DETAIL_MOBILE, tb_product_info.MV_URL, tb_product_info.P_COMPONENT_INFO, tb_product_info.P_TAG, tb_product_info.MAIN_IMG, tb_product_info.OTHER_IMG1, tb_product_info.OTHER_IMG2, tb_product_info.OTHER_IMG3, tb_product_info.OTHER_IMG4, tb_product_info.OTHER_IMG5, tb_product_info.ICON_YN, tb_product_info.WITH_PRODUCT_LIST);

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

                AdminProductContext.SP_ADMIN_PRODUCT_UPD(tb_product_info.IDX, tb_product_info.P_CATE_CODE, tb_product_info.C_CATE_CODE, tb_product_info.L_CATE_CODE, tb_product_info.P_CODE, tb_product_info.P_NAME, tb_product_info.P_COUNT, tb_product_info.SELLING_PRICE, tb_product_info.DISCOUNT_RATE, tb_product_info.DISCOUNT_PRICE, tb_product_info.SOLDOUT_YN, tb_product_info.P_INFO_DETAIL_WEB, tb_product_info.P_INFO_DETAIL_MOBILE, tb_product_info.MV_URL, tb_product_info.P_COMPONENT_INFO, tb_product_info.P_TAG, tb_product_info.MAIN_IMG, tb_product_info.OTHER_IMG1, tb_product_info.OTHER_IMG2, tb_product_info.OTHER_IMG3, tb_product_info.OTHER_IMG4, tb_product_info.OTHER_IMG5, tb_product_info.DISPLAY_YN, tb_product_info.ICON_YN, tb_product_info.WITH_PRODUCT_LIST);
            }
        }
        #endregion

       

    }
}
