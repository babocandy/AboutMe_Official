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

        //관리자 -회원 수정 :기본정보
        //리턴:ERR_CODE : 0:에러없음
        public int SetAdminFrontMemberUpdate_Basic(string m_ID = "", string m_GRADE = "A", string m_MOBILE = "", string m_PHONE = "", string m_EMAIL = "", string m_ZIPCODE = "", string m_ADDR1 = "", string m_ADDR2 = "", string m_ISSMS = "", string m_ISEMAIL = "", string m_ISDM = "")
        {

            int ERR_CODE = 999; //일단 SP호출 에러 있음
            using (AdminFrontMemberEntities AdminFrontMemberContext = new AdminFrontMemberEntities())
            {
                /**try {**/
                ObjectParameter objOutParam = new ObjectParameter("ERR_CODE", typeof(Int32));//sp의 output parameter변수명을 동일하게 사용한다.
                int sp_ret = AdminFrontMemberContext.SP_ADMIN_FRONT_MEMBER_UPD_BASIC(m_ID, m_GRADE, m_MOBILE, m_PHONE, m_EMAIL, m_ZIPCODE, m_ADDR1, m_ADDR2, m_ISSMS, m_ISEMAIL, m_ISDM, objOutParam);
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

        //관리자 -회원 수정 :암호변경
        //리턴:ERR_CODE : 0:에러없음
        public int SetAdminFrontMemberUpdate_PWD(string m_ID = "", string m_PWD = "")
        {

            int ERR_CODE = 999; //일단 SP호출 에러 있음
            using (AdminFrontMemberEntities AdminFrontMemberContext = new AdminFrontMemberEntities())
            {
                /**try {**/
                ObjectParameter objOutParam = new ObjectParameter("ERR_CODE", typeof(Int32));//sp의 output parameter변수명을 동일하게 사용한다.
                int sp_ret = AdminFrontMemberContext.SP_ADMIN_FRONT_MEMBER_UPD_PWD(m_ID, m_PWD, objOutParam);
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

        //관리자 -회원 수정 :임직원정
        //리턴:ERR_CODE : 0:에러없음
        public int SetAdminFrontMemberUpdate_Staff(string m_ID = "", string m_GBN = "", string m_STAFF_COMAPNY = "", string m_STAFF_ID = "")
        {

            int ERR_CODE = 999; //일단 SP호출 에러 있음
            using (AdminFrontMemberEntities AdminFrontMemberContext = new AdminFrontMemberEntities())
            {
                /**try {**/
                ObjectParameter objOutParam = new ObjectParameter("ERR_CODE", typeof(Int32));//sp의 output parameter변수명을 동일하게 사용한다.
                int sp_ret = AdminFrontMemberContext.SP_ADMIN_FRONT_MEMBER_UPD_STAFF(m_ID, m_GBN,m_STAFF_COMAPNY,m_STAFF_ID, objOutParam);
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


        //관리자 -회원 수정 :탈퇴
        //리턴:ERR_CODE : 0:에러없음
        public int SetAdminFrontMemberUpdate_Retire(string m_ID = "", string m_IS_RETIRE = "", string m_DEL_REASON = "")
        {

            int ERR_CODE = 999; //일단 SP호출 에러 있음
            using (AdminFrontMemberEntities AdminFrontMemberContext = new AdminFrontMemberEntities())
            {
                /**try {**/
                ObjectParameter objOutParam = new ObjectParameter("ERR_CODE", typeof(Int32));//sp의 output parameter변수명을 동일하게 사용한다.
                int sp_ret = AdminFrontMemberContext.SP_ADMIN_FRONT_MEMBER_UPD_RETIRE(m_ID, m_IS_RETIRE, m_DEL_REASON, objOutParam);
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


        //---임직원 신청----------------------------------------------------------------------------------------

        //관리자 - 임직원 신청 목록
        public List<SP_ADMIN_MEMBER_STAFF_REQUST_VIEW_Result> GetAdminMemberStaffRequestList(string dATE_FROM = "", string dATE_TO = "", string sTATUS = "", string sEARCH_COL = "", string sEARCH_KEYWORD = "", string sORT_COL = "", string sORT_DIR = "", int pAGE = 1, int pAGESIZE = 10)
        {

            List<SP_ADMIN_MEMBER_STAFF_REQUST_VIEW_Result> lst = new List<SP_ADMIN_MEMBER_STAFF_REQUST_VIEW_Result>();
            using (AdminFrontMemberEntities AdminFrontMemberContext = new AdminFrontMemberEntities())
            {
                /**try {**/
                //lst = AdmEtcContext.SP_ADMIN_ADMIN_MEMBER_SEL(sEARCH_COL, sEARCH_KEYWORD, sORT_COL, sORT_DIR, pAGE, pAGESIZE).ToList();
                lst = AdminFrontMemberContext.SP_ADMIN_MEMBER_STAFF_REQUST_SEL(dATE_FROM, dATE_TO, sTATUS,sEARCH_COL, sEARCH_KEYWORD, sORT_COL, sORT_DIR, pAGE, pAGESIZE).ToList();

                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;

        }

        //관리자-임직원 신청 목록 COUNT
        public int GetAdminMemberStaffRequestListCount(string dATE_FROM = "", string dATE_TO = "", string sTATUS = "", string sEARCH_COL = "", string sEARCH_KEYWORD = "")
        {

            List<SP_ADMIN_FRONT_COMMON_CNT_Result> lst = new List<SP_ADMIN_FRONT_COMMON_CNT_Result>();
            int list_cnt = 0;

            using (AdminFrontMemberEntities AdminFrontMemberContext = new AdminFrontMemberEntities())
            {
                /**try {**/
                lst = AdminFrontMemberContext.SP_ADMIN_MEMBER_STAFF_REQUST_CNT(dATE_FROM, dATE_TO, sTATUS, sEARCH_COL, sEARCH_KEYWORD).ToList();
                if (lst != null && lst.Count > 0)
                    list_cnt = lst[0].COUNT;
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return list_cnt;
        }

        //관리자 -임직원 신청 수정 
        //리턴:ERR_CODE : 0:에러없음
        public int SetAdminMemberStaffRequestUpdate(int iDX = -1, string sTATUS = "", string pROC_COMMENT = "", string pROC_ADM_ID = "")
        {

            int ERR_CODE = 999; //일단 SP호출 에러 있음
            using (AdminFrontMemberEntities AdminFrontMemberContext = new AdminFrontMemberEntities())
            {
                /**try {**/
                ObjectParameter objOutParam = new ObjectParameter("ERR_CODE", typeof(Int32));//sp의 output parameter변수명을 동일하게 사용한다.
                int sp_ret = AdminFrontMemberContext.SP_ADMIN_MEMBER_STAFF_REQUST_UPD(iDX, sTATUS, pROC_COMMENT, pROC_ADM_ID, objOutParam);
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

        //관리자 임직원 신청 - 상세 1건
        public SP_ADMIN_MEMBER_STAFF_REQUST_VIEW_Result GetAdminMemberStaffRequestView(int iDX = -1)
        {

            SP_ADMIN_MEMBER_STAFF_REQUST_VIEW_Result row1 = new SP_ADMIN_MEMBER_STAFF_REQUST_VIEW_Result();
            using (AdminFrontMemberEntities AdminFrontMemberContext = new AdminFrontMemberEntities())
            {
                /**try {**/
                row1 = AdminFrontMemberContext.SP_ADMIN_MEMBER_STAFF_REQUST_VIEW(iDX).FirstOrDefault();

                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return row1;

        }

        //----------------------------------------------------------------------------------------------------
        //관리자  -임직원신청 상세>  연관정보 :임직원DB
        public List<SP_ADMIN_MEMBER_STAFF_REQUST_REF_BASEDB_Result> GetAdminMemberStaffRequest_REF_BASEDB(string sTAFF_ID="")
        {

            List<SP_ADMIN_MEMBER_STAFF_REQUST_REF_BASEDB_Result> lst = new List<SP_ADMIN_MEMBER_STAFF_REQUST_REF_BASEDB_Result>();
            using (AdminFrontMemberEntities AdminFrontMemberContext = new AdminFrontMemberEntities())
            {
                /**try {**/
                lst = AdminFrontMemberContext.SP_ADMIN_MEMBER_STAFF_REQUST_REF_BASEDB(sTAFF_ID).ToList();

                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;
        }

        //관리자  -임직원신청 상세>  연관정보 : 회원TBL
        public List<SP_ADMIN_MEMBER_STAFF_REQUST_REF_MEMBER_Result> GetAdminMemberStaffRequest_REF_MEMBER(string sTAFF_ID = "")
        {

            List<SP_ADMIN_MEMBER_STAFF_REQUST_REF_MEMBER_Result> lst = new List<SP_ADMIN_MEMBER_STAFF_REQUST_REF_MEMBER_Result>();
            using (AdminFrontMemberEntities AdminFrontMemberContext = new AdminFrontMemberEntities())
            {
                /**try {**/
                lst = AdminFrontMemberContext.SP_ADMIN_MEMBER_STAFF_REQUST_REF_MEMBER(sTAFF_ID).ToList();

                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;
        }

        //관리자  -임직원신청 상세>  연관정보 : 임직원신청이력
        public List<SP_ADMIN_MEMBER_STAFF_REQUST_REF_REQUEST_Result> GetAdminMemberStaffRequest_REF_REQUEST(string sTAFF_ID = "")
        {

            List<SP_ADMIN_MEMBER_STAFF_REQUST_REF_REQUEST_Result> lst = new List<SP_ADMIN_MEMBER_STAFF_REQUST_REF_REQUEST_Result>();
            using (AdminFrontMemberEntities AdminFrontMemberContext = new AdminFrontMemberEntities())
            {
                /**try {**/
                lst = AdminFrontMemberContext.SP_ADMIN_MEMBER_STAFF_REQUST_REF_REQUEST(sTAFF_ID).ToList();

                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;
        }
 
        //----------------------------------------------------------------------------------------------------------
        //관리자 - 임직원기준DB -목록
        public List<SP_ADMIN_MEMBER_STAFF_BASE_VIEW_Result> GetAdminMemberStaffBaseList(string dATE_FROM="", string dATE_TO="", string sEARCH_COL="", string sEARCH_KEYWORD="", string sORT_COL="IDX", string sORT_DIR="DESC", int pAGE=1, int pAGESIZE=10)
        {

            List<SP_ADMIN_MEMBER_STAFF_BASE_VIEW_Result> lst = new List<SP_ADMIN_MEMBER_STAFF_BASE_VIEW_Result>();
            using (AdminFrontMemberEntities AdminFrontMemberContext = new AdminFrontMemberEntities())
            {
                /**try {**/
                lst = AdminFrontMemberContext.SP_ADMIN_MEMBER_STAFF_BASE_SEL(dATE_FROM, dATE_TO, sEARCH_COL, sEARCH_KEYWORD,  sORT_COL, sORT_DIR, pAGE, pAGESIZE).ToList();

                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;

        }

        //관리자-임직원기준DB  목록 COUNT
        public int GetAdminMemberStaffBaseCount(string dATE_FROM = "", string dATE_TO = "", string sEARCH_COL = "", string sEARCH_KEYWORD = "")
        {

            List<SP_ADMIN_FRONT_COMMON_CNT_Result> lst = new List<SP_ADMIN_FRONT_COMMON_CNT_Result>();
            int list_cnt = 0;

            using (AdminFrontMemberEntities AdminFrontMemberContext = new AdminFrontMemberEntities())
            {
                /**try {**/
                lst = AdminFrontMemberContext.SP_ADMIN_MEMBER_STAFF_BASE_CNT(dATE_FROM, dATE_TO, sEARCH_COL, sEARCH_KEYWORD).ToList();
                if (lst != null && lst.Count > 0)
                    list_cnt = lst[0].COUNT;
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return list_cnt;
        }

        //관리자 -임직원기준DB -INSERT 1건
        //리턴:ERR_CODE : 0:에러없음
        public int SetAdminMemberStaffBaseInsert(string sTAFF_COMPANY="", string sTAFF_ID="", string sTAFF_NAME="", string wORK_TEMP_ID="")
        {

            int ERR_CODE = 999; //일단 SP호출 에러 있음
            using (AdminFrontMemberEntities AdminFrontMemberContext = new AdminFrontMemberEntities())
            {
                /**try {**/
                ObjectParameter objOutParam = new ObjectParameter("ERR_CODE", typeof(Int32));//sp의 output parameter변수명을 동일하게 사용한다.
                int sp_ret = AdminFrontMemberContext.SP_ADMIN_MEMBER_STAFF_BASE_INS(sTAFF_COMPANY, sTAFF_ID, sTAFF_NAME, wORK_TEMP_ID, objOutParam);
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

        //관리자 -임직원기준DB -삭제1건 
        //리턴:ERR_CODE : 0:에러없음
        public int SetAdminMemberStaffBaseDel(int iDX = -1)
        {

            int ERR_CODE = 999; //일단 SP호출 에러 있음
            using (AdminFrontMemberEntities AdminFrontMemberContext = new AdminFrontMemberEntities())
            {
                /**try {**/
                ObjectParameter objOutParam = new ObjectParameter("ERR_CODE", typeof(Int32));//sp의 output parameter변수명을 동일하게 사용한다.
                int sp_ret = AdminFrontMemberContext.SP_ADMIN_MEMBER_STAFF_BASE_DEL(iDX, objOutParam);
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

        //관리자 -임직원기준DB -사번 중복체크
        //리턴:ERR_CODE : 0:에러없음
        public int SetAdminMemberStaffBaseDupCheck(string sTAFF_ID="")
        {

            int ERR_CODE = 999; //일단 SP호출 에러 있음
            using (AdminFrontMemberEntities AdminFrontMemberContext = new AdminFrontMemberEntities())
            {
                /**try {**/
                ObjectParameter objOutParam = new ObjectParameter("ERR_CODE", typeof(Int32));//sp의 output parameter변수명을 동일하게 사용한다.
                int sp_ret = AdminFrontMemberContext.SP_ADMIN_MEMBER_STAFF_BASE_DUP_CHECK(sTAFF_ID, objOutParam);
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
        
        //----------------------------------------------------------------------------------------------------------
        //관리자 - 임직원기준DB TEMP-목록
        public List<SP_ADMIN_MEMBER_STAFF_BASE_TMP_LIST_Result> GetAdminMemberStaffBaseTempList(string wORK_TEMP_ID = "")
        {

            List<SP_ADMIN_MEMBER_STAFF_BASE_TMP_LIST_Result> lst = new List<SP_ADMIN_MEMBER_STAFF_BASE_TMP_LIST_Result>();
            using (AdminFrontMemberEntities AdminFrontMemberContext = new AdminFrontMemberEntities())
            {
                /**try {**/
                lst = AdminFrontMemberContext.SP_ADMIN_MEMBER_STAFF_BASE_TMP_LIST(wORK_TEMP_ID).ToList();

                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;

        }

        //관리자-임직원기준DB TEMP-목록 COUNT
        public int GetAdminMemberStaffBaseTempCount(string wORK_TEMP_ID = "", string aPP_RESULT="")
        {

            List<SP_ADMIN_FRONT_COMMON_CNT_Result> lst = new List<SP_ADMIN_FRONT_COMMON_CNT_Result>();
            int list_cnt = 0;

            using (AdminFrontMemberEntities AdminFrontMemberContext = new AdminFrontMemberEntities())
            {
                /**try {**/
                lst = AdminFrontMemberContext.SP_ADMIN_MEMBER_STAFF_BASE_TMP_CNT(wORK_TEMP_ID, aPP_RESULT).ToList();
                if (lst != null && lst.Count > 0)
                    list_cnt = lst[0].COUNT;
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return list_cnt;
        }

        //관리자 -임직원기준DB TEMP -INSERT 1건
        //리턴:ERR_CODE : 0:에러없음
        public int SetAdminMemberStaffBaseTempInsert(string sTAFF_COMPANY = "", string sTAFF_ID = "", string sTAFF_NAME = "", string wORK_TEMP_ID = "", string aDM_ID="", string iP="")
        {

            int ERR_CODE = 999; //일단 SP호출 에러 있음
            using (AdminFrontMemberEntities AdminFrontMemberContext = new AdminFrontMemberEntities())
            {
                /**try {**/
                ObjectParameter objOutParam = new ObjectParameter("ERR_CODE", typeof(Int32));//sp의 output parameter변수명을 동일하게 사용한다.
                int sp_ret = AdminFrontMemberContext.SP_ADMIN_MEMBER_STAFF_BASE_TMP_INS(sTAFF_COMPANY, sTAFF_ID, sTAFF_NAME, wORK_TEMP_ID, aDM_ID,iP, objOutParam);
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

        //관리자 -임직원기준DB TEMP -처리결과 수정 1건
        //리턴:ERR_CODE : 0:에러없음
        public int SetAdminMemberStaffBaseTempUpdate(int iDX=-1, string aPP_RESULT= "")
        {

            int ERR_CODE = 0; //일단 SP호출 에러 없음
            using (AdminFrontMemberEntities AdminFrontMemberContext = new AdminFrontMemberEntities())
            {
                /**try {**/
                //ObjectParameter objOutParam = new ObjectParameter("ERR_CODE", typeof(Int32));//sp의 output parameter변수명을 동일하게 사용한다.
                int sp_ret = AdminFrontMemberContext.SP_ADMIN_MEMBER_STAFF_BASE_TMP_UPD(iDX, aPP_RESULT);
                //ERR_CODE = (int)objOutParam.Value;

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


        //----------------------------------------------------------------------------------------------------------
        //관리자 - 휴면계정-목록
        public List<SP_ADMIN_FRONT_MEMBER_VIEW_Result> GetAdminMemberSleepingList(string m_LASTVISITDATE = "", string sEARCH_COL = "", string sEARCH_KEYWORD = "", string sORT_COL = "M_LASTVISITDATE", string sORT_DIR = "DESC", int pAGE=1, int pAGESIZE=10)
        {

            List<SP_ADMIN_FRONT_MEMBER_VIEW_Result> lst = new List<SP_ADMIN_FRONT_MEMBER_VIEW_Result>();
            using (AdminFrontMemberEntities AdminFrontMemberContext = new AdminFrontMemberEntities())
            {
                /**try {**/
                lst = AdminFrontMemberContext.SP_ADMIN_MEMBER_SLEEPING_SEL(m_LASTVISITDATE, sEARCH_COL, sEARCH_KEYWORD, sORT_COL, sORT_DIR, pAGE, pAGESIZE).ToList();

                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;

        }

        //관리자-휴면계정-목록 COUNT
        public int GetAdminMemberSleepingCount(string m_LASTVISITDATE = "", string sEARCH_COL = "", string sEARCH_KEYWORD = "")
        {

            List<SP_ADMIN_FRONT_COMMON_CNT_Result> lst = new List<SP_ADMIN_FRONT_COMMON_CNT_Result>();
            int list_cnt = 0;

            using (AdminFrontMemberEntities AdminFrontMemberContext = new AdminFrontMemberEntities())
            {
                /**try {**/
                lst = AdminFrontMemberContext.SP_ADMIN_MEMBER_SLEEPING_CNT(m_LASTVISITDATE, sEARCH_COL, sEARCH_KEYWORD).ToList();
                if (lst != null && lst.Count > 0)
                    list_cnt = lst[0].COUNT;
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return list_cnt;
        }


        //----------------------------------------------------------------------------------------------------------
        //관리자 - 통계-회원등급별
        public List<SP_ADMIN_MEMBER_REPORT_GRADE_Result> GetAdminReportMemberGradeList()
        {

            List<SP_ADMIN_MEMBER_REPORT_GRADE_Result> lst = new List<SP_ADMIN_MEMBER_REPORT_GRADE_Result>();
            using (AdminFrontMemberEntities AdminFrontMemberContext = new AdminFrontMemberEntities())
            {
                /**try {**/
                lst = AdminFrontMemberContext.SP_ADMIN_MEMBER_REPORT_GRADE().ToList();

                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;

        }

        //관리자 - 통계-월별- 회원가입/탈퇴
        public List<SP_ADMIN_MEMBER_REPORT_MONTHLY_Result> GetAdminReportMemberMonthlyList(string yEAR = "")
        {

            List<SP_ADMIN_MEMBER_REPORT_MONTHLY_Result> lst = new List<SP_ADMIN_MEMBER_REPORT_MONTHLY_Result>();
            using (AdminFrontMemberEntities AdminFrontMemberContext = new AdminFrontMemberEntities())
            {
                /**try {**/
                lst = AdminFrontMemberContext.SP_ADMIN_MEMBER_REPORT_MONTHLY(yEAR).ToList();

                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;

        }


        //####################################################################################################################################
        //데이타 이행 :회원암호 -list  --오픈전 마이그레이션시 1회 필요
        public List<SP_ZZ_MIGRATION_MEMBER_PWD_MD5_2_SHA256_SEL_Result> GetZZ_MIGRATION_MEMBER_PWD_MD5_2_SHA256_SEL()
        {

            List<SP_ZZ_MIGRATION_MEMBER_PWD_MD5_2_SHA256_SEL_Result> lst = new List<SP_ZZ_MIGRATION_MEMBER_PWD_MD5_2_SHA256_SEL_Result>();
            using (AdminFrontMemberEntities AdminFrontMemberContext = new AdminFrontMemberEntities())
            {
                /**try {**/
                lst = AdminFrontMemberContext.SP_ZZ_MIGRATION_MEMBER_PWD_MD5_2_SHA256_SEL().ToList();

                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;
        }

        //데이타 이행 :회원암호 -수정저장  --오픈전 마이그레이션시 1회 필요
        public void SetZZ_MIGRATION_MEMBER_PWD_MD5_2_SHA256_UPD(string m_ID="", string m_PWD_SHA256="")
        {

            using (AdminFrontMemberEntities AdminFrontMemberContext = new AdminFrontMemberEntities())
            {
                /**try {**/
                int ret = AdminFrontMemberContext.SP_ZZ_MIGRATION_MEMBER_PWD_MD5_2_SHA256_UPD(m_ID, m_PWD_SHA256);

                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

        }



    } //class
} //namespace
