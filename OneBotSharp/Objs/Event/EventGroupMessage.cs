using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Event;

public record EventGroupMessage : EventMessage
{
    public override string MessageType => TypeMessageGroup;

    [JsonProperty("sender")]
    public EventSender Sender { get; set; }
    [JsonProperty("anonymous")]
    public object Anonymous { get; set; }

    public record EventSender
    {
        [JsonIgnore]
        public const string RoleOwner = "owner";

        [JsonProperty("user_id")]
        public long UserId { get; set; }
        [JsonProperty("nickname")]
        public string NickName { get; set; }
        [JsonProperty("sex")]
        public string Sex { get; set; }
        [JsonProperty("age")]
        public int Age { get; set; }
        [JsonProperty("card")]
        public string Card { get; set; }
        [JsonProperty("area")]
        public string Area { get; set; }
        [JsonProperty("level")]
        public string Level { get; set; }
        [JsonProperty("role")]
        public string Role { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
    }

    public static new EventGroupMessage? JsonParse(JObject obj)
    {
        var msg = obj.ToObject<EventGroupMessage>();
        if (msg == null)
        {
            return null;
        }
        msg.ParseMessage();
        return msg;
    }
}
