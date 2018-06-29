using DinersClubOnline.Api.Models;
using DinersClubOnline.Model;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace DinersClubOnline.Api.Controllers
{
    [RoutePrefix("api/servicios")]
    public class ServicioController : ApiController
    {
        private readonly IServicioRepository _servicioRepository;
        public ServicioController(IServicioRepository servicioRepository)
        {
            _servicioRepository = servicioRepository;
        }

        [Route]
        public async Task<IHttpActionResult> Get(string idEmpresa)
        {
            var data = await _servicioRepository.ListarPorEmpresa(idEmpresa);
            var dataMapped = data.Select(d =>new ServicioViewModel{
                 IdServicio = d.IdServicio,
                 NombreServicio = d.NombreServicio,
                 ParametroServicio = d.ParametroServicio
            });

            return Ok(dataMapped);
        }
    }
}