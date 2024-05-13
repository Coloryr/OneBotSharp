using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Event;

public abstract record EventMeta : EventBase
{
    public override string PostType => Enums.EventType.Meta;

    [JsonProperty("meta_event_type")]
    public abstract string MetaEventType { get; }

    public static new readonly Dictionary<string, Func<JObject, EventMeta?>> JsonParser = new()
    {
        { Enums.MetaType.Lifecycle, EventMetaLifecycle.JsonParse },
        { Enums.MetaType.Heartbeat, EventMetaHeartbeat.JsonParse }
    };

    public static EventMeta? JsonParse(JObject obj)
    {
        if (obj.TryGetValue("meta_event_type", out var value))
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
