using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TORNEOS.Web.Startup))]
namespace TORNEOS.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
