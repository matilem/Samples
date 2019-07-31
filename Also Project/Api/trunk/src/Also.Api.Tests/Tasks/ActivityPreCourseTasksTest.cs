using Aafp.Also.Api.Dtos;
using Aafp.Also.Api.Tasks.Interfaces;
using Aafp.WebApi.Components;
using Aafp.Also.Api.Daos.Queries.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Linq;

namespace Aafp.Also.Api.Tests.Tasks
{
    [TestFixture]
    [Category("Service")]
    public class ActivityPreCourseTasksTest : AbstractTestFixture
    {
        private IActivityPreCourseTasks preCourseTasks;

        private IActivityTasks activityTasks;

        private IndividualDto individual;

        private ActivityDto activity;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            AutomapperConfig.Configure();
            preCourseTasks = StructureMapConfig.Resolve<IActivityPreCourseTasks>();
            preCourseTasks.ActivityPreCourseQuery = StructureMapConfig.Resolve<IActivityPreCourseQuery>();
            preCourseTasks.IndividualTasks = Substitute.For<IIndividualTasks>();
            activityTasks = StructureMapConfig.Resolve<IActivityTasks>();
        }

        [TearDown]
        public override void TearDown()
        {
            preCourseTasks = null;
            base.TearDown();
        }

        [Test]
        public async void TestGetActivityPreCourse()
        {
            CreateTestHelpers();
            CreateTestData();
            preCourseTasks.IndividualTasks.GetIndividualByWebLogin(Arg.Any<string>()).Returns(individual);

            var precourse = await preCourseTasks.GetPreCourse(activity.ActivityNumber, individual.CustomerId);

            Assert.IsTrue(precourse.Activity.ActivityNumber == activity.ActivityNumber);
        }

        [Test]
        public async void TestSaveActivityPreCourse()
        {
            CreateTestHelpers();
            CreateTestData();

            var dto = new ActivityPreCourseSubmissionDto
            {
                ActivityKey = activity.ActivityKey,
                ActivityNumber = activity.ActivityNumber,
                ActivityCourseType = "ALSO Provider",
                WebLogin = individual.WebLogin,
                CourseCoordinatorId = "9145534",
                CourseDirectorId = "9145534",
                MilitaryBranchKey = new Guid("BA603D69-F8C0-4D5E-92D0-6697CA350742"),
                Status = "Save"
            };

            var success = await preCourseTasks.SavePreCourse(dto);

            Assert.IsTrue(success);
        }

        private void CreateTestHelpers()
        {
            individual = new IndividualDto
            {
                Key = new Guid("296D6427-528E-4E40-A850-A558F4938755"),
                CustomerId = "8108065",
                WebLogin = "8108065"
            };
        }

        private void CreateTestData()
        {
            var staffActivity = activityTasks.ActivityQuery.GetAlsoActivitiesForStaff().FirstOrDefault();

            activity = new ActivityDto
            {
                ActivityKey = staffActivity.ActivityKey,
                ActivityNumber = staffActivity.ActivityNumber,
                ActivityBeginDate = staffActivity.ActivityBeginDate
            };
        }
    }
}