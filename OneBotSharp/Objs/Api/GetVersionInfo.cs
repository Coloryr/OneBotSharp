using Newtonsoft.Json;

namespace OneBotSharp.Objs.Api;

public record GetVersionInfo
{

}

public record GetVersionInfoRes
{
    /// <summary>
    /// 应用标识
    /// </summary>
    [JsonProperty("app_name")]
    public string AppName { get; set; }
    /// <summary>
    /// 应用版本
    /// </summary>
    [JsonProperty("app_version")]
    public string AppVersion { get; set; }
    /// <summary>
    /// OneBot 标准版本
    /// </summary>
    [JsonProperty("protocol_version")]
    public string ProtocolVersion { get; set; }
}
