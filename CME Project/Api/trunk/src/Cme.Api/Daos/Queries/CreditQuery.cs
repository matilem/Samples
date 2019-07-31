using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Aafp.Cme.Api.Daos.Queries.Interfaces;
using Aafp.Cme.Api.Dtos;
using Dapper;

namespace Aafp.Cme.Api.Daos.Queries
{
    public class CreditQuery : ICreditQuery
    {
        public List<CreditReElectionDto> GetByCustomerForReElectionCalculation(Guid customerKey, int startYear, int endYear)
        {
            var dto = new List<CreditReElectionDto>();

            using (var connection = new SqlConnection(ApplicationConfig.DatabaseConnectionString))
            {
                connection.Open();
                dto = connection.Query<CreditReElectionDto>("client_aafp_get_cme_credits_by_customer_for_reelection_calculation", new { customerKey, startYear, endYear }, commandType: CommandType.StoredProcedure).ToList();
            }

            return dto;
        }

        public List<CreditTranscriptDto> GetByCustomerForTranscript(Guid customerKey, DateTime startDate, DateTime endDate)
        {
            var dto = new List<CreditTranscriptDto>();

            using (var connection = new SqlConnection(ApplicationConfig.DatabaseConnectionString))
            {
                connection.Open();
                dto = connection.Query<CreditTranscriptDto>("client_aafp_get_cme_credits_for_transcript", new { customerKey, startDate, endDate }, commandType: CommandType.StoredProcedure).ToList();
            }

            return dto;
        }

        public List<CreditDto> GetTeachingCredits(Guid customerKey)
        {
            var dto = new List<CreditDto>();

            using (var connection = new SqlConnection(ApplicationConfig.DatabaseConnectionString))
            {
                connection.Open();
                dto = connection.Query<CreditDto>("get_cme_teaching_credits", new { customerKey }, commandType: CommandType.StoredProcedure).ToList();
            }

            return dto;
        }

    }
}