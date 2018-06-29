using DinersClubOnline.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DinersClubOnline.Repositories
{
    public class EstadoDeCuentaRepository : IEstadoDeCuentaRepository
    {
        private readonly Services.Client.TarjetaService.Tarjeta _tarjetaService;

        public EstadoDeCuentaRepository(Services.Client.TarjetaService.Tarjeta tarjetaService)
        {
            _tarjetaService = tarjetaService;
        }
        
        public Task<EstadoDeCuenta> ObtenerEstadoDeCuentaActualAsync(string idTarjeta)
        {
            throw new NotImplementedException();
        }

        public Task<List<EstadoCuentaDescargaPdf>> ObtenerUltimosEstadosDeCuentaAsync(string idTarjeta)
        {
            throw new NotImplementedException();
        }
    }
}
