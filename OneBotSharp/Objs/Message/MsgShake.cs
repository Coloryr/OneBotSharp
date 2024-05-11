using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Message;

/// <summary>
/// 窗口抖动（戳一戳）
/// </summary>
public record MsgShake : MsgBase
{
    [JsonIgnore]
    public const string MsgType = "shake";

    public override string Type => MsgType;

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

    public static MsgShake Build()
    {
        return new()
        {
            Data = new()
            {

            }
        };
    }

    public static MsgShake MsgRecvParse(CqCode code)
    {
        if (code.Type != MsgType)
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

    public static MsgShake MsgSendParse(CqCode code)
    {
        return MsgRecvParse(code);
    }

    public static MsgShake? JsonParse(JObject text, bool send)
    {
        return text.ToObject<MsgShake>();
    }
}
