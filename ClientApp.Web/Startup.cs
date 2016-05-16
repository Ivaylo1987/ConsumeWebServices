using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClientApp.Web.Startup))]
namespace ClientApp.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
