using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinersClubOnline.Model.Solicitudes
{
    public interface IDineroEfectivoRepository
    {
        Task<DineroEfectivo> ObtenerAsync(string id);

        Task<SolicitudResult> GuardarAsync(DineroEfectivo prestamoPersonal);
    }
}
