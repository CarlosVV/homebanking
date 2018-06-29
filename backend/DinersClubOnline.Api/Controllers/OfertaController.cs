using DinersClubOnline.Api.Filters;
using DinersClubOnline.Api.Models;
using DinersClubOnline.Api.Principals;
using DinersClubOnline.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace DinersClubOnline.Api.Controllers
{
    [ScopeAuthorize(Scopes.Consultas)]
    [RoutePrefix("api/oferta")]
    public class OfertaController : ApiController
    {
        private readonly IOfertaRepository _ofertaRepository;

        public OfertaController(IOfertaRepository ofertaRepository)
        {
            _ofertaRepository = ofertaRepository;
        }

        [Route]
        [ResponseType(typeof(IEnumerable<OfertaGetViewModel>))]
        public async Task<IHttpActionResult> Get()
        {
            var response = await _ofertaRepository.ListarOfertasPorUsuarioAsync(User.ToDinersUser().IdUsuario);
            if (response == null)
            {
                return Ok();
            }
            return Ok(response.Select(x => new OfertaGetViewModel
            {
                IdTipoOferta = x.IdTipoOferta,
                IdTarjeta = x.IdTarjeta,
                MontoLineaNueva = x.MontoLineaNueva,
                FechaInicio = x.FechaInicio,
                FechaFin = x.FechaFin,
                MontoOferta = x.MontoOferta,
                TEA = x.TEA,
                TCEA = x.TCEA
            }).OrderBy(t => t.IdTipoOferta).ToList());
        }
    }
}