using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.Product;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Domain.Service.Product
{
    public class ProductService : IProductService
    {

        #region 상품

        #region 상품 메인 리스트
        public List<SP_PRODUCT_MAIN_SEL_Result> GetProductMainList(string cate)
        {

            List<SP_PRODUCT_MAIN_SEL_Result> lst = new List<SP_PRODUCT_MAIN_SEL_Result>();
            using (ProductEntities ProductContext = new ProductEntities())
            {
                lst = ProductContext.SP_PRODUCT_MAIN_SEL(cate).ToList();
            }

            return lst;
        }
        #endregion

        #region 상품 리스트
        public List<SP_PRODUCT_SEL_Result> GetProductList(Product_front_search_entity product_front_search_entity)
        {

            List<SP_PRODUCT_SEL_Result> lst = new List<SP_PRODUCT_SEL_Result>();
            using (ProductEntities ProductContext = new ProductEntities())
            {
                //try {
                lst = ProductContext.SP_PRODUCT_SEL(product_front_search_entity.PAGE, product_front_search_entity.PAGESIZE, product_front_search_entity.P_CATE_CODE, product_front_search_entity.C_CATE_CODE, product_front_search_entity.L_CATE_CODE, product_front_search_entity.SORT_GBN, product_front_search_entity.P_OUTLET_YN, product_front_search_entity.P_CATE_CODE_3DEPTH).ToList();
                 //}catch
                 //{
                 //      ProductContext.Dispose();
                 //}
            }

            return lst;

        }
        #endregion

        #region 상품 모바일 리스트
        public List<SP_PRODUCT_MOBILE_SEL_Result> GetProductMobileList(Product_front_search_entity product_front_search_entity)
        {

            List<SP_PRODUCT_MOBILE_SEL_Result> lst = new List<SP_PRODUCT_MOBILE_SEL_Result>();
            using (ProductEntities ProductContext = new ProductEntities())
            {
                //try {
                lst = ProductContext.SP_PRODUCT_MOBILE_SEL(product_front_search_entity.PAGE, product_front_search_entity.PAGESIZE, product_front_search_entity.P_CATE_CODE, product_front_search_entity.C_CATE_CODE, product_front_search_entity.L_CATE_CODE, product_front_search_entity.SORT_GBN, product_front_search_entity.P_OUTLET_YN, product_front_search_entity.P_CATE_CODE_3DEPTH).ToList();
                //}catch
                //{
                //      ProductContext.Dispose();
                //}
            }

            return lst;

        }
        #endregion

        #region 상품 카운트
        public int GetProductCnt(Product_front_search_entity product_front_search_entity)
        {
            List<SP_PRODUCT_CNT_Result> lst = new List<SP_PRODUCT_CNT_Result>();
            int productCount = -1;

            using (ProductEntities ProductContext = new ProductEntities())
            {

                lst = ProductContext.SP_PRODUCT_CNT(product_front_search_entity.P_CATE_CODE, product_front_search_entity.C_CATE_CODE, product_front_search_entity.L_CATE_CODE, product_front_search_entity.SORT_GBN, product_front_search_entity.P_OUTLET_YN, product_front_search_entity.P_CATE_CODE_3DEPTH).ToList();
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

        #region 상품 VIEW

        public SP_PRODUCT_DETAIL_VIEW_Result ViewProduct(string PCODE)
        {

            SP_PRODUCT_DETAIL_VIEW_Result productView;

            using (ProductEntities ProductContext = new ProductEntities())
            {
                productView = ProductContext.SP_PRODUCT_DETAIL_VIEW(PCODE).FirstOrDefault();
            }
            return productView;
        }

        #endregion

        #region 상품 모바일 VIEW
        public SP_PRODUCT_MOBILE_DETAIL_VIEW_Result ViewProductMobile(string PCODE)
        {

            SP_PRODUCT_MOBILE_DETAIL_VIEW_Result productView;

            using (ProductEntities ProductContext = new ProductEntities())
            {
                productView = ProductContext.SP_PRODUCT_MOBILE_DETAIL_VIEW(PCODE).FirstOrDefault();
            }
            return productView;
        }
        #endregion

        #region 상품 검색 리스트
        public List<SP_PRODUCT_SEARCH_SEL_Result> GetProductSearchList(Product_front_search_entity product_front_search_entity)
        {

            List<SP_PRODUCT_SEARCH_SEL_Result> lst = new List<SP_PRODUCT_SEARCH_SEL_Result>();
            using (ProductEntities ProductContext = new ProductEntities())
            {
                //try {
                lst = ProductContext.SP_PRODUCT_SEARCH_SEL(product_front_search_entity.PAGE, product_front_search_entity.PAGESIZE, product_front_search_entity.SORT_GBN, product_front_search_entity.SEARCH_KEYWORD).ToList();
                //}catch
                //{
                //      ProductContext.Dispose();
                //}
            }

            return lst;

        }
        #endregion

        #region 상품 모바일 검색 리스트
        public List<SP_PRODUCT_MOBILE_SEARCH_SEL_Result> GetProductMobileSearchList(Product_front_search_entity product_front_search_entity)
        {

            List<SP_PRODUCT_MOBILE_SEARCH_SEL_Result> lst = new List<SP_PRODUCT_MOBILE_SEARCH_SEL_Result>();
            using (ProductEntities ProductContext = new ProductEntities())
            {
                //try {
                lst = ProductContext.SP_PRODUCT_MOBILE_SEARCH_SEL(product_front_search_entity.PAGE, product_front_search_entity.PAGESIZE, product_front_search_entity.SORT_GBN, product_front_search_entity.SEARCH_KEYWORD).ToList();
                //}catch
                //{
                //      ProductContext.Dispose();
                //}
            }

            return lst;

        }
        #endregion

        #region 상품 검색 카운트
        public int GetProductSearchCnt(Product_front_search_entity product_front_search_entity)
        {
            List<SP_PRODUCT_SEARCH_CNT_Result> lst = new List<SP_PRODUCT_SEARCH_CNT_Result>();
            int productCount = -1;

            using (ProductEntities ProductContext = new ProductEntities())
            {

                lst = ProductContext.SP_PRODUCT_SEARCH_CNT(product_front_search_entity.SEARCH_KEYWORD).ToList();
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

        #region 상품 mypage skin type 리스트
        public List<SP_PRODUCT_MYPAGE_SKIKTYPE_SEL_Result> GetProductMypageSkinTypeList(string cate)
        {
            List<SP_PRODUCT_MYPAGE_SKIKTYPE_SEL_Result> lst = new List<SP_PRODUCT_MYPAGE_SKIKTYPE_SEL_Result>();
            using (ProductEntities ProductContext = new ProductEntities())
            {
                lst = ProductContext.SP_PRODUCT_MYPAGE_SKIKTYPE_SEL(cate).ToList();
            }
            return lst;
        }
        #endregion

        #region 연관상품 리스트
        public List<SP_PRODUCT_WITH_SEL_Result> GetProductWithList(string _with_product_list)
        {

            List<SP_PRODUCT_WITH_SEL_Result> lst = new List<SP_PRODUCT_WITH_SEL_Result>();
            using (ProductEntities ProductContext = new ProductEntities())
            {
                lst = ProductContext.SP_PRODUCT_WITH_SEL(_with_product_list).ToList();
            }

            return lst;
        }
        #endregion

        #region 연관상품 리스트 [모바일]
        public List<SP_PRODUCT_MOBILE_WITH_SEL_Result> GetProductMobileWithList(string _with_product_list)
        {

            List<SP_PRODUCT_MOBILE_WITH_SEL_Result> lst = new List<SP_PRODUCT_MOBILE_WITH_SEL_Result>();
            using (ProductEntities ProductContext = new ProductEntities())
            {
                lst = ProductContext.SP_PRODUCT_MOBILE_WITH_SEL(_with_product_list).ToList();
            }

            return lst;
        }
        #endregion

        #endregion

        #region 카테고리

        #region 카테고리 리스트
        public List<SP_CATEGORY_DEPTH_SEL_Result> GetCategoryDeptList(string CATE_GBN, string DEPTH1_CODE, string DEPTH2_CODE)
        {
            List<SP_CATEGORY_DEPTH_SEL_Result> lst = new List<SP_CATEGORY_DEPTH_SEL_Result>();
            using (ProductEntities ProductContext = new ProductEntities())
            {
                lst = ProductContext.SP_CATEGORY_DEPTH_SEL(CATE_GBN, DEPTH1_CODE, DEPTH2_CODE).ToList();
            }
            return lst;

        }
        #endregion

        #endregion
    }
}
