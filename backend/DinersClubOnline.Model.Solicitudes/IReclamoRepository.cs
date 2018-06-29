using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinersClubOnline.Model.Solicitudes
{
    public interface IReclamoRepository
    {
        Task<Reclamo> ObtenerAsync(string id);

        Task<SolicitudResult> GuardarAsync(Reclamo reclamo);
    }
}
