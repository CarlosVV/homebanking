using System;
using System.Configuration;
using DinersClubOnline.Auth.OAuthProviders;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace DinersClubOnline.Auth
{
    
    public static class AuthConfig
    {
        public const string ClaveDigitalTokenExpirationInHoursKey = "ClaveDigitalTokenExpirationInHours";
        public const string TarjetaTokenExpirationInMinutesKey = "TarjetaTokenExpirationInMinutes";
        public const string OperacionesTokenExpirationInSecondsKey = "OperacionesTokenExpirationInSeconds";


        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public static void ConfigureAuth(IAppBuilder app)
        {
            // Configure the application for OAuth based flow
            PublicClientId = "self";

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/token-tarjeta"),
                Provider = new TarjetaOAuthProvider(PublicClientId),
                // TODO: Verificar valores correctos para AuthorizeEndpointPath y AccessTokenExpireTimeSpan
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(Int32.Parse(ConfigurationManager.AppSettings[TarjetaTokenExpirationInMinutesKey])),
                // In production mode set AllowInsecureHttp = false
#if DEBUG
                AllowInsecureHttp = true
#endif
            });

            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/token-clave-digital"),
                Provider = new ClaveDigitalOAuthProvider(PublicClientId),
                // TODO: Verificar valores correctos para AuthorizeEndpointPath y AccessTokenExpireTimeSpan
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                // In production mode set AllowInsecureHttp = false
#if DEBUG
                AllowInsecureHttp = true
#endif
            });
        }
    }
}