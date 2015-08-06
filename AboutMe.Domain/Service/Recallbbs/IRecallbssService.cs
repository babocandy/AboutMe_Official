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
        SP_TB_RECALL_BBS_VIEW_Result RecallView(int IDX, string M_ID, string ORDER_CODE);
        //insert
        void RecallBbsInsert(TB_RECALL_BBS itemRecall);
        //update
        void RecallBbsUpdate(TB_RECALL_BBS itemRecall);

        List<SP_ADMIN_RECALL_BBS_SEL_Result> RecallAdminList(RECALL_ADMIN_SEARCH param);
        int RecallAdminCount(RECALL_ADMIN_SEARCH param);
        void RecallAdminUpdate(int Idx, string ADM_ID, string CONFIRM_CONTENTS);
        SP_ADMIN_RECALL_BBS_VIEW_Result QnaAdminView(int Idx);
    }
}
