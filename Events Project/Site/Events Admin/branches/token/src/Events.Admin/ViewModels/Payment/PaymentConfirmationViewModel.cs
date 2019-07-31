using System;
using System.Collections.Generic;
using System.Linq;
using Aafp.Events.Admin.ViewModels.Registration;

namespace Aafp.Events.Admin.ViewModels.Payment
{
    public class PaymentConfirmationViewModel : ViewModelBase
    {
        public UserSearchViewModel UserSearch { get; set; }

        public Guid RegistrationKey { get; set; }

        public CustomerViewModel Customer { get; set; }

        public EventDetailViewModel Event { get; set; }

        public List<InvoiceDetailsViewModel> InvoiceDetails { get; set; }

        public List<SessionDetailsViewModel> Sessions { get; set; }

        public decimal Subtotal
        {
            get
            {
                var total = 0m;
                var items = InvoiceDetails.Where(item => item.InvoiceType != "Discount").ToList();

                foreach (var item in items)
                {
                    if (item.Price > 0)
                    {
                        total += (item.Price * item.Quantity);
                    }
                }

                return total;
            }
        }

        public decimal TotalDue
        {
            get
            {
                var totaldue = 0m;

                    totaldue = Subtotal - DiscountPrice;

                return totaldue;
            }
        }

        public decimal DiscountPrice
        {
            get
            {
                var discount = 0.00m;

                foreach (var item in InvoiceDetails)
                {

                    if (item.InvoiceType == "Discount")
                    {
                        discount = item.Price;
                    }
                }

                return discount;
            }
        }

        public string DiscountPriceDisplay => DiscountPrice.ToString("F");

        public string InvoiceCodeDisplay
        {
            get
            {
                var invoice = InvoiceDetails.FirstOrDefault();

                var invoicecode = invoice.InvoiceCode;

                return invoicecode;
            }

        }
        public bool? InvoiceFlagStatus
        {
            get
            {
                var invoice = InvoiceDetails.FirstOrDefault();

                var invoiceflag = invoice.InvoiceClosedFlag;

                return invoiceflag;
            }

        }

        public Guid? InvoiceKeyDisplay
        {
            get
            {
                var invoice = InvoiceDetails.FirstOrDefault();

                var invoicekey = invoice.InvoiceKey;

                return invoicekey;
            }

        }

        public string SubtotalDisplay
        {
            get
            {
                var display = string.Empty;

                if (Subtotal > 0)
                {
                    display = Subtotal.ToString("F");
                }
                else
                {
                    display = "0.00";
                }

                return display;
            }
        }

        public string TotalDueDisplay
        {
            get
            {
                var display = string.Empty;

                if (TotalDue > 0)
                {
                    display = TotalDue.ToString("F");
                }
                else
                {
                    display = "0.00";
                }

                return display;
            }
        }

        public string SessionDateDisplay
        {
            get
            {
                var date = string.Empty;

                foreach (var item in InvoiceDetails)
                {
                    if (item.PriceEndDate == null)
                    {
                        foreach (var session in Sessions)
                        {
                            if (session.SessionCode == item.ProductCode)
                            {
                                item.PriceEndDate = session.SessionDate;
                                item.Time = session.SessionTime;
                            }
                        }

                        if (item.PriceEndDate == null)
                        {
                            date = "n/a";
                        }
                    }
                }

                return date;
            }
        }
    }
}