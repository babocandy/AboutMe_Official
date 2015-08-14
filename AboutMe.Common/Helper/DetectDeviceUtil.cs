using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;

using WURFL;



namespace AboutMe.Common.Helper
{
    public static class DetectDeviceUtil
    {

        public static string GetUserDeviceType()
        {
            string WurfMngCacheKey = AboutMe.Common.Helper.Config.GetConfigValue("WurflManagerCacheKey");
            var wurflManager = HttpContext.Current.Cache[WurfMngCacheKey] as IWURFLManager;
            var userAgent = HttpContext.Current.Request.UserAgent;

            IDevice device;

            if(wurflManager == null) //global.asa에서 WURFL매니져를 캐싱하고 있다.. 만약 캐싱이 안되었다면 새로 creat하고
            {
               device = WURFLManagerBuilder.Instance.GetDeviceForRequest(userAgent);
            }
            else //캐싱이 잘 되어있으면 캐싱된 매니저를 사용
            {
                device = wurflManager.GetDeviceForRequest(userAgent);
            }
           

      
            var rtn = "";


            if(device.GetVirtualCapability("is_mobile") == "true")
            {
                rtn = "m";
            }


            if (device.GetVirtualCapability("is_tablet") == "true")
            {
                rtn = "t";
            }

            if (device.GetVirtualCapability("is_smartphone") == "true")
            {
                rtn = "s";
            }


            if (device.GetVirtualCapability("is_full_desktop") == "true")
            {
                rtn = "d";
            }
                /**
              return device.IsWireless() && !device.IsTablet() &&
              device.IsTouch() &&
              device.Width() > 320 &&
              (device.HasOs("android", new Version(2, 2)) ||
              device.HasOs("iphone os", new Version(3, 2)) ||
              device.HasOs("windows phone os", new Version(7, 1)) ||
              device.HasOs("rim os", new Version(6, 0)));
                 * **/
            return rtn;
        }

        /**
        public static string IsTablet(this IDevice device)
        {
            var userAgent = request.UserAgent;
            var device =
              WURFLManagerBuilder.Instance.GetDeviceForRequest(userAgent);
            var gotTablet = device.GetVirtualCapability("is_tablet");

            return userAgent;
            //return device.GetCapability("is_tablet");
        }
         * **/
    }
}
