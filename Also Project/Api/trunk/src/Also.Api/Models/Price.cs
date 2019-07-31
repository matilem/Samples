using System;
using Aafp.Also.Api.Models;
using System.Collections.Generic;

namespace Aafp.Also.Api.Models
{
    public class Price
    {
        public virtual Guid Key { get; set; }

        public virtual string AddUser { get; set; }

        public virtual DateTime AddDate { get; set; }

        public virtual string ChangeUser { get; set; }

        public virtual DateTime ChangeDate { get; set; }

        public virtual bool DeleteFlag { get; set; }

        public virtual Guid EntityKey { get; set; }

        public virtual Guid ProductKey { get; set; }

        public virtual Guid ProductTypeKey { get; set; }

        public virtual Guid ProductCompanyKey { get; set; }

        public virtual string PriceCode { get; set; }

        public virtual decimal PriceAmount { get; set; }

        public virtual decimal PricePercent { get; set; }

        public virtual string PriceDisplayName { get; set; }

        public virtual Guid PriceRevenueKey { get; set; }

        public virtual DateTime PriceStartDate { get; set; }

        public virtual DateTime PriceEndDate { get; set; }

        public virtual string PriceEwebCode { get; set; }

        public virtual bool RenewUnpaidOrdersFlag { get; set; }

        public virtual bool AllowUnpaidOrdersFlag { get; set; }
    }
}