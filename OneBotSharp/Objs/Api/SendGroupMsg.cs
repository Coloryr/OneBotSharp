using Newtonsoft.Json;
using OneBotSharp.Objs.Message;

namespace OneBotSharp.Objs.Api;

public record SendGroupMsg
{
    /// <summary>
    /// 群号
    /// </summary>
    [JsonProperty("group_id")]
    public required long GroupId { get; set; }
    /// <summary>
    /// 要发送的内容
    /// </summary>
    [JsonProperty("message")]
    public object Message { get; set; }
    /// <summary>
    /// 解析 CQ 码
    /// </summary>
    [JsonProperty("auto_escape")]
    public bool AutoEscape { get; set; }

    /// <summary>
    /// 创建消息
    /// </summary>
    /// <param name="group">群号</param>
    /// <param name="msg">消息</param>
    /// <param name="escape">解析 CQ 码</param>
    /// <returns></returns>
    public static SendGroupMsg Build(long group, string msg, bool escape = false)
    {
        return new()
        {
            GroupId = group,
            Message = msg,
            AutoEscape = escape
        };
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="group">群号</param>
    /// <param name="msg">消息</param>
    /// <returns></returns>
    public static SendGroupMsg Build(long group, List<MsgBase> msg)
    {
        return new()
        {
            GroupId = group,
            Message = msg,
            AutoEscape = false
        };
    }
}

public record SendGroupMsgRes
{
    /// <summary>
    /// 消息 ID
    /// </summary>
    [JsonProperty("message_id")]
    public int MessageId { get; set; }
}