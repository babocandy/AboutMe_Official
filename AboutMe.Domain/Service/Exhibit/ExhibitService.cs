using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.Exhibit;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Domain.Service.Exhibit
{
    public class ExhibitService : IExhibitService
    {
        #region Admin List
        public List<SP_ADMIN_EXHIBIT_SEL_Result> ExhibitAdminList(EXHIBIT_ADMIN_SEARCH param)
        {
            if (!param.Page.HasValue || param.Page == 0)
                param.Page = 1;
            if (!param.PageSize.HasValue || param.PageSize == 0)
                param.PageSize = 10;

            List<SP_ADMIN_EXHIBIT_SEL_Result> lst = new List<SP_ADMIN_EXHIBIT_SEL_Result>();
            using (ExhibitEntities EfContext = new ExhibitEntities())
            {
                lst = EfContext.SP_ADMIN_EXHIBIT_SEL(param.FromDate, param.ToDate, param.IngGbn, param.UseYn, param.SearchCol, param.SearchKeyword, param.Page, param.PageSize).ToList();
            }

            return lst;

        }
        #endregion

        #region Admin Count
        public int ExhibitAdminCount(EXHIBIT_ADMIN_SEARCH param)
        {
            SP_ADMIN_EXHIBIT_CNT_Result result = new SP_ADMIN_EXHIBIT_CNT_Result();
            using (ExhibitEntities EfContext = new ExhibitEntities())
            {
                result = EfContext.SP_ADMIN_EXHIBIT_CNT(param.FromDate, param.ToDate, param.IngGbn, param.UseYn, param.SearchCol, param.SearchKeyword).FirstOrDefault();
            }
            return result.COUNT;
        }
        #endregion

        #region Admin View
        public SP_ADMIN_EXHIBIT_VIEW_Result ExhibitAdminView(int IDX)
        {
            SP_ADMIN_EXHIBIT_VIEW_Result result = new SP_ADMIN_EXHIBIT_VIEW_Result();
            using (ExhibitEntities EfContext = new ExhibitEntities())
            {
                result = EfContext.SP_ADMIN_EXHIBIT_VIEW(IDX).FirstOrDefault();
            }
            return result;
        }
        #endregion

        #region Admin Insert
        public int ExhibitAdminInsert(SP_ADMIN_EXHIBIT_VIEW_Result param)
        {
            ObjectParameter new_idx = new ObjectParameter("NEW_IDX", typeof(int));
            using (ExhibitEntities EfContext = new ExhibitEntities())
            {
                EfContext.SP_ADMIN_EXHIBIT_INS(param.EXHIBIT_NAME, param.FROM_DATE, param.FROM_TIME, param.TO_DATE, param.TO_TIME, param.EXHIBIT_GBN, param.WEB_CONTENTS, param.MOBILE_FILE, param.WEB_BANNER, param.MOBILE_BANNER, param.USE_YN, param.ADM_ID, new_idx);
            }
            return Convert.ToInt16(new_idx.Value);
        }
        #endregion

        #region Admin Update
        public void ExhibitAdminUpdate(int IDX, SP_ADMIN_EXHIBIT_VIEW_Result param)
        {
            using (ExhibitEntities EfContext = new ExhibitEntities())
            {
                EfContext.SP_ADMIN_EXHIBIT_UPD(IDX, param.EXHIBIT_NAME, param.FROM_DATE, param.FROM_TIME, param.TO_DATE, param.TO_TIME, param.EXHIBIT_GBN, param.WEB_CONTENTS, param.MOBILE_FILE, param.WEB_BANNER, param.MOBILE_BANNER, param.USE_YN, param.ADM_ID);
            }
        }
        #endregion

        #region Admin Tab List
        public List<SP_ADMIN_EXHIBIT_TAB_SEL_Result> ExhibitAdminTabList(int EXHIBIT_IDX)
        {
            List<SP_ADMIN_EXHIBIT_TAB_SEL_Result> lst = new List<SP_ADMIN_EXHIBIT_TAB_SEL_Result>();
            using (ExhibitEntities EfContext = new ExhibitEntities())
            {
                lst = EfContext.SP_ADMIN_EXHIBIT_TAB_SEL(EXHIBIT_IDX).ToList();
            }
            return lst;
        }
        #endregion

        #region Admin Tab Insert
        public int ExhibitAdminTabInsert(int EXHIBIT_IDX, string TAB_NAME, int DISPLAY_ORDER, string ADM_ID)
        {
            ObjectParameter new_idx = new ObjectParameter("NEW_IDX", typeof(int));
            using (ExhibitEntities EfContext = new ExhibitEntities())
            {
                EfContext.SP_ADMIN_EXHIBIT_TAB_INS(EXHIBIT_IDX,TAB_NAME, DISPLAY_ORDER,ADM_ID, new_idx);
            }
            return Convert.ToInt16(new_idx.Value);
        }
        #endregion

        #region Admin Tab Update
        public void ExhibitAdminTabUpdate(int TAB_IDX, string TAB_NAME, int DISPLAY_ORDER, string ADM_ID)
        {
            using (ExhibitEntities EfContext = new ExhibitEntities())
            {
                EfContext.SP_ADMIN_EXHIBIT_TAB_UPD(TAB_IDX, TAB_NAME, DISPLAY_ORDER, ADM_ID);
            }
        }
        #endregion

        #region Admin Tab Order Update
        public void ExhibitAdminTabOrderUpdate(int TAB_IDX, int DISPLAY_ORDER, string ADM_ID)
        {
            using (ExhibitEntities EfContext = new ExhibitEntities())
            {
                EfContext.SP_ADMIN_EXHIBIT_TAB_ORDER_UPD(TAB_IDX, DISPLAY_ORDER, ADM_ID);
            }
        }
        #endregion

        #region Admin Tab Delete
        public void ExhibitAdminTabDelete(int TAB_IDX)
        {
            using (ExhibitEntities EfContext = new ExhibitEntities())
            {
                EfContext.SP_ADMIN_EXHIBIT_TAB_DEL(TAB_IDX);
            }
        }
        #endregion

        #region Admin Tab > Product List
        public List<SP_ADMIN_EXHIBIT_TAB_PRODUCT_LIST_Result> ExhibitAdminTabProductList(int TAB_IDX)
        {
            List<SP_ADMIN_EXHIBIT_TAB_PRODUCT_LIST_Result> lst = new List<SP_ADMIN_EXHIBIT_TAB_PRODUCT_LIST_Result>();
            using (ExhibitEntities EfContext = new ExhibitEntities())
            {
                lst = EfContext.SP_ADMIN_EXHIBIT_TAB_PRODUCT_LIST(TAB_IDX).ToList();
            }
            return lst;
        }
        #endregion

        #region Admin Tab > Product Search List
        public List<SP_ADMIN_EXHIBIT_TAB_PRODUCT_SEARCH_Result> ExhibitAdminTabProductSearch(string SEARCH_COL, string SEARCH_KEYWORD)
        {
            List<SP_ADMIN_EXHIBIT_TAB_PRODUCT_SEARCH_Result> lst = new List<SP_ADMIN_EXHIBIT_TAB_PRODUCT_SEARCH_Result>();
            using (ExhibitEntities EfContext = new ExhibitEntities())
            {
                lst = EfContext.SP_ADMIN_EXHIBIT_TAB_PRODUCT_SEARCH(SEARCH_COL, SEARCH_KEYWORD).ToList();
            }
            return lst;
        }
        #endregion

        #region Admin Tab > Product Insert
        public int ExhibitAdminTabProductInsert(int TAB_IDX, string P_CODE, int DISPLAY_ORDER, string ADM_ID)
        {
            ObjectParameter new_idx = new ObjectParameter("NEW_IDX", typeof(int));
            using (ExhibitEntities EfContext = new ExhibitEntities())
            {
                EfContext.SP_ADMIN_EXHIBIT_TAB_PRODUCT_INS(TAB_IDX, P_CODE, DISPLAY_ORDER, ADM_ID, new_idx);
            }
            return Convert.ToInt16(new_idx.Value);
        }
        #endregion

        #region Admin Tab > Product Update
        public void ExhibitAdminTabProductUpdate(int IDX, int DISPLAY_ORDER, string ADM_ID)
        {
            using (ExhibitEntities EfContext = new ExhibitEntities())
            {
                EfContext.SP_ADMIN_EXHIBIT_TAB_PRODUCT_UPD(IDX, DISPLAY_ORDER, ADM_ID);
            }
        }
        #endregion

        #region Admin Tab > Product Delete
        public void ExhibitAdminTabProductDelete(int IDX)
        {
            using (ExhibitEntities EfContext = new ExhibitEntities())
            {
                EfContext.SP_ADMIN_EXHIBIT_TAB_PRODUCT_DEL(IDX);
            }
        }
        #endregion

        #region Admin Tab > Product 전체 Delete
        public void ExhibitAdminTabProductAllDelete(int TAB_IDX)
        {
            using (ExhibitEntities EfContext = new ExhibitEntities())
            {
                EfContext.SP_ADMIN_EXHIBIT_TAB_PRODUCT_ALLDEL(TAB_IDX);
            }
        }
        #endregion

        #region User 기획전 View
        public SP_EXHIBIT_VIEW_Result ExhibitView(int IDX)
        {
            SP_EXHIBIT_VIEW_Result result = new SP_EXHIBIT_VIEW_Result();
            using (ExhibitEntities EfContext = new ExhibitEntities())
            {
                result = EfContext.SP_EXHIBIT_VIEW(IDX).FirstOrDefault();
            }
            return result;
        }
        #endregion

        #region User 기획전 Product List
        public List<SP_EXHIBIT_TAB_PRODUCT_LIST_Result> ExhibitTabProductList(int TAB_IDX)
        {
            List<SP_EXHIBIT_TAB_PRODUCT_LIST_Result> lst = new List<SP_EXHIBIT_TAB_PRODUCT_LIST_Result>();
            using (ExhibitEntities EfContext = new ExhibitEntities())
            {
                lst = EfContext.SP_EXHIBIT_TAB_PRODUCT_LIST(TAB_IDX).ToList();
            }
            return lst;
        }
        #endregion


        #region User 상품상세페이지의 해당상품관련 기획전모음
        public List<SP_EXHIBIT_PCODE_LINK_ALL_Result> ExhibitProductLinkAll(string P_CODE)
        {
            List<SP_EXHIBIT_PCODE_LINK_ALL_Result> lst = new List<SP_EXHIBIT_PCODE_LINK_ALL_Result>();
            using (ExhibitEntities EfContext = new ExhibitEntities())
            {
                lst = EfContext.SP_EXHIBIT_PCODE_LINK_ALL(P_CODE).ToList();
            }
            return lst;
        }
        #endregion
    }
}
