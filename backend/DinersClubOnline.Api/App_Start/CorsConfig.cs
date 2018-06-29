using Microsoft.Owin.Cors;
using Owin;

namespace DinersClubOnline.Api
{
    public static class CorsConfig
    {
        public static void Configure(IAppBuilder app)
        {
            //TODO: Restringir los dominios desde donde se puede llamar al API
            //var corsPolicy = new CorsPolicy
            //{
            //    AllowAnyHeader = true,
            //    AllowAnyMethod = true
            //};

            //foreach (var origin in ConfigurationManager.AppSettings["AllowedCorsOrigins"].Split(','))
            //{
            //    corsPolicy.Origins.Add(origin);
            //}

            //app.UseCors(new CorsOptions
            //{
            //    PolicyProvider = new CorsPolicyProvider()
            //    {
            //        PolicyResolver = context => Task.FromResult(corsPolicy)
            //    }
            //});

            app.UseCors(CorsOptions.AllowAll);
        }
    }
}