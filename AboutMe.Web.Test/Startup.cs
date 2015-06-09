using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AboutMe.Web.Test.Startup))]
namespace AboutMe.Web.Test
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
