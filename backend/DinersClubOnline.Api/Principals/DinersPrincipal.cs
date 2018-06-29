using System.Security.Claims;

namespace DinersClubOnline.Api.Principals
{
    public class DinersPrincipal : ClaimsPrincipal
    {
        public string IdTarjeta { get; set; }
        public bool? TieneClaveDigital { get; set; }
        public string IdUsuario { get; set; }
        public string DocumentoIdentidad { get; set; }
    }
}