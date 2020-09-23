using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AspMvcApp.Startup))]
namespace AspMvcApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
