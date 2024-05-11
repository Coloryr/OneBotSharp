using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Message;

/// <summary>
/// 推荐 好友/群
/// </summary>
public record MsgContact : MsgBase
{
    [JsonIgnore]
    public const string Friend = "qq";
    [JsonIgnore]
    public const string Group = "group";
    [JsonIgnore]
    public const string MsgType = "contact";

    public override string Type => MsgType;

    [JsonProperty("data")]
    public MsgData Data { get; set; }
    public record MsgData
    {
        //被推荐的号码
        [JsonProperty("id")]
        public string? Id { get; set; }
        /// <summary>
        /// 推荐类型
        /// </summary>
        [JsonProperty("type")]
        public string? Type { get; set; }
    }

    public override string BuildSendCq()
    {
        return $"[CQ:contact,id={Data.Id},type={Data.Type}]";
    }

    public override string BuildRecvCq()
    {
        return BuildSendCq();
    }

    public static MsgContact BuildFriend(string id)
    {
        return new()
        {
            Data = new()
            {
                Type = Friend,
                Id = id
            }
        };
    }

    public static MsgContact BuildGroup(string id)
    {
        return new()
        {
            Data = new()
            {
                Type = Group,
                Id = id
            }
        };
    }

    public static MsgContact MsgRecvParse(CqCode code)
    {
        if (code.Type != MsgType)
        {
            throw new ArgumentException("cqcode type error");
        }

        return new()
        {
            Data = new()
            {
                Type = code["type"],
                Id = code["id"]
            }
        };
    }

    public static MsgContact MsgSendParse(CqCode code)
    {
        return MsgRecvParse(code);
    }

    public static MsgContact? JsonParse(JObject text, bool send)
    {
        return text.ToObject<MsgContact>();
    }
}
