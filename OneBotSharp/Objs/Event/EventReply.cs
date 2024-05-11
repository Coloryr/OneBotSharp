using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OneBotSharp.Objs.Event;

public record EventReply
{
    public record Private
    {
        /// <summary>
        /// 要回复的内容
        /// </summary>
        [JsonProperty("reply")]
        public object Message { get; set; }
        /// <summary>
        /// 不解析 CQ 码
        /// </summary>
        [JsonProperty("auto_escape")]
        public bool Escape { get; set; }
    }

    public record Group : Private
    {
        /// <summary>
        /// 回复开头 at 发送者
        /// </summary>
        [JsonProperty("at_sender")]
        public bool At { get; set; }
        /// <summary>
        /// 撤回该条消息
        /// </summary>
        [JsonProperty("delete")]
        public bool Delete { get; set; }
        /// <summary>
        /// 发送者踢出群组
        /// </summary>
        [JsonProperty("kick")]
        public bool Kick { get; set; }
        /// <summary>
        /// 把发送者禁言
        /// </summary>
        [JsonProperty("ban")]
        public bool Ban { get; set; }
        /// <summary>
        /// 禁言时长
        /// </summary>
        [JsonProperty("ban_duration")]
        public long Time { get; set; }
    }
}
