using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Fravegabot.Infrastructure;
using Microsoft.Practices.Unity;
using Fravegabot.Controllers;

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

    public class MessageListBinder  : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var values = new MessageList();
            int i = 0;
            var valueProviderResult = bindingContext.ValueProvider.GetValue("value" + i);
            while (valueProviderResult!=null)
            {
                i++;
                values.Add(valueProviderResult.AttemptedValue);
                valueProviderResult = bindingContext.ValueProvider.GetValue("value" + i);
            }
            return values;
        }
    }

    public class GuidBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var key = bindingContext.ModelName;
            var valueProviderResult = bindingContext.ValueProvider.GetValue(key);
            if(valueProviderResult != null)
            {
                return Guid.Parse( valueProviderResult.AttemptedValue.Replace("-",""));
            }
            return Guid.Empty;
        }
    }

}