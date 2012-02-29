using System.Web.Mvc;

namespace Fravegabot.Controllers
{
    public class UserMessageProcessorController : Controller
    {
        [HttpPost]
        public ActionResult SendMessage()
        {
            return SendToUser("Hola");
        }

        private static ActionResult SendToUser(string message)
        {
            return new ContentResult{ Content = message };
        }
    }
}
