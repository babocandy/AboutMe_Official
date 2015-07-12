using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboutMe.Domain.Entity.AdminCoupon
{

    public partial class TB_COUPON_MASTER_Meatadata
    {
        public string IDX { get; set; }
        public string CD_COUPON { get; set; }
        public string COUPON_GBN { get; set; }
        public string RATE_OR_MONEY { get; set; }
        public string SERVICE_LIFE_GBN { get; set; }
        public Nullable<System.DateTime> FIXED_PERIOD_FROM { get; set; }
        public Nullable<System.DateTime> FIXED_PERIOD_TO { get; set; }
        public Nullable<int> EXRIRED_DAY_FROM_ISSUE_DT { get; set; }
        public string USABLE_DEVICE_GBN { get; set; }
        public string PRODUCT_APP_SCOPE_GBN { get; set; }
        public string MEMBER_APP_SCOPE_GBN { get; set; }
        public string ISSUE_METHOD_GBN { get; set; }
        public string ISSUE_METHOD_WITH_AUTO { get; set; }
        public string COUPON_NUM_CHECK_TF { get; set; }
        public Nullable<int> ISSUE_MAX_LIMIT { get; set; }
        public Nullable<System.DateTime> MASTER_FROM_DATE { get; set; }
        public Nullable<System.DateTime> MASTER_TO_DATE { get; set; }
        public Nullable<System.DateTime> USABLE_YN { get; set; }
        public Nullable<System.DateTime> INS_DATE { get; set; }

        /**
        public int IDX { get; set; }
        public string CD_PROMOTION_PRODUCT { get; set; }
        [Required(ErrorMessage = "상품코드를 입력하세요.")]
        public string P_CODE { get; set; }
        public Nullable<int> P_DISCOUNT_PRICE { get; set; }
        public string PMO_PRODUCT_RATE_OR_MONEY { get; set; }
        public Nullable<byte> PMO_PRODUCT_DISCOUNT_RATE { get; set; }
        public Nullable<int> PMO_PRODUCT_DISCOUNT_MONEY { get; set; }

        [Required(ErrorMessage = "최종할인가를 입력하세요.")]
        [Range(0, 1000000, ErrorMessage = "0~1000000 사이의 숫자형식으로 입력하세요.")]
        public Nullable<int> PMO_PRICE { get; set; }
        public string PMO_ONE_ONE_P_CODE { get; set; }
        [Range(0, 1000000, ErrorMessage = "0~1000000 사이의 숫자형식으로 입력하세요.")]
        public Nullable<int> PMO_ONE_ONE_PRICE { get; set; }

        [Required(ErrorMessage = "활성화여부를 선택하세요.")]
        public string USABLE_YN { get; set; }
        public Nullable<System.DateTime> INS_DATE { get; set; }
         * **/
    }
}
