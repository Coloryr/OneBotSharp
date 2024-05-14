using Newtonsoft.Json;

namespace OneBotSharp.Objs.Api;

public record SetGroupAnonymousBan
{
    /// <summary>
    /// 群号
    /// </summary>
    [JsonProperty("group_id")]
    public required long GroupId { get; set; }
    /// <summary>
    /// 可选，要禁言的匿名用户对象
    /// </summary>
    [JsonProperty("anonymous")]
    public object Anonymous { get; set; }
    /// <summary>
    /// 可选，要禁言的匿名用户的 flag
    /// </summary>
    [JsonProperty("flag")]
    public object Flag { get; set; }
    /// <summary>
    /// 禁言时长，单位秒，无法取消匿名用户禁言
    /// </summary>
    [JsonProperty("duration")]
    public long Duration { get; set; }
}

public record SetGroupAnonymousBanRes
{

}