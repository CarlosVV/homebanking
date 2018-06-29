using Microsoft.Owin.Security.OAuth;
using Owin;

namespace DinersClubOnline.Api
{
    public static class AuthConfig
    {
        public static void ConfigureAuth(IAppBuilder app)
        {
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}