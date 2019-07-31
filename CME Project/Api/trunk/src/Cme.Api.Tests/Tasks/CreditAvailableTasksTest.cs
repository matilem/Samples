using System;
using System.Linq;
using Aafp.Cme.Api.Daos.Queries.Interfaces;
using Aafp.Cme.Api.Dtos;
using Aafp.Cme.Api.Tasks.Interfaces;
using Aafp.WebApi.Components;
using NSubstitute;
using NUnit.Framework;

namespace Aafp.Cme.Api.Tests.Tasks
{
    [TestFixture]
    [Category("Integration")]
    public class CreditAvailableTasksTest : AbstractTestFixture
    {
        private ICreditAvailableTasks creditAvailableTasks;

        private IndividualDto individual;

        private string webLogin = "9071109";

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            AutomapperConfig.Configure();
            creditAvailableTasks = StructureMapConfig.Resolve<ICreditAvailableTasks>();
            creditAvailableTasks.CreditAvailableQuery = StructureMapConfig.Resolve<ICreditAvailableQuery>();
            creditAvailableTasks.IndividualTasks = Substitute.For<IIndividualTasks>();
        }

        [TearDown]
        public override void TearDown()
        {
            creditAvailableTasks = null;
            base.TearDown();
        }

        [Test]
        public async void TestGetPurchasedByCustomer()
        {
            CreateTestHelpers();
            creditAvailableTasks.IndividualTasks.GetIndividualByWebLogin(Arg.Any<string>()).Returns(individual);

            var results = await creditAvailableTasks.GetPurchasedByCustomer(webLogin);

            Assert.IsTrue(results.Any());
            Assert.IsTrue(!string.IsNullOrWhiteSpace(results[0].Title));
            Assert.IsTrue(results[0].ProductKey != Guid.Empty);
            Assert.IsTrue(results[0].TransactionDate.HasValue);
            Assert.IsTrue(results[0].ExpirationDate.HasValue);
            Assert.IsTrue(results[0].CreditsAvailable > 0);
            Assert.IsTrue(results.All(x => x.CreditsAvailable != x.CreditsReported));
        }

        [Test]
        public async void TestGetSubscriptionsByCustomer()
        {
            CreateTestHelpers();
            creditAvailableTasks.IndividualTasks.GetIndividualByWebLogin(Arg.Any<string>()).Returns(individual);

            var results = await creditAvailableTasks.GetSubscriptionsByCustomer(webLogin);

            Assert.IsTrue(results.Any());
            Assert.IsTrue(!string.IsNullOrWhiteSpace(results[0].Title));
            Assert.IsTrue(results[0].ProductKey != Guid.Empty);
            Assert.IsTrue(results[0].CreditsAvailable > 0);
            Assert.IsTrue(results.Count(x => x.Title == "American Family Physician") <= 1);
            Assert.IsTrue(results.All(x => x.CreditsAvailable != x.CreditsReported));
        }

        [Test]
        [Ignore]
        public async void TestGetCompletedByCustomer()
        {
            CreateTestHelpers();
            creditAvailableTasks.IndividualTasks.GetIndividualByWebLogin(Arg.Any<string>()).Returns(individual);

            var results = await creditAvailableTasks.GetCompletedByCustomer(webLogin);

            Assert.IsTrue(results.Any());
            Assert.IsTrue(!string.IsNullOrWhiteSpace(results[0].Title));
            Assert.IsTrue(results.All(x => x.CreditsAvailable == x.CreditsReported));
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
