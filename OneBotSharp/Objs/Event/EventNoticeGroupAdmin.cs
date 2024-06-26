﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Event;

/// <summary>
/// 群管理员变动
/// </summary>
public record EventNoticeGroupAdmin : EventNotice
{
    public override string NoticeType => Enums.NoticeType.GroupAdmin;

    /// <summary>
    /// 群号
    /// </summary>
    [JsonProperty("group_id")]
    public long GroupId { get; set; }
    /// <summary>
    /// 文件信息
    /// </summary>
    [JsonProperty("sub_type")]
    public string SubType { get; set; }

    public static EventNoticeGroupAdmin Build(long group, string type)
    {
        return new()
        {
            GroupId = group,
            SubType = type
        };
    }

    public static new EventNoticeGroupAdmin? JsonParse(JObject obj)
    {
        var msg = obj.ToObject<EventNoticeGroupAdmin>();
        if (msg == null)
        {
            return null;
        }

        return msg;
    }
}
