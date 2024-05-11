using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Event;

public record EventNoticeGroupBan : EventNotice
{
    [JsonIgnore]
    public const string SubTypeBan = "ban";
    [JsonIgnore]
    public const string SubTypeLiftBan = "lift_ban";

    public override string NoticeType => NoticeGroupBan;

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
    /// <summary>
    /// 禁言时长，单位秒
    /// </summary>
    public long Duration { get; set; }

    public static new EventNoticeGroupBan? JsonParse(JObject obj)
    {
        var msg = obj.ToObject<EventNoticeGroupBan>();
        if (msg == null)
        {
            return null;
        }

        return msg;
    }
}
