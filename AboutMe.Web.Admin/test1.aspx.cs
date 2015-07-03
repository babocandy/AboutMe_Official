using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using AboutMe.Common.Helper;

namespace AboutMe.Web.Admin
{
    public partial class test1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(AboutMe.Common.Helper.Config.GetConfigValue("ProductPhotoPath"));

        }
    }
}