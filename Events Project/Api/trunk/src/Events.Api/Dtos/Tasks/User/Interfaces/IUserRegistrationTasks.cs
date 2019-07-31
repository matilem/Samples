using System;
using System.Collections.Generic;
using Aafp.Events.Api.Dao.Interfaces;
using Aafp.Events.Api.Dtos.EditedRegistration;
using Aafp.Events.Api.Dtos.User.Registration;

namespace Aafp.Events.Api.Tasks.User.Interfaces
{
    public interface IUserRegistrationTasks
    {
        IEventDao EventDao { get; set; }

        IFeeDao FeeDao { get; set; }

        IRegistrantDao RegistrantDao { get; set; }

        List<UserRegistrationDto> GetRegistrationsForUserProfile(string webLogin);

        UserRegistrationHomeDto GetRegistrationHome(string webLogin);

        UserRegistrationIntroDto GetNewRegistrationIntro(string eventCode, string webLogin);

        UserRegistrationIntroDto GetRegistrationIntro(Guid pendingRegistrationKey);

        Guid SaveRegistrationIntro(UserRegistrationIntroDto dto);

        UserRegistrationContactInfoDto GetRegistrationContactInfo(Guid pendingRegistrationKey);

        Guid SaveRegistrationContactInfo(UserRegistrationContactInfoDto dto);

        UserRegistrationStepDto GetRegistrationStep(Guid registrationKey, Guid stepKey);

        UserRegistrationStepDto GetEditRegistrationStep(Guid registrationKey, Guid stepKey);

        Guid SaveRegistrationSessions(UserRegistrationStepDto dto);

        UserRegistrationConflictDto GetRegistrationSessionConflicts(Guid registrationKey, int conflictType);

        UserRegistrationConflictDto GetEditRegistrationSessionConflicts(Guid registrationKey, int conflictType);

        Guid ResolveSessionConflicts(UserRegistrationConflictDto dto);

        Guid ResolveRegistrationSessionConflicts(UserRegistrationConflictDto dto);

        Guid ResolveEditRegistrationSessionConflicts(UserRegistrationConflictDto dto);

        UserRegistrationConfirmationDto GetRegistrationConfirmation(Guid registrationKey, string status);

        bool SendConfirmationEmail(Guid registrationKey);

        bool SaveUserComments(Guid registrationKey, string comments);

        EditUserRegistrationContactInfoDto GetRegistrationContactInfoForEdit(Guid registrationKey);

        bool UpdateRegistrationContactInfo(UserRegistrationContactInfoDto dto);

        EditUserRegistrationSessionsDto GetRegistrationSessionsForEdit(Guid registrationKey);

        UserEditedRegistrationDto GetEditedSessions(Guid editedRegistrationKey);

        EditedRegistrationPaymentDto GetCustomerEventRegistrationForEdit(Guid eventKey, Guid customerKey);
    }
}
