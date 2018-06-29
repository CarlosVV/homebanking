using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DinersClubOnline.Api.Models;
using DinersClubOnline.Model;

namespace DinersClubOnline.Api.Controllers
{
    [RoutePrefix("api/operadores-telefonicos")]
    public class OperadorTelefonicoController : ApiController
    {
        private readonly IOperadorTelefonicoRepository _operadorTelefonicoRepository;

        public OperadorTelefonicoController(IOperadorTelefonicoRepository operadorTelefonicoRepository)
        {
            _operadorTelefonicoRepository = operadorTelefonicoRepository;
        }

        /// <summary>Obtiene la lista de operadores telefónicos disponibles</summary>
        [Route]
        [ResponseType(typeof(IEnumerable<OperadorTelefonicoViewModel>))]
        public async Task<IEnumerable<OperadorTelefonicoViewModel>> Get()
        {
            return (await _operadorTelefonicoRepository.ListarOperadoresTelefonicosAsync()).Select(x=> new OperadorTelefonicoViewModel
            {
                Id = x.Id,
                Nombre = x.Nombre
            });
        }
    }
}