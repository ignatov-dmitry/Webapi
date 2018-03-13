using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Boxy.WebUI.Startup))]
namespace Boxy.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
