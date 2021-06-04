using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JobOffersApp.Startup))]
namespace JobOffersApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
