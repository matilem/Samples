using Aafp.Also.Api.Daos.Queries.Interfaces;
using Aafp.Also.Api.Dtos;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Aafp.Also.Api.Daos.Queries
{
    public class AlsoStatusQuery : IAlsoStatusQuery
    {
        public List<AlsoStatusDto> GetAlsoStatuses(Guid customerKey)
        {
            var dto = new List<AlsoStatusDto>();

            using (var connection = new SqlConnection(ApplicationConfig.DatabaseConnectionString))
            {
                connection.Open();
                dto = connection.Query<AlsoStatusDto>("get_also_statuses", new { customerKey }, commandType: CommandType.StoredProcedure).ToList();
            }

            return dto;
        }

        public List<AlsoStatusCourseHistoryDto> GetAlsoCourseHistory(Guid customerKey)
        {
            var dto = new List<AlsoStatusCourseHistoryDto>();

            using (var connection = new SqlConnection(ApplicationConfig.DatabaseConnectionString))
            {
                connection.Open();
                dto = connection.Query<AlsoStatusCourseHistoryDto>("get_also_course_history", new { customerKey }, commandType: CommandType.StoredProcedure).ToList();
            }

            return dto;
        }

        public int GetCmeTeachingCreditsCount(Guid customerKey, int yearsNeeded)
        {
            var numberofCredits = 0; 

            using (var connection = new SqlConnection(ApplicationConfig.DatabaseConnectionString))
            {
                connection.Open();
                numberofCredits = connection.Query<int>("get_cme_teaching_credits_count", new { customerKey, yearsNeeded }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }

            return numberofCredits;
        }

        public List<AlsoStatusTypeDto> GetAlsoStatusTypes()
        {
            var dto = new List<AlsoStatusTypeDto>();

            using (var connection = new SqlConnection(ApplicationConfig.DatabaseConnectionString))
            {
                connection.Open();
                dto = connection.Query<AlsoStatusTypeDto>("get_also_status_types", commandType: CommandType.StoredProcedure).ToList();
            }

            return dto;
        }

        public bool GetAdvisoryFacultyRecommendation(Guid customerKey)
        {
            var hasRecommendation = false;

            using (var connection = new SqlConnection(ApplicationConfig.DatabaseConnectionString))
            {
                connection.Open();
                hasRecommendation = connection.Query<bool>("get_advisory_faculty_recommendation", new { customerKey }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }

            return hasRecommendation;
        }
    }
}