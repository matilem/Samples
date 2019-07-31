using System;
using Aafp.Events.Api.Dao.Interfaces;
using Aafp.Events.Api.Dtos;
using NHibernate;

namespace Aafp.Events.Api.Dao
{
    public class HealthCheckDao : IHealthCheckDao
    {
        public ISession Session { get; set; }

        public HealthCheckResultDto CanConnectToDatabase()
        {
            var result = new HealthCheckResultDto();

            try
            {
                var query = Session.CreateSQLQuery("SELECT TOP 1 * FROM co_customer WITH (NOLOCK)");
                var customer = query.UniqueResult();

                if (customer != null)
                {
                    result.Success = true;
                    result.Message = "Able to query co_customer.";
                }
                else
                {
                    result.Success = false;
                    result.Message = "Unable to query co_customer.";
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }
    }
}