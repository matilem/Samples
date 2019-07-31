using System;
using Aafp.Events.Api.Dao.Interfaces;
using Aafp.Events.Api.Tasks.Admin.Interfaces;
using Aafp.WebApi.Components;
using NUnit.Framework;

namespace Aafp.Events.Api.Tests.Tasks
{
    [TestFixture]
    [Category("Database")]
    public class SessionTasksTests : AbstractTestFixture
    {
        private IAdminSessionTasks adminSessionTasks;

        private ISessionDao sessionDao;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            adminSessionTasks = StructureMapConfig.Resolve<IAdminSessionTasks>();
            adminSessionTasks.SessionDao = StructureMapConfig.Resolve<ISessionDao>();

            sessionDao = StructureMapConfig.Resolve<ISessionDao>();
        }

        [TearDown]
        public override void TearDown()
        {
            adminSessionTasks = null;
            sessionDao = null;

            base.TearDown();
        }

        [Test]
        public void TestUpdateSessionCapacity()
        {
            var capacity = 0;

            using (var transaction = Session.BeginTransaction())
            {
                var session = sessionDao.GetByKey(new Guid("C35C0232-4CB9-4DA4-ADF1-00512FDFBCD3"));
                capacity = session.Capacity;
                adminSessionTasks.IncreaseSessionCapacity(session.Key);

                transaction.Commit();
            }
               
            Session.Clear();

            using (var transaction = Session.BeginTransaction())
            {
                var session = sessionDao.GetByKey(new Guid("C35C0232-4CB9-4DA4-ADF1-00512FDFBCD3"));
                
                Assert.IsTrue(session.Capacity == (capacity + 1));

                transaction.Commit();
            }
        }
    }
}
