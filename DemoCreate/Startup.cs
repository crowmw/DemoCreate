using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DemoCreate.Startup))]
namespace DemoCreate
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
