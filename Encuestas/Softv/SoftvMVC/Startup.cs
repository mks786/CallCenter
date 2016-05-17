using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SoftvMVC.Startup))]
namespace SoftvMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
