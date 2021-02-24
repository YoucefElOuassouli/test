using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EcommerceSite.Startup))]
namespace EcommerceSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
