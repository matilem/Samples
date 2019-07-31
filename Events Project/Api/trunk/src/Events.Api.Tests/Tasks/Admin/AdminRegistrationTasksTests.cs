using System;
using System.Linq;
using Aafp.Events.Api.Dao;
using Aafp.Events.Api.Dao.Interfaces;
using Aafp.Events.Api.Dtos;
using Aafp.Events.Api.Models;
using Aafp.Events.Api.Tasks.Admin.Interfaces;
using Aafp.WebApi.Components;
using NUnit.Framework;

namespace Aafp.Events.Api.Tests.Tasks.Admin
{
    [TestFixture]
    [Category("Service")]
    public class AdminRegistrationTasksTests : AbstractTestFixture
    {
        private IAdminRegistrationTasks adminRegistrationTasks;

        private IEventDao eventDao;

        private IFeeDao feeDao;

        private IPendingRegistrationDao pendingRegistrationDao;

        private IRegistrantDao registrantDao;

        private Guid customerKey = new Guid("D38F3C54-6F87-4084-9FFB-D2D65D685961");

        private string webLogin = "9071109";

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            adminRegistrationTasks = StructureMapConfig.Resolve<IAdminRegistrationTasks>();
            adminRegistrationTasks.EventDao = StructureMapConfig.Resolve<IEventDao>();
            adminRegistrationTasks.PendingRegistrationDao = StructureMapConfig.Resolve<IPendingRegistrationDao>();
            adminRegistrationTasks.RegistrantDao = StructureMapConfig.Resolve<IRegistrantDao>();

            eventDao = StructureMapConfig.Resolve<IEventDao>();
            feeDao = StructureMapConfig.Resolve<IFeeDao>();
            pendingRegistrationDao = StructureMapConfig.Resolve<IPendingRegistrationDao>();
            registrantDao = StructureMapConfig.Resolve<IRegistrantDao>();
        }

        [TearDown]
        public override void TearDown()
        {
            adminRegistrationTasks = null;
            eventDao = null;
            feeDao = null;
            pendingRegistrationDao = null;
            registrantDao = null;

            base.TearDown();
        }

        [Test]
        public void TestGetAdminRegistrationEvents()
        {
            using (var session = Session.BeginTransaction())
            {
                var events = adminRegistrationTasks.GetAdminRegistrationEvents();

                Assert.IsTrue(events.Any());
                Assert.IsTrue(events.Exists(x => x.StartDate.Value >= DateTime.Today.AddMonths(-12)));
                Assert.IsFalse(events.Exists(x => x.StartDate.Value < DateTime.Today.AddMonths(-12)));

                session.Commit();
            }
        }

        [Test]
        public void TestGetEventRegistrationTypesByCustomerKey()
        {
            using (var session = Session.BeginTransaction())
            {
                var eventItem = eventDao.GetActiveEvents().First();
                var eventDetail = adminRegistrationTasks.GetEventRegistrationTypesByCustomerKey(eventItem.Key, customerKey, eventItem.StartDate.Value);

                Assert.IsTrue(eventDetail != null);
                Assert.IsTrue(eventDetail.Fees.Count > 0);

                session.Commit();
            }
        }

        [Test]
        public void TestGetEventRegistrationTypesByWebLogin()
        {
            using (var session = Session.BeginTransaction())
            {
                var eventItem = eventDao.GetActiveEvents().First();
                var eventDetail = adminRegistrationTasks.GetEventRegistrationTypesByWebLogin(eventItem.Key, webLogin, eventItem.StartDate.Value);

                Assert.IsTrue(eventDetail != null);
                Assert.IsTrue(eventDetail.Fees.Count > 0);

                session.Commit();
            }
        }

        [Test]
        public void TestGetSearchResults()
        {
            using (var session = Session.BeginTransaction())
            {
                var results = adminRegistrationTasks.GetAdminCustomerSearchResults("jason");

                Assert.IsTrue(results.Any());
                Assert.IsTrue(results.Count == 50);

                session.Commit();
            }

            using (var session = Session.BeginTransaction())
            {
                var results = adminRegistrationTasks.GetAdminCustomerSearchResults("douglas henley");

                Assert.IsTrue(results.Any());
                Assert.IsTrue(results.Any(x => x.Events.Any()));

                session.Commit();
            }
        }

        [Test]
        public void TestGetNewRegistration()
        {
            using (var session = Session.BeginTransaction())
            {
                var eventItem = eventDao.GetActiveEvents().First();
                var fee = feeDao.GetEventFeesByCustomerForAdmin(eventItem.Key, customerKey, eventItem.StartDate.Value);
                var registration = adminRegistrationTasks.GetNewRegistration(eventItem.Key, customerKey, fee[0].PriceKey, eventItem.StartDate.Value);

                Assert.IsTrue(registration != null);
                Assert.IsTrue(registration.Event.Fees.Count > 0);
                Assert.IsTrue(registration.Event.Steps.Count > 0);
                Assert.IsTrue(registration.Customer != null);

                session.Commit();
            }
        }

        [Test]
        public void TestSavePendingRegistration()
        {
            var key = Guid.Empty;
            var eventKey = Guid.Empty;

            using (var session = Session.BeginTransaction())
            {
                var eventItem = eventDao.GetActiveEvents().First();
                eventKey = eventItem.Key;
                var fee = feeDao.GetEventFeesByCustomerForAdmin(eventKey, customerKey, eventItem.StartDate.Value);
                customerKey = new Guid("EB458D80-4C43-4F3B-90E0-28227B339B7B");
                var registration = adminRegistrationTasks.GetNewRegistration(eventKey, customerKey, fee[0].PriceKey, eventItem.StartDate.Value);
                registration.CurrentUser = "WebUpdate";
                registration.Event.Steps[0].Headings[0].Sessions[0].Selected = true;
                registration.Badge.NickName = "Speedy";

                var saveRegistration = adminRegistrationTasks.SavePendingRegistration(registration);
                key = saveRegistration.Key;

                Assert.IsTrue(saveRegistration != null);

                session.Commit();
            }

            Session.Clear();

            using (var session = Session.BeginTransaction())
            {
                var registration = adminRegistrationTasks.GetRegistrationFromPendingRegistration(key);

                Assert.IsTrue(registration != null);
                Assert.IsTrue(registration.Event.Key == eventKey);
                Assert.IsTrue(registration.Customer.Key == customerKey);
                Assert.IsTrue(registration.Event.Fees.Count > 0);
                Assert.IsTrue(registration.Event.Steps.Count > 0);
                Assert.IsTrue(registration.Event.Steps[0].Headings[0].Sessions[0].Selected);
                Assert.IsTrue(registration.Badge.NickName == "Speedy");

                session.Commit();
            }
        }

        [Test]
        public void TestMarkPendingRegistrationAsProcessed()
        {
            var key = Guid.Empty;
            var eventKey = Guid.Empty;

            using (var session = Session.BeginTransaction())
            {
                var eventItem = eventDao.GetActiveEvents().First();
                eventKey = eventItem.Key;
                customerKey = new Guid("EB458D80-4C43-4F3B-90E0-28227B339B7B");
                var fee = feeDao.GetEventFeesByCustomerForAdmin(eventKey, customerKey, eventItem.StartDate.Value);
                var registration = adminRegistrationTasks.GetNewRegistration(eventKey, customerKey, fee[0].PriceKey, eventItem.StartDate.Value);
                registration.CurrentUser = "WebUpdate";
                registration.Event.Steps[0].Headings[0].Sessions[0].Selected = true;
                registration.Badge.NickName = "Speedy";

                var saveRegistration = adminRegistrationTasks.SavePendingRegistration(registration);

                Assert.IsTrue(saveRegistration != null);

                session.Commit();
            }

            Session.Clear();

            using (var session = Session.BeginTransaction())
            {
                adminRegistrationTasks.MarkPendingRegistrationAsProcessed(eventKey, customerKey);

                session.Commit();
            }

            Session.Clear();

            using (var session = Session.BeginTransaction())
            {
                try
                {
                    var pendingRegistration = pendingRegistrationDao.GetByKey(key);
                }
                catch (NullReferenceException ex)
                {
                    Assert.Pass();
                }
                finally
                {
                    session.Commit();
                }
            }
        }

        [Test]
        public void TestGetRegistration()
        {
            using (var session = Session.BeginTransaction())
            {
                var eventItem = eventDao.GetActiveEvents().First();

                var registrants = registrantDao.GetRegistrantsForEvent(eventItem.Key);
                var registration = adminRegistrationTasks.GetRegistration(registrants[0].Key);

                Assert.IsTrue(registration != null);
                Assert.IsTrue(registration.Key == registrants[0].Key);

                session.Commit();
            }
        }

        [Test]
        public void TestSaveEmergencyContactInformation()
        {
            Registrant registrant = null;

            using (var session = Session.BeginTransaction())
            {
                var eventItem = eventDao.GetActiveEvents().First();

                var registrants = registrantDao.GetRegistrantsForEvent(eventItem.Key);
                registrant = registrantDao.GetRegistrantByKey(registrants[0].Key);
                registrant.EmergencyContactName = "Barry Allen";
                registrant.EmergencyContactPhone = "555-555-5555";
                registrantDao.Store(registrant);

                session.Commit();
            }
            
            Session.Clear();

            using (var session = Session.BeginTransaction())
            {
                var testRegistrant = registrantDao.GetRegistrantByKey(registrant.Key);
                
                Assert.IsTrue(testRegistrant.EmergencyContactName == registrant.EmergencyContactName);
                Assert.IsTrue(testRegistrant.EmergencyContactPhone == registrant.EmergencyContactPhone);
                
                session.Commit();
            }
        }

		[Test]
        public void TestAddToWaitList()
        {
            var waitListdto = new WaitListDto();
            waitListdto.CustomerKey = customerKey;

            using (var session = Session.BeginTransaction())
            {
                var evt = eventDao.GetActiveEvents().OrderByDescending(x => x.AllowWaitList).First();
                
                waitListdto.EventKey = evt.Key;
                var success = adminRegistrationTasks.AddToWaitList(waitListdto);

                Assert.IsTrue(success);

                session.Commit();
            }

            Session.Clear();

            using (var session = Session.BeginTransaction())
            {
                var evt = adminRegistrationTasks.EventDao.GetByKey(waitListdto.EventKey);

                Assert.IsTrue(evt.RegistrantsOnWait.Any(x => x.CustomerKey == customerKey));

                session.Commit();
            }
        }
    }
}

