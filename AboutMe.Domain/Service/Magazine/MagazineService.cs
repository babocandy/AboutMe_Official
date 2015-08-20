using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.Magazine;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Domain.Service.Magazine
{
    public class MagazineService : IMagazineService
    {
        #region Admin List
        public List<SP_ADMIN_MAGAZINE_SEL_Result> MagazineAdminList(MAGAZINE_ADMIN_SEARCH param)
        {
            if (!param.Page.HasValue || param.Page == 0)
                param.Page = 1;
            if (!param.PageSize.HasValue || param.PageSize == 0)
                param.PageSize = 10;

            List<SP_ADMIN_MAGAZINE_SEL_Result> lst = new List<SP_ADMIN_MAGAZINE_SEL_Result>();
            using (MagazineEntities EfContext = new MagazineEntities())
            {
                lst = EfContext.SP_ADMIN_MAGAZINE_SEL(param.UseYn, param.SearchCol, param.SearchKeyword, param.Page, param.PageSize).ToList();
            }

            return lst;

        }
        #endregion

        #region Admin Count
        public Int32 MagazineAdminCount(MAGAZINE_ADMIN_SEARCH param)
        {
            Int32 result = 0;
            using (MagazineEntities EfContext = new MagazineEntities())
            {
                result = EfContext.SP_ADMIN_MAGAZINE_CNT(param.UseYn, param.SearchCol, param.SearchKeyword).DefaultIfEmpty(0).FirstOrDefault().Value;
            }
            return result;
        }
        #endregion

        #region Admin View
        public SP_ADMIN_MAGAZINE_VIEW_Result MagazineAdminView(int IDX)
        {
            SP_ADMIN_MAGAZINE_VIEW_Result result = new SP_ADMIN_MAGAZINE_VIEW_Result();
            using (MagazineEntities EfContext = new MagazineEntities())
            {
                result = EfContext.SP_ADMIN_MAGAZINE_VIEW(IDX).FirstOrDefault();
            }
            return result;
        }
        #endregion

        #region Admin Insert
        public int MagazineAdminInsert(SP_ADMIN_MAGAZINE_VIEW_Result param, string ADM_ID)
        {
            ObjectParameter new_idx = new ObjectParameter("NEW_IDX", typeof(int));
            using (MagazineEntities EfContext = new MagazineEntities())
            {
                EfContext.SP_ADMIN_MAGAZINE_INS(param.TITLE, param.SUB_TITLE, param.CONTENT_GBN, param.THUMB_IMG_PATH, param.IMG_PATH, param.MOVIE_URL, 0, param.USE_YN, ADM_ID, new_idx);
            }
            return Convert.ToInt16(new_idx.Value);
        }
        #endregion

        #region Admin Update
        public void MagazineAdminUpdate(int IDX, SP_ADMIN_MAGAZINE_VIEW_Result param, string ADM_ID)
        {
            using (MagazineEntities EfContext = new MagazineEntities())
            {
                EfContext.SP_ADMIN_MAGAZINE_UPD(IDX, param.TITLE, param.SUB_TITLE, param.CONTENT_GBN, param.THUMB_IMG_PATH, param.IMG_PATH, param.MOVIE_URL, 0, param.USE_YN, ADM_ID);
            }
        }
        #endregion
        
        #region User View
        public SP_MAGAZINE_VIEW_Result MagazineView(int IDX)
        {
            SP_MAGAZINE_VIEW_Result result = new SP_MAGAZINE_VIEW_Result();
            using (MagazineEntities EfContext = new MagazineEntities())
            {
                result = EfContext.SP_MAGAZINE_VIEW(IDX).FirstOrDefault();
            }
            return result;
        }
        #endregion

        #region Front List
        public List<SP_MAGAZINE_LIST_Result> MagazineList()
        {
            List<SP_MAGAZINE_LIST_Result> lst = new List<SP_MAGAZINE_LIST_Result>();
            using (MagazineEntities EfContext = new MagazineEntities())
            {
                lst = EfContext.SP_MAGAZINE_LIST().ToList();
            }
            return lst;
        }
        #endregion
    }
}
