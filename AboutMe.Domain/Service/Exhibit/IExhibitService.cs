using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.Exhibit;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Domain.Service.Exhibit
{
    public interface IExhibitService
    {
        //Admin
        List<SP_ADMIN_EXHIBIT_SEL_Result> ExhibitAdminList(EXHIBIT_ADMIN_SEARCH param);
        int ExhibitAdminCount(EXHIBIT_ADMIN_SEARCH param);
        SP_ADMIN_EXHIBIT_VIEW_Result ExhibitAdminView(int IDX);
        int ExhibitAdminInsert(SP_ADMIN_EXHIBIT_VIEW_Result param);
        void ExhibitAdminUpdate(int IDX, SP_ADMIN_EXHIBIT_VIEW_Result param);
        List<SP_ADMIN_EXHIBIT_TAB_SEL_Result> ExhibitAdminTabList(int EXHIBIT_IDX);
        int ExhibitAdminTabInsert(int EXHIBIT_IDX, string TAB_NAME, int DISPLAY_ORDER, string ADM_ID);
        void ExhibitAdminTabUpdate(int TAB_IDX, string TAB_NAME, int DISPLAY_ORDER, string ADM_ID);
        void ExhibitAdminTabOrderUpdate(int TAB_IDX, int DISPLAY_ORDER, string ADM_ID);
        void ExhibitAdminTabDelete(int TAB_IDX);
        List<SP_ADMIN_EXHIBIT_TAB_PRODUCT_LIST_Result> ExhibitAdminTabProductList(int TAB_IDX);
        List<SP_ADMIN_EXHIBIT_TAB_PRODUCT_SEARCH_Result> ExhibitAdminTabProductSearch(string SEARCH_COL, string SEARCH_KEYWORD);
        int ExhibitAdminTabProductInsert(int TAB_IDX, string P_CODE, int DISPLAY_ORDER, string ADM_ID);
        void ExhibitAdminTabProductUpdate(int IDX, int DISPLAY_ORDER, string ADM_ID);
        void ExhibitAdminTabProductDelete(int IDX);
        void ExhibitAdminTabProductAllDelete(int TAB_IDX);
    }
}
