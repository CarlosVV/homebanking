using DinersClubOnline.Api.Models;
using DinersClubOnline.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace DinersClubOnline.Api.Controllers
{
    [RoutePrefix("api/tipos-documentos")]
    public class TipoDocumentoController : ApiController
    {
        private readonly ITipoDocumentoRepository _tipoDocumentoRepository;

        public TipoDocumentoController(ITipoDocumentoRepository tipoDocumentoRepository)
        {
            _tipoDocumentoRepository = tipoDocumentoRepository;
        }

        [Route]
        [ResponseType(typeof(IEnumerable<TipoDocumentoViewModel>))]
        public async Task<IEnumerable<TipoDocumentoViewModel>> Get()
        {
            return (await _tipoDocumentoRepository.ListarTiposDocumentosAsync()).Select(x => new TipoDocumentoViewModel
            {
                IdTipoDocumento = x.IdTipoDocumento,
                NombreTipoDocumento = x.NombreTipoDocumento
            });
        }
    }
}
