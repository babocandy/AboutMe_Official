using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.AdminEtc;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Domain.Service.AdminEtc
{
    public class AdminUserService : IAdminUserService
    {

        //관리자 로그인
        public List<SP_ADMIN_ADMIN_LOGIN_Result> GetAdminLoginList(string sADM_ID = "")
        {

            List<SP_ADMIN_ADMIN_LOGIN_Result> lst = new List<SP_ADMIN_ADMIN_LOGIN_Result>();
            using (AdminEtcEntities AdmEtcContext = new AdminEtcEntities())
            {
                /**try {**/
                //lst = AdmEtcContext.SP_ADMIN_ADMIN_MEMBER_SEL(sEARCH_COL, sEARCH_KEYWORD, sORT_COL, sORT_DIR, pAGE, pAGESIZE).ToList();
                lst = AdmEtcContext.SP_ADMIN_ADMIN_LOGIN(sADM_ID).ToList();

                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;

        }

        //관리자 목록
        public List<SP_ADMIN_ADMIN_MEMBER_VIEW_Result> GetAdminMemberList(string sEARCH_COL, string sEARCH_KEYWORD, string sORT_COL, string sORT_DIR, int pAGE, int pAGESIZE)
        {

            List<SP_ADMIN_ADMIN_MEMBER_VIEW_Result> lst = new List<SP_ADMIN_ADMIN_MEMBER_VIEW_Result>();
            using (AdminEtcEntities AdmEtcContext = new AdminEtcEntities())
            {
                /**try {**/
                //lst = AdmEtcContext.SP_ADMIN_ADMIN_MEMBER_SEL(sEARCH_COL, sEARCH_KEYWORD, sORT_COL, sORT_DIR, pAGE, pAGESIZE).ToList();
                lst = AdmEtcContext.SP_ADMIN_ADMIN_MEMBER_SEL(sEARCH_COL, sEARCH_KEYWORD, sORT_COL, sORT_DIR, pAGE, pAGESIZE).ToList();

                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;

        }

        //관리자 목록 COUNT
        public int GetAdminMemberListCnt(string sEARCH_COL, string sEARCH_KEYWORD)
        {

            List<SP_ADMIN_COMMON_CNT_Result> lst = new List<SP_ADMIN_COMMON_CNT_Result>();
            int list_cnt = 0;

            using (AdminEtcEntities AdmEtcContext = new AdminEtcEntities())
            {
                /**try {**/
                lst = AdmEtcContext.SP_ADMIN_ADMIN_MEMBER_CNT(sEARCH_COL, sEARCH_KEYWORD).ToList();
                if (lst != null && lst.Count > 0)
                    list_cnt = lst[0].COUNT;
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return list_cnt;

        }

        //관리자 등록
        //리턴:ERR_CODE : 0:에러없음
        public int SetAdminMemberInsert(string sADM_ID = "", string sADM_NAME = "", string sADM_PWD = "", string sADM_GRADE = "A", string sADM_EMAIL="", string sADM_PHONE="", string sADM_USE_YN="N")
        {

            int ERR_CODE = 999; //일단 SP호출 에러 있음
            using (AdminEtcEntities AdmEtcContext = new AdminEtcEntities())
            {
                /**try {**/
                ObjectParameter objOutParam = new ObjectParameter("ERR_CODE", typeof(Int32));//sp의 output parameter변수명을 동일하게 사용한다.
               int sp_ret=  AdmEtcContext.SP_ADMIN_ADMIN_MEMBER_INS(sADM_ID, sADM_NAME, sADM_PWD, sADM_GRADE, sADM_EMAIL, sADM_PHONE, sADM_USE_YN, objOutParam);
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

        //관리자 수정
        //리턴:ERR_CODE : 0:에러없음
        public int SetAdminMemberUpdate(string sADM_ID = "", string sADM_NAME = "", string sADM_PWD = "", string sADM_GRADE = "A", string sADM_EMAIL = "", string sADM_PHONE = "", string sADM_USE_YN = "N")
        {

            int ERR_CODE = 999; //일단 SP호출 에러 있음
            using (AdminEtcEntities AdmEtcContext = new AdminEtcEntities())
            {
                /**try {**/
                ObjectParameter objOutParam = new ObjectParameter("ERR_CODE", typeof(Int32));//sp의 output parameter변수명을 동일하게 사용한다.
                int sp_ret = AdmEtcContext.SP_ADMIN_ADMIN_MEMBER_UPD(sADM_ID, sADM_NAME, sADM_PWD, sADM_GRADE, sADM_EMAIL, sADM_PHONE, sADM_USE_YN, objOutParam);
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
        public SP_ADMIN_ADMIN_MEMBER_VIEW_Result GetAdminMemberView(string sADMIN_ID="")
        {

            SP_ADMIN_ADMIN_MEMBER_VIEW_Result row1 = new SP_ADMIN_ADMIN_MEMBER_VIEW_Result();
            using (AdminEtcEntities AdmEtcContext = new AdminEtcEntities())
            {
                /**try {**/
                //lst = AdmEtcContext.SP_ADMIN_ADMIN_MEMBER_VIEW(sADMIN_ID).ToList();
                row1 = AdmEtcContext.SP_ADMIN_ADMIN_MEMBER_VIEW(sADMIN_ID).FirstOrDefault();

                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return row1;

        }



    }
}
