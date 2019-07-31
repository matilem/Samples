using System;
using System.Transactions;
using Aafp.Payments.Api;
using Aafp.Payments.Api.ApplicationConfig;
using Aafp.WebApi.Components;
using NHibernate;
using NUnit.Framework;

namespace Aafp.Payments.Api.Tests
{
    public abstract class AbstractTestFixture
    {
        public ISession Session { get; set; }

        public TransactionScope Scope { get; set; }

        [SetUp]
        public virtual void SetUp()
        {
            AutomapperConfig.Configure();
            Log4NetConfig.ConfigureWithDb(ApplicationConfigManager.Settings.ConnectionString, true);
            StructureMapConfig.Initialize("Aafp.Payments.Api", ApplicationConfigManager.Settings.ConnectionString);
            //Scope = new TransactionScope();
            Session = StructureMapConfig.Resolve<ISession>();

            if (ApplicationConfigManager.Settings.ConnectionString == "server=db1.webad.aafp.org;database=netForum;uid=netForumAdminUser;pwd=iW3bBegone")
                throw new Exception("Do not run tests against production.");
        }

        [TearDown]
        public virtual void TearDown()
        {
            //Scope.Dispose();
        }
    }
}
