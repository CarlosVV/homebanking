using DinersClubOnline.Api.Filters;
using DinersClubOnline.Api.Infrastructure;
using DinersClubOnline.Api.Models;
using DinersClubOnline.Api.Models.Solicitudes;
using DinersClubOnline.Api.Principals;
using DinersClubOnline.Api.Properties;
using DinersClubOnline.Mail.EmailService;
using DinersClubOnline.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace DinersClubOnline.Api.Controllers
{
    [ScopeAuthorize(Scopes.Consultas)]
    [RoutePrefix("api/tarjetas")]
    public class TarjetasController : ApiController
    {
        private readonly ITarjetaRepository _tarjetaRepository;
        private readonly IMovimientoRepository _movimientoRepository;
        private readonly IEstadoDeCuentaRepository _estadoDeCuentaRepository;
        private readonly IAlertaRepository _alertaRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public TarjetasController(ITarjetaRepository tarjetaRepository, IMovimientoRepository movimientoRepository, IEstadoDeCuentaRepository estadoDeCuentaRepository, IAlertaRepository alertaRepository, IUsuarioRepository usuarioRepository)
        {
            _tarjetaRepository = tarjetaRepository;
            _movimientoRepository = movimientoRepository;
            _estadoDeCuentaRepository = estadoDeCuentaRepository;
            _alertaRepository = alertaRepository;
            _usuarioRepository = usuarioRepository;
        }

        /// <summary>Obtiene la lista de tarjetas del usuario</summary>
        [Route]
        public async Task<IEnumerable<TarjetaGetViewModel>> Get()
        {
            var tarjetas = await _tarjetaRepository.ObtenerTarjetasPorUsuarioAsync(User.ToDinersUser().IdUsuario);

            return (tarjetas).Select(x => new TarjetaGetViewModel
            {
                IdTarjeta = x.Id,
                NumeroTarjeta = x.NumeroTarjeta,
                Alias = x.Alias,
                NombreProducto = x.Producto.Nombre,
                LineaCreditoSoles = x.LineaCreditoSoles,
                LineaCreditoDolares = x.LineaCreditoDolares,
                LineaDisponibleSoles = x.LineaDisponibleSoles,
                LineaDisponibleDolares = x.LineaDisponibleDolares,
                MillasDisponibles = x.MillasDisponibles,
                MontoTotalMesSoles = x.MontoTotalSoles,
                MontoTotalMesDolares = x.MontoTotalDolares,
                MontoMinimoMesSoles = x.MontoMinimoSoles,
                MontoMinimoMesDolares = x.MontoMinimoDolares,
                EstadoTarjeta = (DinersClubOnline.Api.Models.EstadoTarjeta)x.EstadoTarjeta,
                NumeroOperacion = x.NumeroOperacion,
                FechaOperacion = x.FechaOperacion,
                FechaVencimiento = x.EstadoDeCuenta.FechaVencimiento,
                Pagado = !x.EstadoDeCuenta.Vencido,
                SocioTarjeta = x.Socio == null ? null : new SocioViewModel
                {
                    Nombre = x.Socio.Nombre,
                    ApellidoMaterno = x.Socio.ApellidoMaterno,
                    ApellidoPaterno = x.Socio.ApellidoPaterno,
                    SegundoNombre = x.Socio.SegundoNombre,
                },
                Adicionales = x.Adicionales == null ? null : x.Adicionales.Select(t => new TarjetaGetViewModel
                {
                    IdTarjeta = t.Id,
                    NumeroTarjeta = t.NumeroTarjeta,
                    Alias = t.Alias,
                    NombreProducto = t.Producto.Nombre,
                    EstadoTarjeta = (DinersClubOnline.Api.Models.EstadoTarjeta)t.EstadoTarjeta,
                    NumeroOperacion = t.NumeroOperacion,
                    FechaOperacion = t.FechaOperacion,
                    SocioTarjeta = t.Socio == null ? null : new SocioViewModel
                    {
                        Nombre = t.Socio.Nombre,
                        ApellidoMaterno = t.Socio.ApellidoMaterno,
                        ApellidoPaterno = t.Socio.ApellidoPaterno,
                        SegundoNombre = t.Socio.SegundoNombre,
                    },
                }).ToList(),
                AcumulaMillas = x.Producto.AcumulaMillas,
                TieneAdicionales = x.TieneAdicionales
            });
        }

        /// <summary>Obtiene el detalle de una tarjeta</summary>
        [Route("{id}")]
        public async Task<TarjetaGetViewModel> Get(string id)
        {
            return (await _tarjetaRepository.ObtenerTarjetasPorUsuarioAsync(User.ToDinersUser().IdUsuario)).Select(x => new TarjetaGetViewModel
            {
                IdTarjeta = x.Id,
                Alias = x.Alias,
                NombreProducto = x.Producto.Nombre,
                LineaCreditoSoles = x.LineaCreditoSoles,
                LineaCreditoDolares = x.LineaCreditoDolares,
                LineaDisponibleSoles = x.LineaDisponibleSoles,
                LineaDisponibleDolares = x.LineaDisponibleDolares,
                MillasDisponibles = x.MillasDisponibles,
                MontoTotalMesSoles = x.MontoTotalSoles,
                MontoTotalMesDolares = x.MontoTotalDolares,
                MontoMinimoMesSoles = x.MontoMinimoSoles,
                MontoMinimoMesDolares = x.MontoMinimoSoles,
                FechaVencimiento = x.EstadoDeCuenta.FechaVencimiento,
                Pagado = !x.EstadoDeCuenta.Vencido,
                NumeroTarjeta = x.NumeroTarjeta,
                NumeroOperacion = x.NumeroOperacion,
                FechaOperacion = x.FechaOperacion
            }).FirstOrDefault(x => x.IdTarjeta == id);
        }

        /// <summary>Obtiene el detalle de una tarjeta</summary>
        [Route("{id}/adicionales")]
        [HttpGet]
        public async Task<TarjetaGetViewModel> GetAdicionales(string id)
        {
            return (await _tarjetaRepository.ObtenerTarjetasPorUsuarioAsync(User.ToDinersUser().IdUsuario)).Select(x => new TarjetaGetViewModel
            {
                IdTarjeta = x.Id,
                Alias = x.Alias,
                NombreProducto = x.Producto.Nombre,
                NumeroTarjeta = x.NumeroTarjeta,
                LineaCreditoSoles = x.LineaCreditoSoles,
                LineaCreditoDolares = x.LineaCreditoDolares,
                LineaDisponibleSoles = x.LineaDisponibleSoles,
                LineaDisponibleDolares = x.LineaDisponibleDolares,
                MillasDisponibles = x.MillasDisponibles,
                MontoTotalMesSoles = x.MontoTotalSoles,
                MontoTotalMesDolares = x.MontoTotalDolares,
                MontoMinimoMesSoles = x.MontoMinimoSoles,
                MontoMinimoMesDolares = x.MontoMinimoDolares,
                FechaVencimiento = x.EstadoDeCuenta.FechaVencimiento,
                Pagado = !x.EstadoDeCuenta.Vencido,
                SocioTarjeta = x.Socio == null ? null : new SocioViewModel
                {
                    Nombre = x.Socio.Nombre,
                    ApellidoMaterno = x.Socio.ApellidoMaterno,
                    ApellidoPaterno = x.Socio.ApellidoPaterno,
                    SegundoNombre = x.Socio.SegundoNombre,
                },
                AcumulaMillas = x.Producto.AcumulaMillas,
                TieneAdicionales = x.TieneAdicionales,
                Adicionales = x.Adicionales.Select(a => new TarjetaGetViewModel
                {
                    IdTarjeta = a.Id,
                    Alias = a.Alias,
                    NombreProducto = a.Producto.Nombre,
                    NumeroTarjeta = a.NumeroTarjeta,
                    LineaCreditoSoles = a.LineaCreditoSoles,
                    LineaCreditoDolares = a.LineaCreditoDolares,
                    LineaDisponibleSoles = a.LineaDisponibleSoles,
                    LineaDisponibleDolares = a.LineaDisponibleDolares,
                    MillasDisponibles = a.MillasDisponibles,
                    MontoTotalMesSoles = a.MontoTotalSoles,
                    MontoTotalMesDolares = a.MontoTotalDolares,
                    MontoMinimoMesSoles = a.MontoMinimoSoles,
                    MontoMinimoMesDolares = a.MontoMinimoDolares,
                    FechaVencimiento = a.EstadoDeCuenta.FechaVencimiento,
                    Pagado = !a.EstadoDeCuenta.Vencido,
                    SocioTarjeta = a.Socio == null ? null : new SocioViewModel
                    {
                        Nombre = a.Socio.Nombre,
                        ApellidoMaterno = a.Socio.ApellidoMaterno,
                        ApellidoPaterno = a.Socio.ApellidoPaterno,
                        SegundoNombre = a.Socio.SegundoNombre,
                    },
                    AcumulaMillas = a.Producto.AcumulaMillas
                }).ToList()
            }).FirstOrDefault(t => t.IdTarjeta == id);
        }

        /// <summary>Obtiene los movimientos de la tarjeta por filtros</summary>
        /// <param name="id">Id de la tarjeta titular</param>
        /// <param name="idTarjetaConsulta">Id de la tarjeta a filtrar, si es null se devuelven los movimientos de la tarjeta titular y de sus adicionales</param>
        /// <param name="fechaInicio">Fecha de inicio para filtrar resultados</param>
        /// <param name="fechaFin">Fecha de fin para filtrar resultados</param>
        /// <param name="cantidadMovimientos">Número de movimientos a devolver</param>
        /// <param name="pagina">Pagina de resultados a devolver</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}/movimientos")]
        [ResponseType(typeof(ResumenMovimientoViewModel))]
        public async Task<IHttpActionResult> GetMovimientos(string id, int cantidadMovimientos, int pagina, string idTarjetaConsulta = null, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            var response = await _movimientoRepository.BuscarMovimientosAsync(id, idTarjetaConsulta, fechaInicio, fechaFin, cantidadMovimientos, pagina);

            if (response == null || response.Resultados == null)
            {
                return Ok();
            }

            return Ok(new ResumenMovimientoViewModel
            {
                Movimientos = response.Resultados.Select(m => new MovimientoViewModel
                {
                    Fecha = m.Fecha,
                    Descripcion = m.Descripcion,
                    Cuotas = m.Cuotas,
                    ImporteSoles = m.ImporteSoles,
                    ImporteDolares = m.ImporteDolares,
                    Socio = new SocioViewModel
                    {
                        Nombre = m.Tarjeta.Socio.Nombre,
                        SegundoNombre = m.Tarjeta.Socio.SegundoNombre,
                        ApellidoPaterno = m.Tarjeta.Socio.ApellidoPaterno,
                        ApellidoMaterno = m.Tarjeta.Socio.ApellidoMaterno
                    },
                    PendienteProcesamiento = m.PendienteProcesamiento,
                    Lugar = m.Lugar
                }),
                TotalMovimientos = response.TotalMovimientos
            });
        }

        /// <summary>Obtiene los ultimos n movimientos de una tarjeta</summary>
        /// <param name="id">Identificador de tarjeta</param>
        /// <param name="movimientos">Número de últimos movimientos a devolver</param>
        /// <returns>Lista de movimientos</returns>
        [HttpGet]
        [Route("{id}/movimientos/ultimos")]
        [ResponseType(typeof(UltimosMovimientosViewModel))]
        public async Task<IHttpActionResult> GetUltimosMovimientos(string id, int movimientos)
        {
            var response = await _movimientoRepository.UltimosMovimientosAsync(id, movimientos);
            if (response == null || !response.Any())
            {
                return Ok();
            }
            return Ok(new UltimosMovimientosViewModel
            {
                Movimientos = response.Select(m => new MovimientoViewModel
                {
                    Fecha = m.Fecha,
                    Descripcion = m.Descripcion,
                    Cuotas = m.Cuotas,
                    ImporteSoles = m.ImporteSoles,
                    ImporteDolares = m.ImporteDolares,
                    Socio = new SocioViewModel
                    {
                        Nombre = m.Tarjeta.Socio.Nombre,
                        SegundoNombre = m.Tarjeta.Socio.SegundoNombre,
                        ApellidoPaterno = m.Tarjeta.Socio.ApellidoPaterno,
                        ApellidoMaterno = m.Tarjeta.Socio.ApellidoMaterno
                    },
                    PendienteProcesamiento = m.PendienteProcesamiento,
                    Lugar = m.Lugar
                })
            });
        }

        [HttpGet]
        [Route("{id}/estadoCuenta")]
        [ResponseType(typeof(EstadoCuentaViewModel))]
        public async Task<IHttpActionResult> GetEstadoCuenta(string id)
        {
            try
            {
                var response = await _estadoDeCuentaRepository.ObtenerEstadoDeCuentaActualAsync(id);
                var responseTarjeta = await _tarjetaRepository.ObtenerTarjetasPorUsuarioAsync(User.ToDinersUser().IdUsuario);
                var tarjeta = responseTarjeta.FirstOrDefault(t => t.Id == id);
                if (response != null)
                {
                    var estadoCuenta = new EstadoCuentaViewModel
                    {
                        FechaInicioPeriodo = response.FechaInicioPeriodo,
                        FechaFinPeriodo = response.FechaFinPeriodo,
                        FechaVencimiento = response.FechaVencimiento,
                        EstaVencido = response.Vencido,
                        DeudaAnteriorSoles = response.DeudaAnteriorSoles,
                        DeudaAnteriorDolares = response.DeudaAnteriorDolares,
                        AbonoSoles = response.AbonosSoles,
                        AbonoDolares = response.AbonosDolares,
                        ConsumosSoles = response.ConsumosSoles,
                        ConsumosDolares = response.ConsumosDolares,
                        ComisionesInteresesPenalidadesGastosSoles = response.ComisionesSoles,
                        ComisionesInteresesPenalidadesGastosDolares = response.ComisionesDolares,
                        DeudaTotalSoles = response.DeudaTotalSoles,
                        DeudaTotalDolares = response.DeudaTotalDolares,
                        MontoAPagarMinimoSoles = tarjeta.MontoMinimoSoles,
                        MontoAPagarMinimoDolares = tarjeta.MontoMinimoDolares,
                        MontoAPagarTotalSoles = tarjeta.MontoTotalSoles,
                        MontoAPagarTotalDolares = tarjeta.MontoTotalDolares,
                        MillasSaldoAnterior = response.MillasSaldoAnterior,
                        MillasGanadas = response.MillasGanadas,
                        MillasVencidas = response.MillasVencidas,
                        MillasDisponibles = response.MillasDisponibles,
                        AcumulaMillas = tarjeta.Producto != null ? tarjeta.Producto.AcumulaMillas : false
                    };

                    return Ok(estadoCuenta);
                }
                return NotFound();
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("{id}/bloquear")]
        [ResponseType(typeof(BloquearTarjetaResult))]
        public async Task<IHttpActionResult> PostBloquear(string id, BloquearTarjetaViewModel bloquearTarjeta)
        {
            var tarjetasTitulares = await _tarjetaRepository.ObtenerTarjetasPorUsuarioAsync(User.ToDinersUser().IdUsuario);
            var tarjetasAdicionales = tarjetasTitulares.SelectMany(x => x.Adicionales).ToList();
            var tarjeta = tarjetasTitulares.Concat(tarjetasAdicionales).FirstOrDefault(x => x.Id == id);

            if (tarjeta == null)
            {
                return NotFound();
            }

            var result = await _tarjetaRepository.BloquearAsync(tarjeta.Id, bloquearTarjeta.IdMotivo);
            var usuario = await _usuarioRepository.ObtenerUsuarioAsync(User.ToDinersUser().IdUsuario);

            if (!result)
                return NotFound();

            tarjeta.NumeroOperacion = tarjetasTitulares.Concat(tarjetasAdicionales).Count(x => x.NumeroOperacion != 0) + 1;
            tarjeta.FechaOperacion = DateTime.Now;

            EmailHelper.BloqueoTarjeta_ProcesarEnviarCorreo(bloquearTarjeta, usuario, tarjeta.NumeroOperacion.ToString(), tarjeta.FechaOperacion);

            return Ok(new BloquearTarjetaResult
            {
                NumeroOperacion = tarjeta.NumeroOperacion,
                FechaOperacion = tarjeta.FechaOperacion
            });
        }

        [HttpGet]
        [Route("{id}/estadoCuentaDescargar")]
        public async Task<IHttpActionResult> GetListaDescargasEstadoCuenta(string id)
        {
            try
            {
                var response = await _estadoDeCuentaRepository.ObtenerUltimosEstadosDeCuentaAsync(id);

                if (response != null)
                {
                    var listaEstadoCuentaDescargas = response.Select(e => new EstadoCuentaDescargaViewModel
                    {
                        LinkDescarga = e.UrlDescarga,
                        PeriodoDescarga = new DateTime(e.Periodo.Anho, e.Periodo.Mes, 1).ToString("MMMM, yyyy")
                    });
                    return Ok(listaEstadoCuentaDescargas);
                }
                return NotFound();
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("{id}/descargarMovimientosHistoricos")]
        public async Task<HttpResponseMessage> DescargarMovimientosHistoricos(string id, string idTarjetaConsulta, DateTime? fechaInicio, DateTime? fechaFin)
        {
            var movimientosResult = await _movimientoRepository.BuscarMovimientosAsync(id, idTarjetaConsulta, fechaInicio, fechaFin, 0, 0);

            //Crear un stream a partir del byte[] con el archivo que sale del archivo de recursos
            var stream = new MemoryStream();
            await stream.WriteAsync(Resources.PlantillaMovimientos, 0, Resources.PlantillaMovimientos.Length);

            //Actualizar el excel con los movimientos
            ExcelMovimientos.ExcelMovimientosHelper.ActualizarStreamExcelConMovimientos(stream, movimientosResult.Resultados);

            //Resetear la lectura del stream al inicio para que pueda ser devuelto por el response desde el comienzo
            stream.Seek(0, SeekOrigin.Begin);

            //Retornar el archivo por el response
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(stream)
                {
                    Headers =
                    {
                        ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"),
                        ContentDisposition = new ContentDispositionHeaderValue("attachment")
                        {
                            FileName = "Data.xlsx"
                        }
                    }
                }
            };
        }

        [HttpGet]
        [Route("{id}/descargarPdfMovimientosHistoricos")]
        public async Task<HttpResponseMessage> DescargarPdfMovimientosHistoricos(string id, string idTarjetaConsulta, DateTime? fechaInicio, DateTime? fechaFin)
        {
            var response = await _movimientoRepository.BuscarMovimientosAsync(id, idTarjetaConsulta, fechaInicio, fechaFin, 0, 0);

            if (response == null) { return new HttpResponseMessage(HttpStatusCode.NotFound); }
            if (!response.Resultados.Any()) { return new HttpResponseMessage(HttpStatusCode.OK); }

            
           
            var stream = DinersClubOnline.Api.Infrastructure.Pdf.CrearPdfHelper.CrearPdfConMovimientos(response.Resultados, fechaInicio.Value, fechaFin.Value);
            var streamResult = new MemoryStream(stream.GetBuffer());
            streamResult.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(streamResult)
                {
                    Headers =
                    {
                        ContentType = new MediaTypeHeaderValue("application/pdf"),
                        ContentDisposition = new ContentDispositionHeaderValue("attachment")
                        {
                            FileName = "test"
                        }
                    }
                }
            };
        }

        [HttpPost]
        [Route("{id}/cambiar-alias")]
        public async Task<IHttpActionResult> PostCambiarAlias(string id, string alias)
        {

            if (string.IsNullOrEmpty(alias))
            {
                return BadRequest("Debe ingresar el alias");
            }
            try
            {
                var responseTarjeta = await _tarjetaRepository.CambiarAliasAsync(User.ToDinersUser().IdUsuario, id, alias);
                return Ok(responseTarjeta);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(BancoAfiliadoViewModel))]
        [HttpGet]
        [Route("{id}/alertas")]
        public async Task<AlertaTarjetaUsuarioViewModel> ObtenerAlertasTajeta(string id)
        {
            var data = await _alertaRepository.ObtenerAlertasPorTarjetaAsync(User.ToDinersUser().IdUsuario, id);

            if (data == null)
            {
                return new AlertaTarjetaUsuarioViewModel()
                {
                    IdTarjeta = id,
                    IdUsuario = User.ToDinersUser().IdUsuario
                };
            }

            return new AlertaTarjetaUsuarioViewModel
            {
                IdTarjeta = data.IdTarjeta,
                IdUsuario = data.IdUsuario,
                EmailSeleccionado = data.EmailSeleccionado,
                EmailAdicional = data.EmailAdicional,
                TelefonoAdicional = data.TelefonoAdicional,
                Activo = data.Activo,
                AlertasActivas = data.AlertasActivas.Select(m => new AlertaTarjetaViewModel
                {
                    IdAlerta = m.IdAlerta,
                    IdCondicionAdicional = m.IdCondicionAdicional,
                    Celular1Activo = m.Celular1Activo,
                    Celular2Activo = m.Celular2Activo,
                    Email1Activo = m.Email1Activo,
                    Email2Activo = m.Email2Activo
                }).ToList()
            };
        }

        [ResponseType(typeof(IHttpActionResult))]
        [HttpPut]
        [Route("{id}/alertas")]
        public async Task<IHttpActionResult> GuardarAlertasTarjeta(string id, [FromBody]AlertaTarjetaUsuarioViewModel modelo)
        {
            var alertaTarjet = new AlertaTarjetaUsuario
            {
                IdTarjeta = modelo.IdTarjeta,
                IdUsuario = User.ToDinersUser().IdUsuario,
                EmailSeleccionado = modelo.EmailSeleccionado,
                EmailAdicional = modelo.EmailAdicional,
                TelefonoAdicional = modelo.TelefonoAdicional,
                Activo = modelo.Activo,
                AlertasActivas = modelo.AlertasActivas.Select(m => new AlertaTarjeta
                {
                    IdAlerta = m.IdAlerta,
                    IdCondicionAdicional = m.IdCondicionAdicional,
                    Celular1Activo = m.Celular1Activo,
                    Celular2Activo = m.Celular2Activo,
                    Email1Activo = m.Email1Activo,
                    Email2Activo = m.Email2Activo
                }).ToList()
            };

            var data = await _alertaRepository.ActualizarAlertasAsync(alertaTarjet);
            var usuario = await _usuarioRepository.ObtenerUsuarioAsync(User.ToDinersUser().IdUsuario);
            if (data && modelo.DatosCorreo != null)
            {
                SendEmail(usuario, modelo.DatosCorreo);
                
            }
            
            return Ok(data);
        }

        private void SendEmail(Usuario usuario, List<Diccionario> data)
        {
            var correoDiners = System.Configuration.ConfigurationManager.AppSettings["CorreoDinersSac"];
            var emailsTo = new List<string>();
            emailsTo.Add(usuario.EmailPrincipal);

            var emailsCc = new List<string>();
            emailsCc.Add(usuario.EmailAlternativo);

            try
            {
                var content = EmailHelper.CrearHtmlOperacionEmail(usuario.Socio.NombreCompleto, data);

                EmailSenderService.SendEmail("Canal Web – SOLICITUD CARGO AUTOMATICO", content, correoDiners, emailsTo, new List<string>(), emailsCc, null);
            }
            catch (System.Exception) { throw; }
        }

		[ResponseType(typeof(CorreoFiltrosViewModel))]
		[HttpPost]
        [Route("{id}/enviar-correo")]
        public async Task<IHttpActionResult> PostEnviarCorreo(CorreoFiltrosViewModel model)
        {
            var response = await _movimientoRepository.BuscarMovimientosAsync(model.Id, model.IdTarjetaConsulta, model.FechaInicio, model.FechaFin, 0, 0);

            if (response == null) { return NotFound(); }
            if (!response.Resultados.Any()) { return Ok(); }

            var stream = DinersClubOnline.Api.Infrastructure.Pdf.CrearPdfHelper.CrearPdfConMovimientos(response.Resultados, model.FechaInicio.Value, model.FechaFin.Value);
            var streamResult = new MemoryStream(stream.GetBuffer());
            streamResult.Seek(0, SeekOrigin.Begin);

            var streamDic = new Dictionary<MemoryStream, string>();
            streamDic.Add(streamResult, "MovimientosHistoricos.pdf");

            EmailHelper.Movimientos_ProcesarEnviarCorreo(model.Correo, "Movimientos Históricos", streamDic);

            return Ok();
        }
    }
}