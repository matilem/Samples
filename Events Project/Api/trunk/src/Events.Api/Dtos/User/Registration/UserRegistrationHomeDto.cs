using System.Collections.Generic;

namespace Aafp.Events.Api.Dtos.User.Registration
{
    public class UserRegistrationHomeDto
    {
        public List<UserRegistrationDto> PendingRegistrations { get; set; }
        
        public List<UserRegistrationDto> CurrentRegistrations { get; set; }
        
        public List<UserRegistrationDto> UpcomingRegistrations { get; set; }   
    }
}