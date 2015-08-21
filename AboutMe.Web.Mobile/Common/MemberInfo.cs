
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AboutMe.Common.Helper;



namespace AboutMe.Web.Mobile.Common
{
    public class MemberInfo
    {

        //회원로그인 여부 : true: 로그인, false :로그인 안 함
        public static bool IsMemberLogin()
        {
            bool bRet = false; //기본 로그인 암함
            if (GetMemberId() != null)
                if (GetMemberId() != "")
                    bRet = true;
            return bRet;
        }

        //회원 계정 varchar(13)
        public static string GetMemberId()
        {
            CookieSessionStore cookiesession = new CookieSessionStore();
            string M_ID = cookiesession.GetSecretSession("M_ID");
            return M_ID;
        }

        //회원 이름 varchar(30)
        public static string GetMemberName()
        {
            CookieSessionStore cookiesession = new CookieSessionStore();
            string M_NAME = cookiesession.GetSecretSession("M_NAME");
            return M_NAME;
        }

        //회원 등급 varchar(1) (B/S/G/V) 브론즈/실버/골드/VIP
        public static string GetMemberGrade()
        {
            CookieSessionStore cookiesession = new CookieSessionStore();
            string M_GRADE = cookiesession.GetSecretSession("M_GRADE");
            return M_GRADE;
        }

        //회원 등급명
        public static string GetMemberGradeName()
        {
            string M_GRADE = GetMemberGrade();
            string M_GRADE_NAME = M_GRADE;

            switch (M_GRADE)
            {
                case "B":
                    M_GRADE_NAME = "BRONZE";
                    break;
                case "S":
                    M_GRADE_NAME = "SILVER";
                    break;
                case "G":
                    M_GRADE_NAME = "GOLD";
                    break;
                case "V":
                    M_GRADE_NAME = "VIP";
                    break;
            }


            return M_GRADE_NAME;
        }

        //회원 구분  S:임직원 , A or기타:일반회원 
        public static string GetMemberGBN()
        {
            CookieSessionStore cookiesession = new CookieSessionStore();
            string M_GBN = cookiesession.GetSecretSession("M_GBN");
            return M_GBN;
        }


        //회원 EMAIL  varchar(100)
        public static string GetMemberEmail()
        {
            CookieSessionStore cookiesession = new CookieSessionStore();
            string M_EMAIL = cookiesession.GetSecretSession("M_EMAIL");
            return M_EMAIL;
        }

        //회원 피부트러블 코드  char(9)
        public static string GetMemberSkinTroubleCD()
        {
            CookieSessionStore cookiesession = new CookieSessionStore();
            string M_SKIN_TROUBLE_CD = cookiesession.GetSecretSession("M_SKIN_TROUBLE_CD");
            return M_SKIN_TROUBLE_CD;
        }


        //비회원 주문번호
        public static string GetNomemberOrderCode()
        {
            CookieSessionStore cookiesession = new CookieSessionStore();
            string NOMEMBER_ORDER_CODE = cookiesession.GetSecretSession("NOMEMBER_ORDER_CODE");
            return NOMEMBER_ORDER_CODE;
        }

    }
}