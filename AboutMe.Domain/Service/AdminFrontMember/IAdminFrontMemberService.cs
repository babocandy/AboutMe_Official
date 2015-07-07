using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using AboutMe.Domain.Entity.AdminFrontMember;

namespace AboutMe.Domain.Service.AdminFrontMember
{
    public interface IAdminFrontMemberService
    {
        List<SP_ADMIN_FRONT_MEMBER_VIEW_Result> GetAdminFrontMemberList(string dATE_FROM, string dATE_TO, string gRADE_LIST, string gBN, string iS_RETIRE, string sEARCH_COL, string sEARCH_KEYWORD, string sORT_COL, string sORT_DIR, int pAGE, int pAGESIZE); //관리자-회원 목록 
        int GetAdminFrontMemberListCount(string dATE_FROM, string dATE_TO, string gRADE_LIST, string gBN, string iS_RETIRE, string sEARCH_COL, string sEARCH_KEYWORD); //관리자 -회원 목록 COUNT

        int SetAdminFrontMemberUpdate(string m_ID, string m_GRADE, string m_MOBILE, string m_PHONE, string m_EMAIL, string m_ZIPCODE, string m_ADDR1, string m_ADDR2, string m_ISSMS, string m_ISEMAIL, string m_ISDM, string m_SKIN_TROUBLE_CD, string m_GBN, string m_STAFF_COMAPNY, string m_STAFF_ID, string m_IS_RETIRE, string m_DEL_REASON); //관리자 -회원수정.리턴 0:에러없음 <<사용안함
        int SetAdminFrontMemberUpdate_Basic(string m_ID, string m_GRADE, string m_MOBILE, string m_PHONE, string m_EMAIL, string m_ZIPCODE, string m_ADDR1, string m_ADDR2, string m_ISSMS, string m_ISEMAIL, string m_ISDM); //관리자 -회원수정-기본정보.리턴 0:에러없음
        int SetAdminFrontMemberUpdate_PWD(string m_ID, string m_PWD); //관리자 -회원수정-암호변경.리턴 0:에러없음
        int SetAdminFrontMemberUpdate_Staff(string m_ID, string m_GBN, string m_STAFF_COMAPNY, string m_STAFF_ID); //관리자 -회원수정-임직원.리턴 0:에러없음
        int SetAdminFrontMemberUpdate_Retire(string m_ID, string m_IS_RETIRE, string m_DEL_REASON); //관리자 -회원수정-탈퇴.리턴 0:에러없음

        SP_ADMIN_FRONT_MEMBER_VIEW_Result GetAdminFrontMemberView(string m_ID);  //관리자-회원 상세 1명

    }
}
