using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.Shopinfo;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Domain.Service.Shopinfo
{
    public interface IShopinfoService
    {
        List<SP_ADMIN_SHOPINFO_SEL_Result> ShopinfoAdminList(SHOPINFO_ADMIN_SEARCH param);
        int ShopinfoAdminCount(SHOPINFO_ADMIN_SEARCH param);
        SP_ADMIN_SHOPINFO_VIEW_Result ShopinfoAdminView(int IDX);
        int ShopinfoAdminInsert(SP_ADMIN_SHOPINFO_VIEW_Result param, string ADM_ID);
        void ShopinfoAdminUpdate(int IDX, SP_ADMIN_SHOPINFO_VIEW_Result param, string ADM_ID);
        List<SP_SHOPINFO_LIST_Result> ShopinfoList();
    }
}
