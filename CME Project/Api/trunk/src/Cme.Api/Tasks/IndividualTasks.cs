using System.Net;
using System.Threading.Tasks;
using Aafp.Cme.Api.Dtos;
using Aafp.Cme.Api.Tasks.Interfaces;
using ApiClientHelper.Components;

namespace Aafp.Cme.Api.Tasks
{
    public class IndividualTasks : IIndividualTasks
    {
        private string customerService = $"{ApplicationConfig.BaseUrl}/customers-api/";
        public async Task<IndividualDto> GetIndividualByWebLogin(string webLogin)
        {
            IndividualDto individual = null;
            var result = await HttpClientHelper.GetJson<IndividualDto>(customerService, $"individual/by-weblogin/{webLogin}/cme");

            if (result.StatusCode == HttpStatusCode.OK)
            {
                individual = result.Data;
            }
            else
            {
                throw new ServiceException(result.ErrorMessage);
            }

            return individual;
        }

        public async Task<bool> CheckUserAccess(string webLogin)
        {
            IndividualDto individual = null;
            var result = await HttpClientHelper.GetJson<IndividualDto>(customerService, $"individual/by-weblogin/{webLogin}/cme");

            if (result.StatusCode == HttpStatusCode.OK)
            {
                individual = result.Data;
            }
            else
            {
                throw new ServiceException(result.ErrorMessage);
            }

            return false;
        }
    }
}