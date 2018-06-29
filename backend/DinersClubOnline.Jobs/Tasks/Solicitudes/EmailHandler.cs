using DinersClubOnline.Jobs.Tasks.Solicitudes.Interfaces;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace DinersClubOnline.Jobs.Tasks.Solicitudes
{
    public class EmailHandler : IEmailHandler
    {
        public void SendReport(IReportConfiguration configuration)
        {
            if (configuration != null && configuration.Attachment!=null)
            {
                string filename = "Solicitudes.xlsx";
                EmailSender sender = new EmailSender();
                sender.Email(
                    configuration.EmailTo,
                    configuration.EmailBody,
                    configuration.EmailSubject,
                    configuration.EmailFrom,
                    configuration.EmailFrom,
                    configuration.CredentialUser,
                    configuration.CredentialPassword,
                    new MailAttachment(configuration.Attachment, filename)
                    );
            }
        }
    }

    public class EmailSender
    {
        public void Email(string to,
                         string body,
                         string subject,
                         string fromAddress,
                         string fromDisplay,
                         string credentialUser,
                         string credentialPassword,
                         params MailAttachment[] attachments)
        {
            try
            {
                using (var mail = new MailMessage())
                {
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    mail.To.Add(new MailAddress(to));
                    mail.From = new MailAddress(fromAddress, fromDisplay, Encoding.UTF8);
                    mail.Subject = subject;
                    mail.SubjectEncoding = Encoding.UTF8;
                    mail.Priority = MailPriority.Normal;

                    if (attachments != null)
                    {
                        foreach (MailAttachment ma in attachments)
                        {
                            mail.Attachments.Add(ma.File);
                        }
                    }

                    using (var smtp = new SmtpClient())
                    {
                        smtp.Credentials = new NetworkCredential(credentialUser, credentialPassword);
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder(1024);
                sb.Append("\nTo:" + to);
                sb.Append("\nbody:" + body);
                sb.Append("\nsubject:" + subject);
                sb.Append("\nfromAddress:" + fromAddress);
                sb.Append("\nfromDisplay:" + fromDisplay);
                sb.Append("\ncredentialUser:" + credentialUser);
                sb.Append("\nException:" + ex.Message);
                throw new ArgumentException(sb.ToString());
            }
        }
    }

    public class MailAttachment : IDisposable
    {
        #region Fields

        private readonly MemoryStream stream;
        private readonly string fileName;
        private readonly string mediaType;

        #endregion Fields

        #region Properties

        public Stream Data { get { return stream; } }
        public string Filename { get { return fileName; } }
        public string MediaType { get { return mediaType; } }
        public Attachment File { get { return new Attachment(Data, Filename, MediaType); } }

        #endregion Properties

        #region Constructors

        public MailAttachment(byte[] data, string fileName)
        {
            stream = new MemoryStream(data);
            this.fileName = fileName;
            mediaType = MediaTypeNames.Application.Octet;
        }

        public MailAttachment(string data, string fileName)
        {
            stream = new MemoryStream(Encoding.ASCII.GetBytes(data));
            this.fileName = fileName;
            mediaType = MediaTypeNames.Text.Html;
        }

        #region IDisposable Support
        private bool disposedValue; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    stream.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~MailAttachment()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
            // TODO: uncomment the following line if the finalizer is overridden above.

        }
        #endregion


        #endregion Constructors
    }
}