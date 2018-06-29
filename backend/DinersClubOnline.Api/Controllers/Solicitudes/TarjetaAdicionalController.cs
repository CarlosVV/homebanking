using DinersClubOnline.Api.Filters;
using DinersClubOnline.Api.Models.Solicitudes;
using DinersClubOnline.Api.Principals;
using DinersClubOnline.Model;
using DinersClubOnline.Model.Solicitudes;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using DinersClubOnline.Api.Infrastructure;

namespace DinersClubOnline.Api.Controllers.Solicitudes
{
    [ScopeAuthorize(Scopes.Consultas)]
    [RoutePrefix("api/solicitudes/tarjeta-adicional")]
    public class TarjetaAdicionalController : ApiController
    {
        private readonly ITarjetaAdicionalRepository _tarjetaAdicionalRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public TarjetaAdicionalController(ITarjetaAdicionalRepository tarjetaAdicionalRepository, IUsuarioRepository usuarioRepository)
        {
            _tarjetaAdicionalRepository = tarjetaAdicionalRepository;
            _usuarioRepository = usuarioRepository;
        }

        [Route]
        [ScopeAuthorize(Scopes.Consultas)]
        public async Task<IHttpActionResult> Post(TarjetaAdicionalViewModel tarjetaAdicional)
        {
            var solicitud = new TarjetaAdicional
            {
                IdTipoOferta = tarjetaAdicional.IdTipoOferta,
                IdTarjeta = tarjetaAdicional.IdTarjeta,
                TarjetasAdicionales = tarjetaAdicional.TarjetasAdicionalesDetalle.Select(r => new TarjetaAdicionalDetalle 
                {
                    Nombre = r.Nombre,
                    SegundoNombre = r.SegundoNombre,
                    ApellidoPaterno = r.ApellidoPaterno,
                    ApellidoMaterno = r.ApellidoMaterno,
                    TipoDocumento = r.TipoDocumento,
                    NumeroDocumento = r.NumeroDocumento,
                    NumeroTelefono = r.NumeroTelefono,
                    FechaNacimiento = r.FechaNacimiento,
                    Correo = r.Correo,
                    NombreTarjeta = r.NombreTarjeta,
                    TopeConsumoMensual = r.TopeConsumoMensual
                }).ToList()
            };

            var usuario = await _usuarioRepository.ObtenerUsuarioAsync(User.ToDinersUser().IdUsuario);
            var result = await _tarjetaAdicionalRepository.GuardarAsync(solicitud);

            if (result.Resultado)
            {
                EmailHelper.TarjetaAdicional_ProcesarEnviarCorreo(tarjetaAdicional, usuario, result.NumeroSolicitud.ToString(), result.FechaRegistro.ToString());

                return Created("api/solicitudes/tarjeta-adicional", new SolicitudResponseViewModel
                {
                    NumeroSolicitud = result.NumeroSolicitud,
                    FechaRegistro = result.FechaRegistro
                });
            }

            return BadRequest("And error ocurred");   
        }
    }
}
