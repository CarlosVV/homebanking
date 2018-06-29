using DinersClubOnline.Api.Filters;
using DinersClubOnline.Api.Infrastructure;
using DinersClubOnline.Api.Models.Solicitudes;
using DinersClubOnline.Api.Principals;
using DinersClubOnline.Model;
using DinersClubOnline.Model.Solicitudes;
using System.Threading.Tasks;
using System.Web.Http;

namespace DinersClubOnline.Api.Controllers.Solicitudes
{
    [ScopeAuthorize(Scopes.Consultas)]
    [RoutePrefix("api/solicitudes/prestamo-personal")]
    public class PrestamoPersonalController : ApiController
    {
        private readonly IPrestamoPersonalRepository _prestamoPersonalRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public PrestamoPersonalController(IPrestamoPersonalRepository prestamoPersonalRepository, IUsuarioRepository usuarioRepository)
        {
            _prestamoPersonalRepository = prestamoPersonalRepository;
            _usuarioRepository = usuarioRepository;
        }

        [Route]
        [ScopeAuthorize(Scopes.Consultas)]
        public async Task<IHttpActionResult> Post(PrestamoPersonalViewModel prestamoPersonal)
        {
            var solicitud = new PrestamoPersonal
            {
                IdTipoOferta = prestamoPersonal.IdTipoOferta,
                IdBanco = prestamoPersonal.IdBanco,
                Banco = prestamoPersonal.Banco,
                IdTarjeta = prestamoPersonal.IdTarjeta,
                NumeroCuentaDestino = prestamoPersonal.NumeroCuentaDestino,
                MontoPrestamo = prestamoPersonal.MontoPrestamo,
                Cuotas = prestamoPersonal.Cuotas,
                Tcea = prestamoPersonal.Tcea,
                MontoCuota = prestamoPersonal.MontoCuota,
                TipoCuenta = prestamoPersonal.TipoCuenta,
                TipoMoneda = prestamoPersonal.TipoMoneda
            };

            var usuario = await _usuarioRepository.ObtenerUsuarioAsync(User.ToDinersUser().IdUsuario);
            var result = await _prestamoPersonalRepository.GuardarAsync(solicitud);

            if (result.Resultado)
            {

                EmailHelper.PrestamoPersonal_ProcesarEnviarCorreo(prestamoPersonal, usuario, result.NumeroSolicitud.ToString(), result.FechaRegistro.ToString());

                return Created("api/solicitudes/prestamo-personal", new SolicitudResponseViewModel
                {
                    NumeroSolicitud = result.NumeroSolicitud,
                    FechaRegistro = result.FechaRegistro
                });
            }

            return BadRequest("And error ocurred");
        }
    }
}