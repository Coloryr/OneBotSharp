using Newtonsoft.Json;

namespace OneBotSharp.Objs.Api;

public record SetGroupBan
{
    /// <summary>
    /// 群号
    /// </summary>
    [JsonProperty("group_id")]
    public required long GroupId { get; set; }
    /// <summary>
    /// 要禁言的 QQ 号
    /// </summary>
    [JsonProperty("user_id")]
    public required long UserId { get; set; }
    /// <summary>
    /// 禁言时长，单位秒，0 表示取消禁言
    /// </summary>
    [JsonProperty("duration")]
    public long Duration { get; set; } = 180;
}

public record SetGroupBanRes
{

}