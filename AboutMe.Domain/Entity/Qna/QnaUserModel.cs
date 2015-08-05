using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AboutMe.Domain.Entity.Common;

namespace AboutMe.Domain.Entity.Qna
{
    public class QNA_SEARCH
    {
        public QNA_SEARCH()
        { 
            this.reg_id = ""; 
        }
        public string reg_id { get; set; }
        public int? page { get; set; }
        public int? pagesize { get; set; }
    }

    public class QNA_INDEX
    {
        public QNA_SEARCH SearchParam { get; set; }
        public int QnaCount { get; set; }
        public List<SP_TB_QNA_SEL_Result> QnaList { get; set; }
        public SP_TB_QNA_VIEW_Result QnaView { get; set; }
        public string mode { get; set; }
    }

    public class QNA_ADMIN_SEARCH
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

    public class QNA_ADMIN_EDIT
    {
        public QNA_ADMIN_SEARCH SearchParam { get; set; }
        public SP_ADMIN_QNA_VIEW_Result QnaInfo { get; set; }
    }

}
