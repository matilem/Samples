using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Aafp.Events.Api.ApplicationConfig;
using Aafp.Events.Api.Dao.Interfaces;
using Aafp.Events.Api.Dtos.Customer;
using Aafp.Events.Api.Models;
using Aafp.Events.Api.Models.Badges;
using Aafp.Events.Api.Tasks.Admin.Interfaces;
using ApiClientHelper.Components;

namespace Aafp.Events.Api.Tasks.Admin
{
    public class AdminBadgeTasks : IAdminBadgeTasks
    {
        private string customerService = ApplicationConfigManager.Settings.CustomerServiceUrl;

        public IEventDao EventDao { get; set; }

        public IGuestDao GuestDao { get; set; }

        public IRegistrantDao RegistrantDao { get; set; }

        public IRegistrantSessionDao RegistrantSessionDao { get; set; }

        public ISessionDao SessionDao { get; set; }

        public BadgeBase GetRegistrantBadge(Guid registrantkey)
        {
            var registrant = RegistrantDao.GetRegistrantByKey(registrantkey);

            return GetRegistrantBadge(registrant);
        }

        public BadgeBase GetUnsoldSessionBadge(Guid sessionKey)
        {
            var badge = new SessionBadge();
            var session = SessionDao.GetByKey(sessionKey);
            
            badge.SessionCode = session.Code;
            badge.SessionName = session.Title;
            badge.SessionDate = session.Date?.ToString("dddd, MMMM d") ?? string.Empty;
            badge.SessionDate += string.IsNullOrEmpty(session.Time) ? " " : ", " + session.Time;
            badge.Location = GetSessionRooms(session.Key);

            return badge;
        }

        public BadgeBase GetRegistrantBadge(Registrant registrant)
        {
            var badge = new RegistrantBadge();

            if (registrant.CancelationDate.HasValue)
            {
                badge = null;
            }
            else
            {
                var evt = EventDao.GetEventBaseByKey(registrant.EventKey);
                var displayName = registrant.InvoiceDetail != null ? registrant.InvoiceDetail.PriceDisplayName : string.Empty;
                var customer = new CustomerDto();
                var result = HttpClientHelper.GetJson<CustomerDto>(customerService, $"individual/{registrant.CustomerKey}/event");

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    customer = result.Data;
                }
                else
                {
                    throw new ServiceException(result.ErrorMessage);
                }

                badge.Nickname = string.IsNullOrEmpty(registrant.BadgeName) ? string.Empty : registrant.BadgeName;
                badge.FullName = FormatFullName(customer);
				
				if (customer.IsAafpStaff)
                {
                    badge.Company = customer.OrganizationName;
					badge.Position = customer.Title;
                }
				else
				{
					if (evt.DisplayBadgeCompany)
					{
						if (!string.IsNullOrWhiteSpace(registrant.Organization))
							badge.Company = registrant.Organization;
					}

					if (evt.DisplayBadgePosition)
					{
						if (!string.IsNullOrWhiteSpace(registrant.Title))
							badge.Position = registrant.Title;
					}
				}                
                
                badge.Address = registrant.Country == "UNITED STATES" ? $"{registrant.City}, {registrant.State}" : $"{registrant.City}, {registrant.State}  {registrant.Country}";
                badge.ShowFAAFP = customer.IsMember && !string.IsNullOrEmpty(customer.FaafpFellowshipYear);
                badge.LastName = customer.LastName;
                badge.EventCode = evt.Code;
                badge.MemberId = customer.CustomerId;
                badge.AttendeeType = displayName;
            }

            return badge;
        }

        public List<BadgeBase> GetAllRegistrantBadges(Guid registrantkey)
        {
            var badges = new List<BadgeBase>();
            var registrant = RegistrantDao.GetRegistrantByKey(registrantkey);

            //Get Registrant Badge
            var registrantBadge = GetRegistrantBadge(registrant);
            badges.Add(registrantBadge);

            //Get Guest Badges
            var guestBadges = GetGuestBadges(registrant);

            if (guestBadges.Count > 0)
                badges.AddRange(guestBadges);

            //Get Session

            if (registrant.Sessions != null && registrant.Sessions.Count > 0)
            {
                var sessionBadges = GetRegistrantSessionBadges(registrant);
                badges.AddRange(sessionBadges);
            }

            return badges;
        }

        public List<BadgeBase> GetGuestBadges(Registrant registrant)
        {
            var guestBadges = new List<BadgeBase>();

            if (registrant.Guests != null && registrant.Guests.Count > 0)
            {
                foreach (var guest in registrant.Guests)
                {
                    var badge = new GuestBadge();
                    var customer = new CustomerDto();
                    var result = HttpClientHelper.GetJson<CustomerDto>(customerService,
                        $"individual/{registrant.CustomerKey}/event");

                    if (result.StatusCode == HttpStatusCode.OK)
                    {
                        customer = result.Data;
                    }
                    else
                    {
                        throw new ServiceException(result.ErrorMessage);
                    }

                    var evt = EventDao.GetEventBaseByKey(registrant.EventKey);
                    var displayName = registrant.InvoiceDetail.PriceDisplayName;

                    badge.Name = guest.Name;
                    badge.Position = string.Empty;

                    if (string.IsNullOrWhiteSpace(guest.Location))
                    {
                        guest.City = registrant.City;
                        guest.State = registrant.State;
                        guest.Country = registrant.Country;
                        badge.Address = registrant.Country == "UNITED STATES" ? $"{guest.City}, {guest.State}" : $"{guest.City}, {guest.State}, {guest.Country}";
                    }
                    else
                    {
                        badge.Address = guest.Location;
                    }

                    badge.EventCode = evt.Code;
                    badge.MemberId = customer.CustomerId;
                    badge.AttendeeType = displayName;
                    guestBadges.Add(badge);
                }
            }

            return guestBadges;
        }

        public List<BadgeBase> GetRegistrantSessionBadges(Guid registrantKey)
        {
            var registrant = RegistrantDao.GetRegistrantByKey(registrantKey);

            return GetRegistrantSessionBadges(registrant);
        }

        public List<BadgeBase> GetRegistrantSessionBadges(Registrant registrant)
        {
            var badges = new List<BadgeBase>();

            if (registrant.Sessions != null && registrant.Sessions.Count > 0)
            {
                foreach (var registrantSession in registrant.Sessions)
                {
                    var badge = new SessionBadge();
                    var session = registrantSession.Session;

                    if (!registrantSession.CancelDate.HasValue && session.PrintTicket.HasValue && session.PrintTicket.Value == true)
                    {
                        var evt = EventDao.GetEventBaseByKey(registrant.EventKey);
                        var displayName = registrant.InvoiceDetail != null ? registrant.InvoiceDetail.PriceDisplayName : string.Empty;
                        var customer = new CustomerDto();
                        var result = HttpClientHelper.GetJson<CustomerDto>(customerService, $"individual/{registrant.CustomerKey}/event");

                        if (result.StatusCode == HttpStatusCode.OK)
                        {
                            customer = result.Data;
                        }
                        else
                        {
                            throw new ServiceException(result.ErrorMessage);
                        }

                        var numberOfBadges = (int)registrantSession.InvoiceDetail.Quantity;

                        for (var i = 0; i < numberOfBadges; i++)
                        {
                            badge.Company = string.IsNullOrEmpty(registrant.Organization) ? string.Empty : registrant.Organization;
                            badge.EventName = evt.Title;
                            badge.EventDateAndLocation = FormatEventDateAndLocation(evt.StartDate, evt.EndDate, evt.LocationCity, evt.LocationState, evt.LocationCountry); ;
                            badge.SessionCode = session.Code;
                            badge.SessionName = session.Title;
                            badge.SessionDate = session.Date?.ToString("dddd, MMMM d") ?? string.Empty;
                            badge.SessionDate += string.IsNullOrEmpty(session.Time) ? " " : ", " + session.Time;
                            badge.Location = GetSessionRooms(session.Key);
                            badge.AttendeeName = FormatFullName(customer);
                            badge.Fee = registrantSession.InvoiceDetail.Price.ToString("C");
                            badge.EventCode = evt.Code;
                            badge.MemberId = customer.CustomerId;
                            badge.AttendeeType = displayName;

                            badges.Add(badge);
                        }
                    }
                }
            }

            return badges;
        }

        public List<BadgeBase> GetRegistrantSessionBadge(Guid registrantSessionKey)
        {
            var badges = new List<BadgeBase>();
            var registrantSession = RegistrantSessionDao.GetByKey(registrantSessionKey);
            var session = registrantSession.Session;

            if (session == null)
                session = SessionDao.GetByKey(registrantSession.Session.Key);

            if (!registrantSession.CancelDate.HasValue && session.PrintTicket.HasValue &&
                session.PrintTicket.Value == true)
            {
                var registrant = registrantSession.Registrant;
                var customer = new CustomerDto();
                var result = HttpClientHelper.GetJson<CustomerDto>(customerService, $"individual/{registrant.CustomerKey}/event");

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    customer = result.Data;
                }
                else
                {
                    throw new ServiceException(result.ErrorMessage);
                }

                var evt = EventDao.GetEventBaseByKey(registrant.EventKey);
                var numberOfBadges = (int)registrantSession.InvoiceDetail.Quantity;

                for (var index = 0; index < numberOfBadges; index++)
                {
                    var badge = new SessionBadge();
                    badge.Company = string.IsNullOrEmpty(registrant.Organization) ? " " : registrant.Organization;
                    badge.EventName = evt.Title;
                    badge.EventDateAndLocation = FormatEventDateAndLocation(evt.StartDate, evt.EndDate, evt.LocationCity, evt.LocationState, evt.LocationCountry);
                    badge.SessionCode = session.Code;
                    badge.SessionName = session.Title;
                    badge.SessionDate = session.Date?.ToString("dddd, MMMM d") ?? string.Empty;
                    badge.SessionDate += string.IsNullOrEmpty(session.Time) ? " " : ", " + session.Time;
                    badge.Location = GetSessionRooms(session.Key);
                    badge.AttendeeName = FormatFullName(customer);
                    badge.Fee = registrantSession.InvoiceDetail.Price.ToString("C");
                    badges.Add(badge);
                }
            }

            return badges;
        }

        public List<BadgeBase> GetEventBadges(Guid eventKey, DateTime? startDate, DateTime? endDate)
        {
            var badges = new List<BadgeBase>();
            var registrants = new List<Registrant>();

            if (startDate.HasValue)
            {
                if (endDate.HasValue)
                {
                    registrants = RegistrantDao.GetRegistrantsForEventByDate(eventKey, startDate.Value, endDate.Value);
                }
                else
                {
                    registrants = RegistrantDao.GetRegistrantsForEventByDate(eventKey, startDate.Value, DateTime.Now);
                }
            }
            else
            {
                registrants = RegistrantDao.GetRegistrantsForEvent(eventKey);
            }

            if (registrants != null && registrants.Count > 0)
            {
                var orderedRegistrants = registrants
                    .Select(r => new { Registrant = r, RegistrantBadge = GetRegistrantBadge(r) })
                    .Where(tmp => tmp.RegistrantBadge != null)
                    .OrderBy(r => ((RegistrantBadge)r.RegistrantBadge).LastName).ThenBy(r => ((RegistrantBadge)r.RegistrantBadge).Nickname);

                foreach (var item in orderedRegistrants)
                {
                    if (item.RegistrantBadge != null)
                    {
                        badges.Add(item.RegistrantBadge);

                        // Now, let's get all the guests for the registrant
                        var guestBadges = GetGuestBadges(item.Registrant);

                        if (guestBadges.Count > 0)
                            badges.AddRange(guestBadges);

                        // And finally, let's get all the sessions for the registrant
                        if (item.Registrant.Sessions != null && item.Registrant.Sessions.Count > 0)
                        {
                            var sessionBadges = GetRegistrantSessionBadges(item.Registrant);
                            badges.AddRange(sessionBadges);
                        }
                    }
                }
            }

            return badges;
        }

        private string FormatEventDateAndLocation(DateTime? startDate, DateTime? endDate, string locationCity, string locationState, string locationCountry)
        {
            var builder = new StringBuilder();

            if (startDate.HasValue)
            {
                builder.Append(startDate.Value.ToString("m"));

                if (endDate.HasValue)
                {
                    if (endDate.Value.Month == startDate.Value.Month)
                    {
                        // September 16-22, 2008
                        builder.Append("-").Append(endDate.Value.Day).Append(", ").Append(startDate.Value.Year);
                    }
                    else
                    {
                        if (endDate.Value.Year == startDate.Value.Year)
                        {
                            // September 29-October 3, 2008
                            builder.Append("-").Append(endDate.Value.ToString("m")).Append(", ").Append(startDate.Value.Year);
                        }
                        else
                        {
                            // December 29, 2008-January 1, 2009
                            builder.Append(", ").Append(startDate.Value.Year).Append("-")
                                .Append(endDate.Value.ToString("m")).Append(", ").Append(endDate.Value.Year);
                        }
                    }
                }
            }
            else
            {
                if (endDate.HasValue)
                {
                    builder.Append(endDate.Value.ToString("m")).Append(", ").Append(endDate.Value.Year);
                }
            }

            if (!string.IsNullOrWhiteSpace(locationCountry) && !string.IsNullOrWhiteSpace(locationState) && !string.IsNullOrWhiteSpace(locationCity))
            {
                if (builder.Length > 0) builder.Append(" ");
                builder.Append(ConstructAddress(locationCountry, locationState, locationCity));
            }

            return builder.ToString();
        }

        private string GetSessionRooms(Guid sessionKey)
        {
            var rooms = SessionDao.GetSessionRoom(sessionKey);
            var result = new List<string>(rooms.Count);

            foreach (var r in rooms)
            {
                var eventRoom = r[0] ?? string.Empty;
                var sessionRoom = r[1] ?? string.Empty;
                result.Add(eventRoom + " " + sessionRoom);
            }

            return result.Count > 0 ? result[0] : string.Empty;
        }

        private string ConstructAddress(string country, string state, string city)
        {
            var addressBuilder = new StringBuilder();
            var hasPrevious = false;

            if (!string.IsNullOrEmpty(city))
            {
                addressBuilder.Append(city);
                hasPrevious = true;
            }

            if (!string.IsNullOrEmpty(state))
            {
                if (hasPrevious)
                    addressBuilder.Append(", ");

                addressBuilder.Append(state);
                hasPrevious = true;
            }

            if (!string.IsNullOrEmpty(country) && country != "UNITED STATES")
            {
                if (hasPrevious)
                    addressBuilder.Append(", ");

                addressBuilder.Append(country);
            }

            return addressBuilder.ToString();
        }

        private string FormatFullName(CustomerDto individual)
        {
            if (individual == null)
                return string.Empty;

            var fullName = individual.FullNameMinusPrefix;

            return fullName;
        }
    }
}