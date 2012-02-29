using System.Web.Mvc;
using Fravegabot.Controllers;
using Fravegabot.Models;

namespace Fravegabot.Binders
{
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
}