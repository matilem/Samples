using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using Aafp.EmailSender.Api.Dtos;

namespace Aafp.EmailSender.Api.Helpers
{
    public class EmailClient
    {
        public static bool Send(DtoBase dto)
        {
            var success = false;

            using (var smtp = new SmtpClient())
            {
                var port = 0;
                Int32.TryParse(ApplicationConfig.SmtpPort, out port);

                if (port > 0)
                {
                    smtp.Port = port;
                    smtp.Host = ApplicationConfig.SmtpHost;
                    var mail = new MailMessage
                    {
                        Body = dto.TextBody,
                        Subject = dto.Subject,
                        From = new MailAddress(dto.From),
                        IsBodyHtml = false
                    };

                    if(dto.To.Any())
                    {
                        foreach(var to in dto.To)
                        {
                            mail.To.Add(to);
                        }
                    }

                    if (dto.Attachments.Any())
                    {
                        foreach (var attachment in dto.Attachments)
                            mail.Attachments.Add(attachment);
                    }

                    var mimeType = new System.Net.Mime.ContentType("text/html");
                    var alternateView = AlternateView.CreateAlternateViewFromString(dto.HtmlBody, mimeType);
                    mail.AlternateViews.Add(alternateView);

                    smtp.Send(mail);

                    success = true;
                }
            }

            return success;
        }

        public static Attachment CreatePdfAttachment(byte[] bytes, string fileName)
        {
            var attachment = CreateAttachment(bytes, fileName, "application/pdf");

            return attachment;
        }

        private static Attachment CreateAttachment(byte[] bytes, string fileName, string mimeType)
        {
            Attachment attachment = null;

            using (var stream = new MemoryStream(bytes))
            {
                attachment = new Attachment(stream, fileName, mimeType);
            }

            return attachment;
        }
    }
}