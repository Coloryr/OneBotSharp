using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OneBotSharp.Objs.Api;
using OneBotSharp.Protocol;

namespace OneBotSharp.Objs.Message;

/// <summary>
/// 合并转发
/// </summary>
public record MsgForward : MsgBase
{
    public override string Type => Enums.MsgType.Forward;

    [JsonProperty("data")]
    public MsgData Data { get; set; }
    public record MsgData
    {
        /// <summary>
        /// 合并转发 ID，需通过 get_forward_msg API 获取具体内容
        /// </summary>
        [JsonProperty("id")]
        public string? Id { get; set; }
    }

    public override string BuildSendCq()
    {
        return $"[CQ:forward,id={Data.Id}]";
    }

    public override string BuildRecvCq()
    {
        return BuildSendCq();
    }

    public Task<GetForwardMsgRes?> GetMsg(ISendClient client)
    {
        return client.GetForwardMsg(new GetForwardMsg()
        {
            Id = Data.Id!
        });
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

    public static MsgForward RecvParse(CqCode code)
    {
        if (code.Type != Enums.MsgType.Forward)
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

    public static MsgForward SendParse(CqCode code)
    {
        return RecvParse(code);
    }

    public static MsgForward? JsonParse(JObject text, bool send)
    {
        return text.ToObject<MsgForward>();
    }
}
