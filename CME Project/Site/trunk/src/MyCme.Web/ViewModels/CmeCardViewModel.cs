using System;

namespace Aafp.MyCme.Web.ViewModels
{
    public class CmeCardViewModel
    {
        public string Title { get; set; }

        public int ActivityNumber { get; set; }

        public string ProductImage { get; set; }

        public Guid ProductKey { get; set; }

        public DateTime? TransactionDate { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public DateTime? BeginDate { get; set; }

        public string AccessUrl { get; set; }

        public string ClaimCreditUrl { get; set; }

        public decimal CreditsAvailable { get; set; }

        public decimal CreditsReported { get; set; }

        public Guid AssessmentGroupKey { get; set; }

        public bool IsAemCard { get; set; }

        public bool IsMember { get; set; }

        public string CreditsDisplay => IsAemCard ? $"{CreditsAvailable} credits available" : $"{CreditsReported} / {CreditsAvailable} credits claimed";

        public string CreditsDisplayClass => CreditsReported == CreditsAvailable ? "check" : "briefcase";

        public string ExpirationDateDisplay => ExpirationDate.HasValue ? $"Expires {ExpirationDate.Value:MMMM dd, yyyy}" : string.Empty;

        public string TransactionDateDisplay => TransactionDate.HasValue ? $"Expires {TransactionDate.Value:MMMM dd, yyyy}" : string.Empty;

        public string BeginDateDisplay => BeginDate.HasValue ? $"Available {BeginDate.Value:MMMM dd, yyyy}" : string.Empty;

        public bool ShowExpirationTag => ExpirationDate.HasValue && (ExpirationDate.Value - DateTime.Today).TotalDays < 30;

        public bool ShowExpirationDate => ExpirationDate.HasValue;

        public bool IsCompletedCard { get; set; }

        public string ProductImageDisplayUrl
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ProductImage))
                    return $"{ApplicationConfig.BaseImagesUrl}/extra-generic-16x9.300.jpg";

                return ProductImage.Contains("./photos") || ProductImage.Contains("./uploads") ? $"{ApplicationConfig.BaseUrl}/eweb{ProductImage.Remove(0, 1)}" : ProductImage;
            }
        }

        public string ProductImageStyle => !IsAemCard && !string.IsNullOrWhiteSpace(ProductImage) ? "-force-enlarge" : string.Empty;

        public string ProductExpiringImageStyle => ShowExpirationTag ? "-expiring" : string.Empty;

        public bool DisableViewButton => string.IsNullOrWhiteSpace(AccessUrl);

        public bool ShowCombinedViewClaimButton => AccessUrl == ClaimCreditUrlDisplay;

        public string ClaimCreditUrlDisplay
        {
            get
            {
                if (!IsMember)
                {                    
                    if (AssessmentGroupKey != Guid.Empty)
                    {
                        return string.IsNullOrWhiteSpace(AccessUrl) ? $"{ApplicationConfig.AssessmentUrl}/{AssessmentGroupKey}" : AccessUrl;
                    }
                    else
                    {
                        return  string.IsNullOrWhiteSpace(AccessUrl) ? ClaimCreditUrl : AccessUrl;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(ClaimCreditUrl))
                        return string.IsNullOrWhiteSpace(AccessUrl) ? $"{ApplicationConfig.BaseUrl}/cme/reporting/Live/SearchResults.aspx?types=Smart+Search&SmartSearchTerms={ActivityNumber}" : AccessUrl;
                    else
                        return ClaimCreditUrl;
                }
            }
        }

        public string ProductCompletedStyle => IsCompletedCard ? "-completed" : string.Empty;

        public bool IsFutureActivity => BeginDate.HasValue && BeginDate.Value > DateTime.Today;
    }
}