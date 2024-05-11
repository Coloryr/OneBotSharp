using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Event;

public record EventPrivateMessage : EventMessage
{
    public override string MessageType => TypeMessagePrivate;

    [JsonProperty("sender")]
    public EventSender Sender { get; set; }
    public record EventSender
    {
        [JsonProperty("user_id")]
        public long UserId { get; set; }
        [JsonProperty("nickname")]
        public string NickName { get; set; }
        [JsonProperty("sex")]
        public string Sex { get; set; }
        [JsonProperty("age")]
        public int Age { get; set; }
    }

    public static new EventPrivateMessage? JsonParse(JObject obj)
    {
        var msg = obj.ToObject<EventPrivateMessage>();
        if (msg == null)
        {
            return null;
        }
        msg.ParseMessage();
        return msg;
    }
}
