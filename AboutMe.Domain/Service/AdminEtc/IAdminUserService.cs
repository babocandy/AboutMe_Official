using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.AdminEtc;

namespace AboutMe.Domain.Service.AdminEtc
{
    public interface IAdminUserService
    {
        List<SP_ADMIN_ADMIN_MEMBER_VIEW_Result> GetAdminMemberList(string sEARCH_COL, string sEARCH_KEYWORD, string sORT_COL, string sORT_DIR, int pAGE, int pAGESIZE);  
        int GetAdminMemberListCnt(string sEARCH_COL, string sEARCH_KEYWORD); 

    }

}
