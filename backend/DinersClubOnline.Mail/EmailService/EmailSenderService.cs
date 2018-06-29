using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace DinersClubOnline.Mail.EmailService
{
    public static class EmailSenderService
    {
        public static void SendEmail(string subject, string body, string mailFrom, List<string> mailsTo, List<string> bcc, List<string> cc, Dictionary<MemoryStream, string> attachments)
        {

            using (var mail = new MailMessage())
            {
                mail.From = new MailAddress(mailFrom);

                foreach (var address in mailsTo)
                {
                    mail.To.Add(address);
                }

                if (bcc != null)
                {
                    foreach (var address in bcc)
                    {
                        mail.Bcc.Add(address);
                    }
                }

                if (cc != null)
                {
                    foreach (var address in cc)
                    {
                        mail.CC.Add(address);
                    }
                }

                if (attachments != null)
                {
                    foreach (var attachment in attachments)
                    {
                        mail.Attachments.Add(new Attachment(attachment.Key, attachment.Value));
                    }
                }

                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;


                using (var smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("diners.email.test@gmail.com", "123diners321");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }
    }
}
