using System;
using Aafp.Cme.Api.Dtos;
using Aafp.Cme.Api.Tasks.Interfaces;
using Aafp.WebApi.Components;
using NSubstitute;
using NUnit.Framework;

namespace Aafp.Cme.Api.Tests.Tasks
{
    [TestFixture]
    [Category("Integration")]
    public class CmeStatsTasksTest : AbstractTestFixture
    {
        private ICmeStatsTasks cmeStatsTasks;

        private CreditAvailableStatsDto creditAvailableStatsDto;

        private IndividualDto individual;

        private ReElectionDto reElectionMessageDto;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            AutomapperConfig.Configure();
            cmeStatsTasks = StructureMapConfig.Resolve<ICmeStatsTasks>();

            cmeStatsTasks.CreditAvailableTasks = Substitute.For<ICreditAvailableTasks>();
            cmeStatsTasks.ReElectionTasks = Substitute.For<IReElectionTasks>();
            cmeStatsTasks.CreditAvailableTasks.IndividualTasks = Substitute.For<IIndividualTasks>();
        }

        [TearDown]
        public override void TearDown()
        {
            cmeStatsTasks = null;
            base.TearDown();
        }

        [Test]
        public async void GetCmeStatsHtmlTest()
        {
            CreateTestHelpers();
            cmeStatsTasks.CreditAvailableTasks.IndividualTasks.GetIndividualByWebLogin(Arg.Any<string>()).Returns(individual);
            cmeStatsTasks.CreditAvailableTasks.GetCreditTotals(Arg.Any<string>()).Returns(creditAvailableStatsDto);
            cmeStatsTasks.ReElectionTasks.GetReElectionByWebLogin(Arg.Any<string>()).Returns(reElectionMessageDto);

            var html = await cmeStatsTasks.GetCmeStatsHtml("9071109");

            Assert.IsTrue(!string.IsNullOrWhiteSpace(html));
        }

        [Test]
        public void GetCmeStatsHtmlForUnauthenticatedUserTest()
        {
            var html = cmeStatsTasks.GetCmeStatsHtmlForUnauthenticatedUser();

            Assert.IsTrue(!string.IsNullOrWhiteSpace(html));
        }

        private void CreateTestHelpers()
        {
            individual = new IndividualDto
            {
                Key = new Guid("D918E875-CD32-4568-B487-ED12C09FADA1")
            };

            reElectionMessageDto = new ReElectionDto
            {
                IsMember = true,
                Message = "Good Standing",
                Status = "Good"
            };

            creditAvailableStatsDto = new CreditAvailableStatsDto
            {
                CreditsExpiring = 10.00m,
                CreditsPurchased = 20.00m,
                QuizzesAvailable = 30.00m
            };
        }
    }
}
