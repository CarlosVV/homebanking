using System.Collections.Generic;
using System.Threading.Tasks;

namespace DinersClubOnline.Model
{
    public interface IOperadorTelefonicoRepository
    {
        Task<List<OperadorTelefonico>> ListarOperadoresTelefonicosAsync();
    }
}