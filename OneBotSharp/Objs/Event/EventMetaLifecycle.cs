using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Event;

public record EventMetaLifecycle : EventMeta
{
    [JsonIgnore]
    public const string SubTypeEnable = "enable";
    [JsonIgnore]
    public const string SubTypeDisable = "disable";
    [JsonIgnore]
    public const string SubTypeConnect = "connect";
    public override string MetaEventType => MetaLifecycle;

    [JsonProperty("sub_type")]
    public string SubType { get; set; }

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
