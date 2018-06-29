using DinersClubOnline.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinersClubOnline.Services.Client.Common;
using DinersClubOnline.Services.Client.TarjetaService;
using Tarjeta = DinersClubOnline.Model.Tarjeta;

namespace DinersClubOnline.Repositories
{
    public class TarjetaRepository : ITarjetaRepository
    {
        private readonly Services.Client.TarjetaService.Tarjeta _tarjetaService;

        public TarjetaRepository(Services.Client.TarjetaService.Tarjeta tarjetaService)
        {
            _tarjetaService = tarjetaService;
        }

        public Task<Tarjeta> ObtenerTarjetaAsync(string idTarjeta)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Tarjeta>> ObtenerTarjetasPorUsuarioAsync(string idUsuario)
        {
            var response = await _tarjetaService.listarTarjetaAsync(new listarTarjetaRequest(new listarTarjeta
            {
                arg0 = new requestListarTarjeta
                {
                    idUsuario = idUsuario
                }
            }));

            var result = response.listarTarjetaResponse.@return;

            if (result.codigoMensaje != DinersResponseCodes.EjecucionExitosa)
                throw new Exception(result.mensaje);

            var tarjetas = result.tarjetas.Select(x =>
            {
                var detalleResponse =
                    _tarjetaService.obtenerDetalleTarjeta(new obtenerDetalleTarjetaRequest(new obtenerDetalleTarjeta
                    {
                        arg0 = new requestObtenerDetalleTarjeta
                        {
                            idTarjeta = x.idTarjeta,
                        }
                    }));

                var detalleTarjetaResult = detalleResponse.obtenerDetalleTarjetaResponse.@return;

                var tarjeta = new Tarjeta
                {
                    Id = x.idTarjeta,
                    Alias = x.alias,
                    Producto = new Producto
                    {
                        Nombre = x.nombreProducto,
                        AcumulaMillas = true //TODO: Agregar campo al servicio
                    },
                    //TODO: Parsear los números según los servicios del banco.
                    LineaCreditoSoles = Decimal.Parse(detalleTarjetaResult.lineaTotalSoles),
                    LineaCreditoDolares = Decimal.Parse("TODO: detalleTarjetaResult.lineaTotalDolares"),
                    LineaDisponibleSoles = Decimal.Parse(detalleTarjetaResult.lineaDisponibleSoles),
                    LineaDisponibleDolares = Decimal.Parse(detalleTarjetaResult.lineaDisponibleDolares),
                    MillasDisponibles = Int32.Parse(detalleTarjetaResult.millas),

                    //MontoTotalMesSoles = /*x.MontoTotalMesSoles*/,
                    //MontoTotalMesDolares = x.MontoTotalMesDolares,
                    //MontoMinimoMesSoles = x.MontoMinimoMesSoles,
                    //MontoMinimoMesDolares = x.MontoMinimoMesDolares,
                    //FechaVencimiento = x.FechaVencimiento,
                    //Pagado = x.Pagado,
                    //IdTarjetaMaster = x.IdTarjetaMaster,
                    //SocioTarjeta = x.SocioTarjeta == null ? null : new Socio
                    //{
                    //    Nombre = x.SocioTarjeta.Nombre,
                    //    ApellidoMaterno = x.SocioTarjeta.ApellidoMaterno,
                    //    ApellidoPaterno = x.SocioTarjeta.ApellidoPaterno,
                    //    SegundoNombre = x.SocioTarjeta.SegundoNombre,
                    //    IdTarjeta = x.SocioTarjeta.IdTarjeta
                    //},
                    //AcumulaMillas = x.AcumulaMillas
                };

                return tarjeta;
            });

            return tarjetas.ToList();
        }

        public Task<bool> CambiarAliasAsync(string idUsuario, string idTarjetaTitular, string nuevoAlias)
        {
            throw new NotImplementedException();
        }


        public Task<bool> BloquearAsync(string idTarjeta, string idMotivo)
        {
            throw new NotImplementedException();
        }
    }
}