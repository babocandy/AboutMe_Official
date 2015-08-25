using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AboutMe.Domain.Entity.Common;

namespace AboutMe.Domain.Entity.OrderStat
{

    // 일별 주문현황
    public class OrderStatDay
    {
        public OrderStatDay()
        {
            this.TO_DATE = DateTime.Today.ToString().Substring(0, 10);
            this.FROM_DATE = DateTime.Today.AddDays(-7).ToString().Substring(0, 10);
            this.PAT_GUBUN1 = true;
            this.PAT_GUBUN2 = true;
            this.MEMBER_GBN1 = true;
            this.MEMBER_GBN2 = true;
            this.MEMBER_GBN3 = true;
        }
        public string FROM_DATE { get; set; }
        public string TO_DATE { get; set; }
        public bool PAT_GUBUN1 { get; set; }
        public bool PAT_GUBUN2 { get; set; }
        public bool MEMBER_GBN1 { get; set; }
        public bool MEMBER_GBN2 { get; set; }
        public bool MEMBER_GBN3 { get; set; }

        
    }

    public class OrderStatIndex
    {
        public OrderStatDay searchParam { get; set; }
        public List<SP_ADMIN_STAT_ORDER_DAY_Result> OrderStatDayList { get; set; }
    }


    // 카테고리별 매출통계
    public class OrderStatCate
    {
        public OrderStatCate()
        {
            this.TO_DATE = DateTime.Today.ToString().Substring(0, 10);
            this.FROM_DATE = DateTime.Today.AddDays(-7).ToString().Substring(0, 10);
            this.PAT_GUBUN1 = true;
            this.PAT_GUBUN2 = true;
            this.MEMBER_GBN1 = true;
            this.MEMBER_GBN2 = true;
            this.MEMBER_GBN3 = true;
            this.P_CATE_CODE = "";
            this.C_CATE_CODE = "";
            this.L_CATE_CODE = "";
            this.cateCode = "";
        }
        public string FROM_DATE { get; set; }
        public string TO_DATE { get; set; }
        public bool PAT_GUBUN1 { get; set; }
        public bool PAT_GUBUN2 { get; set; }
        public bool MEMBER_GBN1 { get; set; }
        public bool MEMBER_GBN2 { get; set; }
        public bool MEMBER_GBN3 { get; set; }
        public string P_CATE_CODE { get; set; }
        public string C_CATE_CODE { get; set; }
        public string L_CATE_CODE { get; set; }
        public string cateCode { get; set; }


    }

    public class OrderStatCateIndex
    {
        public OrderStatCate searchParam { get; set; }
        public List<SP_ADMIN_STAT_ORDER_CATEGORY_Result> OrderStatCateList { get; set; }
    }
}
