namespace DinersClubOnline.Jobs.Tasks.Solicitudes.Interfaces
{
    public interface IReportConfiguration
    {
        string EmailTo { get; set; }
        string EmailFrom { get; set; }
        string EmailCc { get; set; }
        string EmailBc { get; set; }
        string EmailSubject { get; set; }
        string EmailBody { get; set; }
        string[] Files { get; set; }
        string FileAttachment { get; set; }
        byte[] Attachment { get; set; }
        string CredentialUser { get; set; }
        string CredentialPassword { get; set; }
    }
}
