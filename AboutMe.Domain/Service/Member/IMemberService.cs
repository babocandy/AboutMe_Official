using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.Member;

namespace AboutMe.Domain.Service.Member
{
    public interface IMemberService
    {
        SP_MEMBER_VIEW_Result GetMemberView(string sM_ID);  //회원 상세 1명
        int SetMemberLoginUpdate(string sM_ID); //회원-로그인후 방분기록 수정 .리턴 0:에러없음

    }
}
