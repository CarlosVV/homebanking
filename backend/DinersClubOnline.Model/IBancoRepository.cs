using System.Collections.Generic;
using System.Threading.Tasks;

namespace DinersClubOnline.Model
{
    public interface IBancoRepository
    {
        Task<List<Banco>> ListarBancosAsync();
    }
}