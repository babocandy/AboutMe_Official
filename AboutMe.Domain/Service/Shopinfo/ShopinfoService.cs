using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.Shopinfo;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Domain.Service.Shopinfo
{
    public class ShopinfoService : IShopinfoService
    {
        #region Admin List
        public List<SP_ADMIN_SHOPINFO_SEL_Result> ShopinfoAdminList(SHOPINFO_ADMIN_SEARCH param)
        {
            if (!param.Page.HasValue || param.Page == 0)
                param.Page = 1;
            if (!param.PageSize.HasValue || param.PageSize == 0)
                param.PageSize = 10;

            List<SP_ADMIN_SHOPINFO_SEL_Result> lst = new List<SP_ADMIN_SHOPINFO_SEL_Result>();
            using (ShopinfoEntities EfContext = new ShopinfoEntities())
            {
                lst = EfContext.SP_ADMIN_SHOPINFO_SEL(param.UseYn, param.SearchCol, param.SearchKeyword, param.Page, param.PageSize).ToList();
            }

            return lst;

        }
        #endregion

        #region Admin Count
        public Int32 ShopinfoAdminCount(SHOPINFO_ADMIN_SEARCH param)
        {
            Int32 result = 0;
            using (ShopinfoEntities EfContext = new ShopinfoEntities())
            {
                result = EfContext.SP_ADMIN_SHOPINFO_CNT(param.UseYn, param.SearchCol, param.SearchKeyword).DefaultIfEmpty(0).FirstOrDefault().Value;
            }
            return result;
        }
        #endregion

        #region Admin View
        public SP_ADMIN_SHOPINFO_VIEW_Result ShopinfoAdminView(int IDX)
        {
            SP_ADMIN_SHOPINFO_VIEW_Result result = new SP_ADMIN_SHOPINFO_VIEW_Result();
            using (ShopinfoEntities EfContext = new ShopinfoEntities())
            {
                result = EfContext.SP_ADMIN_SHOPINFO_VIEW(IDX).FirstOrDefault();
            }
            return result;
        }
        #endregion

        #region Admin Insert
        public int ShopinfoAdminInsert(SP_ADMIN_SHOPINFO_VIEW_Result param, string ADM_ID)
        {
            ObjectParameter new_idx = new ObjectParameter("NEW_IDX", typeof(int));
            using (ShopinfoEntities EfContext = new ShopinfoEntities())
            {
                //string sHOP_NAME, string tEL_NUM, string pOST, string aDDR1, string aDDR2, string lOCATION_INFO, string lATITUDE, string lONGITUDE, Nullable<int> dISPLAY_ORDER, string uSE_YN, string aDM_ID
                EfContext.SP_ADMIN_SHOPINFO_INS(param.SHOP_NAME, param.TEL_NUM, param.POST, param.ADDR1, param.ADDR2, param.LOCATION_INFO, param.LATITUDE, param.LONGITUDE, 0, param.USE_YN, ADM_ID, new_idx);
            }
            return Convert.ToInt16(new_idx.Value);
        }
        #endregion

        #region Admin Update
        public void ShopinfoAdminUpdate(int IDX, SP_ADMIN_SHOPINFO_VIEW_Result param, string ADM_ID)
        {
            using (ShopinfoEntities EfContext = new ShopinfoEntities())
            {
                EfContext.SP_ADMIN_SHOPINFO_UPD(IDX, param.SHOP_NAME, param.TEL_NUM, param.POST, param.ADDR1, param.ADDR2, param.LOCATION_INFO, param.LATITUDE, param.LONGITUDE, 0, param.USE_YN, ADM_ID);
            }
        }
        #endregion
      
        #region Front List
        public List<SP_SHOPINFO_LIST_Result> ShopinfoList()
        {
            List<SP_SHOPINFO_LIST_Result> lst = new List<SP_SHOPINFO_LIST_Result>();
            using (ShopinfoEntities EfContext = new ShopinfoEntities())
            {
                lst = EfContext.SP_SHOPINFO_LIST().ToList();
            }
            return lst;
        }
        #endregion
    }
}
