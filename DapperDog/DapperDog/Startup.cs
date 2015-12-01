using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DapperDog.Startup))]
namespace DapperDog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
