using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.AccessControl;
using System.Text;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Aafp.Events.Admin.ApplicationConfig;
using Aafp.Events.Admin.Tasks.Interfaces;
using Aafp.Events.Admin.ViewModels;
using Aafp.Events.Admin.ViewModels.Payment;
using Aafp.Events.Admin.ViewModels.Registration;
using ApiClientHelper.Components;

namespace Aafp.Events.Admin.Tasks
{
    public class RegistrationTasks : IRegistrationTasks
    {
        private string eventService = ApplicationConfigManager.Settings.EventServiceUrl;

        public UserSearchViewModel GetHomeViewModel()
        {
            return new UserSearchViewModel();
        }

        public CustomerSearchViewModel Search(UserSearchViewModel model)
        {
            var searchTerm = model.SearchTerm;
            var viewModel = new CustomerSearchViewModel();

            try
            {
                viewModel.UserSearch = model;

                var searchResult = HttpClientHelper.GetJson<List<CustomerSearchResultViewModel>>(eventService,
                    $"admin/registration/customer-search?searchTerm={searchTerm}");

                if (searchResult.StatusCode == HttpStatusCode.OK)
                {
                    viewModel.Results = searchResult.Data;
                }
                else
                {
                    throw new ServiceException(searchResult.ErrorMessage);
                }

                foreach (var item in viewModel.Results)
                {
                    var itemsToRemove = new List<Guid>();

                    foreach (var recentItem in item.Events)
                    {
                        if (recentItem.RelatedRegistrationKeys.Any())
                        {
                            foreach (var key in recentItem.RelatedRegistrationKeys)
                            {
                                var relatedRegistration = item.Events.FirstOrDefault(x => x.RegistrationKey == key);

                                if (relatedRegistration != null)
                                {
                                    recentItem.RelatedRegistrations.Add(relatedRegistration);
                                    itemsToRemove.Add(key);
                                }
                            }
                        }
                    }

                    item.Events.RemoveAll(x => itemsToRemove.Contains(x.RegistrationKey));
                    item.Events = item.Events.OrderByDescending(x => x.IsPending).ToList();
                }

                var eventResults = HttpClientHelper.GetJson<List<EventListItemViewModel>>(eventService,
                    "admin/registration/events");

                if (eventResults.StatusCode == HttpStatusCode.OK)
                {
                    viewModel.Events = eventResults.Data;
                }
                else
                {
                    throw new ServiceException(eventResults.ErrorMessage);
                }

                viewModel.Events = viewModel.Events.OrderByDescending(x => x.StartDate).ToList();
            }
            catch (ServiceException ex)
            {
                viewModel.HasError = true;
                viewModel.ErrorMessage = ex.Message;
            }
            catch (Exception)
            {
                viewModel.HasError = true;
                viewModel.ErrorMessage = "We are unable to retrieve results from the server at this time.";
            }

            return viewModel;
        }

        public RegistrationTypeViewModel GetEventRegistrationTypeInfoByCustomer(Guid eventKey, Guid customerKey, DateTime registrationDate)
        {
            var viewModel = new RegistrationTypeViewModel();

            try
            {
                var result = HttpClientHelper.GetJson<RegistrationTypeViewModel>(eventService,
                    $"admin/registration/registration-types/{eventKey}/customer/{customerKey}?registrationDate={registrationDate}");

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    viewModel = result.Data;
                }
                else
                {
                    throw new ServiceException(result.ErrorMessage);
                }
            }
            catch (ServiceException ex)
            {
                viewModel.HasError = true;
                viewModel.ErrorMessage = ex.Message;
            }
            catch (Exception)
            {
                viewModel.HasError = true;
                viewModel.ErrorMessage = "We are unable to retrieve results from the server at this time.";
            }

            return viewModel;
        }

        public RegistrationViewModel GetPendingRegistration(Guid registrationKey)
        {
            var viewModel = new RegistrationViewModel();

            try
            {
                var result = HttpClientHelper.GetJson<RegistrationViewModel>(eventService,
                    $"admin/registration/pending/{registrationKey}");

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    viewModel = result.Data;
                    viewModel.UserSearch = new UserSearchViewModel();
                    SetUpSessionCapacity(viewModel.Event.Steps);
                }
                else
                {
                    throw new ServiceException(result.ErrorMessage);
                }
            }
            catch (ServiceException ex)
            {
                viewModel.HasError = true;
                viewModel.ErrorMessage = ex.Message;
            }
            catch (Exception)
            {
                viewModel.HasError = true;
                viewModel.ErrorMessage =
                    "We are unable to retrieve the requested pending registration from the server at this time.";
            }

            return viewModel;
        }

        public RegistrationViewModel GetNewRegistration(Guid eventKey, Guid customerKey, Guid registrationTypeKey, DateTime registrationDate)
        {
            var viewModel = new RegistrationViewModel();

            try
            {
                var result = HttpClientHelper.GetJson<RegistrationViewModel>(eventService,
                    $"admin/registration/new?eventKey={eventKey}&customerKey={customerKey}&registrationTypeKey={registrationTypeKey}&registrationDate={registrationDate}");

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    viewModel = result.Data;
                    viewModel.UserSearch = new UserSearchViewModel();
                    SetUpSessionCapacity(viewModel.Event.Steps);
                }
                else
                {
                    throw new ServiceException(result.ErrorMessage);
                }
            }
            catch (ServiceException ex)
            {
                viewModel.HasError = true;
                viewModel.ErrorMessage = ex.Message;
            }
            catch (Exception)
            {
                viewModel.HasError = true;
                viewModel.ErrorMessage = "We are unable to create a new registration at this time.";
            }

            return viewModel;
        }

        public RegistrantViewModel GetCustomerEventRegistration(Guid eventKey, Guid customerKey)
        {
            var viewModel = new RegistrantViewModel();

            try
            {
                var result = HttpClientHelper.GetJson<RegistrantViewModel>(eventService,
                    $"admin/registration/get-registrant/{eventKey}/{customerKey}");

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    viewModel = result.Data;
                }
                else
                {
                    throw new ServiceException(result.ErrorMessage);
                }
            }
            catch (ServiceException ex)
            {
                viewModel.HasError = true;
                viewModel.ErrorMessage = ex.Message;
            }
            catch (Exception)
            {
                viewModel.HasError = true;
                viewModel.ErrorMessage = "We are unable to retrieve results from the server at this time.";
            }

            return viewModel;
        }

        public EditedRegistrationViewModel SaveRegistration(RegistrationViewModel model)
        {
            var viewModel = new EditedRegistrationViewModel();

            try
            {
                var result = HttpClientHelper.PostJson<EditedRegistrationViewModel>(eventService, "admin/registration/save-registration", model);

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    viewModel = result.Data;
                }
                else
                {
                    throw new ServiceException(result.ErrorMessage);
                }
            }
            catch (ServiceException ex)
            {
                viewModel.HasError = true;
                viewModel.ErrorMessage = ex.Message;
            }
            catch (Exception)
            {
                viewModel.HasError = true;
                viewModel.ErrorMessage = "We are unable to create a new registration at this time.";
            }

            return viewModel;
        }

        public RegistrationViewModel GetRegistrationViewModel(Guid registrationKey)
        {
            var viewModel = new RegistrationViewModel();

            try
            {
                var result = HttpClientHelper.GetJson<RegistrationViewModel>(eventService,
                    $"admin/registration/{registrationKey}");

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    viewModel = result.Data;
                    viewModel.UserSearch = new UserSearchViewModel();
                    SetUpSessionCapacity(viewModel.Event.Steps);
                }
                else
                {
                    throw new ServiceException(result.ErrorMessage);
                }
            }
            catch (ServiceException ex)
            {
                viewModel.HasError = true;
                viewModel.ErrorMessage = ex.Message;
            }
            catch (Exception)
            {
                viewModel.HasError = true;
                viewModel.ErrorMessage =
                    "We are unable to retrieve the requested registration from the server at this time.";
            }

            return viewModel;
        }

        public RegistrantEmailViewModel GetRegistrantEmailViewModel(Guid registrantKey)
        {
            var viewModel = new RegistrantEmailViewModel();

            try
            {
                var result = HttpClientHelper.GetJson<EditRegistrationViewModel>(eventService,
                    $"admin/registration/{registrantKey}");

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    viewModel.RegistrantKey = registrantKey;
                    viewModel.FullName = result.Data.Customer.FullName;
                    viewModel.EmailAddress = result.Data.Customer.Email;
                    viewModel.EventTitle = result.Data.Event.Title;
                }
                else
                {
                    throw new ServiceException(result.ErrorMessage);
                }
            }
            catch (ServiceException ex)
            {
                viewModel.HasError = true;
                viewModel.ErrorMessage = ex.Message;
            }
            catch (Exception)
            {
                viewModel.HasError = true;
                viewModel.ErrorMessage =
                    "We are unable to retrieve the requested information from the server at this time.";
            }

            return viewModel;
        }

        public SessionCapacityViewModel IncreaseSessionCapacity(Guid sessionKey)
        {
            var viewModel = new SessionCapacityViewModel();

            try
            {
                var result = HttpClientHelper.PostJson<SessionViewModel>(eventService,
                    $"admin/session/{sessionKey}/increase-capacity", string.Empty);

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    viewModel = new SessionCapacityViewModel
                    {
                        SessionKey = result.Data.Key,
                        SessionCode = result.Data.Code,
                        SessionTitle = result.Data.Title,
                        SessionCapacity = result.Data.Capacity,
                        SessionRegistrantCount = result.Data.RegisteredTicketsTotal,
                        SessionTicketed = result.Data.Ticketed
                    };
                }
                else
                {
                    throw new ServiceException(result.ErrorMessage);
                }
            }
            catch (ServiceException ex)
            {
                viewModel.HasError = true;
                viewModel.ErrorMessage = ex.Message;
            }
            catch (Exception)
            {
                viewModel.HasError = true;
                viewModel.ErrorMessage = "We are unable to increase session capacity at this time.";
            }

            return viewModel;
        }

        public PrintRegistrantViewModel GetPrintBadgeSelectorViewModel(Guid registrantKey)
        {
            var viewModel = new PrintRegistrantViewModel();

            try
            {
                var result = HttpClientHelper.GetJson<PrintRegistrantViewModel>(eventService,
                    $"admin/registration/print/{registrantKey}");

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    viewModel = result.Data;
                }
                else
                {
                    throw new ServiceException(result.ErrorMessage);
                }
            }
            catch (ServiceException ex)
            {
                viewModel.HasError = true;
                viewModel.ErrorMessage = ex.Message;
            }
            catch (Exception)
            {
                viewModel.HasError = true;
                viewModel.ErrorMessage = "We are unable to retrieve the print options for the user at this time.";
            }

            return viewModel;
        }

        public JsonResultViewModel<bool> SendConfirmationEmail(Guid registrantKey, string email)
        {
            var viewModel = new JsonResultViewModel<bool>();

            try
            {
                var result = HttpClientHelper.PostJson<bool>(eventService,
                    $"admin/registration/confirmation-email/{registrantKey}", email);

                if (result.StatusCode != HttpStatusCode.OK)
                {
                    throw new ServiceException(result.ErrorMessage);
                }
                else
                {
                    viewModel.Data = result.Data;
                }
            }
            catch (ServiceException ex)
            {
                viewModel.HasError = true;
                viewModel.ErrorMessage = ex.Message;
            }
            catch (Exception)
            {
                viewModel.HasError = true;
                viewModel.ErrorMessage = "We are unable to send the confirmation email for the user at this time.";
            }

            return viewModel;
        }

        public JsonResultViewModel<bool> AddToWaitList(Guid eventKey, Guid customerKey)
        {
            var viewModel = new JsonResultViewModel<bool>();

            try
            {
                dynamic data = new ExpandoObject();
                data.EventKey = eventKey;
                data.CustomerKey = customerKey;
                var result = HttpClientHelper.PostJson<bool>(eventService, $"admin/registration/add-to-wait-list", data);

                if (result.StatusCode != HttpStatusCode.OK)
                {
                    throw new ServiceException(result.ErrorMessage);
                }
                else
                {
                    viewModel.Data = result.Data;
                }
            }
            catch (ServiceException ex)
            {
                viewModel.HasError = true;
                viewModel.ErrorMessage = ex.Message;
            }
            catch (Exception)
            {
                viewModel.HasError = true;
                viewModel.ErrorMessage = "We are unable to add the user to the wait list at this time.";
            }

            return viewModel;
        }

        private void SetUpSessionCapacity(List<StepViewModel> steps)
        {
            foreach (var step in steps)
            {
                foreach (var heading in step.Headings)
                {
                    foreach (var session in heading.Sessions)
                    {
                        session.SessionCapacity = new SessionCapacityViewModel
                        {
                            SessionKey = session.Key,
                            SessionCode = session.Code,
                            SessionTitle = session.Title,
                            SessionCapacity = session.Capacity,
                            SessionRegistrantCount = session.RegisteredTicketsTotal,
                            SessionTicketed = session.Ticketed
                        };
                    }
                }
            }
        }

        public PaymentConfirmationViewModel GetPaymentConfirmationViewModel(Guid registrationKey)
        {
            var viewModel = new PaymentConfirmationViewModel();

            try
            {
                var result = HttpClientHelper.GetJson<PaymentConfirmationViewModel>(eventService,
                    $"admin/registration/payment-confirmation/{registrationKey}");

                if (result.StatusCode != HttpStatusCode.OK)
                {
                    throw new ServiceException(result.ErrorMessage);
                }
                else
                {
                    viewModel = result.Data;
                }
            }
            catch (ServiceException ex)
            {
                viewModel.HasError = true;
                viewModel.ErrorMessage = ex.Message;
            }
            catch (Exception)
            {
                viewModel.HasError = true;
                viewModel.ErrorMessage = "We are unable to return a registration confirmation at this time.";
            }

            viewModel.UserSearch = new UserSearchViewModel();

            return viewModel;
        }

        public RegistrationViewModel GetRegistration(Guid eventKey, Guid customerKey)
        {
            var viewModel = new RegistrationViewModel();

            try
            {
                var result = HttpClientHelper.GetJson<RegistrationViewModel>(eventService,
                    $"admin/registration/eventKey={eventKey}&customerKey={customerKey}");

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    viewModel = result.Data;
                    viewModel.UserSearch = new UserSearchViewModel();
                    SetUpSessionCapacity(viewModel.Event.Steps);
                }
                else
                {
                    throw new ServiceException(result.ErrorMessage);
                }
            }
            catch (ServiceException ex)
            {
                viewModel.HasError = true;
                viewModel.ErrorMessage = ex.Message;
            }
            catch (Exception)
            {
                viewModel.HasError = true;
                viewModel.ErrorMessage = "We are unable to create a new registration at this time.";
            }

            return viewModel;
        }

        public BatchViewModel GetEventBatchRegistration()
        {
            var viewModel = new BatchViewModel();

            var eventResults = HttpClientHelper.GetJson<List<EventListItemViewModel>>(eventService,
                "admin/registration/events");

            if (eventResults.StatusCode == HttpStatusCode.OK)
            {
                viewModel.UserSearch = new UserSearchViewModel();
                viewModel.Events = eventResults.Data;
            }
            else
            {
                throw new ServiceException(eventResults.ErrorMessage);
            }

            viewModel.Events = viewModel.Events.OrderByDescending(x => x.StartDate).ToList();

            return viewModel;
        }

        public RegistrationTypeViewModel GetBatchEventRegistrationInfo(Guid eventKey)
        {
            var viewModel = new RegistrationTypeViewModel();

            try
            {
                var result = HttpClientHelper.GetJson<RegistrationTypeViewModel>(eventService,
                    $"admin/registration/batch/get-eventinfo/{eventKey}");

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    viewModel = result.Data;
                }
                else
                {
                    throw new ServiceException(result.ErrorMessage);
                }
            }
            catch (ServiceException ex)
            {
                viewModel.HasError = true;
                viewModel.ErrorMessage = ex.Message;
            }
            catch (Exception)
            {
                viewModel.HasError = true;
                viewModel.ErrorMessage = "We are unable to retrieve results from the server at this time.";
            }

            return viewModel;
        }

        public List<BatchCustomerViewModel> BatchEventRegistrationUpload(HttpRequestBase request, Guid eventKey, Guid registrationTypeKey, DateTime registrationDate)
        {
            var batchcustomers = new List<BatchCustomerViewModel>();

            var ids = UploadFile(request);

            foreach (var id in ids)
            {
                var viewModel = new BatchCustomerViewModel();

                try
                {
                    var result = HttpClientHelper.GetJson<BatchCustomerViewModel>(eventService, $"admin/registration/batch/save-registration/event/{eventKey}/customer/{id}/type/{registrationTypeKey}?registrationDate={registrationDate}");

                    viewModel.FirstName = result.Data.FirstName;
                    viewModel.LastName = result.Data.LastName;
                    viewModel.CustomerKey = result.Data.CustomerKey;
                    viewModel.MemberId = result.Data.MemberId;
                    viewModel.RegistrationStatus = result.Data.RegistrationStatus;
                    
                    if (result.StatusCode != HttpStatusCode.OK)
                    {
                        throw new ServiceException(result.ErrorMessage);
                    }

                    batchcustomers.Add(viewModel);
                }
                catch (ServiceException ex)
                {
                    viewModel.ErrorMessage = ex.Message;
                }
                catch (Exception)
                {
                    viewModel.ErrorMessage = "We are unable to save the registration at this time.";
                }
            }

            return batchcustomers;
        }

        public JsonResultViewModel<bool> RemovePendingRegistration(Guid pendingRegistrationKey)
        {
            var viewModel = new JsonResultViewModel<bool>();

            try
            {
                var result = HttpClientHelper.PostJson<bool>(eventService, $"admin/registration/pending/remove/{pendingRegistrationKey}", null);

                if (result.StatusCode != HttpStatusCode.OK)
                {
                    throw new ServiceException(result.ErrorMessage);
                }
                else
                {
                    viewModel.Data = result.Data;
                }
            }
            catch (ServiceException ex)
            {
                viewModel.HasError = true;
                viewModel.ErrorMessage = ex.Message;
            }
            catch (Exception)
            {
                viewModel.HasError = true;
                viewModel.ErrorMessage = "We are unable to remove the pending registration for the user at this time.";
            }

            return viewModel;
        }

        private List<string> UploadFile(HttpRequestBase request)
        {
            var fileInfo = new UploadFileResultModel();
            List<string> memberIds = new List<string>();

            for (int i = 0; i < request.Files.Count; i++)
            {
                var file = request.Files[i];
                fileInfo.Name = file.FileName;

                using (StreamReader reader = new StreamReader(file.InputStream, Encoding.ASCII, true))
                {
                    while (!reader.EndOfStream)
                    {
                        var record = reader.ReadLine().Split(',');

                        foreach (var item in record)
                        {
                            memberIds.Add(item);
                        }
                    }
                }
            }

            return memberIds;
        }
    }
}