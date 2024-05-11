using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Message;

/// <summary>
/// 掷骰子魔法表情
/// </summary>
public record MsgDice : MsgBase
{
    [JsonIgnore]
    public const string MsgType = "dice";

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

    public static MsgDice Build()
    {
        return new()
        {
            Data = new()
            {
                
            }
        };
    }

    public static MsgDice MsgRecvParse(CqCode code)
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

    public static MsgDice MsgSendParse(CqCode code)
    {
        return MsgRecvParse(code);
    }

    public static MsgDice? JsonParse(JObject text, bool send)
    {
        return text.ToObject<MsgDice>();
    }
}
