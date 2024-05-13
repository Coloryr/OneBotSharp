using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Message;

/// <summary>
/// @某人
/// </summary>
public record MsgAt : MsgBase
{
    public override string Type => Enums.MsgType.At;

    [JsonProperty("data")]
    public MsgData Data { get; set; }
    public record MsgData
    {
        /// <summary>
        /// @的 QQ 号，all 表示全体成员
        /// </summary>
        [JsonProperty("qq")]
        public string? QQ { get; set; }
    }

    public override string BuildSendCq()
    {
        return $"[CQ:at,qq={Data.QQ}]";
    }

    public override string BuildRecvCq()
    {
        return BuildSendCq();
    }

    public static MsgAt BuildAt(string id)
    {
        return new()
        {
            Data = new()
            {
                QQ = id.ToString()
            }
        };
    }

    public static MsgAt BuildAll()
    {
        return new()
        {
            Data = new()
            {
                QQ = "all"
            }
        };
    }

    public static MsgAt RecvParse(CqCode code)
    {
        if (code.Type != Enums.MsgType.At)
        {
            throw new ArgumentException("cqcode type error");
        }

        return new()
        {
            Data = new()
            {
                QQ = code["qq"]
            }
        };
    }

    public static MsgAt SendParse(CqCode code)
    {
        return RecvParse(code);
    }

    public static MsgAt? JsonParse(JObject text, bool send)
    {
        return text.ToObject<MsgAt>();
    }
}
