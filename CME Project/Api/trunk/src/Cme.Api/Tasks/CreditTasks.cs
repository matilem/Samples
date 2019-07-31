using System;
using System.Collections.Generic;
using Aafp.Cme.Api.Daos.Queries.Interfaces;
using Aafp.Cme.Api.Dtos;
using Aafp.Cme.Api.Tasks.Interfaces;
using Aafp.Cme.Api.Daos.Commands.Interfaces;
using System.Threading.Tasks;
using Aafp.Cme.Api.Helpers;

namespace Aafp.Cme.Api.Tasks
{
    public class CreditTasks : ICreditTasks
    {
        public ICreditQuery CreditQuery { get; set; }

        public IIndividualTasks IndividualTasks { get; set; }

        public ICreditCommand CreditCommand { get; set; }

        public ICmeSessionQuery SessionQuery { get; set; }

        public List<CreditReElectionDto> GetByCustomerForReElectionCalculation(Guid customerKey, int startYear, int endYear)
        {
            return CreditQuery.GetByCustomerForReElectionCalculation(customerKey, startYear, endYear);
        }

        public async Task<List<CreditDto>> ReportCmeCredit(string webLogin, List<Guid> credits)
        {
            var customer = await IndividualTasks.GetIndividualByWebLogin(webLogin);
            var list = new List<CreditDto>();

            foreach (var credit in credits)
            {
                var session = SessionQuery.GetCmeSessionsByKey(credit);
                var errorMessage = string.Empty;
                var hasError = false;

                try
                {
                    CreditCommand.ReportCredit(customer.Key, session, webLogin);
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;

                    if (ex.InnerException != null)
                    {
                        errorMessage = ex.InnerException.Message;
                    }

                    hasError = true;
                }

                var creditDto = new CreditDto
                {
                    PrescribedCredits = session.SessionPrescribedCredits,
                    ElectiveCredits = session.SessionElectiveCredits,
                    SessionKey = session.SessionKey,
                    ErrorMessage = errorMessage,
                    HasError = hasError
                };

                list.Add(creditDto);
            }

            return list;
        }

        public bool ReportCmeCreditBySession(AlsoCreditDto dto)
        {
            var session = SessionQuery.GetCmeSessionsByKey(dto.SessionKey);
            var success = false;

            foreach (var cstKey in dto.CustomerKeys)
            {          
                try
                {
                    CreditCommand.ReportCredit(cstKey, session, dto.WebLogin);
                    success = true;
                }
                catch (Exception ex)
                {
                    var message = $"Unable to report the credit for Customer: {cstKey}, Session: {dto.SessionKey}, Error: {ex.Message}.";
                    Logger.LogError(message);
                }
            }

            return success;
        }

        public bool ReportTeachingCreditBySession(TeachingCreditDto dto)
        {
            var session = SessionQuery.GetCmeSessionsByKey(dto.SessionKey);
            var success = false;

            foreach (var cstKey in dto.CustomerKeys)
            {
                try
                {
                    CreditCommand.ReportTeachingCredit(cstKey, session, dto.WebLogin);
                    success = true;
                }
                catch (Exception ex)
                {
                    var message = $"Unable to report the teaching credit for Customer: {cstKey}, Session: {dto.SessionKey}, Error: {ex.Message}.";
                    Logger.LogError(message);
                }
            }

            return success;
        }

        public async Task<List<CreditTranscriptDto>> GetLiveCreditsForTranscript(string webLogin)
        {
            var customer = await IndividualTasks.GetIndividualByWebLogin(webLogin);

            var startDate = DateTime.Today.AddYears(-1);
            var endDate = DateTime.Today.AddDays(1);

            return CreditQuery.GetByCustomerForTranscript(customer.Key, startDate, endDate);
        }
    }
}