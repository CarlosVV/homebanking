using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace DinersClubOnline.Api.Principals
{
    public static class PrincipalExtensions
    {
        public static DinersPrincipal ToDinersUser(this IPrincipal user)
        {
            var identity = user as ClaimsPrincipal;

            if (identity == null)
                return null;

            var claims = identity.Claims.ToList();
            var idTarjetaClaim = claims.SingleOrDefault(x => x.Type == "idTarjeta");
            var tieneClaveDigitalClaim = claims.SingleOrDefault(x => x.Type == "tieneClaveDigital");
            var idUsuarioClaim = claims.SingleOrDefault(x => x.Type == "idUsuario");

            return new DinersPrincipal
            {
                IdTarjeta = idTarjetaClaim != null ? idTarjetaClaim.Value : null,
                TieneClaveDigital = tieneClaveDigitalClaim != null ? Boolean.Parse(tieneClaveDigitalClaim.Value) : (bool?)null,
                IdUsuario = idUsuarioClaim != null ? idUsuarioClaim.Value : null
            };
        }
    }
}