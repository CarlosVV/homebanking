using System;
using System.Threading.Tasks;
using DinersClubOnline.Model.Auth;
using DinersClubOnline.Services.Client.SeguridadService;
using DinersClubOnline.Services.Client.Common;

namespace DinersClubOnline.Repositories
{
    public class LoginService : ILoginService
    {
        private readonly Seguridad _seguridadService;

        public LoginService(Seguridad seguridadService)
        {
            _seguridadService = seguridadService;
        }

        public Task<ValidarTarjetaResult> ValidarTarjetaAsync(string ultimos4DigitosTarjeta, string cvv2, string numeroDocumentoIdentidad)
        {
            //var serviceResponse = await _seguridadService.validarTarjetaAsync(new validarTarjetaRequest
            //{
            //    validarTarjeta = new validarTarjeta
            //    {
            //        arg0 = new requestValidarTarjeta { numeroTarjeta = numeroTarjeta, claveATM = claveAtm }
            //    }
            //});

            //var result = serviceResponse.validarTarjetaResponse.@return;

            //if (result.codigoMensaje != DinersResponseCodes.EjecucionExitosa)
            //    throw new Exception(result.mensaje);

            ////TODO: llamar al UsuarioService para completar la informacion
            //return ValidarTarjetaResult
            //{
            //    EsValido = true,
            //    IdTarjeta = result.idTarjeta
            //    //Nombre = ??
            //};

            throw new NotImplementedException();
        }

        public async Task<LoginResult> LoginAsync(string username, string claveDigital)
        {
            var serviceResponse = await _seguridadService.loginAsync(new loginRequest
            {
                login = new login
                {
                    arg0 = new requestLogin
                    {
                        usuario = username,
                        claveDigital = claveDigital
                    }
                }
            });

            var result = serviceResponse.loginResponse.@return;

            if (result.codigoMensaje != DinersResponseCodes.EjecucionExitosa)
            {
                throw new Exception(result.mensaje);
            }

            return LoginResult.CreateValido(result.idUsuario);
        }
    }
}