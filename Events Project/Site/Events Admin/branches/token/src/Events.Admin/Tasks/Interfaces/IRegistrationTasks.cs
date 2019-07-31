using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Aafp.Events.Admin.ViewModels;
using Aafp.Events.Admin.ViewModels.Payment;
using Aafp.Events.Admin.ViewModels.Registration;
using ApiClientHelper.Components;

namespace Aafp.Events.Admin.Tasks.Interfaces
{
    public interface IRegistrationTasks
    {
        UserSearchViewModel GetHomeViewModel();

        CustomerSearchViewModel Search(UserSearchViewModel model);

        RegistrationTypeViewModel GetEventRegistrationTypeInfoByCustomer(Guid eventKey, Guid customerKey,
            DateTime registrationDate);

        RegistrationViewModel GetPendingRegistration(Guid registrationKey);

        RegistrationViewModel GetNewRegistration(Guid eventKey, Guid customerKey,
            Guid registrationTypeKey, DateTime registrationDate);

        RegistrantViewModel GetCustomerEventRegistration(Guid eventKey, Guid customerKey);

        AafpServiceResult<Guid> UpdatePendingRegistration(Guid eventKey, Guid customerKey,
            Guid registrationTypeKey, DateTime registrationDate, RegistrationViewModel registration);

        AafpServiceResult<Guid> SavePendingRegistration(RegistrationViewModel registration);

        AafpServiceResult<Guid> SaveEditRegistration(EditRegistrationViewModel model);

        EditRegistrationViewModel GetEditRegistrationViewModel(Guid registrationKey);

        PrintRegistrantViewModel GetPrintBadgeSelectorViewModel(Guid registrantKey);

        RegistrantEmailViewModel GetRegistrantEmailViewModel(Guid registrantKey);

        JsonResultViewModel<bool> SendConfirmationEmail(Guid registrantKey, string email);

        JsonResultViewModel<bool> AddToWaitList(Guid eventKey, Guid customerKey);

        SessionCapacityViewModel IncreaseSessionCapacity(Guid sessionKey);

        PaymentConfirmationViewModel GetPaymentConfirmationViewModel(Guid registrantKey);

        BatchViewModel GetEventBatchRegistration();

        RegistrationTypeViewModel GetBatchEventRegistrationInfo(Guid eventKey);

        List<BatchCustomerViewModel> BatchEventRegistrationUpload(HttpRequestBase request, Guid eventKey,
            Guid registrationTypeKey, DateTime registrationDate);



    }
}