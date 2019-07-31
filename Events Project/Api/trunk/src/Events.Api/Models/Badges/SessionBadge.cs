namespace Aafp.Events.Api.Models.Badges
{
    public class SessionBadge : BadgeBase
    {
        public string Company { get; set; }

        public string EventName { get; set; }

        public string EventDateAndLocation { get; set; }

        public string SessionCode { get; set; }

        public string SessionName { get; set; }

        public string SessionDate { get; set; }

        public string Location { get; set; }

        public string AttendeeName { get; set; }

        public string Fee { get; set; }
    }
}