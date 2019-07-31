using Aafp.Also.Api.Daos.Queries.Interfaces;
using Aafp.Also.Api.Dtos;
using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Aafp.Also.Api.Daos.Queries
{
    public class AlsoCourseQuery : IAlsoCourseQuery
    {
        public AlsoCourseDto GetAlsoCourse(Guid activityKey)
        {
            var dto = new AlsoCourseDto();

            using (var connection = new SqlConnection(ApplicationConfig.DatabaseConnectionString))
            {
                connection.Open();
                dto = connection.Query<AlsoCourseDto>("get_also_course", new { activityKey }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }

            return dto;
        }
    }
}