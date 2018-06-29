using DinersClubOnline.Model;
using DinersClubOnline.Model.Campanas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DinersClubOnline.Repositories.Campanas
{
    public class CampanaRepository : ICampanaRepository
    {
        public async Task<Campana> ObtenerAsync(string id)
        {
            using (var ctx = new CampanaContext())
            {
                var data = await ctx.Campanas.FirstOrDefaultAsync(r => r.Id == id);
                if (data == null)
                {
                    return null;
                }

                return data;
            }
        }

        public async Task<List<Campana>> ObtenerTodosAsync()
        {
            using (var ctx = new CampanaContext())
            {
                var data = await ctx.Campanas.Where(
                        x=>x.Activo &&
                        x.FechaInicio <= DateTime.Today && x.FechaFin >= DateTime.Now)
                        .OrderBy(m=>m.Prioridad)
                        .Take(3).ToListAsync();
                if (data == null)
                {
                    return null;
                }

                return data;
            }
        }

        public async Task<CampanaResult> GuardarAsync(Campana campana)
        {
            using (var ctx = new CampanaContext())
            {
                ctx.Campanas.Add(campana);
                var result = await ctx.SaveChangesAsync() > 0;

                return result ? CampanaResult.CreateCampanaResult(campana.Id, campana.FechaCreacion)
                    : CampanaResult.CreateErrorResult();
            }
        }
    }
}
