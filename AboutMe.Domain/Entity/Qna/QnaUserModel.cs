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
}
