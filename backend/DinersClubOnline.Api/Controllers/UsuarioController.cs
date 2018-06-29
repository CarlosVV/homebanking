using DinersClubOnline.Api.Filters;
using DinersClubOnline.Api.Models;
using DinersClubOnline.Api.Principals;
using DinersClubOnline.Model;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace DinersClubOnline.Api.Controllers
{
    [RoutePrefix("api/usuario")]
    public class UsuarioController : ApiController
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        /// <summary>Registra a un nuevo usuario en sistema</summary>
        [ScopeAuthorize(Scopes.Registro)]
        [Route]
        public async Task<IHttpActionResult> Post(UsuarioPostViewModel usuarioViewModel)
        {
            var result = await _usuarioRepository.RegistrarClaveDigitalAsync(User.ToDinersUser().IdTarjeta, usuarioViewModel.ClaveDigital, usuarioViewModel.Email, 
                                                                                    usuarioViewModel.IdOperadorTelefonico, usuarioViewModel.NumeroCelular, 
                                                                                    usuarioViewModel.IdImagen, usuarioViewModel.IdFacebook);

            if (!result.Resultado)
            {
                if (result.Error.HasValue)
                {
                    switch (result.Error.Value)
                    {
                        case ErrorRegistrarClaveDigital.TarjetaNoExiste:
                            return BadRequest("La tarjeta no existe");

                        case ErrorRegistrarClaveDigital.YaTieneClaveDigital:
                            return BadRequest("Ud ya cuenta con clave digital");
                    }
                }

                return InternalServerError(new Exception("Ocurrió un error"));
            }

            return Created("api/usuario/" + result.UsuarioCreado.Id, result.UsuarioCreado.Id);
        }

        /// <summary>Obtiene la información de un usuario</summary>
        /// TODO: crear view model exclusivamente para datos de Usuario
        [ScopeAuthorize(Scopes.Consultas)]
        [Route]
        [ResponseType(typeof(UsuarioGetViewModel))]
        public async Task<IHttpActionResult> Get()
        {
            var usuario = await _usuarioRepository.ObtenerUsuarioAsync(User.ToDinersUser().IdUsuario);

            if (usuario == null)
            {
                return NotFound();
            }
                

            return Ok(new UsuarioGetViewModel
            {
                IdUsuario = usuario.Id,
                NumeroDocumentoIdentidad = usuario.Socio.NumeroDocumentoIdentidad,
                TipoDocumentoIdentidad = usuario.Socio.TipoDocumentoIdentidad,
                Nombre = usuario.Socio.Nombre,
                SegundoNombre = usuario.Socio.SegundoNombre,
                ApellidoPaterno = usuario.Socio.ApellidoPaterno,
                ApellidoMaterno = usuario.Socio.ApellidoMaterno,
                FechaNacimiento = usuario.Socio.FechaNacimiento,
                Sexo = usuario.Socio.Sexo,
                EstadoCivil = usuario.Socio.EstadoCivil,
                Procedencia = usuario.Socio.Procedencia,
                EmailPrincipal = usuario.EmailPrincipal,
                EmailAlternativo = usuario.EmailAlternativo,
                EmailSeleccionado = usuario.EmailSeleccionado,
                IdOperadorTelefonico = usuario.OperadorTelefonico.Id,
                NumeroCelular = usuario.NumeroCelular,
                IdImagen = usuario.IdImagen,
                IdFacebook = usuario.IdFacebook,
                Alias = usuario.Alias
            });
        }

        /// <summary>Actualiza la clave digital de un usuario.</summary>
        [ScopeAuthorize(Scopes.Registro | Scopes.Operaciones)]
        [Route("clave-digital")]
        [ResponseType(typeof(UsuarioClaveDigitalPutResponseViewModel))]
        public async Task<IHttpActionResult> PutClaveDigital(UsuarioClaveDigitalPutViewModel viewModel)
        {
            await _usuarioRepository.ActualizarClaveDigitalAsync(User.ToDinersUser().IdUsuario, viewModel.ClaveDigital);

            return Ok(new UsuarioClaveDigitalPutResponseViewModel { FechaOperacion = DateTime.Now });
        }

        /// <summary>Actualiza los datos de Usuario</summary>
        /// <param name="usuarioViewModel">Datos de Usuario</param>
        /// <returns>Status OK</returns>
        [ScopeAuthorize(Scopes.Consultas)]
        [Route]
        public async Task<IHttpActionResult> Patch(UsuarioPatchViewModel usuarioViewModel)
        {
            var result = await _usuarioRepository.ActualizarUsuarioAsync(User.ToDinersUser().IdUsuario, usuarioViewModel.emailPrincipal, usuarioViewModel.emailAlternativo, usuarioViewModel.IdOperadorTelefonico, usuarioViewModel.NumeroCelular);

            if (!result.Resultado)
            {
                if (result.Error.HasValue)
                {
                    switch (result.Error.Value)
                    {
                        case ErrorActualizarUsuario.EmailIncorrecto:
                            return BadRequest("E-mail incorrecto");

                        case ErrorActualizarUsuario.OperadorIncorrecto:
                            return BadRequest("Operador Incorrecto");
                        default:
                            return BadRequest("Ocurrió un error");

                    }
                }

                return InternalServerError(new Exception("Ocurrió un error"));
            }

            return Ok(User.ToDinersUser().IdUsuario);
        }

        /// <summary>
        /// Actualiza el alias del usuario
        /// </summary>
        /// <param name="alias"></param>
        /// <returns></returns>
        [ScopeAuthorize(Scopes.Consultas)]
        [Route("cambiar-alias")]
        [HttpPatch]
        public async Task<IHttpActionResult> PatchCambiarAlias(string alias)
        {
            if (string.IsNullOrEmpty(alias))
            {
                return BadRequest("Debe ingresar el alias");
            }
            var result = await _usuarioRepository.ActualizarAliasAsync(User.ToDinersUser().IdUsuario, alias);

            if (!result)
            {
                return NotFound();
            }

            return Ok(true);
        }

        [ScopeAuthorize(Scopes.Consultas)]
        [Route("imagenPerfil")]
        [HttpPatch]
        public async Task<IHttpActionResult> PatchCambiarImagenPerfil(UsuarioPatchActualizarImagenPerfil usuario)
        {
            var result = await _usuarioRepository.ActualizarImagenPerfil(User.ToDinersUser().IdUsuario, usuario.IdFacebook, usuario.IdImagen);

            if (!result)
            {
                return NotFound();
            }

            return Ok(true);
        }
    }
}