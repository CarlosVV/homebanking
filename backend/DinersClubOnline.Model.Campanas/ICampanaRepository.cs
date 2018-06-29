using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinersClubOnline.Model.Campanas
{
    [CLSCompliant(true)]
    public interface ICampanaRepository
    {
        Task<Campana> ObtenerAsync(string id);
        Task<List<Campana>> ObtenerTodosAsync();
        Task<CampanaResult> GuardarAsync(Campana campana);
    }
}
