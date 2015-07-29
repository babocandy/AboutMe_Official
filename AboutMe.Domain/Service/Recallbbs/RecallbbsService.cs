using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AboutMe.Domain.Entity.Recallbbs;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Domain.Service.Recallbbs
{
    public class RecallbbsService : IRecallbbsService
    {
        #region List 
        public List<SP_TB_RECALL_BBS_SEL_Result> RecallList(RECALLBBS_SEARCH param)
        {
            if (!param.page.HasValue || param.page == 0)
                param.page = 1;
            if (!param.pagesize.HasValue || param.pagesize == 0)
                param.pagesize = 10;

            List<SP_TB_RECALL_BBS_SEL_Result> lst = new List<SP_TB_RECALL_BBS_SEL_Result>();
            using (RecallbbsEntities EfContext = new RecallbbsEntities())
            {
                lst = EfContext.SP_TB_RECALL_BBS_SEL(param.start_date, param.end_date, param.reg_id, param.order_code, param.page, param.pagesize).ToList();
            }

            return lst;
        }
        #endregion
        
        #region Count
        public int RecallCount(RECALLBBS_SEARCH param) 
        {
            SP_TB_RECALL_BBS_CNT_Result result;
            using (RecallbbsEntities EfContext = new RecallbbsEntities())
            {
                result = EfContext.SP_TB_RECALL_BBS_CNT(param.start_date, param.end_date, param.reg_id, param.order_code).FirstOrDefault();
            }
            return result.COUNT;
        }
        #endregion

        #region View
        public SP_TB_RECALL_BBS_VIEW_Result RecallView(int IDX)
        {
            SP_TB_RECALL_BBS_VIEW_Result result;

            using (RecallbbsEntities EfContext = new RecallbbsEntities())
            {
                result = EfContext.SP_TB_RECALL_BBS_VIEW(IDX).FirstOrDefault();
            }
            return result;
        }
        #endregion

        #region Insert
        public int RecallBbsInsert(TB_RECALL_BBS itemRecall)
        {
            ObjectParameter new_idx = new ObjectParameter("NEW_IDX", typeof(int));

            using (RecallbbsEntities EfContext = new RecallbbsEntities())
            {
                EfContext.SP_TB_RECALL_BBS_INS(itemRecall.ORDER_CODE,itemRecall.ORDER_DETAIL_IDX,itemRecall.GUBUN,itemRecall.TITLE,itemRecall.CONTENTS,itemRecall.STATUS,itemRecall.REG_ID,new_idx);
            }
            return Convert.ToInt16(new_idx.Value);
        }
        #endregion

         #region Update
        public void RecallBbsUpdate(TB_RECALL_BBS itemRecall)
        {
            ObjectParameter ErrCode = new ObjectParameter("ERR_CODE", typeof(string));
            using (RecallbbsEntities EfContext = new RecallbbsEntities())
            {
                EfContext.SP_TB_RECALL_BBS_UPD(itemRecall.IDX, itemRecall.GUBUN, itemRecall.TITLE, itemRecall.CONTENTS, itemRecall.CONFIRM_CONTENTS, itemRecall.STATUS, itemRecall.ADM_ID, ErrCode);
            } 
        }
        #endregion

 
    }
}

        