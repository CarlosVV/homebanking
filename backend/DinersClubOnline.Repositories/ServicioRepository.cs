using DinersClubOnline.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinersClubOnline.Repositories
{
    public class ServicioRepository : IServicioRepository
    {
        public Task<List<Servicio>> ListarPorEmpresa(string idEmpresa)
        {
            throw new NotImplementedException();
        }
    }
}
