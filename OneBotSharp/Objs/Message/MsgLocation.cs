using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Message;

/// <summary>
/// 位置
/// </summary>
public record MsgLocation : MsgBase
{
    public override string Type => Enums.MsgType.Location;

    [JsonProperty("data")]
    public MsgData Data { get; set; }
    public record MsgData
    {
        /// <summary>
        /// 纬度
        /// </summary>
        [JsonProperty("lat")]
        public string? Lat { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        [JsonProperty("lon")]
        public string? Lon { get; set; }
        /// <summary>
        /// 发送时可选，标题
        /// </summary>
        [JsonProperty("title")]
        public string? Title { get; set; }
        /// <summary>
        /// 发送时可选，内容描述
        /// </summary>
        [JsonProperty("content")]
        public string? Content { get; set; }
    }

    public override string BuildSendCq()
    {
        var code = new CqCode { Type = Type };
        if (!string.IsNullOrWhiteSpace(Data.Lat))
        {
            code.Datas.Add("lat", Data.Lat);
        }
        if (!string.IsNullOrWhiteSpace(Data.Lon))
        {
            code.Datas.Add("lon", Data.Lon);
        }
        if (!string.IsNullOrWhiteSpace(Data.Title))
        {
            code.Datas.Add("title", Data.Title);
        }
        if (!string.IsNullOrWhiteSpace(Data.Content))
        {
            code.Datas.Add("content", Data.Content);
        }
        return code.ToString();
    }

    public override string BuildRecvCq()
    {
        return BuildSendCq();
    }

    public override string ToString()
    {
        var str = $"位置信息：{Data.Lat},{Data.Lon}";
        if (Data.Title is { } text)
        {
            str += "," + text;
        }
        if (Data.Content is { } content)
        {
            str += "," + content;
        }

        return str;
    }

    public static MsgLocation Build(string lat, string lon, string? title = null, string? content = null)
    {
        return new()
        {
            Data = new()
            {
                Lat = lat,
                Lon = lon,
                Title = title,
                Content = content
            }
        };
    }

    public static MsgLocation RecvParse(CqCode code)
    {
        if (code.Type != Enums.MsgType.Location)
        {
            throw new ArgumentException("cqcode type error");
        }

        var title = code["title"];
        var content = code["content"];

        return new()
        {
            Data = new()
            {
                Lat = code["lat"],
                Lon = code["lon"],
                Title = title,
                Content = content
            }
        };
    }

    public static MsgLocation SendParse(CqCode code)
    {
        return RecvParse(code);
    }

    public static MsgLocation? JsonParse(JObject text, bool send)
    {
        return text.ToObject<MsgLocation>();
    }
}
