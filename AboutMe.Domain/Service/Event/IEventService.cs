using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.Event;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Domain.Service.Event
{
    public interface IEventService
    {
        //Admin
        List<SP_ADMIN_EVENT_SEL_Result> EventAdminList(EVENT_ADMIN_SEARCH param);
        int EventAdminCount(EVENT_ADMIN_SEARCH param);
        SP_ADMIN_EVENT_VIEW_Result EventAdminView(int IDX);
        int EventAdminInsert(SP_ADMIN_EVENT_VIEW_Result param);
        void EventAdminUpdate(int IDX, SP_ADMIN_EVENT_VIEW_Result param);
        SP_ADMIN_EVENT_MAIN_VIEW_Result EventAdminMainView();
        List<SP_ADMIN_EVENT_MAIN_LIST_Result> EventAdminMainIngList();
        void EventAdminMainListOrderUpdate(string GBN, int IDX, int ORDER);
        void EventAdminMainImageUpdate(string BANNER_GBN, string IMG, string URL, string ADM_ID);
        void EventAdminMainImageDelete(string BANNER_GBN, string ADM_ID);
    }
}
