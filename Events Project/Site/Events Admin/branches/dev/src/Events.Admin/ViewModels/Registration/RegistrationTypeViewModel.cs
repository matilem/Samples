using System;
using System.Collections.Generic;
namespace Aafp.Events.Admin.ViewModels.Registration
{
    public class RegistrationTypeViewModel : ViewModelBase
    {
        public Guid Key { get; set; }

        public string Title { get; set; }

        public string Code { get; set; }

        public DateTime? CutOffDate { get; set; }

        public string CutOffDateDisplay
        {
            get
            {
                var display = CutOffDate?.ToShortDateString();

                if (CutOffDate == DateTime.Parse("1/1/001"))
                {
                    display = null;
                }

                return display;
            }
        }

        public List<EventFeeViewModel> Fees { get; set; }
    }
}