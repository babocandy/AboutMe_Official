using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.Member;
using AboutMe.Domain.Entity.Common;

using System.Data.Entity.Core.Objects;

//using System.Web;
//using System.Web.Mvc;
using System.Json;
 
//using System.Net.Json;


namespace AboutMe.Domain.Service.Member
{
    public class MemberService : IMemberService
    {

        //회원 상세 1명
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
        //회원 가입:ID 중복체크
        //리턴:ReturnDic
        public ReturnDic GetMemberID_Dup_Check(string m_ID = "")
        {

            int nERR_CODE = 0; //에러 없음
            string strERR_MSG = ""; //에러 없음
            using (MemberEntities MemberContext = new MemberEntities())
            {
                /**try {**/
                ObjectParameter objOutParam = new ObjectParameter("ERR_CODE", typeof(Int32));//sp의 output parameter변수명을 동일하게 사용한다.
                int sp_ret = MemberContext.SP_MEMBER_DUP_CHK(m_ID, objOutParam);
                nERR_CODE = (int)objOutParam.Value;

                if (nERR_CODE != 0)
                    strERR_MSG = "오류! DB 처리 오류. ERR_CODE:" + nERR_CODE.ToString();

                if (nERR_CODE == 10)
                    strERR_MSG = "이미 존재하는 ID입니다. 다른 계정을 사용 하십시오.";


                /** }catch()
                 {
                       MemberContext.Dispose();
                 }**/
            }

            //결과 리턴
            ReturnDic rObj = new ReturnDic();
            rObj.ERR_CODE = nERR_CODE.ToString();
            rObj.ERR_MSG = strERR_MSG;

            return rObj; 
        }

        //회원 가입:실명인증 로그등록
        public void SetRealNameLogInsert(string wORK_TMP_ID = "", string m_JOIN_TYPE = "", string rESULT_CODE = "", string tRAN_NO = "", string m_NAME = "", string di = "", string ci = "", string m_BIRTHDAY = "", string m_SEX = "", string nation = "", string rETURN_MSG_ALL = "", string iP = "")
        {

            using (MemberEntities MemberContext = new MemberEntities())
            {
                /**try {**/
                int sp_ret = MemberContext.SP_MEMBER_REALNAME_LOG_INS(wORK_TMP_ID, m_JOIN_TYPE, rESULT_CODE, tRAN_NO, m_NAME, di, ci, m_BIRTHDAY, m_SEX, nation, rETURN_MSG_ALL,iP);

                /** }catch()
                 {
                       MemberContext.Dispose();
                 }**/
            }
        }

        //회원 가입:실명인증 로그조회 1건(1시간내)
        public SP_MEMBER_REALNAME_LOG_VIEW_Result GetRealNameLogWorkTmp(string wORK_TMP_ID = "")
        {

            SP_MEMBER_REALNAME_LOG_VIEW_Result row1 = new SP_MEMBER_REALNAME_LOG_VIEW_Result();
            using (MemberEntities MemberContext = new MemberEntities())
            {
                /**try {**/
                row1 = MemberContext.SP_MEMBER_REALNAME_LOG_VIEW(wORK_TMP_ID).FirstOrDefault();

                /** }catch()
                 {
                       MemberContext.Dispose();
                 }**/
            }
            return row1;
        }

        //회원 가입:실명인증 DI  사용여부확인 <<-- 동일인 중복가입방지
        //리턴:ReturnDic
        public ReturnDic  GetMemberFindDI(string m_DI="")
        {
            int nERR_CODE = 0; //에러 없음
            string strERR_MSG = ""; //에러 없음
            string strRET_M_ID = ""; //DI로 중복되는 계정
            using (MemberEntities MemberContext = new MemberEntities())
            {
                /**try {**/
                ObjectParameter objOutParam1 = new ObjectParameter("ERR_CODE", typeof(Int32));//sp의 output parameter변수명을 동일하게 사용한다.
                ObjectParameter objOutParam2 = new ObjectParameter("RET_M_ID", typeof(string));//sp의 output parameter변수명을 동일하게 사용한다.
                int sp_ret = MemberContext.SP_MEMBER_FIND_DI(m_DI, objOutParam1, objOutParam2);
                nERR_CODE = (int)objOutParam1.Value;
                strRET_M_ID = (string)objOutParam2.Value;

                if (nERR_CODE != 0)
                    strERR_MSG = "오류! DB 처리 오류.ERR_CODE:" + nERR_CODE.ToString();

                if (nERR_CODE == 10)
                    strERR_MSG = "오류! 이미 가입하신 회원 입니다. 아이디/암호찾기를 활용 하십시오.";
                if (nERR_CODE == 20)
                    strERR_MSG = "오류! 파라메타 DI값 전달오류.";


                /** }catch()
                 {
                       MemberContext.Dispose();
                 }**/
            }

            //결과 리턴
            ReturnDic rObj = new ReturnDic();
            rObj.ERR_CODE = nERR_CODE.ToString();
            rObj.ERR_MSG = strERR_MSG;
            rObj.ETC1 = strRET_M_ID; //DI로 중복되는 계정

            return rObj; 
        }

        //회원 신규가입:등록
        //리턴:ReturnDic
        public ReturnDic SetMemberRegister(string m_ID = "", string m_NAME = "", string m_PWD = "", string m_GRADE = "", string m_SEX = "", string m_BIRTHDAY = "", string m_MOBILE = "", string m_PHONE = "", string m_EMAIL = "", string m_ZIPCODE = "", string m_ADDR1 = "", string m_ADDR2 = "", string m_ISSMS = "", string m_ISEMAIL = "", string m_ISDM = ""
            , string m_JOIN_MODE = "", string m_DI = "", string m_AGREE = "", string m_AGREE2 = "", string m_SKIN_TROUBLE_CD = ""
            , string m_GBN = "", string m_STAFF_COMPANY = "", string m_STAFF_ID = "",string m_DEVICE_GBN="W")
        {

            int nERR_CODE = 0; //에러 없음
            string strERR_MSG = ""; //에러 없음
            using (MemberEntities MemberContext = new MemberEntities())
            {
                /**try {**/
                ObjectParameter objOutParam = new ObjectParameter("ERR_CODE", typeof(Int32));//sp의 output parameter변수명을 동일하게 사용한다.
                int sp_ret = MemberContext.SP_MEMBER_INS(m_ID, m_NAME, m_PWD, m_GRADE, m_SEX, m_BIRTHDAY, m_MOBILE, m_PHONE, m_EMAIL, m_ZIPCODE, m_ADDR1, m_ADDR2, m_ISSMS, m_ISEMAIL, m_ISDM
                                                                , m_JOIN_MODE, m_DI, m_AGREE, m_AGREE2, m_SKIN_TROUBLE_CD
                                                                , m_GBN, m_STAFF_COMPANY, m_STAFF_ID, m_DEVICE_GBN
                                                                , objOutParam);
                nERR_CODE = (int)objOutParam.Value;

                if (nERR_CODE !=0)
                    strERR_MSG = "오류! DB 처리 오류. ERR_CODE:" + nERR_CODE.ToString();

                if (nERR_CODE == 10)
                    strERR_MSG = "오류! 이미 존재하는 ID입니다.";
                if (nERR_CODE == 11)
                    strERR_MSG = "오류! 이미 존재하는 메일계정 입니다.";


                /** }catch()
                 {
                       MemberContext.Dispose();
                 }**/
            }

            //결과 리턴

            ReturnDic rObj = new ReturnDic();
            rObj.ERR_CODE=nERR_CODE.ToString();
            rObj.ERR_MSG = strERR_MSG;

            return rObj; 
        }

        //회원 ID찾기
        //리턴:ReturnDic
        public ReturnDic GetMemberFindID(string m_NAME = "", string m_EMAIL = "", string m_MOBILE = "")
        {

            int nERR_CODE = 0; //에러 없음
            string strERR_MSG = ""; //에러 없음
            string strRET_M_ID = ""; //찾아진 계정
            string strRET_M_NAME = ""; //찾아진 이름
            string strRET_M_CREDATE; //찾아진 가입일
            using (MemberEntities MemberContext = new MemberEntities())
            {
                /**try {**/
                ObjectParameter objOutParam = new ObjectParameter("ERR_CODE", typeof(Int32));//sp의 output parameter변수명을 동일하게 사용한다.
                ObjectParameter objOutParam2 = new ObjectParameter("RET_M_ID", typeof(string));//sp의 output parameter변수명을 동일하게 사용한다.
                ObjectParameter objOutParam3 = new ObjectParameter("RET_M_NAME", typeof(string));//sp의 output parameter변수명을 동일하게 사용한다.
                ObjectParameter objOutParam4 = new ObjectParameter("RET_M_CREDATE", typeof(string));//sp의 output parameter변수명을 동일하게 사용한다.

                int sp_ret = MemberContext.SP_MEMBER_FIND_ID(m_NAME, m_EMAIL, m_MOBILE, objOutParam, objOutParam2, objOutParam3, objOutParam4);
                nERR_CODE = (int)objOutParam.Value;
                strRET_M_ID = (string)objOutParam2.Value;
                strRET_M_NAME = (string)objOutParam3.Value;
                strRET_M_CREDATE = (string)objOutParam4.Value;

                if (nERR_CODE != 0)
                    strERR_MSG = "오류! 아이디찾기 DB 처리 오류. ERR_CODE:" + nERR_CODE.ToString();

                if (nERR_CODE == 10)
                    strERR_MSG = "오류! 회원계정을 찾을수 없습니다.";
                if (nERR_CODE == 20)
                    strERR_MSG = "오류! 파라메타 전달 오류.";


                /** }catch()
                 {
                       MemberContext.Dispose();
                 }**/
            }

            //결과 리턴
            ReturnDic rObj = new ReturnDic();
            rObj.ERR_CODE = nERR_CODE.ToString();
            rObj.ERR_MSG = strERR_MSG;
            rObj.ETC1 = strRET_M_ID; //찾은 계정 : ERR_CODE=="0"일때만 유효
            rObj.ETC2 = strRET_M_NAME; //찾은 이름 : ERR_CODE=="0"일때만 유효
            rObj.ETC3 = strRET_M_CREDATE; //찾은 가입일 : ERR_CODE=="0"일때만 유효
            

            return rObj;
        }

        //회원 비밀번호찾기
        //리턴:ReturnDic
        public ReturnDic GetMemberFindPWD(string m_ID = "", string m_NAME = "", string m_EMAIL = "", string m_MOBILE = "", string m_PWD_NEW = "")
        {

            int nERR_CODE = 0; //에러 없음
            string strERR_MSG = ""; //에러 없음
            string strRET_M_NAME = ""; //에러 없음
            string strRET_M_EMAIL = ""; //에러 없음
            using (MemberEntities MemberContext = new MemberEntities())
            {
                /**try {**/
                ObjectParameter objOutParam = new ObjectParameter("ERR_CODE", typeof(Int32));//sp의 output parameter변수명을 동일하게 사용한다.
                ObjectParameter objOutParam2 = new ObjectParameter("RET_M_NAME", typeof(string));//sp의 output parameter변수명을 동일하게 사용한다.
                ObjectParameter objOutParam3 = new ObjectParameter("RET_M_EMAIL", typeof(string));//sp의 output parameter변수명을 동일하게 사용한다.
                int sp_ret = MemberContext.SP_MEMBER_FIND_PWD(m_ID, m_NAME, m_EMAIL, m_MOBILE, m_PWD_NEW, objOutParam, objOutParam2, objOutParam3);
                nERR_CODE = (int)objOutParam.Value;
                strRET_M_NAME = (string)objOutParam2.Value;
                strRET_M_EMAIL = (string)objOutParam3.Value;

                if (nERR_CODE != 0)
                    strERR_MSG = "오류! 비밀번호찾기 DB 처리 오류. ERR_CODE:" + nERR_CODE.ToString();

                if (nERR_CODE == 10)
                    strERR_MSG = "오류! 회원계정을 찾을수 없습니다.";
                if (nERR_CODE == 20)
                    strERR_MSG = "오류! 파라메타 전달 오류.";

                /** }catch()
                 {
                       MemberContext.Dispose();
                 }**/
            }

            //결과 리턴
            ReturnDic rObj = new ReturnDic();
            rObj.ERR_CODE = nERR_CODE.ToString();
            rObj.ERR_MSG = strERR_MSG;
            rObj.ETC1 = strRET_M_NAME; //찾은 이름 : ERR_CODE=="0"일때만 유효
            rObj.ETC2 = strRET_M_EMAIL; //찾은 EMAIL : ERR_CODE=="0"일때만 유효


            return rObj;
        }

        //회원 탈퇴처리
        //리턴:ReturnDic
        public ReturnDic SetMemberRetire(string m_ID="", string m_IS_RETIRE="Y", string m_DEL_REASON="")
        {

            int nERR_CODE = 0; //에러 없음
            string strERR_MSG = ""; //에러 없음
            using (MemberEntities MemberContext = new MemberEntities())
            {
                /**try {**/
                ObjectParameter objOutParam = new ObjectParameter("ERR_CODE", typeof(Int32));//sp의 output parameter변수명을 동일하게 사용한다.
                int sp_ret = MemberContext.SP_MEMBER_UPD_RETIRE(m_ID, "Y", m_DEL_REASON, objOutParam);
                nERR_CODE = (int)objOutParam.Value;

                if (nERR_CODE != 0)
                    strERR_MSG = "오류! 탈퇴할 회원 찾기 DB 처리 오류. ERR_CODE:" + nERR_CODE.ToString();

                if (nERR_CODE == 10)
                    strERR_MSG = "오류! 회원계정을 찾을수 없습니다.";

                /** }catch()
                 {
                       MemberContext.Dispose();
                 }**/
            }

            //결과 리턴
            ReturnDic rObj = new ReturnDic();
            rObj.ERR_CODE = nERR_CODE.ToString();
            rObj.ERR_MSG = strERR_MSG;

            return rObj;
        }


        //회원 정보수정
        //리턴:ReturnDic
        public ReturnDic SetMemberUpdate(string m_ID="", string m_MOBILE="--", string m_PHONE="--", string m_EMAIL="@", string m_ZIPCODE="", string m_ADDR1="", string m_ADDR2="", string m_ISSMS="N", string m_ISEMAIL="N", string m_ISDM="N")
        {

            int nERR_CODE = 0; //에러 없음
            string strERR_MSG = ""; //에러 없음
            using (MemberEntities MemberContext = new MemberEntities())
            {
                /**try {**/
                ObjectParameter objOutParam = new ObjectParameter("ERR_CODE", typeof(Int32));//sp의 output parameter변수명을 동일하게 사용한다.
                int sp_ret = MemberContext.SP_MEMBER_UPD(m_ID,  m_MOBILE,  m_PHONE,  m_EMAIL,  m_ZIPCODE,  m_ADDR1,  m_ADDR2,  m_ISSMS,  m_ISEMAIL,  m_ISDM, objOutParam);
                nERR_CODE = (int)objOutParam.Value;

                if (nERR_CODE != 0)
                    strERR_MSG = "오류! DB 처리 오류. ERR_CODE:" + nERR_CODE.ToString();

                if (nERR_CODE == 10)
                    strERR_MSG = "오류! 회원계정을 찾을수 없습니다.";
                if (nERR_CODE == 11)
                    strERR_MSG = "오류! 이미 사용중인 메일계정입니다.";

                /** }catch()
                 {
                       MemberContext.Dispose();
                 }**/
            }

            //결과 리턴
            ReturnDic rObj = new ReturnDic();
            rObj.ERR_CODE = nERR_CODE.ToString();
            rObj.ERR_MSG = strERR_MSG;

            return rObj;
        }

        //회원 비밀번호변경
        //리턴:ReturnDic
        public ReturnDic SetMemberPWDChange(string m_ID="", string m_PWD_OLD="", string m_PWD_NEW="")
        {

            int nERR_CODE = 0; //에러 없음
            string strERR_MSG = ""; //에러 없음

            using (MemberEntities MemberContext = new MemberEntities())
            {
                /**try {**/
                ObjectParameter objOutParam = new ObjectParameter("ERR_CODE", typeof(Int32));//sp의 output parameter변수명을 동일하게 사용한다.
                int sp_ret = MemberContext.SP_MEMBER_UPD_PWD(m_ID, m_PWD_OLD, m_PWD_NEW, objOutParam);
                nERR_CODE = (int)objOutParam.Value;

                if (nERR_CODE != 0)
                    strERR_MSG = "오류! DB 처리 오류. ERR_CODE:" + nERR_CODE.ToString();

                if (nERR_CODE == 10)
                    strERR_MSG = "오류! 회원계정을 찾을수 없습니다.";

                if (nERR_CODE == 20)
                    strERR_MSG = "오류! 기존 비밀번호가 일치하지 않습니다.";

                /** }catch()
                 {
                       MemberContext.Dispose();
                 }**/
            }

            //결과 리턴
            ReturnDic rObj = new ReturnDic();
            rObj.ERR_CODE = nERR_CODE.ToString();
            rObj.ERR_MSG = strERR_MSG;

            return rObj;
        }

        //회원 피부트러블 변경
        //리턴:ReturnDic
        public ReturnDic SetMemberSkinTroubleUpdate(string m_ID = "", string m_SKIN_TROUBLE_CD = "")
        {

            int nERR_CODE = 0; //에러 없음
            string strERR_MSG = ""; //에러 없음

            using (MemberEntities MemberContext = new MemberEntities())
            {
                /**try {**/
                ObjectParameter objOutParam = new ObjectParameter("ERR_CODE", typeof(Int32));//sp의 output parameter변수명을 동일하게 사용한다.
                int sp_ret = MemberContext.SP_MEMBER_UPD_SKIN_TROUBLE(m_ID, m_SKIN_TROUBLE_CD, objOutParam);
                nERR_CODE = (int)objOutParam.Value;

                if (nERR_CODE != 0)
                    strERR_MSG = "오류! DB 처리 오류. ERR_CODE:" + nERR_CODE.ToString();

                if (nERR_CODE == 10)
                    strERR_MSG = "오류! 회원계정을 찾을수 없습니다.";

                /** }catch()
                 {
                       MemberContext.Dispose();
                 }**/
            }

            //결과 리턴
            ReturnDic rObj = new ReturnDic();
            rObj.ERR_CODE = nERR_CODE.ToString();
            rObj.ERR_MSG = strERR_MSG;

            return rObj;
        }


        //회원 임직원 신청 등록
        //리턴:ReturnDic
        public ReturnDic SetMemberStaffRequestInert(string m_ID = "", string m_NAME = "", string m_GRADE = "", string sTAFF_COMPANY = "", string sTAFF_ID = "", string sTAFF_NAME = "")
        {

            int nERR_CODE = 0; //에러 없음
            string strERR_MSG = ""; //에러 없음

            using (MemberEntities MemberContext = new MemberEntities())
            {
                /**try {**/
                ObjectParameter objOutParam = new ObjectParameter("ERR_CODE", typeof(Int32));//sp의 output parameter변수명을 동일하게 사용한다.
                int sp_ret = MemberContext.SP_MEMBER_STAFF_REQUEST_INS(m_ID, m_NAME, m_GRADE, sTAFF_COMPANY, sTAFF_ID, sTAFF_NAME, objOutParam);
                nERR_CODE = (int)objOutParam.Value;

                if (nERR_CODE != 0)
                    strERR_MSG = "오류! 임직원 신청 등록 DB 처리 오류. ERR_CODE:" + nERR_CODE.ToString();


                /** }catch()
                 {
                       MemberContext.Dispose();
                 }**/
            }

            //결과 리턴
            ReturnDic rObj = new ReturnDic();
            rObj.ERR_CODE = nERR_CODE.ToString();
            rObj.ERR_MSG = strERR_MSG;

            return rObj;
        }

    } //class
} //namespace
