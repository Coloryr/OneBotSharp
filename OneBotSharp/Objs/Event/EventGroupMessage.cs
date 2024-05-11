using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Event;

public record EventGroupMessage : EventMessage
{
    public override string MessageType => MessageGroup;

    /// <summary>
    /// 发送人信息
    /// </summary>
    [JsonProperty("sender")]
    public EventSender Sender { get; set; }
    /// <summary>
    /// 匿名信息，如果不是匿名消息则为 null
    /// </summary>
    [JsonProperty("anonymous")]
    public object Anonymous { get; set; }
    /// <summary>
    /// 群号
    /// </summary>
    [JsonProperty("group_id")]
    public long GroupId { get; set; }

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
        /// <summary>
        /// 群名片／备注
        /// </summary>
        [JsonProperty("card")]
        public string Card { get; set; }
        /// <summary>
        /// 地区
        /// </summary>
        [JsonProperty("area")]
        public string Area { get; set; }
        /// <summary>
        /// 成员等级
        /// </summary>
        [JsonProperty("level")]
        public string Level { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        [JsonProperty("role")]
        public string Role { get; set; }
        /// <summary>
        /// 专属头衔
        /// </summary>
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
