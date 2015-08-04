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
        //List
        List<SP_TB_QNA_SEL_Result> QnaList(QNA_SEARCH param);

        //Count
        int QnaCount(QNA_SEARCH param);
       
        //Delete
        void QnaDelete(int IDX);

        //View
        SP_TB_QNA_VIEW_Result QnaView(int IDX);
         
        //insert
        int QnaInsert(TB_QNA itemQna);

        //update
        void QnaUpdate(TB_QNA itemQna);
    }
}
