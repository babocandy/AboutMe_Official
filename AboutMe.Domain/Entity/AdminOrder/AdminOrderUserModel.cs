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
            this.PAGESIZE = 5;
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
        public List<SP_ADMIN_ORDER_LIST_Result> OrderList { get; set; }
        public Int32 TotalCount {get; set;}
    }

    /*
    public class CART_INSERT
    {
        public string p_code { get; set; }
        public string p_count { get; set; }
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
     * */
}
