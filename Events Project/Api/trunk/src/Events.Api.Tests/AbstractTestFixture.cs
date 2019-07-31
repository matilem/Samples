using System;
using System.Transactions;
using Aafp.Events.Api.ApplicationConfig;
using Aafp.WebApi.Components;
using NHibernate;
using NUnit.Framework;

namespace Aafp.Events.Api.Tests
{
    public abstract class AbstractTestFixture
    {
        public ISession Session { get; set; }

        public TransactionScope Scope { get; set; }

        [SetUp]
        public virtual void SetUp()
        {
            AutomapperConfig.Configure();
            StructureMapConfig.Initialize("Aafp.Events.Api", ApplicationConfigManager.Settings.ConnectionString);
            Scope = new TransactionScope();
            Session = StructureMapConfig.Resolve<ISession>();

            if (ApplicationConfigManager.Settings.ConnectionString.Contains("server=db1.webad.aafp.org;Initial Catalog=netForum;uid=netForumAdminUser;pwd=iW3bBegone"))
                throw new Exception("Do not run tests against production.");
        }

        [TearDown]
        public virtual void TearDown()
        {
            Scope.Dispose();
        }
    }
}
