using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.WebPages;

using WURFL;
using WURFL.Config;

using AboutMe.Web.Front.DI_Core; //Dependency Injection definition ssw

namespace AboutMe.Web.Front
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //public const String WurflManagerCacheKey = "__WurflManager";
        //public const String WurflDataFilePath = "~/App_Data/wurfl-latest.zip";
        //-----  구버전에서 쓰이던 것 public const String WurflPatchFilePath = "~/App_Data/web_browsers_patch.xml";

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Bootstrapper.Initialise(); //For Depency injection ... 

            //var mgr = WURFLManagerBuilder.Build(new ApplicationConfigurer());
            LoadWurflData(Context); //Device 감지를 위한 기능 


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

        protected void Application_Error(object sender, EventArgs e)
        {

            Common.AppErrorLog AppErr = new Common.AppErrorLog();
            AppErr.HandlerInsertAppErrLog(sender, e);
           
            /**
            Exception exception = Server.GetLastError();
            //Response.Clear();

            HttpException httpException = exception as HttpException;

            if (httpException != null)
            {
                string action;

                switch (httpException.GetHttpCode())
                {
                    case 404:
                        // page not found
                        action = "AboutMeHttpError404";
                        break;
                    case 500:
                        // server error
                        Common.AppErrorLog AppErr =  new Common.AppErrorLog();
                        AppErr.HandlerInsertAppErrLog(sender, e);
                        action = "AboutMeHttpError500";

                        break;
                    default:
                        action = "AboutMeHttpError500";
                        break;
                }

                // clear error on server
                Server.ClearError();

                Response.Redirect(String.Format("~/Error/{0}/?message={1}", action, exception.Message));
            }
             * **/

            Response.Redirect("/CustomError/Err500");
             

        }

        public static void LoadWurflData(HttpContext context)
        {
            var server = context.Server;
            var wurflDataFile = server.MapPath(AboutMe.Common.Helper.Config.GetConfigValue("WurflDataFilePath"));
            //var wurflPatchFile = server.MapPath(WurflPatchFilePath);

            var configurer = new InMemoryConfigurer()
                .MainFile(wurflDataFile);
               // .PatchFile(wurflPatchFile);

            var manager = WURFLManagerBuilder.Build(configurer);

            context.Cache[AboutMe.Common.Helper.Config.GetConfigValue("WurflManagerCacheKey")] = manager;

       }

    } //class


}//namespace
