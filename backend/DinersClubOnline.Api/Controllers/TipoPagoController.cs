using DinersClubOnline.Api.Models;
using DinersClubOnline.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace DinersClubOnline.Api.Controllers
{
    [RoutePrefix("api/tiposPago")]
    public class TipoPagoController : ApiController
    {
        private readonly ITipoPagoRepository _tipoPagoRepository;

        public TipoPagoController(ITipoPagoRepository tipoPagoRepository)
        {
            _tipoPagoRepository = tipoPagoRepository;
        }

        [Route]
        [ResponseType(typeof(IEnumerable<TipoPagoViewModel>))]
        public async Task<IEnumerable<TipoPagoViewModel>> Get()
        {
            return (await _tipoPagoRepository.ListarTiposPagoAsync()).Select(x => new TipoPagoViewModel
            {
                Id = x.Id,
                Nombre = x.Nombre
            });
        }
    }
}