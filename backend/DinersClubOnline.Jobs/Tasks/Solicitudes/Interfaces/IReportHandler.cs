using System.Collections.Generic;

namespace DinersClubOnline.Jobs.Tasks.Solicitudes.Interfaces
{
    public interface IReportHandler
    {
        byte[] BuildReport(IEnumerable<SolicitudEnvioModel> lstSolicitudes);

    }
}
