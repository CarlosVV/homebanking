using System.Threading.Tasks;

namespace DinersClubOnline.Model.Auth
{
    public interface ILoginService
    {
        Task<ValidarTarjetaResult> ValidarTarjetaAsync(string ultimos4DigitosTarjeta, string cvv2, string numeroDocumentoIdentidad);
        Task<LoginResult> LoginAsync(string numeroDocumentoIdentidad, string claveDigital);
    }
}