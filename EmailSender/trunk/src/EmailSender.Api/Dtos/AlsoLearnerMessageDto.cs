using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aafp.EmailSender.Api.Dtos
{
    public class AlsoLearnerMessageDto : DtoBase
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string LetterofParticipationUrl => $"{ApplicationConfig.BaseUrl}/Cme/Reporting/NonMemberParticipation.aspx";
    }
}