using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Event;

public abstract record EventBase
{
    [JsonIgnore]
    public const string TypeMessage = "message";
    [JsonIgnore]
    public const string TypeNotice = "notice";
    [JsonIgnore]
    public const string TypeRequest = "request";
    [JsonIgnore]
    public const string TypeMeta = "meta_event";

    [JsonProperty("time")]
    public long Time { get; set; }
    [JsonProperty("self_id")]
    public long Id { get; set; }
    [JsonProperty("post_type")]
    public abstract string EventType { get; }

    public static readonly Dictionary<string, Func<JObject, EventBase?>> JsonParser = new()
    {
        { TypeMessage, EventMessage.JsonParse },
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
