using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using DinersClubOnline.Auth.Models.Auth;
using DinersClubOnline.Model.Auth;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace DinersClubOnline.Auth.OAuthProviders
{
    public class ClaveDigitalOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        public ClaveDigitalOAuthProvider(string publicClientId)
        {
            if (publicClientId == null) throw new ArgumentNullException("publicClientId");

            _publicClientId = publicClientId;
        }

        //TODO: Leer [The OAuth 2.0 Authorization Framework: Bearer Token Usage](https://tools.ietf.org/html/rfc6750) para implementarlo correctamente 
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            if (context.Scope.Count != 1 || context.Scope.Any(x => !Scopes.ValidClaveDigitalScopes.Contains(x)))
            {
                context.SetError("invalid_scope", "The scope is invalid");
                return;
            }

            var resolver = GlobalConfiguration.Configuration.DependencyResolver;
            var loginService = (ILoginService)resolver.GetService(typeof(ILoginService));
            var result = await loginService.LoginAsync(context.UserName, context.Password);
            if (context.UserName == null || context.Password == null || !result.EsValido)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var scope = context.Scope.Single();

            switch (scope)
            {
                case Scopes.Consultas:
                    context.Options.AccessTokenExpireTimeSpan = TimeSpan.FromHours(Int32.Parse(ConfigurationManager.AppSettings[AuthConfig.ClaveDigitalTokenExpirationInHoursKey]));
                    break;
                case Scopes.Operaciones:
                    context.Options.AccessTokenExpireTimeSpan = TimeSpan.FromSeconds(Int32.Parse(ConfigurationManager.AppSettings[AuthConfig.OperacionesTokenExpirationInSecondsKey]));
                    break;
                default:
                    context.SetError("invalid_scope", "The scope is invalid");
                    return;
            }

            var ticket = CreateAuthenticationTicket(scope, result.IdUsuario);

            context.Validated(ticket);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }


        public AuthenticationTicket CreateAuthenticationTicket(string scope, string idUsuario)
        {
            if (scope == null) throw new ArgumentNullException("scope");
            if (!Scopes.ValidClaveDigitalScopes.Contains(scope)) throw new ArgumentException("Invalid scope", "scope");

            return new AuthenticationTicket(
                new ClaimsIdentity(new[]
                {
                    new Claim("scope", scope),
                    new Claim("idUsuario", idUsuario),
                }, OAuthDefaults.AuthenticationType),
                new AuthenticationProperties(new Dictionary<string, string>
                {
                    { "scope", scope }
                })
            );
        }
    }
}