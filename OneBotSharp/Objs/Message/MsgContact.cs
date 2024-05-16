using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Message;

/// <summary>
/// 推荐 好友/群
/// </summary>
public record MsgContact : MsgBase
{
    public override string Type => Enums.MsgType.Contact;

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

    public override string ToString()
    {
        return $"推荐{(Data.Type == Enums.ContactType.Group ? "群聊" : "好友")}：{Data.Id}";
    }

    public static MsgContact BuildFriend(string id)
    {
        return new()
        {
            Data = new()
            {
                Type = Enums.ContactType.Friend,
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
                Type = Enums.ContactType.Group,
                Id = id
            }
        };
    }

    public static MsgContact RecvParse(CqCode code)
    {
        if (code.Type != Enums.MsgType.Contact)
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

    public static MsgContact SendParse(CqCode code)
    {
        return RecvParse(code);
    }

    public static MsgContact? JsonParse(JObject text, bool send)
    {
        return text.ToObject<MsgContact>();
    }
}
