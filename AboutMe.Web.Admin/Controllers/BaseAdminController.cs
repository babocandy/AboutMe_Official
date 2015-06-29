//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
using System.Web.Mvc;
using AboutMe.Common.Helper;

namespace AboutMe.Web.Admin.Controllers
{
    public class BaseAdminController : Controller
    {
        private string ADM_ID ;
        private string ADM_NAME;
        private string ADM_GRADE; //S,A,''
        private string ADM_EMAIL;
        private string ADM_PHONE;


       protected override void Initialize(System.Web.Routing.RequestContext requestContext)
       {
          
         
          base.Initialize(requestContext);
      
         // MyInitialzie()
        

       }

      
    }
}