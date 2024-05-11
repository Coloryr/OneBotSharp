using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Event;

public record EventMetaHeartbeat : EventMeta
{
    public override string MetaEventType => MetaHeartbeat;

    [JsonProperty("status")]
    public object Status { get; set; }
    [JsonProperty("interval")]
    public long Interval { get; set; }

    public static new EventMetaHeartbeat? JsonParse(JObject obj)
    {
        var msg = obj.ToObject<EventMetaHeartbeat>();
        if (msg == null)
        {
            return null;
        }

        return msg;
    }
}
