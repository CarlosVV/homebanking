using DinersClubOnline.Api.Filters;
using DinersClubOnline.Api.Infrastructure;
using DinersClubOnline.Api.Models.Solicitudes;
using DinersClubOnline.Api.Principals;
using DinersClubOnline.Mail.EmailService;
using DinersClubOnline.Model;
using DinersClubOnline.Model.Solicitudes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace DinersClubOnline.Api.Controllers.Solicitudes
{
    [ScopeAuthorize(Scopes.Consultas)]
    [RoutePrefix("api/solicitudes/cargo-automatico")]
    public class CargoAutomaticoController : ApiController
    {
        private readonly ICargoAutomaticoRepository _cargoAutomaticoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public CargoAutomaticoController(ICargoAutomaticoRepository cargoAutomaticoRepository, IUsuarioRepository usuarioRepository)
        {
            _cargoAutomaticoRepository = cargoAutomaticoRepository;
            _usuarioRepository = usuarioRepository;
        }

        [Route]
        [ResponseType(typeof(SolicitudResponseViewModel))]
        public async Task<IHttpActionResult> Post(CargoAutomaticoViewModel modelo)
        {
            var cargoAutomatico = new CargoAutomatico
            {
                IdSocio = User.ToDinersUser().IdUsuario,
                IdTipoOferta = modelo.IdTipoOferta,

                IdTarjeta = modelo.IdTarjeta,
                TarjetaProducto = modelo.TarjetaProducto,
                TarjetaVence = modelo.TarjetaVence,
                TarjetaNumero = modelo.TarjetaNumero,

                IdCategoria = modelo.IdCategoria,
                CategoriaNombre = modelo.CategoriaNombre,

                IdEmpresa = modelo.IdEmpresa,
                EmpresaNombre = modelo.EmpresaNombre,

                IdServicio = modelo.IdServicio,
                ServicioNombre = modelo.ServicioNombre,

                DatoServicio = modelo.DatoServicio,
                MontoTope = modelo.MontoTope
            };

            var usuario = await _usuarioRepository.ObtenerUsuarioAsync(User.ToDinersUser().IdUsuario);
            cargoAutomatico.SocioNombres = string.Format("{0} {1} {2} {3}", usuario.Socio.Nombre, usuario.Socio.SegundoNombre, usuario.Socio.ApellidoPaterno, usuario.Socio.ApellidoMaterno);
            cargoAutomatico.SocioTipoDocumento = usuario.Socio.TipoDocumentoIdentidad;
            cargoAutomatico.SocioNumeroDocumento = usuario.Socio.NumeroDocumentoIdentidad;
            cargoAutomatico.SocioCorreElectronico = usuario.EmailPrincipal;
            cargoAutomatico.SocioCelular = usuario.NumeroCelular;

            var result = await _cargoAutomaticoRepository.GuardarAsync(cargoAutomatico);

            if (result.Resultado)
            {
                if (modelo.DatosCorreo != null)
                {
                    SendEmail(cargoAutomatico, usuario, modelo.DatosCorreo, result.FechaRegistro.Value, result.NumeroSolicitud);
                }

                return Created("api/solicitudes/cargo-automatico", new SolicitudResponseViewModel
                {
                    NumeroSolicitud = result.NumeroSolicitud,
                    FechaRegistro = result.FechaRegistro
                });
            }

            return BadRequest("An error ocurred");
        }

        private void SendEmail(CargoAutomatico cargoAutomatico, Usuario usuario, List<Diccionario> data, DateTime fechaOperacion, int? numeroOperacion)
        {
            var correoDiners = System.Configuration.ConfigurationManager.AppSettings["CorreoDinersSac"];
            var emailsTo = new List<string>();
            emailsTo.Add(usuario.EmailPrincipal);

            var emailsCc = new List<string>();
            emailsCc.Add(usuario.EmailAlternativo);

            try
            {
                var dataEmail = new List<Diccionario>(){
                    new Diccionario { Key = "Fecha y hora", Value = fechaOperacion.ToString("dd/MM/yyyy HH:mm")},
                    new Diccionario { Key = "Nº Solicitud", Value = numeroOperacion.ToString()}
                };

                data.ToList().ForEach(item =>
                {
                    dataEmail.Add(new Diccionario { Key = item.Key, Value = item.Value });
                });

                var content = EmailHelper.CrearHtmlOperacionEmail(usuario.Socio.NombreCompleto, data);

                EmailSenderService.SendEmail("Canal Web – SOLICITUD CARGO AUTOMATICO", content, correoDiners, emailsTo, new List<string>(), emailsCc, null);

                var dataEmailSac = new List<Diccionario>() { 
                    new Diccionario { Key = "Nº Solicitud", Value = numeroOperacion.ToString()},
                    new Diccionario { Key = "Fecha y hora", Value = fechaOperacion.ToString("dd/MM/yyyy HH:mm")},
                    new Diccionario { Key = "Tarjeta", Value = cargoAutomatico.TarjetaNumero},
                    new Diccionario { Key = "Categoría", Value = cargoAutomatico.CategoriaNombre},
                    new Diccionario { Key = "Empresa", Value = cargoAutomatico.EmpresaNombre},
                    new Diccionario { Key = "Servicio", Value = cargoAutomatico.ServicioNombre},
                    new Diccionario { Key = "Dato del servicio", Value = cargoAutomatico.DatoServicio},
                    new Diccionario { Key = "Nombres:", Value = cargoAutomatico.SocioNombres},
                    new Diccionario { Key = "Tipo y N° documento", Value = cargoAutomatico.SocioTipoDocumento + " " + cargoAutomatico.SocioNumeroDocumento},
                    new Diccionario { Key = "Correo Electronico", Value = cargoAutomatico.SocioCorreElectronico},
                    new Diccionario { Key = "Celular", Value = cargoAutomatico.SocioCelular}
                };

                var contenidoSAC = EmailHelper.CrearHtmlOperacionEmail(usuario.Socio.NombreCompleto, dataEmailSac);

                EmailSenderService.SendEmail("Canal Web – SOLICITUD CARGO AUTOMATICO", contenidoSAC, correoDiners, new List<string>() { correoDiners }, new List<string>(), emailsCc, null);
            }
            catch (System.Exception) { throw; }
        }
    }
}