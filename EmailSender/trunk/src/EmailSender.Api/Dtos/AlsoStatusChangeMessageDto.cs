using System;

namespace Aafp.EmailSender.Api.Dtos
{
    public class AlsoStatusChangeMessageDto : DtoBase
    {
        public string AlsoStatus { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Designation { get; set; }

        public string Email { get; set; }

        public string StartDateDisplay
        {
            get
            {
                var display = string.Empty;

                    display = $"{StartDate.ToString("M/d/yyyy")}";

                return display;
            }
        }

        public string ExpirationDateDisplay
        {
            get
            {
                var display = string.Empty;

                display = $"{ExpirationDate.ToString("M/yyyy")}";

                return display;
            }
        }
    }
}