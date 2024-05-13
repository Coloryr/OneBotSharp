using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Event;

public record EventRequestGroup : EventRequest
{
    public override string RequestType => Enums.RequestType.Group;

    /// <summary>
    /// 群号
    /// </summary>
    [JsonProperty("group_id")]
    public long GroupId { get; set; }
    /// <summary>
    /// 请求子类型
    /// </summary>
    [JsonProperty("sub_type")]
    public string SubType { get; set; }

    public static new EventRequestGroup? JsonParse(JObject obj)
    {
        var msg = obj.ToObject<EventRequestGroup>();
        if (msg == null)
        {
            return null;
        }

        return msg;
    }
}
