using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.Member;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Domain.Service.Member
{
    public class MemberService : IMemberService
    {

        //관리자 상세 1명
        //public List<SP_ADMIN_ADMIN_MEMBER_VIEW_Result> GetAdminMemberView(string sADMIN_ID = "")
        public SP_MEMBER_VIEW_Result GetMemberView(string sM_ID = "")
        {

            SP_MEMBER_VIEW_Result row1 = new SP_MEMBER_VIEW_Result();
            using (MemberEntities MemberContext = new MemberEntities())
            {
                /**try {**/
                row1 = MemberContext.SP_MEMBER_VIEW(sM_ID).FirstOrDefault();

                /** }catch()
                 {
                       MemberContext.Dispose();
                 }**/
            }

            return row1;

        }

        //회원 로그인후 수정: 방수일,방문횟수
        //리턴:무조건 성공으로 간주. 리턴 0:에러없음
        public int SetMemberLoginUpdate(string sM_ID = "")
        {

            int ERR_CODE = 0; //에러 없음
            using (MemberEntities MemberContext = new MemberEntities())
            {
                /**try {**/
                //ObjectParameter objOutParam = new ObjectParameter("ERR_CODE", typeof(Int32));//sp의 output parameter변수명을 동일하게 사용한다.
                //int sp_ret = MemberContext.SP_MEMBER_LOGIN_UPD(sM_ID, objOutParam);
                int sp_ret = MemberContext.SP_MEMBER_LOGIN_UPD(sM_ID);
                //ERR_CODE = (int)objOutParam.Value;

                //if (sp_ret==1)
                //     ERR_CODE = (int)objOutParam.Value;
                //else
                //    ERR_CODE = (int)sp_ret;



                /** }catch()
                 {
                       MemberContext.Dispose();
                 }**/
            }

            return ERR_CODE;

        }

    }
}
