using Aafp.Also.Api.Daos.Queries.Interfaces;
using Aafp.Also.Api.Dtos;
using Aafp.Also.Api.Tasks.Interfaces;
using ApiClientHelper.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Aafp.Also.Api.Tasks
{
    public class IndividualTasks : IIndividualTasks
    {
        private string customerService = $"{ApplicationConfig.BaseUrl}/customers-api/";
        //private string customerService = $"http://dev.ams.aafp.org/customers-api/";

        public IAlsoStatusQuery AlsoStatusQuery { get; set; }

        public async Task<IndividualDto> GetIndividualByWebLogin(string webLogin)
        {
            IndividualDto individual = null;
            var result = await HttpClientHelper.GetJson<IndividualDto>(customerService, $"individual/by-weblogin/{webLogin}/");

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

        public async Task<IndividualDto> GetIndividualByCustomerId(string customerId)
        {
            IndividualDto individual = null;
            var result = await HttpClientHelper.GetJson<IndividualDto>(customerService, $"individual/by-customerid/{customerId}");

            if (result.StatusCode == HttpStatusCode.OK)
            {
                individual = result.Data;

                if (individual.Phones != null)
                {
                    foreach (var phone in individual.Phones)
                    {
                        if (phone.IsPrimary)
                            individual.PrimaryPhone = phone;
                    }
                    if (individual.PrimaryPhone == null && individual.Phones.Count > 0)
                    {
                        individual.PrimaryPhone = individual.Phones.First();
                    }
                }

                individual.AlsoStatuses = GetAlsoStatuses(individual.Key);

                if (individual.AlsoStatuses != null)
                {
                    individual.CurrentAlsoStatus = CurrentAlsoStatus(individual.AlsoStatuses);
                }
            }

            return individual;
        }

        public async Task<IndividualDto> GetIndividualByCustomerKey(Guid customerKey)
        {
            IndividualDto individual = null;
            var result = await HttpClientHelper.GetJson<IndividualDto>(customerService, $"individual/{customerKey}");

            if (result.StatusCode == HttpStatusCode.OK)
            {
                individual = result.Data;
            }

            return individual;
        }

        public List<AlsoStatusDto> GetAlsoStatuses(Guid customerKey)
        {
            var statuses = new List<AlsoStatusDto>();

            statuses = AlsoStatusQuery.GetAlsoStatuses(customerKey);

            return statuses;
        }

        public string CurrentAlsoStatus(List<AlsoStatusDto> alsoStatuses)
        {
            var display = string.Empty;

            foreach (var status in alsoStatuses)
            {
                if (DateTime.Now >= status.ExpirationDate)
                {
                    if (status.AlsoStatusType == "Advisory Faculty")
                    {
                        return status.AlsoStatusType;
                    }
                    if (status.AlsoStatusType == "Instructor")
                    {
                        return status.AlsoStatusType;
                    }
                    if (status.AlsoStatusType == "Instructor Candidate")
                    {
                        return status.AlsoStatusType;
                    }
                    if (status.AlsoStatusType == "Provider")
                    {
                        return status.AlsoStatusType;
                    }
                }
            }

            return display;
        }
    }
}