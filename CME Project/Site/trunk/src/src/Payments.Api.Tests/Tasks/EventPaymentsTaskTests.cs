using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.SessionState;
using Aafp.Payments.Api.Dtos.Registration;
using Aafp.Payments.Api.Models;
using Aafp.Payments.Api.Tasks.Interfaces;
using Aafp.WebApi.Components;
using NUnit.Framework;

namespace Aafp.Payments.Api.Tests.Tasks
{
    [TestFixture]
    [Category("Service")]
    public class EventPaymentsTaskTests : AbstractTestFixture
    {
        private IRegistrationPaymentTasks eventPaymentTasks;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            eventPaymentTasks = StructureMapConfig.Resolve<IRegistrationPaymentTasks>();
            HttpContext.Current = FakeHttpContext("http://www.aafp.org");
        }

        [TearDown]
        public override void TearDown()
        {
            base.TearDown();
        }
        
        [Test]
        public void TestRegisterForEventWithNoSessions()
        {
            var pendingRegistration = CreatePendingRegistration();
            var registrationKey = eventPaymentTasks.Register(pendingRegistration, false);

            Assert.IsNotNull(registrationKey);
            Assert.IsTrue(registrationKey != Guid.Empty);
        }
        
        [Test]
        public void TestRegisterForEventWithOneSession()
        {
            var pendingRegistration = CreatePendingRegistration();
            pendingRegistration.Event.Steps = new List<RegistrationStepDto> { new RegistrationStepDto() };
            pendingRegistration.Event.Steps[0].Headings = new List<RegistrationHeadingDto> { new RegistrationHeadingDto() };
            pendingRegistration.Event.Steps[0].Headings[0].Sessions = new List<RegistrationSessionDto> { new RegistrationSessionDto
            {
                Key = new Guid("86003304-487f-4950-964c-16c00a866b5a"),
                Selected = true,
                Fee = new RegistrationSessionFeeDto { PriceKey = new Guid("afaa5eb1-e291-4409-aa36-fdb4923a70b7") }
            }};

            var registrationKey = eventPaymentTasks.Register(pendingRegistration, false);

            Assert.IsNotNull(registrationKey);
            Assert.IsTrue(registrationKey != Guid.Empty);
        }

        private RegistrationDto CreatePendingRegistration()
        {
            var registration = new RegistrationDto();
            registration.CurrentUser = "9071109";
            registration.RegistrationDate = DateTime.Now;
            registration.Customer = new CustomerDto { Key = new Guid("d9c9ef74-849e-4f7b-877e-0468165bbba6") };
            registration.Customer.WebLogin = "9071109";
            registration.Event = new RegistrationEventDto { Key = new Guid("e0acab3f-039e-42f8-9776-49049083fae8") };
            registration.Event.Steps = new List<RegistrationStepDto>();
            registration.PriceKey = new Guid("7014c4e5-f4d8-43da-a1d0-1b587a211f13");
            registration.Payment = CreatePaymentInfo();

            return registration;
        }

        private static PaymentInfo CreatePaymentInfo()
        {
            var paymentInfo = new PaymentInfo
            {
                CardholderName = "Test Test",
                CreditCardNumber = "4111111111111111",
                ExpirationMonth = 12,
                ExpirationYear = 28,
                VerificationCode = "123"
            };
            return paymentInfo;
        }

        public static HttpContext FakeHttpContext(string url)
        {
            var uri = new Uri(url);
            var httpRequest = new HttpRequest(string.Empty, uri.ToString(),
                                                uri.Query.TrimStart('?'));
            var stringWriter = new StringWriter();
            var httpResponse = new HttpResponse(stringWriter);
            var httpContext = new HttpContext(httpRequest, httpResponse);

            var sessionContainer = new HttpSessionStateContainer("id",
                                            new SessionStateItemCollection(),
                                            new HttpStaticObjectsCollection(),
                                            10, true, HttpCookieMode.AutoDetect,
                                            SessionStateMode.InProc, false);

            SessionStateUtility.AddHttpSessionStateToContext(
                                                 httpContext, sessionContainer);

            return httpContext;
        }
    }
}
