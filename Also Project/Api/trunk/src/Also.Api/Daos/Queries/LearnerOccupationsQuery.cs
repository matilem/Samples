using Aafp.Also.Api.Daos.Queries.Interfaces;
using Aafp.Also.Api.Dtos;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Aafp.Also.Api.Daos.Queries
{
    public class LearnerOccupationsQuery : ILearnerOccupationsQuery
    {
        public List<LearnerOccupationDto> GetLearnerOccupations()
        {
            var dto = new List<LearnerOccupationDto>();

            using (var connection = new SqlConnection(ApplicationConfig.DatabaseConnectionString))
            {
                connection.Open();
                dto = connection.Query<LearnerOccupationDto>("get_also_learner_occupations", null, commandType: CommandType.StoredProcedure).ToList();
            }

            return dto;
        }
    }
}