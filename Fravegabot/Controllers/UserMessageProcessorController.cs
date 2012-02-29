using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

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

    public class UserMessageModel
    {
        public Guid BotKey { get; set; }
        public string UserKey { get; set; }
        public string Network { get; set; }
        public string User { get; set; }
        public string Channel { get; set; }
        public string Msg { get; set; }
        public long Step { get; set; }
    }

    public class MessageList : List<string>
    {
    }

    public static class RequestExtensions
    {
        public static string Dump(this HttpRequestBase request)
        {
            var sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine("-------------  PostedParameters --------------");
            foreach (var key in request.Form.Keys.Cast<string>().OrderBy(f => f))
            {
                sb.AppendLine(key.PadLeft(40, ' ') + " : " + request.Params[key]);
            }

            sb.AppendLine("");
            sb.AppendLine("-------------  Query String ------------------");
            foreach (var key in request.QueryString.Keys.Cast<string>().OrderBy(f => f))
            {
                sb.AppendLine(key.PadLeft(40, ' ') + " : " + request.QueryString[key]);
            }

            sb.AppendLine("");
            sb.AppendLine("-------------  Http Headers -------------------");
            foreach (var key in request.Headers.Keys.Cast<string>().OrderBy(f => f))
            {
                sb.AppendLine(key.PadLeft(40, ' ') + " : " + request.Headers[key]);
            }

            sb.AppendLine("");
            sb.AppendLine("--------------  Cookies -------------------------");
            foreach (var key in request.Cookies.Keys.Cast<string>().OrderBy(f => f))
            {
                sb.AppendLine(key.PadLeft(40, ' ') + " : " + request.Cookies[key].Value + " - ExpireDate: " +  request.Cookies[key].Expires);
            }

            return sb.ToString();
        }
    }
}
