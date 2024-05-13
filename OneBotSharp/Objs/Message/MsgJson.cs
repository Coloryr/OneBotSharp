using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Message;

/// <summary>
/// JSON 消息
/// </summary>
public record MsgJson : MsgBase
{
    public override string Type => Enums.MsgType.Json;

    [JsonProperty("data")]
    public MsgData Data { get; set; }
    public record MsgData
    {
        /// <summary>
        /// JSON 内容
        /// </summary>
        [JsonProperty("data")]
        public string? Data { get; set; }
    }

    public override string BuildSendCq()
    {
        return $"[CQ:xml,data={Data.Data}]";
    }

    public override string BuildRecvCq()
    {
        return BuildSendCq();
    }

    public static MsgJson Build(string json)
    {
        return new()
        {
            Data = new()
            {
                Data = json
            }
        };
    }

    public static MsgJson Build(JObject obj)
    {
        return new()
        {
            Data = new()
            {
                Data = obj.ToString()
            }
        };
    }

    public static MsgJson MsgRecvParse(CqCode code)
    {
        if (code.Type != Enums.MsgType.Json)
        {
            throw new ArgumentException("cqcode type error");
        }

        return new()
        {
            Data = new()
            {
                Data = code["json"]
            }
        };
    }

    public static MsgJson MsgSendParse(CqCode code)
    {
        return MsgRecvParse(code);
    }

    public static MsgJson? MsgJsonParser(JObject text, bool send)
    {
        return text.ToObject<MsgJson>();
    }
}
