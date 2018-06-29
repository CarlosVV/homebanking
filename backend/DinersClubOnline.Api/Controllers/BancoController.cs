using DinersClubOnline.Api.Models;
using DinersClubOnline.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace DinersClubOnline.Api.Controllers
{
    [RoutePrefix("api/bancos")]
    public class BancoController : ApiController
    {
        private readonly IBancoRepository _bancoRepository;

        public BancoController(IBancoRepository bancoRepository)
        {
            _bancoRepository = bancoRepository;
        }

        [Route]
        [ResponseType(typeof(IEnumerable<BancoViewModel>))]
        public async Task<IEnumerable<BancoViewModel>> Get()
        {
            return (await _bancoRepository.ListarBancosAsync()).Select(x => new BancoViewModel
            {
                Id = x.Id,
                Nombre = x.Nombre
            });
        }
    }
}