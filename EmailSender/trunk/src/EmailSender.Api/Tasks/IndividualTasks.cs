using System;
using System.Net;
using System.Threading.Tasks;
using Aafp.EmailSender.Api.Dtos;
using Aafp.EmailSender.Api.Tasks.Interfaces;
using ApiClientHelper.Components;

namespace Aafp.EmailSender.Api.Tasks
{
    public class IndividualTasks : IIndividualTasks
    {
        public async Task<IndividualDto> GetByEmail(string email)
        {
            IndividualDto individual = null;

            try
            {
                var result = await HttpClientHelper.GetJson<IndividualDto>(ApplicationConfig.CustomersApi, $"individual/by-email/{email}/");

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    individual = result.Data;
                }
            }
            catch (Exception)
            {
                return null;
            }

            return individual;
        }
    }
}