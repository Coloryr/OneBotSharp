using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OneBotSharp.Objs.Event;
using OneBotSharp.Objs.Message;

namespace OneBotSharp.Objs.Api;

public record GetMsg
{
    /// <summary>
    /// 消息 ID
    /// </summary>
    [JsonProperty("message_id")]
    public required int MessageId { get; set; }
}

public record GetMsgRes
{
    /// <summary>
    /// 发送时间
    /// </summary>
    [JsonProperty("time")]
    public int Time { get; set; }
    /// <summary>
    /// 消息类型
    /// </summary>
    [JsonProperty("message_type")]
    public string MessageType { get; set; }
    /// <summary>
    /// 消息 ID
    /// </summary>
    [JsonProperty("message_id")]
    public int MessageId { get; set; }
    /// <summary>
    /// 消息真实 ID
    /// </summary>
    [JsonProperty("real_id")]
    public int RealId { get; set; }
    /// <summary>
    /// 发送人信息
    /// </summary>
    [JsonProperty("sender")]
    public object Sender { get; set; }
    /// <summary>
    /// 使用Messages来获取
    /// </summary>
    [JsonProperty("message")]
    public object Message { get; set; }

    /// <summary>
    /// 消息内容
    /// </summary>
    public List<MsgBase> Messages = [];

    public void Parse()
    {
        if (Message is string str)
        {
            Messages = CqHelper.ParseMsg(str);
        }
        else if (Message is JArray list)
        {
            foreach (var item in list)
            {
                var item1 = (item as JObject)!;
                var msg = MsgBase.ParseRecv(item1);
                if (msg != null)
                {
                    Messages.Add(msg);
                }
            }
        }

        if (Sender is JObject obj)
        {
            if (MessageType == Enums.MessageType.Group)
            {
                Sender = obj.ToObject<EventGroupMessage.EventSender>() ?? Sender;
            }
            else if (MessageType == Enums.MessageType.Private)
            {
                Sender = obj.ToObject<EventPrivateMessage.EventSender>() ?? Sender;
            }
        }
    }
}
