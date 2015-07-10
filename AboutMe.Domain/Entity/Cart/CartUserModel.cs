using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

    public class USER_PROFILE
    {
        public bool IS_LOGIN {get; set; }
        public string M_ID { get; set; }
        public string M_NAME  { get; set; }
        public string SESSION_ID { get; set; }
        public string M_GRADE  { get; set; } //사용자 등급코드 B/S/G/V  varchar(1)
        public string M_GRADE_NAME   { get; set; }
        public string M_GBN   { get; set; } //회원 구분  S:임직원 , A or기타:일반회원 varchar(1)
        public string M_EMAIL { get; set; }
        public string M_M_SKIN_TROUBLE_CD { get; set; } //회원 피부트러블 코드  char(9)

    }

    public class CART_INDEX_MODEL
    {
        public USER_PROFILE UserProfile { get; set; }
        public List<SP_TB_CART_PRODUCT_ADD_Param> CartLIst { get; set; }
    }
}
