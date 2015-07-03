using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using AboutMe.Web.Admin.DI_Core; //Dependency Injection definition ssw

namespace AboutMe.Web.Admin
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

        protected void Application_Error(object sender, EventArgs e)
        {
            /**
            Exception exception = Server.GetLastError();
            Response.Clear();

            HttpException httpException = exception as HttpException;

            if (httpException != null)
            {
                string action;

                switch (httpException.GetHttpCode())
                {
                    case 404:
                        // page not found
                        action = "SongsunwooHttpError404";
                        break;
                    case 500:
                        // server error
                        action = "SongsunwooHttpError500";
                        break;
                    default:
                        action = "SongsunwooGeneral";
                        break;
                }

                // clear error on server
                Server.ClearError();

                Response.Redirect(String.Format("~/Error/{0}/?message={1}", action, exception.Message));
            
            }
            ***/
        }
    }
}
