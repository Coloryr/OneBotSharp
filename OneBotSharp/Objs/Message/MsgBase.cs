using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Message;

/// <summary>
/// 基础消息类型
/// </summary>
public abstract record MsgBase
{
    public static readonly Dictionary<string,
        (Func<CqCode, MsgBase> Recv, Func<CqCode, MsgBase> Send)> CqParser = new()
    {
        { MsgFace.MsgType, (MsgFace.MsgRecvParse, MsgFace.MsgSendParse) },
        { MsgImage.MsgType, (MsgImage.MsgRecvParse, MsgImage.MsgSendParse) },
        { MsgRecord.MsgType, (MsgRecord.MsgRecvParse, MsgRecord.MsgSendParse) },
        { MsgVideo.MsgType, (MsgVideo.MsgRecvParse, MsgVideo.MsgSendParse) },
        { MsgAt.MsgType, (MsgAt.MsgRecvParse, MsgAt.MsgSendParse) },
        { MsgRps.MsgType, (MsgRps.MsgRecvParse, MsgRps.MsgSendParse) },
        { MsgDice.MsgType, (MsgDice.MsgRecvParse, MsgDice.MsgSendParse) },
        { MsgShake.MsgType, (MsgShake.MsgRecvParse, MsgShake.MsgSendParse) },
        { MsgPoke.MsgType, (MsgPoke.MsgRecvParse, MsgPoke.MsgSendParse) },
        { MsgAnonymous.MsgType, (MsgAnonymous.MsgRecvParse, MsgAnonymous.MsgSendParse) },
        { MsgShare.MsgType, (MsgShare.MsgRecvParse, MsgShare.MsgSendParse) },
        { MsgContact.MsgType, (MsgContact.MsgRecvParse, MsgContact.MsgSendParse) },
        { MsgLocation.MsgType, (MsgLocation.MsgRecvParse, MsgLocation.MsgSendParse) },
        { MsgMusic.MsgType, (MsgMusic.MsgRecvParse, MsgMusic.MsgSendParse) },
        { MsgReply.MsgType, (MsgReply.MsgRecvParse, MsgReply.MsgSendParse) },
        { MsgForward.MsgType, (MsgForward.MsgRecvParse, MsgForward.MsgSendParse) },
        { MsgNode.MsgType, (MsgNode.MsgRecvParse, MsgNode.MsgSendParse) },
        { MsgXml.MsgType, (MsgXml.MsgRecvParse, MsgXml.MsgSendParse) },
    };

    public static readonly Dictionary<string, Func<JObject, bool, MsgBase?>> JsonParser = new()
    {
        { MsgText.MsgType, MsgText.JsonParse },
        { MsgFace.MsgType, MsgFace.JsonParse },
        { MsgImage.MsgType, MsgImage.JsonParse },
        { MsgRecord.MsgType, MsgRecord.JsonParse },
        { MsgVideo.MsgType, MsgVideo.JsonParse },
        { MsgAt.MsgType, MsgAt.JsonParse },
        { MsgRps.MsgType, MsgRps.JsonParse },
        { MsgDice.MsgType, MsgDice.JsonParse },
        { MsgShake.MsgType, MsgShake.JsonParse },
        { MsgPoke.MsgType, MsgPoke.JsonParse },
        { MsgAnonymous.MsgType, MsgAnonymous.JsonParse },
        { MsgShare.MsgType, MsgShare.JsonParse },
        { MsgContact.MsgType, MsgContact.JsonParse },
        { MsgLocation.MsgType, MsgLocation.JsonParse },
        { MsgMusic.MsgType, MsgMusic.JsonParse },
        { MsgReply.MsgType, MsgReply.JsonParse },
        { MsgForward.MsgType, MsgForward.JsonParse },
        { MsgNode.MsgType, MsgNode.JsonParse },
        { MsgXml.MsgType, MsgXml.JsonParse },
    };

    [JsonProperty("type")]
    public abstract string Type { get; }

    /// <summary>
    /// 构建发送到OneBot的CQ码
    /// </summary>
    /// <returns></returns>
    public abstract string BuildSendCq();
    /// <summary>
    /// 构建发送到插件的CQ码
    /// </summary>
    /// <returns></returns>
    public abstract string BuildRecvCq();

    /// <summary>
    /// 构建json消息
    /// </summary>
    /// <returns></returns>
    public virtual string BuildJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    /// <summary>
    /// 从插件接收到的CQ码进行解析
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public static MsgBase? ParseSend(CqCode code)
    {
        if (CqParser.TryGetValue(code.Type, out var value))
        {
            return value.Send(code);
        }

        return null;
    }

    /// <summary>
    /// 从OneBot接收到的CQ码进行解析
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public static MsgBase? ParseRecv(CqCode code)
    {
        if (CqParser.TryGetValue(code.Type, out var value))
        {
            return value.Recv(code);
        }

        return null;
    }

    /// <summary>
    /// 从插件接收到的json消息解析
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public static MsgBase? ParseSend(JObject code)
    {
        if (code.TryGetValue("type", out var value))
        {
            var type = value.ToString();
            if (JsonParser.TryGetValue(type, out var type1))
            {
                return type1(code, true);
            }
        }

        return null;
    }

    /// <summary>
    /// 从Onebot接收到的json消息解析
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public static MsgBase? ParseRecv(JObject code)
    {
        if (code.TryGetValue("type", out var value))
        {
            var type = value.ToString();
            if (JsonParser.TryGetValue(type, out var type1))
            {
                return type1(code, false);
            }
        }

        return null;
    }
}
