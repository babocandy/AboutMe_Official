using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AboutMe.Domain.Entity.Cart;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Domain.Service.Cart
{
    public class CartService : ICartService
    {
        #region List Cart
        public List<SP_TB_CART_LIST_Result> CartList(string m_id, string session_id)
        {
            List<SP_TB_CART_LIST_Result> lst = new List<SP_TB_CART_LIST_Result>();
            using (CartEntities EfContext = new CartEntities())
            {
                lst = EfContext.SP_TB_CART_LIST(m_id, session_id).ToList();
            }

            return lst;
        }
        #endregion

        #region Count
        public int CartListCount(string m_id, string session_id)
        {
            SP_TB_CART_CNT_Result result;
            using (CartEntities EfContext = new CartEntities())
            {
                result = EfContext.SP_TB_CART_CNT(m_id, session_id).FirstOrDefault();
            }
            return result.COUNT;
        }
        #endregion

        #region Insert
        public void CartInsert(SP_TB_CART_PRODUCT_ADD_Param newItem)
        {
            using (CartEntities EfContext = new CartEntities())
            {
                EfContext.SP_TB_CART_PRODUCT_ADD(newItem.M_ID, newItem.SESSION_ID, newItem.P_CODE_LIST, newItem.P_COUNT_LIST,newItem.MERGY_OPT);
            }
        }
        #endregion

        #region Delete
        public void CartDelete(string m_id, string session_id, string cart_idx)
        {
            using (CartEntities EfContext = new CartEntities())
            {
                EfContext.SP_TB_CART_PRODUCT_DEL(m_id, session_id, cart_idx);
            }
        }
        #endregion


        #region Merge
        public void CartMerge(string m_id, string session_id, string merge_opt)
        {
            using (CartEntities EfContext = new CartEntities())
            {
                EfContext.SP_TB_CART_MERGE(m_id, session_id, merge_opt);
            }
        }
        #endregion

        #region Update Count
        public void CartUpdateCnt(string m_id, string session_id, int cart_idx, int? p_count = 1)
        {
            using (CartEntities EfContext = new CartEntities())
            {
                EfContext.SP_TB_CART_COUNT_CHANGE(m_id, session_id, cart_idx, p_count);
            }
        }
        #endregion


        #region Wish Count
        public int WishListCount(string m_id)
        {
            SP_TB_WISH_CNT_Result result;
            using (CartEntities EfContext = new CartEntities())
            {
                result = EfContext.SP_TB_WISHLIST_CNT(m_id).FirstOrDefault();
            }
            return result.COUNT;
        }
        #endregion

        #region Wish Insert
        public int WishInsert(string m_id, string p_code)
        {
            SP_TB_WISH_CNT_Result result;
            using (CartEntities EfContext = new CartEntities())
            {
                result = EfContext.SP_TB_WISHLIST_PRODUCT_ADD(m_id, p_code).FirstOrDefault();
            }
            return result.COUNT;
        }
        #endregion

        #region Wish delete
        public int WishDelete(string m_id, int idx)
        {
            SP_TB_WISH_CNT_Result result;
            using (CartEntities EfContext = new CartEntities())
            {
                result = EfContext.SP_TB_WISHLIST_PRODUCT_DEL(m_id, idx).FirstOrDefault();
            }
            return result.COUNT;
        }
        #endregion

        #region Wish List
        public List<SP_TB_WISHLIST_LIST_Result> WishList(string m_id, int? page, int? pagesize)
        {
            List<SP_TB_WISHLIST_LIST_Result> result = new List<SP_TB_WISHLIST_LIST_Result>();
            using (CartEntities EfContext = new CartEntities())
            {
                result = EfContext.SP_TB_WISHLIST_LIST(m_id, page, pagesize).ToList();
            }
            return result;
        }
        #endregion


        #region 상품상세의 특정상품의 쇼핑백에 담긴수
        public Int32 ProductofCartCnt(string p_code)
        {
            Int32 result = 0;
            using (CartEntities EfContext = new CartEntities())
            {
                result = EfContext.SP_TB_CART_PCODE_CNT(p_code).DefaultIfEmpty(0).FirstOrDefault().Value ;
            }
            return result;
        }
        #endregion
    }
}
