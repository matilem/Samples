﻿using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Aafp.Events.Web.ApplicationConfig;
using StructureMap;
using WebSite.Components;

namespace Aafp.Events.Web
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            StructureMapConfig.Initialize("Aafp.Events.Web");
            Log4NetConfig.ConfigureWithDb(ApplicationConfigManager.Settings.ConnectionString, true);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RegisterControllerFactory();
        }

        public static void RegisterControllerFactory()
        {
            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());

            var container = ObjectFactory.Container;
            GlobalConfiguration.Configuration.DependencyResolver = new StructureMapDependencyResolver(container);
        }
    }
}