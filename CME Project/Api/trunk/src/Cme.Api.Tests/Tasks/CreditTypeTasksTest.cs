using System.Linq;
using Aafp.Cme.Api.Daos.Queries.Interfaces;
using Aafp.Cme.Api.Tasks.Interfaces;
using Aafp.WebApi.Components;
using NUnit.Framework;

namespace Aafp.Cme.Api.Tests.Tasks
{
    [TestFixture]
    [Category("Integration")]
    public class CreditTypeTasksTest : AbstractTestFixture
    {
        private ICreditTypeTasks creditTypeTasks;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            creditTypeTasks = StructureMapConfig.Resolve<ICreditTypeTasks>();
            creditTypeTasks.CreditTypeQuery = StructureMapConfig.Resolve<ICreditTypeQuery>();
        }

        [TearDown]
        public override void TearDown()
        {
            creditTypeTasks = null;
            base.TearDown();
        }

        [Test]
        public void TestGetAllCreditTypes()
        {
            var types = creditTypeTasks.GetAllCreditTypes();

            Assert.IsTrue(types.Any());
        }
    }
}
