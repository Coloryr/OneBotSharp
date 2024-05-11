using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Event;

public abstract record EventNotice : EventBase
{
    [JsonIgnore]
    public const string NoticeGroupUpload = "group_upload";
    [JsonIgnore]
    public const string NoticeGroupAdmin = "group_admin";
    [JsonIgnore]
    public const string NoticeGroupDecrease = "group_decrease";
    [JsonIgnore]
    public const string NoticeGroupIncrease = "group_increase";
    [JsonIgnore]
    public const string NoticeGroupBan = "group_ban";
    [JsonIgnore]
    public const string NoticeFriendAdd = "friend_add";
    [JsonIgnore]
    public const string NoticeGroupRecall = "group_recall";
    [JsonIgnore]
    public const string NoticeFriendRecall = "friend_recall";
    [JsonIgnore]
    public const string NoticeGroupNotify = "notify";
    [JsonIgnore]
    public const string NoticeGroupLuckyKing = "lucky_king";

    public override string EventType => Notice;

    [JsonProperty("notice_type")]
    public abstract string NoticeType { get; }

    /// <summary>
    /// 发送者 QQ 号
    /// </summary>
    [JsonProperty("user_id")]
    public long UserId { get; set; }

    public static new readonly Dictionary<string, Func<JObject, EventNotice?>> JsonParser = new()
    {
        { NoticeGroupUpload, EventNoticeGroupUpload.JsonParse },
        { NoticeGroupAdmin, EventNoticeGroupAdmin.JsonParse },
        { NoticeGroupDecrease, EventNoticeGroupDecrease.JsonParse },
        { NoticeGroupIncrease, EventNoticeGroupIncrease.JsonParse },
        { NoticeGroupBan, EventNoticeGroupBan.JsonParse },
        { NoticeFriendAdd, EventNoticeFriendAdd.JsonParse },
        { NoticeGroupRecall, EventNoticeGroupRecall.JsonParse },
        { NoticeFriendRecall, EventNoticeFriendRecall.JsonParse },
        { NoticeGroupNotify, EventNoticeGroupNotify.JsonParse },
        { NoticeGroupLuckyKing, EventNoticeGroupLuckyKing.JsonParse }
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
