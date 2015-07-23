using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AboutMe.Web.Mobile.Startup))]
namespace AboutMe.Web.Mobile
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
