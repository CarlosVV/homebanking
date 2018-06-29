using System.Linq;
using System.Threading.Tasks;
using DinersClubOnline.Model;

namespace DinersClubOnline.FakeRepositories
{
    public class PrivilegioFakeRepository : IPrivilegioRepository
    {
        public Task<ObtenerCercanosResult> ObtenerCercanosAsync(Coordenada coordenada, double distanciaMaxima, int? numeroResultados)
        {
            var resultados = Data.Privilegios.Select(x => new
            {
                Privilegio = x,
                Distancia = x.Coordenada.Distancia(coordenada)
            })
            .Where(x => x.Distancia < distanciaMaxima)
            .OrderBy(x=> x.Distancia)
            .Select(x => x.Privilegio).ToList();

            var totalResultados = resultados.Count;

            if (numeroResultados.HasValue)
            {
                resultados = resultados.Take(numeroResultados.Value).ToList();
            }

            return Task.FromResult(ObtenerCercanosResult.CreateObtenerCercanosResult(totalResultados, resultados, "http://www.dinersclub.com.pe"));
        }
    }
}