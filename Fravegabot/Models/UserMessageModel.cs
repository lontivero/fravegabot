using System;

namespace Fravegabot.Models
{
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
}