using System;
using System.Collections.Generic;

namespace Aafp.MyCme.Web.ViewModels
{
    public class CmeActivityViewModel
    {
        public Guid ActivityKey { get; set; }

        public bool HasError { get; set; }

        public int ActivityNumber { get; set; }

        public string ActivityTitle { get; set; }

        public DateTime ActivityStartDate { get; set; }

        public DateTime ActivityEndDate { get; set; }

        public string ActivityCity { get; set; }

        public string ActivityState { get; set; }

        public decimal ActivityPrescribedCredits { get; set; }

        public decimal ActivityElectiveCredits { get; set; }

        public List<CmeActivitySessionViewModel> Sessions { get; set; }

        public IndividualViewModel Customer { get; set; }

        public List<SessionsByDateViewModel> SessionsByDate { get; set; }

        public string ActivityDateDisplay
        {
            get
            {
                string display = string.Empty;

                display = $"{ActivityStartDate.ToString("ddd, MMM d, yyyy")}" + $" - {ActivityEndDate.ToString("ddd, MMM d, yyyy")}";

                return display;
            }
        }

        public string ActivityLocationDisplay
        {
            get
            {
                string display;

                display = ActivityCity + ", " + ActivityState;

                return display;
            }
        }

        public List<string> ActivityDatesDisplay
        {
            get
            {
                var dates = new List<string>();

                for (var date = ActivityStartDate; date <= ActivityEndDate; date = date.AddDays(1))
                {
                    var display = $"{date.ToString("ddd, MMM d, yyyy")}";
                    dates.Add(display);
                }

                return dates;
            }
        }

        public List<DateTime> ActivityDates
        {
            get
            {
                var dates = new List<DateTime>();

                for (var date = ActivityStartDate; date <= ActivityEndDate; date = date.AddDays(1))
                {
                    dates.Add(date);
                }

                return dates;
            }
        }
    }
}