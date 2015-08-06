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
    }
}
