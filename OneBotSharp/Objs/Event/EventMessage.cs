using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OneBotSharp.Objs.Message;

namespace OneBotSharp.Objs.Event;

public abstract record EventMessage : EventBase
{
    public override string PostType => Enums.EventType.Message;

    /// <summary>
    /// 消息类型
    /// </summary>
    [JsonProperty("message_type")]
    public abstract string EventMessageType { get; }
    /// <summary>
    /// 消息子类型
    /// </summary>
    [JsonProperty("sub_type")]
    public string? SubType { get; set; }
    /// <summary>
    /// 消息 ID
    /// </summary>
    [JsonProperty("message_id")]
    public int MessageId { get; set; }
    /// <summary>
    /// 发送者 QQ 号
    /// </summary>
    [JsonProperty("user_id")]
    public int UserId { get; set; }
    /// <summary>
    /// 不要改这个，从Messages获取 
    /// </summary>
    [JsonProperty("message")]
    public object Message { get; set; }
    /// <summary>
    /// 原始消息内容
    /// </summary>
    [JsonProperty("raw_message")]
    public string RawMessage { get; set; }
    /// <summary>
    /// 字体
    /// </summary>
    [JsonProperty("font")]
    public int Font { get; set; }

    /// <summary>
    /// 消息内容
    /// </summary>
    [JsonIgnore]
    public List<MsgBase> Messages = [];

    protected void ParseMessage()
    {
        if (Message is string str)
        {
            Messages = CqHelper.ParseMsg(str);
        }
        else if (Message is JArray list)
        {
            foreach (var item in list)
            {
                var item1 = (item as JObject)!;
                var msg = MsgBase.ParseRecv(item1);
                if (msg != null)
                {
                    Messages.Add(msg);
                }
            }
        }
    }

    public static new readonly Dictionary<string, Func<JObject, EventMessage?>> JsonParser = new()
    {
        { Enums.MessageType.Private, EventPrivateMessage.JsonParse },
        { Enums.MessageType.Group, EventGroupMessage.JsonParse },
    };

    public static EventMessage? JsonParse(JObject obj)
    {
        if (obj.TryGetValue("message_type", out var value))
        {
            var type = value.ToString();
            if (JsonParser.TryGetValue(type, out var type1))
            {
                return type1(obj);
            }
        }

        return null;
    }
}
