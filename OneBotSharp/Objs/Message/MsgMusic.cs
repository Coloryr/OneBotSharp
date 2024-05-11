using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Message;

public record MsgMusic : MsgBase
{
    [JsonIgnore]
    public const string MusicQQ = "qq";
    [JsonIgnore]
    public const string Music163 = "163";
    [JsonIgnore]
    public const string MusicXM = "xm";
    [JsonIgnore]
    public const string MusicCustom = "custom";
    [JsonIgnore]
    public const string MsgType = "music";

    public override string Type => MsgType;

    [JsonProperty("data")]
    public MsgData Data { get; set; }
    public record MsgData
    {
        [JsonProperty("type")]
        public string? Type { get; set; }
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("url")]
        public string? Url { get; set; }
        [JsonProperty("audio")]
        public string? Audio { get; set; }
        [JsonProperty("title")]
        public string? Title { get; set; }
        [JsonProperty("content")]
        public string? Content { get; set; }
        [JsonProperty("image")]
        public string? Image { get; set; }
    }

    public override string BuildSendCq()
    {
        if (Data.Type == MusicCustom)
        {
            var code = new CqCode { Type = Type };
            code.Datas.Add("url", Data.Url);
            code.Datas.Add("audio", Data.Audio);
            code.Datas.Add("title", Data.Title);
            if (!string.IsNullOrWhiteSpace(Data.Content))
            {
                code.Datas.Add("content", Data.Content);
            }
            if (!string.IsNullOrWhiteSpace(Data.Image))
            {
                code.Datas.Add("image", Data.Image);
            }
        }
        return $"[CQ:music,type={Data.Type},id={Data.Id}]";
    }

    public override string BuildRecvCq()
    {
        return BuildSendCq();
    }

    public static MsgMusic BuildQQ(string id)
    {
        return new()
        {
            Data = new()
            {
                Type = MusicQQ,
                Id = id
            }
        };
    }

    public static MsgMusic Build163(string id)
    {
        return new()
        {
            Data = new()
            {
                Type = Music163,
                Id = id
            }
        };
    }

    public static MsgMusic BuildXM(string id)
    {
        return new()
        {
            Data = new()
            {
                Type = MusicXM,
                Id = id
            }
        };
    }

    public static MsgMusic BuildCustom(string url, string audio, string title, string? content = null, string? image = null)
    {
        return new()
        {
            Data = new()
            {
                Type = MusicCustom,
                Url = url,
                Audio = audio,
                Title = title,
                Content = content,
                Image = image
            }
        };
    }

    public static MsgMusic MsgRecvParse(CqCode code)
    {
        throw new Exception("can not decode recv msg music");
    }

    public static MsgMusic MsgSendParse(CqCode code)
    {
        if (code.Type != MsgType)
        {
            throw new ArgumentException("cqcode type error");
        }

        var type = code["type"];
        if (type == MusicCustom)
        {
            return new()
            {
                Data = new()
                {
                    Type = type,
                    Title = code["title"],
                    Audio = code["audio"],
                    Url = code["url"],
                    Image = code["image"],
                    Content = code["content"]
                }
            };
        }

        return new()
        {
            Data = new()
            {
                Type = type,
                Id = code["id"]
            }
        };
    }

    public static MsgMusic? JsonParse(JObject text, bool send)
    {
        return text.ToObject<MsgMusic>();
    }
}
