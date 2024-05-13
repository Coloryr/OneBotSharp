using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OneBotSharp.Objs.Api;

public record GetGroupList
{
     
}

public record GetGroupListRes
{
    /// <summary>
    /// 群号
    /// </summary>
    [JsonProperty("group_id")]
    public long GroupId { get; set; }
    /// <summary>
    /// 群名称
    /// </summary>
    [JsonProperty("group_name")]
    public string GroupName { get; set; }
    /// <summary>
    /// 成员数
    /// </summary>
    [JsonProperty("member_count")]
    public int Count { get; set; }
    /// <summary>
    /// 最大成员数（群容量）
    /// </summary>
    [JsonProperty("max_member_count")]
    public int MaxCount { get; set; }
}