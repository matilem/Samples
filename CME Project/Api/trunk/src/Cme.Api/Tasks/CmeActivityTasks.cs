using Aafp.Cme.Api.Daos.Queries.Interfaces;
using Aafp.Cme.Api.Dtos;
using Aafp.Cme.Api.Tasks.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aafp.Cme.Api.Tasks
{
    public class CmeActivityTasks : ICmeActivityTasks
    {
        public ICmeActivityQuery CmeActivityQuery { get; set; }

        public ICreditTasks CreditTasks { get; set; }

        public IIndividualTasks IndividualTasks { get; set; }

        public async Task<CmeActivityDto> GetCmeSessionsByActivity(int activityNumber, string webLogin)
        {
            var dto = new CmeActivityDto();
            dto.Sessions = new List<CmeActivitySessionDto>();

            dto =  CmeActivityQuery.GetCmeSessionsByActivity(activityNumber);

            if (dto != null)
            {
                var credits = await CreditTasks.GetLiveCreditsForTranscript(webLogin);
                dto.Customer = await IndividualTasks.GetIndividualByWebLogin(webLogin);

                foreach (var session in dto.Sessions)
                {
                    foreach (var credit in credits)
                    {
                        if (session.SessionKey == credit.SessionKey)
                        {
                            session.Reported = true;
                        }
                    }
                }
            }

            return dto;
        }
    }
}