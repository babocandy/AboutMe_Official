using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.Event;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Domain.Service.Event
{
    public class EventService : IEventService
    {
        #region Admin List
        public List<SP_ADMIN_EVENT_SEL_Result> EventAdminList(EVENT_ADMIN_SEARCH param)
        {
            if (!param.Page.HasValue || param.Page == 0)
                param.Page = 1;
            if (!param.PageSize.HasValue || param.PageSize == 0)
                param.PageSize = 10;

            List<SP_ADMIN_EVENT_SEL_Result> lst = new List<SP_ADMIN_EVENT_SEL_Result>();
            using (EventEntities EfContext = new EventEntities())
            {
                lst = EfContext.SP_ADMIN_EVENT_SEL(param.FromDate, param.ToDate, param.IngGbn, param.UseYn, param.SearchCol, param.SearchKeyword, param.Page, param.PageSize).ToList();
            }

            return lst;

        }
        #endregion

        #region Admin Count
        public int EventAdminCount(EVENT_ADMIN_SEARCH param)
        {
            SP_ADMIN_EVENT_CNT_Result result = new SP_ADMIN_EVENT_CNT_Result();
            using (EventEntities EfContext = new EventEntities())
            {
                result = EfContext.SP_ADMIN_EVENT_CNT(param.FromDate, param.ToDate, param.IngGbn, param.UseYn, param.SearchCol, param.SearchKeyword).FirstOrDefault();
            }
            return result.COUNT;
        }
        #endregion

        #region Admin View
        public SP_ADMIN_EVENT_VIEW_Result EventAdminView(int IDX)
        {
            SP_ADMIN_EVENT_VIEW_Result result = new SP_ADMIN_EVENT_VIEW_Result();
            using (EventEntities EfContext = new EventEntities())
            {
                result = EfContext.SP_ADMIN_EVENT_VIEW(IDX).FirstOrDefault();
            }
            return result;
        }
        #endregion

        #region Admin Insert
        public int EventAdminInsert(SP_ADMIN_EVENT_VIEW_Result param)
        {
            ObjectParameter new_idx = new ObjectParameter("NEW_IDX", typeof(int));
            using (EventEntities EfContext = new EventEntities())
            {
                EfContext.SP_ADMIN_EVENT_INS(param.EVENT_NAME, param.FROM_DATE, param.FROM_TIME, param.TO_DATE, param.TO_TIME, param.EVENT_GBN, param.WEB_CONTENTS, param.MOBILE_FILE, param.WEB_BANNER, param.MOBILE_BANNER, param.WEB_URL, param.MOBILE_URL, param.USE_YN, param.ADM_ID, new_idx);
            }
            return Convert.ToInt16(new_idx.Value);
        }
        #endregion

        #region Admin Update
        public void EventAdminUpdate(int IDX, SP_ADMIN_EVENT_VIEW_Result param)
        {
            using (EventEntities EfContext = new EventEntities())
            {
                EfContext.SP_ADMIN_EVENT_UPD(IDX, param.EVENT_NAME, param.FROM_DATE, param.FROM_TIME, param.TO_DATE, param.TO_TIME, param.EVENT_GBN, param.WEB_CONTENTS, param.MOBILE_FILE, param.WEB_BANNER, param.MOBILE_BANNER, param.WEB_URL, param.MOBILE_URL, param.USE_YN, param.ADM_ID);
            }
        }
        #endregion

    }
}
