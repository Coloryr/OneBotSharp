using Newtonsoft.Json;

namespace OneBotSharp.Objs.Api;

public record SetGroupAnonymous
{
    /// <summary>
    /// 群号
    /// </summary>
    [JsonProperty("group_id")]
    public required long GroupId { get; set; }
    /// <summary>
    /// 是否允许匿名聊天
    /// </summary>
    [JsonProperty("enable")]
    public bool Enable { get; set; } = true;
}

public record SetGroupAnonymousRes
{

}