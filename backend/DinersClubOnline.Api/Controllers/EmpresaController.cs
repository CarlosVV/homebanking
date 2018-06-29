using DinersClubOnline.Api.Models;
using DinersClubOnline.Model;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace DinersClubOnline.Api.Controllers
{
    [RoutePrefix("api/empresas")]
    public class EmpresaController : ApiController
    {        
        private readonly IEmpresaRepository _empresaRepository;
        public EmpresaController(IEmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }

        [Route]
        public async Task<IHttpActionResult> Get(string idCategoria)
        {
            var data = await _empresaRepository.ListarPorCategoria(idCategoria);
            var dataMapped = data.Select(d => new EmpresaViewModel
            {
                IdEmpresa = d.IdEmpresa,
                NombreEmpresa = d.NombreEmpresa
            });

            return Ok(dataMapped);
        }
    }
}