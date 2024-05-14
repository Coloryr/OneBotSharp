using Newtonsoft.Json;

namespace OneBotSharp.Objs.Api;

public record GetCsrfToken
{
}

public record GetCsrfTokenRes
{
    /// <summary>
    /// CSRF Token
    /// </summary>
    [JsonProperty("token")]
    public int Token { get; set; }
}