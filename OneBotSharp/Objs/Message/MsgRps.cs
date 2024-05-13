using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Message;

/// <summary>
/// 猜拳魔法表情
/// </summary>
public record MsgRps : MsgBase
{
    public override string Type => Enums.MsgType.Rps;

    [JsonProperty("data")]
    public MsgData Data { get; set; }
    public record MsgData
    {

    }

    public override string BuildSendCq()
    {
        return $"[CQ:rps]";
    }

    public override string BuildRecvCq()
    {
        return BuildSendCq();
    }

    public static MsgRps Build()
    {
        return new()
        {
            Data = new()
            {

            }
        };
    }

    public static MsgRps RecvParse(CqCode code)
    {
        if (code.Type != Enums.MsgType.Rps)
        {
            throw new ArgumentException("cqcode type error");
        }

        return new()
        {
            Data = new()
            {

            }
        };
    }

    public static MsgRps SendParse(CqCode code)
    {
        return RecvParse(code);
    }

    public static MsgRps? JsonParse(JObject text, bool send)
    {
        return text.ToObject<MsgRps>();
    }
}
