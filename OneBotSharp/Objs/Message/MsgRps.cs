using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Message;

public record MsgRps : MsgBase
{
    [JsonIgnore]
    public const string MsgType = "rps";

    public override string Type => MsgType;

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

    public static MsgRps MsgRecvParse(CqCode code)
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

    public static MsgRps MsgSendParse(CqCode code)
    {
        return MsgRecvParse(code);
    }

    public static MsgRps? JsonParse(JObject text, bool send)
    {
        return text.ToObject<MsgRps>();
    }
}
