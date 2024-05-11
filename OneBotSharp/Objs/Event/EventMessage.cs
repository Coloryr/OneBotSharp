using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OneBotSharp.Objs.Message;

namespace OneBotSharp.Objs.Event;

public abstract record EventMessage : EventBase
{
    [JsonIgnore]
    public const string TypeMessagePrivate = "private";
    [JsonIgnore]
    public const string TypeMessageGroup = "group";

    public override string EventType => TypeMessage;

    [JsonProperty("message_type")]
    public abstract string MessageType { get; }
    [JsonProperty("sub_type")]
    public string? SubType { get; set; }
    [JsonProperty("message_id")]
    public int MessageId { get; set; }
    [JsonProperty("user_id")]
    public int UserId { get; set; }
    [JsonProperty("message")]
    public object Message { get; set; }
    [JsonProperty("raw_message")]
    public string RawMessage { get; set; }
    [JsonProperty("font")]
    public int Font { get; set; }

    [JsonIgnore]
    public List<MsgBase> Messages = [];

    protected void ParseMessage()
    {
        if (Message is string str)
        {
            Messages = CqHelper.ParseMsg(str);
        }
        else if (Message is JArray list)
        {
            foreach (var item in list)
            {
                var item1 = (item as JObject)!;
                var msg = MsgBase.ParseRecv(item1);
                if (msg != null)
                {
                    Messages.Add(msg);
                }
            }
        }
    }

    public static new readonly Dictionary<string, Func<JObject, EventMessage?>> JsonParser = new()
    {
        { TypeMessagePrivate, EventPrivateMessage.JsonParse },
    };

    public static EventMessage? JsonParse(JObject obj)
    {
        if (obj.TryGetValue("message_type", out var value))
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
