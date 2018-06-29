using DinersClubOnline.Api.Filters;
using DinersClubOnline.Api.Models.Solicitudes;
using DinersClubOnline.Model.Solicitudes;
using System.Threading.Tasks;
using System.Web.Http;
using DinersClubOnline.Api.Helpers;
using DinersClubOnline.Api.Infrastructure;
using DinersClubOnline.Model;
using DinersClubOnline.Api.Principals;

namespace DinersClubOnline.Api.Controllers.Solicitudes
{
    [ScopeAuthorize(Scopes.Consultas)]
    [RoutePrefix("api/solicitudes/dinero-efectivo")]
    public class DineroEfectivoController : ApiController
    {
        private readonly IDineroEfectivoRepository _dineroEfectivoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public DineroEfectivoController(IDineroEfectivoRepository dineroEfectivoRepository, IUsuarioRepository usuarioRepository)
        {
            _dineroEfectivoRepository = dineroEfectivoRepository;
            _usuarioRepository = usuarioRepository;
        }

        [Route]
        [ScopeAuthorize(Scopes.Consultas)]
        public async Task<IHttpActionResult> Post(DineroEfectivoViewModel dineroEfectivo)
        {
            var solicitud = new DineroEfectivo
            {
                IdTipoOferta = dineroEfectivo.IdTipoOferta,
                IdBanco = dineroEfectivo.IdBanco,
                IdTarjeta = dineroEfectivo.IdTarjeta,
                Banco = dineroEfectivo.Banco,
                NumeroCuentaDestino = dineroEfectivo.NumeroCuentaDestino,
                MontoPrestamo = dineroEfectivo.MontoPrestamo,
                Cuotas = dineroEfectivo.Cuotas,
                Tcea = dineroEfectivo.Tcea,
                MontoCuota = dineroEfectivo.MontoCuota,
                TipoCuenta = dineroEfectivo.TipoCuenta,
                TipoMoneda = dineroEfectivo.TipoMoneda
            };

            var usuario = await _usuarioRepository.ObtenerUsuarioAsync(User.ToDinersUser().IdUsuario);
            var result = await _dineroEfectivoRepository.GuardarAsync(solicitud);

            if (result.Resultado)
            {
                EmailHelper.DineroEfectivo_ProcesarEnviarCorreo(dineroEfectivo, usuario, result.NumeroSolicitud.ToString(), result.FechaRegistro.ToString());

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