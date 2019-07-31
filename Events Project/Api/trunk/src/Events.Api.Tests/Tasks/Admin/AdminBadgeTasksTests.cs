using System;
using System.Collections.Generic;
using System.Linq;
using Aafp.Events.Api.Dao.Interfaces;
using Aafp.Events.Api.Dtos;
using Aafp.Events.Api.Models;
using Aafp.Events.Api.Models.Badges;
using Aafp.Events.Api.Tasks.Admin.Interfaces;
using Aafp.Events.Api.Tasks.Interfaces;
using Aafp.WebApi.Components;
using NHibernate.Mapping;
using NUnit.Framework;

namespace Aafp.Events.Api.Tests.Tasks.Admin
{
    [TestFixture]
    [Category("Service")]
    public class AdminBadgeTasksTests : AbstractTestFixture
    {
        private IAdminBadgeTasks adminBadgeTasks;

        private IPdfTasks pdfTasks;

        private IEventDao eventDao;

        private IRegistrantDao registrantDao;

        private Registrant registrant;

        private Guid regKey = new Guid("00A70847-28DF-4157-8203-4CC1662518EA");

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            adminBadgeTasks = StructureMapConfig.Resolve<IAdminBadgeTasks>();
            adminBadgeTasks.EventDao = StructureMapConfig.Resolve<IEventDao>();
            adminBadgeTasks.GuestDao = StructureMapConfig.Resolve<IGuestDao>();
            adminBadgeTasks.RegistrantDao = StructureMapConfig.Resolve<IRegistrantDao>();
            adminBadgeTasks.RegistrantSessionDao = StructureMapConfig.Resolve<IRegistrantSessionDao>();
            adminBadgeTasks.SessionDao = StructureMapConfig.Resolve<ISessionDao>();

            eventDao = StructureMapConfig.Resolve<IEventDao>();
            registrantDao = StructureMapConfig.Resolve<IRegistrantDao>();
            pdfTasks = StructureMapConfig.Resolve<IPdfTasks>();


        }

        [TearDown]
        public override void TearDown()
        {
            adminBadgeTasks = null;
            pdfTasks = null;

            base.TearDown();
        }

        [Test]
        public void TestGetRegistrantBadge()
        {
            using (var session = Session.BeginTransaction())
            {
                var badge = adminBadgeTasks.GetRegistrantBadge(regKey);
                var pdf = pdfTasks.GetPdf(new List<BadgeBase> { badge });

                Assert.IsTrue(badge != null);
                Assert.IsTrue(!string.IsNullOrWhiteSpace(badge.MemberId));
                Assert.IsTrue(pdf != null);

                session.Commit();
            }
        }

        [Test]
        public void TestGetAllRegistrantBadges()
        {
            using (var session = Session.BeginTransaction())
            {
                var badges = adminBadgeTasks.GetAllRegistrantBadges(regKey);
                var pdf = pdfTasks.GetPdf(badges);

                Assert.IsTrue(badges != null);
                Assert.IsTrue(!string.IsNullOrWhiteSpace(badges[0].MemberId));
                Assert.IsTrue(pdf != null);

                session.Commit();
            }
        }

        [Test]
        public void TestGetRegistrantSessionBadges()
        {
            CreateTestData();

            using (var session = Session.BeginTransaction())
            {
                var badges = adminBadgeTasks.GetRegistrantSessionBadges(registrant.Key);
                var pdf = pdfTasks.GetPdf(badges);

                Assert.IsTrue(badges != null);
                Assert.IsTrue(pdf != null);

                session.Commit();
            }
        }

        [Test]
        public void TestGetRegistrantSessionBadge()
        {
            CreateTestData();

            using (var session = Session.BeginTransaction())
            {
                var badges = adminBadgeTasks.GetRegistrantSessionBadge(registrant.Sessions[0].Key);
                var pdf = pdfTasks.GetPdf(badges);

                Assert.IsTrue(badges != null);
                Assert.IsTrue(pdf != null);

                session.Commit();
            }
        }

        [Test]
        public void TestGetEventBadges()
        {
            using (var session = Session.BeginTransaction())
            {
                var evts = eventDao.GetActiveEvents();

                foreach (var e in evts)
                {
                    var registration = registrantDao.GetEventRegistrantsCount(e.Key);
                    if (registration > 0)
                    {
                        var badges = adminBadgeTasks.GetEventBadges(e.Key, e.StartDate.Value.AddMonths(-1), e.EndDate);
                        if (badges.Count > 0)
                        {
                            var pdf = pdfTasks.GetPdf(badges);

                            Assert.IsTrue(badges != null);
                            Assert.IsTrue(pdf != null);
                            break;
                        }
                    }
                }
                session.Commit();
            }
        }

        private void CreateTestData()
        {
            var evts = eventDao.GetActiveEvents();
            registrant = new Registrant();
            registrant.Sessions = new List<RegistrantSession>();

            foreach (var e in evts)
            {
                var registration = registrantDao.GetEventRegistrantsCount(e.Key);
                if (registration > 0)
                {
                    var registrants = registrantDao.GetRegistrantsForEvent(e.Key).Where(x => x.Sessions != null && x.Sessions.Count > 0);
                    if (registrants != null)
                    {
                        foreach (var reg in registrants)
                        {
                            foreach (var registrantSession in reg.Sessions)
                            {
                                var rsession = registrantSession.Session;

                                if (!registrantSession.CancelDate.HasValue && rsession.PrintTicket.HasValue && rsession.PrintTicket.Value == true)
                                {
                                    registrant.Key = reg.Key;
                                    registrant.Sessions.Add(registrantSession);
                                    break;
                                }
                            }
                            if (registrant.Key != Guid.Empty)
                            {
                                break;
                            }
                        }
                    }
                }
                if (registrant.Key != Guid.Empty)
                {
                    break;
                }
            }
        }
    }
}
