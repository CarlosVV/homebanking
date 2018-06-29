using DinersClubOnline.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinersClubOnline.Repositories
{
    public class EmpresaRepository : IEmpresaRepository
    {
        public Task<List<Empresa>> ListarPorCategoria(string idCategoria)
        {
            throw new NotImplementedException();
        }
    }
}
