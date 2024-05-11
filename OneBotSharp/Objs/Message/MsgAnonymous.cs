using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Message;

/// <summary>
/// 匿名发消息
/// </summary>
public record MsgAnonymous : MsgBase
{
    [JsonIgnore]
    public const string MsgType = "anonymous";

    public override string Type => MsgType;

    [JsonProperty("data")]
    public MsgData Data { get; set; }
    public record MsgData
    {
        /// <summary>
        /// 可选，表示无法匿名时是否继续发送
        /// </summary>
        [JsonProperty("ignore")]
        public int? Ignore { get; set; }
    }

    public override string BuildSendCq()
    {
        if (Data.Ignore is { } value)
        {
            return $"[CQ:anonymous,ignore={value}]";
        }
        return $"[CQ:anonymous]";
    }

    public override string BuildRecvCq()
    {
        return BuildSendCq();
    }

    public static MsgAnonymous Build(bool? ignore = null)
    {
        return new()
        {
            Data = new()
            {
                Ignore = ignore is { } 
                    ? ignore.Value ? 1 : 0 : null
            }
        };
    }

    public static MsgAnonymous MsgRecvParse(CqCode code)
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

    public static MsgAnonymous MsgSendParse(CqCode code)
    {
        if (code.Type != MsgType)
        {
            throw new ArgumentException("cqcode type error");
        }

        var ign = code["ignore"];

        return new()
        {
            Data = new()
            {
                Ignore = ign is { } ? int.Parse(ign) : null
            }
        };
    }

    public static MsgAnonymous? JsonParse(JObject text, bool send)
    {
        return text.ToObject<MsgAnonymous>();
    }
}
