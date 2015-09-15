using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AboutMe.Domain.Entity.Common;

namespace AboutMe.Domain.Entity.AdminOrder
{

    public class SP_TB_ADMIN_ORDER_Param
    {
        public SP_TB_ADMIN_ORDER_Param()
        {
            this.TO_DATE = DateTime.Today.ToString().Substring(0,10);
            this.FROM_DATE = DateTime.Today.AddDays(-7).ToString().Substring(0, 10);
            this.MEMBER_GBN = "0";
            this.MEMBER_GRADE_B = true;
            this.MEMBER_GRADE_S = true;
            this.MEMBER_GRADE_G = true;
            this.MEMBER_GRADE_V = true;
            this.PAY_GBN = "A";
            this.PAT_GUBUN_W = true;
            this.PAT_GUBUN_M = true;
            this.DETAIL_STATUS_20 = true; //결재완료
            this.DETAIL_STATUS_10 = false;
            this.DETAIL_STATUS_30 = false;
            this.DETAIL_STATUS_40 = false;
            this.DETAIL_STATUS_50 = false;
            this.DETAIL_STATUS_60 = false;
            this.DETAIL_STATUS_90 = false;
            this.DETAIL_STATUS_80 = false;
            this.DETAIL_STATUS_70 = false;
            this.DELIVERY_FROM_DATE = "";
            this.DELIVERY_TO_DATE = "";
            this.SEARCH_COL = "";
            this.SEARCH_KEYWORD = "";
            this.PAGE = 1;
            this.PAGESIZE = 20;
        }
        
         private string _delivery_from_date;
         private string _delivery_to_date;
         private string _search_col;
         private string _search_keyword;

        public string FROM_DATE { get; set; }
        public string TO_DATE { get; set; }
        public string MEMBER_GBN { get; set; } // 회원구분 (0:전체, 일반:1, 비회원:2, 임직원:3)
        public bool MEMBER_GRADE_B { get; set; }
        public bool MEMBER_GRADE_S { get; set; }
        public bool MEMBER_GRADE_G { get; set; }
        public bool MEMBER_GRADE_V { get; set; }
        public string PAY_GBN { get; set; }     //결제수단 (A:전체, 1:신용카드, 2:실시간계좌이체, 3:가상계좌, 4:포인트결제)
        public bool PAT_GUBUN_W { get; set; }
        public bool PAT_GUBUN_M { get; set; }
        public bool DETAIL_STATUS_20 { get; set; }
        public bool DETAIL_STATUS_10 { get; set; }
        public bool DETAIL_STATUS_30 { get; set; }
        public bool DETAIL_STATUS_40 { get; set; }
        public bool DETAIL_STATUS_50 { get; set; }
        public bool DETAIL_STATUS_60 { get; set; }
        public bool DETAIL_STATUS_90 { get; set; }
        public bool DETAIL_STATUS_80 { get; set; }
        public bool DETAIL_STATUS_70 { get; set; }
        public string DELIVERY_FROM_DATE // 시작 출고일자 (ex 2015-07-09)
        {
            get
            {
                return this._delivery_from_date ?? "";
            }
            set
            {
                this._delivery_from_date = value;
            }
        }	

        public string DELIVERY_TO_DATE  // 종료 출고일자 (ex 2015-07-09)
        {
            get
            {
                return this._delivery_to_date ?? "";
            }
            set
            {
                this._delivery_to_date = value;
            }
        } 
        public string SEARCH_COL //	기타검색필드명 (ORDER_CODE 주문코드,M_ID 회원아이디,P_NAME 상품명,M_NAME 주문자명,RECEIVER_NAME 수취인명,SENDER_HP 보낸사람HP,SENDER_TEL 보낸사람TEL,RECEIVER_HP 수취인HP,RECEIVER_TEL 수취인TEL )
        {
            get
            {
                return this._search_col ?? "";
            }
            set
            {
                this._search_col = value;
            }
        }
        public string SEARCH_KEYWORD
        {
            get
            {
                return this._search_keyword ?? "";
            }
            set
            {
                this._search_keyword = value;
            }
        }

        public int PAGE { get; set; }
        public int PAGESIZE { get; set; }
    }

    public class ORDER_INDEX_MODEL
    {
        public SP_TB_ADMIN_ORDER_Param SearchOption { set; get; }
        public SP_ADMIN_ORDER_STATUS_Result OrderStatusCnt { set; get; }
        public List<SP_ADMIN_ORDER_LIST_Result> OrderList { get; set; }
        public Int32 TotalCount { get; set; }
    }

    public class ORDER_DETAIL_MODEL
    {
        public Int32 ORDER_IDX { get; set; }
        public SP_TB_ADMIN_ORDER_Param SearchOption { set; get; }
        public SP_ADMIN_ORDERLIST_DETAIL_BODY_Result BodyInfo { set; get; }
        public List<SP_ADMIN_ORDERLIST_DETAIL_LIST_Result> OrderDetailList { get; set; }
        public List<string> LogList { get; set; }
    }

    public class INIREPAY_MODEL
    {
        public Int32 ORDER_IDX { get; set; }
        public SP_ADMIN_ORDERLIST_DETAIL_BODY_Result MasterInfo { set; get; }
        public SP_ADMIN_ORDER_PART_CANCEL_TOP_SELECT_Result Top1Info { set; get; }
        public List<SP_ADMIN_ORDER_PART_CANCEL_SELECT_Result> RepayList { get; set; }
    }

    public class INIREPAY_OK_MODEL
    {
        public Int32 ORDER_IDX { get; set; }
        public string oldtid { set; get; }   //원거래번호
        public string price { set; get; }    //취소할금액
        public string confirm_price { set; get; }  //재승인금액 (결제금액-취소할금액)
        public string buyeremail { set; get; } //이메일주소
    }
    public class INIREPAY_RESULT
    {
        public Int32 ORDER_IDX { get; set; }
        public string TID { set; get; }   //신 거래번호
        public string ResultCode { set; get; }    //부분취소결과코드 "00"이면 성공 그외 실패
        public string ResultMsg { set; get; } //결과메시지
        public string PRTC_TID { set; get; }  //원거래TID
        public string PRTC_Remains { set; get; }  //최종결제금액
        public string PRTC_Price { set; get; }  //부분취소한금액
        public string PRTC_Cnt { set; get; }  //재승인횟수(최대9회)
        public string PRTC_Type { set; get; }  //부분취소,재승인 구분값(0:재승인  1:부분취소) 
    }

    public class DELIVERTY_EXCEL_RESULT
    {

        public string ORDER_IDX { get; set; }
        public string ORDER_DETAIL_IDX { set; get; }
        public string ORDER_CODE { set; get; }
        public string ORDER_DATE { set; get; }
        public string ORDER_NAME { set; get; }

        public string RECEIVER_NAME { get; set; }
        public string RECEIVER_TEL { get; set; }
        public string RECEIVER_HP { get; set; }
        public string RECEIVER_ADDR { get; set; }
        public string ORDER_MEMO { get; set; }

        public string EMP_YN { set; get; }
        public string P_CODE { set; get; }
        public string P_NAME { set; get; }
        public string P_COUNT { set; get; }
        public string PAY_NM { set; get; }
        public string REAL_PAY_PRICE { set; get; }
        public string ORDER_STATUS_NM { set; get; }   
        public string PROMOTION_NM { set; get; }   
        public string PMO_PRODUCT_NAME { set; get; }   
        public string FREEGIFT_NAME { set; get; }
        public string DELIVERY_NUM { set; get; }   
    }


    public class ORDER_MEMBER_MODEL
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string M_ID { get; set; }
        public int OrderCount { get; set; }
        public List<SP_ADMIN_ORDER_LIST_Result> OrderList { get; set; }
    }

    public class INIESCROW_DELIVERY_RESULT
    {
        public string tid { get; set; }
        public string resultcode { get; set; }
        public string resultmsg { get; set; }  //I:배송등록, U:배송수정
        public string DLV_Date { get; set; } //운송장 번호
        public string DLV_Time { get; set; } //배송등록자
    }


}
