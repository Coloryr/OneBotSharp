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
        { Enums.MsgType.Face, (MsgFace.RecvParse, MsgFace.SendParse) },
        { Enums.MsgType.Image, (MsgImage.RecvParse, MsgImage.SendParse) },
        { Enums.MsgType.Record, (MsgRecord.RecvParse, MsgRecord.SendParse) },
        { Enums.MsgType.Video, (MsgVideo.RecvParse, MsgVideo.SendParse) },
        { Enums.MsgType.At, (MsgAt.RecvParse, MsgAt.SendParse) },
        { Enums.MsgType.Rps, (MsgRps.RecvParse, MsgRps.SendParse) },
        { Enums.MsgType.Dice, (MsgDice.RecvParse, MsgDice.SendParse) },
        { Enums.MsgType.Shake, (MsgShake.RecvParse, MsgShake.SendParse) },
        { Enums.MsgType.Poke, (MsgPoke.RecvParse, MsgPoke.SendParse) },
        { Enums.MsgType.Anonymous, (MsgAnonymous.RecvParse, MsgAnonymous.SendParse) },
        { Enums.MsgType.Share, (MsgShare.RecvParse, MsgShare.SendParse) },
        { Enums.MsgType.Contact, (MsgContact.RecvParse, MsgContact.SendParse) },
        { Enums.MsgType.Location, (MsgLocation.RecvParse, MsgLocation.SendParse) },
        { Enums.MsgType.Music, (MsgMusic.RecvParse, MsgMusic.SendParse) },
        { Enums.MsgType.Reply, (MsgReply.RecvParse, MsgReply.SendParse) },
        { Enums.MsgType.Forward, (MsgForward.RecvParse, MsgForward.SendParse) },
        { Enums.MsgType.Node, (MsgNode.RecvParse, MsgNode.SendParse) },
        { Enums.MsgType.Xml, (MsgXml.RecvParse, MsgXml.SendParse) },
    };

    public static readonly Dictionary<string, Func<JObject, bool, MsgBase?>> JsonParser = new()
    {
        { Enums.MsgType.Text, MsgText.JsonParse },
        { Enums.MsgType.Face, MsgFace.JsonParse },
        { Enums.MsgType.Image, MsgImage.JsonParse },
        { Enums.MsgType.Record, MsgRecord.JsonParse },
        { Enums.MsgType.Video, MsgVideo.JsonParse },
        { Enums.MsgType.At, MsgAt.JsonParse },
        { Enums.MsgType.Rps, MsgRps.JsonParse },
        { Enums.MsgType.Dice, MsgDice.JsonParse },
        { Enums.MsgType.Shake, MsgShake.JsonParse },
        { Enums.MsgType.Poke, MsgPoke.JsonParse },
        { Enums.MsgType.Anonymous, MsgAnonymous.JsonParse },
        { Enums.MsgType.Share, MsgShare.JsonParse },
        { Enums.MsgType.Contact, MsgContact.JsonParse },
        { Enums.MsgType.Location, MsgLocation.JsonParse },
        { Enums.MsgType.Music, MsgMusic.JsonParse },
        { Enums.MsgType.Reply, MsgReply.JsonParse },
        { Enums.MsgType.Forward, MsgForward.JsonParse },
        { Enums.MsgType.Node, MsgNode.JsonParse },
        { Enums.MsgType.Xml, MsgXml.JsonParse },
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
