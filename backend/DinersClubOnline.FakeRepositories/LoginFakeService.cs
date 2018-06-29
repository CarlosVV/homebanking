using System.Linq;
using System.Threading.Tasks;
using DinersClubOnline.Model.Auth;

namespace DinersClubOnline.FakeRepositories
{
    public class LoginFakeService : ILoginService
    {
        public Task<ValidarTarjetaResult> ValidarTarjetaAsync(string ultimos4DigitosTarjeta, string cvv2, string numeroDocumentoIdentidad)
        {
            var tarjeta = Data.TarjetasTitulares.FirstOrDefault(x => x.NumeroTarjeta.EndsWith(ultimos4DigitosTarjeta) && x.Cvv2 == cvv2 && x.Socio.NumeroDocumentoIdentidad == numeroDocumentoIdentidad);

            return Task.FromResult(tarjeta != null ?
                new ValidarTarjetaResult
                {
                    EsValido = true,
                    IdTarjeta = tarjeta.Id,
                    TieneClaveDigital = tarjeta.Socio.TieneClaveDigital,
                    Nombre = tarjeta.Socio.Nombre,
                    SegundoNombre = tarjeta.Socio.SegundoNombre,
                    ApellidoPaterno = tarjeta.Socio.ApellidoPaterno,
                    ApellidoMaterno = tarjeta.Socio.ApellidoMaterno
                } :
                new ValidarTarjetaResult
                {
                    EsValido = false
                });
        }

        public Task<LoginResult> LoginAsync(string numeroDocumentoIdentidad, string claveDigital)
        {
            var usuario = Data.Usuarios.FirstOrDefault(x => x.Socio.NumeroDocumentoIdentidad == numeroDocumentoIdentidad && x.ClaveDigital == claveDigital);

            return Task.FromResult(usuario != null ? LoginResult.CreateValido(usuario.Id) : LoginResult.CreateInvalido());
        }
    }
}