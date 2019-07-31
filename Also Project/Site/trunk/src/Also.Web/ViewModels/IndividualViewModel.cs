using System;
using System.Collections.Generic;

namespace Aafp.Also.Web.ViewModels
{
    public class IndividualViewModel
    {
        public Guid Key { get; set; }

        public string CustomerId { get; set; }

        public string WebLogin { get; set; }

        public string Prefix { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Designation { get; set; }

        public string Email { get; set; }

        public List<IndividualPhoneViewModel> Phones { get; set; }

        public IndividualPhoneViewModel PrimaryPhone { get; set; }

        public string OrganizationName { get; set; }

        public string Title { get; set; }

        public string ChapterStateCode { get; set; }

        public int ReElectionStartYear { get; set; }

        public int ReElectionEndYear { get; set; }

        public string FullName { get; set; }

        public string FullNameMinusPrefix { get; set; }

        public bool IsMember { get; set; }

        public bool IsAafpStaff { get; set; }

        public bool HasReElectionCycle { get; set; }

        public List<AlsoStatusViewModel> AlsoStatuses { get; set; }

        public string CurrentAlsoStatus { get; set; }
    }
}