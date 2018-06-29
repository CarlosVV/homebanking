using System.Web.Http;
using JetBrains.Annotations;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(DinersClubOnline.Auth.Startup))]

namespace DinersClubOnline.Auth
{
    public class Startup
    {
        [UsedImplicitly]
        public void Configuration(IAppBuilder app)
        {
            CorsConfig.Configure(app);
            AutofacConfig.Configure();
            AuthConfig.ConfigureAuth(app);
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}