using System.Threading.Tasks;

namespace DinersClubOnline.Model
{
    public interface IUsuarioRepository
    {
        Task<Usuario> ObtenerUsuarioAsync(string idUsuario);

        Task<RegistrarClaveDigitalResult> RegistrarClaveDigitalAsync(string idTarjeta, string claveDigital, string email, string idOperadorTelefonico, string numeroCelular, string idImagen, string idFacebook);

        Task<bool> ActualizarClaveDigitalAsync(string idUsuario, string claveDigital);

        Task<bool> ActualizarAliasAsync(string idUsuario, string alias);

        Task<ActualizarUsuarioResult> ActualizarUsuarioAsync(string idUsuario, string emailPrincipal, string emailAlternativo, string idOperadorTelefonico, string numeroCelular);

        Task<bool> ActualizarImagenPerfil(string idUsuario, string idFacebook, string idImagen);
    }

    public enum ErrorRegistrarClaveDigital
    {
        ErrorGenerico,
        TarjetaNoExiste,
        YaTieneClaveDigital,
    }

    public enum ErrorActualizarUsuario
    {
        ErrorGenerico,
        EmailIncorrecto,
        OperadorIncorrecto,
        UsuarioNoExiste,
        NumeroIncorrecto,
    }
}