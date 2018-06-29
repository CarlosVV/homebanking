using DinersClubOnline.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DinersClubOnline.FakeRepositories
{
    public class EstadoDeCuentaFakeRepository : IEstadoDeCuentaRepository
    {
        public Task<EstadoDeCuenta> ObtenerEstadoDeCuentaActualAsync(string idTarjeta)
        {
            var tarjeta = Data.TarjetasTitulares.FirstOrDefault(x => x.Id == idTarjeta);

            if (tarjeta == null)
            { //throw new ArgumentException("No hay estado de cuenta");
                return Task.FromResult(new EstadoDeCuenta());
            }
            return Task.FromResult(tarjeta.EstadoDeCuenta);
        }

        public Task<List<EstadoCuentaDescargaPdf>> ObtenerUltimosEstadosDeCuentaAsync(string idTarjeta)
        {
            var tarjeta = Data.TarjetasTitulares.FirstOrDefault(x => x.Id == idTarjeta);

            if (tarjeta == null)
            { // throw new ArgumentException("No hay estado de cuenta anteriores"); 
                return Task.FromResult(new List<EstadoCuentaDescargaPdf>());
            }
            return Task.FromResult(tarjeta.EstadosCuentaDescargaPdf.OrderByDescending(e => e.Periodo.Anho).ThenByDescending(e => e.Periodo.Mes).ToList());
        }
    }
}