using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.Recallbbs;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Domain.Service.Recallbbs
{
    public interface IRecallbbsService
    {
        //List
        List<SP_TB_RECALL_BBS_SEL_Result> RecallList(RECALLBBS_SEARCH param);
        int RecallCount(string start_date, string end_date, string reg_id, string search_col, string search_keyword);
        
    }
}
