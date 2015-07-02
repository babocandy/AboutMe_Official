using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AboutMe.Domain.Entity.Faq;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Domain.Service.Faq
{
    public class FaqService : IFaqService
    {
        #region List Faq
        public List<SP_TB_FAQ_SEL_Result> FaqList(string search_col, string search_keyword, string display_yn, string sort_col, string sort_dir, Nullable<int> page, Nullable<int> pagesize)
        {
            if (!page.HasValue || page == 0)
                page = 1;
            if (!pagesize.HasValue || pagesize == 0)
                pagesize = 10;

            List<SP_TB_FAQ_SEL_Result> lst = new List<SP_TB_FAQ_SEL_Result>();
            using (FaqEntities EfContext = new FaqEntities())
            {
                lst = EfContext.SP_TB_FAQ_SEL(search_col, search_keyword,  display_yn, sort_col, sort_dir, page, pagesize).ToList();
            }

            return lst;
        }
        #endregion

        #region Count
        public int FaqListCount(string search_col, string search_keyword, string display_yn) {
            SP_TB_FAQ_CNT_Result result;
            using (FaqEntities EfContext = new FaqEntities())
            {
                result =  EfContext.SP_TB_FAQ_CNT(search_col, search_keyword, display_yn).FirstOrDefault();
            }
            return result.COUNT;
        }
        #endregion

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
    }
}
