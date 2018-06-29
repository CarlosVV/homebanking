using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinersClubOnline.Model;

namespace DinersClubOnline.FakeRepositories
{
    public class MovimientoFakeRepository : IMovimientoRepository
    {
        public Task<List<Movimiento>> UltimosMovimientosAsync(string idTarjetaTitular, int numeroMovimientos)
        {
            var tarjeta = Data.TarjetasTitulares.FirstOrDefault(x => x.Id == idTarjetaTitular);

            if (tarjeta == null)
            {
                //throw new ArgumentException("La tarjeta: " + idTarjetaTitular + " no existe", idTarjetaTitular);
                return Task.FromResult(new List<Movimiento>());
            }

            return Task.FromResult(tarjeta.MovimientosTotales.OrderByDescending(x => x.Fecha).Take(numeroMovimientos).ToList());
        }

        public Task<BuscarMovimientosResult> BuscarMovimientosAsync(string idTarjetaTitular, string idTarjetaConsulta, DateTime? fechaInicio, DateTime? fechaFin,
            int cantidadMovimientos, int pagina)
        {
            if (cantidadMovimientos < 0) throw new ArgumentOutOfRangeException("cantidadMovimientos", "cantidadMovimientos debe ser >= 0");
            if (pagina < 0) throw new ArgumentOutOfRangeException("pagina", "pagina debe ser >= 1");

            var tarjeta = Data.TarjetasTitulares.FirstOrDefault(x => x.Id == idTarjetaTitular);

            if (tarjeta == null)
            {
                //throw new ArgumentException("La tarjeta: " + idTarjetaTitular + " no existe", idTarjetaTitular);
                return Task.FromResult(new BuscarMovimientosResult());
            }

            var totalMovimientos = tarjeta.MovimientosTotales
                .Where(x =>
                    (String.IsNullOrEmpty(idTarjetaConsulta) || x.Tarjeta.Id == idTarjetaConsulta) &&
                    (!fechaInicio.HasValue || x.Fecha.Date >= fechaInicio.Value) &&
                    (!fechaFin.HasValue || x.Fecha.Date <= fechaFin.Value)
                ).ToList();

            if (!totalMovimientos.Any())
            {
                //throw new ArgumentException("no existen datos para la busqueda");
                return Task.FromResult(new BuscarMovimientosResult
                {
                    Resultados = new List<Movimiento>(),
                    TotalMovimientos = 0
                });
            }
            var movimientosPaginados = ( (pagina == 0) && (cantidadMovimientos == 0)) ? totalMovimientos :  totalMovimientos.Skip((pagina - 1) * cantidadMovimientos).Take(cantidadMovimientos).ToList() ;

            return Task.FromResult(new BuscarMovimientosResult
            {
                TotalMovimientos = totalMovimientos.Count(),
                Resultados = movimientosPaginados
            });
        }
    }
}