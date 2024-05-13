using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OneBotSharp.Objs.Api;

public record SetGroupCard
{
    /// <summary>
    /// 群号
    /// </summary>
    [JsonProperty("group_id")]
    public required long GroupId { get; set; }
    /// <summary>
    /// 要设置的 QQ 号
    /// </summary>
    [JsonProperty("user_id")]
    public required long UserId { get; set; }
    /// <summary>
    /// 群名片内容，不填或空字符串表示删除群名片
    /// </summary>
    [JsonProperty("card")]
    public string Card { get; set; }
}

public record SetGroupCardRes
{ 
    
}
