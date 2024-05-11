using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Event;

/// <summary>
/// 好友消息撤回
/// </summary>
public record EventNoticeFriendRecall : EventNotice
{
    public override string NoticeType => NoticeFriendRecall;

    /// <summary>
    /// 被撤回的消息 ID
    /// </summary>
    [JsonProperty("message_id")]
    public long MessageId { get; set; }

    public static new EventNoticeFriendRecall? JsonParse(JObject obj)
    {
        var msg = obj.ToObject<EventNoticeFriendRecall>();
        if (msg == null)
        {
            return null;
        }

        return msg;
    }
}
