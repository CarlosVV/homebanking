using DinersClubOnline.Api.Filters;
using DinersClubOnline.Api.Models;
using DinersClubOnline.Api.Principals;
using DinersClubOnline.Model;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace DinersClubOnline.Api.Controllers
{
    [ScopeAuthorize(Scopes.Registro)]
    [RoutePrefix("api/tarjeta")]
    public class TarjetaController : ApiController
    {
        private readonly ITarjetaRepository _tarjetaRepository;

        public TarjetaController(ITarjetaRepository tarjetaRepository)
        {
            _tarjetaRepository = tarjetaRepository;
        }

        [HttpGet]
        [Route("socio")]
        [ResponseType(typeof(SocioViewModel))]
        public async Task<IHttpActionResult> GetSocio()
        {
            var tarjeta = await _tarjetaRepository.ObtenerTarjetaAsync(User.ToDinersUser().IdTarjeta);

            if (tarjeta == null)
            {
                return NotFound();
            }

            var socioViewModel = new SocioViewModel
            {
                Nombre = tarjeta.Socio.Nombre,
                SegundoNombre = tarjeta.Socio.SegundoNombre,
                ApellidoPaterno = tarjeta.Socio.ApellidoPaterno,
                ApellidoMaterno = tarjeta.Socio.ApellidoMaterno
            };

            return Ok(socioViewModel);
        }
    }
}