using Aafp.Also.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aafp.Also.Api.Tasks.Interfaces
{
    public interface IIndividualTasks
    {
        Task<IndividualDto> GetIndividualByWebLogin(string webLogin);

        Task<IndividualDto> GetIndividualByCustomerId(string id);

        Task<IndividualDto> GetIndividualByCustomerKey(Guid customerKey);

        List<AlsoStatusDto> GetAlsoStatuses(Guid customerKey);

        string CurrentAlsoStatus(List<AlsoStatusDto> alsoStatuses);
    }
}
