using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.Qna;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Domain.Service.Qna
{
    public class QnaService : IQnaService
    {
        #region List 
        public List<SP_TB_QNA_SEL_Result> QnaList(QNA_SEARCH param)
        {
            if (!param.page.HasValue || param.page == 0)
                param.page = 1;
            if (!param.pagesize.HasValue || param.pagesize == 0)
                param.pagesize = 10;

            List<SP_TB_QNA_SEL_Result> lst = new List<SP_TB_QNA_SEL_Result>();
            using (QnaEntities EfContext = new QnaEntities())
            {
                lst = EfContext.SP_TB_QNA_SEL(param.reg_id,param.page,param.pagesize).ToList();
            }

            return lst;

        }
        #endregion

        #region Count
        public int QnaCount(QNA_SEARCH param)
        {
            SP_TB_QNA_CNT_Result result;
            using (QnaEntities EfContext = new QnaEntities())
            {
                result = EfContext.SP_TB_QNA_CNT(param.reg_id).FirstOrDefault();
            }
            return result.COUNT;
        }
        #endregion

        #region Delete
        public void QnaDelete(int IDX)
        {
            ObjectParameter ErrCode = new ObjectParameter("ERR_CODE", typeof(string));
            using (QnaEntities EfContext = new QnaEntities())
            {
                EfContext.SP_TB_QNA_DEL(IDX, ErrCode);
            }
        }
        #endregion

        #region View
        public SP_TB_QNA_VIEW_Result QnaView(int IDX)
        {
            SP_TB_QNA_VIEW_Result view;
            using (QnaEntities EfContext = new QnaEntities())
            {
                view = EfContext.SP_TB_QNA_VIEW(IDX).FirstOrDefault();
            }
            return view;
        }
        #endregion

        #region Insert
        public int QnaInsert(TB_QNA itemQna)
        {
            ObjectParameter new_idx = new ObjectParameter("NEW_IDX", typeof(int));

            using (QnaEntities EfContext = new QnaEntities())
            {
                EfContext.SP_TB_QNA_INS(itemQna.CATEGORY, itemQna.TITLE, itemQna.QUESTION, itemQna.STATUS, itemQna.M_ID, new_idx);
            }
            return Convert.ToInt16(new_idx.Value);
        }
        #endregion

        #region Update
        public void QnaUpdate(TB_QNA itemQna)
        {
            ObjectParameter ErrCode = new ObjectParameter("ERR_CODE", typeof(string));
            using (QnaEntities EfContext = new QnaEntities())
            {
                EfContext.SP_TB_QNA_UPD(itemQna.IDX, itemQna.CATEGORY, itemQna.TITLE, itemQna.QUESTION, itemQna.M_ID, ErrCode);
            }
        }
        #endregion
    }
}
