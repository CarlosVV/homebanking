using System.Web.Http;
using System.Web.Mvc;
using JetBrains.Annotations;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(DinersClubOnline.Api.Startup))]

namespace DinersClubOnline.Api
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
            GlobalConfiguration.Configure(SwaggerConfig.Configure);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }
    }
}