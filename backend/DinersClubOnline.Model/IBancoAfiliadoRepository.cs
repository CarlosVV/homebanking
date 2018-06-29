using System.Collections.Generic;
using System.Threading.Tasks;

namespace DinersClubOnline.Model
{
    public interface IBancoAfiliadoRepository
    {
        Task<List<BancoAfiliado>> ListarBancosAfiliadosAsync();
    }
}
