using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OneBotSharp.Objs.Api;

public record SetGroupKick
{
    /// <summary>
    /// 群号
    /// </summary>
    [JsonProperty("group_id")]
    public required long GroupId { get; set; }
    /// <summary>
    /// 要踢的 QQ 号
    /// </summary>
    [JsonProperty("user_id")]
    public required long UserId { get; set; }
    /// <summary>
    /// 拒绝此人的加群请求
    /// </summary>
    [JsonProperty("reject_add_request")]
    public bool RejectAddRequest { get; set; }
}

public record SetGroupKickRes
{ 
    
}