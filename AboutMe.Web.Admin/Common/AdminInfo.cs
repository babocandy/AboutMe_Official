using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AboutMe.Common.Helper;

namespace AboutMe.Web.Admin.Common
{
    public class AdminInfo   //관리자 정보
    {
        private string ADM_ID ;  
        private string ADM_NAME ;
        private string ADM_GRADE ; //S,A,''
        private string ADM_EMAIL;
        private string ADM_PHONE;

        public string getADM_ID()  //관리자 로그인 계정
        {
            CookieSessionStore cookiesession = new CookieSessionStore();
            this.ADM_ID = cookiesession.GetSecretSession("ADM_ID");
            if (string.IsNullOrEmpty(this.ADM_ID) == true)
                this.ADM_ID = "";
            return this.ADM_ID;
        }

        public string getADM_NAME() //관리자 로그인 이름
        {
            CookieSessionStore cookiesession = new CookieSessionStore();
            this.ADM_NAME = cookiesession.GetSecretSession("ADM_NAME");
            if (string.IsNullOrEmpty(this.ADM_NAME) == true)
                this.ADM_NAME = "";
            return this.ADM_NAME;
        }

        public string getADM_GRADE() ////관리자 로그인 등급
        {
            CookieSessionStore cookiesession = new CookieSessionStore();
            this.ADM_GRADE = cookiesession.GetSecretSession("ADM_GRADE");
            if (string.IsNullOrEmpty(this.ADM_GRADE) == true)
                this.ADM_GRADE = "";
            return this.ADM_GRADE;
        }

        public string getADM_EMAIL() //관리자 로그인 EAMIL
        {
            CookieSessionStore cookiesession = new CookieSessionStore();
            this.ADM_EMAIL = cookiesession.GetSecretSession("ADM_EMAIL");
            if (string.IsNullOrEmpty(this.ADM_EMAIL) == true)
                this.ADM_EMAIL = "";
            return this.ADM_EMAIL;
        }

        public string getADM_PHONE() //관리자 로그인 전화번호
        {
            CookieSessionStore cookiesession = new CookieSessionStore();
            this.ADM_PHONE = cookiesession.GetSecretSession("ADM_PHONE");
            if (string.IsNullOrEmpty(this.ADM_PHONE) == true)
                this.ADM_PHONE = "";
            return this.ADM_PHONE;
        }


    }
}