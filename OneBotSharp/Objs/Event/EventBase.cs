using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Event;

public abstract record EventBase
{
    /// <summary>
    /// 事件发生的时间戳
    /// </summary>
    [JsonProperty("time")]
    public long Time { get; set; }
    /// <summary>
    /// 收到事件的机器人 QQ 号
    /// </summary>
    [JsonProperty("self_id")]
    public long Id { get; set; }
    /// <summary>
    /// 上报类型
    /// </summary>
    [JsonProperty("post_type")]
    public abstract string PostType { get; }

    /// <summary>
    /// 回复内容
    /// </summary>
    [JsonIgnore]
    public object Reply { get; set; }

    public static readonly Dictionary<string, Func<JObject, EventBase?>> JsonParser = new()
    {
        { Enums.EventType.Message, EventMessage.JsonParse },
        { Enums.EventType.Notice, EventNotice.JsonParse },
        { Enums.EventType.Request, EventRequest.JsonParse },
        { Enums.EventType.Meta, EventMeta.JsonParse },
    };

    /// <summary>
    /// 从Onebot接收到的json消息解析
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public static EventBase? ParseRecv(JObject obj)
    {
        if (obj.TryGetValue("post_type", out var value))
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
