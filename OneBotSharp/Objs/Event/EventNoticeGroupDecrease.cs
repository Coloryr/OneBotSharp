﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Event;

/// <summary>
/// 群成员减少
/// </summary>
public record EventNoticeGroupDecrease : EventNotice
{
    [JsonIgnore]
    public const string SubTypeLeave = "leave";
    [JsonIgnore]
    public const string SubTypeKick = "kick";
    [JsonIgnore]
    public const string SubTypeKickMe = "kick_me";

    public override string NoticeType => NoticeGroupDecrease;

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

    public static new EventNoticeGroupDecrease? JsonParse(JObject obj)
    {
        var msg = obj.ToObject<EventNoticeGroupDecrease>();
        if (msg == null)
        {
            return null;
        }

        return msg;
    }
}
