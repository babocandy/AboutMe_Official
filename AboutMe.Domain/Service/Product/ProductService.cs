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

        #region 상품 리스트
        public List<SP_PRODUCT_SEL_Result> GetProductList()
        {

            List<SP_PRODUCT_SEL_Result> lst = new List<SP_PRODUCT_SEL_Result>();
            using (ProductEntities ProductContext = new ProductEntities())
            {
                //try {
                    lst = ProductContext.SP_PRODUCT_SEL().ToList();
                 //}catch
                 //{
                 //      ProductContext.Dispose();
                 //}
            }

            return lst;

        }
        #endregion

        #region 상품 카운트
        public int GetProductCnt()
        {
            List<SP_PRODUCT_CNT_Result> lst = new List<SP_PRODUCT_CNT_Result>();
            int productCount = -1;

            using (ProductEntities ProductContext = new ProductEntities())
            {

                lst = ProductContext.SP_PRODUCT_CNT().ToList();
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
