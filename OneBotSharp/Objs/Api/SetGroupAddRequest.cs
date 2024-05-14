using Newtonsoft.Json;

namespace OneBotSharp.Objs.Api;

public record SetGroupAddRequest
{
    /// <summary>
    /// 加群请求的 flag
    /// </summary>
    [JsonProperty("flag")]
    public required string Flag { get; set; }
    /// <summary>
    /// add 或 invite，请求类型
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; }
    /// <summary>
    /// 是否同意请求／邀请
    /// </summary>
    [JsonProperty("approve")]
    public bool Approve { get; set; } = true;
    /// <summary>
    /// 拒绝理由
    /// </summary>
    [JsonProperty("reason")]
    public string Reason { get; set; }
}

public record SetGroupAddRequestRes
{

}
