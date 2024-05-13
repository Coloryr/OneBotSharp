using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OneBotSharp.Objs.Api;

public record SetGroupSpecialTitle
{
    /// <summary>
    /// 群号
    /// </summary>
    [JsonProperty("group_id")]
    public required long GroupId { get; set; }
    /// <summary>
    /// 群号
    /// </summary>
    [JsonProperty("user_id")]
    public required long UserId { get; set; }
    /// <summary>
    /// 专属头衔，不填或空字符串表示删除专属头衔
    /// </summary>
    [JsonProperty("special_title")]
    public string Title { get; set; }
    /// <summary>
    /// 专属头衔有效期，单位秒，-1 表示永久
    /// </summary>
    [JsonProperty("duration"), Obsolete("此项似乎没有效果，可能是只有某些特殊的时间长度有效，有待测试")]
    public long Duration { get; set; } = -1;
}

public record SetGroupSpecialTitleRes
{ 
    
}
