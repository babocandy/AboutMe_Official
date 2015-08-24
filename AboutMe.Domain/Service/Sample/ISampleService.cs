using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.Sample;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Domain.Service.Sample
{
    public interface ISampleService
    {
        //Admin
        List<SP_ADMIN_SAMPLE_SEL_Result> SampleAdminList(SAMPLE_ADMIN_SEARCH param);
        int SampleAdminCount(SAMPLE_ADMIN_SEARCH param);
        SP_ADMIN_SAMPLE_VIEW_Result SampleAdminView(int IDX);
        int SampleAdminInsert(SP_ADMIN_SAMPLE_VIEW_Result param);
        void SampleAdminUpdate(int IDX, SP_ADMIN_SAMPLE_VIEW_Result param);

        SP_SAMPLE_VIEW_Result SampleView(int IDX);

        List<SP_ADMIN_SAMPLE_MEMBER_SEL_Result> SampleAdminMemberList(int IDX, int Page = 1, int PageSize = 10);
        int SampleAdminMemberCount(int IDX);
        void SampleRequest(int IDX, string M_ID);
    }
}
