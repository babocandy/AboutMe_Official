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
        List<SP_ADMIN_ADMIN_LOGIN_Result> GetAdminLoginList(string sADM_ID);  //관리자 로그인 

        List<SP_ADMIN_ADMIN_MEMBER_VIEW_Result> GetAdminMemberList(string sEARCH_COL, string sEARCH_KEYWORD, string sORT_COL, string sORT_DIR, int pAGE, int pAGESIZE);  //관리자 목록
        int GetAdminMemberListCnt(string sEARCH_COL, string sEARCH_KEYWORD); //관리자 목록 COUNT
        int SetAdminMemberInsert(string sADM_ID, string sADM_NAME, string sADM_PWD, string sADM_GRADE, string sADM_EMAIL, string sADM_PHONE, string sADM_USE_YN); //관리자 등록.리턴 0:에러없음
        int SetAdminMemberUpdate(string sADM_ID, string sADM_NAME, string sADM_PWD, string sADM_GRADE, string sADM_EMAIL, string sADM_PHONE, string sADM_USE_YN); //관리자 수정.리턴 0:에러없음
        SP_ADMIN_ADMIN_MEMBER_VIEW_Result GetAdminMemberView(string sADMIN_ID);  //관리자 상세 1명
    }

}
