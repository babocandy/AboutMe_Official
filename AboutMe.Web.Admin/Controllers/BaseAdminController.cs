﻿using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
using System.Web.Mvc;
using AboutMe.Common.Helper;
using System.Reflection;
using System.Web.Routing;

//using AboutMe.Web.Admin.Common.Filters;

namespace AboutMe.Web.Admin.Controllers
{
    public class BaseAdminController : Controller
    {
        


       protected override void Initialize(System.Web.Routing.RequestContext requestContext)
       {
          
         
          base.Initialize(requestContext);
      
         // MyInitialzie()
        

       }

       public RouteValueDictionary ConvertRouteValue(object Obj)
       {
           RouteValueDictionary param = new RouteValueDictionary();
           Type type = Obj.GetType();
           PropertyInfo[] properties = type.GetProperties();

           foreach (PropertyInfo property in properties)
           {
               param.Add(property.Name, property.GetValue(Obj));
           }
           return param;
       }  
      
    }
}