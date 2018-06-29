using DinersClubOnline.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DinersClubOnline.FakeRepositories
{
    public class BancoAfiliadoFakeRepository : IBancoAfiliadoRepository
    {
        public Task<List<BancoAfiliado>> ListarBancosAfiliadosAsync()
        {
            return Task.FromResult(Data.BancosAfiliados);
        }
    }
}
