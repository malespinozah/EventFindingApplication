using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EventFindingApplication.Startup))]
namespace EventFindingApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
