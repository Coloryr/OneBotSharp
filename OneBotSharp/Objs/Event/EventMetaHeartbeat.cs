using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Event;

public record EventMetaHeartbeat : EventMeta
{
    public override string MetaEventType => Enums.MetaType.Heartbeat;

    [JsonProperty("status")]
    public object Status { get; set; }
    [JsonProperty("interval")]
    public long Interval { get; set; }

    public static EventMetaHeartbeat Build(string status, long interval)
    {
        return new EventMetaHeartbeat()
        {
            Status = status,
            Interval = interval
        };
    }

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
