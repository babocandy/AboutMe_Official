using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AboutMe.Common.Helper;

namespace AboutMe.Web.Admin.Common
{
    public class AdminUserInfo
    {
  

        public static string GetAdmId()
        {
            CookieSessionStore cookiesession = new CookieSessionStore();
            string ADM_ID = cookiesession.GetSecretSession("ADM_ID");
            return ADM_ID ; 
        }

        public static string GetAdmName()
        {
            CookieSessionStore cookiesession = new CookieSessionStore();
            string ADM_NAME = cookiesession.GetSecretSession("ADM_NAME");
            return ADM_NAME ; 
        }

        public static string GetAdmGrade()
        {
            CookieSessionStore cookiesession = new CookieSessionStore();
            string ADM_GRADE = cookiesession.GetSecretSession("ADM_GRADE");
            return ADM_GRADE ; 
        }

            
        public static string GetAdmPhone()
        {
            CookieSessionStore cookiesession = new CookieSessionStore();
            string ADM_PHONE = cookiesession.GetSecretSession("ADM_PHONE");
            return ADM_PHONE ; 
        }

        public static string GetAdmEmail()
        {
            CookieSessionStore cookiesession = new CookieSessionStore();
            string ADM_EMAIL = cookiesession.GetSecretSession("ADM_EMAIL");
            return ADM_EMAIL ; 
        }
         
      

    }
}