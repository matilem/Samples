namespace Aafp.Cme.Api.Dtos
{
    public class CreditAvailableStatsDto
    {
        public decimal CreditsPurchased { get; set; }

        public decimal CreditsExpiring { get; set; }

        public decimal QuizzesAvailable { get; set; }
    }
}