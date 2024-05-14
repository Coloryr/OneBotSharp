using Newtonsoft.Json;

namespace OneBotSharp.Objs.Api;

public record GetImage
{
    /// <summary>
    /// 收到的图片文件名
    /// </summary>
    [JsonProperty("file")]
    public required string File { get; set; }
}

public record GetImageRes
{
    /// <summary>
    /// 下载后的图片文件路径
    /// </summary>
    [JsonProperty("file")]
    public string File { get; set; }
}