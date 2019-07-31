using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Aafp.Events.Api.ApplicationConfig;
using Aafp.Events.Api.Dao;
using Aafp.Events.Api.Dao.Interfaces;
using Aafp.Events.Api.Dtos;
using Aafp.Events.Api.Dtos.Admin.Registration;
using Aafp.Events.Api.Dtos.Customer;
using Aafp.Events.Api.Dtos.EditedRegistration;
using Aafp.Events.Api.Dtos.Registrant;
using Aafp.Events.Api.Dtos.User.Registration;
using Aafp.Events.Api.Models;
using Aafp.Events.Api.Tasks.Admin.Interfaces;
using ApiClientHelper.Components;
using Avectra.netForum.Common;
using Avectra.netForum.Data;
using log4net;
using PendingRegistrationSession = Aafp.Events.Api.Models.PendingRegistrationSession;
using EditedRegistrationSession = Aafp.Events.Api.Models.EditedRegistrationSession;

namespace Aafp.Events.Api.Tasks.Admin
{
    public class AdminRegistrationTasks : IAdminRegistrationTasks
    {
        protected static readonly ILog Log = LogManager.GetLogger("EventService");

        private string customerService = ApplicationConfigManager.Settings.CustomerServiceUrl;

        private string referenceService = ApplicationConfigManager.Settings.ReferenceServiceUrl;

        private string paymentService = ApplicationConfigManager.Settings.PaymentServiceUrl;

        public IEventDao EventDao { get; set; }

        public IFeeDao FeeDao { get; set; }

        public IGuestDao GuestDao { get; set; }

        public IPendingRegistrationDao PendingRegistrationDao { get; set; }

        public IEditedRegistrationDao EditedRegistrationDao { get; set; }

        public IRegistrationInvoiceDetailDao RegistrationInvoiceDetailDao { get; set; }

        public IRegistrantDao RegistrantDao { get; set; }

        public IRegistrantOnWaitDao RegistrantOnWaitDao { get; set; }

        public ISessionDao SessionDao { get; set; }

        public List<CustomerSearchResultDto> GetAdminCustomerSearchResults(string searchTerm)
        {
            var customerItems = new List<CustomerSearchResultDto>();
            var itemsToRemove = new List<Guid>();

            var searchResults = HttpClientHelper.GetJson<List<CustomerSearchResultDto>>(customerService, "customers/search?searchTerm=" + searchTerm);

            if (searchResults.StatusCode == HttpStatusCode.OK)
            {
                customerItems = searchResults.Data;
            }
            else
            {
                throw new ServiceException(searchResults.ErrorMessage);
            }

            var customerKeyList = string.Join(",", customerItems.Select(x => x.CustomerKey));
            var eventItems = EventDao.GetCustomerEvents(customerKeyList);
            var results = AutoMapper.Mapper.Map(customerItems, new List<CustomerSearchResultDto>());

            foreach (var result in results)
            {
                var events = eventItems.Where(x => x.CustomerKey == result.CustomerKey).ToList();

                result.Events = AutoMapper.Mapper.Map(events, new List<CustomerEventDto>());

                foreach (var item in result.Events)
                {
                    if (item.IsPending)
                    {
                        var relatedRegistrations = PendingRegistrationDao.GetByParentKey(item.RegistrationKey);

                        foreach (var relatedItem in relatedRegistrations)
                        {
                            item.RelatedRegistrationKeys.Add(relatedItem.Key);
                        }

                        var registrant = RegistrantDao.GetRegistrant(item.EventKey, item.CustomerKey);

                        if (registrant != null)
                        {
                            itemsToRemove.Add(item.RegistrationKey);
                        }
                    }
                }

                result.Events.RemoveAll(x => itemsToRemove.Contains(x.RegistrationKey));
            }

            return results;
        }

        public List<EventBaseDto> GetAdminRegistrationEvents()
        {
            return EventDao.GetAdminRegistrationEvents();
        }

        public EventRegistrationTypeInfoDto GetEventRegistrationTypesByCustomerKey(Guid eventKey, Guid customerKey, DateTime registrationDate)
        {
            var eventItem = EventDao.GetEventBaseByKey(eventKey);
            var dto = AutoMapper.Mapper.Map(eventItem, new EventRegistrationTypeInfoDto());
            dto.Fees = AutoMapper.Mapper.Map(FeeDao.GetEventFeesByCustomerForAdmin(eventKey, customerKey, registrationDate), new List<EventFeeDto>());

            return dto;
        }

        public EventRegistrationTypeInfoDto GetEventRegistrationTypesByWebLogin(Guid eventKey, string webLogin, DateTime registrationDate)
        {
            var eventItem = EventDao.GetByKey(eventKey);
            var individual = new CustomerDto();
            var result = HttpClientHelper.GetJson<CustomerDto>(customerService, $"individual/by-weblogin/{webLogin}/event");

            if (result.StatusCode == HttpStatusCode.OK)
            {
                individual = result.Data;
            }
            else
            {
                throw new ServiceException(result.ErrorMessage);
            }

            var dto = AutoMapper.Mapper.Map(eventItem, new EventRegistrationTypeInfoDto());
            dto.Fees = AutoMapper.Mapper.Map(FeeDao.GetEventFeesByCustomer(eventKey, individual.Key, individual.IsMember, registrationDate), new List<EventFeeDto>());

            return dto;
        }

        public AdminRegistrationDto GetRegistrationFromPendingRegistration(Guid registrationKey)
        {
            var pendingRegistration = PendingRegistrationDao.GetByKey(registrationKey);

            if (pendingRegistration == null)
                return null;

            var registrationDto = ConvertPendingRegistrationToRegistration(pendingRegistration);

            return registrationDto;
        }

        public EditedRegistration GetRegistrationFromEditedRegistration(Guid editedRegistrationKey)
        {
            var editedRegistration = EditedRegistrationDao.GetByKey(editedRegistrationKey);

            return editedRegistration;
        }

        public PendingRegistration GetPendingRegistrationByEvent(Guid eventKey, Guid customerKey)
        {
            var pendingRegistration = PendingRegistrationDao.GetByEventKey(eventKey, customerKey);

            return pendingRegistration;
        }

        public EditedRegistration GetEditedRegistrationByEvent(Guid eventKey, Guid customerKey)
        {
            var editedRegistration = EditedRegistrationDao.GetByEventKey(eventKey, customerKey);

            return editedRegistration;
        }

        public AdminRegistrationDto GetNewRegistration(Guid eventKey, Guid customerKey, Guid registrationTypeKey, DateTime registrationDate)
        {
            var dto = new AdminRegistrationDto();

            var evt = EventDao.GetByKey(eventKey);
            dto.Event = AutoMapper.Mapper.Map(evt, new AdminRegistrationEventDto());
            var customerResult = HttpClientHelper.GetJson<CustomerDto>(customerService, $"individual/{customerKey}/event");

            if (customerResult.StatusCode == HttpStatusCode.OK)
            {
                dto.Customer = customerResult.Data;
            }
            else
            {
                throw new ServiceException(customerResult.ErrorMessage);
            }

            dto.AllowWaitList = evt.AllowWaitList;
            var registrantCount = RegistrantDao.GetEventRegistrantsCount(eventKey);

            if (registrantCount >= evt.Capacity)
            {
                dto.IsSoldOut = true;

                if (evt.RegistrantsOnWait.Any(x => x.CustomerKey == customerKey))
                    dto.UserIsOnWaitList = true;
            }

            if (RegistrantDao.IsRegisteredForEvent(eventKey, customerKey))
            {
                dto.IsRegistered = true;
                var registrant = RegistrantDao.GetRegistrant(eventKey, customerKey);
                var details = RegistrationInvoiceDetailDao.GetInvoiceDetailsByInvoiceCode(registrant.InvoiceCode);
                dto.Key = registrant.Key;
                dto.InvoiceDetails = AutoMapper.Mapper.Map(details, new List<AdminRegistrantInvoiceDetailsDto>());
            }

            dto.Event.Fees = AutoMapper.Mapper.Map(FeeDao.GetEventFeesByCustomerForAdmin(eventKey, customerKey, registrationDate), new List<EventFeeDto>());
            var sessionFees = AutoMapper.Mapper.Map(FeeDao.GetSessionFeesForCustomer(customerKey, eventKey, registrationDate), new List<SessionFeeDto>());

            dto.RegistrationDate = registrationDate;
            dto.PriceKey = registrationTypeKey;
            dto.CustomerAddressKey = dto.Customer.Addresses?.FirstOrDefault(x => x.IsPrimary)?.Key;
            dto.CustomerPhoneKey = dto.Customer.Phones?.FirstOrDefault(x => x.IsPrimary)?.Key;

            dto.Badge = new RegistrationBadgeDto();
            var statesResult = HttpClientHelper.GetJson<List<StateDto>>(referenceService, $"states/country/{dto.Customer.Addresses?.First(x => x.IsPrimary).Country}");

            if (statesResult.StatusCode == HttpStatusCode.OK)
            {
                dto.Badge.States = statesResult.Data;
            }
            else
            {
                throw new ServiceException(statesResult.ErrorMessage);
            }

            dto.Badge.FirstName = dto.Customer.FirstName;
            dto.Badge.LastName = dto.Customer.LastName;
            dto.Badge.NickName = dto.Customer.FirstName;
            dto.Badge.City = dto.Customer.Addresses?.First(x => x.IsPrimary).City;
            dto.Badge.Country = dto.Customer.Addresses?.First(x => x.IsPrimary).Country;
            dto.Badge.State = dto.Customer.Addresses?.First(x => x.IsPrimary).State;

            foreach (var step in dto.Event.Steps)
            {
                step.Headings = step.Headings.OrderBy(x => x.HeadingSequence).ToList();

                foreach (var heading in step.Headings)
                {
                    heading.Sessions = AutoMapper.Mapper.Map(evt.Sessions.Where(x => x.HeadingKey == heading.Key), new List<AdminRegistrationSessionDto>());
                    heading.Sessions = heading.Sessions.OrderBy(x => x.Code).ToList();

                    foreach (var session in heading.Sessions)
                    {
                        session.Fee = sessionFees.FirstOrDefault(x => x.SessionKey == session.Key);

                        if (session.Fee == null)
                        {
                            session.Fee = new SessionFeeDto
                            {
                                Price = 0.00m
                            };
                        }

                        if (session.SessionTypeCode == "Guest Badge")
                        {
                            session.GuestBadges = new List<RegistrationGuestBadgeDto>();

                            for (var index = 0; index < session.MaxTicketQuantity; index++)
                            {
                                session.GuestBadges.Add(new RegistrationGuestBadgeDto());
                            }
                        }

                        if (session.Conflicts.Any())
                        {
                            foreach (var conflict in session.Conflicts)
                            {
                                var conflictedSession = heading.Sessions.FirstOrDefault(x => x.Key == conflict.ConflictSessionKey);

                                if (conflictedSession != null && conflictedSession.Conflicts.All(x => x.ConflictSessionKey != session.Key))
                                {
                                    conflictedSession.Conflicts.Add(new AdminRegistrationSessionConflictDto
                                    {
                                        ConflictSessionKey = session.Key,
                                        ConflictSessionCode = session.Code
                                    });
                                }
                            }
                        }
                    }
                }
            }

            return dto;
        }

        public Registrant GetCustomerEventRegistration(Guid eventKey, Guid customerKey)
        {
            var customerRegistration = RegistrantDao.GetRegistrant(eventKey, customerKey);

            var registrant = new Registrant();

            if (customerRegistration != null)
            {
                registrant.Key = customerRegistration.Key;
                registrant.CustomerKey = customerRegistration.CustomerKey;
                registrant.EventKey = customerRegistration.EventKey;
            }

            return registrant;
        }

        public AdminEditedRegistrationDto SaveRegistration(AdminRegistrationDto registration)
        {
            switch (registration.RegistrationStatus)
            {
                case "New":
                case "Pending":
                    return SavePendingRegistration(registration);
                case "Edit":
                    if (SelectedSessions(registration))
                    {
                        SaveEditRegistration(registration);
                        return SaveEditedRegistration(registration);
                    }
                    else
                    {
                        return SaveEditRegistration(registration);
                    }

                default:
                    return new AdminEditedRegistrationDto();
            }
        }

        public AdminEditedRegistrationDto SavePendingRegistration(AdminRegistrationDto registration)
        {
            var dto = new AdminEditedRegistrationDto();
            var pendingRegistration = ConvertRegistrationToPendingRegistration(registration);

            PendingRegistrationDao.Store(pendingRegistration);
            dto.Key = pendingRegistration.Key;
            dto.PaymentRequired = true;
            return dto;
        }

        public AdminEditedRegistrationDto SaveEditRegistration(AdminRegistrationDto registration)
        {
            var dto = new AdminEditedRegistrationDto();
            var registrant = RegistrantDao.GetRegistrantByKey(registration.Key);

            registrant.AddressKey = registration.CustomerAddressKey;
            registrant.PhoneKey = registration.CustomerPhoneKey;
            registrant.EmergencyContactName = registration.EmergencyContactName;
            registrant.EmergencyContactPhone = registration.EmergencyContactPhone;
            registrant.BadgeName = registration.Badge.NickName;
            registrant.City = registration.Badge.City;
            registrant.State = registration.Badge.State;
            registrant.Country = registration.Badge.Country;
            registrant.Title = registration.Badge.Position;
            registrant.Organization = registration.Badge.Company;
            registrant.Comment = registration.Badge.Notes;
            registrant.ChangeUser = registration.CurrentUser;
            registrant.ChangeDate = DateTime.Now;

            dto.Key = registrant.Key;
            dto.PaymentRequired = false;

            RegistrantDao.Store(registrant);

            return dto;
        }

        public AdminEditedRegistrationDto SaveEditedRegistration(AdminRegistrationDto registration)
        {
            var dto = new AdminEditedRegistrationDto();
            var editedRegistration = ConvertRegistrationToEditedRegistration(registration);

            EditedRegistrationDao.Store(editedRegistration);
            dto.Key = editedRegistration.Key;
            dto.PaymentRequired = true;

            return dto;
        }

        public AdminEditedRegistrationDto GetEditedSessions(Guid editedRegistrationKey)
        {
            var dto = new AdminEditedRegistrationDto();
            dto.EditedSessions = new List<AdminRegistrationSessionDto>();
            dto.EditedGuestBadges = new List<AdminEditRegistrationGuestBadgeDto>();
            var editedRegistrant = EditedRegistrationDao.GetByKey(editedRegistrationKey);
            var registrant = RegistrantDao.GetRegistrant(editedRegistrant.EventKey, editedRegistrant.CustomerKey);
            var sessionFees = AutoMapper.Mapper.Map(FeeDao.GetSessionFeesForCustomer(editedRegistrant.CustomerKey, editedRegistrant.EventKey, editedRegistrant.RegistrationDate), new List<SessionFeeDto>());
            var evt = EventDao.GetByKey(editedRegistrant.EventKey);
            dto.Event = AutoMapper.Mapper.Map(evt, new EventDetailDto());
            dto.Customer = new CustomerDto();
            var result = HttpClientHelper.GetJson<CustomerDto>(customerService, $"individual/{registrant.CustomerKey}/event");

            if (result.StatusCode == HttpStatusCode.OK)
            {
                dto.Customer = result.Data;
            }
            else
            {
                throw new ServiceException(result.ErrorMessage);
            }

            if (editedRegistrant.Sessions != null)
            {
                foreach (var editedSession in editedRegistrant.Sessions)
                {
                    if (registrant.Sessions != null)
                    {
                        var session = SessionDao.GetByKey(editedSession.SessionKey);
                        var pendingSessionFee = sessionFees.FirstOrDefault(sessionFee => sessionFee.SessionKey == editedSession.SessionKey);

                        dto.EditedSessions.Add(new AdminRegistrationSessionDto
                        {
                            Key = editedSession.SessionKey,
                            Code = session.Code,
                            Title = session.Title,
                            Date = session.Date,
                            SelectedQuantity = editedSession.SelectedQuantity,
                            Fee = pendingSessionFee,
                            Removed = false,
                            Selected = true
                        });
                    }
                }

                if (registrant.Sessions != null)
                {
                    foreach (var session in registrant.Sessions.Where(x => !x.CancelDate.HasValue))
                    {
                        dto.EditedSessions.Add(new AdminRegistrationSessionDto
                        {
                            Key = session.Session.Key,
                            Code = session.Session.Code,
                            Title = session.Session.Title,
                            Date = session.Session.Date,
                            SelectedQuantity = Convert.ToInt32(session.InvoiceDetail.Quantity),
                            Fee = new SessionFeeDto()
                            {
                                Price = session.InvoiceDetail.Price
                            },
                            Removed = true,
                            Selected = false
                        });
                    }

                    var duplicateSessions = dto.EditedSessions.GroupBy(x => x.Key).Where(x => x.Count() > 1);

                    foreach (var duplicate in duplicateSessions)
                    {
                        foreach (var newSession in dto.EditedSessions.Where(x => x.Key == duplicate.Key).ToList())
                        {
                            if (duplicate.Key == newSession.Key)
                            {
                                dto.EditedSessions.Remove(newSession);
                            }
                        }
                    }

                    var guestBadges = editedRegistrant.Badges.Where(x => x.IsGuest).ToList();

                    if (guestBadges.Count > 0)
                    {
                        foreach (var evtSession in evt.Sessions)
                        {
                            if (evtSession.SessionTypeCode == "Guest Badge")
                            {
                                var guestSession = SessionDao.GetByKey(evtSession.Key);

                                foreach (var guest in guestBadges)
                                {
                                    var editedGuestBadge = new AdminEditRegistrationGuestBadgeDto();
                                    editedGuestBadge.Key = guest.Key;
                                    editedGuestBadge.Name = guest.GuestName;
                                    editedGuestBadge.Location = guest.GuestLocation;
                                    editedGuestBadge.SessionCode = guestSession.Code;
                                    editedGuestBadge.SessionTitle = guestSession.Title;
                                    dto.EditedGuestBadges.Add(editedGuestBadge);
                                }
                            }
                        }
                    }
                }
            }

            dto.Key = editedRegistrationKey;
            dto.RegistrationKey = registrant.Key;
            dto.PaymentRequired = true;
            dto.RegistrationStatus = "Edit";

            return dto;
        }

        public AdminRegistrationDto GetRegistration(Guid registrationKey)
        {
            var registrant = RegistrantDao.GetRegistrantByKey(registrationKey);
            var registration = GetNewRegistration(registrant.EventKey, registrant.CustomerKey,
                registrant.InvoiceDetail.PriceKey.Value, registrant.RegistrationDate);

            registration.Key = registrant.Key;
            registration.Badge.NickName = registrant.BadgeName;
            registration.Badge.State = registrant.State;
            registration.Badge.Company = registrant.Organization;
            registration.Badge.Position = registrant.Title;
            registration.Badge.Notes = registrant.Comment;
            registration.EmergencyContactName = registrant.EmergencyContactName;
            registration.EmergencyContactPhone = registrant.EmergencyContactPhone;
            registration.CustomerAddressKey = registrant.AddressKey;
            registration.CustomerPhoneKey = registrant.PhoneKey;

            foreach (var step in registration.Event.Steps)
            {
                foreach (var heading in step.Headings)
                {
                    foreach (var session in heading.Sessions)
                    {
                        foreach (var registeredSession in registrant.Sessions.Where(x => !x.CancelDate.HasValue))
                        {
                            if (session.Key != registeredSession.Session.Key)
                            {
                                continue;
                            }
                            session.Selected = true;
                            session.SelectedQuantity = (int)registeredSession.InvoiceDetail.Quantity;
                        }

                        if (session.SessionTypeCode == "Guest Badge")
                        {
                            session.GuestBadges = new List<RegistrationGuestBadgeDto>();

                            foreach (var guest in registrant.Guests)
                            {
                                var guestBadge = new RegistrationGuestBadgeDto();
                                guestBadge.Name = guest.Name;
                                guestBadge.Location = guest.Location;
                                session.GuestBadges.Add(guestBadge);
                            }

                            session.Selected = true;
                            session.SelectedQuantity = session.GuestBadges.Count;

                            var guestBadgeCount = session.GuestBadges.Count;

                            for (var index = guestBadgeCount; index < session.MaxTicketQuantity; index++)
                            {
                                session.GuestBadges.Add(new RegistrationGuestBadgeDto());
                            }
                        }
                    }
                }
            }

            return registration;
        }

        public PrintRegistrantDto GetRegistrantForPrinting(Guid registrantKey)
        {
            var dto = new PrintRegistrantDto();
            var customer = new CustomerDto();
            var registrant = RegistrantDao.GetRegistrantByKey(registrantKey);
            var evt = EventDao.GetEventBaseByKey(registrant.EventKey);
            var result = HttpClientHelper.GetJson<CustomerDto>(customerService, $"individual/{registrant.CustomerKey}/event");

            if (result.StatusCode == HttpStatusCode.OK)
            {
                customer = result.Data;
            }
            else
            {
                throw new ServiceException(result.ErrorMessage);
            }

            dto.RegistrantKey = registrant.Key;
            dto.EventKey = evt.Key;
            dto.EventTitle = evt.Title;
            dto.LocationCity = evt.LocationCity;
            dto.LocationState = evt.LocationState;
            dto.Sessions = AutoMapper.Mapper.Map(registrant.Sessions.Where(x => x.Session.PrintTicket.HasValue && x.Session.PrintTicket.Value), new List<AdminRegistrantSessionDto>());
            dto.InvoiceKey = registrant.InvoiceDetail.InvoiceKey.Value;
            dto.FullName = customer.FullNameMinusPrefix;
            dto.CstId = customer.CustomerId;
            return dto;
        }

        public bool RemovePendingRegistration(Guid pendingRegistrationKey)
        {
            var success = false;

            try
            {
                var pendingRegistration = PendingRegistrationDao.GetByKey(pendingRegistrationKey);             

                PendingRegistrationDao.Delete(pendingRegistration);

                success = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }

            return success;

        }

        public bool SaveEmergencyContactInformation(Guid registrationKey, string contactName, string contactPhone)
        {
            var success = false;

            try
            {
                var registrant = RegistrantDao.GetRegistrantByKey(registrationKey);
                registrant.EmergencyContactName = contactName;
                registrant.EmergencyContactPhone = contactPhone;
                RegistrantDao.Store(registrant);
                success = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }

            return success;
        }

        public bool SaveBadgeNotes(Guid registrationKey, string badgeNotes)
        {
            var success = false;

            try
            {
                var registrant = RegistrantDao.GetRegistrantByKey(registrationKey);
                registrant.Comment = badgeNotes;
                RegistrantDao.Store(registrant);
                success = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }

            return success;
        }

        public bool SaveGuestBadges(Guid registrationKey, List<AdminRegistrationGuestBadgeDto> guestBadges)
        {
            var success = false;

            try
            {
                foreach (var guestBadge in guestBadges)
                {
                    var guest = new Guest
                    {
                        AddUser = "AdminRegistration",
                        AddDate = DateTime.Now,
                        DeleteFlag = false,
                        RegistrationKey = registrationKey,
                        Name = guestBadge.Name,
                        Location = guestBadge.Location
                    };

                    GuestDao.Store(guest);
                }

                success = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                success = false;
            }

            return success;
        }

        public bool SaveEditedGuestBadges(Guid registrationKey, List<AdminRegistrationGuestBadgeDto> guestBadges)
        {
            var success = false;

            var registrant = RegistrantDao.GetRegistrantByKey(registrationKey);
            var evt = EventDao.GetByKey(registrant.EventKey);
            var details = RegistrationInvoiceDetailDao.GetInvoiceDetailsByInvoiceCode(registrant.InvoiceCode);
            registrant.Guests.Clear();

            try
            {
                foreach (var guestBadge in guestBadges)
                {
                    var guest = new Guest
                    {
                        AddUser = "AdminRegistration",
                        AddDate = DateTime.Now,
                        DeleteFlag = false,
                        RegistrationKey = registrationKey,
                        Name = guestBadge.Name,
                        Location = guestBadge.Location
                    };

                    GuestDao.Store(guest);
                    registrant.Guests.Add(guest);
                }

                success = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                success = false;
            }

            foreach (var session in evt.Sessions)
            {
                if (session.SessionTypeCode == "Guest Badge")
                {
                    foreach (var regSession in registrant.Sessions)
                    {
                        if (session.Key == regSession.Session.Key)
                        {
                            regSession.Quantity = registrant.Guests.Count;

                            SessionDao.UpdateRegistrantGuestCapacity(regSession.Key,
                                        registrant.Guests.Count);

                            foreach (var invoiceDetail in details)
                            {
                                if (invoiceDetail.Key == regSession.InvoiceDetail.Key)
                                {
                                    RegistrationInvoiceDetailDao.UpdateInvoiceGuestCapacity(invoiceDetail.Key,
                                        registrant.Guests.Count);
                                }
                            }
                        }
                    }
                }
            }                                       

            return success;
        }

        public bool SendConfirmationEmail(Guid registrationKey, string email)
        {
            var success = false;

            try
            {
                // Set netforum user credentials
                Config.SystemOptions = new Hashtable();
                SystemFunctions.LoadSystemOptions();
                Config.SuperUser = true;
                Config.CurrentUserName = "WebUpdate";

                // OleDbConnection and OleDbTransaction
                var connection = DataUtils.GetConnection();
                var transaction = connection.BeginTransaction();

                // Load netforum registrant object by reg_key
                var registrant = DataUtils.InstantiateFacadeObject("EventsRegistrant");
                registrant.CurrentKey = registrationKey.ToString();
                registrant.SelectByKey(connection, transaction);

                // set the email confirmation flag
                registrant.SetValue("inv_send_email_confirmation", "1");

                // Pull email template key from object.  Event must have a confirmation template set in iweb setup
                var emailTemplateKey = registrant.GetValue("evt_cct_key");
                var emailAddress = registrant.GetValue("cst_eml_address_dn");

                if (!string.Equals(emailAddress, email, StringComparison.CurrentCultureIgnoreCase))
                    emailAddress = email;

                // Check to make sure this event has a confirmation template set up
                // and that we actually have an email address for this customer
                if (!string.IsNullOrEmpty(emailAddress) && !string.IsNullOrEmpty(emailTemplateKey))
                {
                    // Send the email
                    DataUtils.SendTemplate(registrant, emailTemplateKey, emailAddress, string.Empty, connection, transaction, false, false);
                    transaction.Commit();
                    success = true;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }

            return success;
        }

        public bool MarkPendingRegistrationAsProcessed(Guid eventKey, Guid customerKey)
        {
            var success = false;

            try
            {
                var pendingRegistration = PendingRegistrationDao.GetByEventKey(eventKey, customerKey);
                pendingRegistration.IsProcessed = true;
                PendingRegistrationDao.Store(pendingRegistration);
                success = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }

            return success;
        }

        public bool MarkEditedRegistrationAsProcessed(Guid eventKey, Guid customerKey)
        {
            var success = false;

            try
            {
                var editedRegistration = EditedRegistrationDao.GetByEventKey(eventKey, customerKey);
                editedRegistration.IsProcessed = true;
                EditedRegistrationDao.Store(editedRegistration);
                success = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }

            return success;
        }

        public bool AddToWaitList(WaitListDto dto)
        {
            var success = false;

            var evt = EventDao.GetByKey(dto.EventKey);

            if (evt.AllowWaitList && evt.RegistrantsOnWait.All(x => x.CustomerKey != dto.CustomerKey))
            {
                var registrant = new RegistrantOnWait
                {
                    AddDate = DateTime.Now,
                    AddUser = "AdminRegistration",
                    DeleteFlag = false,
                    Event = evt,
                    CustomerKey = dto.CustomerKey
                };

                RegistrantOnWaitDao.Store(registrant);
                success = true;
            }

            return success;
        }

        private PendingRegistration ConvertRegistrationToPendingRegistration(AdminRegistrationDto registration)
        {
            PendingRegistration pendingRegistration;

            if (registration.Key != Guid.Empty)
            {
                pendingRegistration = PendingRegistrationDao.GetByKey(registration.Key);
                pendingRegistration.ChangeUser = registration.CurrentUser;
                pendingRegistration.ChangeDate = DateTime.Now;
                pendingRegistration.CustomerAddressKey = registration.CustomerAddressKey;
                pendingRegistration.CustomerPhoneKey = registration.CustomerPhoneKey;
                pendingRegistration.EmergencyContactName = registration.EmergencyContactName;
                pendingRegistration.EmergencyContactPhone = registration.EmergencyContactPhone;
                pendingRegistration.RegistrationDate = registration.RegistrationDate;
                pendingRegistration.PriceKey = registration.PriceKey;
            }
            else
            {
                pendingRegistration = AutoMapper.Mapper.Map(registration, new PendingRegistration());
                pendingRegistration.AddUser = registration.CurrentUser;
                pendingRegistration.AddDate = DateTime.Now;
            }

            if (pendingRegistration.Badges != null)
                pendingRegistration.Badges.Clear();
            else
                pendingRegistration.Badges = new List<PendingRegistrationBadge>();

            if (pendingRegistration.Sessions != null)
                pendingRegistration.Sessions.Clear();
            else
                pendingRegistration.Sessions = new List<PendingRegistrationSession>();

            var attendeeBadge = new PendingRegistrationBadge();
            attendeeBadge.AddUser = registration.CurrentUser;
            attendeeBadge.AddDate = DateTime.Now;
            attendeeBadge.NickName = registration.Badge.NickName;
            attendeeBadge.FirstName = registration.Badge.FirstName;
            attendeeBadge.LastName = registration.Badge.LastName;
            attendeeBadge.Company = registration.Badge.Company;
            attendeeBadge.Position = registration.Badge.Position;
            attendeeBadge.City = registration.Badge.City;
            attendeeBadge.State = registration.Badge.State;
            attendeeBadge.Country = registration.Badge.Country;
            attendeeBadge.Notes = registration.Badge.Notes;
            pendingRegistration.Badges.Add(attendeeBadge);

            foreach (var step in registration.Event.Steps)
            {
                if (step.Headings != null)
                {
                    foreach (var heading in step.Headings)
                    {
                        if (heading.Sessions != null)
                        {
                            foreach (var session in heading.Sessions)
                            {
                                if (session.Selected || session.SelectedQuantity > 0)
                                {
                                    pendingRegistration.Sessions.Add(new PendingRegistrationSession
                                    {
                                        AddUser = registration.CurrentUser,
                                        AddDate = DateTime.Now,
                                        SessionKey = session.Key,
                                        SelectedQuantity = session.SelectedQuantity > 0 ? session.SelectedQuantity : 1
                                    });
                                }
                                else if (session.SessionTypeCode == "Guest Badge" && session.GuestBadges.Any(x => !string.IsNullOrWhiteSpace(x.Name)))
                                {
                                    pendingRegistration.Sessions.Add(new PendingRegistrationSession
                                    {
                                        AddUser = registration.CurrentUser,
                                        AddDate = DateTime.Now,
                                        SessionKey = session.Key,
                                        SelectedQuantity = session.GuestBadges.Count(x => !string.IsNullOrWhiteSpace(x.Name))
                                    });

                                    foreach (var badge in session.GuestBadges.Where(x => !string.IsNullOrWhiteSpace(x.Name)))
                                    {
                                        var guestBadge = new PendingRegistrationBadge();
                                        guestBadge.AddUser = registration.CurrentUser;
                                        guestBadge.AddDate = DateTime.Now;
                                        guestBadge.GuestName = badge.Name;
                                        guestBadge.GuestLocation = badge.Location;
                                        guestBadge.IsGuest = true;
                                        pendingRegistration.Badges.Add(guestBadge);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return pendingRegistration;
        }

        private EditedRegistration ConvertRegistrationToEditedRegistration(AdminRegistrationDto registration)
        {
            EditedRegistration editedRegistration = new EditedRegistration();

            editedRegistration.AddUser = registration.CurrentUser;
            editedRegistration.AddDate = DateTime.Now;
            editedRegistration.CustomerAddressKey = registration.CustomerAddressKey;
            editedRegistration.CustomerPhoneKey = registration.CustomerPhoneKey;
            editedRegistration.EmergencyContactName = registration.EmergencyContactName;
            editedRegistration.EmergencyContactPhone = registration.EmergencyContactPhone;
            editedRegistration.RegistrationDate = registration.RegistrationDate;
            editedRegistration.PriceKey = registration.PriceKey;
            editedRegistration.RegistrantKey = registration.Key;
            editedRegistration.EventKey = registration.Event.Key;
            editedRegistration.CustomerKey = registration.Customer.Key;

            if (editedRegistration.Badges != null)
            {
                editedRegistration.Badges.Clear();
            }
            else
            {
                editedRegistration.Badges = new List<EditedRegistrationBadge>();
            }

            if (editedRegistration.Sessions != null)
            {
                editedRegistration.Sessions.Clear();
            }
            else
            {
                editedRegistration.Sessions = new List<EditedRegistrationSession>();
            }

            var attendeeBadge = new EditedRegistrationBadge();
            attendeeBadge.AddUser = registration.CurrentUser;
            attendeeBadge.AddDate = DateTime.Now;
            attendeeBadge.NickName = registration.Badge.NickName;
            attendeeBadge.FirstName = registration.Badge.FirstName;
            attendeeBadge.LastName = registration.Badge.LastName;
            attendeeBadge.Company = registration.Badge.Company;
            attendeeBadge.Position = registration.Badge.Position;
            attendeeBadge.City = registration.Badge.City;
            attendeeBadge.State = registration.Badge.State;
            attendeeBadge.Country = registration.Badge.Country;
            attendeeBadge.Notes = registration.Badge.Notes;
            editedRegistration.Badges.Add(attendeeBadge);

            foreach (var step in registration.Event.Steps)
            {
                if (step.Headings == null)
                {
                    continue;
                }

                foreach (var heading in step.Headings)
                {
                    if (heading.Sessions == null)
                    {
                        continue;
                    }

                    foreach (var session in heading.Sessions)
                    {
                        if (session.Selected || session.SelectedQuantity > 0)
                        {
                            editedRegistration.Sessions.Add(new EditedRegistrationSession
                            {
                                AddUser = registration.CurrentUser,
                                AddDate = DateTime.Now,
                                SessionKey = session.Key,
                                SelectedQuantity = session.SelectedQuantity > 0 ? session.SelectedQuantity : 1
                            });
                            continue;
                        }

                        if (session.SessionTypeCode != "Guest Badge" ||
                            session.GuestBadges == null ||
                            !session.GuestBadges.Any(x => !string.IsNullOrWhiteSpace(x.Name)))
                        {
                            continue;
                        }

                        // Guest Badges

                        editedRegistration.Sessions.Add(new EditedRegistrationSession
                        {
                            AddUser = registration.CurrentUser,
                            AddDate = DateTime.Now,
                            SessionKey = session.Key,
                            SelectedQuantity = session.GuestBadges.Count(x => !string.IsNullOrWhiteSpace(x.Name))
                        });

                        foreach (var badge in session.GuestBadges.Where(x => !string.IsNullOrWhiteSpace(x.Name)))
                        {
                            var guestBadge = new EditedRegistrationBadge();
                            guestBadge.AddUser = registration.CurrentUser;
                            guestBadge.AddDate = DateTime.Now;
                            guestBadge.GuestName = badge.Name;
                            guestBadge.GuestLocation = badge.Location;
                            guestBadge.IsGuest = true;
                            editedRegistration.Badges.Add(guestBadge);
                        }

                    }
                }
            }

            return editedRegistration;
        }

        private AdminRegistrationDto ConvertPendingRegistrationToRegistration(PendingRegistration pendingRegistration)
        {
            var registration = GetNewRegistration(pendingRegistration.EventKey, pendingRegistration.CustomerKey, pendingRegistration.PriceKey.Value, pendingRegistration.RegistrationDate);
            registration.Key = pendingRegistration.Key;
            registration.CustomerAddressKey = pendingRegistration.CustomerAddressKey;
            registration.CustomerPhoneKey = pendingRegistration.CustomerPhoneKey;
            registration.EmergencyContactName = pendingRegistration.EmergencyContactName;
            registration.EmergencyContactPhone = pendingRegistration.EmergencyContactPhone;

            var attendeeBadge = pendingRegistration.Badges.FirstOrDefault(x => !x.IsGuest);
            if (attendeeBadge != null)
            {
                registration.Badge.NickName = attendeeBadge.NickName;
                registration.Badge.FirstName = attendeeBadge.FirstName;
                registration.Badge.LastName = attendeeBadge.LastName;
                registration.Badge.Company = attendeeBadge.Company;
                registration.Badge.Position = attendeeBadge.Position;
                registration.Badge.City = attendeeBadge.City;
                registration.Badge.State = attendeeBadge.State;
                registration.Badge.Country = attendeeBadge.Country;
                registration.Badge.Notes = attendeeBadge.Notes;
            }

            registration.UnavailableSessions = new List<AdminRegistrationSessionDto>();
            registration.RelatedRegistrations = new List<AdminRegistrationDto>();
            var relatedRegistrations = PendingRegistrationDao.GetByParentKey(pendingRegistration.Key);

            foreach (var relatedRegistration in relatedRegistrations)
            {
                var relatedAdminRegistration = ConvertPendingRegistrationToRegistration(relatedRegistration);
                registration.RelatedRegistrations.Add(relatedAdminRegistration);
            }

            foreach (var step in registration.Event.Steps)
            {
                foreach (var heading in step.Headings)
                {
                    foreach (var session in heading.Sessions)
                    {
                        foreach (var pendingSession in pendingRegistration.Sessions)
                        {
                            if (session.Key != pendingSession.SessionKey)
                            {
                                continue;
                            }

                            if (session.SessionTypeCode == "Guest Badge")
                            {
                                session.GuestBadges = new List<RegistrationGuestBadgeDto>();

                                foreach (var badge in pendingRegistration.Badges.Where(x => x.IsGuest))
                                {
                                    var guestBadge = new RegistrationGuestBadgeDto();
                                    guestBadge.Name = badge.GuestName;
                                    guestBadge.Location = badge.GuestLocation;
                                    session.GuestBadges.Add(guestBadge);
                                }

                                session.Selected = true;
                                session.SelectedQuantity = session.GuestBadges.Count;

                                // used to create amx number of badge fields for registration
                                if (session.GuestBadges.Count < session.MaxTicketQuantity)
                                {
                                    var amount = session.MaxTicketQuantity - session.GuestBadges.Count;

                                    for (var index = 0; index < amount; index++)
                                    {
                                        session.GuestBadges.Add(new RegistrationGuestBadgeDto());
                                    }
                                }
                            }
                            else
                            {
                                session.Selected = true;
                                session.SelectedQuantity = pendingSession.SelectedQuantity;

                                if (session.Capacity <= session.RegisteredTicketsTotal)
                                {
                                    registration.UnavailableSessions.Add(session);
                                }
                            }
                        }
                    }
                }
            }

            return registration;
        }

        public PaymentConfirmationDto GetPaymentConfirmation(Guid registrantKey)
        {

            var dto = new PaymentConfirmationDto();
            var registrant = RegistrantDao.GetRegistrantByKey(registrantKey);
            var relatedRegistrants = RegistrantDao.GetRelatedRegistrations(registrant.EventKey, registrant.CustomerKey);
            dto.RegistrationKey = registrant.Key;
            var evt = EventDao.GetByKey(registrant.EventKey);

            var result = HttpClientHelper.GetJson<CustomerDto>(customerService, $"individual/{registrant.CustomerKey}/event");

            if (result.StatusCode == HttpStatusCode.OK)
            {
                dto.Customer = result.Data;
            }
            else
            {
                throw new ServiceException(result.ErrorMessage);
            }

            dto.RelatedRegistrations = new List<PaymentConfirmationDto>();

            foreach (var relatedRegistrant in relatedRegistrants)
            {
                var relatedRegistration = new PaymentConfirmationDto();
                var registration = RegistrantDao.GetRegistrantByKey(relatedRegistrant.Key);
                relatedRegistration.RegistrationKey = registration.Key;
                relatedRegistration.Event = AutoMapper.Mapper.Map(EventDao.GetByKey(registration.EventKey), new AdminRegistrationEventDto());
                dto.RelatedRegistrations.Add(relatedRegistration);
            }

            dto.Event = AutoMapper.Mapper.Map(evt, new AdminRegistrationEventDto());
            dto.Sessions = AutoMapper.Mapper.Map(registrant.Sessions, new List<AdminRegistrantSessionDto>());

            var details = RegistrationInvoiceDetailDao.GetInvoiceDetailsByInvoiceCode(registrant.InvoiceCode);
            dto.InvoiceDetails = AutoMapper.Mapper.Map(details, new List<AdminRegistrantInvoiceDetailsDto>());

            return dto;
        }

        public EventRegistrationTypeInfoDto GetBatchEventRegistrationInfo(Guid eventKey)
        {
            var eventItem = EventDao.GetEventBaseByKey(eventKey);
            var dto = AutoMapper.Mapper.Map(eventItem, new EventRegistrationTypeInfoDto());
            dto.Fees = AutoMapper.Mapper.Map(FeeDao.GetEventFeesForBatch(eventKey), new List<EventFeeDto>());

            return dto;
        }

        public BatchRegistrationDto SaveBatchEventRegistration(string memberId, Guid eventKey, Guid registrationTypeKey, DateTime registrationDate)
        {
            var dto = new AdminRegistrationDto();
            dto.Badge = new RegistrationBadgeDto();
            dto.Customer = new CustomerDto();
            var batchDto = new BatchRegistrationDto();

            var evt = EventDao.GetByKey(eventKey);
            dto.Event = AutoMapper.Mapper.Map(evt, new AdminRegistrationEventDto());

            try
            {
                var customerResult = HttpClientHelper.GetJson<CustomerDto>(customerService,
                    $"individual/batch/get-individual/{memberId}");

                if (customerResult.StatusCode == HttpStatusCode.OK)
                {
                    dto.Customer = customerResult.Data;
                }
            }
            catch (ServiceException ex)
            {
                batchDto.MemberId = memberId;
                batchDto.CustomerKey = Guid.Empty;
                batchDto.FirstName = string.Empty;
                batchDto.LastName = string.Empty;
                Log.Error(ex.Message);
                batchDto.RegistrationStatus = "Does not exist";

                return batchDto;
            }
            catch (Exception ex)
            {
                batchDto.MemberId = memberId;
                batchDto.CustomerKey = Guid.Empty;
                batchDto.FirstName = string.Empty;
                batchDto.LastName = string.Empty;
                Log.Error(ex.Message);
                batchDto.RegistrationStatus = "Does not exist";

                return batchDto;
            }

            var registration = GetNewRegistration(eventKey, dto.Customer.Key, registrationTypeKey, registrationDate);

            if (registration.IsRegistered)
            {
                batchDto.MemberId = dto.Customer.CustomerId;
                batchDto.CustomerKey = dto.Customer.Key;
                batchDto.FirstName = dto.Customer.FirstName;
                batchDto.LastName = dto.Customer.LastName;
                batchDto.RegistrationStatus = "Already registered";

                return batchDto;
            }
            else
            {
                dto.Customer.FirstName = dto.Customer.FirstName;
                dto.Customer.LastName = dto.Customer.LastName;
                dto.Event.Key = eventKey;
                dto.PriceKey = registrationTypeKey;
                dto.RegistrationDate = registrationDate;
                dto.Badge.NickName = dto.Customer.FirstName;
                dto.Badge.FirstName = dto.Customer.FirstName;
                dto.Badge.LastName = dto.Customer.LastName;
                dto.Badge.City = dto.Customer.Addresses?.First(x => x.IsPrimary).City;
                dto.Badge.Country = dto.Customer.Addresses?.First(x => x.IsPrimary).Country;
                dto.Badge.State = dto.Customer.Addresses?.First(x => x.IsPrimary).State;
                dto.CurrentUser = "AdminRegistration";

                try
                {
                    var paymentResult = HttpClientHelper.GetJson<Guid>(paymentService,
                        $"registration/batch/event/{dto.Event.Key}/customer/{dto.Customer.Key}/type/{dto.PriceKey}?registrationDate=" + registrationDate);

                    if (paymentResult.StatusCode != HttpStatusCode.OK)
                    {
                        throw new ServiceException(paymentResult.ErrorMessage);
                    }
                    else
                    {
                        var payment = paymentResult.Data;
                    }
                }
                catch (ServiceException ex)
                {
                    batchDto.MemberId = memberId;
                    batchDto.CustomerKey = dto.Customer.Key;
                    batchDto.FirstName = dto.Customer.FirstName;
                    batchDto.LastName = dto.Customer.LastName;
                    Log.Error(ex.Message);
                    return batchDto;
                }
                catch (Exception ex)
                {
                    batchDto.MemberId = memberId;
                    batchDto.CustomerKey = dto.Customer.Key;
                    batchDto.FirstName = dto.Customer.FirstName;
                    batchDto.LastName = dto.Customer.LastName;
                    Log.Error(ex.Message);
                    return batchDto;
                }

                batchDto.MemberId = memberId;
                batchDto.CustomerKey = dto.Customer.Key;
                batchDto.FirstName = dto.Customer.FirstName;
                batchDto.LastName = dto.Customer.LastName;
                batchDto.RegistrationStatus = "Successful";

                return batchDto;
            }
        }

        public EditedRegistrationPaymentDto GetCustomerEventRegistrationForEdit(Guid eventKey, Guid customerKey)
        {
            var dto = new EditedRegistrationPaymentDto();
            dto.EditedSessions = new List<EditedRegistrationPaymentSessionDto>();
            dto.EditedGuestBadges = new List<EditedRegistrationGuestBadgeDto>();
            var registrant = RegistrantDao.GetRegistrant(eventKey, customerKey);
            var editedRegistrant = EditedRegistrationDao.GetByRegistrationKey(registrant.Key);
            var sessionFees = AutoMapper.Mapper.Map(FeeDao.GetSessionFeesForCustomer(editedRegistrant.CustomerKey, editedRegistrant.EventKey, editedRegistrant.RegistrationDate), new List<SessionFeeDto>());

            var editedRegistrantSessions = GetEditedSessions(editedRegistrant.Key);

            dto.Key = registrant.Key;
            dto.InvoiceTermsKey = registrant.InvoiceDetail.InvoiceAitKey;

            foreach (var editedSession in editedRegistrantSessions.EditedSessions)
            {
                var editedSessionFee = sessionFees.FirstOrDefault(sessionFee => sessionFee.SessionKey == editedSession.Key);

                if (editedSession.Removed)
                {
                    dto.EditedSessions.Add(new EditedRegistrationPaymentSessionDto
                    {
                        Key = null,
                        SessionKey = editedSession.Key,
                        Quantity = 0,
                        Removed = true,
                        Selected = false
                    });
                }
                else
                {
                    dto.EditedSessions.Add(new EditedRegistrationPaymentSessionDto
                    {
                        Key = null,
                        SessionKey = editedSession.Key,
                        Quantity = editedSession.SelectedQuantity,
                        Fee = editedSessionFee,
                        Removed = false,
                        Selected = true
                    });
                }

            }

            foreach (var dtoEditedSession in dto.EditedSessions)
            {
                if (dtoEditedSession.Removed)
                {
                    foreach (var registrantSession in registrant.Sessions)
                    {
                        if (dtoEditedSession.SessionKey == registrantSession.Session.Key)
                        {
                            dtoEditedSession.Key = registrantSession.Key;
                            dtoEditedSession.Fee = new SessionFeeDto();
                            dtoEditedSession.Fee.Price = registrantSession.InvoiceDetail.Price;
                            dtoEditedSession.Fee.PriceKey = registrantSession.InvoiceDetail.PriceKey;
                        }
                    }
                }
            }

            var guestBadges = editedRegistrantSessions.EditedGuestBadges;

            if (guestBadges.Count > 0)
            {
                foreach (var guest in guestBadges)
                {
                    var editedGuestBadge = new EditedRegistrationGuestBadgeDto();
                    editedGuestBadge.Key = guest.Key;
                    editedGuestBadge.Name = guest.Name;
                    editedGuestBadge.Location = guest.Location;
                    editedGuestBadge.SessionCode = guest.SessionCode;
                    editedGuestBadge.SessionTitle = guest.SessionTitle;
                    dto.EditedGuestBadges.Add(editedGuestBadge);
                }
            }

            dto.CurrentTotal = GetEditedCurrentTotal(registrant.CustomerKey, registrant.EventKey, editedRegistrantSessions.EditedSessions);

            dto.Zip = editedRegistrantSessions.Customer.Addresses?.FirstOrDefault(x => x.IsBilling)?.PostalCode;

            return dto;
        }

        private bool SelectedSessions(AdminRegistrationDto registration)
        {
            foreach (var step in registration.Event.Steps)
            {
                if (step.Headings != null)
                {
                    foreach (var heading in step.Headings)
                    {
                        if (heading.Sessions != null)
                        {
                            foreach (var session in heading.Sessions)
                            {
                                if (session.Selected == true)
                                {
                                    registration.PaymentRequired = true;
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            registration.PaymentRequired = false;
            return false;
        }

        private decimal GetEditedCurrentTotal(Guid customerKey, Guid eventKey, List<AdminRegistrationSessionDto> sessions)
        {
            var total = 0m;

            if (sessions.Any())
            {
                var sessionFees = AutoMapper.Mapper.Map(FeeDao.GetSessionFeesForCustomer(customerKey, eventKey, DateTime.Now), new List<SessionFeeDto>());

                foreach (var session in sessions)
                {
                    var sessionFee = sessionFees.FirstOrDefault(x => x.SessionKey == session.Key);

                    if (sessionFee != null)
                    {
                        total += sessionFee.Price * session.SelectedQuantity;
                    }
                }
            }

            return total;
        }
    }
}