using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AboutMe.Domain.Entity.AdminPromotion
{
    public class TB_PROMOTION_BY_TOTAL_Metadata
    {
          
        
        public int IDX { get; set; }
        public string CD_PROMOTION_TOTAL { get; set; }
        
        [Required(ErrorMessage = "프로모션 이름을 입력하세요.")]
        public string PMO_TOTAL_NAME { get; set; }
        
        public string PMO_TOTAL_CATEGORY { get; set; }
        
        //[Required(ErrorMessage = "정률/정액 옵션을 선택하세요.")]
        public string PMO_TOTAL_RATE_OR_MONEY { get; set; }

     
        //[Range(typeof(int), "0", "100", ErrorMessage = "0~100 사이의 숫자형식으로 입력하세요.")]
        [Range(0, 100000, ErrorMessage = "0~100000 사이의 숫자형식으로 입력하세요.")]
        public int? PMO_TOTAL_DISCOUNT_RATE { get; set; }

        //[Range(0, 100000, ErrorMessage = "0~100000 사이의 숫자형식으로 입력하세요.")]
        public short? PMO_TOTAL_DISCOUNT_MONEY { get; set; }
        
        [Required(ErrorMessage = "시작일자를 입력하세요.")]
        [DataType(DataType.Date,ErrorMessage="YYYY-MM-DD 날짜 형식으로 입력하세요.")]
        public System.DateTime? PMO_TOTAL_DATE_FROM { get; set; }
        
        [Required(ErrorMessage = "종료일자를 입력하세요.")]
        [DataType(DataType.Date,ErrorMessage="YYYY-MM-DD 날짜 형식으로 입력하세요.")]
        public System.DateTime? PMO_TOTAL_DATE_TO { get; set; }
        public string USABLE_YN { get; set; }
        public Nullable<System.DateTime> INS_DATE { get; set; }
    
    }


    public class TB_PROMOTION_BY_PRODUCT_Metadata
    {


        public int IDX { get; set; }
        public string CD_PROMOTION_PRODUCT { get; set; }

        [Required(ErrorMessage = "프로모션 이름을 입력하세요.")]
        public string PMO_PRODUCT_NAME { get; set; }

        public string PMO_PRODUCT_CATEGORY { get; set; }

        //[Required(ErrorMessage = "정률/정액 옵션을 선택하세요.")]
        public string PMO_PRODUCT_RATE_OR_MONEY { get; set; }


        //[Range(typeof(int), "0", "100", ErrorMessage = "0~100 사이의 숫자형식으로 입력하세요.")]
        //[Range(0, 100000, ErrorMessage = "0~100000 사이의 숫자형식으로 입력하세요.")]
        public int? PMO_PRODUCT_DISCOUNT_RATE { get; set; }

        //[Range(0, 100000, ErrorMessage = "0~100000 사이의 숫자형식으로 입력하세요.")]
        public short? PMO_PRODUCT_DISCOUNT_MONEY { get; set; }

        [Required(ErrorMessage = "시작일자를 입력하세요.")]
        [DataType(DataType.Date, ErrorMessage = "YYYY-MM-DD 날짜 형식으로 입력하세요.")]
        public System.DateTime? PMO_PRODUCT_DATE_FROM { get; set; }

        [Required(ErrorMessage = "종료일자를 입력하세요.")]
        [DataType(DataType.Date, ErrorMessage = "YYYY-MM-DD 날짜 형식으로 입력하세요.")]
        public System.DateTime? PMO_PRODUCT_DATE_TO { get; set; }
        public string USABLE_YN { get; set; }
        public Nullable<System.DateTime> INS_DATE { get; set; }
    
    
    }



    public partial class TB_PROMOTION_BY_PRODUCT_PRICE_Meatadata
    {
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
    }
}
