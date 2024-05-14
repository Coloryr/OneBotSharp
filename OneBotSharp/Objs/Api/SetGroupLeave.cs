using Newtonsoft.Json;

namespace OneBotSharp.Objs.Api;

public record SetGroupLeave
{
    /// <summary>
    /// 群号
    /// </summary>
    [JsonProperty("group_id")]
    public required long GroupId { get; set; }
    /// <summary>
    /// 是否解散，如果登录号是群主，则仅在此项为 true 时能够解散
    /// </summary>
    [JsonProperty("is_dismiss")]
    public bool IsDismiss { get; set; }
}

public record SetGroupLeaveRes
{

}
