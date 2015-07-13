using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.Product;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Domain.Service.Product
{
    public interface IProductService
    {
        #region 상품
        //상품 리스트
        List<SP_PRODUCT_SEL_Result> GetProductList();
        //상품 카운트
        int GetProductCnt();
        //상품 보기
        SP_PRODUCT_DETAIL_VIEW_Result ViewProduct(string PCODE);
        #endregion
    }
}
