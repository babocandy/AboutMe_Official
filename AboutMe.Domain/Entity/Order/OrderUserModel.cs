using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AboutMe.Domain.Entity.Common;

namespace AboutMe.Domain.Entity.Order
{
    public class ORDER_STEP1_MODEL
    {
        public Int32 OrderIdx { get; set; }
        public USER_PROFILE UserProfile { get; set; }
    }

    public class ORDER_STEP1_ORDERLIST
    {
        public Int32 OrderIdx { get; set; }
        public USER_PROFILE UserProfile { get; set; }
        public List<SP_ORDER_STEP2_PRODUCT_LIST_Result> OrderList { get; set; }
    }

    public class ORDER_STEP1_PRICEINFO
    {
        public Int32 OrderIdx { get; set; }
        public USER_PROFILE UserProfile { get; set; }
        public SP_ORDER_STEP2_PRICE_INFO_Result PriceInfo { get; set; }
    }
    public class ORDER_STEP1_DISCOUNTINFO
    {
        public ORDER_STEP1_DISCOUNTINFO()
        {
            this.MyPoint = 0;
        }
        public Int32 OrderIdx { get; set; }
        public Int32 MyPoint { get; set; }
        public USER_PROFILE UserProfile { get; set; }
        public string TransCouponDisabled { get; set; }  //무료배송쿠폰 사용가능여부 (결제금액이 30000만원이상일경우에만 활성화)
        public SP_ORDER_STEP2_DISCOUNT_INFO_Result DiscountInfo { get; set; }
        public SP_ORDER_STEP2_PRICE_INFO_Result PriceInfo { get; set; }
        public List<SP_ORDER_STEP2_COUPON_SEARCH_Result> TransCouponList { get; set; }
    }
    public class ORDER_STEP1_ADDRESSINFO
    {
        public Int32 OrderIdx { get; set; }
        public USER_PROFILE UserProfile { get; set; }
        public SP_ORDER_STEP2_RECENTADDR_INFO_Result RecentAddrInfo { get; set; }
        public SP_ORDER_STEP2_BASEADDR_INFO_Result BaseAddrInfo { get; set; }
    }
    public class ORDER_STEP1_PAYINFO
    {
        public Int32 OrderIdx { get; set; }
        public USER_PROFILE UserProfile { get; set; }
        public string GoodName { get; set; }
        public SP_ORDER_STEP2_PRICE_INFO_Result PriceInfo { get; set; }
        public INISYSPAY_MODEL InisysStart { get; set; }
    }

    public class ORDER_STEP1_MOBILE_PAYINFO
    {
        public Int32 OrderIdx { get; set; }
        public USER_PROFILE UserProfile { get; set; }
        public string GoodName { get; set; }
        public SP_ORDER_STEP2_PRICE_INFO_Result PriceInfo { get; set; }
        public INISYSPAY_MODEL InisysStart { get; set; }
        public string Mid { get; set; }
        public string TotalPrice { get; set; }
        public string P_NEXT_URL { get; set; }
        public string P_NOTI_URL { get; set; }
        public string P_RETURN_URL { get; set; }
    }

    public class COUPON_APPLY_MODEL
    {
        public string ORDER_DETAIL_IDX { get; set; }
        public string COUPON_IDX { get; set; }
    }
    
    public class SENDER_RECEIVER_SAVE_Param
    {  
        public string SENDER_NAME { get; set; }      
        public string SENDER_POST { get; set; }     
        public string SENDER_ADDR1 { get; set; }    
        public string SENDER_ADDR2 { get; set; }    
        public string SENDER_TEL1 { get; set; }     
        public string SENDER_TEL2 { get; set; }     
        public string SENDER_TEL3 { get; set; }     
        public string SENDER_HP1 { get; set; }      
        public string SENDER_HP2 { get; set; }      
        public string SENDER_HP3 { get; set; }      
        public string SENDER_EMAIL1 { get; set; }   
        public string SENDER_EMAIL2 { get; set; }  

        public string RECEIVER_NAME { get; set; }  
        public string RECEIVER_POST { get; set; } 
        public string RECEIVER_ADDR1 { get; set; }
        public string RECEIVER_ADDR2 { get; set; }
        public string RECEIVER_TEL1 { get; set; } 
        public string RECEIVER_TEL2 { get; set; } 
        public string RECEIVER_TEL3 { get; set; } 
        public string RECEIVER_HP1 { get; set; }  
        public string RECEIVER_HP2 { get; set; }  
        public string RECEIVER_HP3 { get; set; }  
        public string ORDER_MEMO { get; set; }    
        public string NOMEMBER_PASS { get; set; }
    }

    public class INISYSPAY_MODEL
    {
        public string resultcode { get; set; } //결과코드 성공이면 '00' 실패 '01'
        public string resultmsg { get; set; }  //결과메세지 
        public string rn_value { get; set; }     // 암호화 결과값
        public string return_enc { get; set; }  // 암호화 결과값
        public string ini_certid { get; set; }  // 암호화 결과값
        public string ini_encfield { get; set; }
        public string err_msg { get; set; }

    }

    public class INISYSPAY_PARAM
    {
        public string gopaymethod { get; set; } 
        public string goodname { get; set; }  
        public string buyername { get; set; } 
        public string buyeremail { get; set; }
        public string buyertel { get; set; }  
        public string acceptmethod { get; set; }
        public string oid { get; set; }
        public string INIregno { get; set; }
        public string ini_encfield { get; set; }
        public string ini_certid { get; set; }
        public string quotainterest { get; set; }
        public string paymethod { get; set; }
        public string cardcode { get; set; }
        public string cardquota { get; set; }
        public string rbankcode { get; set; }
        public string reqsign { get; set; }
        public string encrypted { get; set; }
        public string sessionkey { get; set; }
        public string uid { get; set; }
        public string sid { get; set; }
        public string version { get; set; }
        public string PAY_GBN { get; set; }
        public string NOMEMBER_PASS { get; set; }


    }
    
    public class INISYSPAY_RESULT
    {
        //-------------------------------------------------------------------------------
        // 가.모든 결제 수단에 공통되는 결제 결과 내용
        //-------------------------------------------------------------------------------
        public string Tid { get; set; }              // 거래번호
        public string Resultcode { get; set; }       // 결과코드 ("00"이면 지불성공)
        public string ResultMsg { get; set; }        // 결과내용 : resultMsg 는 결제실패시 추적할수 있는 단서입니다. 반드시 결과페이지에 출력하시기 바랍니다.
        public string PayMethod { get; set; }        // 지불방법 (매뉴얼 참조)
        public string Moid { get; set; }             // 상점 사용 주문번호 (ORDER_IDX)
        public string GoodsPrice { get; set; }       //결제결과금액

        //-------------------------------------------------------------------------------
        // 나. 신용카드,ISP,핸드폰, 전화 결제, 은행계좌이체, OK CASH BAG Point 결제시에만 결제 결과 내용  (무통장입금 , 문화 상품권 , 네모 제외) 
        public string ApplDate { get; set; }        //이니시스 승인날짜 (무통장입금 제외)
        public string ApplTime { get; set; }            //이니시스 승인시각 (무통장입금 제외)
        //-------------------------------------------------------------------------------
        // 다. 신용카드  결제수단을 이용시에만  결제결과 내용
        //-------------------------------------------------------------------------------
        public string ApplNum { get; set; }         //신용카드 승인번호
        public string CARD_Quota { get; set; }     //할부기간
        public string CARD_Interest { get; set; }   //무이자할부 여부("1"이면 무이자할부)
        public string CARD_Num { get; set; }        //카드번호 12자리	
        public string CARD_Code { get; set; }       //신용카드사 코드 메뉴얼이나 샘플폴더 안의 //카드사_은행_코드.txt// 파일을 참고하세요
        public string CARD_BankCode { get; set; }   //신용카드 발급사(은행) 코드 (매뉴얼 참조)
        public string CARD_AuthType { get; set; }   //본인인증 수행여부 ("00"이면 수행)	
        public string EventCode { get; set; }       //각종 이벤트 적용 여부	
        //아래 내용은 "신용카드 및 OK CASH BAG 복합결제" 또는"신용카드 지불시에 OK CASH BAG적립"시에 추가되는 내용
        public string OCB_ApplTime { get; set; }    //OK Cashbag 적립 승인번호
        public string OCB_SaveApplNum { get; set; }	//OK Cashbag 적립 승인번호
        public string OCB_PayApplNum { get; set; }  //OK Cashbag 사용 승인번호
        public string OCB_ApplDate { get; set; }    //OK Cashbag 승인일시
        public string OCB_Num { get; set; }         //OK Cashbag 번호
        public string CARD_ApplPrice { get; set; }  //OK Cashbag 복합결재시 신용카드 지불금액
        public string OCB_PayPrice { get; set; }    //OK Cashbag 복합결재시 포인트 지불금액
        
        
        //-------------------------------------------------------------------------------
        // 라. 은행계좌이체 결제수단을 이용시에만  결제결과 내용
        //	오직 은행계좌시에만 실시 현금 영수증 발행이 가능하며, 가상계좌는 상점관리자 화면이나, 독립적인 현금영수증 발행(이니시스 기술자료실) 모듈을 사용하세요
        //-------------------------------------------------------------------------------
        public string ACCT_BankCode { get; set; } 	//은행코드
        public string rcash_rslt { get; set; } 	//현금영주증 결과코드 ("0000"이면 지불성공)
        public string ruseopt { get; set; } 		//현금영수증 발행구분코드 

        //-------------------------------------------------------------------------------
        // 마. 무통장 입금(가상계좌) 결제수단을 이용시 결과 내용
        //-------------------------------------------------------------------------------

        public string VACT_Num { get; set; } 		// 입금 계좌 번호
        public string VACT_BankCode { get; set; } 	// 입금 은행 코드
        public string VACT_Date { get; set; } 		// 입금 예정 날짜
        public string VACT_Time { get; set; } 	// 입금 예정 시간 [ 20061018 ]
        public string VACT_InputName { get; set; } 	// 송금자명
        public string VACT_Name { get; set; } 		// 예금주명

        //-------------------------------------------------------------------------------
        // 바. 핸드폰, 전화결제시에만  결제 결과 내용 ( "실패 내역 자세히 보기"에서 필요 , 상점에서는 필요없는 정보임)
        //-------------------------------------------------------------------------------
        public string HPP_GWCode { get; set; } 	//핸드폰,전화결제시 gateway


        //-------------------------------------------------------------------------------
        // 사. 핸드폰 결제수단을 이용시에만  결제 결과 내용
        //-------------------------------------------------------------------------------
        public string HPP_Num { get; set; } 		//핸드폰 결제에 사용된 휴대폰번호

        //-------------------------------------------------------------------------------
        // 아. ARS 전화 결제수단을 이용시에만  결제 결과 내용
        //-------------------------------------------------------------------------------
        public string ARSB_Num { get; set; }		//전화결제에  사용된 전화번호

        // 자. 받는 전화 결제수단을 이용시에만  결제 결과 내용
        //-------------------------------------------------------------------------------
        public string PHNB_Num { get; set; }		//전화결제에  사용된 전화번호

        //-------------------------------------------------------------------------------
        // 차. 문화 상품권 결제수단을 이용시에만  결제 결과 내용	
        //-------------------------------------------------------------------------------
        public string CULT_UserID { get; set; }		//문화상품권 ID

        //-------------------------------------------------------------------------------
        // 카. 현금영수증 발급 결과코드 (은행계좌이체시에만 리턴);
        //-------------------------------------------------------------------------------
        public string CSHR_ResultCode { get; set; }	// 결과코드
        public string CSHR_Type { get; set; }	//발급구분코드

        //-------------------------------------------------------------------------------
        // 파. 틴캐시 결제수단을 이용시에만 결제 결과 내용
        //-------------------------------------------------------------------------------
        public string TEEN_Remains { get; set; }	//틴캐시 잔액
        public string TEEN_UserID { get; set; }   	//틴캐시 ID

        //-------------------------------------------------------------------------------
        // 타.스마트문상(게임 문화 상품권) 결제수단을 이용시에만 결제 결과 내용
        //-------------------------------------------------------------------------------
        public string GAMG_Cnt { get; set; }		//카드 사용 갯수


        //-------------------------------------------------------------------------------
        // 하. 도서문화 상품권 결제수단을 이용시에만 결제 결과 내용
        //-------------------------------------------------------------------------------
        public string BCSH_UserID { get; set; }	//문화상품권 ID

        //-------------------------------------------------------------------------------
        // * . 모든 결제 수단에 대해 결제 실패시에만 결제 결과 데이터 				
        //-------------------------------------------------------------------------------
        public string ResultErrorCode { get; set; }	//결제실패시 상세에러코드 

    }

    public class INIPAYRESULT
    {
        public string Resultcode { get; set; }       // 결과코드 ("00"이면 지불성공)
        public string ResultMsg { get; set; }        // 결과내용 : resultMsg 는 결제실패시 추적할수 있는 단서입니다. 반드시 결과페이지에 출력하시기 바랍니다.
        public string ResultErrorCode { get; set; }     
    }


    public class OrderResult
    {
        public string Resultcode { get; set; }       // 결과코드 ("00"이면 지불성공)
        public string ResultMsg { get; set; }        // 결과내용 : resultMsg 는 결제실패시 추적할수 있는 단서입니다. 반드시 결과페이지에 출력하시기 바랍니다.
        public string ResultErrorCode { get; set; }     
        public int ORDER_IDX { get; set; } //Temp테이블 order_idx
        public string PAY_GBN { get; set; }
        public string ORDER_CODE { get; set; }
    }

    public class ORDER_PAY_PARAM
    {
        public int ORDER_IDX{ get; set; }
        public string PAY_GBN { get; set; }
        public string CARD_GBN { get; set; }   //카드사구분
        public string INSTLMT_AT { get; set; }  //할부여부
        public string BANK_CODE  { get; set; }
        public string PAT_TID { get; set; }
        public string REAL_ACCOUNT_AT { get; set; }   //실시간계좌사용여부
        public string CASHRECEIPT_SE_CODE { get; set; } // 현금영수증발급여부구분
        public string CASHRECEIPT_RESULT_CODE { get; set; } // 현금영수증발급여부구분
        public string HTTP_USER_AGENT { get; set; } // 사용자브라우저
        public string PAT_GUBUN { get; set; } 	// 결제방법구분 (Mobile :모바일, Web : 웹페이지)
        public string SVR_DOMAIN { get; set; } // 도메인
        public string VACT_Num { get; set; } // 가상계좌결제관련 - 입금계좌번호
        public string VACT_BankCode { get; set; } // 가상계좌결제관련 - 입금은행코드
        public string VACT_Name { get; set; } // 가상계좌결제관련 - 예금주명
        public string VACT_InputName { get; set; } // 가상계좌결제관련 - 송금자명
        public string VACT_Date { get; set; } // 가상계좌결제관련 - 송금일자
        public string VACT_Time { get; set; } // 가상계좌결제관련 - 송금시각
        public string ESCROW_YN { get; set; } //에스크로여부
        public string ORDER_STATUS_VALUE { get; set; } //모바일 실시간 계좌이체일경우 비동기방식이어서 입금대기로 상태를 주는경우 사용
    }

    //주문결과 페이지 모델
    public class ORDER_STEP2_MODEL
    {
        public OrderResult OrderResult { get; set; }
        public USER_PROFILE UserProfile { get; set; }
        public List<SP_ORDER_RESULT_PRODUCT_LIST_Result> ProductList { get; set; }
        public SP_ORDER_RESULT_DETAIL_Result DetailInfo { get; set; }
    }
    
    //MyPage 주문 목록
    public class MyORDER_INDEX
    {
        public int? Page { get; set; }
        public int PageSize { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int OrderCnt { get; set; }
        public USER_PROFILE UserProfile { get; set; }
        public List<SP_MYPAGE_ORDERLIST_Result> OrderList { get; set; }
    }

    //MyPage 주문 상세
    public class MyORDER_VIEW
    {
        public int? Page { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public USER_PROFILE UserProfile { get; set; }
        public SP_MYPAGE_ORDERLIST_DETAIL_INFO_Result DetailInfo { get; set; }
        public List<SP_MYPAGE_ORDERLIST_DETAIL_PRODUCT_LIST_Result> ProductList { get; set; }
    }

    public class INIPAYMOBILE_NEXT_RETURN
    { 
        public string P_STATUS {get; set;}   //00 성공, 그외실패
        public string P_RMESG1 { get; set; }  //결과메시지
        public string P_TID { get; set; }     //거래인정번호(성공시에만)
        public string P_REQ_URL { get; set; } //승인요청 URL
        public string P_NOTI { get; set; }    //거래시 설정한 변수값
    }
    
    public class INIPAYMOBILE_CALL_RETURN
    {
        public string P_STATUS { get; set; }   //00 성공, 그외실패
        public string P_TID { get; set; }     //거래인정번호(성공시에만)
        public string P_OID { get; set; }     //주문번호
        public string P_AUTH_DT { get; set; }   //승인일자 char(14) YYYYmmddHHmmss 
        public string P_TYPE { get; set; }     //지불수단  CARD(ISP,안심클릭,국민앱카드,케이 페이) HPMN(해피머니) CULTURE(문화상품권) MOBILE(휴대폰) VBANK(가상계좌) EWALLET(전자지갑) ETC_(알리페이,페이팔 외 기타) 
        public string P_AMT { get; set; }      //거래금액 char(8) 
        public string P_RMESG2 { get; set; }   //신용카드 할부 여부
        public string P_CARD_PURCHASE_CODE { get; set; }   //신용카드 발급사 코드 
        public string P_FN_CD1 { get; set; }    //신용카드 발급사 코드 
        public string P_NOTI { get; set; }    //임시변수 (에스크로여부저장)

        public string P_VACT_NUM { get; set; }
        public string P_VACT_DATE { get; set; }
        public string P_VACT_TIME { get; set; }
        public string P_VACT_NAME { get; set; }
        public string P_VACT_BANK_CODE { get; set; }
    }

    public class ORDER_MOBILE_PARAM
    {
        public string P_GOODS { get; set; }
        public string P_MID { get; set; }
        public string P_AMT { get; set; }
        public string P_OID { get; set; }
        public string P_MNAME { get; set; }
        public string P_NOTI { get; set; }
        public string P_EMAIL { get; set; }
        public string P_UNAME { get; set; }
        public string P_MOBILE { get; set; }
        public string P_NEXT_URL { get; set; }
        public string P_NOTI_URL { get; set; }
        public string P_RETURN_URL { get; set; }
        public string P_RESERVED { get; set; }
    }
    
    
}
