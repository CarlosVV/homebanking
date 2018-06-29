using DinersClubOnline.Api.Filters;
using DinersClubOnline.Api.Infrastructure;
using DinersClubOnline.Api.Models.Solicitudes;
using DinersClubOnline.Api.Principals;
using DinersClubOnline.Mail.EmailService;
using DinersClubOnline.Model;
using DinersClubOnline.Model.Solicitudes;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace DinersClubOnline.Api.Controllers.Solicitudes
{
    [ScopeAuthorize(Scopes.Consultas)]
    [RoutePrefix("api/solicitudes/reclamo")]
    public class ReclamoController : ApiController
    {
        private readonly IReclamoRepository _reclamoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public ReclamoController(IReclamoRepository reclamoRepository, IUsuarioRepository usuarioRepository)
        {
            _reclamoRepository = reclamoRepository;
            _usuarioRepository = usuarioRepository;
        }

        [Route]
        [ResponseType(typeof(SolicitudResponseViewModel))]
        public async Task<IHttpActionResult> Post(ReclamoViewModel modelo)
        {
            var reclamo = new Reclamo
            {
                IdSocio = User.ToDinersUser().IdUsuario,
                IdTipoOferta = modelo.IdTipoOferta,

                IdTarjeta = modelo.IdTarjeta,
                TarjetaNumero = modelo.TarjetaNumero,

                Motivo = modelo.Motivo,
                Medio = modelo.Medio,
                DireccionEnvio = modelo.DireccionEnvio,
                Descripcion = modelo.Descripcion,
                SolucionEsperada = modelo.SolucionEsperada
            };

            var usuario = await _usuarioRepository.ObtenerUsuarioAsync(User.ToDinersUser().IdUsuario);
            reclamo.SocioNombres = string.Format("{0} {1} {2} {3}", usuario.Socio.Nombre, usuario.Socio.SegundoNombre, usuario.Socio.ApellidoPaterno, usuario.Socio.ApellidoMaterno);
            reclamo.SocioTipoDocumento = usuario.Socio.TipoDocumentoIdentidad;
            reclamo.SocioNumeroDocumento = usuario.Socio.NumeroDocumentoIdentidad;
            reclamo.SocioCorreElectronico = usuario.EmailPrincipal;
            reclamo.SocioCelular = usuario.NumeroCelular;

            var result = await _reclamoRepository.GuardarAsync(reclamo);

            if (result.Resultado)
            {
                if (modelo.DatosCorreo != null)
                {
                    var data = modelo.DatosCorreo;
                    var dataSAC = new List<Diccionario>();
                    dataSAC.Add(new Diccionario { Key = "Nombres", Value = reclamo.SocioNombres});
                    dataSAC.Add(new Diccionario { Key = "Tipo Documento", Value = reclamo.SocioTipoDocumento});
                    dataSAC.Add(new Diccionario { Key = "Número Documento", Value = reclamo.SocioNumeroDocumento});
                    dataSAC.Add(new Diccionario { Key = "Correo electrónico", Value = reclamo.SocioCorreElectronico});
                    dataSAC.Add(new Diccionario { Key = "Celular", Value = reclamo.SocioCelular});
                    dataSAC.Add(new Diccionario { Key = "Tarjeta", Value = reclamo.TarjetaNumero});
                    dataSAC.Add(new Diccionario { Key = "Motivo", Value = reclamo.Motivo});
                    dataSAC.Add(new Diccionario { Key = reclamo.Medio, Value = reclamo.DireccionEnvio});
                    dataSAC.Add(new Diccionario { Key = "Descripcion", Value = reclamo.Descripcion});
                    dataSAC.Add(new Diccionario { Key = "SolucionEsperada", Value = reclamo.SolucionEsperada});

                    SendEmail(usuario, data, dataSAC);
                }

                return Created("api/solicitudes/cargo-automatico", new SolicitudResponseViewModel
                {
                    NumeroSolicitud = result.NumeroSolicitud,
                    FechaRegistro = result.FechaRegistro
                });
            }

            return BadRequest("An error ocurred");
        }

        private void SendEmail(Usuario usuario, List<Diccionario> data, List<Diccionario> dataSAC)
        {
            var correoDiners = System.Configuration.ConfigurationManager.AppSettings["CorreoDinersSac"];
            var emailsTo = new List<string>();

            if (!string.IsNullOrEmpty(usuario.EmailPrincipal))
            {
                string correoUsuario = usuario.EmailSeleccionado == "1" ? usuario.EmailPrincipal : usuario.EmailAlternativo;
                emailsTo.Add(correoUsuario);
            }

            var emailsToDiners = new List<string>();
            emailsToDiners.Add(correoDiners);

            var emailsCc = new List<string>();
            emailsCc.Add(usuario.EmailAlternativo);

            try
            {
                var content = EmailHelper.CrearHtmlOperacionEmail(usuario.Socio.NombreCompleto, data);
                EmailSenderService.SendEmail("Canal Web – SOLICITUD INGRESO DE RECLAMO", content, "diners.help@gmail.com", emailsTo, new List<string>(), emailsCc, null);

                var contentSAC = EmailHelper.CrearHtmlOperacionEmail(usuario.Socio.NombreCompleto, dataSAC);
                EmailSenderService.SendEmail("Canal Web – SOLICITUD INGRESO DE RECLAMO", contentSAC, "diners.help@gmail.com", emailsToDiners, new List<string>(), new List<string>(), null);
            }
            catch (System.Exception) { throw; }
        }
    }
}