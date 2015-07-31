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
        void SaveImageTypeDisplayer(int? idx, string kind, string subKind = null, string url = null, string img = null, int? seq = null);
        Tuple<string, string> SaveProductTypeDisplayer(int? idx, string kind, string subKind = null, string pcode = null, int? seq = null);
        Tuple<string, string> SaveLinkTypeDisplayer(int? idx, string kind, string subKind = null, int? seq = null, string title1 = null, string title2 = null, string url = null);
        void RemoveDisplayer(int? idx);
        void RemoveOnlyImageInDisplayer(int? idx);
        List<SP_ADMIN_DISPLAY_SEL_Result> GetDisplayerList(string kind, string subKind = null, int? seq = null);
    }
}
