using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AboutMe.Domain.Entity.Common;

namespace AboutMe.Domain.Entity.Cart
{
    public class CART_INSERT
    {
        public string p_code { get; set; }
        public string p_count { get; set; }
    }

    public class SP_TB_CART_PRODUCT_ADD_Param
    {
        public SP_TB_CART_PRODUCT_ADD_Param()
        {
            this.MERGY_OPT = "N";
        }

        public string M_ID { get; set; }
        public string SESSION_ID { get; set; }
        public string P_CODE_LIST { get; set; }
        public string P_COUNT_LIST { get; set; }
        //기존에 같은 상품이 있을때 옵션  ( Y : 해당상품의 수량을 증가시킴  N : 추가행위를 무효화[기본값] )
        public string MERGY_OPT { get; set; }
    }


    public class CART_INDEX_MODEL
    {
        public Int32 DownloadCouponCnt { set; get; }
        public Int32 DownloadedCouponCnt { set; get; }
        public Int32 MyPoint { get; set; }
        public Int32 CartCnt { get; set; }
        public Int32 SumPrice { get; set; }
        public Int32 SumPoint { get; set; }
        public string BannerUrl { get; set; }
        public USER_PROFILE UserProfile { get; set; }
        public List<SP_TB_CART_LIST_Result> CartList { get; set; }
        public List<SP_TB_CART_LIST_Result> BuyWillList { get; set; }
    }
    public class CART_FLYING_MODEL
    {
        public Int32 CartCnt { get; set; }
        public Int32 CurrentPage { get; set; }
        public Int32 PrevPage { get; set; }
        public Int32 NextPage { get; set; }
        public Int32 TotalPage { get; set; }
        public IEnumerable<SP_TB_CART_LIST_Result> CartList { get; set; }
    }
}
