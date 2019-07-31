using System;
using Aafp.Events.Web.ViewModels;
using ApiClientHelper.Components;

namespace Aafp.Events.Web.Tasks.Interfaces
{
    public interface IRegistrationTasks
    {

        RegistrationHomeViewModel GetRegistrationHomeViewModel(string webLogin);
        RegistrationIntroViewModel GetNewRegistrationIntroViewModel(string eventCode, string webLogin);

        RegistrationIntroViewModel GetRegistrationIntroViewModel(Guid registrationKey);

        Guid SaveRegistrationIntro(RegistrationIntroViewModel model);

        RegistrationContactInfoViewModel GetRegistrationContactInfoViewModel(Guid registrationKey);

        Guid SaveRegistrationContactInfo(RegistrationContactInfoViewModel model);

        RegistrationStepViewModel GetRegistrationStepViewModel(Guid registrationKey, Guid stepKey);

        Guid SaveRegistrationSessions(RegistrationStepViewModel model);

        RegistrationConflictViewModel GetRegistrationAllowedConflictViewModel(Guid registrationKey);

        RegistrationConflictViewModel GetRegistrationNotAllowedConflictViewModel(Guid registrationKey);

        Guid SaveRegistrationSessionConflicts(RegistrationConflictViewModel model);

        RegistrationConfirmationViewModel GetRegistrationConfirmationViewModel(Guid registrationKey);

        AafpServiceFileResult GetInvoice(Guid invoiceKey);

        JsonResultViewModel<bool> SendConfirmationEmail(Guid registrationKey);

        JsonResultViewModel<bool> AddToWaitList(Guid eventKey, Guid customerKey);

        JsonResultViewModel<bool> SaveComments(Guid registrationKey, string comments);
    }
}
