using Aafp.Also.Api.Daos.Queries.Interfaces;
using Aafp.Also.Api.Dtos;
using Aafp.Also.Api.Tasks.Interfaces;
using Aafp.WebApi.Components;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Linq;

namespace Aafp.Also.Api.Tests.Tasks
{
    [TestFixture]
    [Category("Service")]
    public class ActivityTasksTest : AbstractTestFixture
    {
        private IActivityTasks alsoActivityTasks;

        private IndividualDto individual;

        private string webLogin = "8108065";

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            AutomapperConfig.Configure();
            alsoActivityTasks = StructureMapConfig.Resolve<IActivityTasks>();
            alsoActivityTasks.ActivityQuery = StructureMapConfig.Resolve<IActivityQuery>();
            alsoActivityTasks.IndividualTasks = Substitute.For<IIndividualTasks>();
        }

        [TearDown]
        public override void TearDown()
        {
            alsoActivityTasks = null;
            base.TearDown();
        }

        [Test]
        public async void TestGetAllByLearner()
        {
            CreateTestHelpers();
            alsoActivityTasks.IndividualTasks.GetIndividualByWebLogin(Arg.Any<string>()).Returns(individual);
            
            var activities = await alsoActivityTasks.GetActivitiesForCourseList(webLogin);

            Assert.IsTrue(activities.Any());
        }

        private void CreateTestHelpers()
        {
            individual = new IndividualDto
            {
                Key = new Guid("D38F3C54-6F87-4084-9FFB-D2D65D685961")
            };
        }
    }
}