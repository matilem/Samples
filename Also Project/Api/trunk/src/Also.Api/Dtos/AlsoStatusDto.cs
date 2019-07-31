using System;

namespace Aafp.Also.Api.Dtos
{
    public class AlsoStatusDto
    {
        public Guid AlsoStatusKey { get; set; }

        public string AddUser { get; set; }

        public DateTime AddDate { get; set; }

        public string ChangeUser { get; set; }

        public DateTime ChangeDate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public DateTime ApprovalDate { get; set; }

        public string AlsoStatusType { get; set; }

        public virtual bool IsCurrent()
        {
            return DateTime.Now >= StartDate && DateTime.Now <= ExpirationDate;
        }
    }
}