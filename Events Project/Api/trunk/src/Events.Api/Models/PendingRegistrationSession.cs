﻿using System;

namespace Aafp.Events.Api.Models
{
    public class PendingRegistrationSession
    {
        public virtual Guid Key { get; set; }

        public virtual string AddUser { get; set; }

        public virtual DateTime AddDate { get; set; }

        public virtual string ChangeUser { get; set; }

        public virtual DateTime? ChangeDate { get; set; }

        public virtual bool DeleteFlag { get; set; }

        public virtual Guid SessionKey { get; set; }

        public virtual int SelectedQuantity { get; set; }
    }
}