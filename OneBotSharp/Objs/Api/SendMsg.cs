using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OneBotSharp.Objs.Message;

namespace OneBotSharp.Objs.Api;

public record SendMsg
{
    /// <summary>
    /// 消息类型
    /// </summary>
    [JsonProperty("message_type")]
    public required string MessageType { get; set; }
    /// <summary>
    /// 群号
    /// </summary>
    [JsonProperty("group_id")]
    public long GroupId { get; set; }
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
    /// 对方 QQ 号
    /// </summary>
    [JsonProperty("user_id")]
    public long UserId { get; set; }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="user">QQ号</param>
    /// <param name="group">群号</param>
    /// <param name="msg">消息</param>
    /// <param name="escape">解析 CQ 码</param>
    /// <returns></returns>
    public static SendMsg BuildPrivate(long user, long group, string msg, bool escape = false)
    {
        return new()
        {
            MessageType = Enums.MessageType.Private,
            UserId = user,
            GroupId = group,
            Message = msg,
            AutoEscape = escape
        };
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="user">QQ号</param>
    /// <param name="group">群号</param>
    /// <param name="msg">消息</param>
    /// <returns></returns>
    public static SendMsg BuildPrivate(long user, long group, List<MsgBase> msg)
    {
        return new()
        {
            MessageType = Enums.MessageType.Private,
            UserId = user,
            GroupId = group,
            Message = msg,
            AutoEscape = false
        };
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="user">QQ号</param>
    /// <param name="group">群号</param>
    /// <param name="msg">消息</param>
    /// <param name="escape">解析 CQ 码</param>
    /// <returns></returns>
    public static SendMsg BuildGroup(long user, long group, string msg, bool escape = false)
    {
        return new()
        {
            MessageType = Enums.MessageType.Group,
            UserId = user,
            GroupId = group,
            Message = msg,
            AutoEscape = escape
        };
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="user">QQ号</param>
    /// <param name="group">群号</param>
    /// <param name="msg">消息</param>
    /// <returns></returns>
    public static SendMsg BuildGroup(long user, long group, List<MsgBase> msg)
    {
        return new()
        {
            MessageType = Enums.MessageType.Group,
            UserId = user,
            GroupId = group,
            Message = msg,
            AutoEscape = false
        };
    }
}

public record SendMsgRes
{
    /// <summary>
    /// 消息 ID
    /// </summary>
    [JsonProperty("message_id")]
    public int MessageId { get; set; }
}