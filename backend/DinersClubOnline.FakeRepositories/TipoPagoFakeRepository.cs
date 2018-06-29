using DinersClubOnline.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DinersClubOnline.FakeRepositories
{
    public class TipoPagoFakeRepository : ITipoPagoRepository
    {
        public Task<List<TipoPago>> ListarTiposPagoAsync()
        {
            return Task.FromResult(Data.TiposPago);
        }
    }
}