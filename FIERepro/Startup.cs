using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FIERepro.Startup))]
namespace FIERepro
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
