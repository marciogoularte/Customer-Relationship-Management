using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TVChannelsCRM.Web.Startup))]
namespace TVChannelsCRM.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
