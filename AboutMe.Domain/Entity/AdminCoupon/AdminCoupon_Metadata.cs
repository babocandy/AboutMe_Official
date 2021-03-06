﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AboutMe.Domain.Entity.AdminCoupon
{

    public class TB_COUPON_MASTER_Meatadata
    {
        public int IDX { get; set; }
        public string CD_COUPON { get; set; }
        [Required(ErrorMessage = "쿠폰명을 입력하세요.")]
        public string COUPON_NAME { get; set; }
        public string COUPON_AD_MSG { get; set; }
        public string COUPON_USE_DESCRIPTION { get; set; }
        [Required(ErrorMessage = "용도구분을 선택 하세요.")]
        public string COUPON_GBN { get; set; }
        [Required(ErrorMessage = "용도구분(2차)를 선택하세요.")]
        public string COUPON_GBN_M { get; set; }
        [Required(ErrorMessage = "정액/정률 구분을 선택하세요.")]
        public string RATE_OR_MONEY { get; set; }
        [Required(ErrorMessage = "사용기간 기준 구분을 선택하세요.")]
        public string SERVICE_LIFE_GBN { get; set; }

        [DataType(DataType.Date, ErrorMessage = "YYYY-MM-DD HH:MM:SS 형식으로 입력하세요.")]
        public Nullable<System.DateTime> FIXED_PERIOD_FROM { get; set; }
        [DataType(DataType.Date, ErrorMessage = "YYYY-MM-DD HH:MM:SS 형식으로 입력하세요.")]
        public Nullable<System.DateTime> FIXED_PERIOD_TO { get; set; }
        [Range(0, 100, ErrorMessage = "0~100사이의 값을 입력하세요")]
        public Nullable<int> EXRIRED_DAY_FROM_ISSUE_DT { get; set; }

        [DataType(DataType.Date, ErrorMessage = "YYYY-MM-DD HH:MM:SS 형식으로 입력하세요.")]
        public Nullable<System.DateTime> DOWNLOAD_DATE_FROM { get; set; }
        [DataType(DataType.Date, ErrorMessage = "YYYY-MM-DD HH:MM:SS 형식으로 입력하세요.")]
        public Nullable<System.DateTime> DOWNLOAD_DATE_TO { get; set; }

        [Required(ErrorMessage = "사용가능매체 구분을 선택하세요.")]
        public string USABLE_DEVICE_GBN { get; set; }
   
        public string PRODUCT_APP_SCOPE_GBN { get; set; }
        //[Required(ErrorMessage = "회원적용범위 구분을 선택하세요.")]
        public string MEMBER_APP_SCOPE_GBN { get; set; }

        [Required(ErrorMessage = "발행방법 구분을 선택하세요.")]
        public string ISSUE_METHOD_GBN { get; set; }
        public string ISSUE_METHOD_WITH_AUTO { get; set; }
        public string COUPON_NUM_CHECK_TF { get; set; }
        public Nullable<int> ISSUE_MAX_LIMIT { get; set; }
        [Required(ErrorMessage = "발행가능기간 시작일시를 입력하세요.")]
        [DataType(DataType.Date, ErrorMessage = "YYYY-MM-DD HH:MM:SS 형식으로 입력하세요.")]
        public Nullable<System.DateTime> MASTER_FROM_DATE { get; set; }
        [Required(ErrorMessage = "발행가능기간 종료일시를 입력하세요.")]
        [DataType(DataType.Date, ErrorMessage = "YYYY-MM-DD HH:MM:SS 형식으로 입력하세요.")]
        public Nullable<System.DateTime> MASTER_TO_DATE { get; set; }
        [Required(ErrorMessage = "활성화여부를 선택하세요.")]
        public string USABLE_YN { get; set; }
        public Nullable<System.DateTime> INS_DATE { get; set; }

        [Range(0, 100000, ErrorMessage = "0~100000사이의 값을 입력하세요")]
        public Nullable<int> COUPON_DISCOUNT_MONEY { get; set; }
        [Range(0, 100, ErrorMessage = "0~100사이의 값을 입력하세요")]
        public Nullable<int> COUPON_DISCOUNT_RATE { get; set; }

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

    public class TB_COUPON_ISSUED_DETAIL_Meatadata
    {
       [Required(ErrorMessage = "쿠폰코드를 입력하세요.")]
        public string CD_COUPON { get; set; }
        public string COUPON_VERIFI_NUMBER { get; set; }
        //public Nullable<int> COUPON_MONEY_DETAIL_NO { get; set; }
        [Range(0, 100000, ErrorMessage = "0~100000사이의 값을 입력하세요")]
        public Nullable<int> COUPON_MONEY { get; set; }
        [Range(0, 100000, ErrorMessage = "0~100사이의 값을 입력하세요")]
        public Nullable<int> COUPON_DISCOUNT_RATE { get; set; }
        public string CD_COUPON_KIND { get; set; }
        public string USE_STATUS { get; set; }
        public string USABLE_TF { get; set; }
        [Required(ErrorMessage = "발급대상 회원아이디를 입력하세요.")]
        public string M_ID { get; set; }
        public Nullable<System.DateTime> ISSUE_DATE { get; set; }
        public Nullable<System.DateTime> USABLE_DATE_FROM { get; set; }
        public Nullable<System.DateTime> USABLE_DATE_TO { get; set; }
        public Nullable<int> ORDER_NO { get; set; }
        public Nullable<int> ORDER_DETAIL_NO { get; set; }
        public string ORDER_CANCEL_FL { get; set; }
        public string RE_ISSUE_FL { get; set; }
        public Nullable<System.DateTime> RE_ISSUE_DATE { get; set; }
        public Nullable<System.DateTime> DOWNLOAD_DATE_FROM { get; set; }
        public Nullable<System.DateTime> DOWNLOAD_DATE_TO { get; set; }
        public string ISSUED_GBN { get; set; }
        public string ISSUED_MEMO { get; set; }
        [Required(ErrorMessage = "관리자 아이디를 입력하세요.")]
        public string ADMIN_ID { get; set; }
      }
}
