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
        public SP_TB_RECALL_BBS_VIEW_Result RecallView(int IDX, string M_ID, string ORDER_CODE)
        {
            SP_TB_RECALL_BBS_VIEW_Result result;

            using (RecallbbsEntities EfContext = new RecallbbsEntities())
            {
                result = EfContext.SP_TB_RECALL_BBS_VIEW(IDX, M_ID, ORDER_CODE).FirstOrDefault();
            }
            return result;
        }
        #endregion

        #region Insert
        public void RecallBbsInsert(TB_RECALL_BBS itemRecall)
        {
            using (RecallbbsEntities EfContext = new RecallbbsEntities())
            {
                EfContext.SP_TB_RECALL_BBS_INS(itemRecall.ORDER_CODE,itemRecall.ORDER_DETAIL_IDX,itemRecall.GUBUN,itemRecall.TITLE,itemRecall.CONTENTS,itemRecall.REG_ID);
            }
        }
        #endregion

        #region Update
        public void RecallBbsUpdate(TB_RECALL_BBS itemRecall)
        {
            using (RecallbbsEntities EfContext = new RecallbbsEntities())
            {
                EfContext.SP_TB_RECALL_BBS_UPD(itemRecall.IDX, itemRecall.GUBUN, itemRecall.TITLE, itemRecall.CONTENTS, itemRecall.REG_ID, itemRecall.ORDER_CODE);
            } 
        }
        #endregion


        #region Admin List
        public List<SP_ADMIN_RECALL_BBS_SEL_Result> RecallAdminList(RECALL_ADMIN_SEARCH param)
        {
            if (!param.Page.HasValue || param.Page == 0)
                param.Page = 1;
            if (!param.PageSize.HasValue || param.PageSize == 0)
                param.PageSize = 10;

            List<SP_ADMIN_RECALL_BBS_SEL_Result> lst = new List<SP_ADMIN_RECALL_BBS_SEL_Result>();
            using (RecallbbsEntities EfContext = new RecallbbsEntities())
            {
                lst = EfContext.SP_ADMIN_RECALL_BBS_SEL(param.SearchCol, param.SearchKeyword, param.StatusYn, "IDX", "DESC", param.Page, param.PageSize).ToList();
            }

            return lst;

        }
        #endregion

        #region Admin Count
        public int RecallAdminCount(RECALL_ADMIN_SEARCH param)
        {
            SP_ADMIN_RECALL_BBS_CNT_Result result = new SP_ADMIN_RECALL_BBS_CNT_Result();
            using (RecallbbsEntities EfContext = new RecallbbsEntities())
            {
                result = EfContext.SP_ADMIN_RECALL_BBS_CNT(param.SearchCol, param.SearchKeyword, param.StatusYn).FirstOrDefault();
            }
            return result.COUNT;
        }
        #endregion

        #region Admin Update
        public void RecallAdminUpdate(int Idx, string ADM_ID, string CONFIRM_CONTENTS)
        {
            using (RecallbbsEntities EfContext = new RecallbbsEntities())
            {
                EfContext.SP_ADMIN_RECALL_ANSWER_UPD(Idx, ADM_ID, CONFIRM_CONTENTS);
            }
        }
        #endregion

        #region View
        public SP_ADMIN_RECALL_BBS_VIEW_Result QnaAdminView(int Idx)
        {
            SP_ADMIN_RECALL_BBS_VIEW_Result view = new SP_ADMIN_RECALL_BBS_VIEW_Result();
            using (RecallbbsEntities EfContext = new RecallbbsEntities())
            {
                view = EfContext.SP_ADMIN_RECALL_BBS_VIEW(Idx).FirstOrDefault();
            }
            return view;
        }
        #endregion
    }
}

        