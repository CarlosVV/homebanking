using System.Collections.Generic;
using System.Threading.Tasks;

namespace DinersClubOnline.Model
{
    public interface IEstadoDeCuentaRepository
    {

        Task<EstadoDeCuenta> ObtenerEstadoDeCuentaActualAsync(string idTarjeta);
        Task<List<EstadoCuentaDescargaPdf>> ObtenerUltimosEstadosDeCuentaAsync(string idTarjeta);
    }
}