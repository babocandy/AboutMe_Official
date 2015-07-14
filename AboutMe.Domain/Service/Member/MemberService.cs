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
                    strERR_MSG = "DB 처리 오류.ERR_CODE:" + nERR_CODE.ToString();

                if (nERR_CODE == 10)
                    strERR_MSG = "계정 중복 확인. 이미 존재하는 ID입니다.";


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

        //회원 가입:등록
        //리턴:ReturnDic
        public ReturnDic SetMemberRegister(string m_ID = "", string m_NAME = "", string m_PWD = "", string m_GRADE = "", string m_SEX = "", string m_BIRTHDAY = "", string m_MOBILE = "", string m_PHONE = "", string m_EMAIL = "", string m_ZIPCODE = "", string m_ADDR1 = "", string m_ADDR2 = "", string m_ISSMS = "", string m_ISEMAIL = "", string m_ISDM = ""
            , string m_JOIN_MODE = "", string m_DI = "", string m_AGREE = "", string m_AGREE2 = "", string m_SKIN_TROUBLE_CD = ""
            , string m_GBN = "", string m_STAFF_COMPANY = "", string m_STAFF_ID = "")
        {

            int nERR_CODE = 0; //에러 없음
            string strERR_MSG = ""; //에러 없음
            using (MemberEntities MemberContext = new MemberEntities())
            {
                /**try {**/
                ObjectParameter objOutParam = new ObjectParameter("ERR_CODE", typeof(Int32));//sp의 output parameter변수명을 동일하게 사용한다.
                int sp_ret = MemberContext.SP_MEMBER_INS(m_ID, m_NAME, m_PWD, m_GRADE, m_SEX, m_BIRTHDAY, m_MOBILE, m_PHONE, m_EMAIL, m_ZIPCODE, m_ADDR1, m_ADDR2, m_ISSMS, m_ISEMAIL, m_ISDM
                                                                , m_JOIN_MODE, m_DI, m_AGREE, m_AGREE2, m_SKIN_TROUBLE_CD
                                                                , m_GBN, m_STAFF_COMPANY, m_STAFF_ID
                                                                , objOutParam);
                nERR_CODE = (int)objOutParam.Value;

                if (nERR_CODE !=0)
                    strERR_MSG = "DB 처리 오류.ERR_CODE:" + nERR_CODE.ToString() ;

                if (nERR_CODE == 10)
                    strERR_MSG = "회원가입오류. 이미 존재하는 ID입니다.";


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

    } //class
} //namespace
