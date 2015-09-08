using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using AboutMe.Web.Mobile.DI_Core; //Dependency Injection definition hsw

using WURFL;
using WURFL.Config;


namespace AboutMe.Web.Mobile
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //public const String WurflManagerCacheKey = "__WurflManager";
        //구버전에서만 쓰임 public const String WurflDataFilePath = "~/App_Data/wurfl-latest.zip";

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Bootstrapper.Initialise(); //Depency injection ... 

            LoadWurflData(Context); //Device 감지를 위한 기능
        }


        protected void Session_Start(object sender, EventArgs e)
        {
            // event is raised each time a new session is created   
            //더미세션 세팅:
            HttpContext.Current.Session["dummy_session"] = HttpContext.Current.Session.SessionID;

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

        protected void Application_Error(object sender, EventArgs e)
        {

            if (AboutMe.Common.Helper.Config.GetConfigValue("CustomErrorHandlerOnOff") == "ON")
            {
                Common.AppErrorLog AppErr = new Common.AppErrorLog();
                AppErr.HandlerInsertAppErrLog(sender, e);
                //Response.Redirect("/CustomError/Err500");
            } 


        }

    }
}
