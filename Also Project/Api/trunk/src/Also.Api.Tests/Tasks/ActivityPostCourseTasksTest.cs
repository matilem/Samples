using Aafp.Also.Api.Daos.Queries.Interfaces;
using Aafp.Also.Api.Dtos;
using Aafp.Also.Api.Tasks.Interfaces;
using Aafp.WebApi.Components;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aafp.Also.Api.Tests.Tasks
{
    [TestFixture]
    [Category("Service")]
    public class ActivityPostCourseTasksTest : AbstractTestFixture
    {
        private IActivityPostCourseTasks postCourseTasks;

        private IActivityTasks activityTasks;

        private IndividualDto individual;

        private ActivityDto activity;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            AutomapperConfig.Configure();
            postCourseTasks = StructureMapConfig.Resolve<IActivityPostCourseTasks>();
            postCourseTasks.ActivityPostCourseQuery = StructureMapConfig.Resolve<IActivityPostCourseQuery>();
            postCourseTasks.IndividualTasks = Substitute.For<IIndividualTasks>();
            activityTasks = StructureMapConfig.Resolve<IActivityTasks>();
        }

        [TearDown]
        public override void TearDown()
        {
            postCourseTasks = null;
            base.TearDown();
        }

        [Test]
        public async void TestGetActivityPostCourse()
        {
            CreateTestHelpers();
            CreateTestData();
            postCourseTasks.IndividualTasks.GetIndividualByWebLogin(Arg.Any<string>()).Returns(individual);

            var postcourse = await postCourseTasks.GetPostCourse(activity.ActivityNumber, individual.CustomerId);

            Assert.IsTrue(postcourse.Activity.ActivityNumber == activity.ActivityNumber);
        }

        [Test]
        public async void TestSaveActivityPostCourse()
        {
            CreateTestHelpers();
            CreateTestData();

            var dto = new ActivityPostCourseSubmissionDto
            {
                ActivityKey = activity.ActivityKey,
                ActivityNumber = activity.ActivityNumber,
                ActivityCourseType = "ALSO Provider",
                AlsoCourseKey = new Guid("68231A1A-9A1F-4868-93F9-74702557545A"),
                Learners = new List<LearnerSubmissionDto>(),
                WebLogin = individual.WebLogin,
                Status = "Save"
            };

            var learner = new LearnerSubmissionDto
            {
                CustomerKey = individual.Key,
                OccupationKey = new Guid("55E375E8-647A-48C6-A1B9-407CC2D81485"),
                Grade = "Pass"
            };

            dto.Learners.Add(learner);

            var success = await postCourseTasks.SavePostCourse(dto);

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