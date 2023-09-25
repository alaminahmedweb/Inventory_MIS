using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebBasedDiagnosticMIS_MVC.Startup))]
namespace WebBasedDiagnosticMIS_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
