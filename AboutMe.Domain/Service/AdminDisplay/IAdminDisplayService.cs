using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using AboutMe.Domain.Entity.AdminDisplay;

namespace AboutMe.Domain.Service.AdminDisplay
{

    public interface IAdminDisplayService
    {
        void SaveImageTypeDisplayer(DisplayerParam param);
        Tuple<string, string> SaveProductTypeDisplayer(DisplayerParam param);
        Tuple<string, string> SaveLinkTypeDisplayer(DisplayerParam param);
        void RemoveDisplayer(int? idx);
        void RemoveOnlyImageInDisplayer(int? idx);
        List<SP_ADMIN_DISPLAY_SEL_Result> GetDisplayerList(DisplayerParam param);

        Tuple<string, string> PopupAdd(PopupParam param);
        Tuple<List<SP_ADMIN_POPUP_SEL_Result>, int> PopupSel(PopupSearchParam param);
        Tuple<string, string> PopupUpdateDisplay(PopupParam param);
        Tuple<string, string> PopupRemove(PopupParam param);
        Tuple<SP_ADMIN_POPUP_INFO_Result, string, string> PopupInfo(int? idx);
        Tuple<string, string> PopupUpdate(PopupParam param);
    }
}
