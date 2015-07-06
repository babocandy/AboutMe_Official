using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.AdminFrontMember;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Domain.Service.AdminFrontMember
{
    public class AdminFrontMemberService : IAdminFrontMemberService
    {

        //관리자 - 회원 목록
        public List<SP_ADMIN_FRONT_MEMBER_VIEW_Result> GetAdminFrontMemberList(string dATE_FROM = "", string dATE_TO = "", string gRADE_LIST = "", string gBN = "", string iS_RETIRE = "", string sEARCH_COL = "", string sEARCH_KEYWORD = "", string sORT_COL="", string sORT_DIR="", int pAGE=1, int pAGESIZE=10)
        {

            List<SP_ADMIN_FRONT_MEMBER_VIEW_Result> lst = new List<SP_ADMIN_FRONT_MEMBER_VIEW_Result>();
            using (AdminFrontMemberEntities AdminFrontMemberContext = new AdminFrontMemberEntities())
            {
                /**try {**/
                //lst = AdmEtcContext.SP_ADMIN_ADMIN_MEMBER_SEL(sEARCH_COL, sEARCH_KEYWORD, sORT_COL, sORT_DIR, pAGE, pAGESIZE).ToList();
                lst = AdminFrontMemberContext.SP_ADMIN_FRONT_MEMBER_SEL(dATE_FROM, dATE_TO, gRADE_LIST, gBN, iS_RETIRE, sEARCH_COL, sEARCH_KEYWORD, sORT_COL, sORT_DIR, pAGE, pAGESIZE).ToList();

                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;

        }

        //관리자-회원 목록 COUNT
        public int GetAdminFrontMemberListCount(string dATE_FROM = "", string dATE_TO = "", string gRADE_LIST = "", string gBN = "", string iS_RETIRE = "", string sEARCH_COL = "", string sEARCH_KEYWORD = "")
        {

            List<SP_ADMIN_FRONT_COMMON_CNT_Result> lst = new List<SP_ADMIN_FRONT_COMMON_CNT_Result>();
            int list_cnt = 0;

            using (AdminFrontMemberEntities AdminFrontMemberContext = new AdminFrontMemberEntities())
            {
                /**try {**/
                lst = AdminFrontMemberContext.SP_ADMIN_FRONT_MEMBER_CNT(dATE_FROM, dATE_TO, gRADE_LIST, gBN, iS_RETIRE, sEARCH_COL, sEARCH_KEYWORD).ToList();
                if (lst != null && lst.Count > 0)
                    list_cnt = lst[0].COUNT;
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return list_cnt;

        }

        //관리자 -회원 수정
        //리턴:ERR_CODE : 0:에러없음
        public int SetAdminFrontMemberUpdate(string m_ID = "", string m_GRADE = "A", string m_MOBILE = "", string m_PHONE = "", string m_EMAIL = "", string m_ZIPCODE = "", string m_ADDR1 = "", string m_ADDR2 = "", string m_ISSMS = "", string m_ISEMAIL = "", string m_ISDM = "", string m_SKIN_TROUBLE_CD = "", string m_GBN = "", string m_STAFF_COMAPNY = "", string m_STAFF_ID = "", string m_IS_RETIRE = "", string m_DEL_REASON = "")
        {

            int ERR_CODE = 999; //일단 SP호출 에러 있음
            using (AdminFrontMemberEntities AdminFrontMemberContext = new AdminFrontMemberEntities())
            {
                /**try {**/
                ObjectParameter objOutParam = new ObjectParameter("ERR_CODE", typeof(Int32));//sp의 output parameter변수명을 동일하게 사용한다.
                int sp_ret = AdminFrontMemberContext.SP_ADMIN_FRONT_MEMBER_UPD(m_ID, m_GRADE, m_MOBILE, m_PHONE, m_EMAIL, m_ZIPCODE, m_ADDR1,m_ADDR2,m_ISSMS,m_ISEMAIL,m_ISDM,m_SKIN_TROUBLE_CD,m_GBN,m_STAFF_COMAPNY,m_STAFF_ID,m_IS_RETIRE,m_DEL_REASON,  objOutParam);
                ERR_CODE = (int)objOutParam.Value;

                //if (sp_ret==1)
                //     ERR_CODE = (int)objOutParam.Value;
                //else
                //    ERR_CODE = (int)sp_ret;



                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return ERR_CODE;

        }

        //관리자 -회원 암호수정
        //리턴:ERR_CODE : 0:에러없음
        public int SetAdminFrontMemberPwdUpdate(string m_ID = "", string m_PWD = "")
        {

            int ERR_CODE = 999; //일단 SP호출 에러 있음
            using (AdminFrontMemberEntities AdminFrontMemberContext = new AdminFrontMemberEntities())
            {
                /**try {**/
                ObjectParameter objOutParam = new ObjectParameter("ERR_CODE", typeof(Int32));//sp의 output parameter변수명을 동일하게 사용한다.
                int sp_ret = AdminFrontMemberContext.SP_ADMIN_FRONT_MEMBER_PWD_UPD(m_ID, m_PWD, objOutParam);
                ERR_CODE = (int)objOutParam.Value;

                //if (sp_ret==1)
                //     ERR_CODE = (int)objOutParam.Value;
                //else
                //    ERR_CODE = (int)sp_ret;



                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return ERR_CODE;

        }




        //관리자 상세 1명
        //public List<SP_ADMIN_ADMIN_MEMBER_VIEW_Result> GetAdminMemberView(string sADMIN_ID = "")
        public SP_ADMIN_FRONT_MEMBER_VIEW_Result GetAdminFrontMemberView(string m_ID = "")
        {

            SP_ADMIN_FRONT_MEMBER_VIEW_Result row1 = new SP_ADMIN_FRONT_MEMBER_VIEW_Result();
            using (AdminFrontMemberEntities AdminFrontMemberContext = new AdminFrontMemberEntities())
            {
                /**try {**/
                //lst = AdmEtcContext.SP_ADMIN_ADMIN_MEMBER_VIEW(sADMIN_ID).ToList();
                row1 = AdminFrontMemberContext.SP_ADMIN_FRONT_MEMBER_VIEW(m_ID).FirstOrDefault();

                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return row1;

        }




    }
}
