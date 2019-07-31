using System;

namespace Aafp.Also.Web.ViewModels
{
    public class AlsoStatusViewModel
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
    }
}