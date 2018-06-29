using DinersClubOnline.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinersClubOnline.FakeRepositories
{
    public class ServicioFakeRepository : IServicioRepository
    {
        public Task<List<Servicio>> ListarPorEmpresa(string idEmpresa)
        {
            return Task.FromResult(Data.Servicios.Where(e => e.IdEmpresa == idEmpresa).ToList());
        }
    }
}
