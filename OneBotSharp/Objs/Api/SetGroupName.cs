using Newtonsoft.Json;

namespace OneBotSharp.Objs.Api;

public record SetGroupName
{
    /// <summary>
    /// 群号
    /// </summary>
    [JsonProperty("group_id")]
    public required long GroupId { get; set; }
    /// <summary>
    /// 新群名
    /// </summary>
    [JsonProperty("group_name")]
    public string Name { get; set; }
}

public record SetGroupNameRes
{

}
