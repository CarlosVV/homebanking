using System.Collections.Generic;
using System.Threading.Tasks;
using DinersClubOnline.Model;


namespace DinersClubOnline.FakeRepositories
{
    public class OperadorTelefonicoFakeRepository : IOperadorTelefonicoRepository
    {
        public Task<List<OperadorTelefonico>> ListarOperadoresTelefonicosAsync()
        {
            return Task.FromResult(Data.OperadoresTelefonicos);
        }
    }
}