using System;

namespace Aafp.Cme.Api.Dtos
{
    public class CreditTypeDto
    {
        public Guid Key { get; set; }

        public virtual string Title { get; set; }

        public virtual string Designation { get; set; }

        public virtual string GroupType { get; set; }

        public virtual string LimitType { get; set; }

        public virtual decimal? MaximumCreditsPerCycle { get; set; }
    }
}