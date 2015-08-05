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

        //상품 모바일 메인 리스트
        List<SP_PRODUCT_MAIN_SEL_Result> GetProductMainList(string cate);

        //상품 리스트
        List<SP_PRODUCT_SEL_Result> GetProductList(Product_front_search_entity product_front_search_entity);

        //상품 모바일 리스트
        List<SP_PRODUCT_MOBILE_SEL_Result> GetProductMobileList(Product_front_search_entity product_front_search_entity);

        //상품 카운트
        int GetProductCnt(Product_front_search_entity product_front_search_entity);

        //상품 보기
        SP_PRODUCT_DETAIL_VIEW_Result ViewProduct(string PCODE);

        //상품 모바일 보기
        SP_PRODUCT_MOBILE_DETAIL_VIEW_Result ViewProductMobile(string PCODE);

        //상품 검색 리스트
        List<SP_PRODUCT_SEARCH_SEL_Result> GetProductSearchList(Product_front_search_entity product_front_search_entity);

        //상품 모바일 검색 리스트
        List<SP_PRODUCT_MOBILE_SEARCH_SEL_Result> GetProductMobileSearchList(Product_front_search_entity product_front_search_entity);

        //상품 검색 카운트
        int GetProductSearchCnt(Product_front_search_entity product_front_search_entity);

        //카테고리 리스트
        List<SP_CATEGORY_DEPTH_SEL_Result> GetCategoryDeptList(string CATE_GBN, string DEPTH1_CODE, string DEPTH2_CODE);

        //상품 mypage skin type 리스트
        List<SP_PRODUCT_MYPAGE_SKIKTYPE_SEL_Result> GetProductMypageSkinTypeList(string cate);

        #endregion
    }
}
