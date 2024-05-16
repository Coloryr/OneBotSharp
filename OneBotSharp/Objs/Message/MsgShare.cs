using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Message;

public record MsgShare : MsgBase
{
    public override string Type => Enums.MsgType.Share;

    [JsonProperty("data")]
    public MsgData Data { get; set; }
    public record MsgData
    {
        [JsonProperty("url")]
        public string? Url { get; set; }
        [JsonProperty("title")]
        public string? Title { get; set; }
        [JsonProperty("content")]
        public string? Content { get; set; }
        [JsonProperty("image")]
        public string? Image { get; set; }
    }

    public override string BuildSendCq()
    {
        var code = new CqCode { Type = Type };
        if (!string.IsNullOrWhiteSpace(Data.Url))
        {
            code.Datas.Add("url", Data.Url);
        }
        if (!string.IsNullOrWhiteSpace(Data.Title))
        {
            code.Datas.Add("title", Data.Title);
        }
        if (!string.IsNullOrWhiteSpace(Data.Content))
        {
            code.Datas.Add("content", Data.Content);
        }
        if (!string.IsNullOrWhiteSpace(Data.Image))
        {
            code.Datas.Add("image", Data.Image);
        }

        return code.ToString();
    }

    public override string BuildRecvCq()
    {
        return BuildSendCq();
    }

    public override string ToString()
    {
        return $"[分享链接]";
    }

    public static MsgShare Build(string url, string title, string content, string image)
    {
        return new()
        {
            Data = new()
            {
                Url = url,
                Title = title,
                Content = content,
                Image = image
            }
        };
    }

    public static MsgShare RecvParse(CqCode code)
    {
        if (code.Type != Enums.MsgType.Share)
        {
            throw new ArgumentException("cqcode type error");
        }

        return new()
        {
            Data = new()
            {
                Url = code["url"],
                Title = code["title"],
                Content = code["content"],
                Image = code["image"]
            }
        };
    }

    public static MsgShare SendParse(CqCode code)
    {
        return RecvParse(code);
    }

    public static MsgShare? JsonParse(JObject text, bool send)
    {
        return text.ToObject<MsgShare>();
    }
}
