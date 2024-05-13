using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Event;

public abstract record EventNotice : EventBase
{
    public override string PostType => Enums.EventType.Notice;

    [JsonProperty("notice_type")]
    public abstract string NoticeType { get; }

    /// <summary>
    /// 发送者 QQ 号
    /// </summary>
    [JsonProperty("user_id")]
    public long UserId { get; set; }

    public static new readonly Dictionary<string, Func<JObject, EventNotice?>> JsonParser = new()
    {
        { Enums.NoticeType.GroupUpload, EventNoticeGroupUpload.JsonParse },
        { Enums.NoticeType.GroupAdmin, EventNoticeGroupAdmin.JsonParse },
        { Enums.NoticeType.GroupDecrease, EventNoticeGroupDecrease.JsonParse },
        { Enums.NoticeType.GroupIncrease, EventNoticeGroupIncrease.JsonParse },
        { Enums.NoticeType.GroupBan, EventNoticeGroupBan.JsonParse },
        { Enums.NoticeType.FriendAdd, EventNoticeFriendAdd.JsonParse },
        { Enums.NoticeType.GroupRecall, EventNoticeGroupRecall.JsonParse },
        { Enums.NoticeType.FriendRecall, EventNoticeFriendRecall.JsonParse },
        { Enums.NoticeType.GroupNotify, EventNoticeGroupNotify.JsonParse },
        { Enums.NoticeType.GroupLuckyKing, EventNoticeGroupLuckyKing.JsonParse }
    };

    public static EventNotice? JsonParse(JObject obj)
    {
        if (obj.TryGetValue("notice_type", out var value))
        {
            var type = value.ToString();
            if (JsonParser.TryGetValue(type, out var type1))
            {
                return type1(obj);
            }
        }

        return null;
    }
}
