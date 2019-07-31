using System;
using System.Net;
using Aafp.Events.Admin.ApplicationConfig;
using Aafp.Events.Admin.Tasks.Interfaces;
using Aafp.Events.Admin.ViewModels;
using Aafp.Events.Admin.ViewModels.Registration;
using ApiClientHelper.Components;

namespace Aafp.Events.Admin.Tasks
{
    public class BadgeTasks : IBadgeTasks
    {
        private string eventService = ApplicationConfigManager.Settings.EventServiceUrl;

        public AafpServiceFileResult GetRegistrantBadgePdf(Guid registrantkey)
        {
            var result = new AafpServiceFileResult();

            try
            {
                result = HttpClientHelper.GetPdf(eventService, $"admin/badge/registrant/{registrantkey}/pdf");

                if (result.StatusCode != HttpStatusCode.OK)
                {
                    throw new ServiceException(result.ErrorMessage);
                }
            }
            catch (ServiceException ex)
            {
                result.ErrorMessage = ex.Message;
            }
            catch (Exception)
            {
                result.ErrorMessage = "We are unable to retrieve the requested badge for the user at this time.";
            }
            
            return result;
        }

        public AafpServiceFileResult GetRegistrantBadgePdfAll(Guid registrantkey)
        {
            var result = new AafpServiceFileResult();

            try
            {
                result = HttpClientHelper.GetPdf(eventService, $"admin/badge/registrant/{registrantkey}/pdf/all");

                if (result.StatusCode != HttpStatusCode.OK)
                {
                    throw new ServiceException(result.ErrorMessage);
                }
            }
            catch (ServiceException ex)
            {
                result.ErrorMessage = ex.Message;
            }
            catch (Exception)
            {
                result.ErrorMessage = "We are unable to retrieve the requested badge for the user at this time.";
            }

            return result;
        }

        public AafpServiceFileResult GetRegistrantSessionBadgePdf(Guid sessionKey)
        {
            var result = new AafpServiceFileResult();

            try
            {
                result = HttpClientHelper.GetPdf(eventService, $"admin/badge/session/{sessionKey}/pdf");

                if (result.StatusCode != HttpStatusCode.OK)
                {
                    throw new ServiceException(result.ErrorMessage);
                }
            }
            catch (ServiceException ex)
            {
                result.ErrorMessage = ex.Message;
            }
            catch (Exception)
            {
                result.ErrorMessage = "We are unable to retrieve the requested badge for the user at this time.";
            }

            return result;
        }

        public AafpServiceFileResult GetRegistrantSessionBadgePdfs(Guid registrantKey)
        {
            var result = new AafpServiceFileResult();

            try
            {
                result = HttpClientHelper.GetPdf(eventService, $"admin/badge/registrant/{registrantKey}/sessions/pdf");

                if (result.StatusCode != HttpStatusCode.OK)
                {
                    throw new ServiceException(result.ErrorMessage);
                }
            }
            catch (ServiceException ex)
            {
                result.ErrorMessage = ex.Message;
            }
            catch (Exception)
            {
                result.ErrorMessage = "We are unable to retrieve the requested badge for the user at this time.";
            }

            return result;
        }

        public PrintEventBadgeViewModel GetPrintEventBadgeViewModel(Guid eventKey)
        {
            var viewModel = new PrintEventBadgeViewModel();

            try
            {
                var result = HttpClientHelper.GetJson<EventDetailViewModel>(eventService, $"events/{eventKey}");

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    viewModel.EventKey = result.Data.Key;
                    viewModel.EventLocation = result.Data.LocationDisplay;
                    viewModel.EventTitle = result.Data.TitleDisplay;
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
                viewModel.ErrorMessage = "We are unable to retrieve the requested badge for the user at this time.";
            }

            return viewModel;
        }

        public AafpServiceFileResult GetEventPdfs(PrintEventBadgeViewModel model)
        {
            var result = new AafpServiceFileResult();

            try
            {
                result = HttpClientHelper.GetPdf(eventService, $"admin/badge/event/{model.EventKey}/pdf?startDate={model.StartDate}&endDate={model.EndDate}");

                if (result.StatusCode != HttpStatusCode.OK)
                {
                    throw new ServiceException(result.ErrorMessage);
                }
            }
            catch (ServiceException ex)
            {
                result.ErrorMessage = ex.Message;
            }
            catch (Exception)
            {
                result.ErrorMessage = "We are unable to retrieve the requested badge for the user at this time.";
            }

            return result;
        }

        public AafpServiceFileResult GetInvoice(Guid invoiceKey)
        {
            var result = new AafpServiceFileResult();

            try
            {
                using (var client = new WebClient { Credentials = new NetworkCredential("nfaspnet", "ensoniq9", "webad") })
                {
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/pdf");
                    var bytes = client.DownloadData(
                        $"{ApplicationConfigManager.Settings.ReportServerUrl}Custom%20Reports/client_AAFP_ac_invoice_detail_invoice_all_products&rs:Command=Render&rs:Format=PDF&inv_key={invoiceKey}");
                    result.Data = bytes;
                }
            }
            catch (Exception)
            {
                result.ErrorMessage = "We are unable to retrieve the requested badge for the user at this time.";
            }
            

            return result;
        }
    }
}
