using Newtonsoft.Json;

namespace OneBotSharp.Objs.Api;

public record SetGroupAdmin
{
    /// <summary>
    /// 群号
    /// </summary>
    [JsonProperty("group_id")]
    public required long GroupId { get; set; }
    /// <summary>
    /// 要设置管理员的 QQ 号
    /// </summary>
    [JsonProperty("user_id")]
    public required long UserId { get; set; }
    /// <summary>
    /// true 为设置，false 为取消
    /// </summary>
    [JsonProperty("enable")]
    public bool Enable { get; set; } = true;
}

public record SetGroupAdminRes
{

}
