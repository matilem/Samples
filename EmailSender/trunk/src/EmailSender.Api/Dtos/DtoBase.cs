using System.Collections.Generic;
using System.Net.Mail;

namespace Aafp.EmailSender.Api.Dtos
{
    public class DtoBase
    {
        protected DtoBase()
        {
            Attachments = new List<Attachment>();
            To = new List<string>();
        }

        public List<string> To { get; set; }

        public string From { get; set; }

        public string Subject { get; set; }

        public string HtmlBody { get; set; }

        public string TextBody { get; set; }

        public List<Attachment> Attachments { get; set; }
    }
}