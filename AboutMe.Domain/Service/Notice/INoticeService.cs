using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.Notice;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Domain.Service.Notice
{
    public interface INoticeService
    {
        //List
        List<SP_TB_NOTICE_SEL_Result> NoticeList(string search_col, string search_keyword, string display_yn, string sort_col, string sort_dir, Nullable<int> page, Nullable<int> pagesize);
        //Count
        int NoticeListCount(string search_col, string search_keyword, string display_yn);
        //View
        SP_TB_NOTICE_VIEW_Result NoticeView(int IDX);
        //Insert
        int NoticeInsert(TB_NOTICE newItem);
        //Update
        void NoticeUpdate(TB_NOTICE newItem);
        //Delete
        void NoticeDelete(int IDX);
    }
}
