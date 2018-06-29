using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DinersClubOnline.Api.Models;
using DinersClubOnline.Model;

namespace DinersClubOnline.Api.Controllers
{
    [RoutePrefix("api/privilegios")]
    public class PrivilegioController : ApiController
    {
        private readonly IPrivilegioRepository _privilegioRepository;

        public PrivilegioController(IPrivilegioRepository privilegioRepository)
        {
            _privilegioRepository = privilegioRepository;
        }

        [Route]
        [HttpGet]
        [ResponseType(typeof(PrivilegiosGetViewModel))]
        public async Task<IHttpActionResult> Get(double latitud, double longitud, double distanciaMaxima, int? numeroPrivilegios = null)
        {
            var privilegios = await _privilegioRepository.ObtenerCercanosAsync(new Coordenada(latitud, longitud), distanciaMaxima, numeroPrivilegios);

            return Ok(new PrivilegiosGetViewModel
            {
                TotalPrivilegios = privilegios.TotalPrivilegios,
                Resultados = privilegios.Resultados.Select(x=> new PrivilegioViewModel
                {
                    Nombre = x.Nombre,
                    NumeroCuotasSinIntereses = x.NumeroCuotasSinIntereses
                }).ToList(),
                Url = privilegios.Url
            });
        }
    }
}