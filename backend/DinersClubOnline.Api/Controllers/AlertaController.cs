using DinersClubOnline.Api.Models;
using DinersClubOnline.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using DinersClubOnline.Api.Principals;
using DinersClubOnline.Api.Filters;
using System.Web.Http.Description;

namespace DinersClubOnline.Api.Controllers
{
    [ScopeAuthorize(Scopes.Consultas)]
    [RoutePrefix("api/alertas")]
    public class AlertaController : ApiController
    {
        private readonly IAlertaRepository _alertaRepository;

        public AlertaController(IAlertaRepository alertaRepository)
        {
            _alertaRepository = alertaRepository;
        }

        [ResponseType(typeof(IEnumerable<AlertaViewModel>))]
        [HttpGet]
        [Route]
        public async Task<IEnumerable<AlertaViewModel>> Get()
        {
            var data = await _alertaRepository.ObtenerTodasAlertas();

            if (data == null)
            {
                return new List<AlertaViewModel>();
            }

            //todo: change to a var and eval null and empty lists
            return data.Select(m => new AlertaViewModel
            {
                IdAlerta = m.IdAlerta,
                Nombre = m.Nombre,
                Descripcion = m.Descripcion,
                CondicionesAdicionales = m.CondicionesAdicionales.Select(c => new AlertaCondicionAdicionalViewModel
                {
                    IdCondicionAdicional = c.IdCondicionAdicional,
                    Nombre = c.Nombre
                })
            });
        }
    }
}
