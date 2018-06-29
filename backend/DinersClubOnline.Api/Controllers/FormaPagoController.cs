using DinersClubOnline.Api.Models;
using DinersClubOnline.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace DinersClubOnline.Api.Controllers
{
    [RoutePrefix("api/formas-pago")]
    public class FormaPagoController : ApiController
    {
        private readonly IBancoAfiliadoRepository _bancoAfiliadoRepository;

        public FormaPagoController(IBancoAfiliadoRepository bancoAfiliadoRepository)
        {
            _bancoAfiliadoRepository = bancoAfiliadoRepository;
        }

        [Route]
        [ResponseType(typeof(IEnumerable<BancoAfiliadoViewModel>))]
        public async Task<IEnumerable<BancoAfiliadoViewModel>> Get()
        {
            return (await _bancoAfiliadoRepository.ListarBancosAfiliadosAsync()).Select(x => new BancoAfiliadoViewModel
            {
                IdBanco = x.IdBanco,
                NombreBanco = x.NombreBanco,
                PagoVentanilla = x.PagoVentanilla,
                PagoInternet = x.PagoInternet,
                PagoCargoEnCuenta = x.PagoCargoEnCuenta,
                UrlArchivo = x.UrlArchivo
            });
        }
    }
}
