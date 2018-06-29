using DinersClubOnline.Api.Models;
using DinersClubOnline.Model;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace DinersClubOnline.Api.Controllers
{
    [RoutePrefix("api/categorias")]
    public class CategoriaController : ApiController
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public CategoriaController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        [Route]
        public async Task<IHttpActionResult> Get()
        {
            var data = await _categoriaRepository.Listar();
            var dataMapped = data.Select(d =>new CategoriaViewModel{
                 IdCategoria = d.IdCategoria,
                 NombreCategoria= d.NombreCategoria
            });

            return Ok(dataMapped);
        }
    }
}