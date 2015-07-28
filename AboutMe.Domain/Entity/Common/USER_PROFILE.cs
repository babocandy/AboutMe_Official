using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboutMe.Domain.Entity.Common
{
    public class USER_PROFILE
    {
        public USER_PROFILE()
        {
            this.IS_LOGIN = false;
            this.M_ID = "";
            this.M_NAME = "";
            this.SESSION_ID = "";
            this.M_GRADE = "";
            this.M_GRADE_NAME = "";
            this.M_GBN = "";
            this.M_EMAIL = "";
            this.M_M_SKIN_TROUBLE_CD = "";
            this.NOMEMBER_ORDER_CODE = "";
        }
        public bool IS_LOGIN { get; set; }
        public string M_ID { get; set; }
        public string M_NAME { get; set; }
        public string SESSION_ID { get; set; }
        public string M_GRADE { get; set; } //사용자 등급코드 B/S/G/V  varchar(1)
        public string M_GRADE_NAME { get; set; }
        public string M_GBN { get; set; } //회원 구분  S:임직원 , A or기타:일반회원 varchar(1)
        public string M_EMAIL { get; set; }
        public string M_M_SKIN_TROUBLE_CD { get; set; } //회원 피부트러블 코드  char(9)
        public string NOMEMBER_ORDER_CODE { get; set; } //비회원 주문조회코드

    }
}
