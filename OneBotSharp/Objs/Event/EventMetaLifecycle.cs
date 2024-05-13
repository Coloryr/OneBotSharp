using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Event;

public record EventMetaLifecycle : EventMeta
{
    public override string MetaEventType => Enums.MetaType.Lifecycle;

    [JsonProperty("sub_type")]
    public string SubType { get; set; }

    public static EventMetaLifecycle Build(string type)
    {
        return new EventMetaLifecycle()
        {
            SubType = type
        };
    }

    public static new EventMetaLifecycle? JsonParse(JObject obj)
    {
        var msg = obj.ToObject<EventMetaLifecycle>();
        if (msg == null)
        {
            return null;
        }

        return msg;
    }
}
