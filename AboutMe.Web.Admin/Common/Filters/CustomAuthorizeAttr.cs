﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Security.Principal;
using System.Web.Mvc;

using Microsoft.Practices.Unity;
using System.Web.Routing;



namespace AboutMe.Web.Admin.Common.Filters
{
    /**
    public class FilterProvider : IFilterProvider
    {
        private IUnityContainer container;

        public FilterProvider(IUnityContainer container)
        {
            this.container = container;
        }

        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            foreach (IActionFilter actionFilter in this.container.ResolveAll<IActionFilter>())
            {
                yield return new Filter(actionFilter, FilterScope.First, null);
            }
        }
    }
     * **/


    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
      
          private string _rolesDept1;
          private string[] _rolesDept1Split = new string[0];

          private string _rolesDept2;
          private string[] _rolesDept2Split = new string[0];

          public string user_ADM_ID { get; set; }
          public string user_ADM_Role { get; set; }
          public string user_ADM_Role2 { get; set; }


          public string Roles // new keyword will hide base class Roles Property ( 이 프로퍼티이름은 변경하면 안됨)
          {
               get { return _rolesDept1 ?? String.Empty; }
               set
               {
                    _rolesDept1 = value;
                    _rolesDept1Split = AttSplitString(value);
                }
           }

          public string Roles2
          {
              get { return _rolesDept2 ?? String.Empty; }
              set
              {
                  _rolesDept2 = value;
                  _rolesDept2Split = AttSplitString(value);
              }
          }

  
        
        private string[] AttSplitString(string original)
        {
            if (String.IsNullOrEmpty(original))
           {
               return new string[0];
            }

            var split = from piece in original.Split(',')
                         let trimmed = piece.Trim()
                         where !String.IsNullOrEmpty(trimmed)
                        select trimmed;
            return split.ToArray();
         }
        

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {


            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }


            //UserType role  = (UserType)Convert.ToInt32(httpContext.Session[mvc_SessionAuthorize.claGlobal.SessionString_UserType]);

           
            string AdmId = AdminUserInfo.GetAdmId();
            string AdmGrade = AdminUserInfo.GetAdmGrade(); 
           

            // you could get User role or user type from session.
            //&& !_usersDept1Split.Contains(user.Identity.Name,  StringComparer.OrdinalIgnoreCase)
            if (AdmId == "" || AdmId == null)
            {
                return false;
            }


            if (_rolesDept1Split.Length > 0 && !_rolesDept1Split.Contains(AdmGrade, StringComparer.OrdinalIgnoreCase))
            {
                return false;
            }


            if (_rolesDept2Split.Length > 0 && !_rolesDept2Split.Contains(AdmGrade, StringComparer.OrdinalIgnoreCase))
            {
                return false;
            }
     

                       
            return true;
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
             
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "AdminUser", action = "Login", RedirectUrl = HttpContext.Current.Request.Url }));
                
            }
        }


    }
}