using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Aafp.Events.Api.ApplicationConfig;
using Aafp.Events.Api.Dao.Interfaces;
using Aafp.Events.Api.Dtos.User.Registration;
using Aafp.Events.Api.Models;

namespace Aafp.Events.Api.Dao
{
    using NHibernate.Linq;

    public class RegistrantDao : GenericDao<Registrant>, IRegistrantDao
    {
        public Registrant GetRegistrant(Guid eventKey, Guid customerKey)
        {
            var registrants = Session.Query<Registrant>().Where(r => r.EventKey == eventKey && r.CustomerKey == customerKey && r.CancelationDate == null).ToList();

            return registrants.FirstOrDefault();
        }

        public Registrant GetRegistrantByKey(Guid registrantKey)
        {
            return GetByKey(registrantKey);
        }

        public bool IsRegisteredForEvent(Guid eventKey, Guid customerKey)
        {
            var registrant = GetRegistrant(eventKey, customerKey);
            return registrant != default(Registrant);
        }

        public List<Registrant> GetRegistrantsForEvent(Guid eventKey)
        {
            return Session.Query<Registrant>().Where(r => r.EventKey == eventKey && r.CancelationDate == null).ToList();
        }

        public List<Registrant> GetRegistrantsForEventByDate(Guid eventKey, DateTime startDate, DateTime endDate)
        {
            return Session.Query<Registrant>().Where(r => r.EventKey == eventKey && r.CancelationDate == null && r.RegistrationDate >= startDate && r.RegistrationDate <= endDate).ToList();
        }

        public IList<Guid> GetGuestsForRegistrant(Guid registrantKey)
        {
            return Session.Query<Guest>().Where(g => g.RegistrationKey == registrantKey && !g.DeleteFlag).Select(g => g.Key).ToList();
        }

        public int GetEventRegistrantsCount(Guid eventKey)
        {
            return Session.Query<Registrant>().Count(r => r.EventKey == eventKey && r.CancelationDate == null);
        }

        public List<Registrant> GetRelatedRegistrations(Guid eventKey, Guid customerKey)
        {
            var registrants = new List<Registrant>();
            var sql = "SELECT reg_key FROM dbo.ev_registrant WITH (NOLOCK) INNER JOIN dbo.client_aafp_e43_related_event WITH (NOLOCK) ON reg_evt_key = e43_related_evt_key WHERE e43_evt_key = :@EventKey AND reg_cst_key = :@CustomerKey";
            var query = Session.CreateSQLQuery(sql)
                .SetParameter("@EventKey", eventKey)
                .SetParameter("@CustomerKey", customerKey);
            var keys = query.List<Guid>();

            foreach (var key in keys)
            {
                registrants.Add(GetRegistrantByKey(key));
            }

            return registrants;
        }

        public List<UserRegistrationDto> GetUserRegistrationsForHomePage(Guid customerKey)
        {
            var registrations = new List<UserRegistrationDto>();

            using (var connection = new SqlConnection(ApplicationConfigManager.Settings.ConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    var command = new SqlCommand("EXECUTE client_aafp_event_get_home_page_items @CustomerKey", connection, transaction);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@CustomerKey", customerKey);
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var registration = new UserRegistrationDto
                        {
                            Key = new Guid(reader["Key"].ToString()),
                            EventKey = new Guid(reader["EventKey"].ToString()),
                            EventTitle = reader["EventTitle"] != DBNull.Value ? reader["EventTitle"].ToString() : string.Empty,
                            EventCode = reader["EventCode"] != DBNull.Value ? reader["EventCode"].ToString() : string.Empty,
                            EventStartDate = reader["EventStartDate"] != DBNull.Value ? DateTime.Parse(reader["EventStartDate"].ToString()) : (DateTime?)null,
                            EventEndDate = reader["EventEndDate"] != DBNull.Value ? DateTime.Parse(reader["EventEndDate"].ToString()) : (DateTime?)null,
                            PostToWebDate = reader["PostToWebDate"] != DBNull.Value ? DateTime.Parse(reader["PostToWebDate"].ToString()) : (DateTime?)null,
                            RemoveFromWebDate = reader["RemoveFromWebDate"] != DBNull.Value ? DateTime.Parse(reader["RemoveFromWebDate"].ToString()) : (DateTime?)null,
                            EventDescriptionHtml = reader["EventDescriptionHtml"] != DBNull.Value ? reader["EventDescriptionHtml"].ToString() : string.Empty,
                            Capacity = reader["Capacity"] != DBNull.Value ? Int32.Parse(reader["Capacity"].ToString()) : 0,
                            AllowWaitList = reader["AllowWaitList"] != DBNull.Value ? Convert.ToBoolean(Int32.Parse(reader["AllowWaitList"].ToString())) : false,
                            EventCity = reader["EventCity"] != DBNull.Value ? reader["EventCity"].ToString() : string.Empty,
                            EventState = reader["EventState"] != DBNull.Value ? reader["EventState"].ToString() : string.Empty,
                            Type = reader["Type"] != DBNull.Value ? reader["Type"].ToString() : string.Empty
                        };
                        registrations.Add(registration);
                    }

                    reader.Close();
                    transaction.Commit();
                }
            }

            return registrations;
        }
    }
}