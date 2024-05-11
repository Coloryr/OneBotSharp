using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Message;

/// <summary>
/// 回复
/// </summary>
public record MsgReply : MsgBase
{
    [JsonIgnore]
    public const string MsgType = "reply";

    public override string Type => MsgType;

    [JsonProperty("data")]
    public MsgData Data { get; set; }
    public record MsgData
    {
        /// <summary>
        /// 回复时引用的消息 ID
        /// </summary>
        [JsonProperty("id")]
        public string? Id { get; set; }
    }

    public override string BuildSendCq()
    {
        return $"[CQ:reply,id={Data.Id}]";
    }

    public override string BuildRecvCq()
    {
        return BuildSendCq();
    }

    public static MsgReply Build(string id)
    {
        return new()
        {
            Data = new()
            {
                Id = id
            }
        };
    }

    public static MsgReply MsgRecvParse(CqCode code)
    {
        if (code.Type != MsgType)
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

    public static MsgReply MsgSendParse(CqCode code)
    {
        return MsgRecvParse(code);
    }

    public static MsgReply? JsonParse(JObject text, bool send)
    {
        return text.ToObject<MsgReply>();
    }
}
