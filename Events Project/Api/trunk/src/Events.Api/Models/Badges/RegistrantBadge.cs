namespace Aafp.Events.Api.Models.Badges
{
    public class RegistrantBadge : BadgeBase
    {
        public string Nickname { get; set; }

        public string FullName { get; set; }

        public string Company { get; set; }

        public string Position { get; set; }

        public string Address { get; set; }

        public bool ShowFAAFP { get; set; }

        public string LastName { get; set; }
    }
}