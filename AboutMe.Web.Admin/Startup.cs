using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AboutMe.Web.Admin.Startup))]
namespace AboutMe.Web.Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
