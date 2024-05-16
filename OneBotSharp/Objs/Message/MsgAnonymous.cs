using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Message;

/// <summary>
/// 匿名发消息
/// </summary>
public record MsgAnonymous : MsgBase
{
    public override string Type => Enums.MsgType.Anonymous;

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

    public override string ToString()
    {
        return "[匿名消息]";
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

    public static MsgAnonymous RecvParse(CqCode code)
    {
        if (code.Type != Enums.MsgType.Anonymous)
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

    public static MsgAnonymous SendParse(CqCode code)
    {
        if (code.Type != Enums.MsgType.Anonymous)
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
