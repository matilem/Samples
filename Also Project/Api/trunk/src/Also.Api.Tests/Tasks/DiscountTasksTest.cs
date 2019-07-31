using Aafp.Also.Api.Dtos;
using Aafp.Also.Api.Tasks.Interfaces;
using Aafp.WebApi.Components;
using NUnit.Framework;
using System;
using System.Linq;

namespace Aafp.Also.Api.Tests.Tasks
{
    [TestFixture]
    [Category("Service")]
    public class DiscountTasksTest : AbstractTestFixture
    {
        private IDiscountTasks discountTasks;

        private IActivityTasks activityTasks;

        private IndividualDto individual;

        private ActivityDto activity;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            AutomapperConfig.Configure();
            discountTasks = StructureMapConfig.Resolve<IDiscountTasks>();
            activityTasks = StructureMapConfig.Resolve<IActivityTasks>();
        }

        [TearDown]
        public override void TearDown()
        {
            discountTasks = null;
            base.TearDown();
        }

        [Test]
        public void TestCreateDiscount()
        {
            CreateTestHelpers();
            CreateTestData();

            var dto = new DiscountDto
            {
                ActivityNumber = activity.ActivityNumber,
                WebLogin = individual.WebLogin,
                ActivityStartDate = activity.ActivityBeginDate,
                ActivityEndDate = activity.ActivityEndDate
            };

            Guid discount = discountTasks.CreateDiscount(dto);

            Assert.IsTrue(discount != Guid.Empty);
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
            var staffActivity = activityTasks.ActivityQuery.GetAlsoActivitiesForStaff().LastOrDefault();

            activity = new ActivityDto
            {
                ActivityKey = staffActivity.ActivityKey,
                ActivityNumber = staffActivity.ActivityNumber,
                ActivityBeginDate = staffActivity.ActivityBeginDate,
                ActivityEndDate = staffActivity.ActivityEndDate
            };
        }
    }
}