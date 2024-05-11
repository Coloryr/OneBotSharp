using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Message;

/// <summary>
/// QQ 表情
/// </summary>
public record MsgFace : MsgBase
{
    [JsonIgnore]
    public const string MsgType = "face";

    public override string Type => MsgType;

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

    public override string BuildRecvCq()
    {
        return BuildSendCq();
    }

    public static MsgFace MsgRecvParse(CqCode code)
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

    public static MsgFace MsgSendParse(CqCode code)
    {
        return MsgRecvParse(code);
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
