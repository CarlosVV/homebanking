using System.Collections.Generic;
using System.Threading.Tasks;

namespace DinersClubOnline.Model
{
    public interface ITipoPagoRepository
    {
        Task<List<TipoPago>> ListarTiposPagoAsync();
    }
}