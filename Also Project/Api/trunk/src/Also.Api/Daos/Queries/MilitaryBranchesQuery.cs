using Aafp.Also.Api.Daos.Queries.Interfaces;
using Aafp.Also.Api.Dtos;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Aafp.Also.Api.Daos.Queries
{
    public class MilitaryBranchesQuery : IMilitaryBranchesQuery
    {
        public List<MilitaryBranchDto> GetMilitaryBranches()
        {
            var dto = new List<MilitaryBranchDto>();

            using (var connection = new SqlConnection(ApplicationConfig.DatabaseConnectionString))
            {
                connection.Open();
                dto = connection.Query<MilitaryBranchDto>("get_cme_military_branches", null, commandType: CommandType.StoredProcedure).ToList();
            }

            return dto;
        }
    }
}