using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BusLocation.Startup))]
namespace BusLocation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
