using Newtonsoft.Json;

namespace OneBotSharp.Objs.Api;

public record GetLoginInfo
{

}

public record GetLoginInfoRes
{
    /// <summary>
    /// QQ 号
    /// </summary>
    [JsonProperty("user_id")]
    public long UserId { get; set; }
    /// <summary>
    /// QQ 昵称
    /// </summary>
    [JsonProperty("nickname")]
    public string Nickname { get; set; }
}
