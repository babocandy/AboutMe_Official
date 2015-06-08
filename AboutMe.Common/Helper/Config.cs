using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;

namespace AboutMe.Common.Helper
{
    public class Config 
   {

      public static string GetConfigValue( string CustomSetting )
      {
          string ReturnCustomSettingValue = ""; 

          /**
          System.Configuration.Configuration rootWebConfig1 =
              System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(null);
           **/

            System.Configuration.Configuration rootWebConfig1 =
              System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~/");
          
          
          if (rootWebConfig1.AppSettings.Settings.Count > 0)
          {
              System.Configuration.KeyValueConfigurationElement customSetting =
                  rootWebConfig1.AppSettings.Settings[CustomSetting];
              if (customSetting != null)
                  ReturnCustomSettingValue = customSetting.Value;
          }

          return ReturnCustomSettingValue ;
      } 
    
    }
}
