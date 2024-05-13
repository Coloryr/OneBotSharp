using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Event;

public record EventNoticeGroupLuckyKing : EventNotice
{
    public override string NoticeType => Enums.NoticeType.GroupLuckyKing;

    /// <summary>
    /// 群号
    /// </summary>
    [JsonProperty("group_id")]
    public long GroupId { get; set; }
    /// <summary>
    /// 运气王 QQ 号
    /// </summary>
    [JsonProperty("target_id")]
    public long TargetId { get; set; }

    public static new EventNoticeGroupLuckyKing? JsonParse(JObject obj)
    {
        var msg = obj.ToObject<EventNoticeGroupLuckyKing>();
        if (msg == null)
        {
            return null;
        }

        return msg;
    }
}
