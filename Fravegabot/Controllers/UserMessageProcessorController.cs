using System;
using System.Web.Mvc;

namespace Fravegabot.Controllers
{
    public class UserMessageProcessorController : Controller
    {
        [AcceptVerbs("GET", "POST")]
        public ActionResult SendMessage(UserMessageModel model)
        {
            return SendToUser(model.Message);
        }

        private static ActionResult SendToUser(string message)
        {
            return new ContentResult{ Content = message };
        }
    }

    public class UserMessageModel
    {
        public Guid BotKey { get; set; }
        public Guid UserKey { get; set; }
        public string Network { get; set; }
        public string User { get; set; }
        public string Channel { get; set; }
        public string Message { get; set; }
        public long Step { get; set; }
    }
}
