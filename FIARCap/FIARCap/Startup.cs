using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FIARCap.Startup))]
namespace FIARCap
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
