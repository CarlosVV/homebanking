using DinersClubOnline.Api.Models;
using DinersClubOnline.Model;
using DinersClubOnline.Model.Campanas;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;


namespace DinersClubOnline.Api.Controllers
{
    [RoutePrefix("api/campanas")]
    public class CampanaController : ApiController
    {
        private readonly ICampanaRepository _campanaRepository;
        public CampanaController(ICampanaRepository campanaRepository)
        {
            _campanaRepository = campanaRepository;
        }

        [Route]
        [ResponseType(typeof(List<CampanaViewModel>))]
        public async Task<IHttpActionResult> Get()
        {
            var data  = (await _campanaRepository.ObtenerTodosAsync()).Select(x => new CampanaViewModel
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Descripcion = x.Descripcion,
                Imagen = x.Imagen,
                Banner = x.Banner,
                FechaFin = x.FechaFin,
                Prioridad = x.Prioridad,
                FechaInicio = x.FechaInicio,
                Activo = x.Activo
            });
            return Ok(data);
        }

        [Route]
        [ResponseType(typeof(CampanaViewModel))]
        public async Task<IHttpActionResult> Get(string id)
        {
            var data = (await _campanaRepository.ObtenerAsync(id));
            var dataMapped =
                new CampanaViewModel
                {
                    Id = data.Id,
                    Nombre = data.Nombre,
                    Descripcion = data.Descripcion,
                    Imagen = data.Imagen,
                    Banner = data.Banner,
                    FechaFin = data.FechaFin,
                    Prioridad = data.Prioridad,
                    FechaInicio = data.FechaInicio,
                    Activo = data.Activo
                };

            return Ok(dataMapped);
        }

        [Route]
        [ResponseType(typeof(Campana))]
        public async Task<IHttpActionResult> Post(CampanaViewModel modelo)
        {
            var campana = new Campana {
                Nombre = modelo.Nombre,
                Descripcion = modelo.Descripcion,
                Imagen = modelo.Imagen,
                Banner = modelo.Banner,
                CreadoPor = modelo.CreadoPor,
                FechaActualizacion = modelo.FechaActualizacion,
                Prioridad = modelo.Prioridad,
                FechaInicio = modelo.FechaInicio,
                FechaFin = modelo.FechaFin,
                Activo = modelo.Activo,
                ActualizadoPor = modelo.ActualizadoPor,
                FechaCreacion = modelo.FechaCreacion,
                Id = modelo.Id,
            };

            var result = await _campanaRepository.GuardarAsync(campana);

            if (result.Resultado)
            {
                return Created("api/campanas/campana", new CampanaViewModel
                {
                    Id = result.Id,
                    FechaCreacion = result.FechaCreacion
                });
            }

            return BadRequest("An error ocurred");
        }
    }
}