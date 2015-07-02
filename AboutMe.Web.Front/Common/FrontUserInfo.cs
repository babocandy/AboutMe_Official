using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AboutMe.Common.Helper;

namespace AboutMe.Web.Front.Common
{
    public class FrontUserInfo
    {

        //회원 계정 varchar(13)
        public static string GetFrontUserId()
        {
            CookieSessionStore cookiesession = new CookieSessionStore();
            string M_ID = cookiesession.GetSecretSession("M_ID");
            return M_ID; 
        }

        //회원 이름 varchar(30)
        public static string GetFrontUserName()
        {
            CookieSessionStore cookiesession = new CookieSessionStore();
            string M_NAME = cookiesession.GetSecretSession("M_NAME");
            return M_NAME; 
        }

        //회원 등급 varchar(1) (B/S/G/V) 브론즈/실버/골드/VIP
        public static string GetFrontUserGrade()
        {
            CookieSessionStore cookiesession = new CookieSessionStore();
            string M_GRADE = cookiesession.GetSecretSession("M_GRADE");
            return M_GRADE; 
        }

        //회원 구분  S:임직원 , A or기타:일반회원 
        public static string GetFrontUserGBN()
        {
            CookieSessionStore cookiesession = new CookieSessionStore();
            string M_GBN = cookiesession.GetSecretSession("M_GBN");
            return M_GBN;
        }


        //회원 EMAIL  varchar(100)
        public static string GetFrontUserEmail()
        {
            CookieSessionStore cookiesession = new CookieSessionStore();
            string M_EMAIL = cookiesession.GetSecretSession("M_EMAIL");
            return M_EMAIL; 
        }
         
      

    }
}