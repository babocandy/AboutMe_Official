using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.Cart;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Domain.Service.Cart
{
    public interface ICartService
    {
        //List
        List<SP_TB_CART_LIST_Result> CartList(string m_id, string session_id);
        //Count
        int CartListCount(string m_id, string session_id);
        //Insert
        void CartInsert(SP_TB_CART_PRODUCT_ADD_Param newItem);
        //Delete
        void CartDelete(string m_id, string session_id, string cart_idx);
        //Merge
        void CartMerge(string m_id, string session_id, string merge_opt);
        //Update Count
        void CartUpdateCnt(string m_id, string session_id, int cart_idx, int? p_count = 1);

        int WishListCount(string m_id);
        //Insert
        int WishInsert(string m_id, string p_code);
        //Delete
        int WishDelete(string m_id, int idx);
    }
}
