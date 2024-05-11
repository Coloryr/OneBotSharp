using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Event;

public record EventRequestGroup : EventRequest
{
    [JsonIgnore]
    public const string SubTypeAdd = "add";
    [JsonIgnore]
    public const string SubTypeInvite = "invite";

    public override string RequestType => RequestGroup;

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
