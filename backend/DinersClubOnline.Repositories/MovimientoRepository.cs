using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DinersClubOnline.Model;

namespace DinersClubOnline.Repositories
{
    public class MovimientoRepository : IMovimientoRepository
    {
        public Task<List<Movimiento>> UltimosMovimientosAsync(string idTarjetaTitular, int numeroMovimientos)
        {
            throw new NotImplementedException();
        }

        public Task<BuscarMovimientosResult> BuscarMovimientosAsync(string idTarjetaTitular, string idTarjetaConsulta, DateTime? fechaInicio, DateTime? fechaFin,
            int cantidadMovimientos, int pagina)
        {
            throw new NotImplementedException();
        }
    }
}