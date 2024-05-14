using Newtonsoft.Json;

namespace OneBotSharp.Objs.Api;

public record SetFriendAddRequest
{
    /// <summary>
    /// 加好友请求的 flag
    /// </summary>
    [JsonProperty("flag")]
    public required string Flag { get; set; }
    /// <summary>
    /// 是否同意请求
    /// </summary>
    [JsonProperty("approve")]
    public bool Approve { get; set; } = true;
    /// <summary>
    /// 添加后的好友备注
    /// </summary>
    [JsonProperty("remark")]
    public string Remark { get; set; }
}

public record SetFriendAddRequestRes
{

}
