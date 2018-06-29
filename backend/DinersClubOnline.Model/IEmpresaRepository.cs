using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinersClubOnline.Model
{
    public interface IEmpresaRepository
    {
        Task<List<Empresa>> ListarPorCategoria(string idCategoria);
    }
}
