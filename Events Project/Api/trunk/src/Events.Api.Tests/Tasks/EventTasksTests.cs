using System;
using System.Linq;
using Aafp.Events.Api.Dao.Interfaces;
using Aafp.Events.Api.Tasks.Interfaces;
using Aafp.WebApi.Components;
using NUnit.Framework;

namespace Aafp.Events.Api.Tests.Tasks
{
    [TestFixture]
    [Category("Service")]
    public class EventTasksTests : AbstractTestFixture
    {
        private IEventTasks eventTasks;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            eventTasks = StructureMapConfig.Resolve<IEventTasks>();
            eventTasks.EventDao = StructureMapConfig.Resolve<IEventDao>();
        }

        [TearDown]
        public override void TearDown()
        {
            eventTasks = null;

            base.TearDown();
        }

        [Test]
        public void TestGetActiveEvents()
        {
            using (var session = Session.BeginTransaction())
            {
                var events = eventTasks.GetActiveEvents();

                Assert.IsTrue(events.Any());
                Assert.IsTrue(events.Exists(x => x.EndDate >= DateTime.Today));
                Assert.IsFalse(events.Exists(x => x.PostToWebDate.Value > DateTime.Today));
                Assert.IsFalse(events.Exists(x => x.RemoveFromWebDate.Value <= DateTime.Today));

                session.Commit();
            }
        }

        [Test]
        public void TestGetEventSchedule()
        {
            using (var session = Session.BeginTransaction())
            {
                var schedule = eventTasks.GetEventSchedule("fmx2016");

                Assert.IsTrue(schedule.Any());

                session.Commit();
            }
        }

        //[Test]
        //public void TestGetOrgAppEvents()
        //{
        //    ////var events = eventDao.GetOrgAppEvents();
        //    ////foreach (var e in events)
        //    ////    Assert.IsTrue(e.IsOrganizationalApplicationFlag);
        //}
    }
}
