using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.Routing;
using Aafp.Events.Api.Dao.Interfaces;
using Aafp.Events.Api.Tasks.Admin.Interfaces;
using Aafp.WebApi.Components;
using Moq;
using NUnit.Framework;

namespace Aafp.Events.Api.Tests.Routes
{
    [TestFixture]
    [Category("Service")]
    public class AdminRegistrationRoutesTests
    {
        private HttpConfiguration configuration;
        [SetUp]
        public void SetUp()
        {
            configuration = new HttpConfiguration();
            WebApiConfig.Register(configuration);
            configuration.EnsureInitialized();
        }

        [TearDown]
        public void TearDown()
        {
            configuration = null;
        }

        [Test]
        public void TestRoutes()
        {
            var routes = configuration.Services.GetApiExplorer().ApiDescriptions;

            Assert.IsTrue(routes.Any(x => x.RelativePath == "admin/registration/events"));
        }
    }
}
