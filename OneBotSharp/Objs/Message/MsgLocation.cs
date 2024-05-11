using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Message;

public record MsgLocation : MsgBase
{
    [JsonIgnore]
    public const string MsgType = "location";

    public override string Type => MsgType;

    [JsonProperty("data")]
    public MsgData Data { get; set; }
    public record MsgData
    {
        [JsonProperty("lat")]
        public string? Lat { get; set; }
        [JsonProperty("lon")]
        public string? Lon { get; set; }
        [JsonProperty("title")]
        public string? Title { get; set; }
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

    public static MsgLocation MsgRecvParse(CqCode code)
    {
        if (code.Type != MsgType)
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

    public static MsgLocation MsgSendParse(CqCode code)
    {
        return MsgRecvParse(code);
    }

    public static MsgLocation? JsonParse(JObject text, bool send)
    {
        return text.ToObject<MsgLocation>();
    }
}
