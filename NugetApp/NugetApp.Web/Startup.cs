using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NugetApp.Web.Startup))]
namespace NugetApp.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
