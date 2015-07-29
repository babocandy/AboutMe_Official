using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AboutMe.Domain.Entity.Common;

namespace AboutMe.Domain.Entity.Recallbbs
{
    public class RECALLBBS_SEARCH
    {
        public RECALLBBS_SEARCH()
        { 
            this.start_date = "";
            this.end_date = "";
            this.reg_id = "";
            this.order_code = "";
            this.dateType = "5";    //기간 검색 OR 전체
            this.now_date = DateTime.Now.ToString("yyyy-MM-dd");
        }
        public string now_date { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string reg_id { get; set; }
        public string order_code { get; set; }
        public string dateType { get; set; }
        public int? page { get; set; }
        public int? pagesize { get; set; }
    }

    public class RECALL_INDEX
    {
        public RECALLBBS_SEARCH SearchParam { get; set; }
        public int BbsCount { get; set; }
        public List<SP_TB_RECALL_BBS_SEL_Result> BbsList { get; set; }
        public SP_TB_RECALL_BBS_VIEW_Result Bbsview { get; set; }
        public TB_RECALL_BBS model { get; set; }
        public string mode { get; set; }
    }
}
