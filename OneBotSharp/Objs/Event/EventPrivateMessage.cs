using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Event;

public record EventPrivateMessage : EventMessage
{
    public override string EventMessageType => Enums.MessageType.Private;

    /// <summary>
    /// 发送人信息
    /// </summary>
    [JsonProperty("sender")]
    public EventSender Sender { get; set; }
    public record EventSender
    {
        /// <summary>
        /// 发送者 QQ 号
        /// </summary>
        [JsonProperty("user_id")]
        public long UserId { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [JsonProperty("nickname")]
        public string NickName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [JsonProperty("sex")]
        public string Sex { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
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
