using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using Aafp.Events.Web.ApplicationConfig;
using Aafp.Events.Web.Tasks.Interfaces;
using Aafp.Events.Web.ViewModels;
using ApiClientHelper.Components;

namespace Aafp.Events.Web.Tasks
{
    public class RegistrationTasks : IRegistrationTasks
    {
        private string eventService = ApplicationConfigManager.Settings.EventServiceUrl;

        public RegistrationHomeViewModel GetRegistrationHomeViewModel(string webLogin)
        {
            var viewModel = new RegistrationHomeViewModel();

            try
            {
                var homeResults = HttpClientHelper.GetJson<RegistrationHomeViewModel>(eventService, $"registration/home/{webLogin}/");

                if (homeResults.StatusCode == HttpStatusCode.OK)
                {
                    viewModel = homeResults.Data;
                }
                else
                {
                    throw new ServiceException(homeResults.ErrorMessage);
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
                viewModel.ErrorMessage = "We are unable to begin a new event registration at this time.";
            }

            return viewModel;
        }

        public RegistrationIntroViewModel GetNewRegistrationIntroViewModel(string eventCode, string webLogin)
        {
            var viewModel = new RegistrationIntroViewModel();

            try
            {
                var introResults = HttpClientHelper.GetJson<RegistrationIntroViewModel>(eventService, $"registration/new/intro/{eventCode}/user/{webLogin}/");

                if (introResults.StatusCode == HttpStatusCode.OK)
                {
                    viewModel = introResults.Data;
                }
                else
                {
                    throw new ServiceException(introResults.ErrorMessage);
                }

                viewModel.Navigation = GetNavigation(Guid.Empty, viewModel.PendingSteps, 1, "Pending");

                if (viewModel.Event.Fees.Count == 0)
                {
                    viewModel.IsEligible = false;
                }
                else if (viewModel.Event.Fees.Count == 1)
                {
                    viewModel.SelectedPriceKey = viewModel.Event.Fees[0].PriceKey;
                    viewModel.IsEligible = true;
                }
                else
                {
                    viewModel.IsEligible = true;
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
                viewModel.ErrorMessage = "We are unable to begin a new event registration at this time.";
            }

            return viewModel;
        }

        public RegistrationIntroViewModel GetRegistrationIntroViewModel(Guid registrationKey)
        {
            var viewModel = new RegistrationIntroViewModel();

            try
            {
                var introResults = HttpClientHelper.GetJson<RegistrationIntroViewModel>(eventService, $"registration/intro/{registrationKey}");

                if (introResults.StatusCode == HttpStatusCode.OK)
                {
                    viewModel = introResults.Data;
                }
                else
                {
                    throw new ServiceException(introResults.ErrorMessage);
                }

                viewModel.Navigation = GetNavigation(registrationKey, viewModel.PendingSteps, 1, "Pending");

                if (viewModel.Event.Fees.Count == 1)
                    viewModel.SelectedPriceKey = viewModel.Event.Fees[0].PriceKey;
            }
            catch (ServiceException ex)
            {
                viewModel.HasError = true;
                viewModel.ErrorMessage = ex.Message;
            }
            catch (Exception)
            {
                viewModel.HasError = true;
                viewModel.ErrorMessage = "We are unable to continue with the selected registration at this time.";
            }

            return viewModel;
        }

        public Guid SaveRegistrationIntro(RegistrationIntroViewModel model)
        {
            var registrationKey = Guid.Empty;
            var result = HttpClientHelper.PostJson<Guid>(eventService, "registration/save/intro", model);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                registrationKey = result.Data;
            }
            else
            {
                throw new ServiceException(result.ErrorMessage);
            }

            return registrationKey;
        }

        public RegistrationContactInfoViewModel GetRegistrationContactInfoViewModel(Guid registrationKey)
        {
            var viewModel = new RegistrationContactInfoViewModel();

            var result = HttpClientHelper.GetJson<RegistrationContactInfoViewModel>(eventService, $"registration/{registrationKey}/contact-info");

            if (result.StatusCode == HttpStatusCode.OK)
            {
                viewModel = result.Data;
            }
            else
            {
                throw new ServiceException(result.ErrorMessage);
            }

            viewModel.Navigation = GetNavigation(registrationKey, viewModel.PendingSteps, 2, "Pending");

            foreach (var navStep in viewModel.Navigation.NavigationSteps)
            {
                navStep.Event = viewModel.Event;
            }

            return viewModel;
        }

        public Guid SaveRegistrationContactInfo(RegistrationContactInfoViewModel model)
        {
            var registrationKey = Guid.Empty;
            var result = HttpClientHelper.PostJson<Guid>(eventService, "registration/save/contact-info", model);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                registrationKey = result.Data;
            }
            else
            {
                throw new ServiceException(result.ErrorMessage);
            }

            return registrationKey;
        }

        public RegistrationStepViewModel GetRegistrationStepViewModel(Guid registrationKey, Guid stepKey)
        {
            var viewModel = new RegistrationStepViewModel();

            var result = HttpClientHelper.GetJson<RegistrationStepViewModel>(eventService, $"registration/{registrationKey}/step/{stepKey}");

            if (result.StatusCode == HttpStatusCode.OK)
            {
                viewModel = result.Data;
            }
            else
            {
                throw new ServiceException(result.ErrorMessage);
            }

            foreach (var heading in viewModel.Headings)
            {
                foreach (var session in heading.Sessions)
                {
                    session.ShowCost = heading.ShowCost;
                    session.ShowAvailableTickets = heading.ShowAvailableTickets;
                    session.ShowTime = heading.ShowTime;
                    session.ShowNumber = heading.ShowNumber;
                }
            }

            var currentStep = viewModel.PendingSteps.First(x => x.Key == stepKey);
            var currentProgress = viewModel.PendingSteps.IndexOf(currentStep) + 3;
            viewModel.Navigation = GetNavigation(registrationKey, viewModel.PendingSteps, currentProgress, "Pending" ,viewModel.PendingEvents);

            return viewModel;
        }

        public RegistrationStepViewModel GetEditRegistrationStepViewModel(Guid registrationKey, Guid stepKey)
        {
            var viewModel = new RegistrationStepViewModel();

            var result = HttpClientHelper.GetJson<RegistrationStepViewModel>(eventService, $"registration/edit/{registrationKey}/step/{stepKey}");

            if (result.StatusCode == HttpStatusCode.OK)
            {
                viewModel = result.Data;
            }
            else
            {
                throw new ServiceException(result.ErrorMessage);
            }

            foreach (var heading in viewModel.Headings)
            {
                foreach (var session in heading.Sessions)
                {
                    session.ShowCost = heading.ShowCost;
                    session.ShowAvailableTickets = heading.ShowAvailableTickets;
                    session.ShowTime = heading.ShowTime;
                    session.ShowNumber = heading.ShowNumber;
                }
            }

            var currentStep = viewModel.PendingSteps.First(x => x.Key == stepKey);
            var currentProgress = viewModel.PendingSteps.IndexOf(currentStep) + 3;
            viewModel.Navigation = GetNavigation(registrationKey, viewModel.PendingSteps, currentProgress, "Edit", viewModel.PendingEvents);

            return viewModel;
        }

        public Guid SaveRegistrationSessions(RegistrationStepViewModel model)
        {
            var registrationKey = Guid.Empty;
            var result = HttpClientHelper.PostJson<Guid>(eventService, "registration/save/sessions", model);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                registrationKey = result.Data;
            }
            else
            {
                throw new ServiceException(result.ErrorMessage);
            }

            return registrationKey;
        }

        public RegistrationConflictViewModel GetRegistrationAllowedConflictViewModel(Guid registrationKey)
        {
            var viewModel = new RegistrationConflictViewModel();

            var result = HttpClientHelper.GetJson<RegistrationConflictViewModel>(eventService, $"registration/{registrationKey}/session-allowed-conflicts");

            if (result.StatusCode == HttpStatusCode.OK)
            {
                viewModel = result.Data;
            }
            else
            {
                throw new ServiceException(result.ErrorMessage);
            }

            viewModel.Navigation = GetNavigation(registrationKey, viewModel.PendingSteps, viewModel.PendingSteps.Count + 3, "Pending", viewModel.PendingEvents);
            viewModel.AllowedConflicts = true;
            viewModel.RegistrationStatus = "Pending";

            return viewModel;
        }

        public RegistrationConflictViewModel GetRegistrationNotAllowedConflictViewModel(Guid registrationKey)
        {
            var viewModel = new RegistrationConflictViewModel();

            var result = HttpClientHelper.GetJson<RegistrationConflictViewModel>(eventService, $"registration/{registrationKey}/session-not-allowed-conflicts");

            if (result.StatusCode == HttpStatusCode.OK)
            {
                viewModel = result.Data;
            }
            else
            {
                throw new ServiceException(result.ErrorMessage);
            }

            viewModel.Navigation = GetNavigation(registrationKey, viewModel.PendingSteps, viewModel.PendingSteps.Count + 3, "Pending", viewModel.PendingEvents);
            viewModel.AllowedConflicts = false;
            viewModel.RegistrationStatus = "Pending";

            return viewModel;
        }

        public RegistrationConflictViewModel GetEditRegistrationAllowedConflictViewModel(Guid registrationKey)
        {
            var viewModel = new RegistrationConflictViewModel();

            var result = HttpClientHelper.GetJson<RegistrationConflictViewModel>(eventService, $"registration/edit/{registrationKey}/session-allowed-conflicts");

            if (result.StatusCode == HttpStatusCode.OK)
            {
                viewModel = result.Data;
            }
            else
            {
                throw new ServiceException(result.ErrorMessage);
            }

            viewModel.Navigation = GetNavigation(registrationKey, viewModel.PendingSteps, viewModel.PendingSteps.Count + 3, "Pending", viewModel.PendingEvents);
            viewModel.AllowedConflicts = true;
            viewModel.RegistrationStatus = "Edit";

            return viewModel;
        }

        public RegistrationConflictViewModel GetEditRegistrationNotAllowedConflictViewModel(Guid registrationKey)
        {
            var viewModel = new RegistrationConflictViewModel();

            var result = HttpClientHelper.GetJson<RegistrationConflictViewModel>(eventService, $"registration/edit/{registrationKey}/session-not-allowed-conflicts");

            if (result.StatusCode == HttpStatusCode.OK)
            {
                viewModel = result.Data;
            }
            else
            {
                throw new ServiceException(result.ErrorMessage);
            }

            viewModel.Navigation = GetNavigation(registrationKey, viewModel.PendingSteps, viewModel.PendingSteps.Count + 3, "Pending", viewModel.PendingEvents);
            viewModel.AllowedConflicts = false;
            viewModel.RegistrationStatus = "Edit";

            return viewModel;
        }

        public Guid SaveRegistrationSessionConflicts(RegistrationConflictViewModel model)
        {
            var registrationKey = Guid.Empty;
            var result = HttpClientHelper.PostJson<Guid>(eventService, "registration/save/session-conflicts", model);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                registrationKey = result.Data;
            }
            else
            {
                throw new ServiceException(result.ErrorMessage);
            }

            return registrationKey;
        }

        public RegistrationConfirmationViewModel GetRegistrationConfirmationViewModel(Guid registrationKey, string status)
        {
            var viewModel = new RegistrationConfirmationViewModel();

            var result = HttpClientHelper.GetJson<RegistrationConfirmationViewModel>(eventService, $"registration/{registrationKey}/confirmation/{status}");

            if (result.StatusCode == HttpStatusCode.OK)
            {
                viewModel = result.Data;
            }
            else
            {
                throw new ServiceException(result.ErrorMessage);
            }

            viewModel.Status = status;

            return viewModel;
        }

        public AafpServiceFileResult GetInvoice(Guid invoiceKey)
        {
            var result = new AafpServiceFileResult();

            try
            {
                using (var client = new WebClient { Credentials = new NetworkCredential("nfaspnet", "H0st3ss", "webad") })
                {
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/pdf");
                    var bytes = client.DownloadData(
                        $"{ApplicationConfigManager.Settings.ReportServerUrl}Custom%20Reports/client_AAFP_ac_invoice_detail_invoice_all_products&rs:Command=Render&rs:Format=PDF&inv_key={invoiceKey}");
                    result.Data = bytes;
                }
            }
            catch (Exception)
            {
                result.ErrorMessage = "We are unable to retrieve your invoice at this time.";
            }


            return result;
        }

        public JsonResultViewModel<bool> SendConfirmationEmail(Guid registrationKey)
        {
            var viewModel = new JsonResultViewModel<bool>();

            try
            {
                var result = HttpClientHelper.PostJson<bool>(eventService, $"registration/confirmation-email/{registrationKey}", null);

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

        public JsonResultViewModel<bool> SaveComments(Guid registrationKey, string comments)
        {
            var viewModel = new JsonResultViewModel<bool>();

            try
            {
                var result = HttpClientHelper.PostJson<bool>(eventService, $"registration/comments/{registrationKey}", comments);

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
                viewModel.ErrorMessage = "We are unable to save your comments at this time.";
            }

            return viewModel;
        }

        public RegistrationContactInfoViewModel GetRegistrationContactInfoViewModelForEdit(Guid registrationKey)
        {
            var viewModel = new RegistrationContactInfoViewModel();

            var result = HttpClientHelper.GetJson<RegistrationContactInfoViewModel>(eventService, $"registration/edit/{registrationKey}/contact-info");

            if (result.StatusCode == HttpStatusCode.OK)
            {
                viewModel = result.Data;
            }
            else
            {
                throw new ServiceException(result.ErrorMessage);
            }

            return viewModel;
        }

        public bool UpdateContactInformation(RegistrationContactInfoViewModel model)
        {
            var success = false;

            var result = HttpClientHelper.PostJson<bool>(eventService, $"registration/update/{model.RegistrationKey}/contact-info", model);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                success = true;
            }

            return success;
        }

        public RegistrationStepViewModel GetRegistrationSessionsForEdit(Guid registrationKey)
        {
            var editSessions = new RegistrationEditSessionsViewModel();
            var viewModel = new RegistrationStepViewModel();

            var result = HttpClientHelper.GetJson<RegistrationEditSessionsViewModel>(eventService, $"registration/edit/{registrationKey}/sessions");

            if (result.StatusCode == HttpStatusCode.OK)
            {
                editSessions = result.Data;
            }
            else
            {
                throw new ServiceException(result.ErrorMessage);
            }

            viewModel.Event = editSessions.Event;
            viewModel.StepKey = editSessions.StepKey;
            viewModel.StepDescription = editSessions.StepDescription;
            viewModel.StepSequence = editSessions.StepSequence;
            viewModel.Headings = editSessions.Headings;
            viewModel.PendingSteps = editSessions.Event.Steps;
            viewModel.RegistrationStatus = editSessions.RegistrationStatus;
            viewModel.Customer = new CustomerViewModel();
            viewModel.Customer.WebLogin = editSessions.WebLogin;

            foreach (var heading in viewModel.Headings)
            {
                foreach (var session in heading.Sessions)
                {
                    session.ShowCost = heading.ShowCost;
                    session.ShowAvailableTickets = heading.ShowAvailableTickets;
                    session.ShowTime = heading.ShowTime;
                    session.ShowNumber = heading.ShowNumber;
                }
            }

            var currentStep = viewModel.PendingSteps.First(x => x.Key == editSessions.StepKey);
            var currentProgress = viewModel.PendingSteps.IndexOf(currentStep) + 3;
            viewModel.Navigation = GetNavigation(registrationKey, viewModel.PendingSteps, currentProgress, "Edit", viewModel.PendingEvents);

            foreach (var navStep in viewModel.Navigation.NavigationSteps)
            {
                navStep.Event = viewModel.Event;
            }


            return viewModel;
        }

        private RegistrationNavigationViewModel GetNavigation(Guid registrationKey, List<EventStepViewModel> steps, int progress, string status, Dictionary<Guid, EventViewModel> pendingEvents = null)
        {
            var viewModel = new RegistrationNavigationViewModel();
            viewModel.NavigationSteps = new List<RegistrationNavigationStepViewModel>();
            viewModel.CurrentProgress = progress;
            viewModel.RegistrationStatus = status;

            viewModel.IntroUrl = $"{ApplicationConfigManager.Settings.ApplicationUrl}registration/intro/{registrationKey}";
            viewModel.ContactInfoUrl = $"{ApplicationConfigManager.Settings.ApplicationUrl}registration/contact-info/{registrationKey}";

            foreach (var step in steps)
            {
                var stepViewModel = new RegistrationNavigationStepViewModel
                {
                    StepKey = step.Key,
                    StepLink = $"{ApplicationConfigManager.Settings.ApplicationUrl}registration/sessions/{registrationKey}?stepKey={step.Key}",
                    StepDescription = step.StepDescription,
                    StepSequence = step.StepSequence
                };

                if (pendingEvents != null)
                {
                    stepViewModel.Event = pendingEvents[step.Key];
                }

                viewModel.NavigationSteps.Add(stepViewModel);
            }

            viewModel.ConflictUrl = $"{ApplicationConfigManager.Settings.ApplicationUrl}registration/conflicts/{registrationKey}";
            viewModel.PaymentUrl = $"{ApplicationConfigManager.Settings.PaymentsUrl}registration/{registrationKey}";

            return viewModel;
        }
    }
}