using System;
using System.Web.Mvc;

namespace Fravegabot.Binders
{
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