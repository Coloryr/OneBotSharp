using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Message;

/// <summary>
/// 掷骰子魔法表情
/// </summary>
public record MsgDice : MsgBase
{
    public override string Type => Enums.MsgType.Dice;

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
        return $"[随机骰子]";
    }

    public static MsgDice Build()
    {
        return new()
        {
            Data = new()
            {

            }
        };
    }

    public static MsgDice RecvParse(CqCode code)
    {
        if (code.Type != Enums.MsgType.Dice)
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

    public static MsgDice SendParse(CqCode code)
    {
        return RecvParse(code);
    }

    public static MsgDice? JsonParse(JObject text, bool send)
    {
        return text.ToObject<MsgDice>();
    }
}
