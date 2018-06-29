using System;
using System.Threading.Tasks;
using DinersClubOnline.Model;
using DinersClubOnline.Services.Client.SeguridadService;
using DinersClubOnline.Services.Client.Common;

namespace DinersClubOnline.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly Seguridad _seguridadService;

        public UsuarioRepository(Seguridad seguridadService)
        {
            _seguridadService = seguridadService;
        }

        public Task<Usuario> ObtenerUsuarioAsync(string idUsuario)
        {
            throw new NotImplementedException();
        }

        public async Task<RegistrarClaveDigitalResult> RegistrarClaveDigitalAsync(string idTarjeta, string claveDigital, string email, string idOperadorTelefonico, string numeroCelular, string idImagen, string idFacebook)
        {
            var response = await _seguridadService.registrarClaveDigitalAsync(new registrarClaveDigitalRequest
            {
                registrarClaveDigital = new registrarClaveDigital
                {
                    arg0 = new requestRegistrarClaveDigital
                    {
                        idTarjeta = idTarjeta,
                        claveDigital = claveDigital,
                        email = email,
                        operadorTelefonico = idOperadorTelefonico,
                        numeroCelular = numeroCelular,
                        idImagen = idImagen,
                        idFacebook = idFacebook
                    }
                }
            });

            var result = response.registrarClaveDigitalResponse.@return;

            switch (result.codigoMensaje)
            {
                case DinersResponseCodes.EjecucionExitosa:
                    return RegistrarClaveDigitalResult.CreateUsuarioRegistradoResult(new Usuario
                    {
                        Id = result.idUsuario
                    });
                case DinersResponseCodes.TarjetaNoExiste:
                    return RegistrarClaveDigitalResult.CreateTarjetaNoExisteResult();
                default:
                    return RegistrarClaveDigitalResult.CreateErrorGenericoResult();
            }
        }

        public Task<bool> ActualizarClaveDigitalAsync(string idUsuario, string claveDigital)
        {
            throw new NotImplementedException();
        }

        public Task<ActualizarUsuarioResult> ActualizarUsuarioAsync(string idUsuario, string emailPrincipal, string emailAlternativo, string idOperadorTelefonico, string numeroCelular)
        {
            throw new NotImplementedException();
        }


        public Task<ActualizarUsuarioResult> ActualizarUsuarioAsync(string idUsuario, string email, string idOperadorTelefonico, string numeroCelular)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ActualizarAliasAsync(string idUsuario, string alias)
        {
            throw new NotImplementedException();
        }


        public Task<bool> ActualizarImagenPerfil(string idUsuario, string idFacebook, string idImagen)
        {
            throw new NotImplementedException();
        }
    }
}