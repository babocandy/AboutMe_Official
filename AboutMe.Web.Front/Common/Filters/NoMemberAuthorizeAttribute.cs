using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Security.Principal;
using System.Web.Mvc;

using Microsoft.Practices.Unity;
using System.Web.Routing;



namespace AboutMe.Web.Front.Common.Filters
{

    public class NoMemberAuthorizeAttribute : AuthorizeAttribute
    {
        public string NoMemberOrderCode { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            string UserId = MemberInfo.GetMemberId();
            string OrderId = MemberInfo.GetNomemberOrderCode();
            if (!string.IsNullOrEmpty(UserId))
            {
                return true;
            }
            else 
            {
                if (!string.IsNullOrEmpty(OrderId))
                {
                    return true;
                }
            }
            return false;
        }


        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                /**
                filterContext.Result = new RedirectToRouteResult(new
                RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
                 * **/
                Uri RedirTo = new Uri(HttpContext.Current.Request.Url.ToString());

                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "MemberShip", action = "Login", RedirectUrl = RedirTo }));
                
            }
        }


    }
}