namespace Aafp.MyCme.Web.ViewModels
{
    public class ReElectionStatusViewModel
    {
        public string Status { get; set; }

        public string Message { get; set; }

        public  bool IsMember { get; set; }

        public string StatusDisplay => Status.ToLower();
    }
}