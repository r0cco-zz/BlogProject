using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TinyMCE_demo.Startup))]
namespace TinyMCE_demo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
