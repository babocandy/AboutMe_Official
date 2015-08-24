using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.Sample;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Domain.Service.Sample
{
    public class SampleService : ISampleService
    {
        #region Admin List
        public List<SP_ADMIN_SAMPLE_SEL_Result> SampleAdminList(SAMPLE_ADMIN_SEARCH param)
        {
            if (!param.Page.HasValue || param.Page == 0)
                param.Page = 1;
            if (!param.PageSize.HasValue || param.PageSize == 0)
                param.PageSize = 10;

            List<SP_ADMIN_SAMPLE_SEL_Result> lst = new List<SP_ADMIN_SAMPLE_SEL_Result>();
            using (SampleEntities EfContext = new SampleEntities())
            {
                lst = EfContext.SP_ADMIN_SAMPLE_SEL(param.FromDate, param.ToDate, param.IngGbn, param.UseYn, param.SearchCol, param.SearchKeyword, param.Page, param.PageSize).ToList();
            }

            return lst;

        }
        #endregion

        #region Admin Count
        public int SampleAdminCount(SAMPLE_ADMIN_SEARCH param)
        {
            int? result = 0;
            using (SampleEntities EfContext = new SampleEntities())
            {
                result = EfContext.SP_ADMIN_SAMPLE_CNT(param.FromDate, param.ToDate, param.IngGbn, param.UseYn, param.SearchCol, param.SearchKeyword).DefaultIfEmpty(0).FirstOrDefault();
            }
            if (!result.HasValue)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt16(result);
            }
        }
        #endregion

        #region Admin View
        public SP_ADMIN_SAMPLE_VIEW_Result SampleAdminView(int IDX)
        {
            SP_ADMIN_SAMPLE_VIEW_Result result = new SP_ADMIN_SAMPLE_VIEW_Result();
            using (SampleEntities EfContext = new SampleEntities())
            {
                result = EfContext.SP_ADMIN_SAMPLE_VIEW(IDX).FirstOrDefault();
            }
            return result;
        }
        #endregion

        #region Admin Insert
        public int SampleAdminInsert(SP_ADMIN_SAMPLE_VIEW_Result param)
        {
            ObjectParameter new_idx = new ObjectParameter("NEW_IDX", typeof(int));
            using (SampleEntities EfContext = new SampleEntities())
            {
                EfContext.SP_ADMIN_SAMPLE_INS(param.SAMPLE_NAME, param.FROM_DATE, param.FROM_TIME, param.TO_DATE, param.TO_TIME, param.SAMPLE_GBN, param.WEB_CONTENTS, param.MOBILE_FILE, param.WEB_BANNER, param.MOBILE_BANNER, param.USE_YN, param.ADM_ID, new_idx);
            }
            return Convert.ToInt16(new_idx.Value);
        }
        #endregion

        #region Admin Update
        public void SampleAdminUpdate(int IDX, SP_ADMIN_SAMPLE_VIEW_Result param)
        {
            using (SampleEntities EfContext = new SampleEntities())
            {
                EfContext.SP_ADMIN_SAMPLE_UPD(IDX, param.SAMPLE_NAME, param.FROM_DATE, param.FROM_TIME, param.TO_DATE, param.TO_TIME, param.SAMPLE_GBN, param.WEB_CONTENTS, param.MOBILE_FILE, param.WEB_BANNER, param.MOBILE_BANNER, param.USE_YN, param.ADM_ID);
            }
        }
        #endregion

        #region User  View
        public SP_SAMPLE_VIEW_Result SampleView(int IDX)
        {
            SP_SAMPLE_VIEW_Result result = new SP_SAMPLE_VIEW_Result();
            using (SampleEntities EfContext = new SampleEntities())
            {
                result = EfContext.SP_SAMPLE_VIEW(IDX).FirstOrDefault();
            }
            return result;
        }
        #endregion

        #region Admin Member List
        public List<SP_ADMIN_SAMPLE_MEMBER_SEL_Result> SampleAdminMemberList(int IDX, int Page=1, int PageSize=10)
        {
            if (Page == 0)
                Page = 1;
            if (PageSize == 0)
                PageSize = 10;

            List<SP_ADMIN_SAMPLE_MEMBER_SEL_Result> lst = new List<SP_ADMIN_SAMPLE_MEMBER_SEL_Result>();
            using (SampleEntities EfContext = new SampleEntities())
            {
                lst = EfContext.SP_ADMIN_SAMPLE_MEMBER_SEL(IDX,Page,PageSize).ToList();
            }

            return lst;

        }
        #endregion

        #region Admin Member Count
        public int SampleAdminMemberCount(int IDX)
        {
            int? result = 0;
            using (SampleEntities EfContext = new SampleEntities())
            {
                result = EfContext.SP_ADMIN_SAMPLE_MEMBER_CNT(IDX).DefaultIfEmpty(0).FirstOrDefault();
            }
            if (!result.HasValue)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt16(result);
            }
        }
        #endregion

        
        #region 체험단 신청하기
        public void SampleRequest(int IDX, string M_ID)
        {
            using (SampleEntities EfContext = new SampleEntities())
            {
                EfContext.SP_SAMPLE_REQUEST(IDX, M_ID);
            }
        }
        #endregion
    }
}
