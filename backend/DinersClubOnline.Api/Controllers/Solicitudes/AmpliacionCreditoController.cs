using DinersClubOnline.Api.Filters;
using DinersClubOnline.Api.Infrastructure;
using DinersClubOnline.Api.Models.Solicitudes;
using DinersClubOnline.Api.Principals;
using DinersClubOnline.Mail.EmailService;
using DinersClubOnline.Model;
using DinersClubOnline.Model.Solicitudes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace DinersClubOnline.Api.Controllers.Solicitudes
{
    [ScopeAuthorize(Scopes.Consultas)]
    [RoutePrefix("api/solicitudes/ampliacion-credito")]
    public class AmpliacionCreditoController : ApiController
    {
        private readonly IAmpliacionCreditoRepository _ampliacionCreditoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public AmpliacionCreditoController(IAmpliacionCreditoRepository ampliacionCreditoRepository, IUsuarioRepository usuarioRepository)
        {
            _ampliacionCreditoRepository = ampliacionCreditoRepository;
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        [Route]
        [ResponseType(typeof(SolicitudResponseViewModel))]
        public async Task<IHttpActionResult> Post(AmpliacionCreditoViewModel modelo)
        {
            var model = new AmpliacionCredito
            {
                IdSocio = User.ToDinersUser().IdUsuario,
                IdTarjeta = modelo.IdTarjeta,
                IdTipoOferta = modelo.IdTipoOferta,
                NumeroTarjeta = modelo.NumeroTarjeta,
                CreditoActual = modelo.CreditoActual,
                CreditoSolicitado = modelo.CreditoSolicitado,
                NombreProducto = modelo.NombreProducto
            };

            var usuario = await _usuarioRepository.ObtenerUsuarioAsync(User.ToDinersUser().IdUsuario);
            var result = await _ampliacionCreditoRepository.GuardarAsync(model);
            if (result.Resultado)
            {
                // envio de correo a SAC Diners
                if (usuario != null)
                {
                    SendEmail(model, result, usuario);
                }
                return Created("api/solicitudes/ampliacion-credito", new SolicitudResponseViewModel
                {
                    NumeroSolicitud = result.NumeroSolicitud,
                    FechaRegistro = result.FechaRegistro
                });
            }

            return BadRequest("And error ocurred");
        }

        [HttpGet]
        [Route("{idSolicitud}")]
        public async Task<IHttpActionResult> Get(string idSolicitud)
        {
            try
            {
                var result = await _ampliacionCreditoRepository.ObtenerAsync(idSolicitud);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        private void SendEmail(AmpliacionCredito ampliacionCredito, SolicitudResult resultadoSolicitud, Usuario usuario)
        {
            var correoDiners = System.Configuration.ConfigurationManager.AppSettings["CorreoDinersSac"];
            var asuntoEmail = "Canal Web – SOLICITUD DE INCREMENTO DE LÍNEA";
            var emailsTo = new List<string>();

            if (!string.IsNullOrEmpty(usuario.EmailPrincipal))
            {
                string correoUsuario = usuario.EmailSeleccionado == "1" ? usuario.EmailPrincipal : usuario.EmailAlternativo;
                emailsTo.Add(correoUsuario);
            }

            var emailsToDiners = new List<string>();
            emailsToDiners.Add(correoDiners);

            var dataPrincipal = new List<List<string>>();
            var data = new List<string>();
            data.Add("Solicitud");
            data.Add("Solicitud de Incremento de Línea");
            dataPrincipal.Add(data);

            data = new List<string>();
            data.Add("Fecha y hora");
            var fechaFormateada = resultadoSolicitud.FechaRegistro.HasValue ? String.Format("{0:dd/MM/yyyy HH:mm}", (DateTime)resultadoSolicitud.FechaRegistro) : string.Empty;
            data.Add(fechaFormateada);
            dataPrincipal.Add(data);

            data = new List<string>();
            data.Add("N° Solicitud");
            data.Add(resultadoSolicitud.NumeroSolicitud.ToString());
            dataPrincipal.Add(data);

            data = new List<string>();
            data.Add("Tarjeta");
            data.Add(string.Format("{0} {1}", ampliacionCredito.NombreProducto, ampliacionCredito.NumeroTarjeta));
            dataPrincipal.Add(data);

            data = new List<string>();
            data.Add("Línea de crédito actual");
            data.Add(string.Format("S/ {0}",ampliacionCredito.CreditoActual.ToString("0,0.00")));
            dataPrincipal.Add(data);

            data = new List<string>();
            data.Add("Línea de crédito nueva");
            data.Add(string.Format("S/ {0}",ampliacionCredito.CreditoSolicitado.ToString("0,0.00")));
            dataPrincipal.Add(data);

            try
            {
                var contentParaUsuario = EmailHelper.CrearHtmlOperacionEmail(usuario.Socio.NombreCompleto, dataPrincipal);

                EmailSenderService.SendEmail(asuntoEmail, contentParaUsuario, correoDiners, emailsTo, null, null, null);
                data = new List<string>();
                data.Add("Información del socio");
                data.Add("");
                dataPrincipal.Add(data);

                data = new List<string>();
                data.Add("Nombre");
                data.Add(string.Format("{0} {1} {2}", usuario.Socio.Nombre, usuario.Socio.ApellidoPaterno, usuario.Socio.ApellidoMaterno));
                dataPrincipal.Add(data);

                data = new List<string>();
                data.Add("Documento de identidad");
                data.Add(string.Format("{0} {1} ", usuario.Socio.TipoDocumentoIdentidad, usuario.Socio.NumeroDocumentoIdentidad));
                dataPrincipal.Add(data);

                data = new List<string>();
                data.Add("Email");
                data.Add(usuario.EmailPrincipal);
                dataPrincipal.Add(data);

                data = new List<string>();
                data.Add("Celular");
                data.Add(usuario.NumeroCelular);
                dataPrincipal.Add(data);

                var contentParaDiners = EmailHelper.CrearHtmlOperacionEmail(usuario.Socio.NombreCompleto, dataPrincipal);

                EmailSenderService.SendEmail(asuntoEmail, contentParaDiners, correoDiners, emailsToDiners, null, null, null);
            }
            catch (System.Exception) { throw; }
        }
    }
}