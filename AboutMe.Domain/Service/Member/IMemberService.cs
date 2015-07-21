using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.Member;
using AboutMe.Domain.Entity.Common;


//using System.Web;
//using System.Web.Mvc;
using System.Json;

namespace AboutMe.Domain.Service.Member
{
    public interface IMemberService
    {
        SP_MEMBER_VIEW_Result GetMemberView(string sM_ID);  //회원 상세 1명
        int SetMemberLoginUpdate(string sM_ID); //회원-로그인후 방분기록 수정 .리턴 0:에러없음

        ReturnDic GetMemberID_Dup_Check(string m_ID = ""); // 회원 계정중복 확인

        void SetRealNameLogInsert(string wORK_TMP_ID = "", string m_JOIN_TYPE = "", string rESULT_CODE = "", string tRAN_NO = "", string m_NAME = "", string di = "", string ci = "", string m_BIRTHDAY = "", string m_SEX = "", string nation = "", string rETURN_MSG_ALL = "", string iP = ""); ////회원 가입:실명인증 로그등록
        SP_MEMBER_REALNAME_LOG_VIEW_Result GetRealNameLogWorkTmp(string wORK_TMP_ID = ""); //회원 가입:실명인증 로그조회 1건 (1시간내)
        ReturnDic GetMemberFindDI(string m_DI = ""); //회원 가입:실명인증 DI  사용여부확인 <<-- 동일인 중복가입방지

        ReturnDic SetMemberRegister(string m_ID, string m_NAME, string m_PWD, string m_GRADE, string m_SEX, string m_BIRTHDAY, string m_MOBILE, string m_PHONE, string m_EMAIL, string m_ZIPCODE, string m_ADDR1, string m_ADDR2, string m_ISSMS, string m_ISEMAIL, string m_ISDM
            , string m_JOIN_MODE, string m_DI, string m_AGREE, string m_AGREE2, string m_SKIN_TROUBLE_CD
            , string m_GBN, string m_STAFF_COMPANY, string m_STAFF_ID); //회원 신규가입

    }
}
