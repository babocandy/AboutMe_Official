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
        public void CartDelete(string m_id, string session_id, string p_code_list)
        {
            using (CartEntities EfContext = new CartEntities())
            {
                EfContext.SP_TB_CART_PRODUCT_DEL(m_id, session_id, p_code_list);
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
        public void CartUpdateCnt(string m_id, string session_id, string p_code, int? p_count = 1)
        {
            using (CartEntities EfContext = new CartEntities())
            {
                EfContext.SP_TB_CART_COUNT_CHANGE(m_id, session_id, p_code, p_count);
            }
        }
        #endregion
    }
}
