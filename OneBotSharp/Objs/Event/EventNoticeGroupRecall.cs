using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Event;

/// <summary>
/// 群管理员变动
/// </summary>
public record EventNoticeGroupRecall : EventNotice
{
    [JsonIgnore]
    public const string SubTypeSet = "set";
    [JsonIgnore]
    public const string SubTypeUnset = "unset";

    public override string NoticeType => NoticeGroupRecall;

    /// <summary>
    /// 群号
    /// </summary>
    [JsonProperty("group_id")]
    public long GroupId { get; set; }
    /// <summary>
    /// 操作者 QQ 号
    /// </summary>
    [JsonProperty("operator_id")]
    public long OperatorId { get; set; }
    /// <summary>
    /// 被撤回的消息 ID
    /// </summary>
    [JsonProperty("message_id")]
    public long MessageId { get; set; }

    public static new EventNoticeGroupRecall? JsonParse(JObject obj)
    {
        var msg = obj.ToObject<EventNoticeGroupRecall>();
        if (msg == null)
        {
            return null;
        }

        return msg;
    }
}
