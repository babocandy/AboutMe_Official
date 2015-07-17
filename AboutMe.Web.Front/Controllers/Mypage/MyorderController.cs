using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Common.Helper;
using AboutMe.Web.Front.Common;

using AboutMe.Domain.Service.Member;
using AboutMe.Domain.Entity.Member;

using System.Text;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

using AboutMe.Web.Front.Common.Filters;


namespace AboutMe.Web.Front.Controllers
{
    [RoutePrefix("MyPage/MyOrder")]
    [Route("{action=index}")]
    public class MyOrderController : BaseFrontController
    {
        public ActionResult Index()
        {

            return View();
        }

    }
}