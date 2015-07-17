using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


using AboutMe.Web.Front.DI_Core; //Dependency Injection definition ssw

namespace AboutMe.Web.Front
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Bootstrapper.Initialise(); //Depency injection ... 
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            // event is raised each time a new session is created   
            //더미세션 세팅:
            HttpContext.Current.Session["dummy_session"] = HttpContext.Current.Session.SessionID;
 
        }

        protected void Session_End(object sender, EventArgs e)
        {
            // event is raised when a session is abandoned or expires
        }
    } //class


}//namespace
