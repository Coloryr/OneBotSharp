using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OneBotSharp.Objs.Message;

namespace OneBotSharp.Objs.Api;

public record GetForwardMsg
{
    /// <summary>
    /// 合并转发 ID
    /// </summary>
    [JsonProperty("id")]
    public required string Id { get; set; }
}

public record GetForwardMsgRes
{
    /// <summary>
    /// 从Messages取消息
    /// </summary>
    [JsonProperty("message")]
    public object Message;

    /// <summary>
    /// 消息列表
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
    }
}