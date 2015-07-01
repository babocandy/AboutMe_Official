using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Security.Principal;
using System.Web.Mvc;

namespace AboutMe.Web.Admin.Common.Filters
{
    public enum UserType
    {
        Admin = 1,
        Staff = 2,
        User = 3,
    }


    public class CustomAuthorizeOtherAttribute : AuthorizeAttribute
    {
        public new UserType Roles;  // new keyword will hide base class Roles Property
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            if (null == httpContext.Session["SessionString_UserType"]
                || "" == httpContext.Session["SessionString_UserType"])
            {
                return false;
            }

            UserType role
                = (UserType)Convert.ToInt32(httpContext.Session["SessionString_UserType"]);

            // you could get User role or user type from session.

            if (Roles != 0 && ((Roles & role) != role))
            {
                return false;
            }
            return true;
        }
    }
}