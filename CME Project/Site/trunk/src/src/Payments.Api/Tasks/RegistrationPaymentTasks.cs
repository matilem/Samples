using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web;
using System.Web.Script.Serialization;
using Aafp.Payments.Api.ApplicationConfig;
using Aafp.Payments.Api.Dao.Interfaces;
using Aafp.Payments.Api.Dtos.Registration;
using Aafp.Payments.Api.Filters;
using Aafp.Payments.Api.Models;
using Aafp.Payments.Api.Tasks.Interfaces;
using ApiClientHelper.Components;
using Avectra.netForum.Common;
using Avectra.netForum.Components.AC;
using Avectra.netForum.Components.EV;
using Avectra.netForum.Components.OE;
using Avectra.netForum.Data;
using Flurl.Http;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NHibernate.Properties;
using Registration = Avectra.netForum.Components.EV.Registration;

namespace Aafp.Payments.Api.Tasks
{
    public class RegistrationPaymentTasks : IRegistrationPaymentTasks
    {
        private static readonly ILog Log = LogManager.GetLogger("Payment-Registration");

        private static string connectionString = ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString;

        private string eventService = ApplicationConfigManager.Settings.EventServiceUrl;

        private string eventsEditingUrl = ApplicationConfigManager.Settings.EventsEditingUrl;

        private string eventsEditingEndpointUrl = ApplicationConfigManager.Settings.EventsEditingEndPointUrl;

        private string eventsEditingUserName = ApplicationConfigManager.Settings.EventsEditingUserName;

        private string eventsEditingPassword = ApplicationConfigManager.Settings.EventsEditingPassword;

        public IDiscountDao DiscountDao { get; set; }

        public IPaymentMethodDao PaymentMethodDao { get; set; }

        public RegistrationDto GetPendingRegistration(Guid key)
        {
            var result = HttpClientHelper.GetJson<RegistrationDto>(eventService, $"admin/registration/pending/{key}");

            if (result.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }

            var dto = result.Data;
            dto.SelectedFee = dto.Event.Fees.First(x => x.PriceKey == dto.PriceKey.Value);
            dto.Discounts = AutoMapper.Mapper.Map(DiscountDao.GetAdminEventDiscountsByEventPrice(dto.PriceKey.Value), new List<RegistrationDiscountDto>());

            foreach (var relatedRegistration in dto.RelatedRegistrations)
            {
                relatedRegistration.SelectedFee = relatedRegistration.Event.Fees.First(x => x.PriceKey == relatedRegistration.PriceKey.Value);
                relatedRegistration.Discounts = AutoMapper.Mapper.Map(DiscountDao.GetAdminEventDiscountsByEventPrice(relatedRegistration.PriceKey.Value), new List<RegistrationDiscountDto>());
            }

            return dto;
        }

        public RegistrationDto GetEditedRegistration(Guid key)
        {
            var result = HttpClientHelper.GetJson<RegistrationDto>(eventService, $"admin/registration/edited/{key}");

            if (result.StatusCode != HttpStatusCode.OK)
            {
                throw new ServiceException("Unable to find the requested registration.");
            }

            var viewModel = result.Data;

            return viewModel;
        }

        public Discount GetDiscount(Guid discountPriceKey)
        {
            var discount = DiscountDao.GetDiscountByKey(discountPriceKey);

            return discount;
        }

        public Discount GetDiscount(Guid priceKey, string discountCode)
        {
            var discount = DiscountDao.GetDiscountByCode(priceKey, discountCode);

            return discount;
        }

        public Guid ProcessPayment(RegistrationDto dto)
        {
            var result = HttpClientHelper.GetJson<RegistrationDto>(eventService, $"admin/registration/pending/{dto.Key}");

            if (result.StatusCode != HttpStatusCode.OK)
            {
                throw new ServiceException("Unable to find the requested registration.");
            }

            if (result.Data.IsSoldOut)
            {
                throw new ServiceException("We're sorry. This event has sold out.");
            }


            var registration = result.Data;
            registration.Payment = dto.Payment;
            registration.SelectedDiscountPriceKey = dto.SelectedDiscountPriceKey;
            registration.CurrentUser = dto.Customer.WebLogin;

            if (registration.UnavailableSessions.Count > 0)
            {
                var exceptionMessage = "The following sessions are no longer available:";
                exceptionMessage += "<ul>";
                foreach (var unavailableSession in registration.UnavailableSessions)
                {
                    exceptionMessage += "<li>";
                    exceptionMessage += unavailableSession.Code;
                    exceptionMessage += "</li>";
                }
                exceptionMessage += "</ul>";

                throw new ServiceException(exceptionMessage);
            }

            var registrationKey = Register(registration, dto.SendConfirmationEmail);

            foreach (var relatedRegistration in registration.RelatedRegistrations)
            {
                relatedRegistration.Payment = new PaymentInfo();
                relatedRegistration.Payment = dto.Payment;
                relatedRegistration.SelectedDiscountPriceKey = dto.SelectedDiscountPriceKey;
                Register(relatedRegistration, dto.SendConfirmationEmail);
            }

            return registrationKey;
        }

        public Guid ProcessEditedPayment(RegistrationDto dto)
        {
            var registrationKey = new Guid();

            switch (dto.IsAdmin)
            {
                case true:
                    return ProcessEditedAdminPayment(dto);
                case false:
                    return ProcessEditedUserPayment(dto);
                default:
                    return registrationKey;
            }
        }

        public Guid ProcessEditedAdminPayment(RegistrationDto dto)
        {
            var editedRegistration = new EditedRegistrationDto();
            editedRegistration.Sessions = new List<EditedRegistrationSessionDto>();

            var result = HttpClientHelper.GetJson<EditedRegistrationPaymentDto>(eventService, $"admin/registration/get-registrant/edit/{dto.Event.Key}/{dto.Customer.Key}");

            if (result.StatusCode != HttpStatusCode.OK)
            {
                throw new ServiceException("Unable to find the requested registration.");
            }

            var registration = result.Data;
            registration.WebLogin = dto.Customer.WebLogin;

            editedRegistration.RegistrationKey = registration.Key;
            editedRegistration.BatchKey = CreateBatch();
            editedRegistration.InvoiceTermsKey = registration.InvoiceTermsKey;
            editedRegistration.CurrentUser = dto.CurrentUser;

            if (registration.EditedSessions.Count > 0)
            {
                foreach (var editedSession in registration.EditedSessions)
                {
                    editedRegistration.Sessions.Add(new EditedRegistrationSessionDto
                    {
                        Key = editedSession.Key,
                        SessionKey = editedSession.SessionKey,
                        SelectedQuantity = editedSession.Quantity,
                        PriceKey = editedSession.Fee.PriceKey,
                    });
                }

                if (dto.Payment.CreditCardNumber != null)
                {
                    editedRegistration.Payment = new EditedPaymentInfoDto();

                    var card = dto.Payment.ToCreditCard();
                    var paymentMethod = PaymentMethodDao.GetByMethod(card.GetCardType());

                    editedRegistration.Payment.Key = paymentMethod.Key;
                    editedRegistration.Payment.CardholderName = dto.Payment.CardholderName;
                    editedRegistration.Payment.CreditCardNumber = dto.Payment.CreditCardNumber;
                    editedRegistration.Payment.VerificationCode = dto.Payment.VerificationCode;
                    editedRegistration.Payment.ExpirationMonth = dto.Payment.ExpirationMonth;
                    editedRegistration.Payment.ExpirationYear = dto.Payment.ExpirationYear;
                    editedRegistration.Payment.CustomerKey = dto.Customer.Key;
                    editedRegistration.Payment.BillingZip = registration.Zip;
                    editedRegistration.Payment.TotalDue = registration.CurrentTotal;
                    dto.CurrentUser = dto.Customer.WebLogin;

                    int fourDigitYear = CultureInfo.CurrentCulture.Calendar.ToFourDigitYear(dto.Payment.ExpirationYear);
                    string twoDigitMonth = dto.Payment.ExpirationMonth.ToString("00");

                    editedRegistration.Payment.ExpirationDateDisplay = string.Format("{0}/{1}", fourDigitYear,
                        twoDigitMonth);
                }

                editedRegistration.RegistrationKey = RegisterEdited(editedRegistration);
            }

            else
            {
                if (registration.EditedGuestBadges != null)
                {
                    StoreEditedGuestsInformation(registration);
                    SendEmail(dto, editedRegistration.RegistrationKey);
                }
            }

            return editedRegistration.RegistrationKey;
        }

        public Guid ProcessEditedUserPayment(RegistrationDto dto)
        {
            var editedRegistration = new EditedRegistrationDto();
            editedRegistration.Sessions = new List<EditedRegistrationSessionDto>();
            editedRegistration.EditedGuestBadges = new List<EditedRegistrationGuestBadgeDto>();

            var result = HttpClientHelper.GetJson<EditedRegistrationPaymentDto>(eventService, $"registration/get-registrant/edit/{dto.Event.Key}/{dto.Customer.Key}");

            if (result.StatusCode != HttpStatusCode.OK)
            {
                throw new ServiceException("Unable to find the requested registration.");
            }

            var registration = result.Data;
            registration.WebLogin = dto.Customer.WebLogin;

            editedRegistration.RegistrationKey = registration.Key;
            editedRegistration.BatchKey = CreateBatch();
            editedRegistration.InvoiceTermsKey = registration.InvoiceTermsKey;
            editedRegistration.CurrentUser = dto.CurrentUser;

            if (registration.EditedSessions.Count > 0)
            {
                foreach (var editedSession in registration.EditedSessions)
                {
                    editedRegistration.Sessions.Add(new EditedRegistrationSessionDto
                    {
                        Key = editedSession.Key,
                        SessionKey = editedSession.SessionKey,
                        SelectedQuantity = editedSession.Quantity,
                        PriceKey = editedSession.Fee.PriceKey,
                    });
                }


                if (dto.Payment.CreditCardNumber != null)
                {
                    editedRegistration.Payment = new EditedPaymentInfoDto();

                    var card = dto.Payment.ToCreditCard();
                    var paymentMethod = PaymentMethodDao.GetByMethod(card.GetCardType());

                    editedRegistration.Payment.Key = paymentMethod.Key;
                    editedRegistration.Payment.CardholderName = dto.Payment.CardholderName;
                    editedRegistration.Payment.CreditCardNumber = dto.Payment.CreditCardNumber;
                    editedRegistration.Payment.VerificationCode = dto.Payment.VerificationCode;
                    editedRegistration.Payment.ExpirationMonth = dto.Payment.ExpirationMonth;
                    editedRegistration.Payment.ExpirationYear = dto.Payment.ExpirationYear;
                    editedRegistration.Payment.CustomerKey = dto.Customer.Key;
                    editedRegistration.Payment.BillingZip = registration.Zip;
                    editedRegistration.Payment.TotalDue = registration.CurrentTotal;
                    dto.CurrentUser = dto.Customer.WebLogin;

                    int fourDigitYear = CultureInfo.CurrentCulture.Calendar.ToFourDigitYear(dto.Payment.ExpirationYear);
                    string twoDigitMonth = dto.Payment.ExpirationMonth.ToString("00");

                    editedRegistration.Payment.ExpirationDateDisplay = string.Format("{0}/{1}", fourDigitYear,
                        twoDigitMonth);
                }

                editedRegistration.RegistrationKey = RegisterEdited(editedRegistration);

                if (registration.EditedGuestBadges.Count > 0)
                {
                    StoreEditedGuestsInformation(registration);
                }

                SendEmail(dto, editedRegistration.RegistrationKey);
            }

            else
            {
                if (registration.EditedGuestBadges.Count > 0)
                {
                    StoreEditedGuestsInformation(registration);
                }
            }

            return editedRegistration.RegistrationKey;
        }

        public Guid RegisterEdited(EditedRegistrationDto editedRegistration)
        {
            var editedRegistrationPaymentResult = new EditedRegistrationPaymentResultDto();

            var netForumEditedRegistration = GetNetForumEditedRegistration(editedRegistration);

            var result = eventsEditingEndpointUrl.WithBasicAuth(eventsEditingUserName, eventsEditingPassword).PostAsync(null).Result;
            var json = JsonConvert.DeserializeObject<JObject>(result.Content.ReadAsStringAsync().Result);
            var token = json["token"].Value<string>();

            if (result.StatusCode != HttpStatusCode.OK)
            {
                Log.ErrorFormat("Error returned from Gravitate Events Editing Payment. Code: {0}, Message{1}", result.StatusCode, result.RequestMessage);
                throw new ServiceException("Unable to process the edited registration.");
            }      

            var editingResult = eventsEditingUrl.WithHeader("X-EndUser", editedRegistration.CurrentUser).WithOAuthBearerToken(token).PostJsonAsync(netForumEditedRegistration).Result;

            if (editingResult.StatusCode != HttpStatusCode.OK)
            {
                Log.ErrorFormat("Error returned from Gravitate Events Editing Payment. Code: {0}, Message{1}", result.StatusCode, result.RequestMessage);
                throw new ServiceException("Unable to process the edited registration.");
            }

            var editingResultData = JsonConvert.DeserializeObject<JObject>(editingResult.Content.ReadAsStringAsync().Result);

            return editedRegistration.RegistrationKey;
        }

        public Guid ProcessPaymentForBatch(Guid eventKey, Guid customerKey, Guid registrationTypeKey, DateTime registrationDate)
        {
            var dto = new RegistrationDto();
            dto.Event = new RegistrationEventDto();
            dto.Customer = new CustomerDto();

            dto.Event.Key = eventKey;
            dto.Customer.Key = customerKey;
            dto.RegistrationDate = registrationDate;
            dto.PriceKey = registrationTypeKey;

            var regKey = Register(dto, false);

            return regKey;
        }

        public Guid Register(RegistrationDto pendingRegistration, bool sendConfirmationEmail)
        {
            Config.SystemOptions = new Hashtable();
            SystemFunctions.LoadSystemOptions();
            Config.SuperUser = true;
            Config.Context.User = new GenericPrincipal(new GenericIdentity(pendingRegistration.Customer.WebLogin), null);
            Config.CurrentUserName = pendingRegistration.Customer.WebLogin;

            using (var connection = DataUtils.GetConnection())
            {

                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                var transaction = connection.BeginTransaction();

                var registration = InitializeRegistration(pendingRegistration);
                AddInvoiceDetailToRegistration(registration);
                AddDiscountsToRegistration(registration, pendingRegistration);
                AddSessionsToRegistration(registration, pendingRegistration);

                if (pendingRegistration.Payment != null
                    && pendingRegistration.Payment.GetPaymentType() == PaymentInfo.PaymentType.Payment)
                    AddCardPaymentToRegistration(registration, pendingRegistration);

                // Have to set values here, instead of InitializeRegistration
                // because otherwise they are overriden somewhere after InitializeRegistration
                AddRegistrantInfo(registration, pendingRegistration);

                SubmitRegistration(transaction, registration);

                var registrationKey = new Guid(registration.GetValue("reg_key"));
                StoreGuestsInformation(registrationKey, pendingRegistration);

                StoreEmergencyInformation(registrationKey, pendingRegistration.EmergencyContactName,
                    pendingRegistration.EmergencyContactPhone);
                if (pendingRegistration.Badge != null)
                {
                    StoreBadgeNotes(registrationKey, pendingRegistration.Badge.Notes);
                }

                if (registrationKey != Guid.Empty && sendConfirmationEmail)
                {
                    SendEmail(pendingRegistration, registrationKey);
                }

                return registrationKey;
            }
        }

        public void SendEmail(RegistrationDto pendingRegistration, Guid registrationKey)
        {
            try
            {
                // Set netforum user credentials
                Config.SystemOptions = new Hashtable();
                SystemFunctions.LoadSystemOptions();
                Config.SuperUser = true;
                Config.Context.User = new GenericPrincipal(new GenericIdentity(pendingRegistration.Customer.WebLogin),
                    null);
                Config.CurrentUserName = pendingRegistration.Customer.WebLogin;

                // OleDbConnection and OleDbTransaction
                var connection = DataUtils.GetConnection();
                var transaction = connection.BeginTransaction();

                // Load netforum registrant object by reg_key
                var registrant = DataUtils.InstantiateFacadeObject("EventsRegistrant");
                registrant.CurrentKey = registrationKey.ToString();
                registrant.SelectByKey(connection, transaction);
                ////registrant.LoadRelatedData();

                // set the email confirmation flag
                registrant.SetValue("inv_send_email_confirmation", "1");

                // Pull email template key from object.  Event must have a confirmation template set in iweb setup
                var emailTemplateKey = registrant.GetValue("evt_cct_key");

                // Get the customers email address
                var emailAddress = registrant.GetValue("cst_eml_address_dn");

                // Check to make sure this event has a confirmation template set up
                // and that we actually have an email address for this customer
                if (!string.IsNullOrEmpty(emailAddress) && !string.IsNullOrEmpty(emailTemplateKey))
                {
                    // Send the email
                    DataUtils.SendTemplate(registrant, emailTemplateKey, emailAddress, String.Empty, connection,
                        transaction, false, false);
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("Unable to send email for registration: {0}, Error: {1}", registrationKey,
                    ex.Message);
            }
        }

        public void StoreGuestsInformation(Guid registrationKey, RegistrationDto pendingRegistration)
        {
            var selectedSessions = new List<RegistrationSessionDto>();

            if (pendingRegistration.Event.Steps != null)
            {
                foreach (var step in pendingRegistration.Event.Steps)
                {
                    foreach (var heading in step.Headings)
                    {
                        foreach (var session in heading.Sessions)
                        {
                            if (session.Selected)
                                selectedSessions.Add(session);
                        }
                    }
                }

                foreach (var session in selectedSessions)
                {
                    if (session.SessionTypeCode == "Guest Badge" &&
                        session.GuestBadges.Any(x => !string.IsNullOrWhiteSpace(x.Name)))
                    {
                        var guestBadges = session.GuestBadges.Where(x => !string.IsNullOrWhiteSpace(x.Name)).ToList();
                        var result = HttpClientHelper.PostJson<bool>(eventService,
                            $"admin/registration/{registrationKey}/badges/guest", guestBadges);

                        if (result.StatusCode != HttpStatusCode.OK || !result.Data)
                            Log.Error($"Unable to save guest badges for registration: {registrationKey}");
                    }
                }
            }
        }

        public void StoreEditedGuestsInformation(EditedRegistrationPaymentDto registration)
        {
            var guestBadges = registration.EditedGuestBadges.Where(x => !string.IsNullOrWhiteSpace(x.Name)).ToList();

            var result = HttpClientHelper.PostJson<bool>(eventService, $"admin/registration/edit/{registration.Key}/badges/guest", guestBadges);

            if (result.StatusCode != HttpStatusCode.OK || !result.Data)
            {
                Log.Error($"Unable to save guest badges for registration: {registration.Key}");
            }
        }

        private Registration InitializeRegistration(RegistrationDto pendingRegistration)
        {
            var registrant = (Registration)DataUtils.InstantiateFacadeObject("EventsRegistrant");

            if (registrant == null)
                throw new Exception("Couldn't create an EventsRegistrant facade object.");

            registrant.oInvoice = new OrderEntry();
            var registrationKey = Guid.NewGuid();
            registrant.SetValue("reg_key", registrationKey.ToString());
            registrant.SetValue("reg_add_user", pendingRegistration.Customer.WebLogin);
            registrant.CurrentKey = registrationKey.ToString();
            registrant.SetValue("reg_cst_key", pendingRegistration.Customer.Key.ToString());
            registrant.SetValue("reg_evt_key", pendingRegistration.Event.Key.ToString());
            registrant.SetValue("reg_ixo_key", registrant.GetValue("cst_ixo_key"));
            registrant.SetValue("prc_key", pendingRegistration.PriceKey.ToString());
            registrant.SetValue("inv_bat_key", ac_utility.GetWebBatchKey());
            registrant.oInvoice.LoadRelatedData();

            return registrant;
        }

        private void AddRegistrantInfo(Registration registration, RegistrationDto pendingRegistration)
        {
            if (pendingRegistration.Badge != null)
            {
                registration.SetValue("reg_attendance_flag", "1");

                if (!string.IsNullOrEmpty(pendingRegistration.Badge.Company))
                    registration.SetValue("reg_org_name_dn", pendingRegistration.Badge.Company);
                else
                    registration.SetValue("reg_org_name_dn", registration.GetValue("org_name"));

                if (!string.IsNullOrEmpty(pendingRegistration.Badge.Position))
                    registration.SetValue("reg_ixo_title_dn", pendingRegistration.Badge.Position);
                else
                    registration.SetValue("reg_ixo_title_dn", registration.GetValue("ixo_title"));

                if (!string.IsNullOrEmpty(pendingRegistration.Badge.NickName))

                    if (!string.IsNullOrEmpty(pendingRegistration.EmergencyContactName))
                    {
                        if (pendingRegistration.EmergencyContactName.Contains(pendingRegistration.Badge.NickName))
                        {
                            pendingRegistration.Badge.NickName = pendingRegistration.Customer.FirstName;
                        }
                    }

                registration.SetValue("reg_badge_name", pendingRegistration.Badge.NickName);

                if (!string.IsNullOrEmpty(pendingRegistration.Badge.Country))
                    registration.SetValue("reg_adr_country", pendingRegistration.Badge.Country);

                if (!string.IsNullOrEmpty(pendingRegistration.Badge.State))
                    registration.SetValue("reg_adr_state", pendingRegistration.Badge.State);

                if (!string.IsNullOrEmpty(pendingRegistration.Badge.City))
                    registration.SetValue("reg_adr_city", pendingRegistration.Badge.City);

                SetRegistrantAddress(registration, pendingRegistration);
            }
        }

        private void SetRegistrantAddress(Registration registrant, RegistrationDto pendingRegistration)
        {
            var addressKey = pendingRegistration.CustomerAddressKey?.ToString();
            registrant.SetValue("reg_cxa_key", addressKey);
        }

        private void AddSessionsToRegistration(Registration registration, RegistrationDto pendingRegistration)
        {
            var selectedSessions = new List<RegistrationSessionDto>();

            if (pendingRegistration.Event.Steps != null)
            {
                foreach (var step in pendingRegistration.Event.Steps)
                {
                    foreach (var heading in step.Headings)
                    {
                        foreach (var session in heading.Sessions)
                        {
                            if (session.Selected)
                                selectedSessions.Add(session);
                        }
                    }
                }

                foreach (var session in selectedSessions)
                {
                    var registrationDetail =
                        (RegistrationDetail)DataUtils.InstantiateFacadeObject("EventsRegistrantSession");
                    registrationDetail.SetValue("rgs_ses_key", session.Key.ToString());
                    registrationDetail.SetValue("rgs_prc_key", session.Fee.PriceKey.ToString());
                    registrationDetail.SetValue("rgs_add_user", pendingRegistration.Customer.WebLogin);

                    var sessionInvoiceDetail =
                        (InvoiceDetail)DataUtils.InstantiateFacadeObject("AdditionalInvoiceDetail");
                    sessionInvoiceDetail.SetDefaults();
                    sessionInvoiceDetail.SetValue("inv_cst_billing_key",
                        registration.oInvoice.GetValue("inv_cst_billing_key"));
                    sessionInvoiceDetail.SetValue("ivd_prc_key", registrationDetail.GetValue("rgs_prc_key"));
                    sessionInvoiceDetail.LoadRelatedData();

                    var disableQuantity = session.Ticketed ? "0" : "1";
                    sessionInvoiceDetail.SetValue("ivd_disable_quantity", disableQuantity);
                    sessionInvoiceDetail.SetValue("ivd_qty",
                        session.SelectedQuantity > 1 ? session.SelectedQuantity.ToString() : "1");
                    sessionInvoiceDetail.SetValue("ivd_cst_ship_key", registration.GetValue("reg_cst_key"));
                    sessionInvoiceDetail.SetValue("ivd_cxa_key", registration.GetValue("reg_cxa_key"));
                    sessionInvoiceDetail.SetValue("ivd_ivd_key", registration.GetValue("reg_ivd_key"));
                    registration.oInvoice.AddInvoiceDetailLine(sessionInvoiceDetail);
                    registrationDetail.SetValue("rgs_ivd_key", sessionInvoiceDetail.GetValue("ivd_key"));
                    registrationDetail.SetValue("rgs_on_wait_list", registrationDetail.GetValue("ses_wait_list_flag"));
                    registrationDetail.SetValue("rgs_qty",
                        session.SelectedQuantity > 1 ? session.SelectedQuantity.ToString() : "1");
                    registration.AddRegistrationDetails(registrationDetail);
                    registrationDetail.LoadRelatedData();
                }
            }
        }

        private void AddCardPaymentToRegistration(Registration registrant, RegistrationDto pendingRegistration)
        {
            var card = pendingRegistration.Payment.ToCreditCard();
            var paymentMethod = PaymentMethodDao.GetByMethod(card.GetCardType());
            registrant.oInvoice.SetValue("pin_apm_key", paymentMethod.Key.ToString());
            registrant.oInvoice.SetValue("pin_cc_number", card.GetDecryptedCardNumber());
            registrant.oInvoice.SetValue("pin_cc_expire", $"20{card.ExpirationDate}");
            registrant.oInvoice.SetValue("pin_check_amount", registrant.GetValue("inv_balance"));
        }

        private void AddInvoiceDetailToRegistration(Registration registrant)
        {
            var invoiceDetail = (InvoiceDetail)DataUtils.InstantiateFacadeObject("AdditionalInvoiceDetail");

            if (invoiceDetail == null)
                throw new Exception("Couldn't create an AdditionalInvoiceDetail facade object.");

            invoiceDetail.LoadRelatedData();
            invoiceDetail.SetValue("ivd_formkey", registrant.GetValue("reg_formkey"));
            invoiceDetail.SetValue("ivd_prc_key", registrant.GetValue("prc_key"));
            invoiceDetail.SetValue("ivd_disable_quantity", "1");
            invoiceDetail.SetDefaults();
            invoiceDetail.SetValue("ivd_reg_key", registrant.GetValue("reg_key"));
            invoiceDetail.SetValue("ivd_cst_key", registrant.GetValue("reg_cst_key"));
            invoiceDetail.LoadRelatedData();
            invoiceDetail.SetValue("ivd_cst_ship_key", registrant.GetValue("reg_cst_key"));
            invoiceDetail.LoadRelatedData();
            invoiceDetail.SetValue("ivd_cxa_key", registrant.GetValue("reg_cxa_key"));

            registrant.oInvoice.AddInvoiceDetailLine(invoiceDetail);
            registrant.SetValue("reg_ivd_key", invoiceDetail.GetValue("ivd_key"));
            registrant.SetValue("inv_orig_trans_type", "prepaid");
            registrant.LoadRelatedData();
        }

        private void AddDiscountsToRegistration(Registration registrant, RegistrationDto pendingRegistration)
        {
            if (pendingRegistration.SelectedDiscountPriceKey.HasValue)
            {
                var invoiceDetail = new InvoiceDetail();
                invoiceDetail.SetValue("ivd_key", Guid.NewGuid().ToString());
                invoiceDetail.SetValue("ivd_prc_key", pendingRegistration.SelectedDiscountPriceKey.Value.ToString());
                invoiceDetail.SetValue("ivd_type", "Discount");
                invoiceDetail.SetValue("ivd_parity", "-1");
                invoiceDetail.SetValue("ivd_qty", "1");
                invoiceDetail.LoadRelatedData();

                registrant.oInvoice.AdditionalInvoiceDetails.Add(invoiceDetail);
                registrant.oInvoice.ProcessRoundTripEvents(null, null);
            }
        }

        private void SubmitRegistration(DbTransaction transaction, Registration registration)
        {
            registration.bCreateInvoice = true;
            var error = registration.Insert();

            if (error.Number == (int)ErrorClass.ErrorNumber.NoError)
            {
                transaction.Commit();
            }
            else
            {
                transaction.Rollback();

                if (!string.IsNullOrEmpty(error.Message))
                    Log.Error(error.Message);

                throw new RegistrationProcessingException(error.Message);
            }
        }

        private void StoreEmergencyInformation(Guid registrationKey, string emergencyContactName, string emergencyContactPhone)
        {
            if (registrationKey != Guid.Empty &&
                (!string.IsNullOrEmpty(emergencyContactName) || !string.IsNullOrEmpty(emergencyContactPhone)))
            {
                var contactInfo = new RegistrationContactDto
                {
                    ContactName = emergencyContactName,
                    ContactPhone = emergencyContactPhone
                };

                var result = HttpClientHelper.PostJson<bool>(eventService, $"admin/registration/{registrationKey}/update-contact-info", contactInfo);

                if (result.StatusCode != HttpStatusCode.OK)
                    Log.Error($"Unable to save contact info for registration: {registrationKey} contactName: {emergencyContactName} contactPhone: {emergencyContactPhone}");
            }
        }

        private void StoreBadgeNotes(Guid registrationKey, string badgeNotes)
        {
            if (registrationKey != Guid.Empty && !string.IsNullOrEmpty(badgeNotes))
            {
                var badgeInfo = new RegistrationBadgeDto
                {
                    Notes = badgeNotes
                };

                var result = HttpClientHelper.PostJson<bool>(eventService, $"admin/registration/{registrationKey}/update-badge-notes", badgeInfo);

                if (result.StatusCode != HttpStatusCode.OK || !result.Data)
                    Log.Error($"Unable to save badge notes for registration: {registrationKey} badgeNotes: {badgeNotes}");
            }
        }

        private static object GetNetForumEditedRegistration(EditedRegistrationDto editedRegistration)
        {
            var netForumEditedRegistration = new NetForumEditedRegistrationDto();

            netForumEditedRegistration.reg_key = Convert.ToString(editedRegistration.RegistrationKey);
            netForumEditedRegistration.bat_key = Convert.ToString(editedRegistration.BatchKey);

            netForumEditedRegistration.ait_key = "1B021123-C294-4F0D-9985-0860E36C3911";

            netForumEditedRegistration.Sessions = new List<NetForumEditedRegistrationSessionDto>();
            var netForumEditedRegistrationSession = new NetForumEditedRegistrationSessionDto();

            foreach (var editedSession in editedRegistration.Sessions)
            {
                netForumEditedRegistration.Sessions.Add(new NetForumEditedRegistrationSessionDto
                {
                    rgs_key = Convert.ToString(editedSession.Key),
                    ses_key = Convert.ToString(editedSession.SessionKey),
                    ivd_qty = Convert.ToString(editedSession.SelectedQuantity),
                    prc_key = Convert.ToString(editedSession.PriceKey)
                });
            }

            if (editedRegistration.Payment != null)
            {
                netForumEditedRegistration.Payment = new NetForumEditedRegistrationPaymentDto();

                netForumEditedRegistration.Payment.pin_cst_key = Convert.ToString(editedRegistration.Payment.CustomerKey);
                netForumEditedRegistration.Payment.pin_apm_key = Convert.ToString(editedRegistration.Payment.Key);
                netForumEditedRegistration.Payment.pin_cc_cardholder_name = editedRegistration.Payment.CardholderName;
                netForumEditedRegistration.Payment.pin_cc_number = editedRegistration.Payment.CreditCardNumber;
                netForumEditedRegistration.Payment.pin_cc_expire = editedRegistration.Payment.ExpirationDateDisplay;
                netForumEditedRegistration.Payment.pin_cc_security_code = editedRegistration.Payment.VerificationCode;
                netForumEditedRegistration.Payment.pin_zip = editedRegistration.Payment.BillingZip;
                netForumEditedRegistration.Payment.pin_check_amount =
                    Convert.ToString(editedRegistration.Payment.TotalDue, CultureInfo.CurrentCulture);
            }

            return netForumEditedRegistration;
        }

        private Guid? CreateBatch()
        {
            var batchCode = string.Format("{0}-WEB-001", DateTime.Now.ToString("yyyy-MM-dd"));
            var batchKey = string.Empty;
            var arp_key = string.Empty;
            var arp_afy_key = string.Empty;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    var command =
                        new SqlCommand(
                            "SELECT bat_key FROM ac_batch WHERE bat_delete_flag = 0 AND bat_code = @BatchCode and bat_close_flag = 0", connection, transaction);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@BatchCode", batchCode);
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        batchKey = reader["bat_key"].ToString();
                    }

                    reader.Close();
                    transaction.Commit();
                }
            }

            if (string.IsNullOrEmpty(batchKey))
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        var command = new SqlCommand("client_aafp_v_get_ar_keys_for_batch", connection, transaction);
                        command.CommandType = CommandType.StoredProcedure;
                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            arp_key = reader["arp_key"].ToString();
                            arp_afy_key = reader["arp_afy_key"].ToString();
                        }

                        reader.Close();
                        transaction.Commit();
                    }
                }

                if (!string.IsNullOrWhiteSpace(arp_key) || !string.IsNullOrWhiteSpace(arp_afy_key))
                {
                    using (var connection = DataUtils.GetConnection())
                    {
                        if (connection.State != ConnectionState.Open)
                            connection.Open();

                        using (var transaction = connection.BeginTransaction())
                        {
                            Config.SystemOptions = new Hashtable();
                            SystemFunctions.LoadSystemOptions();
                            Config.SuperUser = true;
                            Config.CurrentUserName = "WebUpdate";

                            var batch = FacadeObjectFactory.CreateBatch();
                            batch.SetValue("bat_code", batchCode);
                            batch.SetValue("bat_arp_key", arp_key);
                            batch.SetValue("arp_afy_key", arp_afy_key);
                            batch.SetValue("bat_date", DateTime.Now.ToShortDateString());
                            batch.LoadRelatedData(connection, transaction);

                            batch.Insert(connection, transaction);
                            transaction.Commit();
                            batchKey = batch.GetValue("bat_key");
                        }
                    }
                }
            }

            if (string.IsNullOrEmpty(batchKey))
                return null;
            else
                return new Guid(batchKey);
        }
    }
}