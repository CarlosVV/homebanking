using System;
using System.Collections.Generic;
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
    public class TarjetaOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        public TarjetaOAuthProvider(string publicClientId)
        {
            if (publicClientId == null) throw new ArgumentNullException("publicClientId");

            _publicClientId = publicClientId;
        }

        //TODO: Leer [The OAuth 2.0 Authorization Framework: Bearer Token Usage](https://tools.ietf.org/html/rfc6750) para implementarlo correctamente 
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            if (context.Scope.Count != 1 || context.Scope.Any(scope => !Scopes.ValidTarjetaScopes.Contains(scope)))
            {
                context.SetError("invalid_scope", "The scope is invalid");
                return;
            }

            var resolver = GlobalConfiguration.Configuration.DependencyResolver;
            var loginService = (ILoginService)resolver.GetService(typeof(ILoginService));

            var numeroDocumentoIdentidad = context.UserName;
            if (numeroDocumentoIdentidad == null)
            {
                SetUserOrPasswordError(context);
                return;
            }

            var passwordValues = context.Password.Split(',');
            if (passwordValues.Length != 2)
            {
                SetUserOrPasswordError(context);
                return;
            }
            
            var ultimosDigitosTarjeta = passwordValues[0];
            var cvv2 = passwordValues[1];

            var result = await loginService.ValidarTarjetaAsync(ultimosDigitosTarjeta, cvv2, numeroDocumentoIdentidad);

            if (!result.EsValido)
            {
                SetUserOrPasswordError(context);
                return;
            }

            var ticket = CreateAuthenticationTicket(context.Scope.Single(), result);

            context.Validated(ticket);
        }

        private static void SetUserOrPasswordError(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.SetError("invalid_grant", "The user name or password is incorrect.");
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


        public AuthenticationTicket CreateAuthenticationTicket(string scope, ValidarTarjetaResult tarjeta)
        {// string idTarjeta, bool tieneClaveDigital
            if (scope == null) throw new ArgumentNullException("scope");
            if (!Scopes.ValidTarjetaScopes.Contains(scope)) throw new ArgumentException("Invalid scope", "scope");

            return new AuthenticationTicket(
                new ClaimsIdentity(new[]
                {
                    new Claim("scope", scope),
                    new Claim("idTarjeta", tarjeta.IdTarjeta),
                    new Claim("tieneClaveDigital", tarjeta.TieneClaveDigital.ToString(), ClaimValueTypes.Boolean),                    
                    new Claim("nombreTitular", tarjeta.Nombre),
                    new Claim("segundoNombreTitular", tarjeta.SegundoNombre),
                    new Claim("apellidoPaternoTitular", tarjeta.ApellidoPaterno),
                    new Claim("apellidoMaternoTitular", tarjeta.ApellidoMaterno)
                }, OAuthDefaults.AuthenticationType),
                new AuthenticationProperties(new Dictionary<string, string>
                {
                    { "scope", scope },
                    { "tieneClaveDigital", tarjeta.TieneClaveDigital.ToString() },
                    { "nombreTitular", tarjeta.Nombre },
                    { "segundoNombreTitular", tarjeta.SegundoNombre },
                    { "apellidoPaternoTitular", tarjeta.ApellidoPaterno },
                    { "apellidoMaternoTitular", tarjeta.ApellidoMaterno }
                })
            );
        }
    }
}