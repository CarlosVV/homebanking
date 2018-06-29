namespace DinersClubOnline.Jobs.Tasks.Solicitudes.Interfaces
{
    public interface IEmailHandler
    {
        void SendReport(IReportConfiguration configuration);
    }
}
