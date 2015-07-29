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

        //Count
        int RecallCount(RECALLBBS_SEARCH param);
        
        //View
        SP_TB_RECALL_BBS_VIEW_Result RecallView(int IDX);

        //insert
        int RecallBbsInsert(TB_RECALL_BBS itemRecall);

        //update
        void RecallBbsUpdate(TB_RECALL_BBS itemRecall);

    }
}
