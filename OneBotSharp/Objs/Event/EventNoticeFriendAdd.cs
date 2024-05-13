using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Event;

/// <summary>
/// 好友添加
/// </summary>
public record EventNoticeFriendAdd : EventNotice
{
    public override string NoticeType => Enums.NoticeType.FriendAdd;

    public static new EventNoticeFriendAdd? JsonParse(JObject obj)
    {
        var msg = obj.ToObject<EventNoticeFriendAdd>();
        if (msg == null)
        {
            return null;
        }

        return msg;
    }
}
