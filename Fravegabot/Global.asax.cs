using System;
using System.Web.Mvc;
using System.Web.Routing;
using Fravegabot.Binders;
using Fravegabot.Infrastructure;
using Fravegabot.Models;
using Microsoft.Practices.Unity;

namespace Fravegabot
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}", // URL with parameters
                new { controller = "UserMessageProcessor", action = "SendMessage" } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            ConfigDependencyContainer();

            AreaRegistration.RegisterAllAreas();
            ModelBinders.Binders.Add(typeof(MessageList), new MessageListBinder());
            ModelBinders.Binders.Add(typeof(Guid), new GuidBinder());

            RegisterRoutes(RouteTable.Routes);

        }

        private void ConfigDependencyContainer()
        {
            var container = new UnityContainer();
  
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}