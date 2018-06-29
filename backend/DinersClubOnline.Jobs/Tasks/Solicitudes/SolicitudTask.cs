using DinersClubOnline.Jobs.Tasks.Solicitudes;
using DinersClubOnline.Jobs.Tasks;
using DinersClubOnline.Jobs.Tasks.Solicitudes.Interfaces;

namespace DinersClub.Jobs.Tasks
{
    public class SolicitudTask : ITask
    {
        private readonly IDataHandler _datahandler;
        private readonly IReportHandler _reporthandler;
        private readonly IEmailHandler _emailhandler;
        private readonly IReportConfiguration _reportconfiguration;
        private readonly SearchCriteria _filtercriteria;

        public SolicitudTask(IDataHandler datahandler, IReportHandler reporthandler, IEmailHandler emailhandler, IReportConfiguration reportconfiguration, SearchCriteria filter)
        {
            _datahandler = datahandler;
            _reporthandler = reporthandler;
            _emailhandler = emailhandler;
            _reportconfiguration = reportconfiguration;
            _filtercriteria = filter;
        }

        public void Process()
        {

            var lstSolicitudes = _datahandler.GetData(_filtercriteria);
            _reportconfiguration.Attachment = _reporthandler.BuildReport(lstSolicitudes);
            _emailhandler.SendReport(_reportconfiguration);
            _datahandler.UpdateSentData(lstSolicitudes);
        }
    }
}
