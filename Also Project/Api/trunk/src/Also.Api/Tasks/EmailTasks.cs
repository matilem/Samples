using Aafp.Also.Api.Dtos;
using Aafp.Also.Api.Tasks.Interfaces;
using ApiClientHelper.Components;
using System.Net;
using System.Threading.Tasks;

namespace Aafp.Also.Api.Tasks
{
    public class EmailTasks : IEmailTasks
    {
        //private string emailService = $"{ApplicationConfig.BaseUrl}/emailsender-api/also/";
        private string emailService = $"http://dev.ams.aafp.org/emailsender-api/also/";

        public async Task<bool> SendWelcomeEmail(AlsoMessageDto dto)
        {
            var success = false;
            var result = await HttpClientHelper.PostJson<bool>(emailService, $"welcome", dto);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                success = true;
            }
            else
            {
                success = false;
            }

            return success;
        }

        public async Task<bool> SendStatusChangeEmail(AlsoStatusChangeMessageDto dto)
        {
            var success = false;
            var result = await HttpClientHelper.PostJson<bool>(emailService, $"status-change", dto);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                success = true;
            }
            else
            {
                success = false;
            }

            return success;
        }
    }
}