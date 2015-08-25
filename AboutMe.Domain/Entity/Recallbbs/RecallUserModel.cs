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
            this.dateType = "3";    //기간 검색 OR 전체
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
    }

    public class RECALL_VIEW
    {
        public RECALLBBS_SEARCH SearchParam { get; set; }
        public SP_TB_RECALL_BBS_VIEW_Result Bbsview { get; set; }
        public string Mode { get; set; }

    }


    public class RECALL_ADMIN_SEARCH
    {
        private string searchcol;
        private string searchkeyword;
        private string statusyn;
        private int? page;
        private int? pagesize;

        public string SearchCol
        {
            get
            {
                return this.searchcol ?? "";
            }
            set
            {
                this.searchcol = value;
            }
        }


        public string SearchKeyword
        {
            get
            {
                return this.searchkeyword ?? "";
            }
            set
            {
                this.searchkeyword = value;
            }
        }

        public string StatusYn
        {
            get
            {
                return this.statusyn ?? "A";
            }
            set
            {
                this.statusyn = value;
            }
        }

        public int? Page
        {
            get
            {
                return this.page ?? 1;
            }
            set
            {
                this.page = value;
            }
        }

        public int? PageSize
        {
            get
            {
                return this.pagesize ?? 10;
            }
            set
            {
                this.pagesize = value;
            }
        }
    }

    public class RECALL_ADMIN_EDIT
    {
        public RECALL_ADMIN_SEARCH SearchParam { get; set; }
        public SP_ADMIN_RECALL_BBS_VIEW_Result RecallInfo { get; set; }
    }

}
