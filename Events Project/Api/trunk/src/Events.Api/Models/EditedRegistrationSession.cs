using System;

namespace Aafp.Events.Api.Models
{
    public class EditedRegistrationSession
    {
        public virtual Guid Key { get; set; }

        public virtual string AddUser { get; set; }

        public virtual DateTime AddDate { get; set; }

        public virtual string ChangeUser { get; set; }

        public virtual DateTime? ChangeDate { get; set; }

        public virtual string Code { get; set; }

        public virtual string Title { get; set; }

        public virtual DateTime? Date { get; set; }

        public virtual string Time { get; set; }

        public virtual string StartTime { get; set; }

        public virtual string EndTime { get; set; }

        public virtual int SelectedQuantity { get; set; }

        public virtual bool DeleteFlag { get; set; }

        public virtual bool Ticketed { get; set; }

        public virtual Fee Fee { get; set; }

        public virtual Guid SessionKey { get; set; }

        public virtual bool Selected { get; set; }

        public virtual bool Removed { get; set; }
        
        public virtual bool IsRegistered { get; set; }

        public virtual string DateDisplay
        {
            get
            {
                var display = string.Empty;

                if (Date.HasValue)
                {
                    display = $"{Date.Value.ToString("ddd, MMM dd, yyyy")} </br> {Time}";
                }
                else
                {
                    display = "n/a";
                }

                return display;
            }
        }

        public virtual decimal FeeTotal
        {
            get
            {
                var total = 0m;

                if (Fee != null && Fee.Price > 0 && SelectedQuantity > 1)
                {
                    total = Fee.Price * SelectedQuantity;
                }
                else if (Fee != null && Fee.Price > 0)
                {
                    total = Fee.Price;
                }

                return total;
            }
        }

        public virtual string FeeDisplay => Fee != null ? Fee.Price > 0m ? Fee.Price.ToString("F") : "0.00" : "N/A";

        public virtual string FeeTotalDisplay => FeeTotal > 0m ? FeeTotal.ToString("F") : "0.00";
    }
}