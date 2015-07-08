using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AboutMe.Domain.Entity.Cart
{
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
}
