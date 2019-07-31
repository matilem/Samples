using System;
using System.Collections.Generic;
using Aafp.Events.Api.Dao.Interfaces;
using Aafp.Events.Api.Dtos;
using Aafp.Events.Api.Dtos.Admin.Registration;
using Aafp.Events.Api.Dtos.EditedRegistration;
using Aafp.Events.Api.Dtos.Registrant;
using Aafp.Events.Api.Models;

namespace Aafp.Events.Api.Tasks.Admin.Interfaces
{
    public interface IAdminRegistrationTasks
    {
        IEventDao EventDao { get; set; }

        IPendingRegistrationDao PendingRegistrationDao { get; set; }

        IEditedRegistrationDao EditedRegistrationDao { get; set; }

        IRegistrantDao RegistrantDao { get; set; }

        List<CustomerSearchResultDto> GetAdminCustomerSearchResults(string searchTerm);

        List<EventBaseDto> GetAdminRegistrationEvents();

        EventRegistrationTypeInfoDto GetEventRegistrationTypesByCustomerKey(Guid eventKey, Guid customerKey,
            DateTime registrationDate);

        EventRegistrationTypeInfoDto GetEventRegistrationTypesByWebLogin(Guid eventKey, string webLogin,
            DateTime registrationDate);

        AdminRegistrationDto GetRegistrationFromPendingRegistration(Guid registrationKey);

        EditedRegistration GetRegistrationFromEditedRegistration(Guid registrationKey);

        PendingRegistration GetPendingRegistrationByEvent(Guid eventKey, Guid customerKey);

        EditedRegistration GetEditedRegistrationByEvent(Guid eventKey, Guid customerKey);

        AdminRegistrationDto GetNewRegistration(Guid eventKey, Guid customerKey, Guid registrationTypeKey, DateTime registrationDate);

        Registrant GetCustomerEventRegistration(Guid eventKey, Guid customerKey);

        AdminEditedRegistrationDto SaveRegistration(AdminRegistrationDto registration);

        AdminEditedRegistrationDto SavePendingRegistration(AdminRegistrationDto registration);

        AdminEditedRegistrationDto SaveEditedRegistration(AdminRegistrationDto registration);

        AdminEditedRegistrationDto SaveEditRegistration(AdminRegistrationDto registration);

        AdminRegistrationDto GetRegistration(Guid registrationKey);

        PrintRegistrantDto GetRegistrantForPrinting(Guid registrantKey);

        EditedRegistrationPaymentDto GetCustomerEventRegistrationForEdit(Guid eventKey, Guid customerKey);

        bool SaveEmergencyContactInformation(Guid registrationKey, string contactName, string contactPhone);

        bool SaveBadgeNotes(Guid registrationKey, string badgeNotes);

        bool SaveGuestBadges(Guid registrationKey, List<AdminRegistrationGuestBadgeDto> guestBadges);

        bool SaveEditedGuestBadges(Guid registrationKey, List<AdminRegistrationGuestBadgeDto> guestBadges);

        bool SendConfirmationEmail(Guid registrationKey, string email);

        bool MarkPendingRegistrationAsProcessed(Guid eventKey, Guid customerKey);

        bool MarkEditedRegistrationAsProcessed(Guid eventKey, Guid customerKey);

        bool AddToWaitList(WaitListDto dto);

        bool RemovePendingRegistration(Guid pendingRegistrationKey);

        PaymentConfirmationDto GetPaymentConfirmation(Guid registrantKey);

        EventRegistrationTypeInfoDto GetBatchEventRegistrationInfo(Guid eventKey);

        BatchRegistrationDto SaveBatchEventRegistration(string memberId, Guid eventKey, Guid registrationTypeKey, DateTime registrationDate);

        AdminEditedRegistrationDto GetEditedSessions(Guid editedRegistrationKey);
    }
}
