namespace Aafp.Cme.Api.Dtos
{
    public class ReElectionDto
    {
        public string Message { get; set; }

        public string Status { get; set; }

        public bool IsMember { get; set; }

        public string StatusDisplay => Status.ToLower();

        public ReElectionTotalsDto Totals { get; set; }
    }
}