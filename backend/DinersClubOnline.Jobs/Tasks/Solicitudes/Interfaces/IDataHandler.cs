using System.Collections.Generic;

namespace DinersClubOnline.Jobs.Tasks.Solicitudes.Interfaces
{
    public interface IDataHandler
    {
        IEnumerable<SolicitudEnvioModel> GetData(SearchCriteria criteria);
        bool UpdateSentData(IEnumerable<SolicitudEnvioModel> data);
    }
}
