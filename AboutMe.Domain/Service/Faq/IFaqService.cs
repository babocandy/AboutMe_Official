using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.Faq;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Domain.Service.Faq
{
    public interface IFaqService
    {
        //List
        List<SP_TB_FAQ_SEL_Result> FaqList(string search_col, string search_keyword, string display_yn, string sort_col, string sort_dir, Nullable<int> page, Nullable<int> pagesize);
        //Count
        int FaqListCount(string search_col, string search_keyword, string display_yn);
        //View
        SP_TB_FAQ_VIEW_Result FaqView(int IDX);
        //Insert
        int FaqInsert(TB_FAQ newItem);
        //Update
        void FaqUpdate(TB_FAQ newItem);
        //Delete
        void FaqDelete(int IDX);
    }
}
