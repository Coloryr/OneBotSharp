using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Event;

/// <summary>
/// 群成员增加
/// </summary>
public record EventNoticeGroupIncrease : EventNotice
{
    [JsonIgnore]
    public const string SubTypeApprove = "approve";
    [JsonIgnore]
    public const string SubTypeInvite = "invite";

    public override string NoticeType => NoticeGroupIncrease;

    /// <summary>
    /// 事件子类型
    /// </summary>
    [JsonProperty("sub_type")]
    public string SubType { get; set; }
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

    public static new EventNoticeGroupIncrease? JsonParse(JObject obj)
    {
        var msg = obj.ToObject<EventNoticeGroupIncrease>();
        if (msg == null)
        {
            return null;
        }

        return msg;
    }
}
