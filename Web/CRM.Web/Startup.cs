using Owin;
using Microsoft.Owin;

using CRM.Web;
using CRM.Common.Constants;

[assembly: OwinStartup(typeof(Startup))]
namespace CRM.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AutoMapperConfig.RegisterMappings(Assemblies.CrmServicesData);

            ConfigureAuth(app);
        }
    }
}
