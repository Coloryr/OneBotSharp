using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OneBotSharp.Objs.Api;

public record GetGroupHonorInfo
{
    /// <summary>
    /// 群号
    /// </summary>
    [JsonProperty("group_id")]
    public required long GroupId { get; set; }
    /// <summary>
    /// 要获取的群荣誉类型，可以从Enums.GroupHonorType取
    /// </summary>
    [JsonProperty("type")]
    public required string Type { get; set; }

    public static GetGroupHonorInfo BuildAll(long group)
    {
        return new()
        { 
            GroupId = group, 
            Type = Enums.GroupHonorType.All 
        };
    }
}

public record GetGroupHonorInfoRes
{
    /// <summary>
    /// 群号
    /// </summary>
    [JsonProperty("group_id")]
    public long GroupId { get; set; }
    [JsonProperty("current_talkative")]
    public Talkative CurrentTalkative { get; set; }
    public record Talkative
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
        /// 头像 URL
        /// </summary>
        [JsonProperty("avatar")]
        public string Avatar { get; set; }
        /// <summary>
        /// 持续天数
        /// </summary>
        [JsonProperty("day_count")]
        public int Count { get; set; }
    }
    /// <summary>
    /// 历史龙王
    /// </summary>
    [JsonProperty("talkative_list")]
    public List<Item> TalkativeList { get; set; }
    /// <summary>
    /// 群聊之火
    /// </summary>
    [JsonProperty("performer_list")]
    public List<Item> PerformerList { get; set; }
    /// <summary>
    /// 群聊炽焰
    /// </summary>
    [JsonProperty("legend_list")]
    public List<Item> LegendList { get; set; }
    /// <summary>
    /// 冒尖小春笋
    /// </summary>
    [JsonProperty("strong_newbie_list")]
    public List<Item> StrongNewbieList { get; set; }
    /// <summary>
    /// 快乐之源
    /// </summary>
    [JsonProperty("emotion_list")]
    public List<Item> EmotionList { get; set; }
    public record Item
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
        /// 头像 URL
        /// </summary>
        [JsonProperty("avatar")]
        public string Avatar { get; set; }
        /// <summary>
        /// 荣誉描述
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
