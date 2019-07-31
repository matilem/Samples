using System;
using Aafp.WebApi.Components;
using NHibernate;
using NUnit.Framework;


namespace Aafp.Also.Api.Tests
{
    public class AbstractTestFixture
    {
        [SetUp]
        public virtual void SetUp()
        {
            StructureMapConfig.Initialize("Aafp.Also.Api", ApplicationConfig.DatabaseConnectionString);
            var session = StructureMapConfig.Resolve<ISession>();
            session.BeginTransaction();

            if (ApplicationConfig.DatabaseConnectionString.Contains("server=db1.webad.aafp.org"))
                throw new Exception("Do not run tests against production.");
        }

        [TearDown]
        public virtual void TearDown()
        {
            var session = StructureMapConfig.Resolve<ISession>();

            if (session != null)
            {
                session.Transaction.Rollback();
            }
        }
    }
}