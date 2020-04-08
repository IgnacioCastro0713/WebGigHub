using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebGigHub.Startup))]
namespace WebGigHub
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
