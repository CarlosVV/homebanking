using System.Collections.Generic;
using System.Threading.Tasks;

namespace DinersClubOnline.Model
{
    public interface IOfertaRepository
    {
        Task<List<Oferta>> ListarOfertasPorUsuarioAsync(string idUsuario);
    }
}