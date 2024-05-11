using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Event;

public abstract record EventRequest : EventBase
{
    [JsonIgnore]
    public const string RequestFriend = "friend";
    [JsonIgnore]
    public const string RequestGroup = "group";

    public override string EventType => Request;

    [JsonProperty("request_type")]
    public abstract string RequestType { get; }

    /// <summary>
    /// 发送请求的 QQ 号
    /// </summary>
    [JsonProperty("user_id")]
    public long UserId { get; set; }
    /// <summary>
    /// 验证信息
    /// </summary>
    [JsonProperty("comment")]
    public string Comment { get; set; }
    /// <summary>
    /// 请求 flag
    /// </summary>
    [JsonProperty("flag")]
    public string Flag { get; set; }

    public static new readonly Dictionary<string, Func<JObject, EventRequest?>> JsonParser = new()
    {
        { RequestFriend, EventRequestFriend.JsonParse },
        { RequestGroup, EventRequestGroup.JsonParse }
    };

    public static EventRequest? JsonParse(JObject obj)
    {
        if (obj.TryGetValue("request_type", out var value))
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
