using DinersClubOnline.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinersClubOnline.FakeRepositories
{
    public class EmpresaFakeRepository : IEmpresaRepository
    {
        public Task<List<Empresa>> ListarPorCategoria(string idCategoria)
        {
            return Task.FromResult(Data.Empresas.Where(e => e.IdCategoria == idCategoria).ToList());
        }
    }
}
