using Newtonsoft.Json;

namespace OneBotSharp.Objs.Api;

public record GetStrangerInfo
{
    /// <summary>
    /// QQ 号
    /// </summary>
    [JsonProperty("user_id")]
    public required long UserId { get; set; }
    /// <summary>
    /// 是否不使用缓存
    /// </summary>
    [JsonProperty("no_cache")]
    public bool NoCache { get; set; }
}

public record GetStrangerInfoRes
{
    /// <summary>
    /// QQ 号
    /// </summary>
    [JsonProperty("user_id")]
    public long UserId { get; set; }
    /// <summary>
    /// 昵称
    /// </summary>
    [JsonProperty("nickname")]
    public string Nickname { get; set; }
    /// <summary>
    /// 性别
    /// </summary>
    [JsonProperty("sex")]
    public string Sex { get; set; }
    /// <summary>
    /// 年龄
    /// </summary>
    [JsonProperty("age")]
    public int Arg { get; set; }
}
