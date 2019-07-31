using System;
using System.Transactions;
using Aafp.WebApi.Components;
using NHibernate;
using NUnit.Framework;

namespace Aafp.Cme.Api.Tests
{
    public abstract class AbstractTestFixture
    {
        public ISession Session { get; set; }

        public TransactionScope Scope { get; set; }

        [SetUp]
        public virtual void SetUp()
        {
            StructureMapConfig.Initialize("Aafp.Cme.Api", ApplicationConfig.DatabaseConnectionString);
            Scope = new TransactionScope();
            Session = StructureMapConfig.Resolve<ISession>();

            if (ApplicationConfig.DatabaseConnectionString.Contains("server=db1.webad.aafp.org;Initial Catalog=netForum;uid=netForumAdminUser;pwd=iW3bBegone"))
                throw new Exception("Do not run tests against production.");
        }

        [TearDown]
        public virtual void TearDown()
        {
            Scope.Dispose();
        }
    }
}
