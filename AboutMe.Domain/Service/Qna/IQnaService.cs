using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.Qna;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Domain.Service.Qna
{
    public interface IQnaService
    {
        //User
        List<SP_TB_QNA_SEL_Result> QnaList(QNA_SEARCH param);
        int QnaCount(QNA_SEARCH param);
        void QnaDelete(int IDX, string M_ID);
        SP_TB_QNA_VIEW_Result QnaView(int IDX, string M_ID);
        int QnaInsert(TB_QNA itemQna);
        void QnaUpdate(TB_QNA itemQna);

        //Admin
        List<SP_ADMIN_QNA_SEL_Result> QnaAdminList(QNA_ADMIN_SEARCH param);
        int QnaAdminCount(QNA_ADMIN_SEARCH param);
        void QnaAdminUpdate(int Idx, string Answer);
        SP_ADMIN_QNA_VIEW_Result QnaAdminView(int Idx);

        List<SP_ADMIN_QNA_MEMBER_SEL_Result> QnaAdminMemberList(string M_ID, int Page, int PageSize);
        int QnaAdminMemberCount(string M_ID);
    }
}
