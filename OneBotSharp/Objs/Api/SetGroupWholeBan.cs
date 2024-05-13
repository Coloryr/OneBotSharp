using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OneBotSharp.Objs.Api;

public record SetGroupWholeBan
{
    /// <summary>
    /// 群号
    /// </summary>
    [JsonProperty("group_id")]
    public required long GroupId { get; set; }
    /// <summary>
    /// 是否禁言
    /// </summary>
    [JsonProperty("enable")]
    public bool Enable { get; set; } = true;
}

public record SetGroupWholeBanRes
{ 
    
}
