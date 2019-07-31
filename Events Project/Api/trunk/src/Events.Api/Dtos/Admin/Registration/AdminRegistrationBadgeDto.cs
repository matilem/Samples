﻿using System.Collections.Generic;

namespace Aafp.Events.Api.Dtos.Admin.Registration
{
    public class AdminRegistrationBadgeDto
    {
        public string NickName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Notes { get; set; }

        public string Company { get; set; }

        public string Position { get; set; }

        public List<StateDto> States { get; set; } 
    }
}