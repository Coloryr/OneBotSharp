using Newtonsoft.Json;

namespace OneBotSharp.Objs.Api;

public record SetRestart
{
    /// <summary>
    /// 要延迟的毫秒数，如果默认情况下无法重启，可以尝试设置延迟为 2000 左右
    /// </summary>
    [JsonProperty("delay")]
    public int Delay { get; set; }
}

public record SetRestartRes
{

}
