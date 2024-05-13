using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OneBotSharp.Objs.Api;

public record GetRecord
{
    /// <summary>
    /// 收到的语音文件名
    /// </summary>
    [JsonProperty("file")]
    public required string File { get; set; }
    /// <summary>
    /// 要转换到的格式
    /// </summary>
    [JsonProperty("out_format")]
    public required string Format { get; set; }
}

public record GetRecordRes
{
    /// <summary>
    /// 转换后的语音文件路径
    /// </summary>
    [JsonProperty("file")]
    public string File { get; set; }
}