using System;
using System.Linq;
using Aafp.Events.Api.Dao.Interfaces;
using Aafp.Events.Api.Dtos.User.Registration;
using Aafp.Events.Api.Tasks.User.Interfaces;
using Aafp.WebApi.Components;
using NUnit.Framework;

namespace Aafp.Events.Api.Tests.Tasks.User
{
    [TestFixture]
    [Category("Service")]
    public class UserRegistrationTasksTests : AbstractTestFixture
    {
        private IUserRegistrationTasks userRegistrationTasks;

        private IEventDao eventDao;

        private IRegistrantDao registrantDao;

        private Guid customerKey = new Guid("D38F3C54-6F87-4084-9FFB-D2D65D685961");

        private string webLogin = "9071109";

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            userRegistrationTasks = StructureMapConfig.Resolve<IUserRegistrationTasks>();
            userRegistrationTasks.EventDao = StructureMapConfig.Resolve<IEventDao>();
            userRegistrationTasks.RegistrantDao = StructureMapConfig.Resolve<IRegistrantDao>();

            eventDao = StructureMapConfig.Resolve<IEventDao>();
            registrantDao = StructureMapConfig.Resolve<IRegistrantDao>();
        }

        [TearDown]
        public override void TearDown()
        {
            userRegistrationTasks = null;
            eventDao = null;
            registrantDao = null;

            base.TearDown();
        }

        [Test]
        public void TestGetNewRegistrationIntro()
        {
            using (var session = Session.BeginTransaction())
            {
                var evt = eventDao.GetActiveEvents().First();
                var intro = userRegistrationTasks.GetNewRegistrationIntro(evt.Code, webLogin);

                Assert.IsNotNull(intro);
                Assert.IsTrue(intro.Event.Fees.Count > 0 && intro.Event.Fees.Count <= 3);

                session.Commit();
            }
        }

        [Test]
        public void TestSaveRegistrationIntro()
        {
            var introTest = new UserRegistrationIntroDto();
            var testGuid = Guid.Empty;

            using (var session = Session.BeginTransaction())
            {
                var evt = eventDao.GetActiveEvents().First();
                introTest = userRegistrationTasks.GetNewRegistrationIntro(evt.Code, webLogin);
                introTest.SelectedPriceKey = introTest.Event.Fees[0].PriceKey;
                testGuid = userRegistrationTasks.SaveRegistrationIntro(introTest);

                Assert.IsTrue(testGuid != Guid.Empty);

                session.Commit();
            }

            Session.Clear();

            using (var session = Session.BeginTransaction())
            {
                var intro = userRegistrationTasks.GetRegistrationIntro(testGuid);

                Assert.IsTrue(intro.Event.Key == introTest.Event.Key);
                Assert.IsTrue(intro.SelectedPriceKey == introTest.SelectedPriceKey);

                session.Commit();
            }
        }

        [Test]
        public void TestRegistrationContactInfo()
        {
            var introTest = new UserRegistrationIntroDto();
            var contactTest = new UserRegistrationContactInfoDto();
            var testGuid = Guid.Empty;

            using (var session = Session.BeginTransaction())
            {
                var evt = eventDao.GetActiveEvents().First();
                introTest = userRegistrationTasks.GetNewRegistrationIntro(evt.Code, webLogin);
                introTest.SelectedPriceKey = introTest.Event.Fees[0].PriceKey;
                testGuid = userRegistrationTasks.SaveRegistrationIntro(introTest);

                Assert.IsTrue(testGuid != Guid.Empty);

                session.Commit();
            }

            Session.Clear();

            using (var session = Session.BeginTransaction())
            {
                contactTest = userRegistrationTasks.GetRegistrationContactInfo(testGuid);

                Assert.IsNotNull(contactTest);
                Assert.IsNotNull(contactTest.Customer);
                Assert.IsNotNull(contactTest.Badge);
                Assert.IsTrue(contactTest.Badge.States.Count > 0);

                contactTest.SelectedAddressKey = contactTest.Customer.Addresses[0].Key;
                contactTest.SelectedPhoneKey = contactTest.Customer.Phones[0].Key;
                contactTest.EmergencyContactName = "Test User";
                contactTest.EmergencyContactPhone = "(555) 555-5555";
                contactTest.Badge.NickName = "Tester";

                userRegistrationTasks.SaveRegistrationContactInfo(contactTest);

                session.Commit();
            }

            Session.Clear();

            using (var session = Session.BeginTransaction())
            {
                var contactInfo = userRegistrationTasks.GetRegistrationContactInfo(testGuid);

                Assert.IsNotNull(contactInfo);
                Assert.IsTrue(contactInfo.SelectedAddressKey == contactTest.SelectedAddressKey);
                Assert.IsTrue(contactInfo.SelectedPhoneKey == contactTest.SelectedPhoneKey);
                Assert.IsTrue(contactInfo.EmergencyContactName == contactTest.EmergencyContactName);
                Assert.IsTrue(contactInfo.EmergencyContactPhone == contactTest.EmergencyContactPhone);

                session.Commit();
            }
        }

        [Test]
        public void TestRegistrationStepsAndSessions()
        {
            var introTest = new UserRegistrationIntroDto();
            var stepTest = new UserRegistrationStepDto();
            var testGuid = Guid.Empty;

            using (var session = Session.BeginTransaction())
            {
                var evt = eventDao.GetActiveEvents().First();
                introTest = userRegistrationTasks.GetNewRegistrationIntro(evt.Code, webLogin);
                introTest.SelectedPriceKey = introTest.Event.Fees[0].PriceKey;
                testGuid = userRegistrationTasks.SaveRegistrationIntro(introTest);

                Assert.IsTrue(testGuid != Guid.Empty);

                session.Commit();
            }

            Session.Clear();

            using (var session = Session.BeginTransaction())
            {
                stepTest = userRegistrationTasks.GetRegistrationStep(testGuid, introTest.Event.Steps[0].Key);

                Assert.IsTrue(stepTest.Headings.Count > 0);
                Assert.IsTrue(stepTest.Headings.Any(x => x.Sessions.Count > 0));

                stepTest.Headings.First(x => x.Sessions.Count > 0).Sessions[0].Selected = true;
                stepTest.RegistrationKey = testGuid;

                userRegistrationTasks.SaveRegistrationSessions(stepTest);

                session.Commit();
            }

            Session.Clear();

            using (var session = Session.BeginTransaction())
            {
                var step = userRegistrationTasks.GetRegistrationStep(testGuid, stepTest.Key);

                Assert.IsTrue(step.Headings.First(x => x.Sessions.Count > 0).Sessions[0].Selected);

                session.Commit();
            }
        }

        [Test]
        public void TestGetRegistrationConfirmation()
        {
            using (var session = Session.BeginTransaction())
            {
                var evt = eventDao.GetActiveEvents().First();
                var registrants = registrantDao.GetRegistrantsForEvent(evt.Key);
                var confirmation = userRegistrationTasks.GetRegistrationConfirmation(registrants[0].Key, "view");

                Assert.IsNotNull(confirmation);
                Assert.IsTrue(!string.IsNullOrWhiteSpace(confirmation.InvoiceCode));
                Assert.IsNotNull(confirmation.Fee);

                session.Commit();
            }
        }
    }
}
