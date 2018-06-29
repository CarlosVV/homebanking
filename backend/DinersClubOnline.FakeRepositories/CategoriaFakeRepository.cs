using DinersClubOnline.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinersClubOnline.FakeRepositories
{
    public class CategoriaFakeRepository : ICategoriaRepository
    {
        public Task<List<Categoria>> Listar()
        {
            return Task.FromResult(Data.Categorias);
        }
    }
}
