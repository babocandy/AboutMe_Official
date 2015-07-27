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
                lst = EfContext.SP_TB_RECALL_BBS_SEL(param.start_date, param.end_date, param.reg_id, param.search_col, param.search_keyword, param.sort_col, param.sort_dir, param.page, param.pagesize).ToList();
            }

            return lst;
        }
        #endregion
        
        #region Count
        public int RecallCount(string start_date, string end_date, string reg_id, string search_col, string search_keyword) 
        {
            SP_TB_RECALL_BBS_CNT_Result result;
            using (RecallbbsEntities EfContext = new RecallbbsEntities())
            {
                result =  EfContext.SP_TB_RECALL_BBS_CNT(start_date, end_date, reg_id, search_col, search_keyword).FirstOrDefault();
            }
            return result.COUNT;
        }
        #endregion

        /*
        #region View Faq
        public SP_TB_FAQ_VIEW_Result FaqView(int IDX) {
            SP_TB_FAQ_VIEW_Result result;
            using (FaqEntities EfContext = new FaqEntities())
            {
                result = EfContext.SP_TB_FAQ_VIEW(IDX).FirstOrDefault();
            }
            return result;
        }
        #endregion

        #region Insert
        public int FaqInsert(TB_FAQ newItem) {
            ObjectParameter new_idx = new ObjectParameter("NEW_IDX", typeof(int));
    
            using (FaqEntities EfContext = new FaqEntities())
            {
                EfContext.SP_TB_FAQ_INS(newItem.CATEGORY, newItem.TITLE, newItem.QUESTION, newItem.ANSWER,newItem.DISPLAY_YN, newItem.M_ID, new_idx);
            }
            return Convert.ToInt16(new_idx.Value);
        }
        #endregion
        
        #region Update
        public void FaqUpdate(TB_FAQ faqItem) {
            ObjectParameter ErrCode = new ObjectParameter("ERR_CODE", typeof(string));
            using (FaqEntities EfContext = new FaqEntities())
            {
             
                EfContext.SP_TB_FAQ_UPD(faqItem.IDX, faqItem.CATEGORY, faqItem.TITLE, faqItem.QUESTION, faqItem.ANSWER, faqItem.DISPLAY_YN, faqItem.M_ID, ErrCode);
            }
        }
        #endregion
        
        #region Delete
        public void FaqDelete(int IDX)
        {
            ObjectParameter ErrCode = new ObjectParameter("ERR_CODE", typeof(string));
            using (FaqEntities EfContext = new FaqEntities())
            {
                EfContext.SP_TB_FAQ_DEL(IDX,ErrCode);
            }
        }
        #endregion
         */ 
    }
}

        