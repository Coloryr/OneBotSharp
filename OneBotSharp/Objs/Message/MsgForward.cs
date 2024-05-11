using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Message;

/// <summary>
/// 合并转发
/// </summary>
public record MsgForward : MsgBase
{
    [JsonIgnore]
    public const string MsgType = "forward";

    public override string Type => MsgType;

    [JsonProperty("data")]
    public MsgData Data { get; set; }
    public record MsgData
    {
        /// <summary>
        /// 合并转发 ID，需通过 get_forward_msg API 获取具体内容
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public override string BuildSendCq()
    {
        return $"[CQ:forward,id={Data.Id}]";
    }

    public override string BuildRecvCq()
    {
        return BuildSendCq();
    }

    public static MsgForward Build(string id)
    {
        return new()
        {
            Data = new()
            {
                Id = id
            }
        };
    }

    public static MsgForward MsgRecvParse(CqCode code)
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

    public static MsgForward MsgSendParse(CqCode code)
    {
        return MsgRecvParse(code);
    }

    public static MsgForward? JsonParse(JObject text, bool send)
    {
        return text.ToObject<MsgForward>();
    }
}
