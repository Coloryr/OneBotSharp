using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Message;

/// <summary>
/// 窗口抖动（戳一戳）
/// </summary>
public record MsgShake : MsgBase
{
    public override string Type => Enums.MsgType.Shake;

    [JsonProperty("data")]
    public MsgData Data { get; set; }
    public record MsgData
    {

    }

    public override string BuildSendCq()
    {
        return $"[CQ:dice]";
    }

    public override string BuildRecvCq()
    {
        return BuildSendCq();
    }

    public override string ToString()
    {
        return $"[窗口抖动]";
    }

    public static MsgShake Build()
    {
        return new()
        {
            Data = new()
            {

            }
        };
    }

    public static MsgShake RecvParse(CqCode code)
    {
        if (code.Type != Enums.MsgType.Shake)
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

    public static MsgShake SendParse(CqCode code)
    {
        return RecvParse(code);
    }

    public static MsgShake? JsonParse(JObject text, bool send)
    {
        return text.ToObject<MsgShake>();
    }
}
