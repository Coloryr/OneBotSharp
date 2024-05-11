using System.Text;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Message;

/// <summary>
/// XML 消息
/// </summary>
public record MsgXml : MsgBase
{
    [JsonIgnore]
    public const string MsgType = "xml";

    public override string Type => MsgType;

    [JsonProperty("data")]
    public MsgData Data { get; set; }
    public record MsgData
    {
        /// <summary>
        /// XML 内容
        /// </summary>
        [JsonProperty("data")]
        public string? Data { get; set; }
    }

    public override string BuildSendCq()
    {
        return $"[CQ:xml,data={Data.Data}]";
    }

    public override string BuildRecvCq()
    {
        return BuildSendCq();
    }

    public static MsgXml Build(string xml)
    {
        return new()
        {
            Data = new()
            {
                Data = xml
            }
        };
    }

    public static MsgXml Build(XmlDocument document)
    {
        var builder = new StringBuilder();
        using var write = XmlWriter.Create(builder);
        document.Save(write);
        return new()
        {
            Data = new()
            {
                Data = builder.ToString()
            }
        };
    }

    public static MsgXml MsgRecvParse(CqCode code)
    {
        if (code.Type != MsgType)
        {
            throw new ArgumentException("cqcode type error");
        }

        return new()
        {
            Data = new()
            {
                Data = code["xml"]
            }
        };
    }

    public static MsgXml MsgSendParse(CqCode code)
    {
        return MsgRecvParse(code);
    }

    public static MsgXml? JsonParse(JObject text, bool send)
    {
        return text.ToObject<MsgXml>();
    }
}
