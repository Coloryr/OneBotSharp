﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Event;

/// <summary>
/// 群内戳一戳
/// 群成员荣誉变更
/// </summary>
public record EventNoticeGroupNotify : EventNotice
{
    [JsonIgnore]
    public const string SubTypePoke = "poke";
    [JsonIgnore]
    public const string SubTypeHonor = "honor";

    [JsonIgnore]
    public const string HonorTypeTalkative = "talkative";
    [JsonIgnore]
    public const string HonorTypePerformer = "performer";
    [JsonIgnore]
    public const string HonorTypeEmotion = "emotion";

    public override string NoticeType => NoticeGroupNotify;

    /// <summary>
    /// 提示类型
    /// </summary>
    [JsonProperty("sub_type")]
    public string SubType { get; set; }
    /// <summary>
    /// 群号
    /// </summary>
    [JsonProperty("group_id")]
    public long GroupId { get; set; }
    /// <summary>
    /// 被戳者 QQ 号
    /// </summary>
    [JsonProperty("target_id")]
    public long TargetId { get; set; }
    /// <summary>
    /// 荣誉类型
    /// </summary>
    [JsonProperty("honor_type")]
    public string HonorType { get; set; }

    public static new EventNoticeGroupNotify? JsonParse(JObject obj)
    {
        var msg = obj.ToObject<EventNoticeGroupNotify>();
        if (msg == null)
        {
            return null;
        }

        return msg;
    }
}
