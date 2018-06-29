using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DinersClubOnline.Model
{
    public interface IMovimientoRepository
    {
        Task<List<Movimiento>> UltimosMovimientosAsync(string idTarjetaTitular, int numeroMovimientos);
        Task<BuscarMovimientosResult> BuscarMovimientosAsync(string idTarjetaTitular, string idTarjetaConsulta, DateTime? fechaInicio, DateTime? fechaFin, int cantidadMovimientos, int pagina);
    }
}