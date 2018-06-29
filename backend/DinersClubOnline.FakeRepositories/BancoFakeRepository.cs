using DinersClubOnline.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DinersClubOnline.FakeRepositories
{
    public class BancoFakeRepository : IBancoRepository
    {
        public Task<List<Banco>> ListarBancosAsync()
        {
            return Task.FromResult(Data.Bancos);
        }
    }
}