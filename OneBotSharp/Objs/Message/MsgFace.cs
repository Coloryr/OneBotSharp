using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Message;

/// <summary>
/// QQ 表情
/// </summary>
public record MsgFace : MsgBase
{
    public override string Type => Enums.MsgType.Face;

    [JsonProperty("data")]
    public MsgData Data { get; set; }
    public record MsgData
    {
        /// <summary>
        /// QQ 表情 ID
        /// </summary>
        [JsonProperty("id")]
        public string? Id { get; set; }
    }

    public override string BuildSendCq()
    {
        return $"[CQ:face,id={Data.Id}]";
    }

    public override string ToString()
    {
        return $"[表情]";
    }

    public override string BuildRecvCq()
    {
        return BuildSendCq();
    }

    public static MsgFace RecvParse(CqCode code)
    {
        if (code.Type != Enums.MsgType.Face)
        {
            throw new ArgumentException("cqcode type error");
        }

        return new()
        {
            Data = new()
            {
                Id = code["id"]
            }
        };
    }

    public static MsgFace SendParse(CqCode code)
    {
        return RecvParse(code);
    }

    public static MsgFace? JsonParse(JObject text, bool send)
    {
        return text.ToObject<MsgFace>();
    }

    public static MsgFace BuildSend(string id)
    {
        return new()
        {
            Data = new()
            {
                Id = id
            }
        };
    }
}
