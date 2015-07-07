using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AboutMe.Domain.Entity.Notice;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Domain.Service.Notice
{
    public class NoticeService : INoticeService
    {
        #region List Notice
        public List<SP_TB_NOTICE_SEL_Result> NoticeList(string search_col, string search_keyword, string display_yn, string sort_col, string sort_dir, Nullable<int> page, Nullable<int> pagesize)
        {
            if (!page.HasValue || page == 0)
                page = 1;
            if (!pagesize.HasValue || pagesize == 0)
                pagesize = 10;

            List<SP_TB_NOTICE_SEL_Result> lst = new List<SP_TB_NOTICE_SEL_Result>();
            using (NoticeEntities EfContext = new NoticeEntities())
            {
                lst = EfContext.SP_TB_NOTICE_SEL(search_col, search_keyword,  display_yn, sort_col, sort_dir, page, pagesize).ToList();
            }

            return lst;
        }
        #endregion

        #region Count
        public int NoticeListCount(string search_col, string search_keyword, string display_yn) {
            SP_TB_NOTICE_CNT_Result result;
            using (NoticeEntities EfContext = new NoticeEntities())
            {
                result =  EfContext.SP_TB_NOTICE_CNT(search_col, search_keyword, display_yn).FirstOrDefault();
            }
            return result.COUNT;
        }
        #endregion

        #region View Notice
        public SP_TB_NOTICE_VIEW_Result NoticeView(int IDX)
        {
            SP_TB_NOTICE_VIEW_Result result;
            using (NoticeEntities EfContext = new NoticeEntities())
            {
                result = EfContext.SP_TB_NOTICE_VIEW(IDX).FirstOrDefault();
            }
            return result;
        }
        #endregion

        #region Insert
        public int NoticeInsert(TB_NOTICE newItem) {
            ObjectParameter new_idx = new ObjectParameter("NEW_IDX", typeof(int));

            using (NoticeEntities EfContext = new NoticeEntities())
            {
                EfContext.SP_TB_NOTICE_INS(newItem.TITLE, newItem.CONTENTS, newItem.DISPLAY_YN, newItem.M_ID, new_idx);
            }
            return Convert.ToInt16(new_idx.Value);
        }
        #endregion
        
        #region Update
        public void NoticeUpdate(TB_NOTICE Items)
        {
            ObjectParameter ErrCode = new ObjectParameter("ERR_CODE", typeof(string));
            using (NoticeEntities EfContext = new NoticeEntities())
            {

                EfContext.SP_TB_NOTICE_UPD(Items.IDX, Items.TITLE, Items.CONTENTS, Items.DISPLAY_YN, Items.M_ID, ErrCode);
            }
        }
        #endregion
        
        #region Delete
        public void NoticeDelete(int IDX)
        {
            ObjectParameter ErrCode = new ObjectParameter("ERR_CODE", typeof(string));
            using (NoticeEntities EfContext = new NoticeEntities())
            {
                EfContext.SP_TB_NOTICE_DEL(IDX,ErrCode);
            }
        }
        #endregion
    }
}
