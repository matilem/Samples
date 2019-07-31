using Aafp.Cme.Api.Daos.Queries.Interfaces;
using Aafp.Cme.Api.Tasks.Interfaces;
using Aafp.WebApi.Components;
using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Aafp.Cme.Api.Tests.Tasks
{
    [TestFixture]
    [Category("Integration")]
    public class CreditTasksTest : AbstractTestFixture
    {
        private ICreditTasks creditTasks;

        private string webLogin = "8152494";

        private List<Guid> credits = new List<Guid>();

        private Guid credit = new Guid("97D3CADB-A0B2-4255-BDC7-998A4BAA25C6");

    [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            creditTasks = StructureMapConfig.Resolve<ICreditTasks>();
            creditTasks.CreditQuery = StructureMapConfig.Resolve<ICreditQuery>();
        }

        [TearDown]
        public override void TearDown()
        {
            creditTasks = null;
            base.TearDown();
        }

        [Test]
        public void TestGetByCustomerForReElectionCalculation()
        {
            var credits = creditTasks.GetByCustomerForReElectionCalculation(new Guid("09e468c6-9d3b-4002-a40b-84bf38d1c663"), 2015, 2017);

            Assert.IsTrue(credits.Any());
        }

        [Test]
        public async void TestGetLiveCreditsForTranscript()
        {
            var results = await creditTasks.GetLiveCreditsForTranscript(webLogin);

            Assert.IsTrue(results.Any());
        }

        [Test]
        public async void ReportCmeCredit()
        {
            credits.Add(credit);

            var results = await creditTasks.ReportCmeCredit(webLogin, credits);

            Assert.IsTrue(results.Any());
        }
    }
}
