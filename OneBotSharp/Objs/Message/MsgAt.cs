using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Message;

/// <summary>
/// @某人
/// </summary>
public record MsgAt : MsgBase
{
    [JsonIgnore]
    public const string MsgType = "at";

    public override string Type => MsgType;

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

    public static MsgAt MsgRecvParse(CqCode code)
    {
        if (code.Type != MsgType)
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

    public static MsgAt MsgSendParse(CqCode code)
    {
        return MsgRecvParse(code);
    }

    public static MsgAt? JsonParse(JObject text, bool send)
    {
        return text.ToObject<MsgAt>();
    }
}
