using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Aafp.Events.Api.ApplicationConfig;
using Aafp.Events.Api.Dao.Interfaces;
using Aafp.Events.Api.Dtos;
using Aafp.Events.Api.Dtos.Customer;
using Aafp.Events.Api.Dtos.EditedRegistration;
using Aafp.Events.Api.Dtos.User;
using Aafp.Events.Api.Dtos.User.Registration;
using Aafp.Events.Api.Models;
using Aafp.Events.Api.Tasks.Admin.Interfaces;
using Aafp.Events.Api.Tasks.User.Interfaces;
using ApiClientHelper.Components;
using Avectra.netForum.Common;
using Avectra.netForum.Data;
using log4net;
using PendingRegistrationSession = Aafp.Events.Api.Models.PendingRegistrationSession;

namespace Aafp.Events.Api.Tasks.User
{
    public class UserRegistrationTasks : IUserRegistrationTasks
    {
        protected static readonly ILog Log = LogManager.GetLogger("EventService");

        private string customerService = ApplicationConfigManager.Settings.CustomerServiceUrl;

        private string referenceService = ApplicationConfigManager.Settings.ReferenceServiceUrl;

        public IEventDao EventDao { get; set; }

        public IFeeDao FeeDao { get; set; }

        public IPendingRegistrationDao PendingRegistrationDao { get; set; }

        public IEditedRegistrationDao EditedRegistrationDao { get; set; }

        public IRegistrantDao RegistrantDao { get; set; }

        public IRegistrationInvoiceDetailDao RegistrationInvoiceDetailDao { get; set; }

        public IAdminRegistrationTasks AdminRegistrationTasks { get; set; }

        public IStepDao StepDao { get; set; }

        public ISessionDao SessionDao { get; set; }

        public List<UserRegistrationDto> GetRegistrationsForUserProfile(string webLogin)
        {
            var dto = new List<UserRegistrationDto>();
            var customer = GetCustomerByWebLogin(webLogin);
            var registrations = RegistrantDao.GetUserRegistrationsForHomePage(customer.Key);
            var pendingRegistrations = registrations.Where(x => x.Type == "pending").OrderByDescending(x => x.EventStartDate).ToList();
            var currentRegistrations = registrations.Where(x => x.Type == "current").OrderByDescending(x => x.EventStartDate).ToList();

            foreach (var pendingRegistration in pendingRegistrations.Where(x => currentRegistrations.All(y => y.EventKey != x.EventKey)))
            {
                if (pendingRegistration.PostToWebDate.HasValue && pendingRegistration.RemoveFromWebDate.HasValue)
                {
                    if (DateTime.Now < pendingRegistration.PostToWebDate.Value.Date || DateTime.Now > pendingRegistration.RemoveFromWebDate.Value.Date)
                        break;

                    var registrantCount = RegistrantDao.GetEventRegistrantsCount(pendingRegistration.Key);

                    if (registrantCount >= pendingRegistration.Capacity)
                        break;

                    dto.Add(pendingRegistration);
                }
            }

            return dto;
        }

        public UserRegistrationHomeDto GetRegistrationHome(string webLogin)
        {
            var dto = new UserRegistrationHomeDto();
            dto.PendingRegistrations = new List<UserRegistrationDto>();
            dto.CurrentRegistrations = new List<UserRegistrationDto>();
            dto.UpcomingRegistrations = new List<UserRegistrationDto>();
            var customer = GetCustomerByWebLogin(webLogin);
            var registrations = RegistrantDao.GetUserRegistrationsForHomePage(customer.Key);
            var pendingRegistrations = registrations.Where(x => x.Type == "pending").OrderByDescending(x => x.EventStartDate).ToList();
            var currentRegistrations = registrations.Where(x => x.Type == "current").OrderByDescending(x => x.EventStartDate).ToList();
            var upcomingRegistrations = registrations.Where(x => x.Type == "upcoming").OrderBy(x => x.EventStartDate).ToList();

            foreach (var pendingRegistration in pendingRegistrations.Where(x => currentRegistrations.All(y => y.EventKey != x.EventKey)))
            {
                if (pendingRegistration.PostToWebDate.HasValue && pendingRegistration.RemoveFromWebDate.HasValue)
                {
                    if (DateTime.Now < pendingRegistration.PostToWebDate.Value.Date || DateTime.Now > pendingRegistration.RemoveFromWebDate.Value.Date)
                        break;

                    var registrantCount = RegistrantDao.GetEventRegistrantsCount(pendingRegistration.Key);

                    if (registrantCount >= pendingRegistration.Capacity)
                        break;

                    dto.PendingRegistrations.Add(pendingRegistration);
                }
            }

            dto.CurrentRegistrations.AddRange(currentRegistrations);

            foreach (var upcomingRegistration in upcomingRegistrations.Where(x => currentRegistrations.All(y => y.EventKey != x.EventKey)))
            {
                if (upcomingRegistration.PostToWebDate.HasValue && upcomingRegistration.RemoveFromWebDate.HasValue)
                {
                    if (DateTime.Now < upcomingRegistration.PostToWebDate.Value.Date || DateTime.Now > upcomingRegistration.RemoveFromWebDate.Value.Date)
                      continue;

                    var registrantCount = RegistrantDao.GetEventRegistrantsCount(upcomingRegistration.Key);

                    if (registrantCount >= upcomingRegistration.Capacity)
                    {
                        if (!upcomingRegistration.AllowWaitList)
                            continue;

                        var evt = EventDao.GetByKey(upcomingRegistration.Key);

                        if (evt.RegistrantsOnWait.Any(x => x.CustomerKey == customer.Key))
                            upcomingRegistration.UserIsOnWaitList = true;
                    }

                    dto.UpcomingRegistrations.Add(upcomingRegistration);
                }
            }

            return dto;
        }

        public UserRegistrationIntroDto GetNewRegistrationIntro(string eventCode, string webLogin)
        {
            var dto = new UserRegistrationIntroDto();
            var evt = EventDao.GetByCode(eventCode);

            if (evt == null)
                return null;

            var customer = GetCustomerByWebLogin(webLogin);

            var pendingRegistration = PendingRegistrationDao.GetByCustomerEvent(evt.Key, customer.Key);

            if (pendingRegistration != null)
            {
                dto = GetRegistrationIntro(pendingRegistration.Key);
                return dto;
            }

            dto.IsPending = true;
            dto = PopulateRegistrationIntro(evt, customer);

            dto.RelatedRegistrations = new List<UserRegistrationIntroDto>();
            dto.PendingSteps = new List<EventStepDto>();
            dto.PendingSteps.AddRange(dto.Event.Steps);

            foreach (var relatedEvent in evt.RelatedEvents)
            {
                var relatedDto = new UserRegistrationIntroDto();
                relatedDto = PopulateRegistrationIntro(relatedEvent, customer);

                dto.RelatedRegistrations.Add(relatedDto);
                dto.PendingSteps.AddRange(relatedDto.Event.Steps);
            }

            return dto;
        }

        public UserRegistrationIntroDto GetRegistrationIntro(Guid pendingRegistrationKey)
        {
            var dto = new UserRegistrationIntroDto();
            dto.RegistrationKey = pendingRegistrationKey;
            var pendingRegistration = PendingRegistrationDao.GetByKey(pendingRegistrationKey);

            if (pendingRegistration == null)
                return null;

            var relatedPendingRegistrations = PendingRegistrationDao.GetByParentKey(pendingRegistrationKey);
            var evt = EventDao.GetByKey(pendingRegistration.EventKey);
            dto.Event = AutoMapper.Mapper.Map(evt, new UserEventDto());
            dto.RelatedRegistrations = new List<UserRegistrationIntroDto>();

            dto.PendingSteps = new List<EventStepDto>();
            dto.PendingSteps.AddRange(dto.Event.Steps);

            dto.Customer = GetCustomerByKey(pendingRegistration.CustomerKey);
            dto.Event.Fees = AutoMapper.Mapper.Map(FeeDao.GetEventFeesByCustomer(dto.Event.Key, dto.Customer.Key, dto.Customer.IsMember, DateTime.Today), new List<EventFeeDto>());
            dto.SelectedPriceKey = pendingRegistration.PriceKey;
            dto.CurrentTotal = GetCurrentTotal(dto.Customer.Key, dto.Event, pendingRegistration.PriceKey.Value, pendingRegistration.Sessions.ToList());

            var registrantCount = RegistrantDao.GetEventRegistrantsCount(evt.Key);
            dto.IsRegistered = RegistrantDao.IsRegisteredForEvent(evt.Key, dto.Customer.Key);

            if (dto.IsRegistered)
            {
                dto.RegistrationKey = RegistrantDao.GetRegistrant(evt.Key, dto.Customer.Key).Key;
            }

            if (registrantCount >= dto.Event.Capacity)
            {
                dto.Event.IsSoldOut = true;

                if (evt.RegistrantsOnWait.Any(x => x.CustomerKey == dto.Customer.Key))
                    dto.UserIsOnWaitList = true;
            }

            var eventSessionsCost = 0.0m;
            var sessionFees = FeeDao.GetSessionFeesForCustomer(dto.Customer.Key, dto.Event.Key, pendingRegistration.RegistrationDate);
            foreach (var pendingSession in pendingRegistration.Sessions)
            {
                var pendingSessionFee = sessionFees.FirstOrDefault(sessionFee => sessionFee.SessionKey == pendingSession.SessionKey);

                if (pendingSessionFee == null)
                {
                    continue;
                }

                eventSessionsCost += pendingSessionFee.Price;
            }
            dto.CurrentSessionsCost = eventSessionsCost;

            foreach (var relatedEvent in evt.RelatedEvents)
            {
                var relatedDto = new UserRegistrationIntroDto();
                relatedDto = PopulateRegistrationIntro(relatedEvent, dto.Customer);
                var relatedPendingRegistration = relatedPendingRegistrations.Find(reg => reg.EventKey == relatedEvent.Key);

                if (relatedPendingRegistration != null)
                {
                    relatedDto.RegistrationKey = relatedPendingRegistration.Key;
                    relatedDto.SelectedPriceKey = relatedPendingRegistration.PriceKey;
                }

                dto.RelatedRegistrations.Add(relatedDto);
                dto.PendingSteps.AddRange(relatedDto.Event.Steps);
            }

            foreach (var relatedPendingRegistration in relatedPendingRegistrations)
            {
                var relatedEvent = AutoMapper.Mapper.Map(EventDao.GetByKey(relatedPendingRegistration.EventKey), new UserEventDto());
                relatedEvent.Fees = AutoMapper.Mapper.Map(FeeDao.GetEventFeesByCustomer(relatedEvent.Key, dto.Customer.Key, dto.Customer.IsMember, DateTime.Today), new List<EventFeeDto>());
                var relatedTotal = GetCurrentTotal(dto.Customer.Key, relatedEvent, relatedPendingRegistration.PriceKey.Value, relatedPendingRegistration.Sessions.ToList());

                var relatedEventSessionsCost = 0.0m;
                var relatedSessionFees = FeeDao.GetSessionFeesForCustomer(dto.Customer.Key, relatedEvent.Key, relatedPendingRegistration.RegistrationDate);
                foreach (var pendingSession in relatedPendingRegistration.Sessions)
                {
                    var pendingSessionFee = relatedSessionFees.Find(sessionFee => sessionFee.SessionKey == pendingSession.SessionKey);
                    relatedEventSessionsCost += pendingSessionFee.Price;
                }
                var relatedDto = dto.RelatedRegistrations.Find(reg => reg.Event.Key == relatedPendingRegistration.EventKey);
                var relatedDtoIndex = dto.RelatedRegistrations.IndexOf(relatedDto);
                relatedDto.CurrentSessionsCost = relatedEventSessionsCost;
                dto.RelatedRegistrations[relatedDtoIndex] = relatedDto;

                dto.PendingSteps.AddRange(relatedEvent.Steps);
                dto.CurrentTotal += relatedTotal;
            }

            if (evt.PostToWebDate.HasValue && evt.RemoveFromWebDate.HasValue)
            {
                if (pendingRegistration.RegistrationDate > evt.PostToWebDate.Value.Date && pendingRegistration.RegistrationDate < evt.RemoveFromWebDate.Value.Date)
                    dto.Event.IsOpenForRegistration = true;
            }

            dto.Status = "Pending";

            return dto;
        }

        public Guid SaveRegistrationIntro(UserRegistrationIntroDto dto)
        {
            var pendingRegistration = new PendingRegistration();
            var webLogin = dto.Customer.WebLogin;
            var customerKey = dto.Customer.Key;
            var customer = GetCustomerByKey(customerKey);

            if (dto.RegistrationKey != Guid.Empty)
            {
                pendingRegistration = PendingRegistrationDao.GetByKey(dto.RegistrationKey);
                pendingRegistration.ChangeUser = webLogin;
                pendingRegistration.ChangeDate = DateTime.Now;
                pendingRegistration.RegistrationDate = DateTime.Now;
                pendingRegistration.PriceKey = dto.SelectedPriceKey;
                pendingRegistration.CustomerAddressKey = customer.Addresses?.FirstOrDefault(x => x.IsPrimary)?.Key;
                pendingRegistration.CustomerPhoneKey = customer.Phones?.FirstOrDefault(x => x.IsPrimary)?.Key;
            }
            else
            {
                pendingRegistration.EventKey = dto.Event.Key;
                pendingRegistration.AddUser = webLogin;
                pendingRegistration.AddDate = DateTime.Now;
                pendingRegistration.CustomerKey = customerKey;
                pendingRegistration.PriceKey = dto.SelectedPriceKey;
                pendingRegistration.RegistrationDate = DateTime.Now;
                pendingRegistration.CustomerAddressKey = customer.Addresses?.FirstOrDefault(x => x.IsPrimary)?.Key;
                pendingRegistration.CustomerPhoneKey = customer.Phones?.FirstOrDefault(x => x.IsPrimary)?.Key;
            }

            PendingRegistrationDao.Store(pendingRegistration);

            if (dto.RelatedRegistrations == null)
            {
                return pendingRegistration.Key;
            }

            foreach (var relatedItem in dto.RelatedRegistrations)
            {
                if (relatedItem.IsRegistered)
                {
                    continue;
                }

                if (relatedItem.RegistrationKey == Guid.Empty)
                {
                    if (relatedItem.Event.IsSoldOut)
                    {
                        continue;
                    }

                    if (relatedItem.SelectedPriceKey == Guid.Empty)
                    {
                        continue;
                    }

                    var relatedPendingRegistration = new PendingRegistration();
                    relatedPendingRegistration.EventKey = relatedItem.Event.Key;
                    relatedPendingRegistration.AddUser = webLogin;
                    relatedPendingRegistration.AddDate = DateTime.Now;
                    relatedPendingRegistration.CustomerKey = customerKey;
                    relatedPendingRegistration.PriceKey = relatedItem.SelectedPriceKey;
                    relatedPendingRegistration.RegistrationDate = DateTime.Now;
                    relatedPendingRegistration.ParentRegistrationKey = pendingRegistration.Key;

                    PendingRegistrationDao.Store(relatedPendingRegistration);
                }
                // pending registration exists
                else
                {
                    var relatedPendingRegistration = PendingRegistrationDao.GetByKey(relatedItem.RegistrationKey);

                    if (relatedItem.Event.IsSoldOut)
                    {
                        RemovePendingRegistration(relatedPendingRegistration, dto.Customer);
                        continue;
                    }

                    // no change to selection
                    if (relatedPendingRegistration.PriceKey == relatedItem.SelectedPriceKey)
                    {
                        continue;
                    }

                    // not attending
                    if (relatedItem.SelectedPriceKey == Guid.Empty)
                    {
                        RemovePendingRegistration(relatedPendingRegistration, dto.Customer);
                        continue;
                    }

                    // change in selection
                    relatedPendingRegistration.ChangeUser = webLogin;
                    relatedPendingRegistration.ChangeDate = DateTime.Now;
                    relatedPendingRegistration.RegistrationDate = DateTime.Now;
                    relatedPendingRegistration.PriceKey = relatedItem.SelectedPriceKey;

                    PendingRegistrationDao.Store(relatedPendingRegistration);
                }
            }

            return pendingRegistration.Key;
        }

        public UserRegistrationContactInfoDto GetRegistrationContactInfo(Guid pendingRegistrationKey)
        {
            var dto = new UserRegistrationContactInfoDto();
            var pendingRegistration = PendingRegistrationDao.GetByKey(pendingRegistrationKey);
            var relatedPendingRegistrations = PendingRegistrationDao.GetByParentKey(pendingRegistrationKey);
            var evt = EventDao.GetByKey(pendingRegistration.EventKey);
            dto.RegistrationKey = pendingRegistration.Key;
            dto.Customer = GetCustomerByKey(pendingRegistration.CustomerKey);
            dto.Event = AutoMapper.Mapper.Map(evt, new UserEventDto());
            dto.Event.Fees = AutoMapper.Mapper.Map(FeeDao.GetEventFeesByCustomer(dto.Event.Key, dto.Customer.Key, dto.Customer.IsMember, DateTime.Today), new List<EventFeeDto>());

            dto.PendingSteps = new List<EventStepDto>();
            dto.PendingSteps.AddRange(dto.Event.Steps);

            if (dto.Customer.Addresses.Any())
            {
                if (pendingRegistration.CustomerAddressKey.HasValue)
                    dto.SelectedAddressKey = pendingRegistration.CustomerAddressKey;
                else
                {
                    if (dto.Customer.Addresses.Any(x => x.IsPrimary))
                        dto.SelectedAddressKey = dto.Customer.Addresses.First(x => x.IsPrimary).Key;
                    else
                        dto.SelectedAddressKey = dto.Customer.Addresses.First().Key;
                }
            }

            if (dto.Customer.Phones.Any())
            {
                if (pendingRegistration.CustomerPhoneKey.HasValue)
                    dto.SelectedPhoneKey = pendingRegistration.CustomerPhoneKey;
                else
                {
                    if (dto.Customer.Phones.Any(x => x.IsPrimary))
                        dto.SelectedPhoneKey = dto.Customer.Phones.First(x => x.IsPrimary).Key;
                    else
                        dto.SelectedPhoneKey = dto.Customer.Phones.First().Key;
                }
            }

            dto.EmergencyContactName = pendingRegistration.EmergencyContactName;
            dto.EmergencyContactPhone = pendingRegistration.EmergencyContactPhone;

            if (pendingRegistration.Badges.Count > 0)
            {
                dto.Badge = AutoMapper.Mapper.Map(pendingRegistration.Badges.First(x => !x.IsGuest), new RegistrationBadgeDto());
            }
            else
            {
                dto.Badge = new RegistrationBadgeDto();
            }

            var statesResult = HttpClientHelper.GetJson<List<StateDto>>(referenceService, $"states/country/{dto.Customer.Addresses?.First(x => x.IsPrimary).Country}");

            if (statesResult.StatusCode == HttpStatusCode.OK)
            {
                dto.Badge.States = statesResult.Data;
            }
            else
            {
                throw new ServiceException(statesResult.ErrorMessage);
            }

            if (string.IsNullOrWhiteSpace(dto.Badge.State) && dto.Badge.States != null && dto.Badge.States.Any())
                dto.Badge.State = dto.Customer.Addresses?.First(x => x.IsPrimary).State;

            if (string.IsNullOrWhiteSpace(dto.Badge.City))
                dto.Badge.City = dto.Customer.Addresses?.First(x => x.IsPrimary).City;

            dto.CurrentTotal = GetCurrentTotal(dto.Customer.Key, dto.Event, pendingRegistration.PriceKey.Value, pendingRegistration.Sessions.ToList());

            dto.DisplayBadgeCompany = dto.Event.DisplayBadgeCompany;
            dto.DisplayBadgePosition = dto.Event.DisplayBadgePosition;
            dto.AlternativeCompanyBadgeLabel = dto.Event.AlternativeCompanyBadgeLabel;
            dto.AlternativePositionBadgeLabel = dto.Event.AlternativePositionBadgeLabel;
            dto.RelatedRegistrationEvents = new List<UserEventDto>();

            foreach (var relatedPendingRegistration in relatedPendingRegistrations)
            {
                var relatedEvent = AutoMapper.Mapper.Map(EventDao.GetByKey(relatedPendingRegistration.EventKey), new UserEventDto());
                relatedEvent.Fees = AutoMapper.Mapper.Map(FeeDao.GetEventFeesByCustomer(relatedEvent.Key, dto.Customer.Key, dto.Customer.IsMember, DateTime.Today), new List<EventFeeDto>());
                var relatedTotal = GetCurrentTotal(dto.Customer.Key, relatedEvent, relatedPendingRegistration.PriceKey.Value, relatedPendingRegistration.Sessions.ToList());

                if (!dto.DisplayBadgeCompany)
                {
                    dto.DisplayBadgeCompany = dto.Event.DisplayBadgeCompany || relatedEvent.DisplayBadgeCompany;
                }

                if (!dto.DisplayBadgePosition)
                {
                    dto.DisplayBadgePosition = dto.Event.DisplayBadgePosition || relatedEvent.DisplayBadgePosition;
                }

                if (dto.AlternativeCompanyBadgeLabel == "Company")
                {
                    dto.AlternativeCompanyBadgeLabel = relatedEvent.AlternativeCompanyBadgeLabel;
                }

                if (dto.AlternativePositionBadgeLabel == "Position")
                {
                    dto.AlternativePositionBadgeLabel = relatedEvent.AlternativePositionBadgeLabel;
                }

                dto.RelatedRegistrationEvents.Add(relatedEvent);
                dto.PendingSteps.AddRange(relatedEvent.Steps);
                dto.CurrentTotal += relatedTotal;
            }

            return dto;
        }

        public Guid SaveRegistrationContactInfo(UserRegistrationContactInfoDto dto)
        {
            var pendingRegistration = PendingRegistrationDao.GetByKey(dto.RegistrationKey);
            var relatedRegistrations = PendingRegistrationDao.GetByParentKey(dto.RegistrationKey);

            SaveContactInfo(pendingRegistration, dto);

            foreach (var relatedItem in relatedRegistrations)
            {
                SaveContactInfo(relatedItem, dto);
            }

            return pendingRegistration.Key;
        }

        public UserRegistrationStepDto GetRegistrationStep(Guid registrationKey, Guid stepKey)
        {
            var dto = new UserRegistrationStepDto();
            var parentRegistration = PendingRegistrationDao.GetByKey(registrationKey);
            var relatedPendingRegistrations = PendingRegistrationDao.GetByParentKey(parentRegistration.Key);

            var step = StepDao.GetByKey(stepKey);
            var parentEvent = EventDao.GetByKey(parentRegistration.EventKey);
            var stepEvent = EventDao.GetByKey(step.EventKey);

            var pendingRegistration = parentRegistration;
            foreach (var relatedPendingRegistration in relatedPendingRegistrations)
            {
                if (step.EventKey == relatedPendingRegistration.EventKey)
                {
                    pendingRegistration = relatedPendingRegistration;
                    break;
                }
            }

            dto = AutoMapper.Mapper.Map(step, new UserRegistrationStepDto());
            dto.RegistrationKey = parentRegistration.Key;
            dto.StepKey = step.Key;
            dto.Event = AutoMapper.Mapper.Map(parentEvent, new UserEventDto());
            dto.Customer = GetCustomerByKey(parentRegistration.CustomerKey);
            dto.Event.Fees = AutoMapper.Mapper.Map(FeeDao.GetEventFeesByCustomer(dto.Event.Key, dto.Customer.Key, dto.Customer.IsMember, DateTime.Today), new List<EventFeeDto>());
            var sessionFees = AutoMapper.Mapper.Map(FeeDao.GetSessionFeesForCustomer(dto.Customer.Key, stepEvent.Key, DateTime.Now), new List<SessionFeeDto>());

            foreach (var heading in dto.Headings)
            {
                heading.Sessions = AutoMapper.Mapper.Map(stepEvent.Sessions.Where(x => x.HeadingKey == heading.Key), new List<UserRegistrationSessionDto>());
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

                    // used to create max number of badge fields for registration
                    if (session.SessionTypeCode == "Guest Badge")
                    {
                        session.GuestBadges = new List<RegistrationGuestBadgeDto>();

                        for (var index = 0; index < session.MaxTicketQuantity; index++)
                        {
                            session.GuestBadges.Add(new RegistrationGuestBadgeDto());
                        }
                    }

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
                        }
                        else
                        {
                            session.Selected = true;
                            session.SelectedQuantity = pendingSession.SelectedQuantity;
                        }
                    }
                }
            }

            dto.CurrentTotal = GetCurrentTotal(dto.Customer.Key, dto.Event, parentRegistration.PriceKey.Value, parentRegistration.Sessions.ToList());

            dto.PendingSteps = new List<EventStepDto>();
            dto.PendingSteps.AddRange(dto.Event.Steps);

            dto.PendingEvents = new Dictionary<Guid, UserEventDto>();
            foreach (var eventStep in dto.Event.Steps)
            {
                dto.PendingEvents.Add(eventStep.Key, dto.Event);
            }

            foreach (var relatedPendingRegistration in relatedPendingRegistrations)
            {
                var relatedEvent = AutoMapper.Mapper.Map(EventDao.GetByKey(relatedPendingRegistration.EventKey), new UserEventDto());
                relatedEvent.Fees = AutoMapper.Mapper.Map(FeeDao.GetEventFeesByCustomer(relatedEvent.Key, dto.Customer.Key, dto.Customer.IsMember, DateTime.Today), new List<EventFeeDto>());
                var relatedTotal = GetCurrentTotal(dto.Customer.Key, relatedEvent, relatedPendingRegistration.PriceKey.Value, relatedPendingRegistration.Sessions.ToList());

                foreach (var eventStep in relatedEvent.Steps)
                {
                    dto.PendingEvents.Add(eventStep.Key, relatedEvent);
                }

                dto.PendingSteps.AddRange(relatedEvent.Steps);
                dto.CurrentTotal += relatedTotal;
            }

            dto.RegistrationStatus = "Pending";

            return dto;
        }

        public UserRegistrationStepDto GetEditRegistrationStep(Guid registrationKey, Guid stepKey)
        {
            var dto = new UserRegistrationStepDto();
            var editedRegistration = new EditedRegistration();
            var registration = RegistrantDao.GetRegistrantByKey(registrationKey);
            editedRegistration = EditedRegistrationDao.GetByRegistrationKey(registrationKey);
            dto.RegistrationKey = registrationKey;
            var step = StepDao.GetByKey(stepKey);
            var stepEvent = EventDao.GetByKey(step.EventKey);

            dto = AutoMapper.Mapper.Map(step, new UserRegistrationStepDto());

            dto.Event = AutoMapper.Mapper.Map(stepEvent, new UserEventDto());
            dto.Customer = GetCustomerByKey(registration.CustomerKey);
            dto.Event.Fees = AutoMapper.Mapper.Map(FeeDao.GetEventFeesByCustomer(dto.Event.Key, dto.Customer.Key, dto.Customer.IsMember, DateTime.Today), new List<EventFeeDto>());
            var sessionFees = AutoMapper.Mapper.Map(FeeDao.GetSessionFeesForCustomer(dto.Customer.Key, stepEvent.Key, DateTime.Now), new List<SessionFeeDto>());

            foreach (var heading in dto.Headings)
            {
                heading.Sessions = AutoMapper.Mapper.Map(stepEvent.Sessions.Where(x => x.HeadingKey == heading.Key), new List<UserRegistrationSessionDto>());
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

                    foreach (var regSession in registration.Sessions.Where(x => !x.CancelDate.HasValue))
                    {
                        if (session.Key == regSession.Session.Key)
                        {
                            session.SelectedQuantity = (int)regSession.InvoiceDetail.Quantity;
                            session.Fee = new SessionFeeDto { Price = regSession.InvoiceDetail.Price };
                            session.Selected = true;
                            session.RegistrationStatus = "Edit";
                        }

                        if (session.SessionTypeCode == "Guest Badge")
                        {
                            session.GuestBadges = new List<RegistrationGuestBadgeDto>();

                            if (editedRegistration.Badges.Any(x => x.IsGuest))
                            {
                                foreach (var guest in editedRegistration.Badges.Where(x => x.IsGuest))
                                {
                                    var guestBadge = new RegistrationGuestBadgeDto();
                                    guestBadge.Key = guest.Key;
                                    guestBadge.Name = guest.GuestName;
                                    guestBadge.Location = guest.GuestLocation;
                                    session.GuestBadges.Add(guestBadge);
                                }
                            }
                            else
                            {
                                foreach (var guest in registration.Guests)
                                {
                                    var guestBadge = new RegistrationGuestBadgeDto();
                                    guestBadge.Key = guest.Key;
                                    guestBadge.Name = guest.Name;
                                    guestBadge.Location = guest.Location;
                                    session.GuestBadges.Add(guestBadge);
                                }
                            }

                            var guestBadgeCount = session.GuestBadges.Count;

                            for (var index = guestBadgeCount; index < session.MaxTicketQuantity; index++)
                            {
                                session.GuestBadges.Add(new RegistrationGuestBadgeDto());
                            }

                            session.Selected = true;
                            session.SelectedQuantity = session.GuestBadges.Count;
                        }
                    }

                    foreach (var editedSession in editedRegistration.Sessions)
                    {
                        if (session.Key == editedSession.SessionKey)
                        {
                            session.SelectedQuantity = editedSession.SelectedQuantity;
                            session.Fee = new SessionFeeDto { Price = editedSession.FeeTotal };
                            session.Selected = true;
                        }
                    }
                }
            }

            if (editedRegistration != null)
            {
                dto.CurrentTotal = GetEditCurrentTotal(dto.Customer.Key, dto.Event, editedRegistration.Sessions.ToList());
            }
            else
            {
                dto.CurrentTotal = 0m;
            }

            dto.PendingSteps = new List<EventStepDto>();
            dto.PendingSteps.AddRange(dto.Event.Steps);

            dto.PendingEvents = new Dictionary<Guid, UserEventDto>();
            foreach (var eventStep in dto.Event.Steps)
            {
                dto.PendingEvents.Add(eventStep.Key, dto.Event);
            }

            dto.RegistrationStatus = "Edit";

            return dto;
        }

        public Guid SaveRegistrationSessions(UserRegistrationStepDto dto)
        {
            switch (dto.RegistrationStatus)
            {
                case "New":
                case "Pending":
                    return SavePendingRegistrationSessions(dto);
                case "Edit":
                    return SaveEditedRegistrationSessions(dto);

                default:
                    return new Guid();
            }
        }

        public Guid SavePendingRegistrationSessions(UserRegistrationStepDto dto)
        {
            var pendingRegistration = new PendingRegistration();
            var relatedPendingRegistrations = PendingRegistrationDao.GetByParentKey(dto.RegistrationKey);
            var step = StepDao.GetByKey(dto.StepKey);

            foreach (var relatedRegistration in relatedPendingRegistrations)
            {
                if (relatedRegistration.EventKey == step.EventKey)
                {
                    pendingRegistration = PendingRegistrationDao.GetByKey(relatedRegistration.Key);
                    break;
                }
            }

            if (pendingRegistration.Key == Guid.Empty)
            {
                pendingRegistration = PendingRegistrationDao.GetByKey(dto.RegistrationKey);
            }

            if (dto.Headings == null)
            {
                PendingRegistrationDao.Store(pendingRegistration);
                return pendingRegistration.Key;
            }

            foreach (var heading in dto.Headings)
            {
                if (heading.Sessions == null)
                {
                    continue;
                }

                foreach (var session in heading.Sessions)
                {
                    if (session.Selected || session.SelectedQuantity > 0)
                    {
                        if (pendingRegistration.Sessions.All(x => x.SessionKey != session.Key))
                        {
                            pendingRegistration.Sessions.Add(new PendingRegistrationSession
                            {
                                AddUser = dto.Customer.WebLogin,
                                AddDate = DateTime.Now,
                                SessionKey = session.Key,
                                SelectedQuantity = (session.SelectedQuantity > 0 ? session.SelectedQuantity : 1)
                            });
                        }
                        else
                        {
                            pendingRegistration.Sessions.First(x => x.SessionKey == session.Key).SelectedQuantity = (session.SelectedQuantity > 0 ? session.SelectedQuantity : 1);
                        }
                    }
                    else if (session.SessionTypeCode == "Guest Badge" && session.GuestBadges != null && session.GuestBadges.Any(x => !string.IsNullOrWhiteSpace(x.Name)))
                    {
                        if (pendingRegistration.Sessions.All(x => x.SessionKey != session.Key))
                        {
                            pendingRegistration.Sessions.Add(new PendingRegistrationSession
                            {
                                AddUser = dto.Customer.WebLogin,
                                AddDate = DateTime.Now,
                                SessionKey = session.Key,
                                SelectedQuantity = session.GuestBadges.Count(x => !string.IsNullOrWhiteSpace(x.Name))
                            });
                        }
                        else
                        {
                            pendingRegistration.Sessions.First(x => x.SessionKey == session.Key).SelectedQuantity = session.GuestBadges.Count(x => !string.IsNullOrWhiteSpace(x.Name));

                            var guestBadges = pendingRegistration.Badges.Where(x => x.IsGuest).ToList();

                            foreach (var guestBadge in guestBadges)
                            {
                                pendingRegistration.Badges.Remove(guestBadge);
                            }
                        }

                        foreach (var badge in session.GuestBadges.Where(x => !string.IsNullOrWhiteSpace(x.Name)))
                        {
                            var guestBadge = new PendingRegistrationBadge();
                            guestBadge.AddUser = dto.Customer.WebLogin;
                            guestBadge.AddDate = DateTime.Now;
                            guestBadge.GuestName = badge.Name;
                            guestBadge.GuestLocation = badge.Location;
                            guestBadge.IsGuest = true;
                            pendingRegistration.Badges.Add(guestBadge);
                        }
                    }
                    else if (pendingRegistration.Sessions.Any(x => x.SessionKey == session.Key))
                    {
                        pendingRegistration.Sessions.Remove(pendingRegistration.Sessions.First(x => x.SessionKey == session.Key));
                    }
                }
            }

            PendingRegistrationDao.Store(pendingRegistration);

            return dto.RegistrationKey;
        }

        public Guid SaveEditedRegistrationSessions(UserRegistrationStepDto dto)
        {
            var editedRegistration = new EditedRegistration();
            var registrant = RegistrantDao.GetRegistrantByKey(dto.RegistrationKey);

            if (editedRegistration.Key == Guid.Empty)
            {
                editedRegistration = EditedRegistrationDao.GetByRegistrationKey(dto.RegistrationKey);
            }

            if (editedRegistration == null)
            {
                editedRegistration = new EditedRegistration();
                editedRegistration.AddUser = dto.Customer.WebLogin;
                editedRegistration.AddDate = DateTime.Now;
                editedRegistration.CustomerAddressKey = registrant.AddressKey;
                editedRegistration.CustomerPhoneKey = registrant.PhoneKey;
                editedRegistration.EmergencyContactName = registrant.EmergencyContactName;
                editedRegistration.EmergencyContactPhone = registrant.EmergencyContactPhone;
                editedRegistration.RegistrationDate = registrant.RegistrationDate;
                editedRegistration.PriceKey = registrant.InvoiceDetail.PriceKey;
                editedRegistration.RegistrantKey = registrant.Key;
                editedRegistration.EventKey = registrant.EventKey;
                editedRegistration.CustomerKey = registrant.CustomerKey;
                editedRegistration.Sessions = new List<EditedRegistrationSession>();
            }

            foreach (var heading in dto.Headings)
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
                            AddUser = dto.Customer.WebLogin,
                            AddDate = DateTime.Now,
                            SessionKey = session.Key,
                            SelectedQuantity = session.SelectedQuantity > 0 ? session.SelectedQuantity : 1
                        });
                    }
                    else if (session.SessionTypeCode == "Guest Badge" && session.GuestBadges != null &&
                             session.GuestBadges.Any(x => !string.IsNullOrWhiteSpace(x.Name)))
                    {
                        if (editedRegistration.Sessions.All(x => x.SessionKey != session.Key))
                        {
                            editedRegistration.Sessions.Add(new EditedRegistrationSession
                            {
                                AddUser = dto.Customer.WebLogin,
                                AddDate = DateTime.Now,
                                SessionKey = session.Key,
                                SelectedQuantity =
                                    session.GuestBadges.Count(x => !string.IsNullOrWhiteSpace(x.Name))
                            });
                        }
                        else
                        {
                            editedRegistration.Sessions.First(x => x.SessionKey == session.Key).SelectedQuantity
                                = session.GuestBadges.Count(x => !string.IsNullOrWhiteSpace(x.Name));

                            var guestBadges = editedRegistration.Badges.Where(x => x.IsGuest).ToList();

                            foreach (var guestBadge in guestBadges)
                            {
                                editedRegistration.Badges.Remove(guestBadge);
                            }
                        }

                        foreach (var badge in session.GuestBadges.Where(x => !string.IsNullOrWhiteSpace(x.Name))
                            )
                        {
                            var guestBadge = new EditedRegistrationBadge();
                            guestBadge.AddUser = dto.Customer.WebLogin;
                            guestBadge.AddDate = DateTime.Now;
                            guestBadge.GuestName = badge.Name;
                            guestBadge.GuestLocation = badge.Location;
                            guestBadge.IsGuest = true;
                            editedRegistration.Badges.Add(guestBadge);
                        }
                    }
                    else if (editedRegistration.Sessions.Any(x => x.SessionKey == session.Key))
                    {
                        editedRegistration.Sessions.Remove(editedRegistration.Sessions.First(x => x.SessionKey == session.Key));
                    }
                }
            }

            EditedRegistrationDao.Store(editedRegistration);

            return dto.RegistrationKey;
        }

        public UserRegistrationConflictDto GetRegistrationSessionConflicts(Guid registrationKey, int conflictType)
        {
            var dto = new UserRegistrationConflictDto();
            dto.RegistrationKey = registrationKey;
            dto.ConflictGroups = new List<UserRegistrationSessionConflictGroupDto>();
            var pendingRegistration = PendingRegistrationDao.GetByKey(registrationKey);
            var relatedPendingRegistrations = PendingRegistrationDao.GetByParentKey(registrationKey);
            var evt = EventDao.GetByKey(pendingRegistration.EventKey);
            dto.Event = AutoMapper.Mapper.Map(evt, new UserEventDto());
            var customer = GetCustomerByKey(pendingRegistration.CustomerKey);
            dto.Event.Fees = AutoMapper.Mapper.Map(FeeDao.GetEventFeesByCustomer(dto.Event.Key, customer.Key, customer.IsMember, DateTime.Today), new List<EventFeeDto>());
            var sessionFees = AutoMapper.Mapper.Map(FeeDao.GetSessionFeesForCustomer(customer.Key, evt.Key, DateTime.Now), new List<SessionFeeDto>());
            var conflictedSessions = new List<UserRegistrationSessionDto>();
            var allSessions = new List<PendingRegistrationSession>();
            allSessions.AddRange(pendingRegistration.Sessions);

            foreach (var relatedPendingRegistration in relatedPendingRegistrations)
            {
                foreach (var session in relatedPendingRegistration.Sessions)
                {
                    allSessions.Add(session);
                }

                var relatedSessionFees = AutoMapper.Mapper.Map(FeeDao.GetSessionFeesForCustomer(customer.Key, relatedPendingRegistration.EventKey, DateTime.Now), new List<SessionFeeDto>());
                sessionFees.AddRange(relatedSessionFees);
            }

            foreach (var userSession in allSessions)
            {
                var eventSession = SessionDao.GetByKey(userSession.SessionKey);

                var conflictSession = AutoMapper.Mapper.Map(eventSession, new UserRegistrationSessionDto());
                conflictSession.Fee = sessionFees.FirstOrDefault(x => x.SessionKey == conflictSession.Key);
                conflictSession.Selected = true;
                conflictSession.SelectedQuantity = userSession.SelectedQuantity;
                conflictSession.EventCode = eventSession.Event.Code;

                if (conflictSession.Fee == null)
                {
                    conflictSession.Fee = new SessionFeeDto { Price = 0.00m };
                }

                foreach (var conflict in eventSession.Conflicts)
                {
                    if (conflict.Type != conflictType)
                    {
                        continue;
                    }

                    if (allSessions.All(x => x.SessionKey != conflict.ConflictSession.Key))
                    {
                        continue;
                    }

                    if (conflictedSessions.All(x => x.Key != conflictSession.Key))
                    {
                        conflictedSessions.Add(conflictSession);
                    }

                    if (conflictedSessions.Any(x => x.Key == conflict.ConflictSession.Key))
                    {
                        continue;
                    }

                    var newConflictSession = AutoMapper.Mapper.Map(SessionDao.GetByKey(conflict.ConflictSession.Key), new UserRegistrationSessionDto());
                    newConflictSession.Fee = sessionFees.FirstOrDefault(x => x.SessionKey == newConflictSession.Key);
                    newConflictSession.Selected = true;
                    newConflictSession.SelectedQuantity = allSessions.First(x => x.SessionKey == conflict.ConflictSession.Key).SelectedQuantity;

                    if (newConflictSession.Fee == null)
                    {
                        newConflictSession.Fee = new SessionFeeDto { Price = 0.00m };
                    }

                    conflictedSessions.Add(newConflictSession);
                }
            }

            var dates = (from c in conflictedSessions.OrderBy(x => x.Date) select c.Date).Distinct();

            foreach (var date in dates)
            {
                var conflictGroup = new UserRegistrationSessionConflictGroupDto();
                conflictGroup.ConflictedSessions = new List<UserRegistrationSessionDto>();

                foreach (var conflict in conflictedSessions.Where(conflict => date.HasValue && conflict.Date.HasValue && date.Value.Date == conflict.Date.Value.Date))
                {
                    conflictGroup.ConflictedSessions.Add(conflict);
                }

                dto.ConflictGroups.Add(conflictGroup);
            }

            dto.CurrentTotal = GetCurrentTotal(pendingRegistration.CustomerKey, dto.Event, pendingRegistration.PriceKey.Value, pendingRegistration.Sessions.ToList());

            dto.PendingSteps = new List<EventStepDto>();
            dto.PendingSteps.AddRange(dto.Event.Steps);

            dto.PendingEvents = new Dictionary<Guid, UserEventDto>();
            foreach (var eventStep in dto.Event.Steps)
            {
                dto.PendingEvents.Add(eventStep.Key, dto.Event);
            }

            foreach (var relatedPendingRegistration in relatedPendingRegistrations)
            {
                var relatedEvent = AutoMapper.Mapper.Map(EventDao.GetByKey(relatedPendingRegistration.EventKey), new UserEventDto());
                relatedEvent.Fees = AutoMapper.Mapper.Map(FeeDao.GetEventFeesByCustomer(relatedEvent.Key, customer.Key, customer.IsMember, DateTime.Today), new List<EventFeeDto>());
                var relatedTotal = GetCurrentTotal(customer.Key, relatedEvent, relatedPendingRegistration.PriceKey.Value, relatedPendingRegistration.Sessions.ToList());

                foreach (var eventStep in relatedEvent.Steps)
                {
                    dto.PendingEvents.Add(eventStep.Key, relatedEvent);
                }

                dto.PendingSteps.AddRange(relatedEvent.Steps);
                dto.CurrentTotal += relatedTotal;
            }

            return dto;
        }

        public UserRegistrationConflictDto GetEditRegistrationSessionConflicts(Guid registrationKey, int conflictType)
        {
            var dto = new UserRegistrationConflictDto();
            dto.RegistrationKey = registrationKey;
            dto.ConflictGroups = new List<UserRegistrationSessionConflictGroupDto>();
            var editedRegistration = EditedRegistrationDao.GetByRegistrationKey(registrationKey);
            var registrant = RegistrantDao.GetRegistrant(editedRegistration.EventKey, editedRegistration.CustomerKey);
            var evt = EventDao.GetByKey(editedRegistration.EventKey);
            dto.Event = AutoMapper.Mapper.Map(evt, new UserEventDto());
            var customer = GetCustomerByKey(editedRegistration.CustomerKey);
            dto.Event.Fees = AutoMapper.Mapper.Map(FeeDao.GetEventFeesByCustomer(dto.Event.Key, customer.Key, customer.IsMember, DateTime.Today), new List<EventFeeDto>());
            var sessionFees = AutoMapper.Mapper.Map(FeeDao.GetSessionFeesForCustomer(customer.Key, evt.Key, DateTime.Now), new List<SessionFeeDto>());
            var conflictedSessions = new List<UserRegistrationSessionDto>();
            var allSessions = new List<EditedRegistrationSession>();
            allSessions.AddRange(editedRegistration.Sessions);

            if (registrant.Sessions != null && registrant.Sessions.Count > 0)
            {
                foreach (var session in registrant.Sessions.Where(x => !x.CancelDate.HasValue))
                {
                    var regSession = new EditedRegistrationSession();
                    regSession.SessionKey = session.Session.Key;
                    regSession.Key = session.Key;
                    regSession.AddDate = session.AddDate;
                    regSession.AddUser = session.AddUser;
                    regSession.ChangeDate = session.ChangeDate;
                    regSession.ChangeUser = session.ChangeUser;
                    regSession.Code = session.Session.Code;
                    regSession.Date = session.Session.Date;
                    regSession.DeleteFlag = session.Session.DeleteFlag;
                    regSession.EndTime = session.Session.EndTime;
                    regSession.StartTime = session.Session.StartTime;
                    regSession.Title = session.Session.Title;
                    regSession.Time = session.Session.Time;
                    regSession.SelectedQuantity = Convert.ToInt32(session.InvoiceDetail.Quantity);
                    regSession.Fee = new Fee()
                    {
                        Price = session.InvoiceDetail.Price
                    };
                    regSession.IsRegistered = true;
                    allSessions.Add(regSession);
                }
            }

            foreach (var userSession in allSessions)
            {
                var eventSession = SessionDao.GetByKey(userSession.SessionKey);

                if (eventSession.Conflicts != null)
                {

                    var conflictSession = AutoMapper.Mapper.Map(eventSession, new UserRegistrationSessionDto());
                    conflictSession.Fee = sessionFees.FirstOrDefault(x => x.SessionKey == conflictSession.Key);
                    conflictSession.Selected = true;
                    conflictSession.SelectedQuantity = userSession.SelectedQuantity;
                    conflictSession.EventCode = eventSession.Event.Code;
                    conflictSession.IsRegistered = userSession.IsRegistered;

                    if (conflictSession.Fee == null)
                    {
                        conflictSession.Fee = new SessionFeeDto { Price = 0.00m };
                    }

                    foreach (var conflict in eventSession.Conflicts)
                    {
                        if (conflict.Type != conflictType)
                        {
                            continue;
                        }

                        if (allSessions.All(x => x.SessionKey != conflict.ConflictSession.Key))
                        {
                            continue;
                        }

                        if (conflictedSessions.All(x => x.Key != conflictSession.Key))
                        {
                            conflictedSessions.Add(conflictSession);
                        }

                        if (conflictedSessions.Any(x => x.Key == conflict.ConflictSession.Key))
                        {
                            continue;
                        }

                        var newConflictSession = AutoMapper.Mapper.Map(
                            SessionDao.GetByKey(conflict.ConflictSession.Key), new UserRegistrationSessionDto());
                        newConflictSession.Fee = sessionFees.FirstOrDefault(x => x.SessionKey == newConflictSession.Key);
                        newConflictSession.Selected = true;
                        newConflictSession.SelectedQuantity =
                            allSessions.First(x => x.SessionKey == conflict.ConflictSession.Key).SelectedQuantity;
                        newConflictSession.IsRegistered = allSessions.First(x => x.SessionKey == conflict.ConflictSession.Key).IsRegistered;

                        if (newConflictSession.Fee == null)
                        {
                            newConflictSession.Fee = new SessionFeeDto { Price = 0.00m };
                        }

                        conflictedSessions.Add(newConflictSession);
                    }
                }
            }

            var dates = (from c in conflictedSessions.OrderBy(x => x.Date) select c.Date).Distinct();

            foreach (var date in dates)
            {
                var conflictGroup = new UserRegistrationSessionConflictGroupDto();
                conflictGroup.ConflictedSessions = new List<UserRegistrationSessionDto>();

                foreach (var conflict in conflictedSessions.Where(conflict => date.HasValue && conflict.Date.HasValue && date.Value.Date == conflict.Date.Value.Date))
                {
                    conflictGroup.ConflictedSessions.Add(conflict);
                }

                dto.ConflictGroups.Add(conflictGroup);
            }

            dto.CurrentTotal = GetEditCurrentTotal(editedRegistration.CustomerKey, dto.Event, editedRegistration.Sessions.ToList());

            dto.PendingSteps = new List<EventStepDto>();
            dto.PendingSteps.AddRange(dto.Event.Steps);

            dto.PendingEvents = new Dictionary<Guid, UserEventDto>();
            foreach (var eventStep in dto.Event.Steps)
            {
                dto.PendingEvents.Add(eventStep.Key, dto.Event);
            }

            return dto;
        }

        public Guid ResolveSessionConflicts(UserRegistrationConflictDto dto)
        {
            switch (dto.RegistrationStatus)
            {
                case "New":
                case "Pending":
                    return ResolveRegistrationSessionConflicts(dto);
                case "Edit":
                    return ResolveEditRegistrationSessionConflicts(dto);

                default:
                    return new Guid();
            }
        }

        public Guid ResolveRegistrationSessionConflicts(UserRegistrationConflictDto dto)
        {
            var pendingRegistration = PendingRegistrationDao.GetByKey(dto.RegistrationKey);
            var relatedPendingRegistrations = PendingRegistrationDao.GetByParentKey(dto.RegistrationKey);

            foreach (var conflictGroup in dto.ConflictGroups)
            {
                foreach (var session in conflictGroup.ConflictedSessions)
                {
                    var pendingSession = pendingRegistration.Sessions.FirstOrDefault(x => x.SessionKey == session.Key);

                    if (pendingSession != null)
                    {
                        if (!session.Selected || session.SelectedQuantity == 0)
                        {
                            pendingRegistration.Sessions.Remove(pendingSession);
                        }
                        else
                        {
                            pendingSession.SelectedQuantity = session.SelectedQuantity;
                        }
                        continue;
                    }

                    foreach (var relatedPendingRegistration in relatedPendingRegistrations)
                    {
                        pendingSession = new PendingRegistrationSession();
                        pendingSession = relatedPendingRegistration.Sessions.FirstOrDefault(x => x.SessionKey == session.Key);

                        if (pendingSession == null)
                        {
                            continue;
                        }

                        if (!session.Selected || session.SelectedQuantity == 0)
                        {
                            relatedPendingRegistration.Sessions.Remove(pendingSession);
                        }
                        else
                        {
                            pendingSession.SelectedQuantity = session.SelectedQuantity;
                        }
                        break;
                    }
                }
            }

            PendingRegistrationDao.Store(pendingRegistration);
            foreach (var relatedPendingRegistration in relatedPendingRegistrations)
            {
                PendingRegistrationDao.Store(relatedPendingRegistration);
            }


            dto.RegistrationStatus = "Pending";
            return pendingRegistration.Key;
        }

        public Guid ResolveEditRegistrationSessionConflicts(UserRegistrationConflictDto dto)
        {
            var editedRegistration = EditedRegistrationDao.GetByRegistrationKey(dto.RegistrationKey);

            foreach (var conflictGroup in dto.ConflictGroups)
            {
                foreach (var session in conflictGroup.ConflictedSessions)
                {
                    var editedSession = editedRegistration.Sessions.FirstOrDefault(x => x.SessionKey == session.Key);

                    if (editedSession != null)
                    {
                        if (!session.Selected || session.SelectedQuantity == 0)
                        {
                            editedRegistration.Sessions.Remove(editedSession);
                        }
                        else
                        {
                            editedSession.SelectedQuantity = session.SelectedQuantity;
                        }
                        continue;
                    }
                }
            }

            EditedRegistrationDao.Store(editedRegistration);

            dto.RegistrationStatus = "Edit";
            return dto.RegistrationKey;
        }

        public UserRegistrationConfirmationDto GetRegistrationConfirmation(Guid registrationKey, string status)
        {
            var registrant = RegistrantDao.GetRegistrantByKey(registrationKey);
            var relatedRegistrants = RegistrantDao.GetRelatedRegistrations(registrant.EventKey, registrant.CustomerKey);

            if (status == "Updated")
            {
                AdminRegistrationTasks.MarkEditedRegistrationAsProcessed(registrant.EventKey, registrant.CustomerKey);
            }
            else
            {
                AdminRegistrationTasks.MarkPendingRegistrationAsProcessed(registrant.EventKey, registrant.CustomerKey);
            }

            var dto = new UserRegistrationConfirmationDto();
            dto.Key = registrationKey;
            dto.EmergencyContactName = registrant.EmergencyContactName;
            dto.EmergencyContactPhone = registrant.EmergencyContactPhone;
            dto.SelectedAddressKey = registrant.AddressKey;
            dto.SelectedPhoneKey = registrant.PhoneKey;
            dto.Comment = registrant.Comment;
            dto.InvoiceCode = registrant.InvoiceCode;
            dto.InvoiceKey = registrant.InvoiceDetail.InvoiceKey.Value;
            dto.Badge = new RegistrationBadgeDto();
            dto.Badge.NickName = registrant.BadgeName;
            dto.Badge.City = registrant.City;
            dto.Badge.State = registrant.State;
            dto.Badge.Company = registrant.Organization;
            dto.Badge.Position = registrant.Title;
            dto.Fee = new EventFeeDto { Price = registrant.InvoiceDetail.Price, ProductName = registrant.InvoiceDetail.ProductName };
            dto.Sessions = new List<UserRegistrationSessionDto>();
            dto.Customer = GetCustomerByKey(registrant.CustomerKey);
            var evt = EventDao.GetByKey(registrant.EventKey);
            dto.Event = AutoMapper.Mapper.Map(evt, new UserEventDto());
            var invoiceDetails = RegistrationInvoiceDetailDao.GetInvoiceDetailsByInvoiceCode(registrant.InvoiceCode);
            var discount = invoiceDetails.FirstOrDefault(x => x.InvoiceType == "Discount");
            dto.RelatedRegistrations = new List<UserRegistrationConfirmationDto>();

            foreach (var relatedRegistrant in relatedRegistrants)
            {
                var relatedRegistration = new UserRegistrationConfirmationDto();
                var registration = RegistrantDao.GetRegistrantByKey(relatedRegistrant.Key);
                relatedRegistration.Key = registration.Key;
                relatedRegistration.Event = AutoMapper.Mapper.Map(EventDao.GetByKey(registration.EventKey), new UserEventDto());
                dto.RelatedRegistrations.Add(relatedRegistration);
            }

            if (discount != null)
            {
                dto.DiscountName = discount.ProductName;
                dto.DiscountAmount = discount.Price;
            }

            foreach (var session in registrant.Sessions)
            {
                if (!session.CancelDate.HasValue)
                {
                    var userSession = AutoMapper.Mapper.Map(session.Session, new UserRegistrationSessionDto());
                    userSession.SelectedQuantity = (int)session.InvoiceDetail.Quantity;
                    userSession.Fee = new SessionFeeDto { Price = session.InvoiceDetail.Price };
                    dto.Sessions.Add(userSession);
                }
            }

            return dto;
        }

        public bool SendConfirmationEmail(Guid registrationKey)
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

        public bool SaveUserComments(Guid registrationKey, string comments)
        {
            var success = false;

            try
            {
                var registration = RegistrantDao.GetRegistrantByKey(registrationKey);
                registration.Comment = comments;
                RegistrantDao.Store(registration);
                success = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }

            return success;
        }

        public EditUserRegistrationContactInfoDto GetRegistrationContactInfoForEdit(Guid registrationKey)
        {
            var dto = new EditUserRegistrationContactInfoDto();
            var registration = RegistrantDao.GetRegistrantByKey(registrationKey);
            var evt = EventDao.GetEventBaseByKey(registration.EventKey);
            dto.RegistrationKey = registration.Key;
            dto.Customer = GetCustomerByKey(registration.CustomerKey);
            dto.Event = AutoMapper.Mapper.Map(evt, new EventBaseDto());

            if (dto.Customer.Addresses.Any())
            {
                if (registration.AddressKey.HasValue)
                    dto.SelectedAddressKey = registration.AddressKey;
                else
                {
                    if (dto.Customer.Addresses.Any(x => x.IsPrimary))
                        dto.SelectedAddressKey = dto.Customer.Addresses.First(x => x.IsPrimary).Key;
                    else
                        dto.SelectedAddressKey = dto.Customer.Addresses.First().Key;
                }
            }

            if (dto.Customer.Phones.Any())
            {
                if (registration.PhoneKey.HasValue)
                    dto.SelectedPhoneKey = registration.PhoneKey;
                else
                {
                    if (dto.Customer.Phones.Any(x => x.IsPrimary))
                        dto.SelectedPhoneKey = dto.Customer.Phones.First(x => x.IsPrimary).Key;
                    else
                        dto.SelectedPhoneKey = dto.Customer.Phones.First().Key;
                }
            }

            dto.EmergencyContactName = registration.EmergencyContactName;
            dto.EmergencyContactPhone = registration.EmergencyContactPhone;
            dto.Badge = new RegistrationBadgeDto
            {
                City = registration.City,
                Company = registration.Organization,
                Country = registration.Country,
                FirstName = dto.Customer.FirstName,
                LastName = dto.Customer.LastName,
                NickName = registration.BadgeName,
                Position = registration.Title,
                State = registration.State
            };
            dto.DisplayBadgeCompany = dto.Event.DisplayBadgeCompany;
            dto.DisplayBadgePosition = dto.Event.DisplayBadgePosition;
            dto.AlternativeCompanyBadgeLabel = dto.Event.AlternativeCompanyBadgeLabel;
            dto.AlternativePositionBadgeLabel = dto.Event.AlternativePositionBadgeLabel;

            var statesResult = HttpClientHelper.GetJson<List<StateDto>>(referenceService, $"states/country/{dto.Customer.Addresses?.First(x => x.IsPrimary).Country}");

            if (statesResult.StatusCode == HttpStatusCode.OK)
            {
                dto.Badge.States = statesResult.Data;
            }
            else
            {
                throw new ServiceException(statesResult.ErrorMessage);
            }

            var relatedEvents = EventDao.GetRelatedEvents(evt.Key);

            foreach (var relatedEvent in relatedEvents)
            {
                if (!dto.DisplayBadgeCompany)
                {
                    dto.DisplayBadgeCompany = dto.Event.DisplayBadgeCompany || relatedEvent.DisplayBadgeCompany;
                }

                if (!dto.DisplayBadgePosition)
                {
                    dto.DisplayBadgePosition = dto.Event.DisplayBadgePosition || relatedEvent.DisplayBadgePosition;
                }

                if (dto.AlternativeCompanyBadgeLabel == "Company")
                {
                    dto.AlternativeCompanyBadgeLabel = relatedEvent.AlternativeCompanyBadgeLabel;
                }

                if (dto.AlternativePositionBadgeLabel == "Position")
                {
                    dto.AlternativePositionBadgeLabel = relatedEvent.AlternativePositionBadgeLabel;
                }
            }

            return dto;
        }

        public bool UpdateRegistrationContactInfo(UserRegistrationContactInfoDto dto)
        {
            var success = false;

            try
            {
                var registrations = new List<Registrant>();
                var registration = RegistrantDao.GetRegistrantByKey(dto.RegistrationKey);
                registrations.Add(registration);
                registrations.AddRange(RegistrantDao.GetRelatedRegistrations(registration.EventKey, registration.CustomerKey));

                foreach (var item in registrations)
                {
                    item.AddressKey = dto.SelectedAddressKey;
                    item.PhoneKey = dto.SelectedPhoneKey;
                    item.EmergencyContactName = dto.EmergencyContactName;
                    item.EmergencyContactPhone = dto.EmergencyContactPhone;
                    item.BadgeName = dto.Badge.NickName;
                    item.Title = dto.Badge.Position;
                    item.Organization = dto.Badge.Company;
                    item.City = dto.Badge.City;
                    item.State = dto.Badge.State;
                    item.Country = dto.Badge.Country;
                    item.ChangeDate = DateTime.Now;
                    item.ChangeUser = dto.Customer.WebLogin;

                    RegistrantDao.Store(item);
                }

                success = true;
            }
            catch (Exception ex)
            {
                Log.Error($"Unable to update contact information for registration {dto.RegistrationKey}. Error: {ex.Message}");
            }

            return success;
        }

        public EditUserRegistrationSessionsDto GetRegistrationSessionsForEdit(Guid registrationKey)
        {
            var dto = new EditUserRegistrationSessionsDto();
            var registration = RegistrantDao.GetRegistrantByKey(registrationKey);
            var evt = EventDao.GetByKey(registration.EventKey);

            var editRegistration = EditedRegistrationDao.GetByCustomerEvent(evt.Key, registration.CustomerKey);

            if (editRegistration != null)
            {
                if (!editRegistration.IsProcessed)
                {
                    dto = GetEditRegistration(editRegistration.Key);
                }

                return dto;
            }


            dto.Key = registration.Key;
            dto.Event = AutoMapper.Mapper.Map(evt, new UserRegistrationEventDto());
            dto.StepKey = evt.Steps[0].Key;
            dto.StepDescription = evt.Steps[0].StepDescription;
            dto.StepHeading = evt.Steps[0].StepHeading;
            dto.StepSequence = evt.Steps[0].StepSequence;
            var sessionFees = AutoMapper.Mapper.Map(FeeDao.GetSessionFeesForCustomer(registration.CustomerKey, registration.EventKey, registration.RegistrationDate), new List<SessionFeeDto>());

            var customer = new CustomerDto();
            var result = HttpClientHelper.GetJson<CustomerDto>(customerService, $"individual/{registration.CustomerKey}/event");

            if (result.StatusCode == HttpStatusCode.OK)
            {
                customer = result.Data;
            }
            else
            {
                throw new ServiceException(result.ErrorMessage);
            }

            foreach (var step in dto.Event.Steps)
            {
                step.Headings = step.Headings.OrderBy(x => x.HeadingSequence).ToList();

                foreach (var heading in step.Headings)
                {
                    heading.Sessions = AutoMapper.Mapper.Map(evt.Sessions.Where(x => x.HeadingKey == heading.Key),
                        new List<UserRegistrationSessionDto>());
                    heading.Sessions = heading.Sessions.OrderBy(x => x.Code).ToList();
                    heading.RegistrationStatus = "Edit";

                    foreach (var session in heading.Sessions)
                    {
                        session.Fee = sessionFees.FirstOrDefault(x => x.SessionKey == session.Key);
                        session.RegistrationStatus = "Edit";

                        if (session.Fee == null)
                        {
                            session.Fee = new SessionFeeDto
                            {
                                Price = 0.00m
                            };
                        }

                        foreach (var regSession in registration.Sessions.Where(x => !x.CancelDate.HasValue))
                        {
                            if (session.Key == regSession.Session.Key)
                            {
                                session.SelectedQuantity = (int)regSession.InvoiceDetail.Quantity;
                                session.Fee = new SessionFeeDto { Price = regSession.InvoiceDetail.Price };
                                session.Selected = true;
                                session.RegistrationStatus = "Edit";
                            }
                        }
                    }
                }
            }

            dto.Headings = dto.Event.Steps[0].Headings;
            dto.RegistrationStatus = "Edit";
            dto.WebLogin = customer.WebLogin;

            return dto;
        }

        public EditUserRegistrationSessionsDto GetEditRegistration(Guid editRegistrationKey)
        {
            var dto = new EditUserRegistrationSessionsDto();

            var editRegistration = EditedRegistrationDao.GetByKey(editRegistrationKey);
            var registration = RegistrantDao.GetRegistrantByKey(editRegistration.RegistrantKey.Value);
            var evt = EventDao.GetByKey(editRegistration.EventKey);

            dto.Key = editRegistrationKey;
            dto.Event = AutoMapper.Mapper.Map(evt, new UserRegistrationEventDto());
            dto.StepKey = evt.Steps[0].Key;
            dto.StepDescription = evt.Steps[0].StepDescription;
            dto.StepHeading = evt.Steps[0].StepHeading;
            dto.StepSequence = evt.Steps[0].StepSequence;
            var sessionFees = AutoMapper.Mapper.Map(FeeDao.GetSessionFeesForCustomer(editRegistration.CustomerKey, editRegistration.EventKey, editRegistration.RegistrationDate), new List<SessionFeeDto>());

            var customer = new CustomerDto();
            var result = HttpClientHelper.GetJson<CustomerDto>(customerService, $"individual/{registration.CustomerKey}/event");

            if (result.StatusCode == HttpStatusCode.OK)
            {
                customer = result.Data;
            }
            else
            {
                throw new ServiceException(result.ErrorMessage);
            }

            foreach (var step in dto.Event.Steps)
            {
                step.Headings = step.Headings.OrderBy(x => x.HeadingSequence).ToList();

                foreach (var heading in step.Headings)
                {
                    heading.Sessions = AutoMapper.Mapper.Map(evt.Sessions.Where(x => x.HeadingKey == heading.Key),
                        new List<UserRegistrationSessionDto>());
                    heading.Sessions = heading.Sessions.OrderBy(x => x.Code).ToList();
                    heading.RegistrationStatus = "Edit";

                    foreach (var session in heading.Sessions)
                    {
                        session.Fee = sessionFees.FirstOrDefault(x => x.SessionKey == session.Key);
                        session.RegistrationStatus = "Edit";

                        if (session.Fee == null)
                        {
                            session.Fee = new SessionFeeDto
                            {
                                Price = 0.00m
                            };
                        }

                        foreach (var editedSession in editRegistration.Sessions)
                        {
                            if (session.Key == editedSession.SessionKey)
                            {
                                session.SelectedQuantity = editedSession.SelectedQuantity;
                                session.Selected = true;
                                session.RegistrationStatus = "Edit";
                            }
                        }

                        foreach (var regSession in registration.Sessions.Where(x => !x.CancelDate.HasValue))
                        {
                            if (session.Key == regSession.Session.Key)
                            {
                                session.SelectedQuantity = (int)regSession.InvoiceDetail.Quantity;
                                session.Fee = new SessionFeeDto { Price = regSession.InvoiceDetail.Price };
                                session.Selected = true;
                                session.RegistrationStatus = "Edit";
                            }
                        }

                        if (session.SessionTypeCode == "Guest Badge")
                        {
                            if (session.SessionTypeCode == "Guest Badge")
                            {
                                session.GuestBadges = new List<RegistrationGuestBadgeDto>();

                                if (editRegistration.Badges.Any(x => x.IsGuest))
                                {
                                    foreach (var guest in editRegistration.Badges.Where(x => x.IsGuest))
                                    {
                                        var guestBadge = new RegistrationGuestBadgeDto();
                                        guestBadge.Key = guest.Key;
                                        guestBadge.Name = guest.GuestName;
                                        guestBadge.Location = guest.GuestLocation;
                                        session.GuestBadges.Add(guestBadge);
                                    }
                                }
                                else
                                {
                                    foreach (var guest in registration.Guests)
                                    {
                                        var guestBadge = new RegistrationGuestBadgeDto();
                                        guestBadge.Key = guest.Key;
                                        guestBadge.Name = guest.Name;
                                        guestBadge.Location = guest.Location;
                                        session.GuestBadges.Add(guestBadge);
                                    }
                                }

                                var guestBadgeCount = session.GuestBadges.Count;

                                for (var index = guestBadgeCount; index < session.MaxTicketQuantity; index++)
                                {
                                    session.GuestBadges.Add(new RegistrationGuestBadgeDto());
                                }

                                session.Selected = true;
                                session.SelectedQuantity = session.GuestBadges.Count;
                            }
                        }
                    }
                }
            }

            dto.Headings = dto.Event.Steps[0].Headings;
            dto.RegistrationStatus = "Edit";
            dto.WebLogin = customer.WebLogin;

            return dto;
        }

        public UserEditedRegistrationDto GetEditedSessions(Guid editedRegistrationKey)
        {
            var dto = new UserEditedRegistrationDto();
            dto.EditedSessions = new List<UserRegistrationSessionDto>();
            dto.EditedGuestBadges = new List<EditedRegistrationGuestBadgeDto>();
            var editedRegistrant = EditedRegistrationDao.GetByRegistrationKey(editedRegistrationKey);
            var registrant = RegistrantDao.GetRegistrant(editedRegistrant.EventKey, editedRegistrant.CustomerKey);
            var sessionFees = AutoMapper.Mapper.Map(FeeDao.GetSessionFeesForCustomer(editedRegistrant.CustomerKey, editedRegistrant.EventKey, editedRegistrant.RegistrationDate), new List<SessionFeeDto>());
            var evt = EventDao.GetByKey(editedRegistrant.EventKey);
            dto.Event = AutoMapper.Mapper.Map(evt, new EventDetailDto());
            dto.Customer = new CustomerDto();
            var result = HttpClientHelper.GetJson<CustomerDto>(customerService, $"individual/{editedRegistrant.CustomerKey}/event");

            if (result.StatusCode == HttpStatusCode.OK)
            {
                dto.Customer = result.Data;
            }
            else
            {
                throw new ServiceException(result.ErrorMessage);
            }

            if (editedRegistrant.Sessions.Count > 0)
            {
                foreach (var editedSession in editedRegistrant.Sessions)
                {
                    if (registrant.Sessions != null)
                    {
                        var session = SessionDao.GetByKey(editedSession.SessionKey);
                        var pendingSessionFee = sessionFees.FirstOrDefault(sessionFee => sessionFee.SessionKey == editedSession.SessionKey);

                        dto.EditedSessions.Add(new UserRegistrationSessionDto
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
            }

            if (registrant.Sessions.Count > 0)
            {
                foreach (var session in registrant.Sessions.Where(x => !x.CancelDate.HasValue && x.InvoiceDetail.Price == 0))
                {
                    {
                        dto.EditedSessions.Add(new UserRegistrationSessionDto
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
                }
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
                            var editedGuestBadge = new EditedRegistrationGuestBadgeDto();
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

            dto.Key = editedRegistrationKey;
            dto.PaymentRequired = true;
            dto.RegistrationStatus = "Edit";

            return dto;
        }

        public EditedRegistrationPaymentDto GetCustomerEventRegistrationForEdit(Guid eventKey, Guid customerKey)
        {
            var dto = new EditedRegistrationPaymentDto();
            dto.EditedSessions = new List<EditedRegistrationPaymentSessionDto>();
            dto.EditedGuestBadges = new List<EditedRegistrationGuestBadgeDto>();
            var registrant = RegistrantDao.GetRegistrant(eventKey, customerKey);
            var editedRegistrant = EditedRegistrationDao.GetByRegistrationKey(registrant.Key);
            var sessionFees = AutoMapper.Mapper.Map(FeeDao.GetSessionFeesForCustomer(editedRegistrant.CustomerKey, editedRegistrant.EventKey, editedRegistrant.RegistrationDate), new List<SessionFeeDto>());

            var editedRegistrantSessions = GetEditedSessions(registrant.Key);

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

            if (editedRegistrantSessions.EditedGuestBadges.Count > 0)
            {
                foreach (var editedGuestBadge in editedRegistrantSessions.EditedGuestBadges)
                {
                    dto.EditedGuestBadges.Add(editedGuestBadge);
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

            dto.CurrentTotal = GetEditedUserCurrentTotal(registrant.CustomerKey, registrant.EventKey, editedRegistrantSessions.EditedSessions);

            dto.Zip = editedRegistrantSessions.Customer.Addresses?.FirstOrDefault(x => x.IsBilling)?.PostalCode;

            return dto;
        }

        private void RemovePendingRegistration(PendingRegistration registration, CustomerDto customer)
        {
            registration.ChangeUser = customer.WebLogin;
            registration.ChangeDate = DateTime.Now;
            registration.DeleteFlag = true;

            PendingRegistrationDao.Store(registration);
        }

        private CustomerDto GetCustomerByWebLogin(string webLogin)
        {
            var dto = new CustomerDto();
            var customerResult = HttpClientHelper.GetJson<CustomerDto>(customerService, $"individual/by-weblogin/{webLogin}/event");

            if (customerResult.StatusCode == HttpStatusCode.OK)
            {
                dto = customerResult.Data;
            }
            else
            {
                throw new ServiceException(customerResult.ErrorMessage);
            }

            return dto;
        }

        private CustomerDto GetCustomerByKey(Guid customerKey)
        {
            var dto = new CustomerDto();
            var customerResult = HttpClientHelper.GetJson<CustomerDto>(customerService, $"individual/{customerKey}/event");

            if (customerResult.StatusCode == HttpStatusCode.OK)
            {
                dto = customerResult.Data;
            }
            else
            {
                throw new ServiceException(customerResult.ErrorMessage);
            }

            return dto;
        }

        private UserRegistrationIntroDto PopulateRegistrationIntro(Event evt, CustomerDto customer)
        {
            var dto = new UserRegistrationIntroDto();
            dto.Customer = customer;
            dto.Event = AutoMapper.Mapper.Map(evt, new UserEventDto());
            var registrantCount = RegistrantDao.GetEventRegistrantsCount(evt.Key);
            dto.IsRegistered = RegistrantDao.IsRegisteredForEvent(evt.Key, customer.Key);

            if (dto.IsRegistered)
            {
                dto.RegistrationKey = RegistrantDao.GetRegistrant(evt.Key, customer.Key).Key;
            }

            if (registrantCount >= dto.Event.Capacity)
            {
                dto.Event.IsSoldOut = true;

                if (evt.RegistrantsOnWait.Any(x => x.CustomerKey == customer.Key))
                    dto.UserIsOnWaitList = true;
            }

            if (evt.PostToWebDate.HasValue && evt.RemoveFromWebDate.HasValue)
            {
                if (DateTime.Now > evt.PostToWebDate.Value.Date && DateTime.Now < evt.RemoveFromWebDate.Value.Date)
                    dto.Event.IsOpenForRegistration = true;
            }

            dto.Event.Fees = AutoMapper.Mapper.Map(FeeDao.GetEventFeesByCustomer(evt.Key, customer.Key, customer.IsMember, DateTime.Today), new List<EventFeeDto>());

            return dto;
        }

        private void SaveContactInfo(PendingRegistration pendingRegistration, UserRegistrationContactInfoDto dto)
        {
            pendingRegistration.ChangeUser = dto.Customer.WebLogin;
            pendingRegistration.ChangeDate = DateTime.Now;
            pendingRegistration.CustomerAddressKey = dto.SelectedAddressKey;
            pendingRegistration.CustomerPhoneKey = dto.SelectedPhoneKey;
            pendingRegistration.EmergencyContactName = dto.EmergencyContactName;
            pendingRegistration.EmergencyContactPhone = dto.EmergencyContactPhone;

            var attendeeBadge = new PendingRegistrationBadge();
            attendeeBadge.AddUser = dto.Customer.WebLogin;
            attendeeBadge.AddDate = DateTime.Now;
            attendeeBadge.NickName = dto.Badge.NickName;
            attendeeBadge.FirstName = dto.Badge.FirstName;
            attendeeBadge.LastName = dto.Badge.LastName;
            attendeeBadge.Company = dto.Badge.Company;
            attendeeBadge.Position = dto.Badge.Position;
            attendeeBadge.City = dto.Badge.City;
            attendeeBadge.State = dto.Badge.State;
            attendeeBadge.Country = dto.Badge.Country;
            attendeeBadge.Notes = dto.Badge.Notes;
            attendeeBadge.IsGuest = false;
            pendingRegistration.Badges.Add(attendeeBadge);

            PendingRegistrationDao.Store(pendingRegistration);
        }

        private decimal GetCurrentTotal(Guid customerKey, UserEventDto evt, Guid priceKey, List<PendingRegistrationSession> sessions)
        {
            var total = 0m;
            var fee = evt.Fees.FirstOrDefault(x => x.PriceKey == priceKey);

            if (fee == null)
            {
                return total;
            }

            total = evt.Fees.FirstOrDefault(x => x.PriceKey == priceKey).Price;

            if (sessions.Any())
            {
                var sessionFees = AutoMapper.Mapper.Map(FeeDao.GetSessionFeesForCustomer(customerKey, evt.Key, DateTime.Now), new List<SessionFeeDto>());

                foreach (var session in sessions)
                {
                    var sessionFee = sessionFees.FirstOrDefault(x => x.SessionKey == session.SessionKey);

                    if (sessionFee != null)
                    {
                        total += sessionFee.Price * session.SelectedQuantity;
                    }
                }
            }

            return total;
        }

        private decimal GetEditCurrentTotal(Guid customerKey, UserEventDto evt, List<EditedRegistrationSession> sessions)
        {
            var total = 0m;

            if (sessions.Any())
            {
                var sessionFees = AutoMapper.Mapper.Map(FeeDao.GetSessionFeesForCustomer(customerKey, evt.Key, DateTime.Now), new List<SessionFeeDto>());

                foreach (var session in sessions)
                {
                    var sessionFee = sessionFees.FirstOrDefault(x => x.SessionKey == session.SessionKey);

                    if (sessionFee != null)
                    {
                        total += sessionFee.Price * session.SelectedQuantity;
                    }
                }
            }

            return total;
        }

        private decimal GetEditedUserCurrentTotal(Guid customerKey, Guid eventKey, List<UserRegistrationSessionDto> sessions)
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