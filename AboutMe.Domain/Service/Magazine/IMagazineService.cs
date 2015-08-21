using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.Magazine;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Domain.Service.Magazine
{
    public interface IMagazineService
    {
        List<SP_ADMIN_MAGAZINE_SEL_Result> MagazineAdminList(MAGAZINE_ADMIN_SEARCH param);
        int MagazineAdminCount(MAGAZINE_ADMIN_SEARCH param);
        SP_ADMIN_MAGAZINE_VIEW_Result MagazineAdminView(int IDX);
        int MagazineAdminInsert(SP_ADMIN_MAGAZINE_VIEW_Result param, string ADM_ID);
        void MagazineAdminUpdate(int IDX, SP_ADMIN_MAGAZINE_VIEW_Result param, string ADM_ID);
        SP_MAGAZINE_VIEW_Result MagazineView(int IDX);
        List<SP_MAGAZINE_LIST_Result> MagazineList();
    }
}
