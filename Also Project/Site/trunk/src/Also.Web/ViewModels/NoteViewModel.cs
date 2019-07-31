using System;

namespace Aafp.Also.Web.ViewModels
{
    public class NoteViewModel
    {
        public Guid AlsoCourseKey { get; set; }

        public DateTime AddDate { get; set; }

        public string AddUser { get; set; }

        public string Note { get; set; }

        public string DateDisplay
        {
            get
            {
                var display = string.Empty;

                display = $"{AddDate.ToString("M/d/yyyy")}";

                return display;
            }
        }

        public string TimeDisplay
        {
            get
            {
                var display = string.Empty;

                display = $"{AddDate.ToString("h:mm tt")}";

                return display;
            }
        }
    }
}