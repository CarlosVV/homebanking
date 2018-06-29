using DinersClubOnline.Api.Filters;
using DinersClubOnline.Api.Models.Solicitudes;
using DinersClubOnline.Model.Solicitudes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using DinersClubOnline.Api.Principals;
using System.Web.Http.Description;
using DinersClubOnline.Mail.EmailService;
using DinersClubOnline.Model;
using DinersClubOnline.Api.Infrastructure;

namespace DinersClubOnline.Api.Controllers.Solicitudes
{
    [ScopeAuthorize(Scopes.Consultas)]
    [RoutePrefix("api/solicitudes/debito-automatico")]
    public class DebitoAutomaticoController : ApiController
    {
        private readonly IDebitoAutomaticoRepository _debitoAutomaticoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public DebitoAutomaticoController(IDebitoAutomaticoRepository debitoAutomaticoRepository, IUsuarioRepository usuarioRepository)
        {
            _debitoAutomaticoRepository = debitoAutomaticoRepository;
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        [Route]
        [ResponseType(typeof(SolicitudResponseViewModel))]   
        public async Task<IHttpActionResult> Post(DebitoAutomaticoViewModel modelo)
        {
            var model = new DebitoAutomatico
            {
                IdSocio = User.ToDinersUser().IdUsuario,
                IdTarjeta = modelo.IdTarjeta,
                IdTipoOferta = modelo.IdTipoOferta,
                NumeroTarjeta = modelo.NumeroTarjeta,
                TipoPagoaCargar = modelo.TipoPagoaCargar,
                NombreProducto = modelo.NombreProducto,

                IdBancoSoles = modelo.FacturacionSoles.IdBanco,
                BancoSoles = modelo.FacturacionSoles.Banco,
                TipoCuentaSoles = modelo.FacturacionSoles.TipoCuenta,
                MonedaDelaCtaSoles = modelo.FacturacionSoles.MonedaDelaCta,
                NumeroCuentaSoles = modelo.FacturacionSoles.NumeroCuenta,

                IdBancoDolares = modelo.FacturacionDolares.IdBanco,
                BancoDolares = modelo.FacturacionDolares.Banco,
                TipoCuentaDolares = modelo.FacturacionDolares.TipoCuenta,
                MonedaDelaCtaDolares = modelo.FacturacionDolares.MonedaDelaCta,
                NumeroCuentaDolares = modelo.FacturacionDolares.NumeroCuenta
            };

            var usuario = await _usuarioRepository.ObtenerUsuarioAsync(User.ToDinersUser().IdUsuario);
            var result = await _debitoAutomaticoRepository.GuardarAsync(model);
            if (result.Resultado)
            {
                // envio de correo a SAC Diners
                if (usuario != null)
                { 
                    SendEmail(model, result, usuario);
                }
                return Created("api/solicitudes/debito-automatico", new SolicitudResponseViewModel
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
                var result = await _debitoAutomaticoRepository.ObtenerAsync(idSolicitud);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception ex) {
                return InternalServerError(ex);
            }

        }

        private void SendEmail(DebitoAutomatico debitoAutomatico, SolicitudResult resultadoSolicitud,  Usuario usuario)
        {
            var correoDiners = System.Configuration.ConfigurationManager.AppSettings["CorreoDinersSac"];
            var asuntoEmail = "Canal Web – SOLICITUD DE DÉBITO AUTOMÁTICO";
            var emailsTo = new List<string>();

            if (!string.IsNullOrEmpty(usuario.EmailPrincipal))
            {
                string correoUsuario = usuario.EmailSeleccionado == "1" ? usuario.EmailPrincipal : usuario.EmailAlternativo;
                emailsTo.Add(correoUsuario); 
            }

            var emailsToDiners = new List<string>();
            emailsToDiners.Add(correoDiners);

            var dataPrincipal =new List<List<string>>();
            var data = new List<string>();
            data.Add("Solicitud");
            data.Add("Solicitud de Débito Automático");
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
            data.Add("");
            data.Add("");
            dataPrincipal.Add(data);

            data = new List<string>();
            data.Add("<strong>Datos de la tarjeta</strong>");
            data.Add("");
            dataPrincipal.Add(data);

            data = new List<string>();
            data.Add("Nombre del producto");
            data.Add(debitoAutomatico.NombreProducto);
            dataPrincipal.Add(data);

            data = new List<string>();
            data.Add("N° Tarjeta");
            data.Add(debitoAutomatico.NumeroTarjeta);
            dataPrincipal.Add(data);

            if (!string.IsNullOrEmpty(debitoAutomatico.NumeroCuentaSoles))
            {
                data = new List<string>();
                data.Add("");
                data.Add("");
                dataPrincipal.Add(data);

                data = new List<string>();
                data.Add("<strong>Datos de la facturación en soles</strong>");
                data.Add("");
                dataPrincipal.Add(data);

                data = new List<string>();
                data.Add("Nombre del banco");
                data.Add(debitoAutomatico.BancoSoles);
                dataPrincipal.Add(data);

                data = new List<string>();
                data.Add("Tipo de cuenta");
                data.Add(debitoAutomatico.TipoCuentaSoles);
                dataPrincipal.Add(data);

                data = new List<string>();
                data.Add("Nº Cuenta");
                data.Add(debitoAutomatico.NumeroCuentaSoles);
                dataPrincipal.Add(data);

                data = new List<string>();
                data.Add("Moneda de la cuenta");
                data.Add(debitoAutomatico.MonedaDelaCtaSoles);
                dataPrincipal.Add(data);
            }
            if (!string.IsNullOrEmpty(debitoAutomatico.NumeroCuentaDolares))
            {
                data = new List<string>();
                data.Add("");
                data.Add("");
                dataPrincipal.Add(data);

                data = new List<string>();
                data.Add("<strong>Datos de la facturación en dólares</strong>");
                data.Add("");
                dataPrincipal.Add(data);

                data = new List<string>();
                data.Add("Nombre del banco");
                data.Add(debitoAutomatico.BancoDolares);
                dataPrincipal.Add(data);

                data = new List<string>();
                data.Add("Tipo de cuenta");
                data.Add(debitoAutomatico.TipoCuentaDolares);
                dataPrincipal.Add(data);

                data = new List<string>();
                data.Add("Nº Cuenta");
                data.Add(debitoAutomatico.NumeroCuentaDolares);
                dataPrincipal.Add(data);

                data = new List<string>();
                data.Add("Moneda de la cuenta");
                data.Add(debitoAutomatico.MonedaDelaCtaDolares);
                dataPrincipal.Add(data);
            }
            data = new List<string>();
            data.Add("Tipo de pago");
            data.Add(debitoAutomatico.TipoPagoaCargar);
            dataPrincipal.Add(data);

            try
            {
                var contentParaUsuario = EmailHelper.CrearHtmlOperacionEmail(usuario.Socio.NombreCompleto, dataPrincipal);

                EmailSenderService.SendEmail(asuntoEmail, contentParaUsuario, correoDiners, emailsTo, null, null, null);
                data = new List<string>();
                data.Add("<strong>Datos del socio</strong>");
                data.Add("");
                dataPrincipal.Insert(4,data);

                data = new List<string>();
                data.Add("Nombre completo");
                data.Add(string.Format("{0} {1} {2}", usuario.Socio.Nombre, usuario.Socio.ApellidoPaterno, usuario.Socio.ApellidoMaterno));
                dataPrincipal.Insert(5,data);

                data = new List<string>();
                data.Add("Tipo de documento");
                data.Add(usuario.Socio.TipoDocumentoIdentidad);
                dataPrincipal.Insert(6,data);

                data = new List<string>();
                data.Add("N° Documento");
                data.Add(usuario.Socio.NumeroDocumentoIdentidad);
                dataPrincipal.Insert(7,data);

                data = new List<string>();
                data.Add("Correo personal");
                data.Add(usuario.EmailPrincipal);
                dataPrincipal.Insert(8,data);

                data = new List<string>();
                data.Add("Celular");
                data.Add(usuario.NumeroCelular);
                dataPrincipal.Insert(9,data);

                var contentParaDiners = EmailHelper.CrearHtmlOperacionEmail(usuario.Socio.NombreCompleto, dataPrincipal);

                EmailSenderService.SendEmail(asuntoEmail, contentParaDiners, correoDiners, emailsToDiners, null, null, null);
            }
            catch (System.Exception) { throw; }
        }
    }
}