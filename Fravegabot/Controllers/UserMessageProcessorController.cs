using System.IO;
using System.Web.Mvc;
using Fravegabot.Binders;
using Fravegabot.Extensions;
using Fravegabot.Models;

namespace Fravegabot.Controllers
{
    public class UserMessageProcessorController : Controller
    {
        [AcceptVerbs("GET", "POST")]
        public ActionResult SendMessage(UserMessageModel model, [ModelBinder(typeof(MessageListBinder))] MessageList previousMessages)
        {
            return SendToUser(ModelState.IsValid 
                ? model.Msg
                : Request.Dump());
        }

        private static ActionResult SendToUser(string message)
        {
            return new ContentResult{ Content = message };
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.Result = SendToUser(filterContext.Exception.ToString());
            filterContext.ExceptionHandled = true;
        }
    }
}
