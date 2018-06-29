using DinersClubOnline.Jobs.Tasks.Solicitudes.Interfaces;

namespace DinersClubOnline.Jobs.Tasks.Solicitudes
{
    public class ReportConfiguration : IReportConfiguration
    {
        public string EmailTo { get; set; }

        public string EmailFrom{ get; set; }

        public string EmailCc{ get; set; }

        public string EmailBc{ get; set; }

        public string EmailSubject{ get; set; }

        public string EmailBody{ get; set; }

        public string[] Files{ get; set; }

        public string FileAttachment{ get; set; }
        public byte[] Attachment { get; set; }

        public string CredentialUser{ get; set; }

        public string CredentialPassword{ get; set; }
    }
}
