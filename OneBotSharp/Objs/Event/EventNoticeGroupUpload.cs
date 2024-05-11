using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Event;

/// <summary>
/// 群文件上传
/// </summary>
public record EventNoticeGroupUpload : EventNotice
{
    public override string NoticeType => NoticeGroupUpload;

    /// <summary>
    /// 群号
    /// </summary>
    [JsonProperty("group_id")]
    public long GroupId { get; set; }
    /// <summary>
    /// 文件信息
    /// </summary>
    [JsonProperty("file")]
    public GroupFile File { get; set; }
    public record GroupFile
    {
        /// <summary>
        /// 文件 ID
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// 文件大小（字节数）
        /// </summary>
        [JsonProperty("size")]
        public long Size { get; set; }
        [JsonProperty("busid")]
        public long Busid { get; set; }
    }

    public static new EventNoticeGroupUpload? JsonParse(JObject obj)
    {
        var msg = obj.ToObject<EventNoticeGroupUpload>();
        if (msg == null)
        {
            return null;
        }

        return msg;
    }
}
