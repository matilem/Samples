using System;
using System.Linq;
using Aafp.Cme.Api.Dtos;
using Aafp.Cme.Api.Tasks.Interfaces;
using Aafp.WebApi.Components;
using NSubstitute;
using NUnit.Framework;

namespace Aafp.Cme.Api.Tests.Tasks
{
    [TestFixture]
    [Category("Integration")]
    public class CmeActivityTasksTest : AbstractTestFixture
    {
        private ICmeActivityTasks activityTasks;

        private IndividualDto individual;

        private int activityNumber = 72364;

        private string webLogin = "9071109";

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            activityTasks = StructureMapConfig.Resolve<ICmeActivityTasks>();
            activityTasks.CreditTasks = StructureMapConfig.Resolve<ICreditTasks>();
            activityTasks.IndividualTasks = Substitute.For<IIndividualTasks>();
        }

        [TearDown]
        public override void TearDown()
        {
            activityTasks = null;
            base.TearDown();
        }

        [Test]
        public async void TestGetCmeSessionsByActivity()
        {
            CreateTestHelpers();
            activityTasks.IndividualTasks.GetIndividualByWebLogin(Arg.Any<string>()).Returns(individual);

            var results = await activityTasks.GetCmeSessionsByActivity(activityNumber, webLogin);

            Assert.IsTrue(!string.IsNullOrWhiteSpace(results.ActivityTitle));
            Assert.IsTrue(results.Sessions.Any());
        }

        private void CreateTestHelpers()
        {
            individual = new IndividualDto
            {
                Key = new Guid("D918E875-CD32-4568-B487-ED12C09FADA1")
            };
        }
    }
}
