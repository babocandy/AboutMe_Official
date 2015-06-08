using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AboutMe.Web.Front.Startup))]
namespace AboutMe.Web.Front
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
