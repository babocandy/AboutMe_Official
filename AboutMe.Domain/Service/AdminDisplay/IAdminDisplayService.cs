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
        void SaveWebMainBanner(int? idx, string url, string img);
        void RemoveWebMainBanner(int? idx);
        List<SP_ADMIN_DISPLAY_SEL_Result> GetWebMainBanner();
    }
}
