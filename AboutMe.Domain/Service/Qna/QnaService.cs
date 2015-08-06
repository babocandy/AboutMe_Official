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
        public void QnaDelete(int IDX, string M_ID)
        {
            using (QnaEntities EfContext = new QnaEntities())
            {
                EfContext.SP_TB_QNA_DEL(IDX, M_ID);
            }
        }
        #endregion

        #region View
        public SP_TB_QNA_VIEW_Result QnaView(int IDX, string M_ID)
        {
            SP_TB_QNA_VIEW_Result view;
            using (QnaEntities EfContext = new QnaEntities())
            {
                view = EfContext.SP_TB_QNA_VIEW(IDX, M_ID).FirstOrDefault();
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

        #region Admin List
        public List<SP_ADMIN_QNA_SEL_Result> QnaAdminList(QNA_ADMIN_SEARCH param)
        {
            if (!param.Page.HasValue || param.Page == 0)
                param.Page = 1;
            if (!param.PageSize.HasValue || param.PageSize == 0)
                param.PageSize = 10;

            List<SP_ADMIN_QNA_SEL_Result> lst = new List<SP_ADMIN_QNA_SEL_Result>();
            using (QnaEntities EfContext = new QnaEntities())
            {
                lst = EfContext.SP_ADMIN_QNA_SEL(param.SearchCol, param.SearchKeyword, param.StatusYn, "IDX", "DESC", param.Page, param.PageSize).ToList();
            }

            return lst;

        }
        #endregion

        #region Admin Count
        public int QnaAdminCount(QNA_ADMIN_SEARCH param)
        {
            SP_ADMIN_QNA_CNT_Result result = new SP_ADMIN_QNA_CNT_Result();
            using (QnaEntities EfContext = new QnaEntities())
            {
                result = EfContext.SP_ADMIN_QNA_CNT(param.SearchCol, param.SearchKeyword, param.StatusYn).FirstOrDefault();
            }
            return result.COUNT;
        }
        #endregion

        #region Admin Update
        public void QnaAdminUpdate(int Idx, string Answer)
        {
            using (QnaEntities EfContext = new QnaEntities())
            {
                EfContext.SP_ADMIN_QNA_ANSWER_UPD(Idx, Answer);
            }
        }
        #endregion

        #region View
        public SP_ADMIN_QNA_VIEW_Result QnaAdminView(int Idx)
        {
            SP_ADMIN_QNA_VIEW_Result view = new SP_ADMIN_QNA_VIEW_Result();
            using (QnaEntities EfContext = new QnaEntities())
            {
                view = EfContext.SP_ADMIN_QNA_VIEW(Idx).FirstOrDefault();
            }
            return view;
        }
        #endregion
    }
}
