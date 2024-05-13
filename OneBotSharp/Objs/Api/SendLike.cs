using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OneBotSharp.Objs.Api;

public record SendLike
{
    /// <summary>
    /// 对方 QQ 号
    /// </summary>
    [JsonProperty("user_id")]
    public required long UserId { get; set; }
    /// <summary>
    /// 赞的次数，每个好友每天最多 10 次
    /// </summary>
    [JsonProperty("times")]
    public long Times { get; set; } = 1;
}

public record SendLikeRes
{ 
    
}
