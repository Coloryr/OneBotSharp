using OneBotSharp.Objs.Message;

namespace OneBotSharp.Objs.Event;

public static class EventHelper
{
    /// <summary>
    /// 回复私聊消息
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="message">消息</param>
    /// <param name="cqcpde">解析CQ码</param>
    public static void BuildReply(this EventPrivateMessage obj, string message, bool cqcpde = false)
    {
        obj.Reply = new EventReply.Private()
        {
            Message = message,
            Escape = cqcpde
        };
    }

    /// <summary>
    /// 回复私聊消息
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="message">消息</param>
    public static void BuildReply(this EventPrivateMessage obj, List<MsgBase> message)
    {
        obj.Reply = new EventReply.Private()
        {
            Message = message,
            Escape = false
        };
    }

    /// <summary>
    /// 回复群消息
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="message">消息</param>
    /// <param name="cqcpde">不解析CQ码</param>
    /// <param name="at">同时@群员</param>
    public static void BuildReply(this EventGroupMessage obj, string message, bool cqcpde = false, bool at = true)
    {
        obj.Reply = new EventReply.Group()
        {
            Message = message,
            Escape = cqcpde,
            At = at
        };
    }

    /// <summary>
    /// 回复群消息
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="message">消息</param>
    /// <param name="at">同时@群员</param>
    public static void BuildReply(this EventGroupMessage obj, List<MsgBase> message, bool at = true)
    {
        obj.Reply = new EventReply.Group()
        {
            Message = message,
            Escape = false,
            At = at
        };
    }

    /// <summary>
    /// 对群员进行处理
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="delete">撤回消息</param>
    /// <param name="kick">踢出</param>
    /// <param name="ban">禁言</param>
    /// <param name="time">禁言时间（秒）</param>
    public static void BuildReply(this EventGroupMessage obj, bool delete, bool kick, bool ban, int time)
    {
        obj.Reply = new EventReply.Group()
        {
            Delete = delete,
            Kick = kick,
            Ban = ban,
            Time = time
        };
    }

    /// <summary>
    /// 是否同意加好友请求
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="approve">是否同意</param>
    /// <param name="data">好友备注</param>
    public static void BuildReply(this EventRequestFriend obj, bool approve, string data = "")
    {
        obj.Reply = new EventReply.RequestFriend()
        {
            Approve = approve,
            Remark = data
        };
    }

    /// <summary>
    /// 是否同意入群
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="approve">是否同意</param>
    /// <param name="data">拒绝理由</param>
    public static void BuildReply(this EventRequestGroup obj, bool approve, string data = "")
    {
        obj.Reply = new EventReply.RequestGroup()
        {
            Approve = approve,
            Reason = data
        };
    }
}
