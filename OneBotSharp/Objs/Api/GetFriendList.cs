using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OneBotSharp.Objs.Api;

public record GetFriendList
{
    
}

public record GetFriendListRes
{
    /// <summary>
    /// QQ 号
    /// </summary>
    [JsonProperty("user_id")]
    public long UserId { get; set; }
    /// <summary>
    /// 昵称
    /// </summary>
    [JsonProperty("nickname")]
    public string Nickname { get; set; }
    /// <summary>
    /// 备注名
    /// </summary>
    [JsonProperty("remark")]
    public string Remark { get; set; }
}