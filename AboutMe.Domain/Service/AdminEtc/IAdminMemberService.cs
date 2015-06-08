using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.AdminEtc;

namespace AboutMe.Domain.Service.AdminEtc
{
    public interface IAdminMemberService
    {
        List<SP_ADM_ADMIN_MEMBER_SEL_Result> GetAdminMemberList(string sEARCH_COL, string sEARCH_KEYWORD, string sORT_COL, string sORT_DIR, int pAGE, int pAGESIZE);
        int GetAdminMemberListCnt(string sEARCH_COL, string sEARCH_KEYWORD);
        List<SP_DUMMY_ADMIN_SEL_Result> GetDummyList();

        void InsertAdminMemberTest(string AdmId, string AdmName, string AdmPhoto , string AdmPass, string AdmDept, int Point);
        List<SP_ADM_ADMIN_DEPT_SEL_Result> GetAdmDeptList();
    }
}
