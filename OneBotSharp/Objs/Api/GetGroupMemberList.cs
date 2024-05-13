using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OneBotSharp.Objs.Api;

public record GetGroupMemberList
{
    /// <summary>
    /// 群号
    /// </summary>
    [JsonProperty("group_id")]
    public required long GroupId { get; set; }
}

public record GetGroupMemberListRes
{
    /// <summary>
    /// 群号
    /// </summary>
    [JsonProperty("group_id")]
    public long GroupId { get; set; }
    /// <summary>
    /// QQ 号
    /// </summary>
    [JsonProperty("user_id")]
    public long UserId { get; set; }
    /// <summary>
    /// 昵称
    /// </summary>
    [JsonProperty("nickname")]
    public string NickName { get; set; }
    /// <summary>
    /// 群名片／备注
    /// </summary>
    [JsonProperty("card")]
    public string Card { get; set; }
    /// <summary>
    /// 性别
    /// </summary>
    [JsonProperty("sex")]
    public string Sex { get; set; }
    /// <summary>
    /// 年龄
    /// </summary>
    [JsonProperty("age")]
    public int Age { get; set; }
    /// <summary>
    /// 地区
    /// </summary>
    [JsonProperty("area")]
    public string Area { get; set; }
    /// <summary>
    /// 加群时间戳
    /// </summary>
    [JsonProperty("join_time")]
    public int JoinTime { get; set; }
    /// <summary>
    /// 最后发言时间戳
    /// </summary>
    [JsonProperty("last_sent_time")]
    public int LastSentTime { get; set; }
    /// <summary>
    /// 成员等级
    /// </summary>
    [JsonProperty("level")]
    public string Level { get; set; }
    /// <summary>
    /// 角色
    /// </summary>
    [JsonProperty("role")]
    public string Role { get; set; }
    /// <summary>
    /// 是否不良记录成员
    /// </summary>
    [JsonProperty("unfriendly")]
    public bool Unfriendly { get; set; }
    /// <summary>
    /// 专属头衔
    /// </summary>
    [JsonProperty("title")]
    public string Title { get; set; }
    /// <summary>
    /// 专属头衔过期时间戳
    /// </summary>
    [JsonProperty("title_expire_time")]
    public int TitleExpireTime { get; set; }
    /// <summary>
    /// 是否允许修改群名片
    /// </summary>
    [JsonProperty("card_changeable")]
    public bool CardChangeable { get; set; }
}
