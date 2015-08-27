using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TimeSheet.Web.Startup))]
namespace TimeSheet.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
