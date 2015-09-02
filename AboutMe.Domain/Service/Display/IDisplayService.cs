using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Data.Entity.Core.Objects;

using AboutMe.Domain.Entity.Display;

namespace AboutMe.Domain.Service.Display
{
    public interface IDisplayService
    {
        List<SP_DISPLAY_SEL_Result> GetListDisplay(string kind, string subKind = null, int? seq = null);
        List<SP_POPUP_WEB_SEL_Result> GetListPopupWeb();
        SP_POPUP_DETAIL_Result GePopupDetail(int? idx);
        List<SP_POPUP_MOBILE_SEL_Result> GetListPopupMobile();
    }
}
