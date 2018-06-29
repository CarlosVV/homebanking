using System.Collections.Generic;
using System.Threading.Tasks;

namespace DinersClubOnline.Model
{
    public interface IPrivilegioRepository
    {
        Task<ObtenerCercanosResult> ObtenerCercanosAsync(Coordenada coordenada, double distanciaMaxima, int? numeroResultados);
    }
}