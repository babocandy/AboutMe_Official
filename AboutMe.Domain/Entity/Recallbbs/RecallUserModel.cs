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
            this.search_col = "";
            this.search_keyword = "";
            this.sort_col = "IDX";
            this.sort_dir = "DESC";
        }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string reg_id { get; set; }
        public string search_col { get; set; }
        public string search_keyword { get; set; }
        public string sort_col { get; set; }
        public string sort_dir { get; set; }
        public int? page { get; set; }
        public int? pagesize { get; set; }
    }

    public class RECALL_INDEX
    {
        public RECALLBBS_SEARCH SearchParam { get; set; }
        public List<SP_TB_RECALL_BBS_SEL_Result> BbsList { get; set; }
    }
}
